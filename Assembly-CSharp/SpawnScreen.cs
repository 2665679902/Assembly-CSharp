using System;
using UnityEngine;

// Token: 0x02000BFE RID: 3070
[AddComponentMenu("KMonoBehaviour/scripts/SpawnScreen")]
public class SpawnScreen : KMonoBehaviour
{
	// Token: 0x0600611F RID: 24863 RVA: 0x0023BC34 File Offset: 0x00239E34
	protected override void OnPrefabInit()
	{
		Util.KInstantiateUI(this.Screen, base.gameObject, false);
	}

	// Token: 0x040042ED RID: 17133
	public GameObject Screen;
}
