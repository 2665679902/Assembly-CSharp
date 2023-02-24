using System;
using UnityEngine;

// Token: 0x02000A50 RID: 2640
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/CharacterOverlay")]
public class CharacterOverlay : KMonoBehaviour
{
	// Token: 0x0600502B RID: 20523 RVA: 0x001CB3F1 File Offset: 0x001C95F1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Register();
	}

	// Token: 0x0600502C RID: 20524 RVA: 0x001CB3FF File Offset: 0x001C95FF
	public void Register()
	{
		if (this.registered)
		{
			return;
		}
		this.registered = true;
		NameDisplayScreen.Instance.AddNewEntry(base.gameObject);
	}

	// Token: 0x040035D9 RID: 13785
	public bool shouldShowName;

	// Token: 0x040035DA RID: 13786
	private bool registered;
}
