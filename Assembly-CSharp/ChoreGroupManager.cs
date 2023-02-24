using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000682 RID: 1666
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/ChoreGroupManager")]
public class ChoreGroupManager : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06002CDC RID: 11484 RVA: 0x000EB645 File Offset: 0x000E9845
	public static void DestroyInstance()
	{
		ChoreGroupManager.instance = null;
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06002CDD RID: 11485 RVA: 0x000EB64D File Offset: 0x000E984D
	public List<Tag> DefaultForbiddenTagsList
	{
		get
		{
			return this.defaultForbiddenTagsList;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000EB655 File Offset: 0x000E9855
	public Dictionary<Tag, int> DefaultChorePermission
	{
		get
		{
			return this.defaultChorePermissions;
		}
	}

	// Token: 0x06002CDF RID: 11487 RVA: 0x000EB660 File Offset: 0x000E9860
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ChoreGroupManager.instance = this;
		this.ConvertOldVersion();
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			if (!this.defaultChorePermissions.ContainsKey(choreGroup.Id.ToTag()))
			{
				this.defaultChorePermissions.Add(choreGroup.Id.ToTag(), 2);
			}
		}
	}

	// Token: 0x06002CE0 RID: 11488 RVA: 0x000EB6F8 File Offset: 0x000E98F8
	private void ConvertOldVersion()
	{
		foreach (Tag tag in this.defaultForbiddenTagsList)
		{
			if (!this.defaultChorePermissions.ContainsKey(tag))
			{
				this.defaultChorePermissions.Add(tag, -1);
			}
			this.defaultChorePermissions[tag] = 0;
		}
		this.defaultForbiddenTagsList.Clear();
	}

	// Token: 0x04001ACD RID: 6861
	public static ChoreGroupManager instance;

	// Token: 0x04001ACE RID: 6862
	[Serialize]
	private List<Tag> defaultForbiddenTagsList = new List<Tag>();

	// Token: 0x04001ACF RID: 6863
	[Serialize]
	private Dictionary<Tag, int> defaultChorePermissions = new Dictionary<Tag, int>();
}
