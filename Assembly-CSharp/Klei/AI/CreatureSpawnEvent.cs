using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D87 RID: 3463
	public class CreatureSpawnEvent : GameplayEvent<CreatureSpawnEvent.StatesInstance>
	{
		// Token: 0x060069AD RID: 27053 RVA: 0x0029207C File Offset: 0x0029027C
		public CreatureSpawnEvent()
			: base("HatchSpawnEvent", 0, 0)
		{
			this.title = GAMEPLAY_EVENTS.EVENT_TYPES.CREATURE_SPAWN.NAME;
			this.description = GAMEPLAY_EVENTS.EVENT_TYPES.CREATURE_SPAWN.DESCRIPTION;
		}

		// Token: 0x060069AE RID: 27054 RVA: 0x002920AB File Offset: 0x002902AB
		public override StateMachine.Instance GetSMI(GameplayEventManager manager, GameplayEventInstance eventInstance)
		{
			return new CreatureSpawnEvent.StatesInstance(manager, eventInstance, this);
		}

		// Token: 0x04004F6C RID: 20332
		public const string ID = "HatchSpawnEvent";

		// Token: 0x04004F6D RID: 20333
		public const float UPDATE_TIME = 4f;

		// Token: 0x04004F6E RID: 20334
		public const float NUM_TO_SPAWN = 10f;

		// Token: 0x04004F6F RID: 20335
		public const float duration = 40f;

		// Token: 0x04004F70 RID: 20336
		public static List<string> CreatureSpawnEventIDs = new List<string> { "Hatch", "Squirrel", "Puft", "Crab", "Drecko", "Mole", "LightBug", "Pacu" };

		// Token: 0x02001E59 RID: 7769
		public class StatesInstance : GameplayEventStateMachine<CreatureSpawnEvent.States, CreatureSpawnEvent.StatesInstance, GameplayEventManager, CreatureSpawnEvent>.GameplayEventStateMachineInstance
		{
			// Token: 0x06009B5E RID: 39774 RVA: 0x0033716C File Offset: 0x0033536C
			public StatesInstance(GameplayEventManager master, GameplayEventInstance eventInstance, CreatureSpawnEvent creatureEvent)
				: base(master, eventInstance, creatureEvent)
			{
			}

			// Token: 0x06009B5F RID: 39775 RVA: 0x00337182 File Offset: 0x00335382
			private void PickCreatureToSpawn()
			{
				this.creatureID = CreatureSpawnEvent.CreatureSpawnEventIDs.GetRandom<string>();
			}

			// Token: 0x06009B60 RID: 39776 RVA: 0x00337194 File Offset: 0x00335394
			private void PickSpawnLocations()
			{
				Vector3 position = Components.Telepads.Items.GetRandom<Telepad>().transform.GetPosition();
				int num = 100;
				ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
				GameScenePartitioner.Instance.GatherEntries((int)position.x - num / 2, (int)position.y - num / 2, num, num, GameScenePartitioner.Instance.plants, pooledList);
				foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
				{
					KPrefabID kprefabID = (KPrefabID)scenePartitionerEntry.obj;
					if (!kprefabID.GetComponent<TreeBud>())
					{
						base.smi.spawnPositions.Add(kprefabID.transform.GetPosition());
					}
				}
				pooledList.Recycle();
			}

			// Token: 0x06009B61 RID: 39777 RVA: 0x00337268 File Offset: 0x00335468
			public void InitializeEvent()
			{
				this.PickCreatureToSpawn();
				this.PickSpawnLocations();
			}

			// Token: 0x06009B62 RID: 39778 RVA: 0x00337276 File Offset: 0x00335476
			public void EndEvent()
			{
				this.creatureID = null;
				this.spawnPositions.Clear();
			}

			// Token: 0x06009B63 RID: 39779 RVA: 0x0033728C File Offset: 0x0033548C
			public void SpawnCreature()
			{
				if (this.spawnPositions.Count > 0)
				{
					Vector3 random = this.spawnPositions.GetRandom<Vector3>();
					Util.KInstantiate(Assets.GetPrefab(this.creatureID), random).SetActive(true);
				}
			}

			// Token: 0x04008873 RID: 34931
			[Serialize]
			private List<Vector3> spawnPositions = new List<Vector3>();

			// Token: 0x04008874 RID: 34932
			[Serialize]
			private string creatureID;
		}

		// Token: 0x02001E5A RID: 7770
		public class States : GameplayEventStateMachine<CreatureSpawnEvent.States, CreatureSpawnEvent.StatesInstance, GameplayEventManager, CreatureSpawnEvent>
		{
			// Token: 0x06009B64 RID: 39780 RVA: 0x003372D0 File Offset: 0x003354D0
			public override void InitializeStates(out StateMachine.BaseState default_state)
			{
				default_state = this.initialize_event;
				base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
				this.initialize_event.Enter(delegate(CreatureSpawnEvent.StatesInstance smi)
				{
					smi.InitializeEvent();
					smi.GoTo(this.spawn_season);
				});
				this.start.DoNothing();
				this.spawn_season.Update(delegate(CreatureSpawnEvent.StatesInstance smi, float dt)
				{
					smi.SpawnCreature();
				}, UpdateRate.SIM_4000ms, false).Exit(delegate(CreatureSpawnEvent.StatesInstance smi)
				{
					smi.EndEvent();
				});
			}

			// Token: 0x06009B65 RID: 39781 RVA: 0x00337364 File Offset: 0x00335564
			public override EventInfoData GenerateEventPopupData(CreatureSpawnEvent.StatesInstance smi)
			{
				return new EventInfoData(smi.gameplayEvent.title, smi.gameplayEvent.description, smi.gameplayEvent.animFileName)
				{
					location = GAMEPLAY_EVENTS.LOCATIONS.PRINTING_POD,
					whenDescription = GAMEPLAY_EVENTS.TIMES.NOW
				};
			}

			// Token: 0x04008875 RID: 34933
			public GameStateMachine<CreatureSpawnEvent.States, CreatureSpawnEvent.StatesInstance, GameplayEventManager, object>.State initialize_event;

			// Token: 0x04008876 RID: 34934
			public GameStateMachine<CreatureSpawnEvent.States, CreatureSpawnEvent.StatesInstance, GameplayEventManager, object>.State spawn_season;

			// Token: 0x04008877 RID: 34935
			public GameStateMachine<CreatureSpawnEvent.States, CreatureSpawnEvent.StatesInstance, GameplayEventManager, object>.State start;
		}
	}
}
