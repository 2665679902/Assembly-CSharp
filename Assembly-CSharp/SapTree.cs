using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200089A RID: 2202
public class SapTree : GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>
{
	// Token: 0x06003F21 RID: 16161 RVA: 0x001605EC File Offset: 0x0015E7EC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.alive;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.dead.ToggleStatusItem(CREATURES.STATUSITEMS.DEAD.NAME, CREATURES.STATUSITEMS.DEAD.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).ToggleTag(GameTags.PreventEmittingDisease).Enter(delegate(SapTree.StatesInstance smi)
		{
			GameUtil.KInstantiate(Assets.GetPrefab(EffectConfigs.PlantDeathId), smi.master.transform.GetPosition(), Grid.SceneLayer.FXFront, null, 0).SetActive(true);
			smi.master.Trigger(1623392196, null);
			smi.master.GetComponent<KBatchedAnimController>().StopAndClear();
		});
		this.alive.InitializeStates(this.masterTarget, this.dead).DefaultState(this.alive.normal);
		this.alive.normal.DefaultState(this.alive.normal.idle).EventTransition(GameHashes.Wilt, this.alive.wilting, (SapTree.StatesInstance smi) => smi.wiltCondition.IsWilting()).Update(delegate(SapTree.StatesInstance smi, float dt)
		{
			smi.CheckForFood();
		}, UpdateRate.SIM_1000ms, false);
		this.alive.normal.idle.PlayAnim("idle", KAnim.PlayMode.Loop).ToggleStatusItem(CREATURES.STATUSITEMS.IDLE.NAME, CREATURES.STATUSITEMS.IDLE.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, Db.Get().StatusItemCategories.Main).ParamTransition<bool>(this.hasNearbyEnemy, this.alive.normal.attacking_pre, GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.IsTrue)
			.ParamTransition<float>(this.storedSap, this.alive.normal.oozing, (SapTree.StatesInstance smi, float p) => p >= smi.def.stomachSize)
			.ParamTransition<GameObject>(this.foodItem, this.alive.normal.eating, GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.IsNotNull);
		this.alive.normal.eating.PlayAnim("eat_pre", KAnim.PlayMode.Once).QueueAnim("eat_loop", true, null).Update(delegate(SapTree.StatesInstance smi, float dt)
		{
			smi.EatFoodItem(dt);
		}, UpdateRate.SIM_1000ms, false)
			.ParamTransition<GameObject>(this.foodItem, this.alive.normal.eating_pst, GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.IsNull)
			.ParamTransition<float>(this.storedSap, this.alive.normal.eating_pst, (SapTree.StatesInstance smi, float p) => p >= smi.def.stomachSize);
		this.alive.normal.eating_pst.PlayAnim("eat_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.normal.idle);
		this.alive.normal.oozing.PlayAnim("ooze_pre", KAnim.PlayMode.Once).QueueAnim("ooze_loop", true, null).Update(delegate(SapTree.StatesInstance smi, float dt)
		{
			smi.Ooze(dt);
		}, UpdateRate.SIM_200ms, false)
			.ParamTransition<float>(this.storedSap, this.alive.normal.oozing_pst, (SapTree.StatesInstance smi, float p) => p <= 0f)
			.ParamTransition<bool>(this.hasNearbyEnemy, this.alive.normal.oozing_pst, GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.IsTrue);
		this.alive.normal.oozing_pst.PlayAnim("ooze_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.normal.idle);
		this.alive.normal.attacking_pre.PlayAnim("attacking_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.normal.attacking);
		this.alive.normal.attacking.PlayAnim("attacking_loop", KAnim.PlayMode.Once).Enter(delegate(SapTree.StatesInstance smi)
		{
			smi.DoAttack();
		}).OnAnimQueueComplete(this.alive.normal.attacking_cooldown);
		this.alive.normal.attacking_cooldown.PlayAnim("attacking_pst", KAnim.PlayMode.Once).QueueAnim("attack_cooldown", true, null).ParamTransition<bool>(this.hasNearbyEnemy, this.alive.normal.attacking_done, GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.IsFalse)
			.ScheduleGoTo((SapTree.StatesInstance smi) => smi.def.attackCooldown, this.alive.normal.attacking);
		this.alive.normal.attacking_done.PlayAnim("attack_to_idle", KAnim.PlayMode.Once).OnAnimQueueComplete(this.alive.normal.idle);
		this.alive.wilting.PlayAnim("withered", KAnim.PlayMode.Loop).EventTransition(GameHashes.WiltRecover, this.alive.normal, null).ToggleTag(GameTags.PreventEmittingDisease);
	}

	// Token: 0x0400296C RID: 10604
	public SapTree.AliveStates alive;

	// Token: 0x0400296D RID: 10605
	public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State dead;

	// Token: 0x0400296E RID: 10606
	private StateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.TargetParameter foodItem;

	// Token: 0x0400296F RID: 10607
	private StateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.BoolParameter hasNearbyEnemy;

	// Token: 0x04002970 RID: 10608
	private StateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.FloatParameter storedSap;

	// Token: 0x02001668 RID: 5736
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x0400698E RID: 27022
		public Vector2I foodSenseArea;

		// Token: 0x0400698F RID: 27023
		public float massEatRate;

		// Token: 0x04006990 RID: 27024
		public float kcalorieToKGConversionRatio;

		// Token: 0x04006991 RID: 27025
		public float stomachSize;

		// Token: 0x04006992 RID: 27026
		public float oozeRate;

		// Token: 0x04006993 RID: 27027
		public List<Vector3> oozeOffsets;

		// Token: 0x04006994 RID: 27028
		public Vector2I attackSenseArea;

		// Token: 0x04006995 RID: 27029
		public float attackCooldown;
	}

	// Token: 0x02001669 RID: 5737
	public class AliveStates : GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.PlantAliveSubState
	{
		// Token: 0x04006996 RID: 27030
		public SapTree.NormalStates normal;

		// Token: 0x04006997 RID: 27031
		public SapTree.WiltingState wilting;
	}

	// Token: 0x0200166A RID: 5738
	public class NormalStates : GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State
	{
		// Token: 0x04006998 RID: 27032
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State idle;

		// Token: 0x04006999 RID: 27033
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State eating;

		// Token: 0x0400699A RID: 27034
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State eating_pst;

		// Token: 0x0400699B RID: 27035
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State oozing;

		// Token: 0x0400699C RID: 27036
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State oozing_pst;

		// Token: 0x0400699D RID: 27037
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State attacking_pre;

		// Token: 0x0400699E RID: 27038
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State attacking;

		// Token: 0x0400699F RID: 27039
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State attacking_cooldown;

		// Token: 0x040069A0 RID: 27040
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State attacking_done;
	}

	// Token: 0x0200166B RID: 5739
	public class WiltingState : GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State
	{
		// Token: 0x040069A1 RID: 27041
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State wilting_pre;

		// Token: 0x040069A2 RID: 27042
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State wilting;

		// Token: 0x040069A3 RID: 27043
		public GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.State wilting_pst;
	}

	// Token: 0x0200166C RID: 5740
	public class StatesInstance : GameStateMachine<SapTree, SapTree.StatesInstance, IStateMachineTarget, SapTree.Def>.GameInstance
	{
		// Token: 0x060087A6 RID: 34726 RVA: 0x002F3430 File Offset: 0x002F1630
		public StatesInstance(IStateMachineTarget master, SapTree.Def def)
			: base(master, def)
		{
			Vector2I vector2I = Grid.PosToXY(base.gameObject.transform.GetPosition());
			Vector2I vector2I2 = new Vector2I(vector2I.x - def.attackSenseArea.x / 2, vector2I.y);
			this.attackExtents = new Extents(vector2I2.x, vector2I2.y, def.attackSenseArea.x, def.attackSenseArea.y);
			this.partitionerEntry = GameScenePartitioner.Instance.Add("SapTreeAttacker", this, this.attackExtents, GameScenePartitioner.Instance.objectLayers[0], new Action<object>(this.OnMinionChanged));
			Vector2I vector2I3 = new Vector2I(vector2I.x - def.foodSenseArea.x / 2, vector2I.y);
			this.feedExtents = new Extents(vector2I3.x, vector2I3.y, def.foodSenseArea.x, def.foodSenseArea.y);
		}

		// Token: 0x060087A7 RID: 34727 RVA: 0x002F352B File Offset: 0x002F172B
		protected override void OnCleanUp()
		{
			GameScenePartitioner.Instance.Free(ref this.partitionerEntry);
		}

		// Token: 0x060087A8 RID: 34728 RVA: 0x002F3540 File Offset: 0x002F1740
		public void EatFoodItem(float dt)
		{
			Pickupable pickupable = base.sm.foodItem.Get(this).GetComponent<Pickupable>().Take(base.def.massEatRate * dt);
			if (pickupable != null)
			{
				float num = pickupable.GetComponent<Edible>().Calories * 0.001f * base.def.kcalorieToKGConversionRatio;
				Util.KDestroyGameObject(pickupable.gameObject);
				PrimaryElement component = base.GetComponent<PrimaryElement>();
				this.storage.AddLiquid(SimHashes.Resin, num, component.Temperature, byte.MaxValue, 0, true, false);
				base.sm.storedSap.Set(this.storage.GetMassAvailable(SimHashes.Resin.CreateTag()), this, false);
			}
		}

		// Token: 0x060087A9 RID: 34729 RVA: 0x002F35F8 File Offset: 0x002F17F8
		public void Ooze(float dt)
		{
			float num = Mathf.Min(base.sm.storedSap.Get(this), dt * base.def.oozeRate);
			if (num <= 0f)
			{
				return;
			}
			int num2 = Mathf.FloorToInt(GameClock.Instance.GetTime() % (float)base.def.oozeOffsets.Count);
			this.storage.DropSome(SimHashes.Resin.CreateTag(), num, false, true, base.def.oozeOffsets[num2], true, false);
			base.sm.storedSap.Set(this.storage.GetMassAvailable(SimHashes.Resin.CreateTag()), this, false);
		}

		// Token: 0x060087AA RID: 34730 RVA: 0x002F36A8 File Offset: 0x002F18A8
		public void CheckForFood()
		{
			ListPool<ScenePartitionerEntry, SapTree>.PooledList pooledList = ListPool<ScenePartitionerEntry, SapTree>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(this.feedExtents, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				Pickupable pickupable = scenePartitionerEntry.obj as Pickupable;
				if (pickupable.GetComponent<Edible>() != null)
				{
					base.sm.foodItem.Set(pickupable.gameObject, this, false);
					return;
				}
			}
			base.sm.foodItem.Set(null, this);
		}

		// Token: 0x060087AB RID: 34731 RVA: 0x002F3758 File Offset: 0x002F1958
		public bool DoAttack()
		{
			int num = this.weapon.AttackArea(base.transform.GetPosition());
			base.sm.hasNearbyEnemy.Set(num > 0, this, false);
			return true;
		}

		// Token: 0x060087AC RID: 34732 RVA: 0x002F3794 File Offset: 0x002F1994
		private void OnMinionChanged(object obj)
		{
			if (obj as GameObject != null)
			{
				base.sm.hasNearbyEnemy.Set(true, this, false);
			}
		}

		// Token: 0x040069A4 RID: 27044
		[MyCmpReq]
		public WiltCondition wiltCondition;

		// Token: 0x040069A5 RID: 27045
		[MyCmpReq]
		public EntombVulnerable entombVulnerable;

		// Token: 0x040069A6 RID: 27046
		[MyCmpReq]
		private Storage storage;

		// Token: 0x040069A7 RID: 27047
		[MyCmpReq]
		private Weapon weapon;

		// Token: 0x040069A8 RID: 27048
		private HandleVector<int>.Handle partitionerEntry;

		// Token: 0x040069A9 RID: 27049
		private Extents feedExtents;

		// Token: 0x040069AA RID: 27050
		private Extents attackExtents;
	}
}
