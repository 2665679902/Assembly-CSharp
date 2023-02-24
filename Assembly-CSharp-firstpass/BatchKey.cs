using System;

// Token: 0x02000009 RID: 9
public struct BatchKey : IEquatable<BatchKey>
{
	// Token: 0x06000069 RID: 105 RVA: 0x000040C8 File Offset: 0x000022C8
	private BatchKey(KAnimConverter.IAnimConverter controller)
	{
		this._layer = controller.GetLayer();
		this._groupID = controller.GetBatchGroupID(false);
		this._materialType = controller.GetMaterialType();
		this._z = controller.GetZ();
		this._idx = KAnimBatchManager.ControllerToChunkXY(controller);
		this._hash = 0;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00004119 File Offset: 0x00002319
	private BatchKey(KAnimConverter.IAnimConverter controller, Vector2I idx)
	{
		this = new BatchKey(controller);
		this._idx = idx;
	}

	// Token: 0x0600006B RID: 107 RVA: 0x00004129 File Offset: 0x00002329
	private void CalculateHash()
	{
		this._hash = this._z.GetHashCode() ^ this._layer ^ (int)this._materialType ^ this._groupID.HashValue ^ this._idx.GetHashCode();
	}

	// Token: 0x0600006C RID: 108 RVA: 0x00004168 File Offset: 0x00002368
	public static BatchKey Create(KAnimConverter.IAnimConverter controller, Vector2I idx)
	{
		BatchKey batchKey = new BatchKey(controller, idx);
		batchKey.CalculateHash();
		return batchKey;
	}

	// Token: 0x0600006D RID: 109 RVA: 0x00004188 File Offset: 0x00002388
	public static BatchKey Create(KAnimConverter.IAnimConverter controller)
	{
		BatchKey batchKey = new BatchKey(controller);
		batchKey.CalculateHash();
		return batchKey;
	}

	// Token: 0x0600006E RID: 110 RVA: 0x000041A8 File Offset: 0x000023A8
	public bool Equals(BatchKey other)
	{
		return this._z == other._z && this._layer == other._layer && this._materialType == other._materialType && this._groupID == other._groupID && this._idx == other._idx;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00004205 File Offset: 0x00002405
	public override int GetHashCode()
	{
		return this._hash;
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000070 RID: 112 RVA: 0x0000420D File Offset: 0x0000240D
	public float z
	{
		get
		{
			return this._z;
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000071 RID: 113 RVA: 0x00004215 File Offset: 0x00002415
	public int layer
	{
		get
		{
			return this._layer;
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000072 RID: 114 RVA: 0x0000421D File Offset: 0x0000241D
	public HashedString groupID
	{
		get
		{
			return this._groupID;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000073 RID: 115 RVA: 0x00004225 File Offset: 0x00002425
	public Vector2I idx
	{
		get
		{
			return this._idx;
		}
	}

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000074 RID: 116 RVA: 0x0000422D File Offset: 0x0000242D
	public KAnimBatchGroup.MaterialType materialType
	{
		get
		{
			return this._materialType;
		}
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000075 RID: 117 RVA: 0x00004235 File Offset: 0x00002435
	public int hash
	{
		get
		{
			return this._hash;
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00004240 File Offset: 0x00002440
	public override string ToString()
	{
		string[] array = new string[12];
		array[0] = "[";
		int num = 1;
		Vector2I vector2I = this.idx;
		array[num] = vector2I.x.ToString();
		array[2] = ",";
		int num2 = 3;
		vector2I = this.idx;
		array[num2] = vector2I.y.ToString();
		array[4] = "] [";
		array[5] = this.groupID.HashValue.ToString();
		array[6] = "] [";
		array[7] = this.layer.ToString();
		array[8] = "] [";
		array[9] = this.z.ToString();
		array[10] = "]";
		array[11] = this.materialType.ToString();
		return string.Concat(array);
	}

	// Token: 0x04000006 RID: 6
	private float _z;

	// Token: 0x04000007 RID: 7
	private int _layer;

	// Token: 0x04000008 RID: 8
	private KAnimBatchGroup.MaterialType _materialType;

	// Token: 0x04000009 RID: 9
	private HashedString _groupID;

	// Token: 0x0400000A RID: 10
	private Vector2I _idx;

	// Token: 0x0400000B RID: 11
	private int _hash;
}
