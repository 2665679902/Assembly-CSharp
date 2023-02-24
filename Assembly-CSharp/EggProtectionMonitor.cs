using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020006D0 RID: 1744
public class EggProtectionMonitor : GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>
{
	// Token: 0x06002F6C RID: 12140 RVA: 0x000FA80C File Offset: 0x000F8A0C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.find_egg;
		this.root.EventHandler(GameHashes.ObjectDestroyed, delegate(EggProtectionMonitor.Instance smi, object d)
		{
			smi.Cleanup(d);
		});
		this.find_egg.BatchUpdate(new UpdateBucketWithUpdater<EggProtectionMonitor.Instance>.BatchUpdateDelegate(EggProtectionMonitor.Instance.FindEggToGuard), UpdateRate.SIM_200ms).ParamTransition<bool>(this.hasEggToGuard, this.guard.safe, GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.IsTrue);
		this.guard.Enter(delegate(EggProtectionMonitor.Instance smi)
		{
			smi.gameObject.AddOrGet<SymbolOverrideController>().ApplySymbolOverridesByAffix(Assets.GetAnim("pincher_kanim"), smi.def.animPrefix, "_heat", 0);
			smi.gameObject.AddOrGet<FactionAlignment>().SwitchAlignment(FactionManager.FactionID.Hostile);
		}).Exit(delegate(EggProtectionMonitor.Instance smi)
		{
			if (!smi.def.animPrefix.IsNullOrWhiteSpace())
			{
				smi.gameObject.AddOrGet<SymbolOverrideController>().ApplySymbolOverridesByAffix(Assets.GetAnim("pincher_kanim"), smi.def.animPrefix, null, 0);
			}
			else
			{
				smi.gameObject.AddOrGet<SymbolOverrideController>().RemoveBuildOverride(Assets.GetAnim("pincher_kanim").GetData(), 0);
			}
			smi.gameObject.AddOrGet<FactionAlignment>().SwitchAlignment(FactionManager.FactionID.Pest);
		}).Update("evaulate_egg", delegate(EggProtectionMonitor.Instance smi, float dt)
		{
			smi.CanProtectEgg();
		}, UpdateRate.SIM_1000ms, true)
			.ParamTransition<bool>(this.hasEggToGuard, this.find_egg, GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.IsFalse);
		this.guard.safe.Enter(delegate(EggProtectionMonitor.Instance smi)
		{
			smi.RefreshThreat(null);
		}).Update("safe", delegate(EggProtectionMonitor.Instance smi, float dt)
		{
			smi.RefreshThreat(null);
		}, UpdateRate.SIM_200ms, true).ToggleStatusItem(CREATURES.STATUSITEMS.PROTECTINGENTITY.NAME, CREATURES.STATUSITEMS.PROTECTINGENTITY.TOOLTIP, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, null, null, null);
		this.guard.threatened.ToggleBehaviour(GameTags.Creatures.Defend, (EggProtectionMonitor.Instance smi) => smi.MainThreat != null, delegate(EggProtectionMonitor.Instance smi)
		{
			smi.GoTo(this.guard.safe);
		}).Update("Threatened", new Action<EggProtectionMonitor.Instance, float>(EggProtectionMonitor.CritterUpdateThreats), UpdateRate.SIM_200ms, false);
	}

	// Token: 0x06002F6D RID: 12141 RVA: 0x000FA9FB File Offset: 0x000F8BFB
	private static void CritterUpdateThreats(EggProtectionMonitor.Instance smi, float dt)
	{
		if (smi.isMasterNull)
		{
			return;
		}
		if (!smi.CheckForThreats())
		{
			smi.GoTo(smi.sm.guard.safe);
		}
	}

	// Token: 0x04001C8A RID: 7306
	public StateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.BoolParameter hasEggToGuard;

	// Token: 0x04001C8B RID: 7307
	public GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.State find_egg;

	// Token: 0x04001C8C RID: 7308
	public EggProtectionMonitor.GuardEggStates guard;

	// Token: 0x020013BE RID: 5054
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006170 RID: 24944
		public Tag[] allyTags;

		// Token: 0x04006171 RID: 24945
		public string animPrefix;
	}

	// Token: 0x020013BF RID: 5055
	public class GuardEggStates : GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.State
	{
		// Token: 0x04006172 RID: 24946
		public GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.State safe;

		// Token: 0x04006173 RID: 24947
		public GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.State threatened;
	}

	// Token: 0x020013C0 RID: 5056
	public new class Instance : GameStateMachine<EggProtectionMonitor, EggProtectionMonitor.Instance, IStateMachineTarget, EggProtectionMonitor.Def>.GameInstance
	{
		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06007ED6 RID: 32470 RVA: 0x002DA065 File Offset: 0x002D8265
		public GameObject MainThreat
		{
			get
			{
				return this.mainThreat;
			}
		}

		// Token: 0x06007ED7 RID: 32471 RVA: 0x002DA070 File Offset: 0x002D8270
		public Instance(IStateMachineTarget master, EggProtectionMonitor.Def def)
			: base(master, def)
		{
			this.alignment = master.GetComponent<FactionAlignment>();
			this.navigator = master.GetComponent<Navigator>();
			this.refreshThreatDelegate = new Action<object>(this.RefreshThreat);
		}

		// Token: 0x06007ED8 RID: 32472 RVA: 0x002DA0C4 File Offset: 0x002D82C4
		public void CanProtectEgg()
		{
			bool flag = true;
			if (this.eggToProtect == null)
			{
				flag = false;
			}
			if (flag)
			{
				int num = 150;
				int navigationCost = this.navigator.GetNavigationCost(Grid.PosToCell(this.eggToProtect));
				if (navigationCost == -1 || navigationCost >= num)
				{
					flag = false;
				}
			}
			if (!flag)
			{
				this.SetEggToGuard(null);
			}
		}

		// Token: 0x06007ED9 RID: 32473 RVA: 0x002DA118 File Offset: 0x002D8318
		public static void FindEggToGuard(List<UpdateBucketWithUpdater<EggProtectionMonitor.Instance>.Entry> instances, float time_delta)
		{
			ListPool<KPrefabID, EggProtectionMonitor>.PooledList pooledList = ListPool<KPrefabID, EggProtectionMonitor>.Allocate();
			pooledList.Capacity = Mathf.Max(pooledList.Capacity, Components.IncubationMonitors.Count);
			foreach (object obj in Components.IncubationMonitors)
			{
				IncubationMonitor.Instance instance = (IncubationMonitor.Instance)obj;
				pooledList.Add(instance.gameObject.GetComponent<KPrefabID>());
			}
			ListPool<EggProtectionMonitor.Instance.Egg, EggProtectionMonitor>.PooledList pooledList2 = ListPool<EggProtectionMonitor.Instance.Egg, EggProtectionMonitor>.Allocate();
			EggProtectionMonitor.Instance.find_eggs_job.Reset(pooledList);
			for (int i = 0; i < pooledList.Count; i += 256)
			{
				EggProtectionMonitor.Instance.find_eggs_job.Add(new EggProtectionMonitor.Instance.FindEggsTask(i, Mathf.Min(i + 256, pooledList.Count)));
			}
			GlobalJobManager.Run(EggProtectionMonitor.Instance.find_eggs_job);
			for (int num = 0; num != EggProtectionMonitor.Instance.find_eggs_job.Count; num++)
			{
				EggProtectionMonitor.Instance.find_eggs_job.GetWorkItem(num).Finish(pooledList, pooledList2);
			}
			pooledList.Recycle();
			EggProtectionMonitor.Instance.find_eggs_job.Reset(null);
			foreach (UpdateBucketWithUpdater<EggProtectionMonitor.Instance>.Entry entry in new List<UpdateBucketWithUpdater<EggProtectionMonitor.Instance>.Entry>(instances))
			{
				GameObject gameObject = null;
				int num2 = 100;
				foreach (EggProtectionMonitor.Instance.Egg egg in pooledList2)
				{
					int navigationCost = entry.data.navigator.GetNavigationCost(egg.cell);
					if (navigationCost != -1 && navigationCost < num2)
					{
						gameObject = egg.game_object;
						num2 = navigationCost;
					}
				}
				entry.data.SetEggToGuard(gameObject);
			}
			pooledList2.Recycle();
		}

		// Token: 0x06007EDA RID: 32474 RVA: 0x002DA2FC File Offset: 0x002D84FC
		public void SetEggToGuard(GameObject egg)
		{
			this.eggToProtect = egg;
			base.sm.hasEggToGuard.Set(egg != null, base.smi, false);
		}

		// Token: 0x06007EDB RID: 32475 RVA: 0x002DA324 File Offset: 0x002D8524
		public void SetMainThreat(GameObject threat)
		{
			if (threat == this.mainThreat)
			{
				return;
			}
			if (this.mainThreat != null)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
				if (threat == null)
				{
					base.Trigger(2144432245, null);
				}
			}
			if (this.mainThreat != null)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
			}
			this.mainThreat = threat;
			if (this.mainThreat != null)
			{
				this.mainThreat.Subscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Subscribe(1969584890, this.refreshThreatDelegate);
			}
		}

		// Token: 0x06007EDC RID: 32476 RVA: 0x002DA40C File Offset: 0x002D860C
		public void Cleanup(object data)
		{
			if (this.mainThreat)
			{
				this.mainThreat.Unsubscribe(1623392196, this.refreshThreatDelegate);
				this.mainThreat.Unsubscribe(1969584890, this.refreshThreatDelegate);
			}
		}

		// Token: 0x06007EDD RID: 32477 RVA: 0x002DA447 File Offset: 0x002D8647
		public void GoToThreatened()
		{
			base.smi.GoTo(base.sm.guard.threatened);
		}

		// Token: 0x06007EDE RID: 32478 RVA: 0x002DA464 File Offset: 0x002D8664
		public void RefreshThreat(object data)
		{
			if (!base.IsRunning() || this.eggToProtect == null)
			{
				return;
			}
			if (base.smi.CheckForThreats())
			{
				this.GoToThreatened();
				return;
			}
			if (base.smi.GetCurrentState() != base.sm.guard.safe)
			{
				base.Trigger(-21431934, null);
				base.smi.GoTo(base.sm.guard.safe);
			}
		}

		// Token: 0x06007EDF RID: 32479 RVA: 0x002DA4E0 File Offset: 0x002D86E0
		public bool CheckForThreats()
		{
			if (this.eggToProtect == null)
			{
				return false;
			}
			GameObject gameObject = this.FindThreat();
			this.SetMainThreat(gameObject);
			return gameObject != null;
		}

		// Token: 0x06007EE0 RID: 32480 RVA: 0x002DA514 File Offset: 0x002D8714
		public GameObject FindThreat()
		{
			this.threats.Clear();
			ListPool<ScenePartitionerEntry, ThreatMonitor>.PooledList pooledList = ListPool<ScenePartitionerEntry, ThreatMonitor>.Allocate();
			Extents extents = new Extents(Grid.PosToCell(this.eggToProtect), this.maxThreatDistance);
			GameScenePartitioner.Instance.GatherEntries(extents, GameScenePartitioner.Instance.attackableEntitiesLayer, pooledList);
			for (int i = 0; i < pooledList.Count; i++)
			{
				FactionAlignment factionAlignment = pooledList[i].obj as FactionAlignment;
				if (!(factionAlignment.transform == null) && !(factionAlignment == this.alignment) && factionAlignment.IsAlignmentActive() && this.navigator.CanReach(factionAlignment.attackable))
				{
					bool flag = false;
					foreach (Tag tag in base.def.allyTags)
					{
						if (factionAlignment.HasTag(tag))
						{
							flag = true;
						}
					}
					if (!flag)
					{
						this.threats.Add(factionAlignment);
					}
				}
			}
			pooledList.Recycle();
			return this.PickBestTarget(this.threats);
		}

		// Token: 0x06007EE1 RID: 32481 RVA: 0x002DA620 File Offset: 0x002D8820
		public GameObject PickBestTarget(List<FactionAlignment> threats)
		{
			float num = 1f;
			Vector2 vector = base.gameObject.transform.GetPosition();
			GameObject gameObject = null;
			float num2 = float.PositiveInfinity;
			for (int i = threats.Count - 1; i >= 0; i--)
			{
				FactionAlignment factionAlignment = threats[i];
				float num3 = Vector2.Distance(vector, factionAlignment.transform.GetPosition()) / num;
				if (num3 < num2)
				{
					num2 = num3;
					gameObject = factionAlignment.gameObject;
				}
			}
			return gameObject;
		}

		// Token: 0x04006174 RID: 24948
		public GameObject eggToProtect;

		// Token: 0x04006175 RID: 24949
		public FactionAlignment alignment;

		// Token: 0x04006176 RID: 24950
		private Navigator navigator;

		// Token: 0x04006177 RID: 24951
		private GameObject mainThreat;

		// Token: 0x04006178 RID: 24952
		private List<FactionAlignment> threats = new List<FactionAlignment>();

		// Token: 0x04006179 RID: 24953
		private int maxThreatDistance = 12;

		// Token: 0x0400617A RID: 24954
		private Action<object> refreshThreatDelegate;

		// Token: 0x0400617B RID: 24955
		private static WorkItemCollection<EggProtectionMonitor.Instance.FindEggsTask, List<KPrefabID>> find_eggs_job = new WorkItemCollection<EggProtectionMonitor.Instance.FindEggsTask, List<KPrefabID>>();

		// Token: 0x0200203B RID: 8251
		private struct Egg
		{
			// Token: 0x04008F92 RID: 36754
			public GameObject game_object;

			// Token: 0x04008F93 RID: 36755
			public int cell;
		}

		// Token: 0x0200203C RID: 8252
		private struct FindEggsTask : IWorkItem<List<KPrefabID>>
		{
			// Token: 0x0600A2E6 RID: 41702 RVA: 0x00345F8F File Offset: 0x0034418F
			public FindEggsTask(int start, int end)
			{
				this.start = start;
				this.end = end;
				this.eggs = ListPool<int, EggProtectionMonitor>.Allocate();
			}

			// Token: 0x0600A2E7 RID: 41703 RVA: 0x00345FAC File Offset: 0x003441AC
			public void Run(List<KPrefabID> prefab_ids)
			{
				for (int num = this.start; num != this.end; num++)
				{
					if (EggProtectionMonitor.Instance.FindEggsTask.EGG_TAG.Contains(prefab_ids[num].PrefabTag))
					{
						this.eggs.Add(num);
					}
				}
			}

			// Token: 0x0600A2E8 RID: 41704 RVA: 0x00345FF4 File Offset: 0x003441F4
			public void Finish(List<KPrefabID> prefab_ids, List<EggProtectionMonitor.Instance.Egg> eggs)
			{
				foreach (int num in this.eggs)
				{
					GameObject gameObject = prefab_ids[num].gameObject;
					eggs.Add(new EggProtectionMonitor.Instance.Egg
					{
						game_object = gameObject,
						cell = Grid.PosToCell(gameObject)
					});
				}
				this.eggs.Recycle();
			}

			// Token: 0x04008F94 RID: 36756
			private static readonly List<Tag> EGG_TAG = new List<Tag>
			{
				"CrabEgg".ToTag(),
				"CrabWoodEgg".ToTag(),
				"CrabFreshWaterEgg".ToTag()
			};

			// Token: 0x04008F95 RID: 36757
			private ListPool<int, EggProtectionMonitor>.PooledList eggs;

			// Token: 0x04008F96 RID: 36758
			private int start;

			// Token: 0x04008F97 RID: 36759
			private int end;
		}
	}
}
