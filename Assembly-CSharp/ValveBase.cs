using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000669 RID: 1641
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ValveBase")]
public class ValveBase : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x17000314 RID: 788
	// (get) Token: 0x06002C39 RID: 11321 RVA: 0x000E8387 File Offset: 0x000E6587
	// (set) Token: 0x06002C38 RID: 11320 RVA: 0x000E837E File Offset: 0x000E657E
	public float CurrentFlow
	{
		get
		{
			return this.currentFlow;
		}
		set
		{
			this.currentFlow = value;
		}
	}

	// Token: 0x17000315 RID: 789
	// (get) Token: 0x06002C3A RID: 11322 RVA: 0x000E838F File Offset: 0x000E658F
	public HandleVector<int>.Handle AccumulatorHandle
	{
		get
		{
			return this.flowAccumulator;
		}
	}

	// Token: 0x17000316 RID: 790
	// (get) Token: 0x06002C3B RID: 11323 RVA: 0x000E8397 File Offset: 0x000E6597
	public float MaxFlow
	{
		get
		{
			return this.maxFlow;
		}
	}

	// Token: 0x06002C3C RID: 11324 RVA: 0x000E839F File Offset: 0x000E659F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.flowAccumulator = Game.Instance.accumulators.Add("Flow", this);
	}

	// Token: 0x06002C3D RID: 11325 RVA: 0x000E83C4 File Offset: 0x000E65C4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Building component = base.GetComponent<Building>();
		this.inputCell = component.GetUtilityInputCell();
		this.outputCell = component.GetUtilityOutputCell();
		Conduit.GetFlowManager(this.conduitType).AddConduitUpdater(new Action<float>(this.ConduitUpdate), ConduitFlowPriority.Default);
		this.UpdateAnim();
		this.OnCmpEnable();
	}

	// Token: 0x06002C3E RID: 11326 RVA: 0x000E841F File Offset: 0x000E661F
	protected override void OnCleanUp()
	{
		Game.Instance.accumulators.Remove(this.flowAccumulator);
		Conduit.GetFlowManager(this.conduitType).RemoveConduitUpdater(new Action<float>(this.ConduitUpdate));
		base.OnCleanUp();
	}

	// Token: 0x06002C3F RID: 11327 RVA: 0x000E845C File Offset: 0x000E665C
	private void ConduitUpdate(float dt)
	{
		ConduitFlow flowManager = Conduit.GetFlowManager(this.conduitType);
		ConduitFlow.Conduit conduit = flowManager.GetConduit(this.inputCell);
		if (!flowManager.HasConduit(this.inputCell) || !flowManager.HasConduit(this.outputCell))
		{
			this.OnMassTransfer(0f);
			this.UpdateAnim();
			return;
		}
		ConduitFlow.ConduitContents contents = conduit.GetContents(flowManager);
		float num = Mathf.Min(contents.mass, this.currentFlow * dt);
		float num2 = 0f;
		if (num > 0f)
		{
			int num3 = (int)(num / contents.mass * (float)contents.diseaseCount);
			num2 = flowManager.AddElement(this.outputCell, contents.element, num, contents.temperature, contents.diseaseIdx, num3);
			Game.Instance.accumulators.Accumulate(this.flowAccumulator, num2);
			if (num2 > 0f)
			{
				flowManager.RemoveElement(this.inputCell, num2);
			}
		}
		this.OnMassTransfer(num2);
		this.UpdateAnim();
	}

	// Token: 0x06002C40 RID: 11328 RVA: 0x000E8551 File Offset: 0x000E6751
	protected virtual void OnMassTransfer(float amount)
	{
	}

	// Token: 0x06002C41 RID: 11329 RVA: 0x000E8554 File Offset: 0x000E6754
	public virtual void UpdateAnim()
	{
		float averageRate = Game.Instance.accumulators.GetAverageRate(this.flowAccumulator);
		if (averageRate > 0f)
		{
			int i = 0;
			while (i < this.animFlowRanges.Length)
			{
				if (averageRate <= this.animFlowRanges[i].minFlow)
				{
					if (this.curFlowIdx != i)
					{
						this.curFlowIdx = i;
						this.controller.Play(this.animFlowRanges[i].animName, (averageRate <= 0f) ? KAnim.PlayMode.Once : KAnim.PlayMode.Loop, 1f, 0f);
						return;
					}
					return;
				}
				else
				{
					i++;
				}
			}
			return;
		}
		this.controller.Play("off", KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x04001A47 RID: 6727
	[SerializeField]
	public ConduitType conduitType;

	// Token: 0x04001A48 RID: 6728
	[SerializeField]
	public float maxFlow = 0.5f;

	// Token: 0x04001A49 RID: 6729
	[Serialize]
	private float currentFlow;

	// Token: 0x04001A4A RID: 6730
	[MyCmpGet]
	protected KBatchedAnimController controller;

	// Token: 0x04001A4B RID: 6731
	protected HandleVector<int>.Handle flowAccumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04001A4C RID: 6732
	private int curFlowIdx = -1;

	// Token: 0x04001A4D RID: 6733
	private int inputCell;

	// Token: 0x04001A4E RID: 6734
	private int outputCell;

	// Token: 0x04001A4F RID: 6735
	[SerializeField]
	public ValveBase.AnimRangeInfo[] animFlowRanges;

	// Token: 0x0200132D RID: 4909
	[Serializable]
	public struct AnimRangeInfo
	{
		// Token: 0x06007CEA RID: 31978 RVA: 0x002D1EBD File Offset: 0x002D00BD
		public AnimRangeInfo(float min_flow, string anim_name)
		{
			this.minFlow = min_flow;
			this.animName = anim_name;
		}

		// Token: 0x04005FC0 RID: 24512
		public float minFlow;

		// Token: 0x04005FC1 RID: 24513
		public string animName;
	}
}
