using System;

// Token: 0x02000922 RID: 2338
public class SkyVisibilityMonitor : GameStateMachine<SkyVisibilityMonitor, SkyVisibilityMonitor.Instance, IStateMachineTarget, SkyVisibilityMonitor.Def>
{
	// Token: 0x06004453 RID: 17491 RVA: 0x00181E8F File Offset: 0x0018008F
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Update(new Action<SkyVisibilityMonitor.Instance, float>(SkyVisibilityMonitor.CheckSkyVisibility), UpdateRate.SIM_1000ms, false);
	}

	// Token: 0x06004454 RID: 17492 RVA: 0x00181EB4 File Offset: 0x001800B4
	public static void CheckSkyVisibility(SkyVisibilityMonitor.Instance smi, float dt)
	{
		bool hasSkyVisibility = smi.HasSkyVisibility;
		Grid.IsRangeExposedToSunlight(Grid.OffsetCell(Grid.PosToCell(smi), smi.def.ScanOriginOffset), smi.def.ScanRadius, smi.def.ScanShape, out smi.NumClearCells, 1);
		if (hasSkyVisibility == smi.HasSkyVisibility)
		{
			return;
		}
		smi.TriggerVisibilityChange();
	}

	// Token: 0x02001701 RID: 5889
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006BB7 RID: 27575
		public Operational.State AffectedOperationalState;

		// Token: 0x04006BB8 RID: 27576
		public string StatusItemId = "SPACE_VISIBILITY_NONE";

		// Token: 0x04006BB9 RID: 27577
		public int ScanRadius = 15;

		// Token: 0x04006BBA RID: 27578
		public CellOffset ScanShape = new CellOffset(1, 0);

		// Token: 0x04006BBB RID: 27579
		public CellOffset ScanOriginOffset = new CellOffset(0, 0);
	}

	// Token: 0x02001702 RID: 5890
	public new class Instance : GameStateMachine<SkyVisibilityMonitor, SkyVisibilityMonitor.Instance, IStateMachineTarget, SkyVisibilityMonitor.Def>.GameInstance
	{
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06008931 RID: 35121 RVA: 0x002F7FCC File Offset: 0x002F61CC
		public bool HasSkyVisibility
		{
			get
			{
				return this.PercentClearSky > 0f;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06008932 RID: 35122 RVA: 0x002F7FDB File Offset: 0x002F61DB
		public float PercentClearSky
		{
			get
			{
				return (float)this.NumClearCells * (float)(base.def.ScanRadius + 1);
			}
		}

		// Token: 0x06008933 RID: 35123 RVA: 0x002F7FF4 File Offset: 0x002F61F4
		public Instance(IStateMachineTarget master, SkyVisibilityMonitor.Def def)
			: base(master, def)
		{
			if (string.IsNullOrEmpty(def.StatusItemId))
			{
				return;
			}
			if (def.AffectedOperationalState != Operational.State.None)
			{
				this.skyVisibilityFlag = new Operational.Flag("sky visibility", Operational.Flag.GetFlagType(def.AffectedOperationalState));
			}
			this.visibilityStatusItem = new StatusItem(def.StatusItemId, "BUILDING", "status_item_no_sky", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, true, 129022, new Func<string, object, string>(SkyVisibilityMonitor.Instance.GetStatusItemString));
		}

		// Token: 0x06008934 RID: 35124 RVA: 0x002F8070 File Offset: 0x002F6270
		public override void StartSM()
		{
			base.StartSM();
			SkyVisibilityMonitor.CheckSkyVisibility(this, 0f);
			this.TriggerVisibilityChange();
		}

		// Token: 0x06008935 RID: 35125 RVA: 0x002F808C File Offset: 0x002F628C
		public void TriggerVisibilityChange()
		{
			if (this.visibilityStatusItem != null)
			{
				base.smi.GetComponent<KSelectable>().ToggleStatusItem(this.visibilityStatusItem, !this.HasSkyVisibility, this);
			}
			if (base.def.AffectedOperationalState != Operational.State.None)
			{
				base.smi.GetComponent<Operational>().SetFlag(this.skyVisibilityFlag, this.HasSkyVisibility);
			}
			if (this.SkyVisibilityChanged != null)
			{
				this.SkyVisibilityChanged();
			}
		}

		// Token: 0x06008936 RID: 35126 RVA: 0x002F8100 File Offset: 0x002F6300
		private static string GetStatusItemString(string src_str, object data)
		{
			SkyVisibilityMonitor.Instance instance = (SkyVisibilityMonitor.Instance)data;
			return src_str.Replace("{VISIBILITY}", GameUtil.GetFormattedPercent(instance.PercentClearSky * 100f, GameUtil.TimeSlice.None)).Replace("{RADIUS}", instance.def.ScanRadius.ToString());
		}

		// Token: 0x04006BBC RID: 27580
		public int NumClearCells;

		// Token: 0x04006BBD RID: 27581
		public System.Action SkyVisibilityChanged;

		// Token: 0x04006BBE RID: 27582
		private StatusItem visibilityStatusItem;

		// Token: 0x04006BBF RID: 27583
		private Operational.Flag skyVisibilityFlag;
	}
}
