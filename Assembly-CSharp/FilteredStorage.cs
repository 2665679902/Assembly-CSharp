using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x020005B9 RID: 1465
public class FilteredStorage
{
	// Token: 0x06002448 RID: 9288 RVA: 0x000C4573 File Offset: 0x000C2773
	public void SetHasMeter(bool has_meter)
	{
		this.hasMeter = has_meter;
	}

	// Token: 0x06002449 RID: 9289 RVA: 0x000C457C File Offset: 0x000C277C
	public FilteredStorage(KMonoBehaviour root, Tag[] forbidden_tags, IUserControlledCapacity capacity_control, bool use_logic_meter, ChoreType fetch_chore_type)
	{
		this.root = root;
		this.forbiddenTags = forbidden_tags;
		this.capacityControl = capacity_control;
		this.useLogicMeter = use_logic_meter;
		this.choreType = fetch_chore_type;
		root.Subscribe(-1697596308, new Action<object>(this.OnStorageChanged));
		root.Subscribe(-543130682, new Action<object>(this.OnUserSettingsChanged));
		this.filterable = root.FindOrAdd<TreeFilterable>();
		TreeFilterable treeFilterable = this.filterable;
		treeFilterable.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Combine(treeFilterable.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
		this.storage = root.GetComponent<Storage>();
		this.storage.Subscribe(644822890, new Action<object>(this.OnOnlyFetchMarkedItemsSettingChanged));
		this.storage.Subscribe(-1852328367, new Action<object>(this.OnFunctionalChanged));
	}

	// Token: 0x0600244A RID: 9290 RVA: 0x000C4664 File Offset: 0x000C2864
	private void OnOnlyFetchMarkedItemsSettingChanged(object data)
	{
		this.OnFilterChanged(this.filterable.GetTags());
	}

	// Token: 0x0600244B RID: 9291 RVA: 0x000C4678 File Offset: 0x000C2878
	private void CreateMeter()
	{
		if (!this.hasMeter)
		{
			return;
		}
		this.meter = new MeterController(this.root.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_frame", "meter_level" });
	}

	// Token: 0x0600244C RID: 9292 RVA: 0x000C46C7 File Offset: 0x000C28C7
	private void CreateLogicMeter()
	{
		if (!this.hasMeter)
		{
			return;
		}
		this.logicMeter = new MeterController(this.root.GetComponent<KBatchedAnimController>(), "logicmeter_target", "logicmeter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, Array.Empty<string>());
	}

	// Token: 0x0600244D RID: 9293 RVA: 0x000C46FA File Offset: 0x000C28FA
	public void SetMeter(MeterController meter)
	{
		this.hasMeter = true;
		this.meter = meter;
		this.UpdateMeter();
	}

	// Token: 0x0600244E RID: 9294 RVA: 0x000C4710 File Offset: 0x000C2910
	public void CleanUp()
	{
		if (this.filterable != null)
		{
			TreeFilterable treeFilterable = this.filterable;
			treeFilterable.OnFilterChanged = (Action<HashSet<Tag>>)Delegate.Remove(treeFilterable.OnFilterChanged, new Action<HashSet<Tag>>(this.OnFilterChanged));
		}
		if (this.fetchList != null)
		{
			this.fetchList.Cancel("Parent destroyed");
		}
	}

	// Token: 0x0600244F RID: 9295 RVA: 0x000C476C File Offset: 0x000C296C
	public void FilterChanged()
	{
		if (this.hasMeter)
		{
			if (this.meter == null)
			{
				this.CreateMeter();
			}
			if (this.logicMeter == null && this.useLogicMeter)
			{
				this.CreateLogicMeter();
			}
		}
		this.OnFilterChanged(this.filterable.GetTags());
		this.UpdateMeter();
	}

	// Token: 0x06002450 RID: 9296 RVA: 0x000C47BC File Offset: 0x000C29BC
	private void OnUserSettingsChanged(object data)
	{
		this.OnFilterChanged(this.filterable.GetTags());
		this.UpdateMeter();
	}

	// Token: 0x06002451 RID: 9297 RVA: 0x000C47D5 File Offset: 0x000C29D5
	private void OnStorageChanged(object data)
	{
		if (this.fetchList == null)
		{
			this.OnFilterChanged(this.filterable.GetTags());
		}
		this.UpdateMeter();
	}

	// Token: 0x06002452 RID: 9298 RVA: 0x000C47F6 File Offset: 0x000C29F6
	private void OnFunctionalChanged(object data)
	{
		this.OnFilterChanged(this.filterable.GetTags());
	}

	// Token: 0x06002453 RID: 9299 RVA: 0x000C480C File Offset: 0x000C2A0C
	private void UpdateMeter()
	{
		float maxCapacityMinusStorageMargin = this.GetMaxCapacityMinusStorageMargin();
		float num = Mathf.Clamp01(this.GetAmountStored() / maxCapacityMinusStorageMargin);
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(num);
		}
	}

	// Token: 0x06002454 RID: 9300 RVA: 0x000C4844 File Offset: 0x000C2A44
	public bool IsFull()
	{
		float maxCapacityMinusStorageMargin = this.GetMaxCapacityMinusStorageMargin();
		float num = Mathf.Clamp01(this.GetAmountStored() / maxCapacityMinusStorageMargin);
		if (this.meter != null)
		{
			this.meter.SetPositionPercent(num);
		}
		return num >= 1f;
	}

	// Token: 0x06002455 RID: 9301 RVA: 0x000C4885 File Offset: 0x000C2A85
	private void OnFetchComplete()
	{
		this.OnFilterChanged(this.filterable.GetTags());
	}

	// Token: 0x06002456 RID: 9302 RVA: 0x000C4898 File Offset: 0x000C2A98
	private float GetMaxCapacity()
	{
		float num = this.storage.capacityKg;
		if (this.capacityControl != null)
		{
			num = Mathf.Min(num, this.capacityControl.UserMaxCapacity);
		}
		return num;
	}

	// Token: 0x06002457 RID: 9303 RVA: 0x000C48CC File Offset: 0x000C2ACC
	private float GetMaxCapacityMinusStorageMargin()
	{
		return this.GetMaxCapacity() - this.storage.storageFullMargin;
	}

	// Token: 0x06002458 RID: 9304 RVA: 0x000C48E0 File Offset: 0x000C2AE0
	private float GetAmountStored()
	{
		float num = this.storage.MassStored();
		if (this.capacityControl != null)
		{
			num = this.capacityControl.AmountStored;
		}
		return num;
	}

	// Token: 0x06002459 RID: 9305 RVA: 0x000C4910 File Offset: 0x000C2B10
	private bool IsFunctional()
	{
		Operational component = this.storage.GetComponent<Operational>();
		return component == null || component.IsFunctional;
	}

	// Token: 0x0600245A RID: 9306 RVA: 0x000C493C File Offset: 0x000C2B3C
	private void OnFilterChanged(HashSet<Tag> tags)
	{
		bool flag = tags != null && tags.Count != 0;
		if (this.fetchList != null)
		{
			this.fetchList.Cancel("");
			this.fetchList = null;
		}
		float maxCapacityMinusStorageMargin = this.GetMaxCapacityMinusStorageMargin();
		float amountStored = this.GetAmountStored();
		float num = Mathf.Max(0f, maxCapacityMinusStorageMargin - amountStored);
		if (num > 0f && flag && this.IsFunctional())
		{
			num = Mathf.Max(0f, this.GetMaxCapacity() - amountStored);
			this.fetchList = new FetchList2(this.storage, this.choreType);
			this.fetchList.ShowStatusItem = false;
			this.fetchList.Add(tags, this.forbiddenTags, num, Operational.State.Functional);
			this.fetchList.Submit(new System.Action(this.OnFetchComplete), false);
		}
	}

	// Token: 0x0600245B RID: 9307 RVA: 0x000C4A0A File Offset: 0x000C2C0A
	public void SetLogicMeter(bool on)
	{
		if (this.logicMeter != null)
		{
			this.logicMeter.SetPositionPercent(on ? 1f : 0f);
		}
	}

	// Token: 0x0600245C RID: 9308 RVA: 0x000C4A30 File Offset: 0x000C2C30
	public void AddForbiddenTag(Tag forbidden_tag)
	{
		if (this.forbiddenTags == null)
		{
			this.forbiddenTags = new Tag[0];
		}
		if (!this.forbiddenTags.Contains(forbidden_tag))
		{
			this.forbiddenTags = this.forbiddenTags.Append(forbidden_tag);
			this.OnFilterChanged(this.filterable.GetTags());
		}
	}

	// Token: 0x0600245D RID: 9309 RVA: 0x000C4A84 File Offset: 0x000C2C84
	public void RemoveForbiddenTag(Tag forbidden_tag)
	{
		if (this.forbiddenTags != null)
		{
			List<Tag> list = new List<Tag>(this.forbiddenTags);
			list.Remove(forbidden_tag);
			this.forbiddenTags = list.ToArray();
			this.OnFilterChanged(this.filterable.GetTags());
		}
	}

	// Token: 0x040014E3 RID: 5347
	public static readonly HashedString FULL_PORT_ID = "FULL";

	// Token: 0x040014E4 RID: 5348
	private KMonoBehaviour root;

	// Token: 0x040014E5 RID: 5349
	private FetchList2 fetchList;

	// Token: 0x040014E6 RID: 5350
	private IUserControlledCapacity capacityControl;

	// Token: 0x040014E7 RID: 5351
	private TreeFilterable filterable;

	// Token: 0x040014E8 RID: 5352
	private Storage storage;

	// Token: 0x040014E9 RID: 5353
	private MeterController meter;

	// Token: 0x040014EA RID: 5354
	private MeterController logicMeter;

	// Token: 0x040014EB RID: 5355
	private Tag[] forbiddenTags;

	// Token: 0x040014EC RID: 5356
	private bool hasMeter = true;

	// Token: 0x040014ED RID: 5357
	private bool useLogicMeter;

	// Token: 0x040014EE RID: 5358
	private ChoreType choreType;
}
