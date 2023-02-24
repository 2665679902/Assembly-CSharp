using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020000C2 RID: 194
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/Plugins/KPrefabID")]
public class KPrefabID : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001ECC6 File Offset: 0x0001CEC6
	// (set) Token: 0x06000743 RID: 1859 RVA: 0x0001ECCD File Offset: 0x0001CECD
	public static int NextUniqueID
	{
		get
		{
			return KPrefabID.nextUniqueID;
		}
		set
		{
			KPrefabID.nextUniqueID = value;
		}
	}

	// Token: 0x14000024 RID: 36
	// (add) Token: 0x06000744 RID: 1860 RVA: 0x0001ECD8 File Offset: 0x0001CED8
	// (remove) Token: 0x06000745 RID: 1861 RVA: 0x0001ED10 File Offset: 0x0001CF10
	public event KPrefabID.PrefabFn instantiateFn;

	// Token: 0x14000025 RID: 37
	// (add) Token: 0x06000746 RID: 1862 RVA: 0x0001ED48 File Offset: 0x0001CF48
	// (remove) Token: 0x06000747 RID: 1863 RVA: 0x0001ED80 File Offset: 0x0001CF80
	public event KPrefabID.PrefabFn prefabInitFn;

	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06000748 RID: 1864 RVA: 0x0001EDB8 File Offset: 0x0001CFB8
	// (remove) Token: 0x06000749 RID: 1865 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
	public event KPrefabID.PrefabFn prefabSpawnFn;

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001EE25 File Offset: 0x0001D025
	// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001EE2D File Offset: 0x0001D02D
	public bool pendingDestruction { get; private set; }

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001EE36 File Offset: 0x0001D036
	// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001EE3E File Offset: 0x0001D03E
	public bool conflicted { get; private set; }

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001EE47 File Offset: 0x0001D047
	public HashSet<Tag> Tags
	{
		get
		{
			DebugUtil.DevAssert(this.initialized, "This object is has not been initialized, Tags is not valid. Is it an inactive prefab?", null);
			return this.tags;
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x0001EE60 File Offset: 0x0001D060
	public Tag PrefabID()
	{
		return this.PrefabTag;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x0001EE68 File Offset: 0x0001D068
	public bool IsPrefabID(Tag prefab_id)
	{
		return this.PrefabTag == prefab_id;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x0001EE78 File Offset: 0x0001D078
	public bool IsAnyPrefabID(Tag[] ids)
	{
		for (int i = 0; i < ids.Length; i++)
		{
			if (this.PrefabTag == ids[i])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x0001EEAC File Offset: 0x0001D0AC
	public void CopyTags(KPrefabID other)
	{
		foreach (Tag tag in other.tags)
		{
			this.tags.Add(tag);
		}
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x0001EF08 File Offset: 0x0001D108
	public void CopyInitFunctions(KPrefabID other)
	{
		this.instantiateFn = other.instantiateFn;
		this.prefabInitFn = other.prefabInitFn;
		this.prefabSpawnFn = other.prefabSpawnFn;
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x0001EF2E File Offset: 0x0001D12E
	public void RunInstantiateFn()
	{
		if (this.instantiateFn != null)
		{
			this.instantiateFn(base.gameObject);
			this.instantiateFn = null;
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x0001EF50 File Offset: 0x0001D150
	private void ValidateTags()
	{
		DebugUtil.Assert(this.PrefabTag.IsValid);
		foreach (Tag tag in this.serializedTags)
		{
			global::Debug.Assert(this.tags.Contains(tag), string.Format("serialized tag {0} is not contained in tags", tag));
		}
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
	public void InitializeTags(bool force_initialize = false)
	{
		if (this.initialized && !force_initialize)
		{
			return;
		}
		foreach (Tag tag in this.serializedTags)
		{
			if (this.tags.Add(tag))
			{
				this.dirtyTagBits = true;
			}
		}
		this.initialized = true;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x0001F044 File Offset: 0x0001D244
	public void UpdateSaveLoadTag()
	{
		this.SaveLoadTag = new Tag(this.PrefabTag.Name);
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x0001F05C File Offset: 0x0001D25C
	public Tag GetSaveLoadTag()
	{
		return this.SaveLoadTag;
	}

	// Token: 0x06000759 RID: 1881 RVA: 0x0001F064 File Offset: 0x0001D264
	private void LaunderTagBits()
	{
		if (!this.dirtyTagBits)
		{
			return;
		}
		this.tagBits.ClearAll();
		foreach (Tag tag in this.tags)
		{
			this.tagBits.SetTag(tag);
		}
		this.dirtyTagBits = false;
	}

	// Token: 0x0600075A RID: 1882 RVA: 0x0001F0D8 File Offset: 0x0001D2D8
	public void UpdateTagBits()
	{
		this.InitializeTags(false);
		this.LaunderTagBits();
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x0001F0E7 File Offset: 0x0001D2E7
	public void AndTagBits(ref TagBits rhs)
	{
		this.UpdateTagBits();
		rhs.And(ref this.tagBits);
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x0001F0FC File Offset: 0x0001D2FC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<KPrefabID>(1969584890, KPrefabID.OnObjectDestroyedDelegate);
		this.InitializeTags(true);
		if (this.prefabInitFn != null)
		{
			this.prefabInitFn(base.gameObject);
			this.prefabInitFn = null;
		}
		IStateMachineControllerHack component = base.GetComponent<IStateMachineControllerHack>();
		if (component != null)
		{
			component.CreateSMIS();
		}
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x0001F158 File Offset: 0x0001D358
	protected override void OnSpawn()
	{
		this.InitializeTags(true);
		IStateMachineControllerHack component = base.GetComponent<IStateMachineControllerHack>();
		if (component != null)
		{
			component.StartSMIS();
		}
		if (this.prefabSpawnFn != null)
		{
			this.prefabSpawnFn(base.gameObject);
			this.prefabSpawnFn = null;
		}
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x0001F19C File Offset: 0x0001D39C
	protected override void OnCmpEnable()
	{
		this.InitializeTags(true);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x0001F1A8 File Offset: 0x0001D3A8
	public void AddTag(Tag tag, bool serialize = false)
	{
		DebugUtil.Assert(tag.IsValid);
		if (this.tags.Add(tag))
		{
			this.dirtyTagBits = true;
			base.Trigger(-1582839653, new TagChangedEventData(tag, true));
		}
		if (serialize)
		{
			this.serializedTags.Add(tag);
		}
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x0001F1FD File Offset: 0x0001D3FD
	public void RemoveTag(Tag tag)
	{
		if (this.tags.Remove(tag))
		{
			this.dirtyTagBits = true;
			base.Trigger(-1582839653, new TagChangedEventData(tag, false));
		}
		this.serializedTags.Remove(tag);
	}

	// Token: 0x06000761 RID: 1889 RVA: 0x0001F238 File Offset: 0x0001D438
	public void SetTag(Tag tag, bool set)
	{
		if (set)
		{
			this.AddTag(tag, false);
			return;
		}
		this.RemoveTag(tag);
	}

	// Token: 0x06000762 RID: 1890 RVA: 0x0001F24D File Offset: 0x0001D44D
	public bool HasTag(Tag tag)
	{
		return this.PrefabTag == tag || this.tags.Contains(tag);
	}

	// Token: 0x06000763 RID: 1891 RVA: 0x0001F26C File Offset: 0x0001D46C
	public bool HasAnyTags(List<Tag> search_tags)
	{
		for (int i = 0; i < search_tags.Count; i++)
		{
			Tag tag = search_tags[i];
			if (this.PrefabTag == tag || this.tags.Contains(tag))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000764 RID: 1892 RVA: 0x0001F2B4 File Offset: 0x0001D4B4
	public bool HasAnyTags(Tag[] search_tags)
	{
		foreach (Tag tag in search_tags)
		{
			if (this.PrefabTag == tag || this.tags.Contains(tag))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000765 RID: 1893 RVA: 0x0001F2F6 File Offset: 0x0001D4F6
	public bool HasAnyTags(ref TagBits search_tags)
	{
		this.UpdateTagBits();
		return this.tagBits.HasAny(ref search_tags);
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x0001F30C File Offset: 0x0001D50C
	public bool HasAllTags(List<Tag> search_tags)
	{
		for (int i = 0; i < search_tags.Count; i++)
		{
			Tag tag = search_tags[i];
			if (this.PrefabTag != tag && !this.tags.Contains(tag))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x0001F354 File Offset: 0x0001D554
	public bool HasAllTags(Tag[] search_tags)
	{
		foreach (Tag tag in search_tags)
		{
			if (this.PrefabTag != tag && !this.tags.Contains(tag))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x0001F398 File Offset: 0x0001D598
	public override bool Equals(object o)
	{
		KPrefabID kprefabID = o as KPrefabID;
		return kprefabID != null && this.PrefabTag == kprefabID.PrefabTag;
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
	public override int GetHashCode()
	{
		return this.PrefabTag.GetHashCode();
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0001F3DB File Offset: 0x0001D5DB
	public static int GetUniqueID()
	{
		return KPrefabID.NextUniqueID++;
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x0001F3EA File Offset: 0x0001D5EA
	public string GetDebugName()
	{
		return base.name + "(" + this.InstanceID.ToString() + ")";
	}

	// Token: 0x0600076C RID: 1900 RVA: 0x0001F40C File Offset: 0x0001D60C
	protected override void OnCleanUp()
	{
		this.pendingDestruction = true;
		if (this.InstanceID != -1)
		{
			KPrefabIDTracker.Get().Unregister(this);
		}
		base.Trigger(1969584890, null);
	}

	// Token: 0x0600076D RID: 1901 RVA: 0x0001F435 File Offset: 0x0001D635
	[OnDeserialized]
	internal void OnDeserializedMethod()
	{
		this.InitializeTags(true);
		KPrefabIDTracker kprefabIDTracker = KPrefabIDTracker.Get();
		if (kprefabIDTracker.GetInstance(this.InstanceID))
		{
			this.conflicted = true;
		}
		kprefabIDTracker.Register(this);
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x0001F463 File Offset: 0x0001D663
	private void OnObjectDestroyed(object data)
	{
		this.pendingDestruction = true;
	}

	// Token: 0x040005E8 RID: 1512
	public const int InvalidInstanceID = -1;

	// Token: 0x040005E9 RID: 1513
	private static int nextUniqueID = 0;

	// Token: 0x040005EA RID: 1514
	[ReadOnly]
	public Tag SaveLoadTag;

	// Token: 0x040005EB RID: 1515
	public Tag PrefabTag;

	// Token: 0x040005EC RID: 1516
	private TagBits tagBits;

	// Token: 0x040005ED RID: 1517
	private bool initialized;

	// Token: 0x040005EE RID: 1518
	private bool dirtyTagBits = true;

	// Token: 0x040005EF RID: 1519
	[Serialize]
	public int InstanceID;

	// Token: 0x040005F3 RID: 1523
	public int defaultLayer;

	// Token: 0x040005F4 RID: 1524
	public List<Descriptor> AdditionalRequirements;

	// Token: 0x040005F5 RID: 1525
	public List<Descriptor> AdditionalEffects;

	// Token: 0x040005F8 RID: 1528
	[Serialize]
	private HashSet<Tag> serializedTags = new HashSet<Tag>();

	// Token: 0x040005F9 RID: 1529
	private HashSet<Tag> tags = new HashSet<Tag>();

	// Token: 0x040005FA RID: 1530
	private static readonly EventSystem.IntraObjectHandler<KPrefabID> OnObjectDestroyedDelegate = new EventSystem.IntraObjectHandler<KPrefabID>(delegate(KPrefabID component, object data)
	{
		component.OnObjectDestroyed(data);
	});

	// Token: 0x020009EA RID: 2538
	// (Invoke) Token: 0x060053C9 RID: 21449
	public delegate void PrefabFn(GameObject go);
}
