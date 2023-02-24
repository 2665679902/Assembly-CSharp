using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x02000480 RID: 1152
public class FixedCapturePoint : GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>
{
	// Token: 0x060019B9 RID: 6585 RVA: 0x0008A51C File Offset: 0x0008871C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.operational;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.unoperational.TagTransition(GameTags.Operational, this.operational, false);
		this.operational.DefaultState(this.operational.manual).TagTransition(GameTags.Operational, this.unoperational, true);
		this.operational.manual.ParamTransition<bool>(this.automated, this.operational.automated, GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.IsTrue);
		this.operational.automated.ParamTransition<bool>(this.automated, this.operational.manual, GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.IsFalse).ToggleChore((FixedCapturePoint.Instance smi) => smi.CreateChore(), this.unoperational, this.unoperational).Update("FindFixedCapturable", delegate(FixedCapturePoint.Instance smi, float dt)
		{
			smi.FindFixedCapturable();
		}, UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x04000E6D RID: 3693
	private StateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.BoolParameter automated;

	// Token: 0x04000E6E RID: 3694
	public GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State unoperational;

	// Token: 0x04000E6F RID: 3695
	public FixedCapturePoint.OperationalState operational;

	// Token: 0x020010CC RID: 4300
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040058B7 RID: 22711
		public Func<GameObject, FixedCapturePoint.Instance, bool> isCreatureEligibleToBeCapturedCb;

		// Token: 0x040058B8 RID: 22712
		public Func<FixedCapturePoint.Instance, int> getTargetCapturePoint = delegate(FixedCapturePoint.Instance smi)
		{
			int num = Grid.PosToCell(smi);
			Navigator component = smi.targetCapturable.GetComponent<Navigator>();
			if (Grid.IsValidCell(num - 1) && component.CanReach(num - 1))
			{
				return num - 1;
			}
			if (Grid.IsValidCell(num + 1) && component.CanReach(num + 1))
			{
				return num + 1;
			}
			return num;
		};
	}

	// Token: 0x020010CD RID: 4301
	public class OperationalState : GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State
	{
		// Token: 0x040058B9 RID: 22713
		public GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State manual;

		// Token: 0x040058BA RID: 22714
		public GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.State automated;
	}

	// Token: 0x020010CE RID: 4302
	[SerializationConfig(MemberSerialization.OptIn)]
	public new class Instance : GameStateMachine<FixedCapturePoint, FixedCapturePoint.Instance, IStateMachineTarget, FixedCapturePoint.Def>.GameInstance, ICheckboxControl
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06007475 RID: 29813 RVA: 0x002B3350 File Offset: 0x002B1550
		// (set) Token: 0x06007476 RID: 29814 RVA: 0x002B3358 File Offset: 0x002B1558
		public FixedCapturableMonitor.Instance targetCapturable { get; private set; }

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06007477 RID: 29815 RVA: 0x002B3361 File Offset: 0x002B1561
		// (set) Token: 0x06007478 RID: 29816 RVA: 0x002B3369 File Offset: 0x002B1569
		public bool shouldCreatureGoGetCaptured { get; private set; }

		// Token: 0x06007479 RID: 29817 RVA: 0x002B3372 File Offset: 0x002B1572
		public Instance(IStateMachineTarget master, FixedCapturePoint.Def def)
			: base(master, def)
		{
			base.Subscribe(-905833192, new Action<object>(this.OnCopySettings));
		}

		// Token: 0x0600747A RID: 29818 RVA: 0x002B3394 File Offset: 0x002B1594
		private void OnCopySettings(object data)
		{
			GameObject gameObject = (GameObject)data;
			if (gameObject == null)
			{
				return;
			}
			FixedCapturePoint.Instance smi = gameObject.GetSMI<FixedCapturePoint.Instance>();
			if (smi == null)
			{
				return;
			}
			base.sm.automated.Set(base.sm.automated.Get(smi), this, false);
		}

		// Token: 0x0600747B RID: 29819 RVA: 0x002B33E1 File Offset: 0x002B15E1
		public Chore CreateChore()
		{
			this.FindFixedCapturable();
			return new FixedCaptureChore(base.GetComponent<KPrefabID>());
		}

		// Token: 0x0600747C RID: 29820 RVA: 0x002B33F4 File Offset: 0x002B15F4
		public bool IsCreatureAvailableForFixedCapture()
		{
			if (!this.targetCapturable.IsNullOrStopped())
			{
				int num = Grid.PosToCell(base.transform.GetPosition());
				CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
				return FixedCapturePoint.Instance.CanCapturableBeCapturedAtCapturePoint(this.targetCapturable, this, cavityForCell, num);
			}
			return false;
		}

		// Token: 0x0600747D RID: 29821 RVA: 0x002B3440 File Offset: 0x002B1640
		public void SetRancherIsAvailableForCapturing()
		{
			this.shouldCreatureGoGetCaptured = true;
		}

		// Token: 0x0600747E RID: 29822 RVA: 0x002B3449 File Offset: 0x002B1649
		public void ClearRancherIsAvailableForCapturing()
		{
			this.shouldCreatureGoGetCaptured = false;
		}

		// Token: 0x0600747F RID: 29823 RVA: 0x002B3454 File Offset: 0x002B1654
		private static bool CanCapturableBeCapturedAtCapturePoint(FixedCapturableMonitor.Instance capturable, FixedCapturePoint.Instance capture_point, CavityInfo capture_cavity_info, int capture_cell)
		{
			if (!capturable.IsRunning())
			{
				return false;
			}
			if (capturable.targetCapturePoint != capture_point && !capturable.targetCapturePoint.IsNullOrStopped())
			{
				return false;
			}
			if (capture_point.def.isCreatureEligibleToBeCapturedCb != null && !capture_point.def.isCreatureEligibleToBeCapturedCb(capturable.gameObject, capture_point))
			{
				return false;
			}
			int num = Grid.PosToCell(capturable.transform.GetPosition());
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			if (cavityForCell == null || cavityForCell != capture_cavity_info)
			{
				return false;
			}
			if (capturable.HasTag(GameTags.Creatures.Bagged))
			{
				return false;
			}
			if (!capturable.GetComponent<ChoreConsumer>().IsChoreEqualOrAboveCurrentChorePriority<FixedCaptureStates>())
			{
				return false;
			}
			if (capturable.GetComponent<Navigator>().GetNavigationCost(capture_cell) == -1)
			{
				return false;
			}
			TreeFilterable component = capture_point.GetComponent<TreeFilterable>();
			IUserControlledCapacity component2 = capture_point.GetComponent<IUserControlledCapacity>();
			return !component.ContainsTag(capturable.GetComponent<KPrefabID>().PrefabTag) || component2.AmountStored > component2.UserMaxCapacity;
		}

		// Token: 0x06007480 RID: 29824 RVA: 0x002B3538 File Offset: 0x002B1738
		public void FindFixedCapturable()
		{
			int num = Grid.PosToCell(base.transform.GetPosition());
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
			if (cavityForCell == null)
			{
				this.ResetCapturePoint();
				return;
			}
			if (!this.targetCapturable.IsNullOrStopped() && !FixedCapturePoint.Instance.CanCapturableBeCapturedAtCapturePoint(this.targetCapturable, this, cavityForCell, num))
			{
				this.ResetCapturePoint();
			}
			if (this.targetCapturable.IsNullOrStopped())
			{
				foreach (object obj in Components.FixedCapturableMonitors)
				{
					FixedCapturableMonitor.Instance instance = (FixedCapturableMonitor.Instance)obj;
					if (FixedCapturePoint.Instance.CanCapturableBeCapturedAtCapturePoint(instance, this, cavityForCell, num))
					{
						this.targetCapturable = instance;
						if (!this.targetCapturable.IsNullOrStopped())
						{
							this.targetCapturable.targetCapturePoint = this;
							break;
						}
						break;
					}
				}
			}
		}

		// Token: 0x06007481 RID: 29825 RVA: 0x002B3618 File Offset: 0x002B1818
		public void ResetCapturePoint()
		{
			base.Trigger(643180843, null);
			if (!this.targetCapturable.IsNullOrStopped())
			{
				this.targetCapturable.targetCapturePoint = null;
				this.targetCapturable.Trigger(1034952693, null);
				this.targetCapturable = null;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06007482 RID: 29826 RVA: 0x002B3657 File Offset: 0x002B1857
		string ICheckboxControl.CheckboxTitleKey
		{
			get
			{
				return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.TITLE.key.String;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06007483 RID: 29827 RVA: 0x002B3668 File Offset: 0x002B1868
		string ICheckboxControl.CheckboxLabel
		{
			get
			{
				return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.AUTOWRANGLE;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06007484 RID: 29828 RVA: 0x002B3674 File Offset: 0x002B1874
		string ICheckboxControl.CheckboxTooltip
		{
			get
			{
				return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.AUTOWRANGLE_TOOLTIP;
			}
		}

		// Token: 0x06007485 RID: 29829 RVA: 0x002B3680 File Offset: 0x002B1880
		bool ICheckboxControl.GetCheckboxValue()
		{
			return base.sm.automated.Get(this);
		}

		// Token: 0x06007486 RID: 29830 RVA: 0x002B3693 File Offset: 0x002B1893
		void ICheckboxControl.SetCheckboxValue(bool value)
		{
			base.sm.automated.Set(value, this, false);
		}
	}
}
