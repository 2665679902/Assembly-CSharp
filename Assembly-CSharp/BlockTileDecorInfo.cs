using System;
using Rendering;
using UnityEngine;

// Token: 0x020009E1 RID: 2529
public class BlockTileDecorInfo : ScriptableObject
{
	// Token: 0x06004B9A RID: 19354 RVA: 0x001A8D5C File Offset: 0x001A6F5C
	public void PostProcess()
	{
		if (this.decor != null && this.atlas != null && this.atlas.items != null)
		{
			for (int i = 0; i < this.decor.Length; i++)
			{
				if (this.decor[i].variants != null && this.decor[i].variants.Length != 0)
				{
					for (int j = 0; j < this.decor[i].variants.Length; j++)
					{
						bool flag = false;
						foreach (TextureAtlas.Item item in this.atlas.items)
						{
							string text = item.name;
							int num = text.IndexOf("/");
							if (num != -1)
							{
								text = text.Substring(num + 1);
							}
							if (this.decor[i].variants[j].name == text)
							{
								this.decor[i].variants[j].atlasItem = item;
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							DebugUtil.LogErrorArgs(new object[]
							{
								base.name,
								"/",
								this.decor[i].name,
								"could not find ",
								this.decor[i].variants[j].name,
								"in",
								this.atlas.name
							});
						}
					}
				}
			}
		}
	}

	// Token: 0x04003185 RID: 12677
	public TextureAtlas atlas;

	// Token: 0x04003186 RID: 12678
	public TextureAtlas atlasSpec;

	// Token: 0x04003187 RID: 12679
	public int sortOrder;

	// Token: 0x04003188 RID: 12680
	public BlockTileDecorInfo.Decor[] decor;

	// Token: 0x020017EE RID: 6126
	[Serializable]
	public struct ImageInfo
	{
		// Token: 0x04006E6A RID: 28266
		public string name;

		// Token: 0x04006E6B RID: 28267
		public Vector3 offset;

		// Token: 0x04006E6C RID: 28268
		[NonSerialized]
		public TextureAtlas.Item atlasItem;
	}

	// Token: 0x020017EF RID: 6127
	[Serializable]
	public struct Decor
	{
		// Token: 0x04006E6D RID: 28269
		public string name;

		// Token: 0x04006E6E RID: 28270
		[EnumFlags]
		public BlockTileRenderer.Bits requiredConnections;

		// Token: 0x04006E6F RID: 28271
		[EnumFlags]
		public BlockTileRenderer.Bits forbiddenConnections;

		// Token: 0x04006E70 RID: 28272
		public float probabilityCutoff;

		// Token: 0x04006E71 RID: 28273
		public BlockTileDecorInfo.ImageInfo[] variants;

		// Token: 0x04006E72 RID: 28274
		public int sortOrder;
	}
}
