using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006F0 RID: 1776
[SkipSaveFileSerialization]
public class TemperatureVulnerable : StateMachineComponent<TemperatureVulnerable.StatesInstance>, IGameObjectEffectDescriptor, IWiltCause, ISlicedSim1000ms
{
	// Token: 0x1700036E RID: 878
	// (get) Token: 0x06003049 RID: 12361 RVA: 0x000FF0FC File Offset: 0x000FD2FC
	private OccupyArea occupyArea
	{
		get
		{
			if (this._occupyArea == null)
			{
				this._occupyArea = base.GetComponent<OccupyArea>();
			}
			return this._occupyArea;
		}
	}

	// Token: 0x1700036F RID: 879
	// (get) Token: 0x0600304A RID: 12362 RVA: 0x000FF11E File Offset: 0x000FD31E
	public float TemperatureLethalLow
	{
		get
		{
			return this.internalTemperatureLethal_Low;
		}
	}

	// Token: 0x17000370 RID: 880
	// (get) Token: 0x0600304B RID: 12363 RVA: 0x000FF126 File Offset: 0x000FD326
	public float TemperatureLethalHigh
	{
		get
		{
			return this.internalTemperatureLethal_High;
		}
	}

	// Token: 0x17000371 RID: 881
	// (get) Token: 0x0600304C RID: 12364 RVA: 0x000FF12E File Offset: 0x000FD32E
	public float TemperatureWarningLow
	{
		get
		{
			if (this.wiltTempRangeModAttribute != null)
			{
				return this.internalTemperatureWarning_Low + (1f - this.wiltTempRangeModAttribute.GetTotalValue()) * this.temperatureRangeModScalar;
			}
			return this.internalTemperatureWarning_Low;
		}
	}

	// Token: 0x17000372 RID: 882
	// (get) Token: 0x0600304D RID: 12365 RVA: 0x000FF15E File Offset: 0x000FD35E
	public float TemperatureWarningHigh
	{
		get
		{
			if (this.wiltTempRangeModAttribute != null)
			{
				return this.internalTemperatureWarning_High - (1f - this.wiltTempRangeModAttribute.GetTotalValue()) * this.temperatureRangeModScalar;
			}
			return this.internalTemperatureWarning_High;
		}
	}

	// Token: 0x14000015 RID: 21
	// (add) Token: 0x0600304E RID: 12366 RVA: 0x000FF190 File Offset: 0x000FD390
	// (remove) Token: 0x0600304F RID: 12367 RVA: 0x000FF1C8 File Offset: 0x000FD3C8
	public event Action<float, float> OnTemperature;

	// Token: 0x17000373 RID: 883
	// (get) Token: 0x06003050 RID: 12368 RVA: 0x000FF1FD File Offset: 0x000FD3FD
	public float InternalTemperature
	{
		get
		{
			return this.primaryElement.Temperature;
		}
	}

	// Token: 0x17000374 RID: 884
	// (get) Token: 0x06003051 RID: 12369 RVA: 0x000FF20A File Offset: 0x000FD40A
	public TemperatureVulnerable.TemperatureState GetInternalTemperatureState
	{
		get
		{
			return this.internalTemperatureState;
		}
	}

	// Token: 0x17000375 RID: 885
	// (get) Token: 0x06003052 RID: 12370 RVA: 0x000FF212 File Offset: 0x000FD412
	public bool IsLethal
	{
		get
		{
			return this.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.LethalHot || this.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.LethalCold;
		}
	}

	// Token: 0x17000376 RID: 886
	// (get) Token: 0x06003053 RID: 12371 RVA: 0x000FF228 File Offset: 0x000FD428
	public bool IsNormal
	{
		get
		{
			return this.GetInternalTemperatureState == TemperatureVulnerable.TemperatureState.Normal;
		}
	}

	// Token: 0x17000377 RID: 887
	// (get) Token: 0x06003054 RID: 12372 RVA: 0x000FF233 File Offset: 0x000FD433
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[1];
		}
	}

	// Token: 0x17000378 RID: 888
	// (get) Token: 0x06003055 RID: 12373 RVA: 0x000FF23C File Offset: 0x000FD43C
	public string WiltStateString
	{
		get
		{
			if (base.smi.IsInsideState(base.smi.sm.warningCold))
			{
				return Db.Get().CreatureStatusItems.Cold_Crop.resolveStringCallback(CREATURES.STATUSITEMS.COLD_CROP.NAME, this);
			}
			if (base.smi.IsInsideState(base.smi.sm.warningHot))
			{
				return Db.Get().CreatureStatusItems.Hot_Crop.resolveStringCallback(CREATURES.STATUSITEMS.HOT_CROP.NAME, this);
			}
			return "";
		}
	}

	// Token: 0x06003056 RID: 12374 RVA: 0x000FF2D4 File Offset: 0x000FD4D4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Amounts amounts = base.gameObject.GetAmounts();
		this.displayTemperatureAmount = amounts.Add(new AmountInstance(Db.Get().Amounts.Temperature, base.gameObject));
	}

	// Token: 0x06003057 RID: 12375 RVA: 0x000FF31C File Offset: 0x000FD51C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.wiltTempRangeModAttribute = this.GetAttributes().Get(Db.Get().PlantAttributes.WiltTempRangeMod);
		this.temperatureRangeModScalar = (this.internalTemperatureWarning_High - this.internalTemperatureWarning_Low) / 2f;
		SlicedUpdaterSim1000ms<TemperatureVulnerable>.instance.RegisterUpdate1000ms(this);
		base.smi.sm.internalTemp.Set(this.primaryElement.Temperature, base.smi, false);
		base.smi.StartSM();
	}

	// Token: 0x06003058 RID: 12376 RVA: 0x000FF3A6 File Offset: 0x000FD5A6
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		SlicedUpdaterSim1000ms<TemperatureVulnerable>.instance.UnregisterUpdate1000ms(this);
	}

	// Token: 0x06003059 RID: 12377 RVA: 0x000FF3B9 File Offset: 0x000FD5B9
	public void Configure(float tempWarningLow, float tempLethalLow, float tempWarningHigh, float tempLethalHigh)
	{
		this.internalTemperatureWarning_Low = tempWarningLow;
		this.internalTemperatureLethal_Low = tempLethalLow;
		this.internalTemperatureLethal_High = tempLethalHigh;
		this.internalTemperatureWarning_High = tempWarningHigh;
	}

	// Token: 0x0600305A RID: 12378 RVA: 0x000FF3D8 File Offset: 0x000FD5D8
	public bool IsCellSafe(int cell)
	{
		float averageTemperature = this.GetAverageTemperature(cell);
		return averageTemperature > -1f && averageTemperature > this.TemperatureLethalLow && averageTemperature < this.internalTemperatureLethal_High;
	}

	// Token: 0x0600305B RID: 12379 RVA: 0x000FF40C File Offset: 0x000FD60C
	public void SlicedSim1000ms(float dt)
	{
		if (!Grid.IsValidCell(Grid.PosToCell(base.gameObject)))
		{
			return;
		}
		base.smi.sm.internalTemp.Set(this.InternalTemperature, base.smi, false);
		this.displayTemperatureAmount.value = this.InternalTemperature;
		if (this.OnTemperature != null)
		{
			this.OnTemperature(dt, this.InternalTemperature);
		}
	}

	// Token: 0x0600305C RID: 12380 RVA: 0x000FF47C File Offset: 0x000FD67C
	private static bool GetAverageTemperatureCb(int cell, object data)
	{
		TemperatureVulnerable temperatureVulnerable = data as TemperatureVulnerable;
		if (Grid.Mass[cell] > 0.1f)
		{
			temperatureVulnerable.averageTemp += Grid.Temperature[cell];
			temperatureVulnerable.cellCount++;
		}
		return true;
	}

	// Token: 0x0600305D RID: 12381 RVA: 0x000FF4CC File Offset: 0x000FD6CC
	private float GetAverageTemperature(int cell)
	{
		this.averageTemp = 0f;
		this.cellCount = 0;
		this.occupyArea.TestArea(cell, this, TemperatureVulnerable.GetAverageTemperatureCbDelegate);
		if (this.cellCount > 0)
		{
			return this.averageTemp / (float)this.cellCount;
		}
		return -1f;
	}

	// Token: 0x0600305E RID: 12382 RVA: 0x000FF51C File Offset: 0x000FD71C
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		float num = (this.internalTemperatureWarning_High - this.internalTemperatureWarning_Low) / 2f;
		float num2 = ((this.wiltTempRangeModAttribute != null) ? this.TemperatureWarningLow : (this.internalTemperatureWarning_Low + (1f - base.GetComponent<Modifiers>().GetPreModifiedAttributeValue(Db.Get().PlantAttributes.WiltTempRangeMod)) * num));
		float num3 = ((this.wiltTempRangeModAttribute != null) ? this.TemperatureWarningHigh : (this.internalTemperatureWarning_High - (1f - base.GetComponent<Modifiers>().GetPreModifiedAttributeValue(Db.Get().PlantAttributes.WiltTempRangeMod)) * num));
		return new List<Descriptor>
		{
			new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_TEMPERATURE, GameUtil.GetFormattedTemperature(num2, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, false, false), GameUtil.GetFormattedTemperature(num3, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_TEMPERATURE, GameUtil.GetFormattedTemperature(num2, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, false, false), GameUtil.GetFormattedTemperature(num3, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)), Descriptor.DescriptorType.Requirement, false)
		};
	}

	// Token: 0x04001D1A RID: 7450
	private OccupyArea _occupyArea;

	// Token: 0x04001D1B RID: 7451
	[SerializeField]
	private float internalTemperatureLethal_Low;

	// Token: 0x04001D1C RID: 7452
	[SerializeField]
	private float internalTemperatureWarning_Low;

	// Token: 0x04001D1D RID: 7453
	[SerializeField]
	private float internalTemperatureWarning_High;

	// Token: 0x04001D1E RID: 7454
	[SerializeField]
	private float internalTemperatureLethal_High;

	// Token: 0x04001D1F RID: 7455
	private AttributeInstance wiltTempRangeModAttribute;

	// Token: 0x04001D20 RID: 7456
	private float temperatureRangeModScalar;

	// Token: 0x04001D22 RID: 7458
	private const float minimumMassForReading = 0.1f;

	// Token: 0x04001D23 RID: 7459
	[MyCmpReq]
	private PrimaryElement primaryElement;

	// Token: 0x04001D24 RID: 7460
	[MyCmpReq]
	private SimTemperatureTransfer temperatureTransfer;

	// Token: 0x04001D25 RID: 7461
	private AmountInstance displayTemperatureAmount;

	// Token: 0x04001D26 RID: 7462
	private TemperatureVulnerable.TemperatureState internalTemperatureState = TemperatureVulnerable.TemperatureState.Normal;

	// Token: 0x04001D27 RID: 7463
	private float averageTemp;

	// Token: 0x04001D28 RID: 7464
	private int cellCount;

	// Token: 0x04001D29 RID: 7465
	private static readonly Func<int, object, bool> GetAverageTemperatureCbDelegate = (int cell, object data) => TemperatureVulnerable.GetAverageTemperatureCb(cell, data);

	// Token: 0x0200140C RID: 5132
	public class StatesInstance : GameStateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.GameInstance
	{
		// Token: 0x06007FE5 RID: 32741 RVA: 0x002DDFD5 File Offset: 0x002DC1D5
		public StatesInstance(TemperatureVulnerable master)
			: base(master)
		{
			if (Db.Get().Amounts.Maturity.Lookup(base.gameObject) != null)
			{
				this.hasMaturity = true;
			}
		}

		// Token: 0x04006254 RID: 25172
		public bool hasMaturity;
	}

	// Token: 0x0200140D RID: 5133
	public class States : GameStateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable>
	{
		// Token: 0x06007FE6 RID: 32742 RVA: 0x002DE004 File Offset: 0x002DC204
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.normal;
			this.lethalCold.Enter(delegate(TemperatureVulnerable.StatesInstance smi)
			{
				smi.master.internalTemperatureState = TemperatureVulnerable.TemperatureState.LethalCold;
			}).TriggerOnEnter(GameHashes.TooColdFatal, null).ParamTransition<float>(this.internalTemp, this.warningCold, (TemperatureVulnerable.StatesInstance smi, float p) => p > smi.master.TemperatureLethalLow)
				.Enter(new StateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.State.Callback(TemperatureVulnerable.States.Kill));
			this.lethalHot.Enter(delegate(TemperatureVulnerable.StatesInstance smi)
			{
				smi.master.internalTemperatureState = TemperatureVulnerable.TemperatureState.LethalHot;
			}).TriggerOnEnter(GameHashes.TooHotFatal, null).ParamTransition<float>(this.internalTemp, this.warningHot, (TemperatureVulnerable.StatesInstance smi, float p) => p < smi.master.TemperatureLethalHigh)
				.Enter(new StateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.State.Callback(TemperatureVulnerable.States.Kill));
			this.warningCold.Enter(delegate(TemperatureVulnerable.StatesInstance smi)
			{
				smi.master.internalTemperatureState = TemperatureVulnerable.TemperatureState.WarningCold;
			}).TriggerOnEnter(GameHashes.TooColdWarning, null).ParamTransition<float>(this.internalTemp, this.lethalCold, (TemperatureVulnerable.StatesInstance smi, float p) => p < smi.master.TemperatureLethalLow)
				.ParamTransition<float>(this.internalTemp, this.normal, (TemperatureVulnerable.StatesInstance smi, float p) => p > smi.master.TemperatureWarningLow);
			this.warningHot.Enter(delegate(TemperatureVulnerable.StatesInstance smi)
			{
				smi.master.internalTemperatureState = TemperatureVulnerable.TemperatureState.WarningHot;
			}).TriggerOnEnter(GameHashes.TooHotWarning, null).ParamTransition<float>(this.internalTemp, this.lethalHot, (TemperatureVulnerable.StatesInstance smi, float p) => p > smi.master.TemperatureLethalHigh)
				.ParamTransition<float>(this.internalTemp, this.normal, (TemperatureVulnerable.StatesInstance smi, float p) => p < smi.master.TemperatureWarningHigh);
			this.normal.Enter(delegate(TemperatureVulnerable.StatesInstance smi)
			{
				smi.master.internalTemperatureState = TemperatureVulnerable.TemperatureState.Normal;
			}).TriggerOnEnter(GameHashes.OptimalTemperatureAchieved, null).ParamTransition<float>(this.internalTemp, this.warningHot, (TemperatureVulnerable.StatesInstance smi, float p) => p > smi.master.TemperatureWarningHigh)
				.ParamTransition<float>(this.internalTemp, this.warningCold, (TemperatureVulnerable.StatesInstance smi, float p) => p < smi.master.TemperatureWarningLow);
		}

		// Token: 0x06007FE7 RID: 32743 RVA: 0x002DE2CC File Offset: 0x002DC4CC
		private static void Kill(StateMachine.Instance smi)
		{
			DeathMonitor.Instance smi2 = smi.GetSMI<DeathMonitor.Instance>();
			if (smi2 != null)
			{
				smi2.Kill(Db.Get().Deaths.Generic);
			}
		}

		// Token: 0x04006255 RID: 25173
		public StateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.FloatParameter internalTemp;

		// Token: 0x04006256 RID: 25174
		public GameStateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.State lethalCold;

		// Token: 0x04006257 RID: 25175
		public GameStateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.State lethalHot;

		// Token: 0x04006258 RID: 25176
		public GameStateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.State warningCold;

		// Token: 0x04006259 RID: 25177
		public GameStateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.State warningHot;

		// Token: 0x0400625A RID: 25178
		public GameStateMachine<TemperatureVulnerable.States, TemperatureVulnerable.StatesInstance, TemperatureVulnerable, object>.State normal;
	}

	// Token: 0x0200140E RID: 5134
	public enum TemperatureState
	{
		// Token: 0x0400625C RID: 25180
		LethalCold,
		// Token: 0x0400625D RID: 25181
		WarningCold,
		// Token: 0x0400625E RID: 25182
		Normal,
		// Token: 0x0400625F RID: 25183
		WarningHot,
		// Token: 0x04006260 RID: 25184
		LethalHot
	}
}
