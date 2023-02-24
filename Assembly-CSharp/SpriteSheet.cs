using System;
using UnityEngine;

// Token: 0x020004CF RID: 1231
[Serializable]
public struct SpriteSheet
{
	// Token: 0x0400100E RID: 4110
	public string name;

	// Token: 0x0400100F RID: 4111
	public int numFrames;

	// Token: 0x04001010 RID: 4112
	public int numXFrames;

	// Token: 0x04001011 RID: 4113
	public Vector2 uvFrameSize;

	// Token: 0x04001012 RID: 4114
	public int renderLayer;

	// Token: 0x04001013 RID: 4115
	public Material material;

	// Token: 0x04001014 RID: 4116
	public Texture2D texture;
}
