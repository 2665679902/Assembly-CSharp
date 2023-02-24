using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000774 RID: 1908
public class FetchList2 : IFetchList
{
	// Token: 0x170003D2 RID: 978
	// (get) Token: 0x06003456 RID: 13398 RVA: 0x0011A43C File Offset: 0x0011863C
	// (set) Token: 0x06003457 RID: 13399 RVA: 0x0011A444 File Offset: 0x00118644
	public bool ShowStatusItem
	{
		get
		{
			return this.bShowStatusItem;
		}
		set
		{
			this.bShowStatusItem = value;
		}
	}

	// Token: 0x170003D3 RID: 979
	// (get) Token: 0x06003458 RID: 13400 RVA: 0x0011A44D File Offset: 0x0011864D
	public bool IsComplete
	{
		get
		{
			return this.FetchOrders.Count == 0;
		}
	}

	// Token: 0x170003D4 RID: 980
	// (get) Token: 0x06003459 RID: 13401 RVA: 0x0011A460 File Offset: 0x00118660
	public bool InProgress
	{
		get
		{
			if (this.FetchOrders.Count < 0)
			{
				return false;
			}
			bool flag = false;
			using (List<FetchOrder2>.Enumerator enumerator = this.FetchOrders.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.InProgress)
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}
	}

	// Token: 0x170003D5 RID: 981
	// (get) Token: 0x0600345A RID: 13402 RVA: 0x0011A4CC File Offset: 0x001186CC
	// (set) Token: 0x0600345B RID: 13403 RVA: 0x0011A4D4 File Offset: 0x001186D4
	public Storage Destination { get; private set; }

	// Token: 0x170003D6 RID: 982
	// (get) Token: 0x0600345C RID: 13404 RVA: 0x0011A4DD File Offset: 0x001186DD
	// (set) Token: 0x0600345D RID: 13405 RVA: 0x0011A4E5 File Offset: 0x001186E5
	public int PriorityMod { get; private set; }

	// Token: 0x0600345E RID: 13406 RVA: 0x0011A4F0 File Offset: 0x001186F0
	public FetchList2(Storage destination, ChoreType chore_type)
	{
		this.Destination = destination;
		this.choreType = chore_type;
	}

	// Token: 0x0600345F RID: 13407 RVA: 0x0011A55C File Offset: 0x0011875C
	public void SetPriorityMod(int priorityMod)
	{
		this.PriorityMod = priorityMod;
		for (int i = 0; i < this.FetchOrders.Count; i++)
		{
			this.FetchOrders[i].SetPriorityMod(this.PriorityMod);
		}
	}

	// Token: 0x06003460 RID: 13408 RVA: 0x0011A5A0 File Offset: 0x001187A0
	public void Add(HashSet<Tag> tags, Tag[] forbidden_tags = null, float amount = 1f, Operational.State operationalRequirementDEPRECATED = Operational.State.None)
	{
		foreach (Tag tag in tags)
		{
			if (!this.MinimumAmount.ContainsKey(tag))
			{
				this.MinimumAmount[tag] = amount;
			}
		}
		FetchOrder2 fetchOrder = new FetchOrder2(this.choreType, tags, FetchChore.MatchCriteria.MatchID, Tag.Invalid, forbidden_tags, this.Destination, amount, operationalRequirementDEPRECATED, this.PriorityMod);
		this.FetchOrders.Add(fetchOrder);
	}

	// Token: 0x06003461 RID: 13409 RVA: 0x0011A634 File Offset: 0x00118834
	public void Add(Tag tag, Tag[] forbidden_tags = null, float amount = 1f, Operational.State operationalRequirementDEPRECATED = Operational.State.None)
	{
		if (!this.MinimumAmount.ContainsKey(tag))
		{
			this.MinimumAmount[tag] = amount;
		}
		FetchOrder2 fetchOrder = new FetchOrder2(this.choreType, new HashSet<Tag> { tag }, FetchChore.MatchCriteria.MatchTags, Tag.Invalid, forbidden_tags, this.Destination, amount, operationalRequirementDEPRECATED, this.PriorityMod);
		this.FetchOrders.Add(fetchOrder);
	}

	// Token: 0x06003462 RID: 13410 RVA: 0x0011A698 File Offset: 0x00118898
	public float GetMinimumAmount(Tag tag)
	{
		float num = 0f;
		this.MinimumAmount.TryGetValue(tag, out num);
		return num;
	}

	// Token: 0x06003463 RID: 13411 RVA: 0x0011A6BB File Offset: 0x001188BB
	private void OnFetchOrderComplete(FetchOrder2 fetch_order, Pickupable fetched_item)
	{
		this.FetchOrders.Remove(fetch_order);
		if (this.FetchOrders.Count == 0)
		{
			if (this.OnComplete != null)
			{
				this.OnComplete();
			}
			FetchListStatusItemUpdater.instance.RemoveFetchList(this);
			this.ClearStatus();
		}
	}

	// Token: 0x06003464 RID: 13412 RVA: 0x0011A6FC File Offset: 0x001188FC
	public void Cancel(string reason)
	{
		FetchListStatusItemUpdater.instance.RemoveFetchList(this);
		this.ClearStatus();
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			fetchOrder.Cancel(reason);
		}
	}

	// Token: 0x06003465 RID: 13413 RVA: 0x0011A760 File Offset: 0x00118960
	public void UpdateRemaining()
	{
		this.Remaining.Clear();
		for (int i = 0; i < this.FetchOrders.Count; i++)
		{
			FetchOrder2 fetchOrder = this.FetchOrders[i];
			foreach (Tag tag in fetchOrder.Tags)
			{
				float num = 0f;
				this.Remaining.TryGetValue(tag, out num);
				this.Remaining[tag] = num + fetchOrder.AmountWaitingToFetch();
			}
		}
	}

	// Token: 0x06003466 RID: 13414 RVA: 0x0011A808 File Offset: 0x00118A08
	public Dictionary<Tag, float> GetRemaining()
	{
		return this.Remaining;
	}

	// Token: 0x06003467 RID: 13415 RVA: 0x0011A810 File Offset: 0x00118A10
	public Dictionary<Tag, float> GetRemainingMinimum()
	{
		Dictionary<Tag, float> dictionary = new Dictionary<Tag, float>();
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			foreach (Tag tag in fetchOrder.Tags)
			{
				dictionary[tag] = this.MinimumAmount[tag];
			}
		}
		foreach (GameObject gameObject in this.Destination.items)
		{
			if (gameObject != null)
			{
				Pickupable component = gameObject.GetComponent<Pickupable>();
				if (component != null)
				{
					KPrefabID kprefabID = component.KPrefabID;
					if (dictionary.ContainsKey(kprefabID.PrefabTag))
					{
						dictionary[kprefabID.PrefabTag] = Math.Max(dictionary[kprefabID.PrefabTag] - component.TotalAmount, 0f);
					}
					foreach (Tag tag2 in kprefabID.Tags)
					{
						if (dictionary.ContainsKey(tag2))
						{
							dictionary[tag2] = Math.Max(dictionary[tag2] - component.TotalAmount, 0f);
						}
					}
				}
			}
		}
		return dictionary;
	}

	// Token: 0x06003468 RID: 13416 RVA: 0x0011A9C8 File Offset: 0x00118BC8
	public void Suspend(string reason)
	{
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			fetchOrder.Suspend(reason);
		}
	}

	// Token: 0x06003469 RID: 13417 RVA: 0x0011AA1C File Offset: 0x00118C1C
	public void Resume(string reason)
	{
		foreach (FetchOrder2 fetchOrder in this.FetchOrders)
		{
			fetchOrder.Resume(reason);
		}
	}

	// Token: 0x0600346A RID: 13418 RVA: 0x0011AA70 File Offset: 0x00118C70
	public void Submit(System.Action on_complete, bool check_storage_contents)
	{
		this.OnComplete = on_complete;
		foreach (FetchOrder2 fetchOrder in this.FetchOrders.GetRange(0, this.FetchOrders.Count))
		{
			fetchOrder.Submit(new Action<FetchOrder2, Pickupable>(this.OnFetchOrderComplete), check_storage_contents, null);
		}
		if (!this.IsComplete && this.ShowStatusItem)
		{
			FetchListStatusItemUpdater.instance.AddFetchList(this);
		}
	}

	// Token: 0x0600346B RID: 13419 RVA: 0x0011AB04 File Offset: 0x00118D04
	private void ClearStatus()
	{
		if (this.Destination != null)
		{
			KSelectable component = this.Destination.GetComponent<KSelectable>();
			if (component != null)
			{
				this.waitingForMaterialsHandle = component.RemoveStatusItem(this.waitingForMaterialsHandle, false);
				this.materialsUnavailableHandle = component.RemoveStatusItem(this.materialsUnavailableHandle, false);
				this.materialsUnavailableForRefillHandle = component.RemoveStatusItem(this.materialsUnavailableForRefillHandle, false);
			}
		}
	}

	// Token: 0x0600346C RID: 13420 RVA: 0x0011AB70 File Offset: 0x00118D70
	public void UpdateStatusItem(MaterialsStatusItem status_item, ref Guid handle, bool should_add)
	{
		bool flag = handle != Guid.Empty;
		if (should_add != flag)
		{
			if (should_add)
			{
				KSelectable component = this.Destination.GetComponent<KSelectable>();
				if (component != null)
				{
					handle = component.AddStatusItem(status_item, this);
					GameScheduler.Instance.Schedule("Digging Tutorial", 2f, delegate(object obj)
					{
						Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Digging, true);
					}, null, null);
					return;
				}
			}
			else
			{
				KSelectable component2 = this.Destination.GetComponent<KSelectable>();
				if (component2 != null)
				{
					handle = component2.RemoveStatusItem(handle, false);
				}
			}
		}
	}

	// Token: 0x04002074 RID: 8308
	private System.Action OnComplete;

	// Token: 0x04002077 RID: 8311
	private ChoreType choreType;

	// Token: 0x04002078 RID: 8312
	public Guid waitingForMaterialsHandle = Guid.Empty;

	// Token: 0x04002079 RID: 8313
	public Guid materialsUnavailableForRefillHandle = Guid.Empty;

	// Token: 0x0400207A RID: 8314
	public Guid materialsUnavailableHandle = Guid.Empty;

	// Token: 0x0400207B RID: 8315
	public Dictionary<Tag, float> MinimumAmount = new Dictionary<Tag, float>();

	// Token: 0x0400207C RID: 8316
	public List<FetchOrder2> FetchOrders = new List<FetchOrder2>();

	// Token: 0x0400207D RID: 8317
	private Dictionary<Tag, float> Remaining = new Dictionary<Tag, float>();

	// Token: 0x0400207E RID: 8318
	private bool bShowStatusItem = true;
}
