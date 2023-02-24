using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005EE RID: 1518
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicHEPSensor : Switch, ISaveLoadable, IThresholdSwitch, ISimEveryTick
{
	// Token: 0x060026AB RID: 9899 RVA: 0x000D0DC6 File Offset: 0x000CEFC6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicHEPSensor>(-905833192, LogicHEPSensor.OnCopySettingsDelegate);
	}

	// Token: 0x060026AC RID: 9900 RVA: 0x000D0DE0 File Offset: 0x000CEFE0
	private void OnCopySettings(object data)
	{
		LogicHEPSensor component = ((GameObject)data).GetComponent<LogicHEPSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x000D0E1C File Offset: 0x000CF01C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.OnToggle += this.OnSwitchToggled;
		this.UpdateLogicCircuit();
		this.UpdateVisualState(true);
		this.wasOn = this.switchedOn;
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (System.Action)Delegate.Combine(logicCircuitManager.onLogicTick, new System.Action(this.LogicTick));
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x000D0E85 File Offset: 0x000CF085
	protected override void OnCleanUp()
	{
		LogicCircuitManager logicCircuitManager = Game.Instance.logicCircuitManager;
		logicCircuitManager.onLogicTick = (System.Action)Delegate.Remove(logicCircuitManager.onLogicTick, new System.Action(this.LogicTick));
		base.OnCleanUp();
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x000D0EB8 File Offset: 0x000CF0B8
	public void SimEveryTick(float dt)
	{
		if (this.waitForLogicTick)
		{
			return;
		}
		Vector2I vector2I = Grid.CellToXY(Grid.PosToCell(this));
		ListPool<ScenePartitionerEntry, LogicHEPSensor>.PooledList pooledList = ListPool<ScenePartitionerEntry, LogicHEPSensor>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(vector2I.x, vector2I.y, 1, 1, GameScenePartitioner.Instance.collisionLayer, pooledList);
		float num = 0f;
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			HighEnergyParticle component = (scenePartitionerEntry.obj as KCollider2D).gameObject.GetComponent<HighEnergyParticle>();
			if (!(component == null) && component.isCollideable)
			{
				num += component.payload;
			}
		}
		pooledList.Recycle();
		this.foundPayload = num;
		bool flag = (this.activateOnHigherThan && num > this.thresholdPayload) || (!this.activateOnHigherThan && num < this.thresholdPayload);
		if (flag != this.switchedOn)
		{
			this.waitForLogicTick = true;
		}
		this.SetState(flag);
	}

	// Token: 0x060026B0 RID: 9904 RVA: 0x000D0FC4 File Offset: 0x000CF1C4
	private void LogicTick()
	{
		this.waitForLogicTick = false;
	}

	// Token: 0x060026B1 RID: 9905 RVA: 0x000D0FCD File Offset: 0x000CF1CD
	private void OnSwitchToggled(bool toggled_on)
	{
		this.UpdateLogicCircuit();
		this.UpdateVisualState(false);
	}

	// Token: 0x060026B2 RID: 9906 RVA: 0x000D0FDC File Offset: 0x000CF1DC
	private void UpdateLogicCircuit()
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, this.switchedOn ? 1 : 0);
	}

	// Token: 0x060026B3 RID: 9907 RVA: 0x000D0FFC File Offset: 0x000CF1FC
	private void UpdateVisualState(bool force = false)
	{
		if (this.wasOn != this.switchedOn || force)
		{
			this.wasOn = this.switchedOn;
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			component.Play(this.switchedOn ? "on_pre" : "on_pst", KAnim.PlayMode.Once, 1f, 0f);
			component.Queue(this.switchedOn ? "on" : "off", KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x060026B4 RID: 9908 RVA: 0x000D1084 File Offset: 0x000CF284
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x17000239 RID: 569
	// (get) Token: 0x060026B5 RID: 9909 RVA: 0x000D10D7 File Offset: 0x000CF2D7
	// (set) Token: 0x060026B6 RID: 9910 RVA: 0x000D10DF File Offset: 0x000CF2DF
	public float Threshold
	{
		get
		{
			return this.thresholdPayload;
		}
		set
		{
			this.thresholdPayload = value;
			this.dirty = true;
		}
	}

	// Token: 0x1700023A RID: 570
	// (get) Token: 0x060026B7 RID: 9911 RVA: 0x000D10EF File Offset: 0x000CF2EF
	// (set) Token: 0x060026B8 RID: 9912 RVA: 0x000D10F7 File Offset: 0x000CF2F7
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateOnHigherThan;
		}
		set
		{
			this.activateOnHigherThan = value;
			this.dirty = true;
		}
	}

	// Token: 0x1700023B RID: 571
	// (get) Token: 0x060026B9 RID: 9913 RVA: 0x000D1107 File Offset: 0x000CF307
	public float CurrentValue
	{
		get
		{
			return this.foundPayload;
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x060026BA RID: 9914 RVA: 0x000D110F File Offset: 0x000CF30F
	public float RangeMin
	{
		get
		{
			return this.minPayload;
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x060026BB RID: 9915 RVA: 0x000D1117 File Offset: 0x000CF317
	public float RangeMax
	{
		get
		{
			return this.maxPayload;
		}
	}

	// Token: 0x060026BC RID: 9916 RVA: 0x000D111F File Offset: 0x000CF31F
	public float GetRangeMinInputField()
	{
		return this.minPayload;
	}

	// Token: 0x060026BD RID: 9917 RVA: 0x000D1127 File Offset: 0x000CF327
	public float GetRangeMaxInputField()
	{
		return this.maxPayload;
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x060026BE RID: 9918 RVA: 0x000D112F File Offset: 0x000CF32F
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.HEPSWITCHSIDESCREEN.TITLE;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x060026BF RID: 9919 RVA: 0x000D1136 File Offset: 0x000CF336
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.HEPS;
		}
	}

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x060026C0 RID: 9920 RVA: 0x000D113D File Offset: 0x000CF33D
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.HEPS_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x060026C1 RID: 9921 RVA: 0x000D1149 File Offset: 0x000CF349
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.HEPS_TOOLTIP_BELOW;
		}
	}

	// Token: 0x060026C2 RID: 9922 RVA: 0x000D1155 File Offset: 0x000CF355
	public string Format(float value, bool units)
	{
		return GameUtil.GetFormattedHighEnergyParticles(value, GameUtil.TimeSlice.None, units);
	}

	// Token: 0x060026C3 RID: 9923 RVA: 0x000D115F File Offset: 0x000CF35F
	public float ProcessedSliderValue(float input)
	{
		return Mathf.Round(input);
	}

	// Token: 0x060026C4 RID: 9924 RVA: 0x000D1167 File Offset: 0x000CF367
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x060026C5 RID: 9925 RVA: 0x000D116A File Offset: 0x000CF36A
	public LocString ThresholdValueUnits()
	{
		return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x060026C6 RID: 9926 RVA: 0x000D1171 File Offset: 0x000CF371
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x060026C7 RID: 9927 RVA: 0x000D1174 File Offset: 0x000CF374
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000244 RID: 580
	// (get) Token: 0x060026C8 RID: 9928 RVA: 0x000D1178 File Offset: 0x000CF378
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return new NonLinearSlider.Range[]
			{
				new NonLinearSlider.Range(30f, 50f),
				new NonLinearSlider.Range(30f, 200f),
				new NonLinearSlider.Range(40f, 500f)
			};
		}
	}

	// Token: 0x040016D8 RID: 5848
	[Serialize]
	public float thresholdPayload;

	// Token: 0x040016D9 RID: 5849
	[Serialize]
	public bool activateOnHigherThan;

	// Token: 0x040016DA RID: 5850
	[Serialize]
	public bool dirty = true;

	// Token: 0x040016DB RID: 5851
	private readonly float minPayload;

	// Token: 0x040016DC RID: 5852
	private readonly float maxPayload = 500f;

	// Token: 0x040016DD RID: 5853
	private float foundPayload;

	// Token: 0x040016DE RID: 5854
	private bool waitForLogicTick;

	// Token: 0x040016DF RID: 5855
	private bool wasOn;

	// Token: 0x040016E0 RID: 5856
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x040016E1 RID: 5857
	private static readonly EventSystem.IntraObjectHandler<LogicHEPSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicHEPSensor>(delegate(LogicHEPSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
