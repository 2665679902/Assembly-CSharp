using System;
using KSerialization;
using UnityEngine;

// Token: 0x020006C1 RID: 1729
[AddComponentMenu("KMonoBehaviour/scripts/BudUprootedMonitor")]
public class BudUprootedMonitor : KMonoBehaviour
{
	// Token: 0x1700034D RID: 845
	// (get) Token: 0x06002F0B RID: 12043 RVA: 0x000F8D95 File Offset: 0x000F6F95
	public bool IsUprooted
	{
		get
		{
			return this.uprooted || base.GetComponent<KPrefabID>().HasTag(GameTags.Uprooted);
		}
	}

	// Token: 0x06002F0C RID: 12044 RVA: 0x000F8DB1 File Offset: 0x000F6FB1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<BudUprootedMonitor>(-216549700, BudUprootedMonitor.OnUprootedDelegate);
	}

	// Token: 0x06002F0D RID: 12045 RVA: 0x000F8DCA File Offset: 0x000F6FCA
	public void SetParentObject(KPrefabID id)
	{
		this.parentObject = new Ref<KPrefabID>(id);
		base.Subscribe(id.gameObject, 1969584890, new Action<object>(this.OnLoseParent));
	}

	// Token: 0x06002F0E RID: 12046 RVA: 0x000F8DF6 File Offset: 0x000F6FF6
	private void OnLoseParent(object obj)
	{
		if (!this.uprooted && !base.isNull)
		{
			base.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			this.uprooted = true;
			base.Trigger(-216549700, null);
		}
	}

	// Token: 0x06002F0F RID: 12047 RVA: 0x000F8E2C File Offset: 0x000F702C
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}

	// Token: 0x06002F10 RID: 12048 RVA: 0x000F8E34 File Offset: 0x000F7034
	public static bool IsObjectUprooted(GameObject plant)
	{
		BudUprootedMonitor component = plant.GetComponent<BudUprootedMonitor>();
		return !(component == null) && component.IsUprooted;
	}

	// Token: 0x04001C4E RID: 7246
	[Serialize]
	public bool canBeUprooted = true;

	// Token: 0x04001C4F RID: 7247
	[Serialize]
	private bool uprooted;

	// Token: 0x04001C50 RID: 7248
	public Ref<KPrefabID> parentObject = new Ref<KPrefabID>();

	// Token: 0x04001C51 RID: 7249
	private HandleVector<int>.Handle partitionerEntry;

	// Token: 0x04001C52 RID: 7250
	private static readonly EventSystem.IntraObjectHandler<BudUprootedMonitor> OnUprootedDelegate = new EventSystem.IntraObjectHandler<BudUprootedMonitor>(delegate(BudUprootedMonitor component, object data)
	{
		if (!component.uprooted)
		{
			component.GetComponent<KPrefabID>().AddTag(GameTags.Uprooted, false);
			component.uprooted = true;
			component.Trigger(-216549700, null);
		}
	});
}
