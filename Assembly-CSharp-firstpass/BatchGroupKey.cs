using System;

// Token: 0x0200000D RID: 13
public struct BatchGroupKey : IEquatable<BatchGroupKey>
{
	// Token: 0x060000C5 RID: 197 RVA: 0x00005293 File Offset: 0x00003493
	public BatchGroupKey(HashedString group_id)
	{
		this._groupID = group_id;
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x0000529C File Offset: 0x0000349C
	public bool Equals(BatchGroupKey other)
	{
		return this._groupID == other._groupID;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x000052AF File Offset: 0x000034AF
	public override int GetHashCode()
	{
		return this._groupID.HashValue;
	}

	// Token: 0x17000024 RID: 36
	// (get) Token: 0x060000C8 RID: 200 RVA: 0x000052BC File Offset: 0x000034BC
	public HashedString groupID
	{
		get
		{
			return this._groupID;
		}
	}

	// Token: 0x04000031 RID: 49
	private HashedString _groupID;
}
