using System;
using System.Runtime.Serialization;
using KSerialization;

// Token: 0x02000123 RID: 291
[SerializationConfig(MemberSerialization.OptOut)]
public class Chunk
{
	// Token: 0x06000A29 RID: 2601 RVA: 0x000270EF File Offset: 0x000252EF
	public Chunk()
	{
		this.state = Chunk.State.Unprocessed;
		this.data = null;
		this.overrides = null;
		this.density = null;
		this.heatOffset = null;
		this.defaultTemp = null;
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x00027121 File Offset: 0x00025321
	public Chunk(int x, int y, int width, int height)
	{
		this.offset = new Vector2I(x, y);
		this.size = new Vector2I(width, height);
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x00027144 File Offset: 0x00025344
	[OnDeserializing]
	internal void OnDeserializingMethod()
	{
		int x = this.size.x;
		int y = this.size.y;
		this.data = new float[x * y];
		this.overrides = new float[x * y];
		this.density = new float[x * y];
		this.heatOffset = new float[x * y];
		this.defaultTemp = new float[x * y];
		this.state = Chunk.State.Loaded;
	}

	// Token: 0x040006BB RID: 1723
	public Chunk.State state;

	// Token: 0x040006BC RID: 1724
	public Vector2I offset;

	// Token: 0x040006BD RID: 1725
	public Vector2I size;

	// Token: 0x040006BE RID: 1726
	public float[] data;

	// Token: 0x040006BF RID: 1727
	public float[] overrides;

	// Token: 0x040006C0 RID: 1728
	public float[] density;

	// Token: 0x040006C1 RID: 1729
	public float[] heatOffset;

	// Token: 0x040006C2 RID: 1730
	public float[] defaultTemp;

	// Token: 0x02000A0B RID: 2571
	public enum State
	{
		// Token: 0x04002267 RID: 8807
		Unprocessed,
		// Token: 0x04002268 RID: 8808
		GeneratedNoise,
		// Token: 0x04002269 RID: 8809
		Processed,
		// Token: 0x0400226A RID: 8810
		Loaded
	}
}
