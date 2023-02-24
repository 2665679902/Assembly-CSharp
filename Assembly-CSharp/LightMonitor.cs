using System;
using Klei.AI;
using STRINGS;

// Token: 0x02000836 RID: 2102
public class LightMonitor : GameStateMachine<LightMonitor, LightMonitor.Instance>
{
	// Token: 0x06003CAD RID: 15533 RVA: 0x001528D0 File Offset: 0x00150AD0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.unburnt;
		this.root.EventTransition(GameHashes.SicknessAdded, this.burnt, (LightMonitor.Instance smi) => smi.gameObject.GetSicknesses().Has(Db.Get().Sicknesses.Sunburn)).Update(new Action<LightMonitor.Instance, float>(LightMonitor.CheckLightLevel), UpdateRate.SIM_1000ms, false);
		this.unburnt.DefaultState(this.unburnt.safe).ParamTransition<float>(this.burnResistance, this.get_burnt, GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.IsLTEZero);
		this.unburnt.safe.DefaultState(this.unburnt.safe.unlit).Update(delegate(LightMonitor.Instance smi, float dt)
		{
			smi.sm.burnResistance.DeltaClamp(dt * 0.25f, 0f, 120f, smi);
		}, UpdateRate.SIM_200ms, false);
		this.unburnt.safe.unlit.ParamTransition<float>(this.lightLevel, this.unburnt.safe.normal_light, GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.IsGTZero);
		this.unburnt.safe.normal_light.ParamTransition<float>(this.lightLevel, this.unburnt.safe.unlit, GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.IsLTEZero).ParamTransition<float>(this.lightLevel, this.unburnt.safe.sunlight, (LightMonitor.Instance smi, float p) => p >= 40000f);
		this.unburnt.safe.sunlight.ParamTransition<float>(this.lightLevel, this.unburnt.safe.normal_light, (LightMonitor.Instance smi, float p) => p < 40000f).ParamTransition<float>(this.lightLevel, this.unburnt.burning, (LightMonitor.Instance smi, float p) => p >= 72000f).ToggleEffect("Sunlight_Pleasant");
		this.unburnt.burning.ParamTransition<float>(this.lightLevel, this.unburnt.safe.sunlight, (LightMonitor.Instance smi, float p) => p < 72000f).Update(delegate(LightMonitor.Instance smi, float dt)
		{
			smi.sm.burnResistance.DeltaClamp(-dt, 0f, 120f, smi);
		}, UpdateRate.SIM_200ms, false).ToggleEffect("Sunlight_Burning");
		this.get_burnt.Enter(delegate(LightMonitor.Instance smi)
		{
			smi.gameObject.GetSicknesses().Infect(new SicknessExposureInfo(Db.Get().Sicknesses.Sunburn.Id, DUPLICANTS.DISEASES.SUNBURNSICKNESS.SUNEXPOSURE));
		}).GoTo(this.burnt);
		this.burnt.EventTransition(GameHashes.SicknessCured, this.unburnt, (LightMonitor.Instance smi) => !smi.gameObject.GetSicknesses().Has(Db.Get().Sicknesses.Sunburn)).Exit(delegate(LightMonitor.Instance smi)
		{
			smi.sm.burnResistance.Set(120f, smi, false);
		});
	}

	// Token: 0x06003CAE RID: 15534 RVA: 0x00152BD0 File Offset: 0x00150DD0
	private static void CheckLightLevel(LightMonitor.Instance smi, float dt)
	{
		KPrefabID component = smi.GetComponent<KPrefabID>();
		if (component != null && component.HasTag(GameTags.Shaded))
		{
			smi.sm.lightLevel.Set(0f, smi, false);
			return;
		}
		int num = Grid.PosToCell(smi.gameObject);
		if (Grid.IsValidCell(num))
		{
			smi.sm.lightLevel.Set((float)Grid.LightIntensity[num], smi, false);
		}
	}

	// Token: 0x0400279E RID: 10142
	public const float BURN_RESIST_RECOVERY_FACTOR = 0.25f;

	// Token: 0x0400279F RID: 10143
	public StateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.FloatParameter lightLevel;

	// Token: 0x040027A0 RID: 10144
	public StateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.FloatParameter burnResistance = new StateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.FloatParameter(120f);

	// Token: 0x040027A1 RID: 10145
	public LightMonitor.UnburntStates unburnt;

	// Token: 0x040027A2 RID: 10146
	public GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State get_burnt;

	// Token: 0x040027A3 RID: 10147
	public GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State burnt;

	// Token: 0x020015C0 RID: 5568
	public class UnburntStates : GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040067D0 RID: 26576
		public LightMonitor.SafeStates safe;

		// Token: 0x040067D1 RID: 26577
		public GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State burning;
	}

	// Token: 0x020015C1 RID: 5569
	public class SafeStates : GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State
	{
		// Token: 0x040067D2 RID: 26578
		public GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State unlit;

		// Token: 0x040067D3 RID: 26579
		public GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State normal_light;

		// Token: 0x040067D4 RID: 26580
		public GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.State sunlight;
	}

	// Token: 0x020015C2 RID: 5570
	public new class Instance : GameStateMachine<LightMonitor, LightMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008538 RID: 34104 RVA: 0x002EC1DA File Offset: 0x002EA3DA
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.effects = base.GetComponent<Effects>();
		}

		// Token: 0x040067D5 RID: 26581
		public Effects effects;
	}
}
