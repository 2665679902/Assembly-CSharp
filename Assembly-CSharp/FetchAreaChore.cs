using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000382 RID: 898
public class FetchAreaChore : Chore<FetchAreaChore.StatesInstance>
{
	// Token: 0x1700005D RID: 93
	// (get) Token: 0x06001244 RID: 4676 RVA: 0x00061A06 File Offset: 0x0005FC06
	public bool IsFetching
	{
		get
		{
			return base.smi.pickingup;
		}
	}

	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06001245 RID: 4677 RVA: 0x00061A13 File Offset: 0x0005FC13
	public bool IsDelivering
	{
		get
		{
			return base.smi.delivering;
		}
	}

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06001246 RID: 4678 RVA: 0x00061A20 File Offset: 0x0005FC20
	public GameObject GetFetchTarget
	{
		get
		{
			return base.smi.sm.fetchTarget.Get(base.smi);
		}
	}

	// Token: 0x06001247 RID: 4679 RVA: 0x00061A40 File Offset: 0x0005FC40
	public FetchAreaChore(Chore.Precondition.Context context)
		: base(context.chore.choreType, context.consumerState.consumer, context.consumerState.choreProvider, false, null, null, null, context.masterPriority.priority_class, context.masterPriority.priority_value, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		this.showAvailabilityInHoverText = false;
		base.smi = new FetchAreaChore.StatesInstance(this, context);
	}

	// Token: 0x06001248 RID: 4680 RVA: 0x00061AA8 File Offset: 0x0005FCA8
	public override void Cleanup()
	{
		base.Cleanup();
	}

	// Token: 0x06001249 RID: 4681 RVA: 0x00061AB0 File Offset: 0x0005FCB0
	public override void Begin(Chore.Precondition.Context context)
	{
		base.smi.Begin(context);
		base.Begin(context);
	}

	// Token: 0x0600124A RID: 4682 RVA: 0x00061AC5 File Offset: 0x0005FCC5
	protected override void End(string reason)
	{
		base.smi.End();
		base.End(reason);
	}

	// Token: 0x0600124B RID: 4683 RVA: 0x00061AD9 File Offset: 0x0005FCD9
	private void OnTagsChanged(object data)
	{
		if (base.smi.sm.fetchTarget.Get(base.smi) != null)
		{
			this.Fail("Tags changed");
		}
	}

	// Token: 0x0600124C RID: 4684 RVA: 0x00061B0C File Offset: 0x0005FD0C
	private static bool IsPickupableStillValidForChore(Pickupable pickupable, FetchChore chore)
	{
		KPrefabID component = pickupable.GetComponent<KPrefabID>();
		if ((chore.criteria == FetchChore.MatchCriteria.MatchID && !chore.tags.Contains(component.PrefabTag)) || (chore.criteria == FetchChore.MatchCriteria.MatchTags && !component.HasTag(chore.tagsFirst)))
		{
			global::Debug.Log(string.Format("Pickupable {0} is not valid for chore because it is not or does not contain one of these tags: {1}", pickupable, string.Join<Tag>(",", chore.tags)));
			return false;
		}
		if (chore.requiredTag.IsValid && !component.HasTag(chore.requiredTag))
		{
			global::Debug.Log(string.Format("Pickupable {0} is not valid for chore because it does not have the required tag: {1}", pickupable, chore.requiredTag));
			return false;
		}
		if (component.HasAnyTags(chore.forbiddenTags))
		{
			global::Debug.Log(string.Format("Pickupable {0} is not valid for chore because it has the forbidden tags: {1}", pickupable, string.Join<Tag>(",", chore.forbiddenTags)));
			return false;
		}
		return true;
	}

	// Token: 0x0600124D RID: 4685 RVA: 0x00061BE0 File Offset: 0x0005FDE0
	public static void GatherNearbyFetchChores(FetchChore root_chore, Chore.Precondition.Context context, int x, int y, int radius, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> failed_contexts)
	{
		ListPool<ScenePartitionerEntry, FetchAreaChore>.PooledList pooledList = ListPool<ScenePartitionerEntry, FetchAreaChore>.Allocate();
		GameScenePartitioner.Instance.GatherEntries(x - radius, y - radius, radius * 2 + 1, radius * 2 + 1, GameScenePartitioner.Instance.fetchChoreLayer, pooledList);
		for (int i = 0; i < pooledList.Count; i++)
		{
			(pooledList[i].obj as FetchChore).CollectChoresFromGlobalChoreProvider(context.consumerState, succeeded_contexts, failed_contexts, true);
		}
		pooledList.Recycle();
	}

	// Token: 0x02000F63 RID: 3939
	public class StatesInstance : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.GameInstance
	{
		// Token: 0x06006F55 RID: 28501 RVA: 0x002A0870 File Offset: 0x0029EA70
		public StatesInstance(FetchAreaChore master, Chore.Precondition.Context context)
			: base(master)
		{
			this.rootContext = context;
			this.rootChore = context.chore as FetchChore;
		}

		// Token: 0x06006F56 RID: 28502 RVA: 0x002A08D4 File Offset: 0x0029EAD4
		public void Begin(Chore.Precondition.Context context)
		{
			base.sm.fetcher.Set(context.consumerState.gameObject, base.smi, false);
			this.chores.Clear();
			this.chores.Add(this.rootChore);
			int num;
			int num2;
			Grid.CellToXY(Grid.PosToCell(this.rootChore.destination.transform.GetPosition()), out num, out num2);
			ListPool<Chore.Precondition.Context, FetchAreaChore>.PooledList pooledList = ListPool<Chore.Precondition.Context, FetchAreaChore>.Allocate();
			ListPool<Chore.Precondition.Context, FetchAreaChore>.PooledList pooledList2 = ListPool<Chore.Precondition.Context, FetchAreaChore>.Allocate();
			if (this.rootChore.allowMultifetch)
			{
				FetchAreaChore.GatherNearbyFetchChores(this.rootChore, context, num, num2, 3, pooledList, pooledList2);
			}
			float num3 = Mathf.Max(1f, Db.Get().Attributes.CarryAmount.Lookup(context.consumerState.consumer).GetTotalValue());
			Pickupable pickupable = context.data as Pickupable;
			if (pickupable == null)
			{
				global::Debug.Assert(pooledList.Count > 0, "succeeded_contexts was empty");
				FetchChore fetchChore = (FetchChore)pooledList[0].chore;
				global::Debug.Assert(fetchChore != null, "fetch_chore was null");
				DebugUtil.LogWarningArgs(new object[] { "Missing root_fetchable for FetchAreaChore", fetchChore.destination, fetchChore.tagsFirst });
				pickupable = fetchChore.FindFetchTarget(context.consumerState);
			}
			global::Debug.Assert(pickupable != null, "root_fetchable was null");
			List<Pickupable> list = new List<Pickupable>();
			list.Add(pickupable);
			float num4 = pickupable.UnreservedAmount;
			float minTakeAmount = pickupable.MinTakeAmount;
			int num5 = 0;
			int num6 = 0;
			Grid.CellToXY(Grid.PosToCell(pickupable.transform.GetPosition()), out num5, out num6);
			int num7 = 9;
			num5 -= 3;
			num6 -= 3;
			ListPool<ScenePartitionerEntry, FetchAreaChore>.PooledList pooledList3 = ListPool<ScenePartitionerEntry, FetchAreaChore>.Allocate();
			GameScenePartitioner.Instance.GatherEntries(num5, num6, num7, num7, GameScenePartitioner.Instance.pickupablesLayer, pooledList3);
			Tag prefabTag = pickupable.GetComponent<KPrefabID>().PrefabTag;
			for (int i = 0; i < pooledList3.Count; i++)
			{
				ScenePartitionerEntry scenePartitionerEntry = pooledList3[i];
				if (num4 > num3)
				{
					break;
				}
				Pickupable pickupable2 = scenePartitionerEntry.obj as Pickupable;
				KPrefabID component = pickupable2.GetComponent<KPrefabID>();
				if (!(component.PrefabTag != prefabTag) && pickupable2.UnreservedAmount > 0f && (this.rootChore.criteria != FetchChore.MatchCriteria.MatchID || this.rootChore.tags.Contains(component.PrefabTag)) && (this.rootChore.criteria != FetchChore.MatchCriteria.MatchTags || component.HasTag(this.rootChore.tagsFirst)) && (!this.rootChore.requiredTag.IsValid || component.HasTag(this.rootChore.requiredTag)) && !component.HasAnyTags(this.rootChore.forbiddenTags) && !list.Contains(pickupable2) && this.rootContext.consumerState.consumer.CanReach(pickupable2))
				{
					float unreservedAmount = pickupable2.UnreservedAmount;
					list.Add(pickupable2);
					num4 += unreservedAmount;
					if (list.Count >= 10)
					{
						break;
					}
				}
			}
			pooledList3.Recycle();
			num4 = Mathf.Min(num3, num4);
			if (minTakeAmount > 0f)
			{
				num4 -= num4 % minTakeAmount;
			}
			this.deliveries.Clear();
			float num8 = Mathf.Min(this.rootChore.originalAmount, num4);
			if (minTakeAmount > 0f)
			{
				num8 -= num8 % minTakeAmount;
			}
			this.deliveries.Add(new FetchAreaChore.StatesInstance.Delivery(this.rootContext, num8, new Action<FetchChore>(this.OnFetchChoreCancelled)));
			float num9 = num8;
			int num10 = 0;
			while (num10 < pooledList.Count && num9 < num4)
			{
				Chore.Precondition.Context context2 = pooledList[num10];
				FetchChore fetchChore2 = context2.chore as FetchChore;
				if (fetchChore2 != this.rootChore && fetchChore2.overrideTarget == null && fetchChore2.driver == null && fetchChore2.tagsHash == this.rootChore.tagsHash && fetchChore2.requiredTag == this.rootChore.requiredTag && fetchChore2.forbidHash == this.rootChore.forbidHash)
				{
					num8 = Mathf.Min(fetchChore2.originalAmount, num4 - num9);
					if (minTakeAmount > 0f)
					{
						num8 -= num8 % minTakeAmount;
					}
					this.chores.Add(fetchChore2);
					this.deliveries.Add(new FetchAreaChore.StatesInstance.Delivery(context2, num8, new Action<FetchChore>(this.OnFetchChoreCancelled)));
					num9 += num8;
					if (this.deliveries.Count >= 10)
					{
						break;
					}
				}
				num10++;
			}
			num9 = Mathf.Min(num9, num4);
			float num11 = num9;
			this.fetchables.Clear();
			int num12 = 0;
			while (num12 < list.Count && num11 > 0f)
			{
				Pickupable pickupable3 = list[num12];
				num11 -= pickupable3.UnreservedAmount;
				this.fetchables.Add(pickupable3);
				num12++;
			}
			this.fetchAmountRequested = num9;
			this.reservations.Clear();
			pooledList.Recycle();
			pooledList2.Recycle();
		}

		// Token: 0x06006F57 RID: 28503 RVA: 0x002A0E0C File Offset: 0x0029F00C
		public void End()
		{
			foreach (FetchAreaChore.StatesInstance.Delivery delivery in this.deliveries)
			{
				delivery.Cleanup();
			}
			this.deliveries.Clear();
		}

		// Token: 0x06006F58 RID: 28504 RVA: 0x002A0E6C File Offset: 0x0029F06C
		public void SetupDelivery()
		{
			if (this.deliveries.Count == 0)
			{
				this.StopSM("FetchAreaChoreComplete");
				return;
			}
			FetchAreaChore.StatesInstance.Delivery nextDelivery = this.deliveries[0];
			if (FetchAreaChore.StatesInstance.s_transientDeliveryTags.Contains(nextDelivery.chore.requiredTag))
			{
				nextDelivery.chore.requiredTag = Tag.Invalid;
			}
			this.deliverables.RemoveAll(delegate(Pickupable x)
			{
				if (x == null || x.TotalAmount <= 0f)
				{
					return true;
				}
				if (!FetchAreaChore.IsPickupableStillValidForChore(x, nextDelivery.chore))
				{
					global::Debug.LogWarning(string.Format("Removing deliverable {0} for a delivery to {1} which did not request it", x, nextDelivery.chore.destination));
					return true;
				}
				return false;
			});
			if (this.deliverables.Count == 0)
			{
				this.StopSM("FetchAreaChoreComplete");
				return;
			}
			base.sm.deliveryDestination.Set(nextDelivery.destination, base.smi);
			base.sm.deliveryObject.Set(this.deliverables[0], base.smi);
			if (!(nextDelivery.destination != null))
			{
				base.smi.GoTo(base.sm.delivering.deliverfail);
				return;
			}
			if (!this.rootContext.consumerState.hasSolidTransferArm)
			{
				this.GoTo(base.sm.delivering.movetostorage);
				return;
			}
			if (this.rootContext.consumerState.consumer.IsWithinReach(this.deliveries[0].destination))
			{
				this.GoTo(base.sm.delivering.storing);
				return;
			}
			this.GoTo(base.sm.delivering.deliverfail);
		}

		// Token: 0x06006F59 RID: 28505 RVA: 0x002A1004 File Offset: 0x0029F204
		public void SetupFetch()
		{
			if (this.reservations.Count <= 0)
			{
				this.GoTo(base.sm.delivering.next);
				return;
			}
			base.sm.fetchTarget.Set(this.reservations[0].pickupable, base.smi);
			base.sm.fetchResultTarget.Set(null, base.smi);
			base.sm.fetchAmount.Set(this.reservations[0].amount, base.smi, false);
			if (!(this.reservations[0].pickupable != null))
			{
				this.GoTo(base.sm.fetching.fetchfail);
				return;
			}
			if (!this.rootContext.consumerState.hasSolidTransferArm)
			{
				this.GoTo(base.sm.fetching.movetopickupable);
				return;
			}
			if (this.rootContext.consumerState.consumer.IsWithinReach(this.reservations[0].pickupable))
			{
				this.GoTo(base.sm.fetching.pickup);
				return;
			}
			this.GoTo(base.sm.fetching.fetchfail);
		}

		// Token: 0x06006F5A RID: 28506 RVA: 0x002A1160 File Offset: 0x0029F360
		public void DeliverFail()
		{
			if (this.deliveries.Count > 0)
			{
				this.deliveries[0].Cleanup();
				this.deliveries.RemoveAt(0);
			}
			this.GoTo(base.sm.delivering.next);
		}

		// Token: 0x06006F5B RID: 28507 RVA: 0x002A11B4 File Offset: 0x0029F3B4
		public void DeliverComplete()
		{
			Pickupable pickupable = base.sm.deliveryObject.Get<Pickupable>(base.smi);
			if (!(pickupable == null) && pickupable.TotalAmount > 0f)
			{
				if (this.deliveries.Count > 0)
				{
					FetchAreaChore.StatesInstance.Delivery delivery = this.deliveries[0];
					Chore chore = delivery.chore;
					delivery.Complete(this.deliverables);
					delivery.Cleanup();
					if (this.deliveries.Count > 0 && this.deliveries[0].chore == chore)
					{
						this.deliveries.RemoveAt(0);
					}
				}
				this.GoTo(base.sm.delivering.next);
				return;
			}
			if (this.deliveries.Count > 0 && this.deliveries[0].chore.amount < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
			{
				FetchAreaChore.StatesInstance.Delivery delivery2 = this.deliveries[0];
				Chore chore2 = delivery2.chore;
				delivery2.Complete(this.deliverables);
				delivery2.Cleanup();
				if (this.deliveries.Count > 0 && this.deliveries[0].chore == chore2)
				{
					this.deliveries.RemoveAt(0);
				}
				this.GoTo(base.sm.delivering.next);
				return;
			}
			base.smi.GoTo(base.sm.delivering.deliverfail);
		}

		// Token: 0x06006F5C RID: 28508 RVA: 0x002A1330 File Offset: 0x0029F530
		public void FetchFail()
		{
			this.reservations[0].Cleanup();
			this.reservations.RemoveAt(0);
			this.GoTo(base.sm.fetching.next);
		}

		// Token: 0x06006F5D RID: 28509 RVA: 0x002A1374 File Offset: 0x0029F574
		public void FetchComplete()
		{
			this.reservations[0].Cleanup();
			this.reservations.RemoveAt(0);
			this.GoTo(base.sm.fetching.next);
		}

		// Token: 0x06006F5E RID: 28510 RVA: 0x002A13B8 File Offset: 0x0029F5B8
		public void SetupDeliverables()
		{
			foreach (GameObject gameObject in base.sm.fetcher.Get<Storage>(base.smi).items)
			{
				if (!(gameObject == null))
				{
					KPrefabID component = gameObject.GetComponent<KPrefabID>();
					if (!(component == null))
					{
						Pickupable component2 = component.GetComponent<Pickupable>();
						if (component2 != null)
						{
							this.deliverables.Add(component2);
						}
					}
				}
			}
		}

		// Token: 0x06006F5F RID: 28511 RVA: 0x002A1450 File Offset: 0x0029F650
		public void ReservePickupables()
		{
			ChoreConsumer choreConsumer = base.sm.fetcher.Get<ChoreConsumer>(base.smi);
			float num = this.fetchAmountRequested;
			foreach (Pickupable pickupable in this.fetchables)
			{
				if (num <= 0f)
				{
					break;
				}
				float num2 = Math.Min(num, pickupable.UnreservedAmount);
				num -= num2;
				FetchAreaChore.StatesInstance.Reservation reservation = new FetchAreaChore.StatesInstance.Reservation(choreConsumer, pickupable, num2);
				this.reservations.Add(reservation);
			}
		}

		// Token: 0x06006F60 RID: 28512 RVA: 0x002A14F0 File Offset: 0x0029F6F0
		private void OnFetchChoreCancelled(FetchChore chore)
		{
			int i = 0;
			while (i < this.deliveries.Count)
			{
				if (this.deliveries[i].chore == chore)
				{
					if (this.deliveries.Count == 1)
					{
						this.StopSM("AllDelivericesCancelled");
						return;
					}
					if (i == 0)
					{
						base.sm.currentdeliverycancelled.Trigger(this);
						return;
					}
					this.deliveries[i].Cleanup();
					this.deliveries.RemoveAt(i);
					return;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06006F61 RID: 28513 RVA: 0x002A157C File Offset: 0x0029F77C
		public void UnreservePickupables()
		{
			foreach (FetchAreaChore.StatesInstance.Reservation reservation in this.reservations)
			{
				reservation.Cleanup();
			}
			this.reservations.Clear();
		}

		// Token: 0x06006F62 RID: 28514 RVA: 0x002A15DC File Offset: 0x0029F7DC
		public bool SameDestination(FetchChore fetch)
		{
			using (List<FetchChore>.Enumerator enumerator = this.chores.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.destination == fetch.destination)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04005448 RID: 21576
		private List<FetchChore> chores = new List<FetchChore>();

		// Token: 0x04005449 RID: 21577
		private List<Pickupable> fetchables = new List<Pickupable>();

		// Token: 0x0400544A RID: 21578
		private List<FetchAreaChore.StatesInstance.Reservation> reservations = new List<FetchAreaChore.StatesInstance.Reservation>();

		// Token: 0x0400544B RID: 21579
		private List<Pickupable> deliverables = new List<Pickupable>();

		// Token: 0x0400544C RID: 21580
		public List<FetchAreaChore.StatesInstance.Delivery> deliveries = new List<FetchAreaChore.StatesInstance.Delivery>();

		// Token: 0x0400544D RID: 21581
		private FetchChore rootChore;

		// Token: 0x0400544E RID: 21582
		private Chore.Precondition.Context rootContext;

		// Token: 0x0400544F RID: 21583
		private float fetchAmountRequested;

		// Token: 0x04005450 RID: 21584
		public bool delivering;

		// Token: 0x04005451 RID: 21585
		public bool pickingup;

		// Token: 0x04005452 RID: 21586
		private static Tag[] s_transientDeliveryTags = new Tag[]
		{
			GameTags.Garbage,
			GameTags.Creatures.Deliverable
		};

		// Token: 0x02001EAF RID: 7855
		public struct Delivery
		{
			// Token: 0x170009F2 RID: 2546
			// (get) Token: 0x06009C61 RID: 40033 RVA: 0x0033A3CF File Offset: 0x003385CF
			// (set) Token: 0x06009C62 RID: 40034 RVA: 0x0033A3D7 File Offset: 0x003385D7
			public Storage destination { readonly get; private set; }

			// Token: 0x170009F3 RID: 2547
			// (get) Token: 0x06009C63 RID: 40035 RVA: 0x0033A3E0 File Offset: 0x003385E0
			// (set) Token: 0x06009C64 RID: 40036 RVA: 0x0033A3E8 File Offset: 0x003385E8
			public float amount { readonly get; private set; }

			// Token: 0x170009F4 RID: 2548
			// (get) Token: 0x06009C65 RID: 40037 RVA: 0x0033A3F1 File Offset: 0x003385F1
			// (set) Token: 0x06009C66 RID: 40038 RVA: 0x0033A3F9 File Offset: 0x003385F9
			public FetchChore chore { readonly get; private set; }

			// Token: 0x06009C67 RID: 40039 RVA: 0x0033A404 File Offset: 0x00338604
			public Delivery(Chore.Precondition.Context context, float amount_to_be_fetched, Action<FetchChore> on_cancelled)
			{
				this = default(FetchAreaChore.StatesInstance.Delivery);
				this.chore = context.chore as FetchChore;
				this.amount = this.chore.originalAmount;
				this.destination = this.chore.destination;
				this.chore.SetOverrideTarget(context.consumerState.consumer);
				this.onCancelled = on_cancelled;
				this.onFetchChoreCleanup = new Action<Chore>(this.OnFetchChoreCleanup);
				this.chore.FetchAreaBegin(context, amount_to_be_fetched);
				FetchChore chore = this.chore;
				chore.onCleanup = (Action<Chore>)Delegate.Combine(chore.onCleanup, this.onFetchChoreCleanup);
			}

			// Token: 0x06009C68 RID: 40040 RVA: 0x0033A4B4 File Offset: 0x003386B4
			public void Complete(List<Pickupable> deliverables)
			{
				using (new KProfiler.Region("FAC.Delivery.Complete", null))
				{
					if (!(this.destination == null) && !this.destination.IsEndOfLife())
					{
						FetchChore chore = this.chore;
						chore.onCleanup = (Action<Chore>)Delegate.Remove(chore.onCleanup, this.onFetchChoreCleanup);
						float num = this.amount;
						Pickupable pickupable = null;
						int num2 = 0;
						while (num2 < deliverables.Count && num > 0f)
						{
							if (deliverables[num2] == null)
							{
								if (num < PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
								{
									this.destination.ForceStore(this.chore.tagsFirst, num);
								}
							}
							else if (!FetchAreaChore.IsPickupableStillValidForChore(deliverables[num2], this.chore))
							{
								global::Debug.LogError(string.Format("Attempting to store {0} in a {1} which did not request it", deliverables[num2], this.destination));
							}
							else
							{
								Pickupable pickupable2 = deliverables[num2].Take(num);
								if (pickupable2 != null && pickupable2.TotalAmount > 0f)
								{
									num -= pickupable2.TotalAmount;
									this.destination.Store(pickupable2.gameObject, false, false, true, false);
									pickupable = pickupable2;
									if (pickupable2 == deliverables[num2])
									{
										deliverables[num2] = null;
									}
								}
							}
							num2++;
						}
						if (this.chore.overrideTarget != null)
						{
							this.chore.FetchAreaEnd(this.chore.overrideTarget.GetComponent<ChoreDriver>(), pickupable, true);
						}
						this.chore = null;
					}
				}
			}

			// Token: 0x06009C69 RID: 40041 RVA: 0x0033A66C File Offset: 0x0033886C
			private void OnFetchChoreCleanup(Chore chore)
			{
				if (this.onCancelled != null)
				{
					this.onCancelled(chore as FetchChore);
				}
			}

			// Token: 0x06009C6A RID: 40042 RVA: 0x0033A687 File Offset: 0x00338887
			public void Cleanup()
			{
				if (this.chore != null)
				{
					FetchChore chore = this.chore;
					chore.onCleanup = (Action<Chore>)Delegate.Remove(chore.onCleanup, this.onFetchChoreCleanup);
					this.chore.FetchAreaEnd(null, null, false);
				}
			}

			// Token: 0x04008973 RID: 35187
			private Action<FetchChore> onCancelled;

			// Token: 0x04008974 RID: 35188
			private Action<Chore> onFetchChoreCleanup;
		}

		// Token: 0x02001EB0 RID: 7856
		public struct Reservation
		{
			// Token: 0x170009F5 RID: 2549
			// (get) Token: 0x06009C6B RID: 40043 RVA: 0x0033A6C0 File Offset: 0x003388C0
			// (set) Token: 0x06009C6C RID: 40044 RVA: 0x0033A6C8 File Offset: 0x003388C8
			public float amount { readonly get; private set; }

			// Token: 0x170009F6 RID: 2550
			// (get) Token: 0x06009C6D RID: 40045 RVA: 0x0033A6D1 File Offset: 0x003388D1
			// (set) Token: 0x06009C6E RID: 40046 RVA: 0x0033A6D9 File Offset: 0x003388D9
			public Pickupable pickupable { readonly get; private set; }

			// Token: 0x06009C6F RID: 40047 RVA: 0x0033A6E4 File Offset: 0x003388E4
			public Reservation(ChoreConsumer consumer, Pickupable pickupable, float reservation_amount)
			{
				this = default(FetchAreaChore.StatesInstance.Reservation);
				if (reservation_amount <= 0f)
				{
					global::Debug.LogError("Invalid amount: " + reservation_amount.ToString());
				}
				this.amount = reservation_amount;
				this.pickupable = pickupable;
				this.handle = pickupable.Reserve("FetchAreaChore", consumer.gameObject, reservation_amount);
			}

			// Token: 0x06009C70 RID: 40048 RVA: 0x0033A73C File Offset: 0x0033893C
			public void Cleanup()
			{
				if (this.pickupable != null)
				{
					this.pickupable.Unreserve("FetchAreaChore", this.handle);
				}
			}

			// Token: 0x04008977 RID: 35191
			private int handle;
		}
	}

	// Token: 0x02000F64 RID: 3940
	public class States : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore>
	{
		// Token: 0x06006F64 RID: 28516 RVA: 0x002A1668 File Offset: 0x0029F868
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.fetching;
			base.Target(this.fetcher);
			this.fetching.DefaultState(this.fetching.next).Enter("ReservePickupables", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.ReservePickupables();
			}).Exit("UnreservePickupables", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.UnreservePickupables();
			})
				.Enter("pickingup-on", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.pickingup = true;
				})
				.Exit("pickingup-off", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.pickingup = false;
				});
			this.fetching.next.Enter("SetupFetch", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.SetupFetch();
			});
			this.fetching.movetopickupable.InitializeStates(this.fetcher, this.fetchTarget, this.fetching.pickup, this.fetching.fetchfail, null, NavigationTactics.ReduceTravelDistance);
			this.fetching.pickup.DoPickup(this.fetchTarget, this.fetchResultTarget, this.fetchAmount, this.fetching.fetchcomplete, this.fetching.fetchfail);
			this.fetching.fetchcomplete.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.FetchComplete();
			});
			this.fetching.fetchfail.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.FetchFail();
			});
			this.delivering.DefaultState(this.delivering.next).OnSignal(this.currentdeliverycancelled, this.delivering.deliverfail).Enter("SetupDeliverables", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.SetupDeliverables();
			})
				.Enter("delivering-on", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.delivering = true;
				})
				.Exit("delivering-off", delegate(FetchAreaChore.StatesInstance smi)
				{
					smi.delivering = false;
				});
			this.delivering.next.Enter("SetupDelivery", delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.SetupDelivery();
			});
			this.delivering.movetostorage.InitializeStates(this.fetcher, this.deliveryDestination, this.delivering.storing, this.delivering.deliverfail, null, NavigationTactics.ReduceTravelDistance).Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				if (this.deliveryObject.Get(smi) != null && this.deliveryObject.Get(smi).GetComponent<MinionIdentity>() != null)
				{
					this.deliveryObject.Get(smi).transform.SetLocalPosition(Vector3.zero);
					KBatchedAnimTracker component = this.deliveryObject.Get(smi).GetComponent<KBatchedAnimTracker>();
					component.symbol = new HashedString("snapTo_chest");
					component.offset = new Vector3(0f, 0f, 1f);
				}
			});
			this.delivering.storing.DoDelivery(this.fetcher, this.deliveryDestination, this.delivering.delivercomplete, this.delivering.deliverfail);
			this.delivering.deliverfail.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.DeliverFail();
			});
			this.delivering.delivercomplete.Enter(delegate(FetchAreaChore.StatesInstance smi)
			{
				smi.DeliverComplete();
			});
		}

		// Token: 0x04005453 RID: 21587
		public FetchAreaChore.States.FetchStates fetching;

		// Token: 0x04005454 RID: 21588
		public FetchAreaChore.States.DeliverStates delivering;

		// Token: 0x04005455 RID: 21589
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter fetcher;

		// Token: 0x04005456 RID: 21590
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter fetchTarget;

		// Token: 0x04005457 RID: 21591
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter fetchResultTarget;

		// Token: 0x04005458 RID: 21592
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.FloatParameter fetchAmount;

		// Token: 0x04005459 RID: 21593
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter deliveryDestination;

		// Token: 0x0400545A RID: 21594
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.TargetParameter deliveryObject;

		// Token: 0x0400545B RID: 21595
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.FloatParameter deliveryAmount;

		// Token: 0x0400545C RID: 21596
		public StateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.Signal currentdeliverycancelled;

		// Token: 0x02001EB2 RID: 7858
		public class FetchStates : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State
		{
			// Token: 0x04008979 RID: 35193
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State next;

			// Token: 0x0400897A RID: 35194
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.ApproachSubState<Pickupable> movetopickupable;

			// Token: 0x0400897B RID: 35195
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State pickup;

			// Token: 0x0400897C RID: 35196
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State fetchfail;

			// Token: 0x0400897D RID: 35197
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State fetchcomplete;
		}

		// Token: 0x02001EB3 RID: 7859
		public class DeliverStates : GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State
		{
			// Token: 0x0400897E RID: 35198
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State next;

			// Token: 0x0400897F RID: 35199
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.ApproachSubState<Storage> movetostorage;

			// Token: 0x04008980 RID: 35200
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State storing;

			// Token: 0x04008981 RID: 35201
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State deliverfail;

			// Token: 0x04008982 RID: 35202
			public GameStateMachine<FetchAreaChore.States, FetchAreaChore.StatesInstance, FetchAreaChore, object>.State delivercomplete;
		}
	}
}
