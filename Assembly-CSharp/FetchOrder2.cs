using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000777 RID: 1911
public class FetchOrder2
{
	// Token: 0x170003D7 RID: 983
	// (get) Token: 0x06003485 RID: 13445 RVA: 0x0011B7FD File Offset: 0x001199FD
	// (set) Token: 0x06003486 RID: 13446 RVA: 0x0011B805 File Offset: 0x00119A05
	public float TotalAmount { get; set; }

	// Token: 0x170003D8 RID: 984
	// (get) Token: 0x06003487 RID: 13447 RVA: 0x0011B80E File Offset: 0x00119A0E
	// (set) Token: 0x06003488 RID: 13448 RVA: 0x0011B816 File Offset: 0x00119A16
	public int PriorityMod { get; set; }

	// Token: 0x170003D9 RID: 985
	// (get) Token: 0x06003489 RID: 13449 RVA: 0x0011B81F File Offset: 0x00119A1F
	// (set) Token: 0x0600348A RID: 13450 RVA: 0x0011B827 File Offset: 0x00119A27
	public HashSet<Tag> Tags { get; protected set; }

	// Token: 0x170003DA RID: 986
	// (get) Token: 0x0600348B RID: 13451 RVA: 0x0011B830 File Offset: 0x00119A30
	// (set) Token: 0x0600348C RID: 13452 RVA: 0x0011B838 File Offset: 0x00119A38
	public FetchChore.MatchCriteria Criteria { get; protected set; }

	// Token: 0x170003DB RID: 987
	// (get) Token: 0x0600348D RID: 13453 RVA: 0x0011B841 File Offset: 0x00119A41
	// (set) Token: 0x0600348E RID: 13454 RVA: 0x0011B849 File Offset: 0x00119A49
	public Tag RequiredTag { get; protected set; }

	// Token: 0x170003DC RID: 988
	// (get) Token: 0x0600348F RID: 13455 RVA: 0x0011B852 File Offset: 0x00119A52
	// (set) Token: 0x06003490 RID: 13456 RVA: 0x0011B85A File Offset: 0x00119A5A
	public Tag[] ForbiddenTags { get; protected set; }

	// Token: 0x170003DD RID: 989
	// (get) Token: 0x06003491 RID: 13457 RVA: 0x0011B863 File Offset: 0x00119A63
	// (set) Token: 0x06003492 RID: 13458 RVA: 0x0011B86B File Offset: 0x00119A6B
	public Storage Destination { get; set; }

	// Token: 0x170003DE RID: 990
	// (get) Token: 0x06003493 RID: 13459 RVA: 0x0011B874 File Offset: 0x00119A74
	// (set) Token: 0x06003494 RID: 13460 RVA: 0x0011B87C File Offset: 0x00119A7C
	private float UnfetchedAmount
	{
		get
		{
			return this._UnfetchedAmount;
		}
		set
		{
			this._UnfetchedAmount = value;
			this.Assert(this._UnfetchedAmount <= this.TotalAmount, "_UnfetchedAmount <= TotalAmount");
			this.Assert(this._UnfetchedAmount >= 0f, "_UnfetchedAmount >= 0");
		}
	}

	// Token: 0x06003495 RID: 13461 RVA: 0x0011B8BC File Offset: 0x00119ABC
	public FetchOrder2(ChoreType chore_type, HashSet<Tag> tags, FetchChore.MatchCriteria criteria, Tag required_tag, Tag[] forbidden_tags, Storage destination, float amount, Operational.State operationalRequirementDEPRECATED = Operational.State.None, int priorityMod = 0)
	{
		if (amount <= PICKUPABLETUNING.MINIMUM_PICKABLE_AMOUNT)
		{
			DebugUtil.LogWarningArgs(new object[] { string.Format("FetchOrder2 {0} is requesting {1} {2} to {3}", new object[]
			{
				chore_type.Id,
				tags,
				amount,
				(destination != null) ? destination.name : "to nowhere"
			}) });
		}
		this.choreType = chore_type;
		this.Tags = tags;
		this.Criteria = criteria;
		this.RequiredTag = required_tag;
		this.ForbiddenTags = forbidden_tags;
		this.Destination = destination;
		this.TotalAmount = amount;
		this.UnfetchedAmount = amount;
		this.PriorityMod = priorityMod;
		this.operationalRequirement = operationalRequirementDEPRECATED;
	}

	// Token: 0x170003DF RID: 991
	// (get) Token: 0x06003496 RID: 13462 RVA: 0x0011B988 File Offset: 0x00119B88
	public bool InProgress
	{
		get
		{
			bool flag = false;
			using (List<FetchChore>.Enumerator enumerator = this.Chores.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.InProgress())
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}
	}

	// Token: 0x06003497 RID: 13463 RVA: 0x0011B9E4 File Offset: 0x00119BE4
	private void IssueTask()
	{
		if (this.UnfetchedAmount > 0f)
		{
			this.SetFetchTask(this.UnfetchedAmount);
			this.UnfetchedAmount = 0f;
		}
	}

	// Token: 0x06003498 RID: 13464 RVA: 0x0011BA0C File Offset: 0x00119C0C
	public void SetPriorityMod(int priorityMod)
	{
		this.PriorityMod = priorityMod;
		for (int i = 0; i < this.Chores.Count; i++)
		{
			this.Chores[i].SetPriorityMod(this.PriorityMod);
		}
	}

	// Token: 0x06003499 RID: 13465 RVA: 0x0011BA50 File Offset: 0x00119C50
	private void SetFetchTask(float amount)
	{
		FetchChore fetchChore = new FetchChore(this.choreType, this.Destination, amount, this.Tags, this.Criteria, this.RequiredTag, this.ForbiddenTags, null, true, new Action<Chore>(this.OnFetchChoreComplete), new Action<Chore>(this.OnFetchChoreBegin), new Action<Chore>(this.OnFetchChoreEnd), this.operationalRequirement, this.PriorityMod);
		this.Chores.Add(fetchChore);
	}

	// Token: 0x0600349A RID: 13466 RVA: 0x0011BAC8 File Offset: 0x00119CC8
	private void OnFetchChoreEnd(Chore chore)
	{
		FetchChore fetchChore = (FetchChore)chore;
		if (this.Chores.Contains(fetchChore))
		{
			this.UnfetchedAmount += fetchChore.amount;
			fetchChore.Cancel("FetchChore Redistribution");
			this.Chores.Remove(fetchChore);
			this.IssueTask();
		}
	}

	// Token: 0x0600349B RID: 13467 RVA: 0x0011BB1C File Offset: 0x00119D1C
	private void OnFetchChoreComplete(Chore chore)
	{
		FetchChore fetchChore = (FetchChore)chore;
		this.Chores.Remove(fetchChore);
		if (this.Chores.Count == 0 && this.OnComplete != null)
		{
			this.OnComplete(this, fetchChore.fetchTarget);
		}
	}

	// Token: 0x0600349C RID: 13468 RVA: 0x0011BB64 File Offset: 0x00119D64
	private void OnFetchChoreBegin(Chore chore)
	{
		FetchChore fetchChore = (FetchChore)chore;
		this.UnfetchedAmount += fetchChore.originalAmount - fetchChore.amount;
		this.IssueTask();
		if (this.OnBegin != null)
		{
			this.OnBegin(this, fetchChore.fetchTarget);
		}
	}

	// Token: 0x0600349D RID: 13469 RVA: 0x0011BBB4 File Offset: 0x00119DB4
	public void Cancel(string reason)
	{
		while (this.Chores.Count > 0)
		{
			FetchChore fetchChore = this.Chores[0];
			fetchChore.Cancel(reason);
			this.Chores.Remove(fetchChore);
		}
	}

	// Token: 0x0600349E RID: 13470 RVA: 0x0011BBF2 File Offset: 0x00119DF2
	public void Suspend(string reason)
	{
		global::Debug.LogError("UNIMPLEMENTED!");
	}

	// Token: 0x0600349F RID: 13471 RVA: 0x0011BBFE File Offset: 0x00119DFE
	public void Resume(string reason)
	{
		global::Debug.LogError("UNIMPLEMENTED!");
	}

	// Token: 0x060034A0 RID: 13472 RVA: 0x0011BC0C File Offset: 0x00119E0C
	public void Submit(Action<FetchOrder2, Pickupable> on_complete, bool check_storage_contents, Action<FetchOrder2, Pickupable> on_begin = null)
	{
		this.OnComplete = on_complete;
		this.OnBegin = on_begin;
		this.checkStorageContents = check_storage_contents;
		if (check_storage_contents)
		{
			Pickupable pickupable = null;
			this.UnfetchedAmount = this.GetRemaining(out pickupable);
			if (this.UnfetchedAmount > this.Destination.storageFullMargin)
			{
				this.IssueTask();
				return;
			}
			if (this.OnComplete != null)
			{
				this.OnComplete(this, pickupable);
				return;
			}
		}
		else
		{
			this.IssueTask();
		}
	}

	// Token: 0x060034A1 RID: 13473 RVA: 0x0011BC78 File Offset: 0x00119E78
	public bool IsMaterialOnStorage(Storage storage, ref float amount, ref Pickupable out_item)
	{
		foreach (GameObject gameObject in this.Destination.items)
		{
			if (gameObject != null)
			{
				Pickupable component = gameObject.GetComponent<Pickupable>();
				if (component != null)
				{
					KPrefabID kprefabID = component.KPrefabID;
					foreach (Tag tag in this.Tags)
					{
						if (kprefabID.HasTag(tag))
						{
							amount = component.TotalAmount;
							out_item = component;
							return true;
						}
					}
				}
			}
		}
		return false;
	}

	// Token: 0x060034A2 RID: 13474 RVA: 0x0011BD44 File Offset: 0x00119F44
	public float AmountWaitingToFetch()
	{
		if (!this.checkStorageContents)
		{
			float num = this.UnfetchedAmount;
			for (int i = 0; i < this.Chores.Count; i++)
			{
				num += this.Chores[i].AmountWaitingToFetch();
			}
			return num;
		}
		Pickupable pickupable;
		return this.GetRemaining(out pickupable);
	}

	// Token: 0x060034A3 RID: 13475 RVA: 0x0011BD94 File Offset: 0x00119F94
	public float GetRemaining(out Pickupable out_item)
	{
		float num = this.TotalAmount;
		float num2 = 0f;
		out_item = null;
		if (this.IsMaterialOnStorage(this.Destination, ref num2, ref out_item))
		{
			num = Math.Max(num - num2, 0f);
		}
		return num;
	}

	// Token: 0x060034A4 RID: 13476 RVA: 0x0011BDD4 File Offset: 0x00119FD4
	public bool IsComplete()
	{
		for (int i = 0; i < this.Chores.Count; i++)
		{
			if (!this.Chores[i].isComplete)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060034A5 RID: 13477 RVA: 0x0011BE10 File Offset: 0x0011A010
	private void Assert(bool condition, string message)
	{
		if (condition)
		{
			return;
		}
		string text = "FetchOrder error: " + message;
		if (this.Destination == null)
		{
			text += "\nDestination: None";
		}
		else
		{
			text = text + "\nDestination: " + this.Destination.name;
		}
		text = text + "\nTotal Amount: " + this.TotalAmount.ToString();
		text = text + "\nUnfetched Amount: " + this._UnfetchedAmount.ToString();
		global::Debug.LogError(text);
	}

	// Token: 0x0400208A RID: 8330
	public Action<FetchOrder2, Pickupable> OnComplete;

	// Token: 0x0400208B RID: 8331
	public Action<FetchOrder2, Pickupable> OnBegin;

	// Token: 0x04002093 RID: 8339
	public List<FetchChore> Chores = new List<FetchChore>();

	// Token: 0x04002094 RID: 8340
	private ChoreType choreType;

	// Token: 0x04002095 RID: 8341
	private float _UnfetchedAmount;

	// Token: 0x04002096 RID: 8342
	private bool checkStorageContents;

	// Token: 0x04002097 RID: 8343
	private Operational.State operationalRequirement = Operational.State.None;
}
