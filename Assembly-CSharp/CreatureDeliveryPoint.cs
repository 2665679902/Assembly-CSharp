using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020005A0 RID: 1440
public class CreatureDeliveryPoint : StateMachineComponent<CreatureDeliveryPoint.SMInstance>, IUserControlledCapacity
{
	// Token: 0x06002375 RID: 9077 RVA: 0x000BF7B0 File Offset: 0x000BD9B0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.fetches = new List<FetchOrder2>();
		TreeFilterable component = base.GetComponent<TreeFilterable>();
		component.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Combine(component.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
		base.GetComponent<Storage>().SetOffsets(this.deliveryOffsets);
		Prioritizable.AddRef(base.gameObject);
		if (CreatureDeliveryPoint.capacityStatusItem == null)
		{
			CreatureDeliveryPoint.capacityStatusItem = new StatusItem("StorageLocker", "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null);
			CreatureDeliveryPoint.capacityStatusItem.resolveStringCallback = delegate(string str, object data)
			{
				IUserControlledCapacity userControlledCapacity = (IUserControlledCapacity)data;
				string text = Util.FormatWholeNumber(Mathf.Floor(userControlledCapacity.AmountStored));
				string text2 = Util.FormatWholeNumber(userControlledCapacity.UserMaxCapacity);
				str = str.Replace("{Stored}", text).Replace("{Capacity}", text2).Replace("{Units}", userControlledCapacity.CapacityUnits);
				return str;
			};
		}
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, CreatureDeliveryPoint.capacityStatusItem, this);
	}

	// Token: 0x06002376 RID: 9078 RVA: 0x000BF88A File Offset: 0x000BDA8A
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
		base.Subscribe<CreatureDeliveryPoint>(-905833192, CreatureDeliveryPoint.OnCopySettingsDelegate);
		base.Subscribe<CreatureDeliveryPoint>(643180843, CreatureDeliveryPoint.RefreshCreatureCountDelegate);
		this.RefreshCreatureCount(null);
	}

	// Token: 0x06002377 RID: 9079 RVA: 0x000BF8C8 File Offset: 0x000BDAC8
	private void OnCopySettings(object data)
	{
		GameObject gameObject = (GameObject)data;
		if (gameObject == null)
		{
			return;
		}
		CreatureDeliveryPoint component = gameObject.GetComponent<CreatureDeliveryPoint>();
		if (component == null)
		{
			return;
		}
		this.creatureLimit = component.creatureLimit;
		this.RebalanceFetches();
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x000BF909 File Offset: 0x000BDB09
	private void OnFilterChanged(HashSet<Tag> tags)
	{
		this.ClearFetches();
		this.RebalanceFetches();
	}

	// Token: 0x06002379 RID: 9081 RVA: 0x000BF918 File Offset: 0x000BDB18
	private void RefreshCreatureCount(object data = null)
	{
		int num = Grid.OffsetCell(Grid.PosToCell(this), this.spawnOffset);
		CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(num);
		int num2 = this.storedCreatureCount;
		this.storedCreatureCount = 0;
		if (cavityForCell != null)
		{
			foreach (KPrefabID kprefabID in cavityForCell.creatures)
			{
				if (!kprefabID.HasTag(GameTags.Creatures.Bagged) && !kprefabID.HasTag(GameTags.Trapped))
				{
					this.storedCreatureCount++;
				}
			}
		}
		if (this.storedCreatureCount != num2)
		{
			this.RebalanceFetches();
		}
	}

	// Token: 0x0600237A RID: 9082 RVA: 0x000BF9D4 File Offset: 0x000BDBD4
	private void ClearFetches()
	{
		for (int i = this.fetches.Count - 1; i >= 0; i--)
		{
			this.fetches[i].Cancel("clearing all fetches");
		}
		this.fetches.Clear();
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x000BFA1C File Offset: 0x000BDC1C
	private void RebalanceFetches()
	{
		HashSet<Tag> tags = base.GetComponent<TreeFilterable>().GetTags();
		ChoreType creatureFetch = Db.Get().ChoreTypes.CreatureFetch;
		Storage component = base.GetComponent<Storage>();
		int num = this.creatureLimit - this.storedCreatureCount;
		int count = this.fetches.Count;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		for (int i = this.fetches.Count - 1; i >= 0; i--)
		{
			if (this.fetches[i].IsComplete())
			{
				this.fetches.RemoveAt(i);
				num2++;
			}
		}
		int num6 = 0;
		for (int j = 0; j < this.fetches.Count; j++)
		{
			if (!this.fetches[j].InProgress)
			{
				num6++;
			}
		}
		if (num6 == 0 && this.fetches.Count < num)
		{
			FetchOrder2 fetchOrder = new FetchOrder2(creatureFetch, tags, FetchChore.MatchCriteria.MatchID, GameTags.Creatures.Deliverable, null, component, 1f, Operational.State.Operational, 0);
			fetchOrder.Submit(new Action<FetchOrder2, Pickupable>(this.OnFetchComplete), false, new Action<FetchOrder2, Pickupable>(this.OnFetchBegun));
			this.fetches.Add(fetchOrder);
			num3++;
		}
		int num7 = this.fetches.Count - num;
		for (int k = this.fetches.Count - 1; k >= 0; k--)
		{
			if (num7 <= 0)
			{
				break;
			}
			if (!this.fetches[k].InProgress)
			{
				this.fetches[k].Cancel("fewer creatures in room");
				this.fetches.RemoveAt(k);
				num7--;
				num4++;
			}
		}
		while (num7 > 0 && this.fetches.Count > 0)
		{
			this.fetches[this.fetches.Count - 1].Cancel("fewer creatures in room");
			this.fetches.RemoveAt(this.fetches.Count - 1);
			num7--;
			num5++;
		}
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x000BFC18 File Offset: 0x000BDE18
	private void OnFetchComplete(FetchOrder2 fetchOrder, Pickupable fetchedItem)
	{
		this.RebalanceFetches();
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x000BFC20 File Offset: 0x000BDE20
	private void OnFetchBegun(FetchOrder2 fetchOrder, Pickupable fetchedItem)
	{
		this.RebalanceFetches();
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x000BFC28 File Offset: 0x000BDE28
	protected override void OnCleanUp()
	{
		base.smi.StopSM("OnCleanUp");
		TreeFilterable component = base.GetComponent<TreeFilterable>();
		component.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Remove(component.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
		base.OnCleanUp();
	}

	// Token: 0x170001E9 RID: 489
	// (get) Token: 0x0600237F RID: 9087 RVA: 0x000BFC67 File Offset: 0x000BDE67
	// (set) Token: 0x06002380 RID: 9088 RVA: 0x000BFC70 File Offset: 0x000BDE70
	float IUserControlledCapacity.UserMaxCapacity
	{
		get
		{
			return (float)this.creatureLimit;
		}
		set
		{
			this.creatureLimit = Mathf.RoundToInt(value);
			this.RebalanceFetches();
		}
	}

	// Token: 0x170001EA RID: 490
	// (get) Token: 0x06002381 RID: 9089 RVA: 0x000BFC84 File Offset: 0x000BDE84
	float IUserControlledCapacity.AmountStored
	{
		get
		{
			return (float)this.storedCreatureCount;
		}
	}

	// Token: 0x170001EB RID: 491
	// (get) Token: 0x06002382 RID: 9090 RVA: 0x000BFC8D File Offset: 0x000BDE8D
	float IUserControlledCapacity.MinCapacity
	{
		get
		{
			return 0f;
		}
	}

	// Token: 0x170001EC RID: 492
	// (get) Token: 0x06002383 RID: 9091 RVA: 0x000BFC94 File Offset: 0x000BDE94
	float IUserControlledCapacity.MaxCapacity
	{
		get
		{
			return 20f;
		}
	}

	// Token: 0x170001ED RID: 493
	// (get) Token: 0x06002384 RID: 9092 RVA: 0x000BFC9B File Offset: 0x000BDE9B
	bool IUserControlledCapacity.WholeValues
	{
		get
		{
			return true;
		}
	}

	// Token: 0x170001EE RID: 494
	// (get) Token: 0x06002385 RID: 9093 RVA: 0x000BFC9E File Offset: 0x000BDE9E
	LocString IUserControlledCapacity.CapacityUnits
	{
		get
		{
			return UI.UISIDESCREENS.CAPTURE_POINT_SIDE_SCREEN.UNITS_SUFFIX;
		}
	}

	// Token: 0x04001455 RID: 5205
	[MyCmpAdd]
	private Prioritizable prioritizable;

	// Token: 0x04001456 RID: 5206
	[Serialize]
	private int creatureLimit = 20;

	// Token: 0x04001457 RID: 5207
	private int storedCreatureCount;

	// Token: 0x04001458 RID: 5208
	public CellOffset[] deliveryOffsets = new CellOffset[1];

	// Token: 0x04001459 RID: 5209
	public CellOffset spawnOffset = new CellOffset(0, 0);

	// Token: 0x0400145A RID: 5210
	private List<FetchOrder2> fetches;

	// Token: 0x0400145B RID: 5211
	private static StatusItem capacityStatusItem;

	// Token: 0x0400145C RID: 5212
	public bool playAnimsOnFetch;

	// Token: 0x0400145D RID: 5213
	private static readonly EventSystem.IntraObjectHandler<CreatureDeliveryPoint> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<CreatureDeliveryPoint>(delegate(CreatureDeliveryPoint component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x0400145E RID: 5214
	private static readonly EventSystem.IntraObjectHandler<CreatureDeliveryPoint> RefreshCreatureCountDelegate = new EventSystem.IntraObjectHandler<CreatureDeliveryPoint>(delegate(CreatureDeliveryPoint component, object data)
	{
		component.RefreshCreatureCount(data);
	});

	// Token: 0x020011CE RID: 4558
	public class SMInstance : GameStateMachine<CreatureDeliveryPoint.States, CreatureDeliveryPoint.SMInstance, CreatureDeliveryPoint, object>.GameInstance
	{
		// Token: 0x0600781B RID: 30747 RVA: 0x002BD3EB File Offset: 0x002BB5EB
		public SMInstance(CreatureDeliveryPoint master)
			: base(master)
		{
		}
	}

	// Token: 0x020011CF RID: 4559
	public class States : GameStateMachine<CreatureDeliveryPoint.States, CreatureDeliveryPoint.SMInstance, CreatureDeliveryPoint>
	{
		// Token: 0x0600781C RID: 30748 RVA: 0x002BD3F4 File Offset: 0x002BB5F4
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.waiting;
			this.root.Update("RefreshCreatureCount", delegate(CreatureDeliveryPoint.SMInstance smi, float dt)
			{
				smi.master.RefreshCreatureCount(null);
			}, UpdateRate.SIM_1000ms, false).EventHandler(GameHashes.OnStorageChange, new StateMachine<CreatureDeliveryPoint.States, CreatureDeliveryPoint.SMInstance, CreatureDeliveryPoint, object>.State.Callback(CreatureDeliveryPoint.States.DropAllCreatures));
			this.waiting.EnterTransition(this.interact_waiting, (CreatureDeliveryPoint.SMInstance smi) => smi.master.playAnimsOnFetch);
			this.interact_waiting.WorkableStartTransition((CreatureDeliveryPoint.SMInstance smi) => smi.master.GetComponent<Storage>(), this.interact_delivery);
			this.interact_delivery.PlayAnim("working_pre").QueueAnim("working_pst", false, null).OnAnimQueueComplete(this.interact_waiting);
		}

		// Token: 0x0600781D RID: 30749 RVA: 0x002BD4DC File Offset: 0x002BB6DC
		public static void DropAllCreatures(CreatureDeliveryPoint.SMInstance smi)
		{
			Storage component = smi.master.GetComponent<Storage>();
			if (component.IsEmpty())
			{
				return;
			}
			List<GameObject> items = component.items;
			int count = items.Count;
			Vector3 vector = Grid.CellToPosCBC(Grid.OffsetCell(Grid.PosToCell(smi.transform.GetPosition()), smi.master.spawnOffset), Grid.SceneLayer.Creatures);
			for (int i = count - 1; i >= 0; i--)
			{
				GameObject gameObject = items[i];
				component.Drop(gameObject, true);
				gameObject.transform.SetPosition(vector);
				gameObject.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Creatures);
			}
			smi.master.RefreshCreatureCount(null);
		}

		// Token: 0x04005C0E RID: 23566
		public GameStateMachine<CreatureDeliveryPoint.States, CreatureDeliveryPoint.SMInstance, CreatureDeliveryPoint, object>.State waiting;

		// Token: 0x04005C0F RID: 23567
		public GameStateMachine<CreatureDeliveryPoint.States, CreatureDeliveryPoint.SMInstance, CreatureDeliveryPoint, object>.State interact_waiting;

		// Token: 0x04005C10 RID: 23568
		public GameStateMachine<CreatureDeliveryPoint.States, CreatureDeliveryPoint.SMInstance, CreatureDeliveryPoint, object>.State interact_delivery;
	}
}
