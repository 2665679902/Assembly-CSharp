using System;
using UnityEngine;

// Token: 0x02000463 RID: 1123
public class ClimbableTreeMonitor : GameStateMachine<ClimbableTreeMonitor, ClimbableTreeMonitor.Instance, IStateMachineTarget, ClimbableTreeMonitor.Def>
{
	// Token: 0x060018EF RID: 6383 RVA: 0x0008520C File Offset: 0x0008340C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToClimbTree, (ClimbableTreeMonitor.Instance smi) => smi.UpdateHasClimbable(), delegate(ClimbableTreeMonitor.Instance smi)
		{
			smi.OnClimbComplete();
		});
	}

	// Token: 0x04000DF1 RID: 3569
	private const int MAX_NAV_COST = 2147483647;

	// Token: 0x0200108C RID: 4236
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040057F7 RID: 22519
		public float searchMinInterval = 60f;

		// Token: 0x040057F8 RID: 22520
		public float searchMaxInterval = 120f;
	}

	// Token: 0x0200108D RID: 4237
	public new class Instance : GameStateMachine<ClimbableTreeMonitor, ClimbableTreeMonitor.Instance, IStateMachineTarget, ClimbableTreeMonitor.Def>.GameInstance
	{
		// Token: 0x0600736F RID: 29551 RVA: 0x002B0486 File Offset: 0x002AE686
		public Instance(IStateMachineTarget master, ClimbableTreeMonitor.Def def)
			: base(master, def)
		{
			this.RefreshSearchTime();
		}

		// Token: 0x06007370 RID: 29552 RVA: 0x002B0496 File Offset: 0x002AE696
		private void RefreshSearchTime()
		{
			this.nextSearchTime = Time.time + Mathf.Lerp(base.def.searchMinInterval, base.def.searchMaxInterval, UnityEngine.Random.value);
		}

		// Token: 0x06007371 RID: 29553 RVA: 0x002B04C4 File Offset: 0x002AE6C4
		public bool UpdateHasClimbable()
		{
			if (this.climbTarget == null)
			{
				if (Time.time < this.nextSearchTime)
				{
					return false;
				}
				this.FindClimbableTree();
				this.RefreshSearchTime();
			}
			return this.climbTarget != null;
		}

		// Token: 0x06007372 RID: 29554 RVA: 0x002B04FC File Offset: 0x002AE6FC
		private void FindClimbableTree()
		{
			this.climbTarget = null;
			ListPool<ScenePartitionerEntry, GameScenePartitioner>.PooledList pooledList = ListPool<ScenePartitionerEntry, GameScenePartitioner>.Allocate();
			ListPool<KMonoBehaviour, ClimbableTreeMonitor>.PooledList pooledList2 = ListPool<KMonoBehaviour, ClimbableTreeMonitor>.Allocate();
			Vector3 position = base.master.transform.GetPosition();
			Extents extents = new Extents(Grid.PosToCell(position), 10);
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.plants, pooledList);
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.completeBuildings, pooledList);
			Navigator component = base.GetComponent<Navigator>();
			foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
			{
				KMonoBehaviour kmonoBehaviour = scenePartitionerEntry.obj as KMonoBehaviour;
				if (!kmonoBehaviour.HasTag(GameTags.Creatures.ReservedByCreature))
				{
					int num = Grid.PosToCell(kmonoBehaviour);
					if (component.CanReach(num))
					{
						BuddingTrunk component2 = kmonoBehaviour.GetComponent<BuddingTrunk>();
						StorageLocker component3 = kmonoBehaviour.GetComponent<StorageLocker>();
						if (component2 != null)
						{
							if (!component2.ExtraSeedAvailable)
							{
								continue;
							}
						}
						else
						{
							if (!(component3 != null))
							{
								continue;
							}
							Storage component4 = component3.GetComponent<Storage>();
							if (!component4.allowItemRemoval || component4.IsEmpty())
							{
								continue;
							}
						}
						pooledList2.Add(kmonoBehaviour);
					}
				}
			}
			if (pooledList2.Count > 0)
			{
				int num2 = UnityEngine.Random.Range(0, pooledList2.Count);
				KMonoBehaviour kmonoBehaviour2 = pooledList2[num2];
				this.climbTarget = kmonoBehaviour2.gameObject;
			}
			pooledList.Recycle();
			pooledList2.Recycle();
		}

		// Token: 0x06007373 RID: 29555 RVA: 0x002B066C File Offset: 0x002AE86C
		public void OnClimbComplete()
		{
			this.climbTarget = null;
		}

		// Token: 0x040057F9 RID: 22521
		public GameObject climbTarget;

		// Token: 0x040057FA RID: 22522
		public float nextSearchTime;
	}
}
