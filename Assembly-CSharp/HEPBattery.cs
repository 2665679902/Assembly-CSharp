using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007A3 RID: 1955
public class HEPBattery : GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>
{
	// Token: 0x0600371F RID: 14111 RVA: 0x00132FB8 File Offset: 0x001311B8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.inoperational;
		this.inoperational.PlayAnim("off").TagTransition(GameTags.Operational, this.operational, false).Update(delegate(HEPBattery.Instance smi, float dt)
		{
			smi.DoConsumeParticlesWhileDisabled(dt);
			smi.UpdateDecayStatusItem(false);
		}, UpdateRate.SIM_200ms, false);
		this.operational.Enter("SetActive(true)", delegate(HEPBattery.Instance smi)
		{
			smi.operational.SetActive(true, false);
		}).Exit("SetActive(false)", delegate(HEPBattery.Instance smi)
		{
			smi.operational.SetActive(false, false);
		}).PlayAnim("on", KAnim.PlayMode.Loop)
			.TagTransition(GameTags.Operational, this.inoperational, true)
			.Update(new Action<HEPBattery.Instance, float>(this.LauncherUpdate), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x06003720 RID: 14112 RVA: 0x001330A0 File Offset: 0x001312A0
	public void LauncherUpdate(HEPBattery.Instance smi, float dt)
	{
		smi.UpdateDecayStatusItem(true);
		smi.UpdateMeter(null);
		smi.operational.SetActive(smi.particleStorage.Particles > 0f, false);
		smi.launcherTimer += dt;
		if (smi.launcherTimer < smi.def.minLaunchInterval || !smi.AllowSpawnParticles)
		{
			return;
		}
		if (smi.particleStorage.Particles >= smi.particleThreshold)
		{
			smi.launcherTimer = 0f;
			this.Fire(smi);
		}
	}

	// Token: 0x06003721 RID: 14113 RVA: 0x00133128 File Offset: 0x00131328
	public void Fire(HEPBattery.Instance smi)
	{
		int highEnergyParticleOutputCell = smi.GetComponent<Building>().GetHighEnergyParticleOutputCell();
		GameObject gameObject = GameUtil.KInstantiate(Assets.GetPrefab("HighEnergyParticle"), Grid.CellToPosCCC(highEnergyParticleOutputCell, Grid.SceneLayer.FXFront2), Grid.SceneLayer.FXFront2, null, 0);
		gameObject.SetActive(true);
		if (gameObject != null)
		{
			HighEnergyParticle component = gameObject.GetComponent<HighEnergyParticle>();
			component.payload = smi.particleStorage.ConsumeAndGet(smi.particleThreshold);
			component.SetDirection(smi.def.direction);
		}
	}

	// Token: 0x040024FB RID: 9467
	public static readonly HashedString FIRE_PORT_ID = "HEPBatteryFire";

	// Token: 0x040024FC RID: 9468
	public GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>.State inoperational;

	// Token: 0x040024FD RID: 9469
	public GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>.State operational;

	// Token: 0x02001504 RID: 5380
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006552 RID: 25938
		public float particleDecayRate;

		// Token: 0x04006553 RID: 25939
		public float minLaunchInterval;

		// Token: 0x04006554 RID: 25940
		public float minSlider;

		// Token: 0x04006555 RID: 25941
		public float maxSlider;

		// Token: 0x04006556 RID: 25942
		public EightDirection direction;
	}

	// Token: 0x02001505 RID: 5381
	public new class Instance : GameStateMachine<HEPBattery, HEPBattery.Instance, IStateMachineTarget, HEPBattery.Def>.GameInstance, ISingleSliderControl, ISliderControl
	{
		// Token: 0x0600825C RID: 33372 RVA: 0x002E4884 File Offset: 0x002E2A84
		public Instance(IStateMachineTarget master, HEPBattery.Def def)
			: base(master, def)
		{
			base.Subscribe(-801688580, new Action<object>(this.OnLogicValueChanged));
			this.meterController = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
			this.UpdateMeter(null);
		}

		// Token: 0x0600825D RID: 33373 RVA: 0x002E48F7 File Offset: 0x002E2AF7
		public void DoConsumeParticlesWhileDisabled(float dt)
		{
			if (this.m_skipFirstUpdate)
			{
				this.m_skipFirstUpdate = false;
				return;
			}
			this.particleStorage.ConsumeAndGet(dt * base.def.particleDecayRate);
			this.UpdateMeter(null);
		}

		// Token: 0x0600825E RID: 33374 RVA: 0x002E4929 File Offset: 0x002E2B29
		public void UpdateMeter(object data = null)
		{
			this.meterController.SetPositionPercent(this.particleStorage.Particles / this.particleStorage.Capacity());
		}

		// Token: 0x0600825F RID: 33375 RVA: 0x002E4950 File Offset: 0x002E2B50
		public void UpdateDecayStatusItem(bool hasPower)
		{
			if (!hasPower)
			{
				if (this.particleStorage.Particles > 0f)
				{
					if (this.statusHandle == Guid.Empty)
					{
						this.statusHandle = base.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.LosingRadbolts, null);
						return;
					}
				}
				else if (this.statusHandle != Guid.Empty)
				{
					base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
					this.statusHandle = Guid.Empty;
					return;
				}
			}
			else if (this.statusHandle != Guid.Empty)
			{
				base.GetComponent<KSelectable>().RemoveStatusItem(this.statusHandle, false);
				this.statusHandle = Guid.Empty;
			}
		}

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x06008260 RID: 33376 RVA: 0x002E4A0A File Offset: 0x002E2C0A
		public bool AllowSpawnParticles
		{
			get
			{
				return this.hasLogicWire && this.isLogicActive;
			}
		}

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x06008261 RID: 33377 RVA: 0x002E4A1C File Offset: 0x002E2C1C
		public bool HasLogicWire
		{
			get
			{
				return this.hasLogicWire;
			}
		}

		// Token: 0x170008BD RID: 2237
		// (get) Token: 0x06008262 RID: 33378 RVA: 0x002E4A24 File Offset: 0x002E2C24
		public bool IsLogicActive
		{
			get
			{
				return this.isLogicActive;
			}
		}

		// Token: 0x06008263 RID: 33379 RVA: 0x002E4A2C File Offset: 0x002E2C2C
		private LogicCircuitNetwork GetNetwork()
		{
			int portCell = base.GetComponent<LogicPorts>().GetPortCell(HEPBattery.FIRE_PORT_ID);
			return Game.Instance.logicCircuitManager.GetNetworkForCell(portCell);
		}

		// Token: 0x06008264 RID: 33380 RVA: 0x002E4A5C File Offset: 0x002E2C5C
		private void OnLogicValueChanged(object data)
		{
			LogicValueChanged logicValueChanged = (LogicValueChanged)data;
			if (logicValueChanged.portID == HEPBattery.FIRE_PORT_ID)
			{
				this.isLogicActive = logicValueChanged.newValue > 0;
				this.hasLogicWire = this.GetNetwork() != null;
			}
		}

		// Token: 0x170008BE RID: 2238
		// (get) Token: 0x06008265 RID: 33381 RVA: 0x002E4AA0 File Offset: 0x002E2CA0
		public string SliderTitleKey
		{
			get
			{
				return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TITLE";
			}
		}

		// Token: 0x170008BF RID: 2239
		// (get) Token: 0x06008266 RID: 33382 RVA: 0x002E4AA7 File Offset: 0x002E2CA7
		public string SliderUnits
		{
			get
			{
				return UI.UNITSUFFIXES.HIGHENERGYPARTICLES.PARTRICLES;
			}
		}

		// Token: 0x06008267 RID: 33383 RVA: 0x002E4AB3 File Offset: 0x002E2CB3
		public int SliderDecimalPlaces(int index)
		{
			return 0;
		}

		// Token: 0x06008268 RID: 33384 RVA: 0x002E4AB6 File Offset: 0x002E2CB6
		public float GetSliderMin(int index)
		{
			return base.def.minSlider;
		}

		// Token: 0x06008269 RID: 33385 RVA: 0x002E4AC3 File Offset: 0x002E2CC3
		public float GetSliderMax(int index)
		{
			return base.def.maxSlider;
		}

		// Token: 0x0600826A RID: 33386 RVA: 0x002E4AD0 File Offset: 0x002E2CD0
		public float GetSliderValue(int index)
		{
			return this.particleThreshold;
		}

		// Token: 0x0600826B RID: 33387 RVA: 0x002E4AD8 File Offset: 0x002E2CD8
		public void SetSliderValue(float value, int index)
		{
			this.particleThreshold = value;
		}

		// Token: 0x0600826C RID: 33388 RVA: 0x002E4AE1 File Offset: 0x002E2CE1
		public string GetSliderTooltipKey(int index)
		{
			return "STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP";
		}

		// Token: 0x0600826D RID: 33389 RVA: 0x002E4AE8 File Offset: 0x002E2CE8
		string ISliderControl.GetSliderTooltip()
		{
			return string.Format(Strings.Get("STRINGS.UI.UISIDESCREENS.RADBOLTTHRESHOLDSIDESCREEN.TOOLTIP"), this.particleThreshold);
		}

		// Token: 0x04006557 RID: 25943
		[MyCmpReq]
		public HighEnergyParticleStorage particleStorage;

		// Token: 0x04006558 RID: 25944
		[MyCmpGet]
		public Operational operational;

		// Token: 0x04006559 RID: 25945
		[Serialize]
		public float launcherTimer;

		// Token: 0x0400655A RID: 25946
		[Serialize]
		public float particleThreshold = 50f;

		// Token: 0x0400655B RID: 25947
		public bool ShowWorkingStatus;

		// Token: 0x0400655C RID: 25948
		private bool m_skipFirstUpdate = true;

		// Token: 0x0400655D RID: 25949
		private MeterController meterController;

		// Token: 0x0400655E RID: 25950
		private Guid statusHandle = Guid.Empty;

		// Token: 0x0400655F RID: 25951
		private bool hasLogicWire;

		// Token: 0x04006560 RID: 25952
		private bool isLogicActive;
	}
}
