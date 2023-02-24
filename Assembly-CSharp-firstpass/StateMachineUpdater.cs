using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x02000101 RID: 257
public class StateMachineUpdater : Singleton<StateMachineUpdater>
{
	// Token: 0x060008B3 RID: 2227 RVA: 0x00022F0E File Offset: 0x0002110E
	public StateMachineUpdater()
	{
		this.Initialize();
	}

	// Token: 0x060008B4 RID: 2228 RVA: 0x00022F48 File Offset: 0x00021148
	private void Initialize()
	{
		this.bucketGroups = new List<StateMachineUpdater.BucketGroup>();
		this.simBucketGroups = new List<StateMachineUpdater.BucketGroup>();
		this.renderBucketGroups = new List<StateMachineUpdater.BucketGroup>();
		this.renderEveryTickBucketGroups = new List<StateMachineUpdater.BucketGroup>();
		this.CreateBucketGroup(1, 0.016666668f, UpdateRate.RENDER_EVERY_TICK, this.renderEveryTickBucketGroups);
		this.CreateBucketGroup(12, 0.016666668f, UpdateRate.RENDER_200ms, this.renderBucketGroups);
		this.CreateBucketGroup(60, 0.016666668f, UpdateRate.RENDER_1000ms, this.renderBucketGroups);
		this.CreateBucketGroup(1, 0.016666668f, UpdateRate.SIM_EVERY_TICK, this.simBucketGroups);
		this.CreateBucketGroup(2, 0.016666668f, UpdateRate.SIM_33ms, this.simBucketGroups);
		this.CreateBucketGroup(12, 0.016666668f, UpdateRate.SIM_200ms, this.simBucketGroups);
		this.CreateBucketGroup(60, 0.016666668f, UpdateRate.SIM_1000ms, this.simBucketGroups);
		this.CreateBucketGroup(240, 0.016666668f, UpdateRate.SIM_4000ms, this.simBucketGroups);
	}

	// Token: 0x060008B5 RID: 2229 RVA: 0x00023024 File Offset: 0x00021224
	private void CreateBucketGroup(int sub_tick_count, float seconds_per_sub_tick, UpdateRate update_rate, List<StateMachineUpdater.BucketGroup> sub_group)
	{
		StateMachineUpdater.BucketGroup bucketGroup = new StateMachineUpdater.BucketGroup(sub_tick_count, seconds_per_sub_tick, update_rate);
		this.bucketGroups.Add(bucketGroup);
		sub_group.Add(bucketGroup);
	}

	// Token: 0x060008B6 RID: 2230 RVA: 0x00023050 File Offset: 0x00021250
	public void AdvanceOneSimSubTick()
	{
		foreach (StateMachineUpdater.BucketGroup bucketGroup in this.simBucketGroups)
		{
			float num = (float)bucketGroup.subTickCount * bucketGroup.secondsPerSubTick;
			bucketGroup.AdvanceOneSubTick(num);
		}
	}

	// Token: 0x060008B7 RID: 2231 RVA: 0x000230B4 File Offset: 0x000212B4
	public void Render(float dt)
	{
		foreach (StateMachineUpdater.BucketGroup bucketGroup in this.renderBucketGroups)
		{
			bucketGroup.Advance(dt);
		}
	}

	// Token: 0x060008B8 RID: 2232 RVA: 0x00023108 File Offset: 0x00021308
	public void RenderEveryTick(float dt)
	{
		foreach (StateMachineUpdater.BucketGroup bucketGroup in this.renderEveryTickBucketGroups)
		{
			bucketGroup.AdvanceOneSubTick(dt);
		}
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x0002315C File Offset: 0x0002135C
	public int GetFrameCount(UpdateRate update_rate)
	{
		return this.bucketGroups[(int)update_rate].subTickCount;
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x0002316F File Offset: 0x0002136F
	public void AddBucket(UpdateRate update_rate, StateMachineUpdater.BaseUpdateBucket bucket)
	{
		this.bucketGroups[(int)update_rate].AddBucket(bucket);
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00023183 File Offset: 0x00021383
	public float GetFrameTime(UpdateRate update_rate, int frame)
	{
		return this.bucketGroups[(int)update_rate].GetFrameTime(frame);
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00023197 File Offset: 0x00021397
	public void Clear()
	{
		this.Initialize();
	}

	// Token: 0x04000663 RID: 1635
	private List<StateMachineUpdater.BucketGroup> bucketGroups = new List<StateMachineUpdater.BucketGroup>();

	// Token: 0x04000664 RID: 1636
	private List<StateMachineUpdater.BucketGroup> simBucketGroups = new List<StateMachineUpdater.BucketGroup>();

	// Token: 0x04000665 RID: 1637
	private List<StateMachineUpdater.BucketGroup> renderBucketGroups = new List<StateMachineUpdater.BucketGroup>();

	// Token: 0x04000666 RID: 1638
	private List<StateMachineUpdater.BucketGroup> renderEveryTickBucketGroups = new List<StateMachineUpdater.BucketGroup>();

	// Token: 0x02000A02 RID: 2562
	public class BucketGroup
	{
		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x06005413 RID: 21523 RVA: 0x0009CAE3 File Offset: 0x0009ACE3
		// (set) Token: 0x06005414 RID: 21524 RVA: 0x0009CAEB File Offset: 0x0009ACEB
		public float secondsPerSubTick { get; private set; }

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06005415 RID: 21525 RVA: 0x0009CAF4 File Offset: 0x0009ACF4
		// (set) Token: 0x06005416 RID: 21526 RVA: 0x0009CAFC File Offset: 0x0009ACFC
		public UpdateRate updateRate { get; private set; }

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06005417 RID: 21527 RVA: 0x0009CB05 File Offset: 0x0009AD05
		public int subTickCount
		{
			get
			{
				return this.bucketFrames.Count;
			}
		}

		// Token: 0x06005418 RID: 21528 RVA: 0x0009CB14 File Offset: 0x0009AD14
		public BucketGroup(int frame_count, float seconds_per_sub_tick, UpdateRate update_rate)
		{
			for (int i = 0; i < frame_count; i++)
			{
				this.bucketFrames.Add(new List<StateMachineUpdater.BaseUpdateBucket>());
			}
			this.secondsPerSubTick = seconds_per_sub_tick;
			this.updateRate = update_rate;
			this.name = "BucketGroup-" + update_rate.ToString();
		}

		// Token: 0x06005419 RID: 21529 RVA: 0x0009CB7C File Offset: 0x0009AD7C
		private void InternalAdvance(float dt)
		{
			this.accumulatedTime += dt;
			float num = (float)this.subTickCount * this.secondsPerSubTick;
			while (this.accumulatedTime >= this.secondsPerSubTick)
			{
				this.AdvanceOneSubTick(num);
				this.accumulatedTime -= this.secondsPerSubTick;
			}
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x0009CBD0 File Offset: 0x0009ADD0
		public void AdvanceOneSubTick(float dt)
		{
			List<StateMachineUpdater.BaseUpdateBucket> list = this.bucketFrames[this.nextUpdateIndex];
			int count = list.Count;
			for (int i = 0; i < count; i++)
			{
				StateMachineUpdater.BaseUpdateBucket baseUpdateBucket = list[i];
				if (baseUpdateBucket.count != 0)
				{
					baseUpdateBucket.Update(dt);
				}
			}
			this.nextUpdateIndex = (this.nextUpdateIndex + 1) % this.bucketFrames.Count;
		}

		// Token: 0x0600541B RID: 21531 RVA: 0x0009CC33 File Offset: 0x0009AE33
		public void Advance(float dt)
		{
			this.InternalAdvance(dt);
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x0009CC3C File Offset: 0x0009AE3C
		public void AddBucket(StateMachineUpdater.BaseUpdateBucket bucket)
		{
			this.bucketFrames[this.nextBucketFrame].Add(bucket);
			bucket.frame = this.nextBucketFrame;
			this.nextBucketFrame = (this.nextBucketFrame + 1) % this.bucketFrames.Count;
		}

		// Token: 0x0600541D RID: 21533 RVA: 0x0009CC7C File Offset: 0x0009AE7C
		public float GetFrameTime(int frame)
		{
			int num = this.nextUpdateIndex - 1 - frame;
			if (num <= 0)
			{
				num += this.bucketFrames.Count;
			}
			return (float)num * this.secondsPerSubTick;
		}

		// Token: 0x04002251 RID: 8785
		private List<List<StateMachineUpdater.BaseUpdateBucket>> bucketFrames = new List<List<StateMachineUpdater.BaseUpdateBucket>>();

		// Token: 0x04002252 RID: 8786
		private string name;

		// Token: 0x04002253 RID: 8787
		public float accumulatedTime;

		// Token: 0x04002254 RID: 8788
		private int nextUpdateIndex;

		// Token: 0x04002255 RID: 8789
		private int nextBucketFrame;
	}

	// Token: 0x02000A03 RID: 2563
	[DebuggerDisplay("{name}")]
	public abstract class BaseUpdateBucket
	{
		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x0600541E RID: 21534 RVA: 0x0009CCAF File Offset: 0x0009AEAF
		// (set) Token: 0x0600541F RID: 21535 RVA: 0x0009CCB7 File Offset: 0x0009AEB7
		public string name { get; private set; }

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06005420 RID: 21536
		public abstract int count { get; }

		// Token: 0x06005421 RID: 21537 RVA: 0x0009CCC0 File Offset: 0x0009AEC0
		public BaseUpdateBucket(string name)
		{
			this.name = name;
		}

		// Token: 0x06005422 RID: 21538
		public abstract void Update(float dt);

		// Token: 0x06005423 RID: 21539
		public abstract void Remove(HandleVector<int>.Handle handle);

		// Token: 0x04002259 RID: 8793
		public int frame;
	}
}
