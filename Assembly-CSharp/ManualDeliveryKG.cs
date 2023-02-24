using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200080A RID: 2058
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ManualDeliveryKG")]
public class ManualDeliveryKG : KMonoBehaviour, ISim1000ms
{
	// Token: 0x17000439 RID: 1081
	// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x0014A8B0 File Offset: 0x00148AB0
	// (set) Token: 0x06003BA2 RID: 15266 RVA: 0x0014A8D6 File Offset: 0x00148AD6
	public Tag RequestedItemTag
	{
		get
		{
			if (this.deliveryRequests.Count == 0)
			{
				return Tag.Invalid;
			}
			return this.deliveryRequests[0].Id;
		}
		set
		{
			this.AbortDelivery("Requested Item Tag Changed");
			this.ClearRequests();
			this.RequestItemInternal(value, this.minimumMass, null);
		}
	}

	// Token: 0x1700043A RID: 1082
	// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x0014A8F7 File Offset: 0x00148AF7
	// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x0014A8FF File Offset: 0x00148AFF
	public Tag[] ForbiddenTags
	{
		get
		{
			return this.forbiddenTags;
		}
		set
		{
			this.forbiddenTags = value;
			this.AbortDelivery("Forbidden Tags Changed");
		}
	}

	// Token: 0x1700043B RID: 1083
	// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x0014A913 File Offset: 0x00148B13
	public bool IsPaused
	{
		get
		{
			return this.paused;
		}
	}

	// Token: 0x1700043C RID: 1084
	// (get) Token: 0x06003BA6 RID: 15270 RVA: 0x0014A91B File Offset: 0x00148B1B
	public float Capacity
	{
		get
		{
			return this.capacity;
		}
	}

	// Token: 0x1700043D RID: 1085
	// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x0014A923 File Offset: 0x00148B23
	// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x0014A92B File Offset: 0x00148B2B
	public float MinimumMass
	{
		get
		{
			return this.minimumMass;
		}
		set
		{
			this.minimumMass = value;
			if (this.deliveryRequests != null && this.deliveryRequests.Count == 1)
			{
				this.deliveryRequests[0].MinimumAmountKG = this.minimumMass;
			}
		}
	}

	// Token: 0x06003BA9 RID: 15273 RVA: 0x0014A964 File Offset: 0x00148B64
	protected override void OnSpawn()
	{
		base.OnSpawn();
		DebugUtil.Assert(this.choreTypeIDHash.IsValid, "ManualDeliveryKG Must have a valid chore type specified!", base.name);
		if (this.allowPause)
		{
			base.Subscribe<ManualDeliveryKG>(493375141, ManualDeliveryKG.OnRefreshUserMenuDelegate);
			base.Subscribe<ManualDeliveryKG>(-111137758, ManualDeliveryKG.OnRefreshUserMenuDelegate);
		}
		base.Subscribe<ManualDeliveryKG>(-592767678, ManualDeliveryKG.OnOperationalChangedDelegate);
		if (this.storage != null)
		{
			this.SetStorage(this.storage);
		}
		Prioritizable.AddRef(base.gameObject);
		if (this.userPaused && this.allowPause)
		{
			this.OnPause();
		}
	}

	// Token: 0x06003BAA RID: 15274 RVA: 0x0014AA08 File Offset: 0x00148C08
	protected override void OnCleanUp()
	{
		this.AbortDelivery("ManualDeliverKG destroyed");
		Prioritizable.RemoveRef(base.gameObject);
		base.OnCleanUp();
	}

	// Token: 0x06003BAB RID: 15275 RVA: 0x0014AA28 File Offset: 0x00148C28
	public void SetStorage(Storage storage)
	{
		if (this.storage != null)
		{
			this.storage.Unsubscribe(this.onStorageChangeSubscription);
			this.onStorageChangeSubscription = -1;
		}
		this.AbortDelivery("storage pointer changed");
		this.storage = storage;
		if (this.storage != null && base.isSpawned)
		{
			global::Debug.Assert(this.onStorageChangeSubscription == -1);
			this.onStorageChangeSubscription = this.storage.Subscribe<ManualDeliveryKG>(-1697596308, ManualDeliveryKG.OnStorageChangedDelegate);
		}
	}

	// Token: 0x06003BAC RID: 15276 RVA: 0x0014AAAC File Offset: 0x00148CAC
	public void Pause(bool pause, string reason)
	{
		if (this.paused != pause)
		{
			this.paused = pause;
			if (pause)
			{
				this.AbortDelivery(reason);
			}
		}
	}

	// Token: 0x06003BAD RID: 15277 RVA: 0x0014AAC8 File Offset: 0x00148CC8
	public void ClearRequests()
	{
		for (int i = this.deliveryRequests.Count - 1; i >= 0; i--)
		{
			this.deliveryRequests[i].Reset();
			ManualDeliveryKG.requestPool.ReleaseInstance(this.deliveryRequests[i]);
			this.deliveryRequests.RemoveAt(i);
		}
	}

	// Token: 0x06003BAE RID: 15278 RVA: 0x0014AB20 File Offset: 0x00148D20
	public void RequestItem(Tag id, float minimumAmountKg)
	{
		this.RequestItemInternal(id, minimumAmountKg, null);
	}

	// Token: 0x06003BAF RID: 15279 RVA: 0x0014AB2B File Offset: 0x00148D2B
	public void RequestItem(Tag[] idSet, float minimumAmountKg)
	{
		this.RequestItemInternal(Tag.Invalid, minimumAmountKg, idSet);
	}

	// Token: 0x06003BB0 RID: 15280 RVA: 0x0014AB3C File Offset: 0x00148D3C
	private void RequestItemInternal(Tag id, float minimumAmountKg, Tag[] idSet = null)
	{
		ManualDeliveryKG.Request instance = ManualDeliveryKG.requestPool.GetInstance();
		instance.Id = id;
		instance.MinimumAmountKG = minimumAmountKg;
		int num = 0;
		while (idSet != null && num < idSet.Length)
		{
			instance.IdSet.Add(idSet[num]);
			num++;
		}
		this.deliveryRequests.Add(instance);
	}

	// Token: 0x06003BB1 RID: 15281 RVA: 0x0014AB92 File Offset: 0x00148D92
	public void Sim1000ms(float dt)
	{
		this.UpdateDeliveryState();
	}

	// Token: 0x06003BB2 RID: 15282 RVA: 0x0014AB9A File Offset: 0x00148D9A
	[ContextMenu("UpdateDeliveryState")]
	public void UpdateDeliveryState()
	{
		if (this.deliveryRequests == null || this.storage == null)
		{
			return;
		}
		this.UpdateFetchList();
	}

	// Token: 0x06003BB3 RID: 15283 RVA: 0x0014ABBC File Offset: 0x00148DBC
	private void CalculateDeliveryStats(out float storedMass, out float requestKG, out bool requiresRefill)
	{
		requestKG = 0f;
		storedMass = 0f;
		float num = 0f;
		float num2 = float.PositiveInfinity;
		for (int i = 0; i < this.deliveryRequests.Count; i++)
		{
			ManualDeliveryKG.Request request = this.deliveryRequests[i];
			if (request.IdSet.Count == 0)
			{
				request.LastStoredAmount = this.storage.GetMassAvailable(request.Id);
			}
			else
			{
				request.LastStoredAmount = 0f;
				foreach (Tag tag in request.IdSet)
				{
					request.LastStoredAmount += this.storage.GetMassAvailable(tag);
				}
			}
			if (request.LastStoredAmount < num2)
			{
				num = request.MinimumAmountKG;
				num2 = request.LastStoredAmount;
			}
			storedMass += request.LastStoredAmount;
			requestKG += request.MinimumAmountKG;
		}
		requiresRefill = storedMass <= this.refillMass;
		if (requestKG <= 0f)
		{
			return;
		}
		requiresRefill |= num2 <= num / requestKG * this.refillMass;
	}

	// Token: 0x06003BB4 RID: 15284 RVA: 0x0014ACF4 File Offset: 0x00148EF4
	private void RequestDeliveryInternal(float storedMass, float requestKG)
	{
		if (storedMass >= this.capacity)
		{
			return;
		}
		ChoreType byHash = Db.Get().ChoreTypes.GetByHash(this.choreTypeIDHash);
		this.fetchList = new FetchList2(this.storage, byHash);
		for (int i = 0; i < this.deliveryRequests.Count; i++)
		{
			ManualDeliveryKG.Request request = this.deliveryRequests[i];
			float num = this.capacity * (request.MinimumAmountKG / requestKG) - request.LastStoredAmount;
			if (num > Mathf.Epsilon)
			{
				if (this.RoundFetchAmountToInt)
				{
					num = (float)((int)num);
				}
				num = Mathf.Max(PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT, num);
				this.fetchList.MinimumAmount[request.Id] = Mathf.Max(PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT, request.MinimumAmountKG);
				if (request.IdSet.Count == 0)
				{
					FetchList2 fetchList = this.fetchList;
					Tag id = request.Id;
					float num2 = num;
					fetchList.Add(id, this.forbiddenTags, num2, Operational.State.None);
				}
				else
				{
					FetchList2 fetchList2 = this.fetchList;
					HashSet<Tag> idSet = request.IdSet;
					float num2 = num;
					fetchList2.Add(idSet, this.forbiddenTags, num2, Operational.State.None);
				}
			}
		}
		this.fetchList.ShowStatusItem = this.ShowStatusItem;
		this.fetchList.Submit(null, false);
	}

	// Token: 0x06003BB5 RID: 15285 RVA: 0x0014AE24 File Offset: 0x00149024
	public void RequestDelivery()
	{
		if (this.fetchList != null)
		{
			return;
		}
		float num;
		float num2;
		bool flag;
		this.CalculateDeliveryStats(out num, out num2, out flag);
		this.RequestDeliveryInternal(num, num2);
	}

	// Token: 0x06003BB6 RID: 15286 RVA: 0x0014AE50 File Offset: 0x00149050
	private void UpdateFetchList()
	{
		if (this.paused)
		{
			return;
		}
		if (this.fetchList != null && this.fetchList.IsComplete)
		{
			this.fetchList = null;
		}
		bool flag = this.fetchList != null;
		bool flag2 = this.operational != null && !this.operational.MeetsRequirements(this.operationalRequirement);
		if (flag2 && flag)
		{
			this.fetchList.Cancel("Operational requirements");
			this.fetchList = null;
		}
		if (flag || flag2)
		{
			return;
		}
		float num;
		float num2;
		bool flag3;
		this.CalculateDeliveryStats(out num, out num2, out flag3);
		if (flag3)
		{
			this.RequestDeliveryInternal(num, num2);
		}
	}

	// Token: 0x06003BB7 RID: 15287 RVA: 0x0014AEEC File Offset: 0x001490EC
	public void AbortDelivery(string reason)
	{
		if (this.fetchList != null)
		{
			FetchList2 fetchList = this.fetchList;
			this.fetchList = null;
			fetchList.Cancel(reason);
		}
	}

	// Token: 0x06003BB8 RID: 15288 RVA: 0x0014AF09 File Offset: 0x00149109
	protected void OnStorageChanged(object data)
	{
		this.UpdateDeliveryState();
	}

	// Token: 0x06003BB9 RID: 15289 RVA: 0x0014AF11 File Offset: 0x00149111
	private void OnPause()
	{
		this.userPaused = true;
		this.Pause(true, "Forbid manual delivery");
	}

	// Token: 0x06003BBA RID: 15290 RVA: 0x0014AF26 File Offset: 0x00149126
	private void OnResume()
	{
		this.userPaused = false;
		this.Pause(false, "Allow manual delivery");
	}

	// Token: 0x06003BBB RID: 15291 RVA: 0x0014AF3C File Offset: 0x0014913C
	private void OnRefreshUserMenu(object data)
	{
		if (!this.allowPause)
		{
			return;
		}
		KIconButtonMenu.ButtonInfo buttonInfo = ((!this.paused) ? new KIconButtonMenu.ButtonInfo("action_move_to_storage", UI.USERMENUACTIONS.MANUAL_DELIVERY.NAME, new System.Action(this.OnPause), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_DELIVERY.TOOLTIP, true) : new KIconButtonMenu.ButtonInfo("action_move_to_storage", UI.USERMENUACTIONS.MANUAL_DELIVERY.NAME_OFF, new System.Action(this.OnResume), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.MANUAL_DELIVERY.TOOLTIP_OFF, true));
		Game.Instance.userMenu.AddButton(base.gameObject, buttonInfo, 1f);
	}

	// Token: 0x06003BBC RID: 15292 RVA: 0x0014AFDE File Offset: 0x001491DE
	private void OnOperationalChanged(object data)
	{
		this.UpdateDeliveryState();
	}

	// Token: 0x040026DB RID: 9947
	private static ObjectPool<ManualDeliveryKG.Request> requestPool = new ObjectPool<ManualDeliveryKG.Request>(() => new ManualDeliveryKG.Request(), 64);

	// Token: 0x040026DC RID: 9948
	public float capacity = 100f;

	// Token: 0x040026DD RID: 9949
	public float refillMass = 10f;

	// Token: 0x040026DE RID: 9950
	public bool allowPause;

	// Token: 0x040026DF RID: 9951
	public bool RoundFetchAmountToInt;

	// Token: 0x040026E0 RID: 9952
	public HashedString choreTypeIDHash;

	// Token: 0x040026E1 RID: 9953
	public Operational.State operationalRequirement;

	// Token: 0x040026E2 RID: 9954
	[SerializeField]
	private float minimumMass = 10f;

	// Token: 0x040026E3 RID: 9955
	[SerializeField]
	private Storage storage;

	// Token: 0x040026E4 RID: 9956
	[SerializeField]
	private bool paused;

	// Token: 0x040026E5 RID: 9957
	[Serialize]
	private bool userPaused;

	// Token: 0x040026E6 RID: 9958
	[MyCmpGet]
	private Operational operational;

	// Token: 0x040026E7 RID: 9959
	public bool ShowStatusItem = true;

	// Token: 0x040026E8 RID: 9960
	[SerializeField]
	private List<ManualDeliveryKG.Request> deliveryRequests = new List<ManualDeliveryKG.Request>();

	// Token: 0x040026E9 RID: 9961
	private FetchList2 fetchList;

	// Token: 0x040026EA RID: 9962
	private Tag[] forbiddenTags;

	// Token: 0x040026EB RID: 9963
	private int onStorageChangeSubscription = -1;

	// Token: 0x040026EC RID: 9964
	private static readonly EventSystem.IntraObjectHandler<ManualDeliveryKG> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<ManualDeliveryKG>(delegate(ManualDeliveryKG component, object data)
	{
		component.OnRefreshUserMenu(data);
	});

	// Token: 0x040026ED RID: 9965
	private static readonly EventSystem.IntraObjectHandler<ManualDeliveryKG> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<ManualDeliveryKG>(delegate(ManualDeliveryKG component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x040026EE RID: 9966
	private static readonly EventSystem.IntraObjectHandler<ManualDeliveryKG> OnStorageChangedDelegate = new EventSystem.IntraObjectHandler<ManualDeliveryKG>(delegate(ManualDeliveryKG component, object data)
	{
		component.OnStorageChanged(data);
	});

	// Token: 0x02001560 RID: 5472
	[Serializable]
	public class Request
	{
		// Token: 0x060083BF RID: 33727 RVA: 0x002E82CA File Offset: 0x002E64CA
		public void Reset()
		{
			this.Id = Tag.Invalid;
			this.MinimumAmountKG = 0f;
			this.LastStoredAmount = 0f;
			this.IdSet.Clear();
		}

		// Token: 0x0400668A RID: 26250
		public Tag Id;

		// Token: 0x0400668B RID: 26251
		public HashSet<Tag> IdSet = new HashSet<Tag>();

		// Token: 0x0400668C RID: 26252
		public float MinimumAmountKG;

		// Token: 0x0400668D RID: 26253
		public float LastStoredAmount;
	}
}
