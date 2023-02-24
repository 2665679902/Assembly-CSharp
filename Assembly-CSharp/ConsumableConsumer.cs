using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using KSerialization;
using UnityEngine;

// Token: 0x020003A8 RID: 936
[AddComponentMenu("KMonoBehaviour/scripts/ConsumableConsumer")]
public class ConsumableConsumer : KMonoBehaviour
{
	// Token: 0x06001344 RID: 4932 RVA: 0x00066458 File Offset: 0x00064658
	[OnDeserialized]
	[Obsolete]
	private void OnDeserialized()
	{
		if (SaveLoader.Instance.GameInfo.IsVersionOlderThan(7, 29))
		{
			this.forbiddenTagSet = new HashSet<Tag>(this.forbiddenTags);
			this.forbiddenTags = null;
		}
	}

	// Token: 0x06001345 RID: 4933 RVA: 0x00066494 File Offset: 0x00064694
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (ConsumerManager.instance != null)
		{
			this.forbiddenTagSet = new HashSet<Tag>(ConsumerManager.instance.DefaultForbiddenTagsList);
			return;
		}
		this.forbiddenTagSet = new HashSet<Tag>();
	}

	// Token: 0x06001346 RID: 4934 RVA: 0x000664CC File Offset: 0x000646CC
	public bool IsPermitted(string consumable_id)
	{
		Tag tag = new Tag(consumable_id);
		return !this.forbiddenTagSet.Contains(tag);
	}

	// Token: 0x06001347 RID: 4935 RVA: 0x000664F0 File Offset: 0x000646F0
	public void SetPermitted(string consumable_id, bool is_allowed)
	{
		Tag tag = new Tag(consumable_id);
		if (is_allowed)
		{
			this.forbiddenTagSet.Remove(tag);
		}
		else
		{
			this.forbiddenTagSet.Add(tag);
		}
		this.consumableRulesChanged.Signal();
	}

	// Token: 0x04000A6F RID: 2671
	[Obsolete("Deprecated, use forbiddenTagSet")]
	[Serialize]
	public Tag[] forbiddenTags;

	// Token: 0x04000A70 RID: 2672
	[Serialize]
	public HashSet<Tag> forbiddenTagSet;

	// Token: 0x04000A71 RID: 2673
	public System.Action consumableRulesChanged;
}
