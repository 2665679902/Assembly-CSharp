using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class TextureAtlas : ScriptableObject
{
	// Token: 0x040003B1 RID: 945
	public List<string> sourceReference = new List<string>();

	// Token: 0x040003B2 RID: 946
	public Texture2D texture;

	// Token: 0x040003B3 RID: 947
	public float scaleFactor = 1f;

	// Token: 0x040003B4 RID: 948
	public int mipPaddingPixels = 64;

	// Token: 0x040003B5 RID: 949
	public int tileDimension = 256;

	// Token: 0x040003B6 RID: 950
	public bool onlyRebuildSelfNamedFolder = true;

	// Token: 0x040003B7 RID: 951
	public TextureAtlas.Item[] items;

	// Token: 0x020009A1 RID: 2465
	[Serializable]
	public struct Item
	{
		// Token: 0x04002161 RID: 8545
		public string name;

		// Token: 0x04002162 RID: 8546
		public Vector4 uvBox;

		// Token: 0x04002163 RID: 8547
		public Vector3[] vertices;

		// Token: 0x04002164 RID: 8548
		public Vector2[] uvs;

		// Token: 0x04002165 RID: 8549
		public int[] indices;
	}

	// Token: 0x020009A2 RID: 2466
	public class AtlasData
	{
		// Token: 0x04002166 RID: 8550
		public List<TextureAtlas.AtlasData.Frame> frames;

		// Token: 0x04002167 RID: 8551
		public TextureAtlas.AtlasData.Meta meta;

		// Token: 0x02000B44 RID: 2884
		public class Frame
		{
			// Token: 0x0400268E RID: 9870
			public string filename;

			// Token: 0x0400268F RID: 9871
			public Dictionary<string, int> frame;

			// Token: 0x04002690 RID: 9872
			public List<int[]> vertices;

			// Token: 0x04002691 RID: 9873
			public List<int[]> verticesUV;

			// Token: 0x04002692 RID: 9874
			public List<int[]> triangles;
		}

		// Token: 0x02000B45 RID: 2885
		public struct Meta
		{
			// Token: 0x04002693 RID: 9875
			public Dictionary<string, int> size;
		}
	}
}
