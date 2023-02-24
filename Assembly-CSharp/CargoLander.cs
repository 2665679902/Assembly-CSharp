using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200093B RID: 2363
[SerializationConfig(MemberSerialization.OptIn)]
public class CargoLander : GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>
{
	// Token: 0x0600453F RID: 17727 RVA: 0x001865D8 File Offset: 0x001847D8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.init;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.InitializeOperationalFlag(RocketModule.landedFlag, false).Enter(delegate(CargoLander.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		}).EventHandler(GameHashes.OnStorageChange, delegate(CargoLander.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		});
		this.init.ParamTransition<bool>(this.isLanded, this.grounded, GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.IsTrue).GoTo(this.stored);
		this.stored.TagTransition(GameTags.Stored, this.landing, true).EventHandler(GameHashes.JettisonedLander, delegate(CargoLander.StatesInstance smi)
		{
			smi.OnJettisoned();
		});
		this.landing.PlayAnim("landing", KAnim.PlayMode.Loop).Enter(delegate(CargoLander.StatesInstance smi)
		{
			smi.ShowLandingPreview(true);
		}).Exit(delegate(CargoLander.StatesInstance smi)
		{
			smi.ShowLandingPreview(false);
		})
			.Enter(delegate(CargoLander.StatesInstance smi)
			{
				smi.ResetAnimPosition();
			})
			.Update(delegate(CargoLander.StatesInstance smi, float dt)
			{
				smi.LandingUpdate(dt);
			}, UpdateRate.SIM_EVERY_TICK, false)
			.Transition(this.land, (CargoLander.StatesInstance smi) => smi.flightAnimOffset <= 0f, UpdateRate.SIM_200ms);
		this.land.PlayAnim("grounded_pre").OnAnimQueueComplete(this.grounded);
		this.grounded.DefaultState(this.grounded.loaded).ToggleOperationalFlag(RocketModule.landedFlag).Enter(delegate(CargoLander.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		})
			.Enter(delegate(CargoLander.StatesInstance smi)
			{
				smi.sm.isLanded.Set(true, smi, false);
			});
		this.grounded.loaded.PlayAnim("grounded").ParamTransition<bool>(this.hasCargo, this.grounded.empty, GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.IsFalse).OnSignal(this.emptyCargo, this.grounded.emptying)
			.Enter(delegate(CargoLander.StatesInstance smi)
			{
				smi.DoLand();
			});
		this.grounded.emptying.PlayAnim("deploying").TriggerOnEnter(GameHashes.JettisonCargo, null).OnAnimQueueComplete(this.grounded.empty);
		this.grounded.empty.PlayAnim("deployed").ParamTransition<bool>(this.hasCargo, this.grounded.loaded, GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.IsTrue);
	}

	// Token: 0x04002E25 RID: 11813
	public StateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.BoolParameter hasCargo;

	// Token: 0x04002E26 RID: 11814
	public StateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.Signal emptyCargo;

	// Token: 0x04002E27 RID: 11815
	public GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State init;

	// Token: 0x04002E28 RID: 11816
	public GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State stored;

	// Token: 0x04002E29 RID: 11817
	public GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State landing;

	// Token: 0x04002E2A RID: 11818
	public GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State land;

	// Token: 0x04002E2B RID: 11819
	public CargoLander.CrashedStates grounded;

	// Token: 0x04002E2C RID: 11820
	public StateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.BoolParameter isLanded = new StateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.BoolParameter(false);

	// Token: 0x02001727 RID: 5927
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006C29 RID: 27689
		public Tag previewTag;

		// Token: 0x04006C2A RID: 27690
		public bool deployOnLanding = true;
	}

	// Token: 0x02001728 RID: 5928
	public class CrashedStates : GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State
	{
		// Token: 0x04006C2B RID: 27691
		public GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State loaded;

		// Token: 0x04006C2C RID: 27692
		public GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State emptying;

		// Token: 0x04006C2D RID: 27693
		public GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.State empty;
	}

	// Token: 0x02001729 RID: 5929
	public class StatesInstance : GameStateMachine<CargoLander, CargoLander.StatesInstance, IStateMachineTarget, CargoLander.Def>.GameInstance
	{
		// Token: 0x060089CC RID: 35276 RVA: 0x002F9C08 File Offset: 0x002F7E08
		public StatesInstance(IStateMachineTarget master, CargoLander.Def def)
			: base(master, def)
		{
		}

		// Token: 0x060089CD RID: 35277 RVA: 0x002F9C54 File Offset: 0x002F7E54
		public void ResetAnimPosition()
		{
			base.GetComponent<KBatchedAnimController>().Offset = Vector3.up * this.flightAnimOffset;
		}

		// Token: 0x060089CE RID: 35278 RVA: 0x002F9C71 File Offset: 0x002F7E71
		public void OnJettisoned()
		{
			this.flightAnimOffset = 50f;
		}

		// Token: 0x060089CF RID: 35279 RVA: 0x002F9C80 File Offset: 0x002F7E80
		public void ShowLandingPreview(bool show)
		{
			if (show)
			{
				this.landingPreview = Util.KInstantiate(Assets.GetPrefab(base.def.previewTag), base.transform.GetPosition(), Quaternion.identity, base.gameObject, null, true, 0);
				this.landingPreview.SetActive(true);
				return;
			}
			this.landingPreview.DeleteObject();
			this.landingPreview = null;
		}

		// Token: 0x060089D0 RID: 35280 RVA: 0x002F9CE4 File Offset: 0x002F7EE4
		public void LandingUpdate(float dt)
		{
			this.flightAnimOffset = Mathf.Max(this.flightAnimOffset - dt * this.topSpeed, 0f);
			this.ResetAnimPosition();
			int num = Grid.PosToCell(base.gameObject.transform.GetPosition() + new Vector3(0f, this.flightAnimOffset, 0f));
			if (Grid.IsValidCell(num))
			{
				SimMessages.EmitMass(num, ElementLoader.GetElementIndex(this.exhaustElement), dt * this.exhaustEmitRate, this.exhaustTemperature, 0, 0, -1);
			}
		}

		// Token: 0x060089D1 RID: 35281 RVA: 0x002F9D70 File Offset: 0x002F7F70
		public void DoLand()
		{
			base.smi.master.GetComponent<KBatchedAnimController>().Offset = Vector3.zero;
			OccupyArea component = base.smi.GetComponent<OccupyArea>();
			if (component != null)
			{
				component.ApplyToCells = true;
			}
			if (base.def.deployOnLanding && this.CheckIfLoaded())
			{
				base.sm.emptyCargo.Trigger(this);
			}
			base.smi.master.gameObject.Trigger(1591811118, this);
		}

		// Token: 0x060089D2 RID: 35282 RVA: 0x002F9DF4 File Offset: 0x002F7FF4
		public bool CheckIfLoaded()
		{
			bool flag = false;
			MinionStorage component = base.GetComponent<MinionStorage>();
			if (component != null)
			{
				flag |= component.GetStoredMinionInfo().Count > 0;
			}
			Storage component2 = base.GetComponent<Storage>();
			if (component2 != null && !component2.IsEmpty())
			{
				flag = true;
			}
			if (flag != base.sm.hasCargo.Get(this))
			{
				base.sm.hasCargo.Set(flag, this, false);
			}
			return flag;
		}

		// Token: 0x04006C2E RID: 27694
		[Serialize]
		public float flightAnimOffset = 50f;

		// Token: 0x04006C2F RID: 27695
		public float exhaustEmitRate = 2f;

		// Token: 0x04006C30 RID: 27696
		public float exhaustTemperature = 1000f;

		// Token: 0x04006C31 RID: 27697
		public SimHashes exhaustElement = SimHashes.CarbonDioxide;

		// Token: 0x04006C32 RID: 27698
		public float topSpeed = 5f;

		// Token: 0x04006C33 RID: 27699
		private GameObject landingPreview;
	}
}
