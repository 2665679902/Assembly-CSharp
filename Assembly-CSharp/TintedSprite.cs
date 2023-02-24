using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x0200054E RID: 1358
[DebuggerDisplay("{name}")]
[Serializable]
public class TintedSprite : ISerializationCallbackReceiver
{
	// Token: 0x06002068 RID: 8296 RVA: 0x000B103A File Offset: 0x000AF23A
	public void OnAfterDeserialize()
	{
	}

	// Token: 0x06002069 RID: 8297 RVA: 0x000B103C File Offset: 0x000AF23C
	public void OnBeforeSerialize()
	{
		if (this.sprite != null)
		{
			this.name = this.sprite.name;
		}
	}

	// Token: 0x040012A3 RID: 4771
	[ReadOnly]
	public string name;

	// Token: 0x040012A4 RID: 4772
	public Sprite sprite;

	// Token: 0x040012A5 RID: 4773
	public Color color;
}
