using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x020006E4 RID: 1764
[SkipSaveFileSerialization]
public class PressureVulnerable : StateMachineComponent<PressureVulnerable.StatesInstance>, IGameObjectEffectDescriptor, IWiltCause, ISlicedSim1000ms
{
	// Token: 0x17000362 RID: 866
	// (get) Token: 0x06002FFA RID: 12282 RVA: 0x000FD924 File Offset: 0x000FBB24
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

	// Token: 0x06002FFB RID: 12283 RVA: 0x000FD946 File Offset: 0x000FBB46
	public bool IsSafeElement(Element element)
	{
		return this.safe_atmospheres == null || this.safe_atmospheres.Count == 0 || this.safe_atmospheres.Contains(element);
	}

	// Token: 0x17000363 RID: 867
	// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000FD96E File Offset: 0x000FBB6E
	public PressureVulnerable.PressureState ExternalPressureState
	{
		get
		{
			return this.pressureState;
		}
	}

	// Token: 0x17000364 RID: 868
	// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000FD976 File Offset: 0x000FBB76
	public bool IsLethal
	{
		get
		{
			return this.pressureState == PressureVulnerable.PressureState.LethalHigh || this.pressureState == PressureVulnerable.PressureState.LethalLow || !this.testAreaElementSafe;
		}
	}

	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06002FFE RID: 12286 RVA: 0x000FD994 File Offset: 0x000FBB94
	public bool IsNormal
	{
		get
		{
			return this.testAreaElementSafe && this.pressureState == PressureVulnerable.PressureState.Normal;
		}
	}

	// Token: 0x06002FFF RID: 12287 RVA: 0x000FD9AC File Offset: 0x000FBBAC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Amounts amounts = base.gameObject.GetAmounts();
		this.displayPressureAmount = amounts.Add(new AmountInstance(Db.Get().Amounts.AirPressure, base.gameObject));
	}

	// Token: 0x06003000 RID: 12288 RVA: 0x000FD9F4 File Offset: 0x000FBBF4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SlicedUpdaterSim1000ms<PressureVulnerable>.instance.RegisterUpdate1000ms(this);
		this.cell = Grid.PosToCell(this);
		base.smi.sm.pressure.Set(1f, base.smi, false);
		base.smi.sm.safe_element.Set(this.testAreaElementSafe, base.smi, false);
		base.smi.master.pressureAccumulator = Game.Instance.accumulators.Add("pressureAccumulator", this);
		base.smi.master.elementAccumulator = Game.Instance.accumulators.Add("elementAccumulator", this);
		base.smi.StartSM();
	}

	// Token: 0x06003001 RID: 12289 RVA: 0x000FDAB8 File Offset: 0x000FBCB8
	protected override void OnCleanUp()
	{
		SlicedUpdaterSim1000ms<PressureVulnerable>.instance.UnregisterUpdate1000ms(this);
		base.OnCleanUp();
	}

	// Token: 0x06003002 RID: 12290 RVA: 0x000FDACC File Offset: 0x000FBCCC
	public void Configure(SimHashes[] safeAtmospheres = null)
	{
		this.pressure_sensitive = false;
		this.pressureWarning_Low = float.MinValue;
		this.pressureLethal_Low = float.MinValue;
		this.pressureLethal_High = float.MaxValue;
		this.pressureWarning_High = float.MaxValue;
		this.safe_atmospheres = new HashSet<Element>();
		if (safeAtmospheres != null)
		{
			foreach (SimHashes simHashes in safeAtmospheres)
			{
				this.safe_atmospheres.Add(ElementLoader.FindElementByHash(simHashes));
			}
		}
	}

	// Token: 0x06003003 RID: 12291 RVA: 0x000FDB40 File Offset: 0x000FBD40
	public void Configure(float pressureWarningLow = 0.25f, float pressureLethalLow = 0.01f, float pressureWarningHigh = 10f, float pressureLethalHigh = 30f, SimHashes[] safeAtmospheres = null)
	{
		this.pressure_sensitive = true;
		this.pressureWarning_Low = pressureWarningLow;
		this.pressureLethal_Low = pressureLethalLow;
		this.pressureLethal_High = pressureLethalHigh;
		this.pressureWarning_High = pressureWarningHigh;
		this.safe_atmospheres = new HashSet<Element>();
		if (safeAtmospheres != null)
		{
			foreach (SimHashes simHashes in safeAtmospheres)
			{
				this.safe_atmospheres.Add(ElementLoader.FindElementByHash(simHashes));
			}
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06003004 RID: 12292 RVA: 0x000FDBA7 File Offset: 0x000FBDA7
	WiltCondition.Condition[] IWiltCause.Conditions
	{
		get
		{
			return new WiltCondition.Condition[]
			{
				WiltCondition.Condition.Pressure,
				WiltCondition.Condition.AtmosphereElement
			};
		}
	}

	// Token: 0x17000367 RID: 871
	// (get) Token: 0x06003005 RID: 12293 RVA: 0x000FDBB8 File Offset: 0x000FBDB8
	public string WiltStateString
	{
		get
		{
			string text = "";
			if (base.smi.IsInsideState(base.smi.sm.warningLow) || base.smi.IsInsideState(base.smi.sm.lethalLow))
			{
				text += Db.Get().CreatureStatusItems.AtmosphericPressureTooLow.resolveStringCallback(CREATURES.STATUSITEMS.ATMOSPHERICPRESSURETOOLOW.NAME, this);
			}
			else if (base.smi.IsInsideState(base.smi.sm.warningHigh) || base.smi.IsInsideState(base.smi.sm.lethalHigh))
			{
				text += Db.Get().CreatureStatusItems.AtmosphericPressureTooHigh.resolveStringCallback(CREATURES.STATUSITEMS.ATMOSPHERICPRESSURETOOHIGH.NAME, this);
			}
			else if (base.smi.IsInsideState(base.smi.sm.unsafeElement))
			{
				text += Db.Get().CreatureStatusItems.WrongAtmosphere.resolveStringCallback(CREATURES.STATUSITEMS.WRONGATMOSPHERE.NAME, this);
			}
			return text;
		}
	}

	// Token: 0x06003006 RID: 12294 RVA: 0x000FDCE5 File Offset: 0x000FBEE5
	public bool IsSafePressure(float pressure)
	{
		return !this.pressure_sensitive || (pressure > this.pressureLethal_Low && pressure < this.pressureLethal_High);
	}

	// Token: 0x06003007 RID: 12295 RVA: 0x000FDD08 File Offset: 0x000FBF08
	public void SlicedSim1000ms(float dt)
	{
		float pressureOverArea = this.GetPressureOverArea(this.cell);
		Game.Instance.accumulators.Accumulate(base.smi.master.pressureAccumulator, pressureOverArea);
		float averageRate = Game.Instance.accumulators.GetAverageRate(base.smi.master.pressureAccumulator);
		this.displayPressureAmount.value = averageRate;
		Game.Instance.accumulators.Accumulate(base.smi.master.elementAccumulator, this.testAreaElementSafe ? 1f : 0f);
		bool flag = Game.Instance.accumulators.GetAverageRate(base.smi.master.elementAccumulator) > 0f;
		base.smi.sm.safe_element.Set(flag, base.smi, false);
		base.smi.sm.pressure.Set(averageRate, base.smi, false);
	}

	// Token: 0x06003008 RID: 12296 RVA: 0x000FDE08 File Offset: 0x000FC008
	public float GetExternalPressure()
	{
		return this.GetPressureOverArea(this.cell);
	}

	// Token: 0x06003009 RID: 12297 RVA: 0x000FDE18 File Offset: 0x000FC018
	private float GetPressureOverArea(int cell)
	{
		bool flag = this.testAreaElementSafe;
		PressureVulnerable.testAreaPressure = 0f;
		PressureVulnerable.testAreaCount = 0;
		this.testAreaElementSafe = false;
		this.currentAtmoElement = null;
		this.occupyArea.TestArea(cell, this, PressureVulnerable.testAreaCB);
		this.occupyArea.TestAreaAbove(cell, this, PressureVulnerable.testAreaCB);
		PressureVulnerable.testAreaPressure = ((PressureVulnerable.testAreaCount > 0) ? (PressureVulnerable.testAreaPressure / (float)PressureVulnerable.testAreaCount) : 0f);
		if (this.testAreaElementSafe != flag)
		{
			base.Trigger(-2023773544, null);
		}
		return PressureVulnerable.testAreaPressure;
	}

	// Token: 0x0600300A RID: 12298 RVA: 0x000FDEAC File Offset: 0x000FC0AC
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		List<Descriptor> list = new List<Descriptor>();
		if (this.pressure_sensitive)
		{
			list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_PRESSURE, GameUtil.GetFormattedMass(this.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_PRESSURE, GameUtil.GetFormattedMass(this.pressureWarning_Low, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}")), Descriptor.DescriptorType.Requirement, false));
		}
		if (this.safe_atmospheres != null && this.safe_atmospheres.Count > 0)
		{
			string text = "";
			bool flag = false;
			bool flag2 = false;
			foreach (Element element in this.safe_atmospheres)
			{
				flag |= element.IsGas;
				flag2 |= element.IsLiquid;
				text = text + "\n        • " + element.name;
			}
			if (flag && flag2)
			{
				list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_ATMOSPHERE, text), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_ATMOSPHERE_MIXED, text), Descriptor.DescriptorType.Requirement, false));
			}
			if (flag)
			{
				list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_ATMOSPHERE, text), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_ATMOSPHERE, text), Descriptor.DescriptorType.Requirement, false));
			}
			else
			{
				list.Add(new Descriptor(string.Format(UI.GAMEOBJECTEFFECTS.REQUIRES_ATMOSPHERE, text), string.Format(UI.GAMEOBJECTEFFECTS.TOOLTIPS.REQUIRES_ATMOSPHERE_LIQUID, text), Descriptor.DescriptorType.Requirement, false));
			}
		}
		return list;
	}

	// Token: 0x04001CEE RID: 7406
	private HandleVector<int>.Handle pressureAccumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04001CEF RID: 7407
	private HandleVector<int>.Handle elementAccumulator = HandleVector<int>.InvalidHandle;

	// Token: 0x04001CF0 RID: 7408
	private OccupyArea _occupyArea;

	// Token: 0x04001CF1 RID: 7409
	public float pressureLethal_Low;

	// Token: 0x04001CF2 RID: 7410
	public float pressureWarning_Low;

	// Token: 0x04001CF3 RID: 7411
	public float pressureWarning_High;

	// Token: 0x04001CF4 RID: 7412
	public float pressureLethal_High;

	// Token: 0x04001CF5 RID: 7413
	private static float testAreaPressure;

	// Token: 0x04001CF6 RID: 7414
	private static int testAreaCount;

	// Token: 0x04001CF7 RID: 7415
	public bool testAreaElementSafe = true;

	// Token: 0x04001CF8 RID: 7416
	public Element currentAtmoElement;

	// Token: 0x04001CF9 RID: 7417
	private static Func<int, object, bool> testAreaCB = delegate(int test_cell, object data)
	{
		PressureVulnerable pressureVulnerable = (PressureVulnerable)data;
		if (!Grid.IsSolidCell(test_cell))
		{
			Element element = Grid.Element[test_cell];
			if (pressureVulnerable.IsSafeElement(element))
			{
				PressureVulnerable.testAreaPressure += Grid.Mass[test_cell];
				PressureVulnerable.testAreaCount++;
				pressureVulnerable.testAreaElementSafe = true;
				pressureVulnerable.currentAtmoElement = element;
			}
			if (pressureVulnerable.currentAtmoElement == null)
			{
				pressureVulnerable.currentAtmoElement = element;
			}
		}
		return true;
	};

	// Token: 0x04001CFA RID: 7418
	private AmountInstance displayPressureAmount;

	// Token: 0x04001CFB RID: 7419
	public bool pressure_sensitive = true;

	// Token: 0x04001CFC RID: 7420
	public HashSet<Element> safe_atmospheres = new HashSet<Element>();

	// Token: 0x04001CFD RID: 7421
	private int cell;

	// Token: 0x04001CFE RID: 7422
	private PressureVulnerable.PressureState pressureState = PressureVulnerable.PressureState.Normal;

	// Token: 0x020013F1 RID: 5105
	public class StatesInstance : GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.GameInstance
	{
		// Token: 0x06007FA7 RID: 32679 RVA: 0x002DD3B7 File Offset: 0x002DB5B7
		public StatesInstance(PressureVulnerable master)
			: base(master)
		{
			if (Db.Get().Amounts.Maturity.Lookup(base.gameObject) != null)
			{
				this.hasMaturity = true;
			}
		}

		// Token: 0x04006211 RID: 25105
		public bool hasMaturity;
	}

	// Token: 0x020013F2 RID: 5106
	public class States : GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable>
	{
		// Token: 0x06007FA8 RID: 32680 RVA: 0x002DD3E4 File Offset: 0x002DB5E4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.normal;
			this.lethalLow.ParamTransition<float>(this.pressure, this.warningLow, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureLethal_Low).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse).Enter(delegate(PressureVulnerable.StatesInstance smi)
			{
				smi.master.pressureState = PressureVulnerable.PressureState.LethalLow;
			})
				.TriggerOnEnter(GameHashes.LowPressureFatal, null);
			this.lethalHigh.ParamTransition<float>(this.pressure, this.warningHigh, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureLethal_High).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse).Enter(delegate(PressureVulnerable.StatesInstance smi)
			{
				smi.master.pressureState = PressureVulnerable.PressureState.LethalHigh;
			})
				.TriggerOnEnter(GameHashes.HighPressureFatal, null);
			this.warningLow.ParamTransition<float>(this.pressure, this.lethalLow, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureLethal_Low).ParamTransition<float>(this.pressure, this.normal, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureWarning_Low).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse)
				.Enter(delegate(PressureVulnerable.StatesInstance smi)
				{
					smi.master.pressureState = PressureVulnerable.PressureState.WarningLow;
				})
				.TriggerOnEnter(GameHashes.LowPressureWarning, null);
			this.unsafeElement.ParamTransition<bool>(this.safe_element, this.normal, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsTrue).TriggerOnExit(GameHashes.CorrectAtmosphere, null).TriggerOnEnter(GameHashes.WrongAtmosphere, null);
			this.warningHigh.ParamTransition<float>(this.pressure, this.lethalHigh, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureLethal_High).ParamTransition<float>(this.pressure, this.normal, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureWarning_High).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse)
				.Enter(delegate(PressureVulnerable.StatesInstance smi)
				{
					smi.master.pressureState = PressureVulnerable.PressureState.WarningHigh;
				})
				.TriggerOnEnter(GameHashes.HighPressureWarning, null);
			this.normal.ParamTransition<float>(this.pressure, this.warningHigh, (PressureVulnerable.StatesInstance smi, float p) => p > smi.master.pressureWarning_High).ParamTransition<float>(this.pressure, this.warningLow, (PressureVulnerable.StatesInstance smi, float p) => p < smi.master.pressureWarning_Low).ParamTransition<bool>(this.safe_element, this.unsafeElement, GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.IsFalse)
				.Enter(delegate(PressureVulnerable.StatesInstance smi)
				{
					smi.master.pressureState = PressureVulnerable.PressureState.Normal;
				})
				.TriggerOnEnter(GameHashes.OptimalPressureAchieved, null);
		}

		// Token: 0x04006212 RID: 25106
		public StateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.FloatParameter pressure;

		// Token: 0x04006213 RID: 25107
		public StateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.BoolParameter safe_element;

		// Token: 0x04006214 RID: 25108
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State unsafeElement;

		// Token: 0x04006215 RID: 25109
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State lethalLow;

		// Token: 0x04006216 RID: 25110
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State lethalHigh;

		// Token: 0x04006217 RID: 25111
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State warningLow;

		// Token: 0x04006218 RID: 25112
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State warningHigh;

		// Token: 0x04006219 RID: 25113
		public GameStateMachine<PressureVulnerable.States, PressureVulnerable.StatesInstance, PressureVulnerable, object>.State normal;
	}

	// Token: 0x020013F3 RID: 5107
	public enum PressureState
	{
		// Token: 0x0400621B RID: 25115
		LethalLow,
		// Token: 0x0400621C RID: 25116
		WarningLow,
		// Token: 0x0400621D RID: 25117
		Normal,
		// Token: 0x0400621E RID: 25118
		WarningHigh,
		// Token: 0x0400621F RID: 25119
		LethalHigh
	}
}
