using System;
using UnityEngine;

// Token: 0x0200085A RID: 2138
[AddComponentMenu("KMonoBehaviour/scripts/MoveTarget")]
public class MoveTarget : KMonoBehaviour
{
	// Token: 0x06003D65 RID: 15717 RVA: 0x001573E4 File Offset: 0x001555E4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInEditor | HideFlags.NotEditable | HideFlags.DontSaveInBuild | HideFlags.DontUnloadUnusedAsset;
	}
}
