using System;
using System.Linq;
using KSerialization;
using UnityEngine;

// Token: 0x02000107 RID: 263
[SerializationConfig(MemberSerialization.OptIn)]
[Serializable]
public struct Tag : ISerializationCallbackReceiver, IEquatable<Tag>, IComparable<Tag>
{
	// Token: 0x060008D7 RID: 2263 RVA: 0x000236FD File Offset: 0x000218FD
	public Tag(int hash)
	{
		this.hash = hash;
		this.name = "";
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00023711 File Offset: 0x00021911
	public Tag(Tag orig)
	{
		this.name = orig.name;
		this.hash = orig.hash;
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x0002372B File Offset: 0x0002192B
	public Tag(string name)
	{
		this.name = name;
		this.hash = Hash.SDBMLower(name);
	}

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060008DA RID: 2266 RVA: 0x00023740 File Offset: 0x00021940
	// (set) Token: 0x060008DB RID: 2267 RVA: 0x00023748 File Offset: 0x00021948
	public string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.name = string.Intern(value);
			this.hash = Hash.SDBMLower(this.name);
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060008DC RID: 2268 RVA: 0x00023767 File Offset: 0x00021967
	public bool IsValid
	{
		get
		{
			return this.hash != 0;
		}
	}

	// Token: 0x060008DD RID: 2269 RVA: 0x00023772 File Offset: 0x00021972
	public void Clear()
	{
		this.name = null;
		this.hash = 0;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00023782 File Offset: 0x00021982
	public override int GetHashCode()
	{
		return this.hash;
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x0002378A File Offset: 0x0002198A
	public int GetHash()
	{
		return this.hash;
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00023794 File Offset: 0x00021994
	public override bool Equals(object obj)
	{
		Tag tag = (Tag)obj;
		return this.hash == tag.hash;
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x000237B6 File Offset: 0x000219B6
	public bool Equals(Tag other)
	{
		return this.hash == other.hash;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x000237C6 File Offset: 0x000219C6
	public static bool operator ==(Tag a, Tag b)
	{
		return a.hash == b.hash;
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x000237D6 File Offset: 0x000219D6
	public static bool operator !=(Tag a, Tag b)
	{
		return a.hash != b.hash;
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x000237E9 File Offset: 0x000219E9
	public void OnBeforeSerialize()
	{
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x000237EB File Offset: 0x000219EB
	public void OnAfterDeserialize()
	{
		if (this.name != null)
		{
			this.Name = this.name;
			return;
		}
		this.name = "";
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0002380D File Offset: 0x00021A0D
	public int CompareTo(Tag other)
	{
		return this.hash - other.hash;
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x0002381C File Offset: 0x00021A1C
	public override string ToString()
	{
		if (this.name == null)
		{
			return this.hash.ToString("X");
		}
		return this.name;
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0002383D File Offset: 0x00021A3D
	public static implicit operator Tag(string s)
	{
		return new Tag(s);
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00023845 File Offset: 0x00021A45
	public static string ArrayToString(Tag[] tags)
	{
		return string.Join(",", tags.Select((Tag x) => x.ToString()).ToArray<string>());
	}

	// Token: 0x04000672 RID: 1650
	public static readonly Tag Invalid;

	// Token: 0x04000673 RID: 1651
	[Serialize]
	[SerializeField]
	private string name;

	// Token: 0x04000674 RID: 1652
	[Serialize]
	private int hash;
}
