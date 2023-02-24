using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x0200063F RID: 1599
[AddComponentMenu("KMonoBehaviour/Workable/SingleEntityReceptacle")]
public class SingleEntityReceptacle : Workable, IRender1000ms
{
	// Token: 0x170002D4 RID: 724
	// (get) Token: 0x06002A4E RID: 10830 RVA: 0x000DF913 File Offset: 0x000DDB13
	public FetchChore GetActiveRequest
	{
		get
		{
			return this.fetchChore;
		}
	}

	// Token: 0x170002D5 RID: 725
	// (get) Token: 0x06002A4F RID: 10831 RVA: 0x000DF91B File Offset: 0x000DDB1B
	// (set) Token: 0x06002A50 RID: 10832 RVA: 0x000DF942 File Offset: 0x000DDB42
	protected GameObject occupyingObject
	{
		get
		{
			if (this.occupyObjectRef.Get() != null)
			{
				return this.occupyObjectRef.Get().gameObject;
			}
			return null;
		}
		set
		{
			if (value == null)
			{
				this.occupyObjectRef.Set(null);
				return;
			}
			this.occupyObjectRef.Set(value.GetComponent<KSelectable>());
		}
	}

	// Token: 0x170002D6 RID: 726
	// (get) Token: 0x06002A51 RID: 10833 RVA: 0x000DF96B File Offset: 0x000DDB6B
	public GameObject Occupant
	{
		get
		{
			return this.occupyingObject;
		}
	}

	// Token: 0x170002D7 RID: 727
	// (get) Token: 0x06002A52 RID: 10834 RVA: 0x000DF973 File Offset: 0x000DDB73
	public IReadOnlyList<Tag> possibleDepositObjectTags
	{
		get
		{
			return this.possibleDepositTagsList;
		}
	}

	// Token: 0x06002A53 RID: 10835 RVA: 0x000DF97B File Offset: 0x000DDB7B
	public bool HasDepositTag(Tag tag)
	{
		return this.possibleDepositTagsList.Contains(tag);
	}

	// Token: 0x06002A54 RID: 10836 RVA: 0x000DF98C File Offset: 0x000DDB8C
	public bool IsValidEntity(GameObject candidate)
	{
		IReceptacleDirection component = candidate.GetComponent<IReceptacleDirection>();
		bool flag = this.rotatable != null || component == null || component.Direction == this.Direction;
		int num = 0;
		while (flag && num < this.additionalCriteria.Count)
		{
			flag = this.additionalCriteria[num](candidate);
			num++;
		}
		return flag;
	}

	// Token: 0x170002D8 RID: 728
	// (get) Token: 0x06002A55 RID: 10837 RVA: 0x000DF9F0 File Offset: 0x000DDBF0
	public SingleEntityReceptacle.ReceptacleDirection Direction
	{
		get
		{
			return this.direction;
		}
	}

	// Token: 0x06002A56 RID: 10838 RVA: 0x000DF9F8 File Offset: 0x000DDBF8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06002A57 RID: 10839 RVA: 0x000DFA00 File Offset: 0x000DDC00
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.occupyingObject != null)
		{
			this.PositionOccupyingObject();
			this.SubscribeToOccupant();
		}
		this.UpdateStatusItem();
		if (this.occupyingObject == null && !this.requestedEntityTag.IsValid)
		{
			this.requestedEntityAdditionalFilterTag = null;
		}
		if (this.occupyingObject == null && this.requestedEntityTag.IsValid)
		{
			this.CreateOrder(this.requestedEntityTag, this.requestedEntityAdditionalFilterTag);
		}
		base.Subscribe<SingleEntityReceptacle>(-592767678, SingleEntityReceptacle.OnOperationalChangedDelegate);
	}

	// Token: 0x06002A58 RID: 10840 RVA: 0x000DFA98 File Offset: 0x000DDC98
	public void AddDepositTag(Tag t)
	{
		this.possibleDepositTagsList.Add(t);
	}

	// Token: 0x06002A59 RID: 10841 RVA: 0x000DFAA6 File Offset: 0x000DDCA6
	public void AddAdditionalCriteria(Func<GameObject, bool> criteria)
	{
		this.additionalCriteria.Add(criteria);
	}

	// Token: 0x06002A5A RID: 10842 RVA: 0x000DFAB4 File Offset: 0x000DDCB4
	public void SetReceptacleDirection(SingleEntityReceptacle.ReceptacleDirection d)
	{
		this.direction = d;
	}

	// Token: 0x06002A5B RID: 10843 RVA: 0x000DFABD File Offset: 0x000DDCBD
	public virtual void SetPreview(Tag entityTag, bool solid = false)
	{
	}

	// Token: 0x06002A5C RID: 10844 RVA: 0x000DFABF File Offset: 0x000DDCBF
	public virtual void CreateOrder(Tag entityTag, Tag additionalFilterTag)
	{
		this.requestedEntityTag = entityTag;
		this.requestedEntityAdditionalFilterTag = additionalFilterTag;
		this.CreateFetchChore(this.requestedEntityTag, this.requestedEntityAdditionalFilterTag);
		this.SetPreview(entityTag, true);
		this.UpdateStatusItem();
	}

	// Token: 0x06002A5D RID: 10845 RVA: 0x000DFAEF File Offset: 0x000DDCEF
	public void Render1000ms(float dt)
	{
		this.UpdateStatusItem();
	}

	// Token: 0x06002A5E RID: 10846 RVA: 0x000DFAF8 File Offset: 0x000DDCF8
	protected void UpdateStatusItem()
	{
		KSelectable component = base.GetComponent<KSelectable>();
		if (this.Occupant != null)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, null, null);
			return;
		}
		if (this.fetchChore == null)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, this.statusItemNeed, null);
			return;
		}
		bool flag = this.fetchChore.fetcher != null;
		WorldContainer myWorld = this.GetMyWorld();
		if (!flag && myWorld != null)
		{
			foreach (Tag tag in this.fetchChore.tags)
			{
				if (myWorld.worldInventory.GetTotalAmount(tag, true) > 0f)
				{
					if (myWorld.worldInventory.GetTotalAmount(this.requestedEntityAdditionalFilterTag, true) > 0f || this.requestedEntityAdditionalFilterTag == Tag.Invalid)
					{
						flag = true;
						break;
					}
					break;
				}
			}
		}
		if (flag)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, this.statusItemAwaitingDelivery, null);
			return;
		}
		component.SetStatusItem(Db.Get().StatusItemCategories.EntityReceptacle, this.statusItemNoneAvailable, null);
	}

	// Token: 0x06002A5F RID: 10847 RVA: 0x000DFC4C File Offset: 0x000DDE4C
	protected void CreateFetchChore(Tag entityTag, Tag additionalRequiredTag)
	{
		if (this.fetchChore == null && entityTag.IsValid && entityTag != GameTags.Empty)
		{
			this.fetchChore = new FetchChore(this.choreType, this.storage, 1f, new HashSet<Tag> { entityTag }, FetchChore.MatchCriteria.MatchID, (additionalRequiredTag.IsValid && additionalRequiredTag != GameTags.Empty) ? additionalRequiredTag : Tag.Invalid, null, null, true, new Action<Chore>(this.OnFetchComplete), delegate(Chore chore)
			{
				this.UpdateStatusItem();
			}, delegate(Chore chore)
			{
				this.UpdateStatusItem();
			}, Operational.State.Functional, 0);
			MaterialNeeds.UpdateNeed(this.requestedEntityTag, 1f, base.gameObject.GetMyWorldId());
			this.UpdateStatusItem();
		}
	}

	// Token: 0x06002A60 RID: 10848 RVA: 0x000DFD12 File Offset: 0x000DDF12
	public virtual void OrderRemoveOccupant()
	{
		this.ClearOccupant();
	}

	// Token: 0x06002A61 RID: 10849 RVA: 0x000DFD1C File Offset: 0x000DDF1C
	protected virtual void ClearOccupant()
	{
		if (this.occupyingObject)
		{
			this.UnsubscribeFromOccupant();
			this.storage.DropAll(false, false, default(Vector3), true, null);
		}
		this.occupyingObject = null;
		this.UpdateActive();
		this.UpdateStatusItem();
		base.Trigger(-731304873, this.occupyingObject);
	}

	// Token: 0x06002A62 RID: 10850 RVA: 0x000DFD78 File Offset: 0x000DDF78
	public void CancelActiveRequest()
	{
		if (this.fetchChore != null)
		{
			MaterialNeeds.UpdateNeed(this.requestedEntityTag, -1f, base.gameObject.GetMyWorldId());
			this.fetchChore.Cancel("User canceled");
			this.fetchChore = null;
		}
		this.requestedEntityTag = Tag.Invalid;
		this.requestedEntityAdditionalFilterTag = Tag.Invalid;
		this.UpdateStatusItem();
		this.SetPreview(Tag.Invalid, false);
	}

	// Token: 0x06002A63 RID: 10851 RVA: 0x000DFDE8 File Offset: 0x000DDFE8
	private void OnOccupantDestroyed(object data)
	{
		this.occupyingObject = null;
		this.ClearOccupant();
		if (this.autoReplaceEntity && this.requestedEntityTag.IsValid && this.requestedEntityTag != GameTags.Empty)
		{
			this.CreateOrder(this.requestedEntityTag, this.requestedEntityAdditionalFilterTag);
		}
	}

	// Token: 0x06002A64 RID: 10852 RVA: 0x000DFE3B File Offset: 0x000DE03B
	protected virtual void SubscribeToOccupant()
	{
		if (this.occupyingObject != null)
		{
			base.Subscribe(this.occupyingObject, 1969584890, new Action<object>(this.OnOccupantDestroyed));
		}
	}

	// Token: 0x06002A65 RID: 10853 RVA: 0x000DFE69 File Offset: 0x000DE069
	protected virtual void UnsubscribeFromOccupant()
	{
		if (this.occupyingObject != null)
		{
			base.Unsubscribe(this.occupyingObject, 1969584890, new Action<object>(this.OnOccupantDestroyed));
		}
	}

	// Token: 0x06002A66 RID: 10854 RVA: 0x000DFE98 File Offset: 0x000DE098
	private void OnFetchComplete(Chore chore)
	{
		if (this.fetchChore == null)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} OnFetchComplete fetchChore null", new object[] { base.gameObject });
			return;
		}
		if (this.fetchChore.fetchTarget == null)
		{
			global::Debug.LogWarningFormat(base.gameObject, "{0} OnFetchComplete fetchChore.fetchTarget null", new object[] { base.gameObject });
			return;
		}
		this.OnDepositObject(this.fetchChore.fetchTarget.gameObject);
	}

	// Token: 0x06002A67 RID: 10855 RVA: 0x000DFF16 File Offset: 0x000DE116
	public void ForceDeposit(GameObject depositedObject)
	{
		if (this.occupyingObject != null)
		{
			this.ClearOccupant();
		}
		this.OnDepositObject(depositedObject);
	}

	// Token: 0x06002A68 RID: 10856 RVA: 0x000DFF34 File Offset: 0x000DE134
	private void OnDepositObject(GameObject depositedObject)
	{
		this.SetPreview(Tag.Invalid, false);
		MaterialNeeds.UpdateNeed(this.requestedEntityTag, -1f, base.gameObject.GetMyWorldId());
		KBatchedAnimController component = depositedObject.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.GetBatchInstanceData().ClearOverrideTransformMatrix();
		}
		this.occupyingObject = this.SpawnOccupyingObject(depositedObject);
		if (this.occupyingObject != null)
		{
			this.ConfigureOccupyingObject(this.occupyingObject);
			this.occupyingObject.SetActive(true);
			this.PositionOccupyingObject();
			this.SubscribeToOccupant();
		}
		else
		{
			global::Debug.LogWarning(base.gameObject.name + " EntityReceptacle did not spawn occupying entity.");
		}
		if (this.fetchChore != null)
		{
			this.fetchChore.Cancel("receptacle filled");
			this.fetchChore = null;
		}
		if (!this.autoReplaceEntity)
		{
			this.requestedEntityTag = Tag.Invalid;
			this.requestedEntityAdditionalFilterTag = Tag.Invalid;
		}
		this.UpdateActive();
		this.UpdateStatusItem();
		if (this.destroyEntityOnDeposit)
		{
			Util.KDestroyGameObject(depositedObject);
		}
		base.Trigger(-731304873, this.occupyingObject);
	}

	// Token: 0x06002A69 RID: 10857 RVA: 0x000E0046 File Offset: 0x000DE246
	protected virtual GameObject SpawnOccupyingObject(GameObject depositedEntity)
	{
		return depositedEntity;
	}

	// Token: 0x06002A6A RID: 10858 RVA: 0x000E0049 File Offset: 0x000DE249
	protected virtual void ConfigureOccupyingObject(GameObject source)
	{
	}

	// Token: 0x06002A6B RID: 10859 RVA: 0x000E004C File Offset: 0x000DE24C
	protected virtual void PositionOccupyingObject()
	{
		if (this.rotatable != null)
		{
			this.occupyingObject.transform.SetPosition(base.gameObject.transform.GetPosition() + this.rotatable.GetRotatedOffset(this.occupyingObjectRelativePosition));
		}
		else
		{
			this.occupyingObject.transform.SetPosition(base.gameObject.transform.GetPosition() + this.occupyingObjectRelativePosition);
		}
		KBatchedAnimController component = this.occupyingObject.GetComponent<KBatchedAnimController>();
		component.enabled = false;
		component.enabled = true;
	}

	// Token: 0x06002A6C RID: 10860 RVA: 0x000E00E4 File Offset: 0x000DE2E4
	private void UpdateActive()
	{
		if (this.Equals(null) || this == null || base.gameObject.Equals(null) || base.gameObject == null)
		{
			return;
		}
		if (this.operational != null)
		{
			this.operational.SetActive(this.operational.IsOperational && this.occupyingObject != null, false);
		}
	}

	// Token: 0x06002A6D RID: 10861 RVA: 0x000E0156 File Offset: 0x000DE356
	protected override void OnCleanUp()
	{
		this.CancelActiveRequest();
		this.UnsubscribeFromOccupant();
		base.OnCleanUp();
	}

	// Token: 0x06002A6E RID: 10862 RVA: 0x000E016A File Offset: 0x000DE36A
	private void OnOperationalChanged(object data)
	{
		this.UpdateActive();
		if (this.occupyingObject)
		{
			this.occupyingObject.Trigger(this.operational.IsOperational ? 1628751838 : 960378201, null);
		}
	}

	// Token: 0x04001905 RID: 6405
	[MyCmpGet]
	protected Operational operational;

	// Token: 0x04001906 RID: 6406
	[MyCmpReq]
	protected Storage storage;

	// Token: 0x04001907 RID: 6407
	[MyCmpGet]
	public Rotatable rotatable;

	// Token: 0x04001908 RID: 6408
	protected FetchChore fetchChore;

	// Token: 0x04001909 RID: 6409
	public ChoreType choreType = Db.Get().ChoreTypes.Fetch;

	// Token: 0x0400190A RID: 6410
	[Serialize]
	public bool autoReplaceEntity;

	// Token: 0x0400190B RID: 6411
	[Serialize]
	public Tag requestedEntityTag;

	// Token: 0x0400190C RID: 6412
	[Serialize]
	public Tag requestedEntityAdditionalFilterTag;

	// Token: 0x0400190D RID: 6413
	[Serialize]
	protected Ref<KSelectable> occupyObjectRef = new Ref<KSelectable>();

	// Token: 0x0400190E RID: 6414
	[SerializeField]
	private List<Tag> possibleDepositTagsList = new List<Tag>();

	// Token: 0x0400190F RID: 6415
	[SerializeField]
	private List<Func<GameObject, bool>> additionalCriteria = new List<Func<GameObject, bool>>();

	// Token: 0x04001910 RID: 6416
	[SerializeField]
	protected bool destroyEntityOnDeposit;

	// Token: 0x04001911 RID: 6417
	[SerializeField]
	protected SingleEntityReceptacle.ReceptacleDirection direction;

	// Token: 0x04001912 RID: 6418
	public Vector3 occupyingObjectRelativePosition = new Vector3(0f, 1f, 3f);

	// Token: 0x04001913 RID: 6419
	protected StatusItem statusItemAwaitingDelivery;

	// Token: 0x04001914 RID: 6420
	protected StatusItem statusItemNeed;

	// Token: 0x04001915 RID: 6421
	protected StatusItem statusItemNoneAvailable;

	// Token: 0x04001916 RID: 6422
	private static readonly EventSystem.IntraObjectHandler<SingleEntityReceptacle> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<SingleEntityReceptacle>(delegate(SingleEntityReceptacle component, object data)
	{
		component.OnOperationalChanged(data);
	});

	// Token: 0x020012D9 RID: 4825
	public enum ReceptacleDirection
	{
		// Token: 0x04005EE2 RID: 24290
		Top,
		// Token: 0x04005EE3 RID: 24291
		Side,
		// Token: 0x04005EE4 RID: 24292
		Bottom
	}
}
