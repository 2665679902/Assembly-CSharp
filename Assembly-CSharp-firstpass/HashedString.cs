using System;
using KSerialization;
using UnityEngine;

// Token: 0x020000A7 RID: 167
[SerializationConfig(MemberSerialization.OptIn)]
[Serializable]
public struct HashedString : IComparable<HashedString>, IEquatable<HashedString>, ISerializationCallbackReceiver
{
	// Token: 0x06000641 RID: 1601 RVA: 0x0001C854 File Offset: 0x0001AA54
	public static implicit operator HashedString(string s)
	{
		return new HashedString(s);
	}

	// Token: 0x170000B7 RID: 183
	// (get) Token: 0x06000642 RID: 1602 RVA: 0x0001C85C File Offset: 0x0001AA5C
	public bool IsValid
	{
		get
		{
			return this.HashValue != 0;
		}
	}

	// Token: 0x170000B8 RID: 184
	// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001C867 File Offset: 0x0001AA67
	// (set) Token: 0x06000644 RID: 1604 RVA: 0x0001C86F File Offset: 0x0001AA6F
	public int HashValue
	{
		get
		{
			return this.hash;
		}
		set
		{
			this.hash = value;
		}
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0001C878 File Offset: 0x0001AA78
	public HashedString(string name)
	{
		this.hash = global::Hash.SDBMLower(name);
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x0001C886 File Offset: 0x0001AA86
	public static int Hash(string name)
	{
		return global::Hash.SDBMLower(name);
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x0001C88E File Offset: 0x0001AA8E
	public HashedString(int initial_hash)
	{
		this.hash = initial_hash;
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0001C897 File Offset: 0x0001AA97
	public int CompareTo(HashedString obj)
	{
		return this.hash - obj.hash;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0001C8A8 File Offset: 0x0001AAA8
	public override bool Equals(object obj)
	{
		HashedString hashedString = (HashedString)obj;
		return this.hash == hashedString.hash;
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0001C8CA File Offset: 0x0001AACA
	public bool Equals(HashedString other)
	{
		return this.hash == other.hash;
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0001C8DA File Offset: 0x0001AADA
	public override int GetHashCode()
	{
		return this.hash;
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0001C8E2 File Offset: 0x0001AAE2
	public static bool operator ==(HashedString x, HashedString y)
	{
		return x.hash == y.hash;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0001C8F2 File Offset: 0x0001AAF2
	public static bool operator !=(HashedString x, HashedString y)
	{
		return x.hash != y.hash;
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0001C905 File Offset: 0x0001AB05
	public static implicit operator HashedString(KAnimHashedString hash)
	{
		return new HashedString(hash.HashValue);
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0001C913 File Offset: 0x0001AB13
	public override string ToString()
	{
		return "0x" + this.hash.ToString("X");
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0001C92F File Offset: 0x0001AB2F
	public void OnAfterDeserialize()
	{
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0001C931 File Offset: 0x0001AB31
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x040005A7 RID: 1447
	public static HashedString Invalid;

	// Token: 0x040005A8 RID: 1448
	[SerializeField]
	[Serialize]
	private int hash;
}
