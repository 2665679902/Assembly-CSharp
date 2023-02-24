using System;
using UnityEngine;

// Token: 0x020004CB RID: 1227
[AddComponentMenu("KMonoBehaviour/scripts/SimpleMassStatusItem")]
public class SimpleMassStatusItem : KMonoBehaviour
{
	// Token: 0x06001C76 RID: 7286 RVA: 0x00097BCD File Offset: 0x00095DCD
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.GetComponent<KSelectable>().AddStatusItem(Db.Get().MiscStatusItems.OreMass, base.gameObject);
	}

	// Token: 0x04001005 RID: 4101
	public string symbolPrefix = "";
}
