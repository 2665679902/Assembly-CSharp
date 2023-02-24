using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020005A4 RID: 1444
public class DetectorNetwork : GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>
{
	// Token: 0x0600239C RID: 9116 RVA: 0x000C0368 File Offset: 0x000BE568
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.EventTransition(GameHashes.OperationalChanged, this.operational, (DetectorNetwork.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.operational.DefaultState(this.operational.self_poor.poor).Update("CheckForInterference", delegate(DetectorNetwork.Instance smi, float dt)
		{
			smi.Update(dt);
		}, UpdateRate.SIM_1000ms, false).EventTransition(GameHashes.OperationalChanged, this.inoperational, (DetectorNetwork.Instance smi) => !smi.GetComponent<Operational>().IsOperational);
		this.operational.self_poor.InitializeStates(this).ToggleStatusItem(BUILDING.STATUSITEMS.DETECTORQUALITY.NAME, BUILDING.STATUSITEMS.DETECTORQUALITY.TOOLTIP, "status_item_interference", StatusItem.IconType.Custom, NotificationType.BadMinor, false, default(HashedString), 129022, (string str, DetectorNetwork.Instance smi) => str.Replace("{Quality}", GameUtil.GetFormattedPercent(smi.GetDishQuality() * 100f, GameUtil.TimeSlice.None)), null, null).ParamTransition<float>(this.selfQuality, this.operational.self_good, (DetectorNetwork.Instance smi, float p) => (double)p >= 0.8);
		this.operational.self_good.InitializeStates(this).ToggleStatusItem(BUILDING.STATUSITEMS.DETECTORQUALITY.NAME, BUILDING.STATUSITEMS.DETECTORQUALITY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, (string str, DetectorNetwork.Instance smi) => str.Replace("{Quality}", GameUtil.GetFormattedPercent(smi.GetDishQuality() * 100f, GameUtil.TimeSlice.None)), null, null).ParamTransition<float>(this.selfQuality, this.operational.self_poor, (DetectorNetwork.Instance smi, float p) => (double)p < 0.8);
	}

	// Token: 0x04001470 RID: 5232
	public StateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.FloatParameter selfQuality;

	// Token: 0x04001471 RID: 5233
	public StateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.FloatParameter networkQuality;

	// Token: 0x04001472 RID: 5234
	public GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State inoperational;

	// Token: 0x04001473 RID: 5235
	public DetectorNetwork.SelfStates operational;

	// Token: 0x020011D4 RID: 4564
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005C1E RID: 23582
		public int interferenceRadius;

		// Token: 0x04005C1F RID: 23583
		public float worstWarningTime;

		// Token: 0x04005C20 RID: 23584
		public float bestWarningTime;

		// Token: 0x04005C21 RID: 23585
		public int bestNetworkSize;
	}

	// Token: 0x020011D5 RID: 4565
	public class SelfStates : GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State
	{
		// Token: 0x04005C22 RID: 23586
		public DetectorNetwork.NetworkStates self_poor;

		// Token: 0x04005C23 RID: 23587
		public DetectorNetwork.NetworkStates self_good;
	}

	// Token: 0x020011D6 RID: 4566
	public class NetworkStates : GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State
	{
		// Token: 0x06007834 RID: 30772 RVA: 0x002BDC0C File Offset: 0x002BBE0C
		public DetectorNetwork.NetworkStates InitializeStates(DetectorNetwork parent)
		{
			base.DefaultState(this.poor);
			this.poor.ToggleStatusItem(BUILDING.STATUSITEMS.NETWORKQUALITY.NAME, BUILDING.STATUSITEMS.NETWORKQUALITY.TOOLTIP, "", StatusItem.IconType.Exclamation, NotificationType.BadMinor, false, default(HashedString), 129022, new Func<string, DetectorNetwork.Instance, string>(this.StringCallback), null, null).ParamTransition<float>(parent.networkQuality, this.good, (DetectorNetwork.Instance smi, float p) => (double)p >= 0.8);
			this.good.ToggleStatusItem(BUILDING.STATUSITEMS.NETWORKQUALITY.NAME, BUILDING.STATUSITEMS.NETWORKQUALITY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, new Func<string, DetectorNetwork.Instance, string>(this.StringCallback), null, null).ParamTransition<float>(parent.networkQuality, this.poor, (DetectorNetwork.Instance smi, float p) => (double)p < 0.8);
			return this;
		}

		// Token: 0x06007835 RID: 30773 RVA: 0x002BDD10 File Offset: 0x002BBF10
		private string StringCallback(string str, DetectorNetwork.Instance smi)
		{
			MathUtil.MinMax detectTimeRange = smi.GetDetectTimeRange();
			return str.Replace("{TotalQuality}", GameUtil.GetFormattedPercent(smi.ComputeTotalDishQuality() * 100f, GameUtil.TimeSlice.None)).Replace("{WorstTime}", GameUtil.GetFormattedTime(detectTimeRange.min, "F0")).Replace("{BestTime}", GameUtil.GetFormattedTime(detectTimeRange.max, "F0"));
		}

		// Token: 0x04005C24 RID: 23588
		public GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State poor;

		// Token: 0x04005C25 RID: 23589
		public GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.State good;
	}

	// Token: 0x020011D7 RID: 4567
	public new class Instance : GameStateMachine<DetectorNetwork, DetectorNetwork.Instance, IStateMachineTarget, DetectorNetwork.Def>.GameInstance
	{
		// Token: 0x06007837 RID: 30775 RVA: 0x002BDD7F File Offset: 0x002BBF7F
		public Instance(IStateMachineTarget master, DetectorNetwork.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06007838 RID: 30776 RVA: 0x002BDD94 File Offset: 0x002BBF94
		public override void StartSM()
		{
			Components.DetectorNetworks.Add(this);
			base.StartSM();
		}

		// Token: 0x06007839 RID: 30777 RVA: 0x002BDDA7 File Offset: 0x002BBFA7
		public override void StopSM(string reason)
		{
			base.StopSM(reason);
			Components.DetectorNetworks.Remove(this);
		}

		// Token: 0x0600783A RID: 30778 RVA: 0x002BDDBC File Offset: 0x002BBFBC
		public void Update(float dt)
		{
			this.CheckForVisibility();
			this.CheckForInterference();
			base.sm.selfQuality.Set(this.GetDishQuality(), base.smi, false);
			base.sm.networkQuality.Set(this.ComputeTotalDishQuality(), base.smi, false);
		}

		// Token: 0x0600783B RID: 30779 RVA: 0x002BDE14 File Offset: 0x002BC014
		private void CheckForVisibility()
		{
			int num = Grid.PosToCell(this);
			int num2 = 0;
			num2 += DetectorNetwork.Instance.ScanVisiblityLine(num, 1, 1, base.def.interferenceRadius);
			num2 += DetectorNetwork.Instance.ScanVisiblityLine(num, -1, 1, base.def.interferenceRadius);
			this.visibleSkyCells = num2;
		}

		// Token: 0x0600783C RID: 30780 RVA: 0x002BDE60 File Offset: 0x002BC060
		public static int ScanVisiblityLine(int start_cell, int x_offset, int y_offset, int radius)
		{
			int num = 0;
			int num2 = 0;
			while (Mathf.Abs(num2) <= radius)
			{
				int num3 = Grid.OffsetCell(start_cell, num2 * x_offset, num2 * y_offset);
				if (Grid.IsValidCell(num3))
				{
					if (Grid.ExposedToSunlight[num3] <= 0)
					{
						break;
					}
					num++;
				}
				num2++;
			}
			return num;
		}

		// Token: 0x0600783D RID: 30781 RVA: 0x002BDEA8 File Offset: 0x002BC0A8
		private void CheckForInterference()
		{
			Extents extents = new Extents(Grid.PosToCell(this), base.def.interferenceRadius);
			List<ScenePartitionerEntry> list = new List<ScenePartitionerEntry>();
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.industrialBuildings, list);
			float num = float.MaxValue;
			foreach (ScenePartitionerEntry scenePartitionerEntry in list)
			{
				GameObject gameObject = (GameObject)scenePartitionerEntry.obj;
				if (!(gameObject == base.gameObject))
				{
					float magnitude = (base.gameObject.transform.GetPosition() - gameObject.transform.GetPosition()).magnitude;
					num = Mathf.Min(num, magnitude);
				}
			}
			this.closestMachinery = num;
		}

		// Token: 0x0600783E RID: 30782 RVA: 0x002BDF80 File Offset: 0x002BC180
		public float GetDishQuality()
		{
			if (!base.GetComponent<Operational>().IsOperational)
			{
				return 0f;
			}
			return Mathf.Clamp01(this.closestMachinery / (float)base.def.interferenceRadius) * Mathf.Clamp01((float)this.visibleSkyCells / ((float)base.def.interferenceRadius * 2f));
		}

		// Token: 0x0600783F RID: 30783 RVA: 0x002BDFD8 File Offset: 0x002BC1D8
		public float ComputeTotalDishQuality()
		{
			float num = 0f;
			foreach (DetectorNetwork.Instance instance in Components.DetectorNetworks.Items)
			{
				num += instance.GetDishQuality();
			}
			return num / (float)base.def.bestNetworkSize;
		}

		// Token: 0x06007840 RID: 30784 RVA: 0x002BE048 File Offset: 0x002BC248
		public MathUtil.MinMax GetDetectTimeRange()
		{
			float num = this.ComputeTotalDishQuality();
			return new MathUtil.MinMax(Mathf.Lerp(base.def.worstWarningTime, base.def.bestWarningTime, num), base.def.bestWarningTime);
		}

		// Token: 0x04005C26 RID: 23590
		private float closestMachinery = float.MaxValue;

		// Token: 0x04005C27 RID: 23591
		private int visibleSkyCells;
	}
}
