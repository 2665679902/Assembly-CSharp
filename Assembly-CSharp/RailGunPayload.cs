using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200062C RID: 1580
[SerializationConfig(MemberSerialization.OptIn)]
public class RailGunPayload : GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>
{
	// Token: 0x06002991 RID: 10641 RVA: 0x000DB578 File Offset: 0x000D9778
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.grounded.idle;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.grounded.DefaultState(this.grounded.idle).Enter(delegate(RailGunPayload.StatesInstance smi)
		{
			this.onSurface.Set(true, smi, false);
		}).ToggleMainStatusItem(Db.Get().BuildingStatusItems.RailgunpayloadNeedsEmptying, null)
			.ToggleTag(GameTags.RailGunPayloadEmptyable)
			.ToggleTag(GameTags.ClusterEntityGrounded)
			.EventHandler(GameHashes.DroppedAll, delegate(RailGunPayload.StatesInstance smi)
			{
				smi.OnDroppedAll();
			})
			.OnSignal(this.launch, this.takeoff);
		this.grounded.idle.PlayAnim("idle");
		this.grounded.crater.Enter(delegate(RailGunPayload.StatesInstance smi)
		{
			smi.animController.randomiseLoopedOffset = true;
		}).Exit(delegate(RailGunPayload.StatesInstance smi)
		{
			smi.animController.randomiseLoopedOffset = false;
		}).PlayAnim("landed", KAnim.PlayMode.Loop)
			.EventTransition(GameHashes.OnStore, this.grounded.idle, null);
		this.takeoff.DefaultState(this.takeoff.launch).Enter(delegate(RailGunPayload.StatesInstance smi)
		{
			this.onSurface.Set(false, smi, false);
		}).PlayAnim("launching")
			.OnSignal(this.beginTravelling, this.travel);
		this.takeoff.launch.Enter(delegate(RailGunPayload.StatesInstance smi)
		{
			smi.StartTakeoff();
		}).GoTo(this.takeoff.airborne);
		this.takeoff.airborne.Update("Launch", delegate(RailGunPayload.StatesInstance smi, float dt)
		{
			smi.UpdateLaunch(dt);
		}, UpdateRate.SIM_EVERY_TICK, false);
		this.travel.DefaultState(this.travel.travelling).Enter(delegate(RailGunPayload.StatesInstance smi)
		{
			this.onSurface.Set(false, smi, false);
		}).Enter(delegate(RailGunPayload.StatesInstance smi)
		{
			smi.MoveToSpace();
		})
			.PlayAnim("idle")
			.ToggleTag(GameTags.EntityInSpace)
			.ToggleMainStatusItem(Db.Get().BuildingStatusItems.InFlight, (RailGunPayload.StatesInstance smi) => smi.GetComponent<ClusterTraveler>());
		this.travel.travelling.EventTransition(GameHashes.ClusterDestinationReached, this.travel.transferWorlds, null);
		this.travel.transferWorlds.Enter(delegate(RailGunPayload.StatesInstance smi)
		{
			smi.StartLand();
		}).GoTo(this.landing.landing);
		this.landing.DefaultState(this.landing.landing).ParamTransition<bool>(this.onSurface, this.grounded.crater, GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.IsTrue).ParamTransition<int>(this.destinationWorld, this.takeoff, (RailGunPayload.StatesInstance smi, int p) => p != -1)
			.Enter(delegate(RailGunPayload.StatesInstance smi)
			{
				smi.MoveToWorld();
			});
		this.landing.landing.PlayAnim("falling", KAnim.PlayMode.Loop).UpdateTransition(this.landing.impact, (RailGunPayload.StatesInstance smi, float dt) => smi.UpdateLanding(dt), UpdateRate.SIM_200ms, false).ToggleGravity(this.landing.impact);
		this.landing.impact.PlayAnim("land").TriggerOnEnter(GameHashes.JettisonCargo, null).OnAnimQueueComplete(this.grounded.crater);
	}

	// Token: 0x04001892 RID: 6290
	public StateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.IntParameter destinationWorld = new StateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.IntParameter(-1);

	// Token: 0x04001893 RID: 6291
	public StateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.BoolParameter onSurface = new StateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.BoolParameter(false);

	// Token: 0x04001894 RID: 6292
	public StateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.Signal beginTravelling;

	// Token: 0x04001895 RID: 6293
	public StateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.Signal launch;

	// Token: 0x04001896 RID: 6294
	public RailGunPayload.TakeoffStates takeoff;

	// Token: 0x04001897 RID: 6295
	public RailGunPayload.TravelStates travel;

	// Token: 0x04001898 RID: 6296
	public RailGunPayload.LandingStates landing;

	// Token: 0x04001899 RID: 6297
	public RailGunPayload.GroundedStates grounded;

	// Token: 0x020012AD RID: 4781
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04005E55 RID: 24149
		public bool attractToBeacons;

		// Token: 0x04005E56 RID: 24150
		public string clusterAnimSymbolSwapTarget;

		// Token: 0x04005E57 RID: 24151
		public List<string> randomClusterSymbolSwaps;

		// Token: 0x04005E58 RID: 24152
		public string worldAnimSymbolSwapTarget;

		// Token: 0x04005E59 RID: 24153
		public List<string> randomWorldSymbolSwaps;
	}

	// Token: 0x020012AE RID: 4782
	public class TakeoffStates : GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State
	{
		// Token: 0x04005E5A RID: 24154
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State launch;

		// Token: 0x04005E5B RID: 24155
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State airborne;
	}

	// Token: 0x020012AF RID: 4783
	public class TravelStates : GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State
	{
		// Token: 0x04005E5C RID: 24156
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State travelling;

		// Token: 0x04005E5D RID: 24157
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State transferWorlds;
	}

	// Token: 0x020012B0 RID: 4784
	public class LandingStates : GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State
	{
		// Token: 0x04005E5E RID: 24158
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State landing;

		// Token: 0x04005E5F RID: 24159
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State impact;
	}

	// Token: 0x020012B1 RID: 4785
	public class GroundedStates : GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State
	{
		// Token: 0x04005E60 RID: 24160
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State crater;

		// Token: 0x04005E61 RID: 24161
		public GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.State idle;
	}

	// Token: 0x020012B2 RID: 4786
	public class StatesInstance : GameStateMachine<RailGunPayload, RailGunPayload.StatesInstance, IStateMachineTarget, RailGunPayload.Def>.GameInstance
	{
		// Token: 0x06007B17 RID: 31511 RVA: 0x002CA4E8 File Offset: 0x002C86E8
		public StatesInstance(IStateMachineTarget master, RailGunPayload.Def def)
			: base(master, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
			DebugUtil.Assert(def.clusterAnimSymbolSwapTarget == null == (def.worldAnimSymbolSwapTarget == null), "Must specify both or neither symbol swap targets!");
			DebugUtil.Assert((def.randomClusterSymbolSwaps == null && def.randomWorldSymbolSwaps == null) || def.randomClusterSymbolSwaps.Count == def.randomWorldSymbolSwaps.Count, "Must specify the same number of swaps for both world and cluster!");
			if (def.clusterAnimSymbolSwapTarget != null && def.worldAnimSymbolSwapTarget != null)
			{
				if (this.randomSymbolSwapIndex == -1)
				{
					this.randomSymbolSwapIndex = UnityEngine.Random.Range(0, def.randomClusterSymbolSwaps.Count);
					global::Debug.Log(string.Format("Rolling a random symbol: {0}", this.randomSymbolSwapIndex), base.gameObject);
				}
				base.GetComponent<BallisticClusterGridEntity>().SwapSymbolFromSameAnim(def.clusterAnimSymbolSwapTarget, def.randomClusterSymbolSwaps[this.randomSymbolSwapIndex]);
				KAnim.Build.Symbol symbol = this.animController.AnimFiles[0].GetData().build.GetSymbol(def.randomWorldSymbolSwaps[this.randomSymbolSwapIndex]);
				this.animController.GetComponent<SymbolOverrideController>().AddSymbolOverride(def.worldAnimSymbolSwapTarget, symbol, 0);
			}
		}

		// Token: 0x06007B18 RID: 31512 RVA: 0x002CA62C File Offset: 0x002C882C
		public void Launch(AxialI source, AxialI destination)
		{
			base.GetComponent<BallisticClusterGridEntity>().Configure(source, destination);
			int asteroidWorldIdAtLocation = ClusterUtil.GetAsteroidWorldIdAtLocation(destination);
			base.sm.destinationWorld.Set(asteroidWorldIdAtLocation, this, false);
			this.GoTo(base.sm.takeoff);
		}

		// Token: 0x06007B19 RID: 31513 RVA: 0x002CA674 File Offset: 0x002C8874
		public void Travel(AxialI source, AxialI destination)
		{
			base.GetComponent<BallisticClusterGridEntity>().Configure(source, destination);
			int asteroidWorldIdAtLocation = ClusterUtil.GetAsteroidWorldIdAtLocation(destination);
			base.sm.destinationWorld.Set(asteroidWorldIdAtLocation, this, false);
			this.GoTo(base.sm.travel);
		}

		// Token: 0x06007B1A RID: 31514 RVA: 0x002CA6BA File Offset: 0x002C88BA
		public void StartTakeoff()
		{
			if (GameComps.Fallers.Has(base.gameObject))
			{
				GameComps.Fallers.Remove(base.gameObject);
			}
		}

		// Token: 0x06007B1B RID: 31515 RVA: 0x002CA6E0 File Offset: 0x002C88E0
		public void StartLand()
		{
			WorldContainer world = ClusterManager.Instance.GetWorld(base.sm.destinationWorld.Get(this));
			int num = Grid.InvalidCell;
			if (base.def.attractToBeacons)
			{
				num = ClusterManager.Instance.GetLandingBeaconLocation(world.id);
			}
			int num6;
			if (num != Grid.InvalidCell)
			{
				int num2;
				int num3;
				Grid.CellToXY(num, out num2, out num3);
				int num4 = Mathf.Max(num2 - 3, (int)world.minimumBounds.x);
				int num5 = Mathf.Min(num2 + 3, (int)world.maximumBounds.x);
				num6 = Mathf.RoundToInt((float)UnityEngine.Random.Range(num4, num5));
			}
			else
			{
				num6 = Mathf.RoundToInt(UnityEngine.Random.Range(world.minimumBounds.x + 3f, world.maximumBounds.x - 3f));
			}
			Vector3 vector = new Vector3((float)num6 + 0.5f, world.maximumBounds.y - 1f, Grid.GetLayerZ(Grid.SceneLayer.Front));
			base.transform.SetPosition(vector);
			if (GameComps.Fallers.Has(base.gameObject))
			{
				GameComps.Fallers.Remove(base.gameObject);
			}
			GameComps.Fallers.Add(base.gameObject, new Vector2(0f, -10f));
			base.sm.destinationWorld.Set(-1, this, false);
		}

		// Token: 0x06007B1C RID: 31516 RVA: 0x002CA834 File Offset: 0x002C8A34
		public void UpdateLaunch(float dt)
		{
			if (base.gameObject.GetMyWorld() != null)
			{
				Vector3 vector = base.transform.GetPosition() + new Vector3(0f, this.takeoffVelocity * dt, 0f);
				base.transform.SetPosition(vector);
				return;
			}
			base.sm.beginTravelling.Trigger(this);
			ClusterGridEntity component = base.GetComponent<ClusterGridEntity>();
			if (ClusterGrid.Instance.GetAsteroidAtCell(component.Location) != null)
			{
				base.GetComponent<ClusterTraveler>().AdvancePathOneStep();
			}
		}

		// Token: 0x06007B1D RID: 31517 RVA: 0x002CA8C8 File Offset: 0x002C8AC8
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

		// Token: 0x06007B1E RID: 31518 RVA: 0x002CA91E File Offset: 0x002C8B1E
		public void OnDroppedAll()
		{
			base.gameObject.DeleteObject();
		}

		// Token: 0x06007B1F RID: 31519 RVA: 0x002CA92B File Offset: 0x002C8B2B
		public bool IsTraveling()
		{
			return base.IsInsideState(base.sm.travel.travelling);
		}

		// Token: 0x06007B20 RID: 31520 RVA: 0x002CA944 File Offset: 0x002C8B44
		public void MoveToSpace()
		{
			Pickupable component = base.GetComponent<Pickupable>();
			if (component != null)
			{
				component.deleteOffGrid = false;
			}
			base.gameObject.transform.SetPosition(new Vector3(-1f, -1f, 0f));
		}

		// Token: 0x06007B21 RID: 31521 RVA: 0x002CA990 File Offset: 0x002C8B90
		public void MoveToWorld()
		{
			Pickupable component = base.GetComponent<Pickupable>();
			if (component != null)
			{
				component.deleteOffGrid = true;
			}
		}

		// Token: 0x04005E62 RID: 24162
		[Serialize]
		public float takeoffVelocity;

		// Token: 0x04005E63 RID: 24163
		[Serialize]
		private int randomSymbolSwapIndex = -1;

		// Token: 0x04005E64 RID: 24164
		public KBatchedAnimController animController;
	}
}
