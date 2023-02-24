using System;
using UnityEngine;

// Token: 0x0200001D RID: 29
public class Audio : ScriptableObject
{
	// Token: 0x060001C1 RID: 449 RVA: 0x0000A203 File Offset: 0x00008403
	public static Audio Get()
	{
		if (Audio._Instance == null)
		{
			Audio._Instance = Resources.Load<Audio>("Audio");
		}
		return Audio._Instance;
	}

	// Token: 0x040000AE RID: 174
	private static Audio _Instance;

	// Token: 0x040000AF RID: 175
	public float listenerMinZ;

	// Token: 0x040000B0 RID: 176
	public float listenerMinOrthographicSize;

	// Token: 0x040000B1 RID: 177
	public float listenerReferenceZ;

	// Token: 0x040000B2 RID: 178
	public float listenerReferenceOrthographicSize;
}
