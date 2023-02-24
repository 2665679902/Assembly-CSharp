using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000664 RID: 1636
[SerializationConfig(MemberSerialization.OptIn)]
public class TravellingCargoLander : GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>
{
	// Token: 0x06002C05 RID: 11269 RVA: 0x000E6E14 File Offset: 0x000E5014
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.init;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.root.InitializeOperationalFlag(RocketModule.landedFlag, false).Enter(delegate(TravellingCargoLander.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		}).EventHandler(GameHashes.OnStorageChange, delegate(TravellingCargoLander.StatesInstance smi)
		{
			smi.CheckIfLoaded();
		});
		this.init.ParamTransition<bool>(this.isLanding, this.landing.landing, GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.IsTrue).ParamTransition<bool>(this.isLanded, this.grounded, GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.IsTrue).GoTo(this.travel);
		this.travel.DefaultState(this.travel.travelling).Enter(delegate(TravellingCargoLander.StatesInstance smi)
		{
			smi.MoveToSpace();
		}).PlayAnim("idle")
			.ToggleTag(GameTags.EntityInSpace)
			.ToggleMainStatusItem(Db.Get().BuildingStatusItems.InFlight, (TravellingCargoLander.StatesInstance smi) => smi.GetComponent<ClusterTraveler>());
		this.travel.travelling.EventTransition(GameHashes.ClusterDestinationReached, this.travel.transferWorlds, null);
		this.travel.transferWorlds.Enter(delegate(TravellingCargoLander.StatesInstance smi)
		{
			smi.StartLand();
		}).GoTo(this.landing.landing);
		this.landing.Enter(delegate(TravellingCargoLander.StatesInstance smi)
		{
			this.isLanding.Set(true, smi, false);
		}).Exit(delegate(TravellingCargoLander.StatesInstance smi)
		{
			this.isLanding.Set(false, smi, false);
		});
		this.landing.landing.PlayAnim("landing", KAnim.PlayMode.Loop).Enter(delegate(TravellingCargoLander.StatesInstance smi)
		{
			smi.ResetAnimPosition();
		}).Update(delegate(TravellingCargoLander.StatesInstance smi, float dt)
		{
			smi.LandingUpdate(dt);
		}, UpdateRate.SIM_EVERY_TICK, false)
			.Transition(this.landing.impact, (TravellingCargoLander.StatesInstance smi) => smi.flightAnimOffset <= 0f, UpdateRate.SIM_200ms)
			.Enter(delegate(TravellingCargoLander.StatesInstance smi)
			{
				smi.MoveToWorld();
			});
		this.landing.impact.PlayAnim("grounded_pre").OnAnimQueueComplete(this.grounded);
		this.grounded.DefaultState(this.grounded.loaded).ToggleTag(GameTags.ClusterEntityGrounded).ToggleOperationalFlag(RocketModule.landedFlag)
			.Enter(delegate(TravellingCargoLander.StatesInstance smi)
			{
				smi.CheckIfLoaded();
			})
			.Enter(delegate(TravellingCargoLander.StatesInstance smi)
			{
				this.isLanded.Set(true, smi, false);
			});
		this.grounded.loaded.PlayAnim("grounded").ParamTransition<bool>(this.hasCargo, this.grounded.empty, GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.IsFalse).OnSignal(this.emptyCargo, this.grounded.emptying)
			.Enter(delegate(TravellingCargoLander.StatesInstance smi)
			{
				smi.DoLand();
			});
		this.grounded.emptying.PlayAnim("deploying").TriggerOnEnter(GameHashes.JettisonCargo, null).OnAnimQueueComplete(this.grounded.empty);
		this.grounded.empty.PlayAnim("deployed").ParamTransition<bool>(this.hasCargo, this.grounded.loaded, GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.IsTrue);
	}

	// Token: 0x04001A0A RID: 6666
	public StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.IntParameter destinationWorld = new StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.IntParameter(-1);

	// Token: 0x04001A0B RID: 6667
	public StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.BoolParameter isLanding = new StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.BoolParameter(false);

	// Token: 0x04001A0C RID: 6668
	public StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.BoolParameter isLanded = new StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.BoolParameter(false);

	// Token: 0x04001A0D RID: 6669
	public StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.BoolParameter hasCargo = new StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.BoolParameter(false);

	// Token: 0x04001A0E RID: 6670
	public StateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.Signal emptyCargo;

	// Token: 0x04001A0F RID: 6671
	public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State init;

	// Token: 0x04001A10 RID: 6672
	public TravellingCargoLander.TravelStates travel;

	// Token: 0x04001A11 RID: 6673
	public TravellingCargoLander.LandingStates landing;

	// Token: 0x04001A12 RID: 6674
	public TravellingCargoLander.GroundedStates grounded;

	// Token: 0x02001320 RID: 4896
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005F94 RID: 24468
		public int landerWidth = 1;

		// Token: 0x04005F95 RID: 24469
		public float landingSpeed = 5f;

		// Token: 0x04005F96 RID: 24470
		public bool deployOnLanding;
	}

	// Token: 0x02001321 RID: 4897
	public class TravelStates : GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State
	{
		// Token: 0x04005F97 RID: 24471
		public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State travelling;

		// Token: 0x04005F98 RID: 24472
		public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State transferWorlds;
	}

	// Token: 0x02001322 RID: 4898
	public class LandingStates : GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State
	{
		// Token: 0x04005F99 RID: 24473
		public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State landing;

		// Token: 0x04005F9A RID: 24474
		public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State impact;
	}

	// Token: 0x02001323 RID: 4899
	public class GroundedStates : GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State
	{
		// Token: 0x04005F9B RID: 24475
		public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State loaded;

		// Token: 0x04005F9C RID: 24476
		public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State emptying;

		// Token: 0x04005F9D RID: 24477
		public GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.State empty;
	}

	// Token: 0x02001324 RID: 4900
	public class StatesInstance : GameStateMachine<TravellingCargoLander, TravellingCargoLander.StatesInstance, IStateMachineTarget, TravellingCargoLander.Def>.GameInstance
	{
		// Token: 0x06007CBB RID: 31931 RVA: 0x002D135D File Offset: 0x002CF55D
		public StatesInstance(IStateMachineTarget master, TravellingCargoLander.Def def)
			: base(master, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
		}

		// Token: 0x06007CBC RID: 31932 RVA: 0x002D1380 File Offset: 0x002CF580
		public void Travel(AxialI source, AxialI destination)
		{
			base.GetComponent<BallisticClusterGridEntity>().Configure(source, destination);
			int asteroidWorldIdAtLocation = ClusterUtil.GetAsteroidWorldIdAtLocation(destination);
			base.sm.destinationWorld.Set(asteroidWorldIdAtLocation, this, false);
			this.GoTo(base.sm.travel);
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x002D13C8 File Offset: 0x002CF5C8
		public void StartLand()
		{
			WorldContainer world = ClusterManager.Instance.GetWorld(base.sm.destinationWorld.Get(this));
			Vector3 vector = Grid.CellToPosCBC(ClusterManager.Instance.GetRandomSurfaceCell(world.id, base.def.landerWidth, true), this.animController.sceneLayer);
			base.transform.SetPosition(vector);
		}

		// Token: 0x06007CBE RID: 31934 RVA: 0x002D142C File Offset: 0x002CF62C
		public bool UpdateLanding(float dt)
		{
			if (base.gameObject.GetMyWorld() != null)
			{
				Vector3 position = base.transform.GetPosition();
				position.y -= 0.5f;
				int num = Grid.PosToCell(position);
				if (Grid.IsWorldValidCell(num) && Grid.IsSolidCell(num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06007CBF RID: 31935 RVA: 0x002D1484 File Offset: 0x002CF684
		public void MoveToSpace()
		{
			Pickupable component = base.GetComponent<Pickupable>();
			if (component != null)
			{
				component.deleteOffGrid = false;
			}
			base.gameObject.transform.SetPosition(new Vector3(-1f, -1f, Grid.GetLayerZ(this.animController.sceneLayer)));
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x002D14D8 File Offset: 0x002CF6D8
		public void MoveToWorld()
		{
			Pickupable component = base.GetComponent<Pickupable>();
			if (component != null)
			{
				component.deleteOffGrid = true;
			}
		}

		// Token: 0x06007CC1 RID: 31937 RVA: 0x002D14FC File Offset: 0x002CF6FC
		public void ResetAnimPosition()
		{
			this.animController.Offset = Vector3.up * this.flightAnimOffset;
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x002D1519 File Offset: 0x002CF719
		public void LandingUpdate(float dt)
		{
			this.flightAnimOffset = Mathf.Max(this.flightAnimOffset - dt * base.def.landingSpeed, 0f);
			this.ResetAnimPosition();
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x002D1548 File Offset: 0x002CF748
		public void DoLand()
		{
			this.animController.Offset = Vector3.zero;
			OccupyArea component = base.smi.GetComponent<OccupyArea>();
			if (component != null)
			{
				component.ApplyToCells = true;
			}
			if (base.def.deployOnLanding && this.CheckIfLoaded())
			{
				base.sm.emptyCargo.Trigger(this);
			}
		}

		// Token: 0x06007CC4 RID: 31940 RVA: 0x002D15A8 File Offset: 0x002CF7A8
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

		// Token: 0x04005F9E RID: 24478
		[Serialize]
		public float flightAnimOffset = 50f;

		// Token: 0x04005F9F RID: 24479
		public KBatchedAnimController animController;
	}
}
