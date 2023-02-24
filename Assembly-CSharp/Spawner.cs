using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200098C RID: 2444
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Spawner")]
public class Spawner : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x0600486E RID: 18542 RVA: 0x00196467 File Offset: 0x00194667
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SaveGame.Instance.worldGenSpawner.AddLegacySpawner(this.prefabTag, Grid.PosToCell(this));
		Util.KDestroyGameObject(base.gameObject);
	}

	// Token: 0x04002F9E RID: 12190
	[Serialize]
	public Tag prefabTag;

	// Token: 0x04002F9F RID: 12191
	[Serialize]
	public int units = 1;
}
