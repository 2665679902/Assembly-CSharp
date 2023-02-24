using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200084D RID: 2125
public class TemperatureMonitor : GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance>
{
	// Token: 0x06003D28 RID: 15656 RVA: 0x00155DBC File Offset: 0x00153FBC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.homeostatic;
		this.root.Enter(delegate(TemperatureMonitor.Instance smi)
		{
			smi.averageTemperature = smi.primaryElement.Temperature;
			SicknessTrigger component = smi.master.GetComponent<SicknessTrigger>();
			if (component != null)
			{
				component.AddTrigger(GameHashes.TooHotSickness, new string[] { "HeatSickness" }, (GameObject s, GameObject t) => DUPLICANTS.DISEASES.INFECTIONSOURCES.INTERNAL_TEMPERATURE);
				component.AddTrigger(GameHashes.TooColdSickness, new string[] { "ColdSickness" }, (GameObject s, GameObject t) => DUPLICANTS.DISEASES.INFECTIONSOURCES.INTERNAL_TEMPERATURE);
			}
		}).Update("UpdateTemperature", delegate(TemperatureMonitor.Instance smi, float dt)
		{
			smi.UpdateTemperature(dt);
		}, UpdateRate.SIM_200ms, false);
		this.homeostatic.Transition(this.hyperthermic_pre, (TemperatureMonitor.Instance smi) => smi.IsHyperthermic(), UpdateRate.SIM_200ms).Transition(this.hypothermic_pre, (TemperatureMonitor.Instance smi) => smi.IsHypothermic(), UpdateRate.SIM_200ms).TriggerOnEnter(GameHashes.OptimalTemperatureAchieved, null);
		this.hyperthermic_pre.Enter(delegate(TemperatureMonitor.Instance smi)
		{
			smi.master.Trigger(-1174019026, smi.master.gameObject);
			smi.GoTo(this.hyperthermic);
		});
		this.hypothermic_pre.Enter(delegate(TemperatureMonitor.Instance smi)
		{
			smi.master.Trigger(54654253, smi.master.gameObject);
			smi.GoTo(this.hypothermic);
		});
		this.hyperthermic.Transition(this.homeostatic, (TemperatureMonitor.Instance smi) => !smi.IsHyperthermic(), UpdateRate.SIM_200ms).ToggleUrge(Db.Get().Urges.CoolDown);
		this.hypothermic.Transition(this.homeostatic, (TemperatureMonitor.Instance smi) => !smi.IsHypothermic(), UpdateRate.SIM_200ms).ToggleUrge(Db.Get().Urges.WarmUp);
		this.deathcold.Enter("KillCold", delegate(TemperatureMonitor.Instance smi)
		{
			smi.KillCold();
		}).TriggerOnEnter(GameHashes.TooColdFatal, null);
		this.deathhot.Enter("KillHot", delegate(TemperatureMonitor.Instance smi)
		{
			smi.KillHot();
		}).TriggerOnEnter(GameHashes.TooHotFatal, null);
	}

	// Token: 0x0400280A RID: 10250
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State homeostatic;

	// Token: 0x0400280B RID: 10251
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hyperthermic;

	// Token: 0x0400280C RID: 10252
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hypothermic;

	// Token: 0x0400280D RID: 10253
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hyperthermic_pre;

	// Token: 0x0400280E RID: 10254
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State hypothermic_pre;

	// Token: 0x0400280F RID: 10255
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State deathcold;

	// Token: 0x04002810 RID: 10256
	public GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.State deathhot;

	// Token: 0x04002811 RID: 10257
	private const float TEMPERATURE_AVERAGING_RANGE = 4f;

	// Token: 0x04002812 RID: 10258
	public StateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.IntParameter warmUpCell;

	// Token: 0x04002813 RID: 10259
	public StateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.IntParameter coolDownCell;

	// Token: 0x020015F6 RID: 5622
	public new class Instance : GameStateMachine<TemperatureMonitor, TemperatureMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06008610 RID: 34320 RVA: 0x002EDD38 File Offset: 0x002EBF38
		public Instance(IStateMachineTarget master)
			: base(master)
		{
			this.primaryElement = base.GetComponent<PrimaryElement>();
			this.temperature = Db.Get().Amounts.Temperature.Lookup(base.gameObject);
			this.warmUpQuery = new SafetyQuery(Game.Instance.safetyConditions.WarmUpChecker, base.GetComponent<KMonoBehaviour>(), int.MaxValue);
			this.coolDownQuery = new SafetyQuery(Game.Instance.safetyConditions.CoolDownChecker, base.GetComponent<KMonoBehaviour>(), int.MaxValue);
			this.navigator = base.GetComponent<Navigator>();
		}

		// Token: 0x06008611 RID: 34321 RVA: 0x002EDDFC File Offset: 0x002EBFFC
		public void UpdateTemperature(float dt)
		{
			base.smi.averageTemperature *= 1f - dt / 4f;
			base.smi.averageTemperature += base.smi.primaryElement.Temperature * (dt / 4f);
			base.smi.temperature.SetValue(base.smi.averageTemperature);
		}

		// Token: 0x06008612 RID: 34322 RVA: 0x002EDE6E File Offset: 0x002EC06E
		public bool IsHyperthermic()
		{
			return this.temperature.value > this.HyperthermiaThreshold;
		}

		// Token: 0x06008613 RID: 34323 RVA: 0x002EDE83 File Offset: 0x002EC083
		public bool IsHypothermic()
		{
			return this.temperature.value < this.HypothermiaThreshold;
		}

		// Token: 0x06008614 RID: 34324 RVA: 0x002EDE98 File Offset: 0x002EC098
		public bool IsFatalHypothermic()
		{
			return this.temperature.value < this.FatalHypothermia;
		}

		// Token: 0x06008615 RID: 34325 RVA: 0x002EDEAD File Offset: 0x002EC0AD
		public bool IsFatalHyperthermic()
		{
			return this.temperature.value > this.FatalHyperthermia;
		}

		// Token: 0x06008616 RID: 34326 RVA: 0x002EDEC2 File Offset: 0x002EC0C2
		public void KillHot()
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Overheating);
		}

		// Token: 0x06008617 RID: 34327 RVA: 0x002EDEE3 File Offset: 0x002EC0E3
		public void KillCold()
		{
			base.gameObject.GetSMI<DeathMonitor.Instance>().Kill(Db.Get().Deaths.Frozen);
		}

		// Token: 0x06008618 RID: 34328 RVA: 0x002EDF04 File Offset: 0x002EC104
		public float ExtremeTemperatureDelta()
		{
			if (this.temperature.value > this.HyperthermiaThreshold)
			{
				return this.temperature.value - this.HyperthermiaThreshold;
			}
			if (this.temperature.value < this.HypothermiaThreshold)
			{
				return this.temperature.value - this.HypothermiaThreshold;
			}
			return 0f;
		}

		// Token: 0x06008619 RID: 34329 RVA: 0x002EDF62 File Offset: 0x002EC162
		public float IdealTemperatureDelta()
		{
			return this.temperature.value - 310.15f;
		}

		// Token: 0x0600861A RID: 34330 RVA: 0x002EDF75 File Offset: 0x002EC175
		public int GetWarmUpCell()
		{
			return base.sm.warmUpCell.Get(base.smi);
		}

		// Token: 0x0600861B RID: 34331 RVA: 0x002EDF8D File Offset: 0x002EC18D
		public int GetCoolDownCell()
		{
			return base.sm.coolDownCell.Get(base.smi);
		}

		// Token: 0x0600861C RID: 34332 RVA: 0x002EDFA8 File Offset: 0x002EC1A8
		public void UpdateWarmUpCell()
		{
			this.warmUpQuery.Reset();
			this.navigator.RunQuery(this.warmUpQuery);
			base.sm.warmUpCell.Set(this.warmUpQuery.GetResultCell(), base.smi, false);
		}

		// Token: 0x0600861D RID: 34333 RVA: 0x002EDFF4 File Offset: 0x002EC1F4
		public void UpdateCoolDownCell()
		{
			this.coolDownQuery.Reset();
			this.navigator.RunQuery(this.coolDownQuery);
			base.sm.coolDownCell.Set(this.coolDownQuery.GetResultCell(), base.smi, false);
		}

		// Token: 0x04006871 RID: 26737
		public AmountInstance temperature;

		// Token: 0x04006872 RID: 26738
		public PrimaryElement primaryElement;

		// Token: 0x04006873 RID: 26739
		private Navigator navigator;

		// Token: 0x04006874 RID: 26740
		private SafetyQuery warmUpQuery;

		// Token: 0x04006875 RID: 26741
		private SafetyQuery coolDownQuery;

		// Token: 0x04006876 RID: 26742
		public float averageTemperature;

		// Token: 0x04006877 RID: 26743
		public float HypothermiaThreshold = 307.15f;

		// Token: 0x04006878 RID: 26744
		public float HyperthermiaThreshold = 313.15f;

		// Token: 0x04006879 RID: 26745
		public float FatalHypothermia = 305.15f;

		// Token: 0x0400687A RID: 26746
		public float FatalHyperthermia = 315.15f;
	}
}
