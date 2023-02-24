using System;
using System.Diagnostics;
using UnityEngine;

// Token: 0x02000015 RID: 21
[DebuggerDisplay("Name = {DebuggerDisplay}")]
[Serializable]
public struct KAnimHashedString : IComparable<KAnimHashedString>, IEquatable<KAnimHashedString>
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000132 RID: 306 RVA: 0x000076DA File Offset: 0x000058DA
	// (set) Token: 0x06000133 RID: 307 RVA: 0x000076E2 File Offset: 0x000058E2
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

	// Token: 0x06000134 RID: 308 RVA: 0x000076EB File Offset: 0x000058EB
	public KAnimHashedString(string name)
	{
		this.hash = Hash.SDBMLower(name);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x000076F9 File Offset: 0x000058F9
	public KAnimHashedString(int hash)
	{
		this.hash = hash;
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00007702 File Offset: 0x00005902
	public bool IsValid()
	{
		return this.hash != 0;
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x06000137 RID: 311 RVA: 0x0000770D File Offset: 0x0000590D
	public string DebuggerDisplay
	{
		get
		{
			return HashCache.Get().Get(this.hash);
		}
	}

	// Token: 0x06000138 RID: 312 RVA: 0x0000771F File Offset: 0x0000591F
	public static implicit operator KAnimHashedString(HashedString hash)
	{
		return new KAnimHashedString(hash.HashValue);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000772D File Offset: 0x0000592D
	public static implicit operator KAnimHashedString(string str)
	{
		return new KAnimHashedString(str);
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00007735 File Offset: 0x00005935
	public int CompareTo(KAnimHashedString obj)
	{
		if (this.hash < obj.hash)
		{
			return -1;
		}
		if (this.hash > obj.hash)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00007758 File Offset: 0x00005958
	public override bool Equals(object obj)
	{
		KAnimHashedString kanimHashedString = (KAnimHashedString)obj;
		return this.hash == kanimHashedString.hash;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x0000777A File Offset: 0x0000597A
	public bool Equals(KAnimHashedString other)
	{
		return this.hash == other.hash;
	}

	// Token: 0x0600013D RID: 317 RVA: 0x0000778A File Offset: 0x0000598A
	public override int GetHashCode()
	{
		return this.hash;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00007792 File Offset: 0x00005992
	public static bool operator ==(KAnimHashedString x, HashedString y)
	{
		return x.HashValue == y.HashValue;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x000077A4 File Offset: 0x000059A4
	public static bool operator !=(KAnimHashedString x, HashedString y)
	{
		return x.HashValue != y.HashValue;
	}

	// Token: 0x06000140 RID: 320 RVA: 0x000077B9 File Offset: 0x000059B9
	public static bool operator ==(KAnimHashedString x, KAnimHashedString y)
	{
		return x.hash == y.hash;
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000077C9 File Offset: 0x000059C9
	public static bool operator !=(KAnimHashedString x, KAnimHashedString y)
	{
		return x.hash != y.hash;
	}

	// Token: 0x06000142 RID: 322 RVA: 0x000077DC File Offset: 0x000059DC
	public override string ToString()
	{
		if (string.IsNullOrEmpty(this.DebuggerDisplay))
		{
			return "0x" + this.hash.ToString("X");
		}
		return this.DebuggerDisplay;
	}

	// Token: 0x0400007A RID: 122
	[SerializeField]
	private int hash;
}
