using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200036C RID: 876
[AddComponentMenu("KMonoBehaviour/scripts/BrainScheduler")]
public class BrainScheduler : KMonoBehaviour, IRenderEveryTick, ICPULoad
{
	// Token: 0x1700005B RID: 91
	// (get) Token: 0x060011EC RID: 4588 RVA: 0x0005E788 File Offset: 0x0005C988
	private bool isAsyncPathProbeEnabled
	{
		get
		{
			return !TuningData<BrainScheduler.Tuning>.Get().disableAsyncPathProbes;
		}
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x0005E798 File Offset: 0x0005C998
	protected override void OnPrefabInit()
	{
		this.brainGroups.Add(new BrainScheduler.DupeBrainGroup());
		this.brainGroups.Add(new BrainScheduler.CreatureBrainGroup());
		Components.Brains.Register(new Action<Brain>(this.OnAddBrain), new Action<Brain>(this.OnRemoveBrain));
		CPUBudget.AddRoot(this);
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			CPUBudget.AddChild(this, brainGroup, brainGroup.LoadBalanceThreshold());
		}
		CPUBudget.FinalizeChildren(this);
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x0005E840 File Offset: 0x0005CA40
	private void OnAddBrain(Brain brain)
	{
		bool flag = false;
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			if (brain.HasTag(brainGroup.tag))
			{
				brainGroup.AddBrain(brain);
				flag = true;
			}
			Navigator component = brain.GetComponent<Navigator>();
			if (component != null)
			{
				component.executePathProbeTaskAsync = this.isAsyncPathProbeEnabled;
			}
		}
		DebugUtil.Assert(flag);
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x0005E8C8 File Offset: 0x0005CAC8
	private void OnRemoveBrain(Brain brain)
	{
		bool flag = false;
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			if (brain.HasTag(brainGroup.tag))
			{
				flag = true;
				brainGroup.RemoveBrain(brain);
			}
			Navigator component = brain.GetComponent<Navigator>();
			if (component != null)
			{
				component.executePathProbeTaskAsync = false;
			}
		}
		DebugUtil.Assert(flag);
	}

	// Token: 0x060011F0 RID: 4592 RVA: 0x0005E94C File Offset: 0x0005CB4C
	public float GetEstimatedFrameTime()
	{
		return TuningData<BrainScheduler.Tuning>.Get().frameTime;
	}

	// Token: 0x060011F1 RID: 4593 RVA: 0x0005E958 File Offset: 0x0005CB58
	public bool AdjustLoad(float currentFrameTime, float frameTimeDelta)
	{
		return false;
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x0005E95C File Offset: 0x0005CB5C
	public void RenderEveryTick(float dt)
	{
		if (Game.IsQuitting() || KMonoBehaviour.isLoadingScene)
		{
			return;
		}
		foreach (BrainScheduler.BrainGroup brainGroup in this.brainGroups)
		{
			brainGroup.RenderEveryTick(dt, this.isAsyncPathProbeEnabled);
		}
	}

	// Token: 0x060011F3 RID: 4595 RVA: 0x0005E9C4 File Offset: 0x0005CBC4
	protected override void OnForcedCleanUp()
	{
		CPUBudget.Remove(this);
		base.OnForcedCleanUp();
	}

	// Token: 0x0400099C RID: 2460
	public const float millisecondsPerFrame = 33.33333f;

	// Token: 0x0400099D RID: 2461
	public const float secondsPerFrame = 0.033333328f;

	// Token: 0x0400099E RID: 2462
	public const float framesPerSecond = 30.000006f;

	// Token: 0x0400099F RID: 2463
	private List<BrainScheduler.BrainGroup> brainGroups = new List<BrainScheduler.BrainGroup>();

	// Token: 0x02000F3C RID: 3900
	private class Tuning : TuningData<BrainScheduler.Tuning>
	{
		// Token: 0x0400536F RID: 21359
		public bool disableAsyncPathProbes;

		// Token: 0x04005370 RID: 21360
		public float frameTime = 5f;
	}

	// Token: 0x02000F3D RID: 3901
	private abstract class BrainGroup : ICPULoad
	{
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06006E61 RID: 28257 RVA: 0x0029C99F File Offset: 0x0029AB9F
		// (set) Token: 0x06006E62 RID: 28258 RVA: 0x0029C9A7 File Offset: 0x0029ABA7
		public Tag tag { get; private set; }

		// Token: 0x06006E63 RID: 28259 RVA: 0x0029C9B0 File Offset: 0x0029ABB0
		protected BrainGroup(Tag tag)
		{
			this.tag = tag;
			this.probeSize = this.InitialProbeSize();
			this.probeCount = this.InitialProbeCount();
			string text = tag.ToString();
			this.increaseLoadLabel = "IncLoad" + text;
			this.decreaseLoadLabel = "DecLoad" + text;
		}

		// Token: 0x06006E64 RID: 28260 RVA: 0x0029CA28 File Offset: 0x0029AC28
		public void AddBrain(Brain brain)
		{
			this.brains.Add(brain);
		}

		// Token: 0x06006E65 RID: 28261 RVA: 0x0029CA38 File Offset: 0x0029AC38
		public void RemoveBrain(Brain brain)
		{
			int num = this.brains.IndexOf(brain);
			if (num != -1)
			{
				this.brains.RemoveAt(num);
				this.OnRemoveBrain(num, ref this.nextUpdateBrain);
				this.OnRemoveBrain(num, ref this.nextPathProbeBrain);
			}
		}

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06006E66 RID: 28262 RVA: 0x0029CA7C File Offset: 0x0029AC7C
		// (set) Token: 0x06006E67 RID: 28263 RVA: 0x0029CA84 File Offset: 0x0029AC84
		public int probeSize { get; private set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06006E68 RID: 28264 RVA: 0x0029CA8D File Offset: 0x0029AC8D
		// (set) Token: 0x06006E69 RID: 28265 RVA: 0x0029CA95 File Offset: 0x0029AC95
		public int probeCount { get; private set; }

		// Token: 0x06006E6A RID: 28266 RVA: 0x0029CAA0 File Offset: 0x0029ACA0
		public bool AdjustLoad(float currentFrameTime, float frameTimeDelta)
		{
			bool flag = frameTimeDelta > 0f;
			int num = 0;
			int num2 = Math.Max(this.probeCount, Math.Min(this.brains.Count, CPUBudget.coreCount));
			num += num2 - this.probeCount;
			this.probeCount = num2;
			float num3 = Math.Min(1f, (float)this.probeCount / (float)CPUBudget.coreCount);
			float num4 = num3 * (float)this.probeSize;
			float num5 = num3 * (float)this.probeSize;
			float num6 = currentFrameTime / num5;
			float num7 = frameTimeDelta / num6;
			if (num == 0)
			{
				float num8 = num4 + num7 / (float)CPUBudget.coreCount;
				int num9 = MathUtil.Clamp(this.MinProbeSize(), this.IdealProbeSize(), (int)(num8 / num3));
				num += num9 - this.probeSize;
				this.probeSize = num9;
			}
			if (num == 0)
			{
				int num10 = Math.Max(1, (int)num3 + (flag ? 1 : (-1)));
				int num11 = MathUtil.Clamp(this.MinProbeSize(), this.IdealProbeSize(), (int)((num5 + num7) / (float)num10));
				int num12 = Math.Min(this.brains.Count, num10 * CPUBudget.coreCount);
				num += num12 - this.probeCount;
				this.probeCount = num12;
				this.probeSize = num11;
			}
			if (num == 0 && flag)
			{
				int num13 = this.probeSize + this.ProbeSizeStep();
				num += num13 - this.probeSize;
				this.probeSize = num13;
			}
			if (num >= 0 && num <= 0 && this.brains.Count > 0)
			{
				global::Debug.LogWarning("AdjustLoad() failed");
			}
			return num != 0;
		}

		// Token: 0x06006E6B RID: 28267 RVA: 0x0029CC1A File Offset: 0x0029AE1A
		private void IncrementBrainIndex(ref int brainIndex)
		{
			brainIndex++;
			if (brainIndex == this.brains.Count)
			{
				brainIndex = 0;
			}
		}

		// Token: 0x06006E6C RID: 28268 RVA: 0x0029CC34 File Offset: 0x0029AE34
		private void ClampBrainIndex(ref int brainIndex)
		{
			brainIndex = MathUtil.Clamp(0, this.brains.Count - 1, brainIndex);
		}

		// Token: 0x06006E6D RID: 28269 RVA: 0x0029CC4D File Offset: 0x0029AE4D
		private void OnRemoveBrain(int removedIndex, ref int brainIndex)
		{
			if (removedIndex < brainIndex)
			{
				brainIndex--;
				return;
			}
			if (brainIndex == this.brains.Count)
			{
				brainIndex = 0;
			}
		}

		// Token: 0x06006E6E RID: 28270 RVA: 0x0029CC70 File Offset: 0x0029AE70
		private void AsyncPathProbe()
		{
			int probeSize = this.probeSize;
			this.pathProbeJob.Reset(null);
			for (int num = 0; num != this.brains.Count; num++)
			{
				this.ClampBrainIndex(ref this.nextPathProbeBrain);
				Brain brain = this.brains[this.nextPathProbeBrain];
				if (brain.IsRunning())
				{
					Navigator component = brain.GetComponent<Navigator>();
					if (component != null)
					{
						component.executePathProbeTaskAsync = true;
						component.PathProber.potentialCellsPerUpdate = this.probeSize;
						component.pathProbeTask.Update();
						this.pathProbeJob.Add(component.pathProbeTask);
						if (this.pathProbeJob.Count == this.probeCount)
						{
							break;
						}
					}
				}
				this.IncrementBrainIndex(ref this.nextPathProbeBrain);
			}
			CPUBudget.Start(this);
			GlobalJobManager.Run(this.pathProbeJob);
			CPUBudget.End(this);
		}

		// Token: 0x06006E6F RID: 28271 RVA: 0x0029CD4C File Offset: 0x0029AF4C
		public void RenderEveryTick(float dt, bool isAsyncPathProbeEnabled)
		{
			if (isAsyncPathProbeEnabled)
			{
				this.AsyncPathProbe();
			}
			int num = this.InitialProbeCount();
			int num2 = 0;
			while (num2 != this.brains.Count && num != 0)
			{
				this.ClampBrainIndex(ref this.nextUpdateBrain);
				Brain brain = this.brains[this.nextUpdateBrain];
				if (brain.IsRunning())
				{
					brain.UpdateBrain();
					num--;
				}
				this.IncrementBrainIndex(ref this.nextUpdateBrain);
				num2++;
			}
		}

		// Token: 0x06006E70 RID: 28272 RVA: 0x0029CDC0 File Offset: 0x0029AFC0
		public void AccumulatePathProbeIterations(Dictionary<string, int> pathProbeIterations)
		{
			foreach (Brain brain in this.brains)
			{
				Navigator component = brain.GetComponent<Navigator>();
				if (!(component == null) && !pathProbeIterations.ContainsKey(brain.name))
				{
					pathProbeIterations.Add(brain.name, component.PathProber.updateCount);
				}
			}
		}

		// Token: 0x06006E71 RID: 28273
		protected abstract int InitialProbeCount();

		// Token: 0x06006E72 RID: 28274
		protected abstract int InitialProbeSize();

		// Token: 0x06006E73 RID: 28275
		protected abstract int MinProbeSize();

		// Token: 0x06006E74 RID: 28276
		protected abstract int IdealProbeSize();

		// Token: 0x06006E75 RID: 28277
		protected abstract int ProbeSizeStep();

		// Token: 0x06006E76 RID: 28278
		public abstract float GetEstimatedFrameTime();

		// Token: 0x06006E77 RID: 28279
		public abstract float LoadBalanceThreshold();

		// Token: 0x04005372 RID: 21362
		private List<Brain> brains = new List<Brain>();

		// Token: 0x04005373 RID: 21363
		private string increaseLoadLabel;

		// Token: 0x04005374 RID: 21364
		private string decreaseLoadLabel;

		// Token: 0x04005375 RID: 21365
		private WorkItemCollection<Navigator.PathProbeTask, object> pathProbeJob = new WorkItemCollection<Navigator.PathProbeTask, object>();

		// Token: 0x04005376 RID: 21366
		private int nextUpdateBrain;

		// Token: 0x04005377 RID: 21367
		private int nextPathProbeBrain;
	}

	// Token: 0x02000F3E RID: 3902
	private class DupeBrainGroup : BrainScheduler.BrainGroup
	{
		// Token: 0x06006E78 RID: 28280 RVA: 0x0029CE44 File Offset: 0x0029B044
		public DupeBrainGroup()
			: base(GameTags.DupeBrain)
		{
		}

		// Token: 0x06006E79 RID: 28281 RVA: 0x0029CE51 File Offset: 0x0029B051
		protected override int InitialProbeCount()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().initialProbeCount;
		}

		// Token: 0x06006E7A RID: 28282 RVA: 0x0029CE5D File Offset: 0x0029B05D
		protected override int InitialProbeSize()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().initialProbeSize;
		}

		// Token: 0x06006E7B RID: 28283 RVA: 0x0029CE69 File Offset: 0x0029B069
		protected override int MinProbeSize()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().minProbeSize;
		}

		// Token: 0x06006E7C RID: 28284 RVA: 0x0029CE75 File Offset: 0x0029B075
		protected override int IdealProbeSize()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().idealProbeSize;
		}

		// Token: 0x06006E7D RID: 28285 RVA: 0x0029CE81 File Offset: 0x0029B081
		protected override int ProbeSizeStep()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().probeSizeStep;
		}

		// Token: 0x06006E7E RID: 28286 RVA: 0x0029CE8D File Offset: 0x0029B08D
		public override float GetEstimatedFrameTime()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().estimatedFrameTime;
		}

		// Token: 0x06006E7F RID: 28287 RVA: 0x0029CE99 File Offset: 0x0029B099
		public override float LoadBalanceThreshold()
		{
			return TuningData<BrainScheduler.DupeBrainGroup.Tuning>.Get().loadBalanceThreshold;
		}

		// Token: 0x02001E9A RID: 7834
		public class Tuning : TuningData<BrainScheduler.DupeBrainGroup.Tuning>
		{
			// Token: 0x04008915 RID: 35093
			public int initialProbeCount = 1;

			// Token: 0x04008916 RID: 35094
			public int initialProbeSize = 1000;

			// Token: 0x04008917 RID: 35095
			public int minProbeSize = 100;

			// Token: 0x04008918 RID: 35096
			public int idealProbeSize = 1000;

			// Token: 0x04008919 RID: 35097
			public int probeSizeStep = 100;

			// Token: 0x0400891A RID: 35098
			public float estimatedFrameTime = 2f;

			// Token: 0x0400891B RID: 35099
			public float loadBalanceThreshold = 0.1f;
		}
	}

	// Token: 0x02000F3F RID: 3903
	private class CreatureBrainGroup : BrainScheduler.BrainGroup
	{
		// Token: 0x06006E80 RID: 28288 RVA: 0x0029CEA5 File Offset: 0x0029B0A5
		public CreatureBrainGroup()
			: base(GameTags.CreatureBrain)
		{
		}

		// Token: 0x06006E81 RID: 28289 RVA: 0x0029CEB2 File Offset: 0x0029B0B2
		protected override int InitialProbeCount()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().initialProbeCount;
		}

		// Token: 0x06006E82 RID: 28290 RVA: 0x0029CEBE File Offset: 0x0029B0BE
		protected override int InitialProbeSize()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().initialProbeSize;
		}

		// Token: 0x06006E83 RID: 28291 RVA: 0x0029CECA File Offset: 0x0029B0CA
		protected override int MinProbeSize()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().minProbeSize;
		}

		// Token: 0x06006E84 RID: 28292 RVA: 0x0029CED6 File Offset: 0x0029B0D6
		protected override int IdealProbeSize()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().idealProbeSize;
		}

		// Token: 0x06006E85 RID: 28293 RVA: 0x0029CEE2 File Offset: 0x0029B0E2
		protected override int ProbeSizeStep()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().probeSizeStep;
		}

		// Token: 0x06006E86 RID: 28294 RVA: 0x0029CEEE File Offset: 0x0029B0EE
		public override float GetEstimatedFrameTime()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().estimatedFrameTime;
		}

		// Token: 0x06006E87 RID: 28295 RVA: 0x0029CEFA File Offset: 0x0029B0FA
		public override float LoadBalanceThreshold()
		{
			return TuningData<BrainScheduler.CreatureBrainGroup.Tuning>.Get().loadBalanceThreshold;
		}

		// Token: 0x02001E9B RID: 7835
		public class Tuning : TuningData<BrainScheduler.CreatureBrainGroup.Tuning>
		{
			// Token: 0x0400891C RID: 35100
			public int initialProbeCount = 1;

			// Token: 0x0400891D RID: 35101
			public int initialProbeSize = 1000;

			// Token: 0x0400891E RID: 35102
			public int minProbeSize = 100;

			// Token: 0x0400891F RID: 35103
			public int idealProbeSize = 300;

			// Token: 0x04008920 RID: 35104
			public int probeSizeStep = 100;

			// Token: 0x04008921 RID: 35105
			public float estimatedFrameTime = 1f;

			// Token: 0x04008922 RID: 35106
			public float loadBalanceThreshold = 0.1f;
		}
	}
}
