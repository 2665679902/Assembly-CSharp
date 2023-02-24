using System;
using KSerialization;
using UnityEngine;

// Token: 0x020005B5 RID: 1461
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Fabricator")]
public class Fabricator : KMonoBehaviour
{
	// Token: 0x06002433 RID: 9267 RVA: 0x000C3E17 File Offset: 0x000C2017
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}
}
