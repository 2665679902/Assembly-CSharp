using System;
using UnityEngine;

// Token: 0x02000544 RID: 1348
[AddComponentMenu("KMonoBehaviour/scripts/AmbientSoundManager")]
public class AmbientSoundManager : KMonoBehaviour
{
	// Token: 0x17000183 RID: 387
	// (get) Token: 0x0600202A RID: 8234 RVA: 0x000AFB97 File Offset: 0x000ADD97
	// (set) Token: 0x0600202B RID: 8235 RVA: 0x000AFB9E File Offset: 0x000ADD9E
	public static AmbientSoundManager Instance { get; private set; }

	// Token: 0x0600202C RID: 8236 RVA: 0x000AFBA6 File Offset: 0x000ADDA6
	public static void Destroy()
	{
		AmbientSoundManager.Instance = null;
	}

	// Token: 0x0600202D RID: 8237 RVA: 0x000AFBAE File Offset: 0x000ADDAE
	protected override void OnPrefabInit()
	{
		AmbientSoundManager.Instance = this;
	}

	// Token: 0x0600202E RID: 8238 RVA: 0x000AFBB6 File Offset: 0x000ADDB6
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x0600202F RID: 8239 RVA: 0x000AFBBE File Offset: 0x000ADDBE
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		AmbientSoundManager.Instance = null;
	}

	// Token: 0x04001270 RID: 4720
	[MyCmpAdd]
	private LoopingSounds loopingSounds;
}
