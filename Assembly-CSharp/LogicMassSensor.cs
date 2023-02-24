using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005F0 RID: 1520
[SerializationConfig(MemberSerialization.OptIn)]
public class LogicMassSensor : Switch, ISaveLoadable, IThresholdSwitch
{
	// Token: 0x060026D3 RID: 9939 RVA: 0x000D173F File Offset: 0x000CF93F
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicMassSensor>(-905833192, LogicMassSensor.OnCopySettingsDelegate);
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x000D1758 File Offset: 0x000CF958
	private void OnCopySettings(object data)
	{
		LogicMassSensor component = ((GameObject)data).GetComponent<LogicMassSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x060026D5 RID: 9941 RVA: 0x000D1794 File Offset: 0x000CF994
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateVisualState(true);
		int num = Grid.CellAbove(this.NaturalBuildingCell());
		this.solidChangedEntry = GameScenePartitioner.Instance.Add("LogicMassSensor.SolidChanged", base.gameObject, num, GameScenePartitioner.Instance.solidChangedLayer, new Action<object>(this.OnSolidChanged));
		this.pickupablesChangedEntry = GameScenePartitioner.Instance.Add("LogicMassSensor.PickupablesChanged", base.gameObject, num, GameScenePartitioner.Instance.pickupablesChangedLayer, new Action<object>(this.OnPickupablesChanged));
		this.floorSwitchActivatorChangedEntry = GameScenePartitioner.Instance.Add("LogicMassSensor.SwitchActivatorChanged", base.gameObject, num, GameScenePartitioner.Instance.floorSwitchActivatorChangedLayer, new Action<object>(this.OnActivatorsChanged));
		base.OnToggle += this.SwitchToggled;
	}

	// Token: 0x060026D6 RID: 9942 RVA: 0x000D1862 File Offset: 0x000CFA62
	protected override void OnCleanUp()
	{
		GameScenePartitioner.Instance.Free(ref this.solidChangedEntry);
		GameScenePartitioner.Instance.Free(ref this.pickupablesChangedEntry);
		GameScenePartitioner.Instance.Free(ref this.floorSwitchActivatorChangedEntry);
		base.OnCleanUp();
	}

	// Token: 0x060026D7 RID: 9943 RVA: 0x000D189C File Offset: 0x000CFA9C
	private void Update()
	{
		this.toggleCooldown = Mathf.Max(0f, this.toggleCooldown - Time.deltaTime);
		if (this.toggleCooldown == 0f)
		{
			float currentValue = this.CurrentValue;
			if ((this.activateAboveThreshold ? (currentValue > this.threshold) : (currentValue < this.threshold)) != base.IsSwitchedOn)
			{
				this.Toggle();
				this.toggleCooldown = 0.15f;
			}
			this.UpdateVisualState(false);
		}
	}

	// Token: 0x060026D8 RID: 9944 RVA: 0x000D1918 File Offset: 0x000CFB18
	private void OnSolidChanged(object data)
	{
		int num = Grid.CellAbove(this.NaturalBuildingCell());
		if (Grid.Solid[num])
		{
			this.massSolid = Grid.Mass[num];
			return;
		}
		this.massSolid = 0f;
	}

	// Token: 0x060026D9 RID: 9945 RVA: 0x000D195C File Offset: 0x000CFB5C
	private void OnPickupablesChanged(object data)
	{
		float num = 0f;
		int num2 = Grid.CellAbove(this.NaturalBuildingCell());
		ListPool<ScenePartitionerEntry, LogicMassSensor>.PooledList pooledList = ListPool<ScenePartitionerEntry, LogicMassSensor>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(Grid.CellToXY(num2).x, Grid.CellToXY(num2).y, 1, 1, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			Pickupable pickupable = pooledList[i].obj as Pickupable;
			if (!(pickupable == null) && !pickupable.wasAbsorbed)
			{
				KPrefabID component = pickupable.GetComponent<KPrefabID>();
				if (!component.HasTag(GameTags.Creature) || (component.HasTag(GameTags.Creatures.Walker) || component.HasTag(GameTags.Creatures.Hoverer) || component.HasTag(GameTags.Creatures.Flopping)))
				{
					num += pickupable.PrimaryElement.Mass;
				}
			}
		}
		pooledList.Recycle();
		this.massPickupables = num;
	}

	// Token: 0x060026DA RID: 9946 RVA: 0x000D1A48 File Offset: 0x000CFC48
	private void OnActivatorsChanged(object data)
	{
		float num = 0f;
		int num2 = Grid.CellAbove(this.NaturalBuildingCell());
		ListPool<ScenePartitionerEntry, LogicMassSensor>.PooledList pooledList = ListPool<ScenePartitionerEntry, LogicMassSensor>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(Grid.CellToXY(num2).x, Grid.CellToXY(num2).y, 1, 1, GameScenePartitioner.Instance.floorSwitchActivatorLayer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			FloorSwitchActivator floorSwitchActivator = pooledList[i].obj as FloorSwitchActivator;
			if (!(floorSwitchActivator == null))
			{
				num += floorSwitchActivator.PrimaryElement.Mass;
			}
		}
		pooledList.Recycle();
		this.massActivators = num;
	}

	// Token: 0x17000245 RID: 581
	// (get) Token: 0x060026DB RID: 9947 RVA: 0x000D1AE4 File Offset: 0x000CFCE4
	public LocString Title
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.TITLE;
		}
	}

	// Token: 0x17000246 RID: 582
	// (get) Token: 0x060026DC RID: 9948 RVA: 0x000D1AEB File Offset: 0x000CFCEB
	// (set) Token: 0x060026DD RID: 9949 RVA: 0x000D1AF3 File Offset: 0x000CFCF3
	public float Threshold
	{
		get
		{
			return this.threshold;
		}
		set
		{
			this.threshold = value;
		}
	}

	// Token: 0x17000247 RID: 583
	// (get) Token: 0x060026DE RID: 9950 RVA: 0x000D1AFC File Offset: 0x000CFCFC
	// (set) Token: 0x060026DF RID: 9951 RVA: 0x000D1B04 File Offset: 0x000CFD04
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateAboveThreshold;
		}
		set
		{
			this.activateAboveThreshold = value;
		}
	}

	// Token: 0x17000248 RID: 584
	// (get) Token: 0x060026E0 RID: 9952 RVA: 0x000D1B0D File Offset: 0x000CFD0D
	public float CurrentValue
	{
		get
		{
			return this.massSolid + this.massPickupables + this.massActivators;
		}
	}

	// Token: 0x17000249 RID: 585
	// (get) Token: 0x060026E1 RID: 9953 RVA: 0x000D1B23 File Offset: 0x000CFD23
	public float RangeMin
	{
		get
		{
			return this.rangeMin;
		}
	}

	// Token: 0x1700024A RID: 586
	// (get) Token: 0x060026E2 RID: 9954 RVA: 0x000D1B2B File Offset: 0x000CFD2B
	public float RangeMax
	{
		get
		{
			return this.rangeMax;
		}
	}

	// Token: 0x060026E3 RID: 9955 RVA: 0x000D1B33 File Offset: 0x000CFD33
	public float GetRangeMinInputField()
	{
		return this.rangeMin;
	}

	// Token: 0x060026E4 RID: 9956 RVA: 0x000D1B3B File Offset: 0x000CFD3B
	public float GetRangeMaxInputField()
	{
		return this.rangeMax;
	}

	// Token: 0x1700024B RID: 587
	// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000D1B43 File Offset: 0x000CFD43
	public LocString ThresholdValueName
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE;
		}
	}

	// Token: 0x1700024C RID: 588
	// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000D1B4A File Offset: 0x000CFD4A
	public string AboveToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_ABOVE;
		}
	}

	// Token: 0x1700024D RID: 589
	// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000D1B56 File Offset: 0x000CFD56
	public string BelowToolTip
	{
		get
		{
			return UI.UISIDESCREENS.THRESHOLD_SWITCH_SIDESCREEN.PRESSURE_TOOLTIP_BELOW;
		}
	}

	// Token: 0x060026E8 RID: 9960 RVA: 0x000D1B64 File Offset: 0x000CFD64
	public string Format(float value, bool units)
	{
		GameUtil.MetricMassFormat metricMassFormat = GameUtil.MetricMassFormat.Kilogram;
		return GameUtil.GetFormattedMass(value, GameUtil.TimeSlice.None, metricMassFormat, units, "{0:0.#}");
	}

	// Token: 0x060026E9 RID: 9961 RVA: 0x000D1B83 File Offset: 0x000CFD83
	public float ProcessedSliderValue(float input)
	{
		input = Mathf.Round(input);
		return input;
	}

	// Token: 0x060026EA RID: 9962 RVA: 0x000D1B8E File Offset: 0x000CFD8E
	public float ProcessedInputValue(float input)
	{
		return input;
	}

	// Token: 0x060026EB RID: 9963 RVA: 0x000D1B91 File Offset: 0x000CFD91
	public LocString ThresholdValueUnits()
	{
		return GameUtil.GetCurrentMassUnit(false);
	}

	// Token: 0x1700024E RID: 590
	// (get) Token: 0x060026EC RID: 9964 RVA: 0x000D1B99 File Offset: 0x000CFD99
	public ThresholdScreenLayoutType LayoutType
	{
		get
		{
			return ThresholdScreenLayoutType.SliderBar;
		}
	}

	// Token: 0x1700024F RID: 591
	// (get) Token: 0x060026ED RID: 9965 RVA: 0x000D1B9C File Offset: 0x000CFD9C
	public int IncrementScale
	{
		get
		{
			return 1;
		}
	}

	// Token: 0x17000250 RID: 592
	// (get) Token: 0x060026EE RID: 9966 RVA: 0x000D1B9F File Offset: 0x000CFD9F
	public NonLinearSlider.Range[] GetRanges
	{
		get
		{
			return NonLinearSlider.GetDefaultRange(this.RangeMax);
		}
	}

	// Token: 0x060026EF RID: 9967 RVA: 0x000D1BAC File Offset: 0x000CFDAC
	private void SwitchToggled(bool toggled_on)
	{
		base.GetComponent<LogicPorts>().SendSignal(LogicSwitch.PORT_ID, toggled_on ? 1 : 0);
	}

	// Token: 0x060026F0 RID: 9968 RVA: 0x000D1BC8 File Offset: 0x000CFDC8
	private void UpdateVisualState(bool force = false)
	{
		bool flag = this.CurrentValue > this.threshold;
		if (flag != this.was_pressed || this.was_on != base.IsSwitchedOn || force)
		{
			KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
			if (flag)
			{
				if (force)
				{
					component.Play(base.IsSwitchedOn ? "on_down" : "off_down", KAnim.PlayMode.Once, 1f, 0f);
				}
				else
				{
					component.Play(base.IsSwitchedOn ? "on_down_pre" : "off_down_pre", KAnim.PlayMode.Once, 1f, 0f);
					component.Queue(base.IsSwitchedOn ? "on_down" : "off_down", KAnim.PlayMode.Once, 1f, 0f);
				}
			}
			else if (force)
			{
				component.Play(base.IsSwitchedOn ? "on_up" : "off_up", KAnim.PlayMode.Once, 1f, 0f);
			}
			else
			{
				component.Play(base.IsSwitchedOn ? "on_up_pre" : "off_up_pre", KAnim.PlayMode.Once, 1f, 0f);
				component.Queue(base.IsSwitchedOn ? "on_up" : "off_up", KAnim.PlayMode.Once, 1f, 0f);
			}
			this.was_pressed = flag;
			this.was_on = base.IsSwitchedOn;
		}
	}

	// Token: 0x060026F1 RID: 9969 RVA: 0x000D1D38 File Offset: 0x000CFF38
	protected override void UpdateSwitchStatus()
	{
		StatusItem statusItem = (this.switchedOn ? Db.Get().BuildingStatusItems.LogicSensorStatusActive : Db.Get().BuildingStatusItems.LogicSensorStatusInactive);
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Power, statusItem, null);
	}

	// Token: 0x040016F2 RID: 5874
	[SerializeField]
	[Serialize]
	private float threshold;

	// Token: 0x040016F3 RID: 5875
	[SerializeField]
	[Serialize]
	private bool activateAboveThreshold = true;

	// Token: 0x040016F4 RID: 5876
	[MyCmpGet]
	private LogicPorts logicPorts;

	// Token: 0x040016F5 RID: 5877
	private bool was_pressed;

	// Token: 0x040016F6 RID: 5878
	private bool was_on;

	// Token: 0x040016F7 RID: 5879
	public float rangeMin;

	// Token: 0x040016F8 RID: 5880
	public float rangeMax = 1f;

	// Token: 0x040016F9 RID: 5881
	[Serialize]
	private float massSolid;

	// Token: 0x040016FA RID: 5882
	[Serialize]
	private float massPickupables;

	// Token: 0x040016FB RID: 5883
	[Serialize]
	private float massActivators;

	// Token: 0x040016FC RID: 5884
	private const float MIN_TOGGLE_TIME = 0.15f;

	// Token: 0x040016FD RID: 5885
	private float toggleCooldown = 0.15f;

	// Token: 0x040016FE RID: 5886
	private HandleVector<int>.Handle solidChangedEntry;

	// Token: 0x040016FF RID: 5887
	private HandleVector<int>.Handle pickupablesChangedEntry;

	// Token: 0x04001700 RID: 5888
	private HandleVector<int>.Handle floorSwitchActivatorChangedEntry;

	// Token: 0x04001701 RID: 5889
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001702 RID: 5890
	private static readonly EventSystem.IntraObjectHandler<LogicMassSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicMassSensor>(delegate(LogicMassSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
