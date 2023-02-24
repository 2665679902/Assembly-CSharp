using System;
using System.Collections.Generic;

// Token: 0x0200010A RID: 266
public struct TagBits
{
	// Token: 0x060008F3 RID: 2291 RVA: 0x00023A0E File Offset: 0x00021C0E
	public TagBits(ref TagBits other)
	{
		this.bits0 = other.bits0;
		this.bits1 = other.bits1;
		this.bits2 = other.bits2;
		this.bits3 = other.bits3;
		this.bits4 = other.bits4;
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x00023A4C File Offset: 0x00021C4C
	public TagBits(Tag tag)
	{
		this.bits0 = 0UL;
		this.bits1 = 0UL;
		this.bits2 = 0UL;
		this.bits3 = 0UL;
		this.bits4 = 0UL;
		this.SetTag(tag);
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x00023A80 File Offset: 0x00021C80
	public TagBits(Tag[] tags)
	{
		this.bits0 = 0UL;
		this.bits1 = 0UL;
		this.bits2 = 0UL;
		this.bits3 = 0UL;
		this.bits4 = 0UL;
		if (tags == null)
		{
			return;
		}
		foreach (Tag tag in tags)
		{
			this.SetTag(tag);
		}
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x00023AD8 File Offset: 0x00021CD8
	public List<Tag> GetTagsVerySlow()
	{
		List<Tag> list = new List<Tag>();
		this.GetTagsVerySlow(0, this.bits0, list);
		this.GetTagsVerySlow(1, this.bits1, list);
		this.GetTagsVerySlow(2, this.bits2, list);
		this.GetTagsVerySlow(3, this.bits3, list);
		this.GetTagsVerySlow(4, this.bits4, list);
		return list;
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x00023B34 File Offset: 0x00021D34
	private void GetTagsVerySlow(int bits_idx, ulong bits, List<Tag> tags)
	{
		for (int i = 0; i < 64; i++)
		{
			if ((bits & (1UL << i)) != 0UL)
			{
				int num = 64 * bits_idx + i;
				tags.Add(TagBits.inverseTagTable[num]);
			}
		}
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x00023B74 File Offset: 0x00021D74
	private static int ManifestFlagIndex(Tag tag)
	{
		int count;
		if (TagBits.tagTable.TryGetValue(tag, out count))
		{
			return count;
		}
		count = TagBits.tagTable.Count;
		TagBits.tagTable.Add(tag, count);
		TagBits.inverseTagTable.Add(tag);
		DebugUtil.Assert(TagBits.inverseTagTable.Count == count + 1);
		if (TagBits.tagTable.Count >= 320)
		{
			string text = "Out of tag bits:\n";
			int num = 0;
			foreach (KeyValuePair<Tag, int> keyValuePair in TagBits.tagTable)
			{
				text = text + keyValuePair.Key.ToString() + ", ";
				num++;
				if (num % 64 == 0)
				{
					text += "\n";
				}
			}
			Debug.LogError(text);
		}
		return count;
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x00023C60 File Offset: 0x00021E60
	public void SetTag(Tag tag)
	{
		int num = TagBits.ManifestFlagIndex(tag);
		if (num < 64)
		{
			this.bits0 |= 1UL << num;
			return;
		}
		if (num < 128)
		{
			this.bits1 |= 1UL << num;
			return;
		}
		if (num < 192)
		{
			this.bits2 |= 1UL << num;
			return;
		}
		if (num < 256)
		{
			this.bits3 |= 1UL << num;
			return;
		}
		if (num < 320)
		{
			this.bits4 |= 1UL << num;
			return;
		}
		Debug.LogError("Out of bits!");
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x00023D0C File Offset: 0x00021F0C
	public void Clear(Tag tag)
	{
		int num = TagBits.ManifestFlagIndex(tag);
		if (num < 64)
		{
			this.bits0 &= ~(1UL << num);
			return;
		}
		if (num < 128)
		{
			this.bits1 &= ~(1UL << num);
			return;
		}
		if (num < 192)
		{
			this.bits2 &= ~(1UL << num);
			return;
		}
		if (num < 256)
		{
			this.bits3 &= ~(1UL << num);
			return;
		}
		if (num < 320)
		{
			this.bits4 &= ~(1UL << num);
			return;
		}
		Debug.LogError("Out of bits!");
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x00023DBD File Offset: 0x00021FBD
	public void ClearAll()
	{
		this.bits0 = 0UL;
		this.bits1 = 0UL;
		this.bits2 = 0UL;
		this.bits3 = 0UL;
		this.bits4 = 0UL;
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x00023DE8 File Offset: 0x00021FE8
	public bool HasAll(ref TagBits tag_bits)
	{
		return (this.bits0 & tag_bits.bits0) == tag_bits.bits0 && (this.bits1 & tag_bits.bits1) == tag_bits.bits1 && (this.bits2 & tag_bits.bits2) == tag_bits.bits2 && (this.bits3 & tag_bits.bits3) == tag_bits.bits3 && (this.bits4 & tag_bits.bits4) == tag_bits.bits4;
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x00023E60 File Offset: 0x00022060
	public bool HasAny(ref TagBits tag_bits)
	{
		return ((this.bits0 & tag_bits.bits0) | (this.bits1 & tag_bits.bits1) | (this.bits2 & tag_bits.bits2) | (this.bits3 & tag_bits.bits3) | (this.bits4 & tag_bits.bits4)) > 0UL;
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00023EB8 File Offset: 0x000220B8
	public bool AreEqual(ref TagBits tag_bits)
	{
		return tag_bits.bits0 == this.bits0 && tag_bits.bits1 == this.bits1 && tag_bits.bits2 == this.bits2 && tag_bits.bits3 == this.bits3 && tag_bits.bits4 == this.bits4;
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00023F10 File Offset: 0x00022110
	public void And(ref TagBits rhs)
	{
		this.bits0 &= rhs.bits0;
		this.bits1 &= rhs.bits1;
		this.bits2 &= rhs.bits2;
		this.bits3 &= rhs.bits3;
		this.bits4 &= rhs.bits4;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00023F7C File Offset: 0x0002217C
	public void Or(ref TagBits rhs)
	{
		this.bits0 |= rhs.bits0;
		this.bits1 |= rhs.bits1;
		this.bits2 |= rhs.bits2;
		this.bits3 |= rhs.bits3;
		this.bits4 |= rhs.bits4;
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00023FE8 File Offset: 0x000221E8
	public void Xor(ref TagBits rhs)
	{
		this.bits0 ^= rhs.bits0;
		this.bits1 ^= rhs.bits1;
		this.bits2 ^= rhs.bits2;
		this.bits3 ^= rhs.bits3;
		this.bits4 ^= rhs.bits4;
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00024054 File Offset: 0x00022254
	public void Complement()
	{
		this.bits0 = ~this.bits0;
		this.bits1 = ~this.bits1;
		this.bits2 = ~this.bits2;
		this.bits3 = ~this.bits3;
		this.bits4 = ~this.bits4;
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x000240A4 File Offset: 0x000222A4
	public static TagBits MakeComplement(ref TagBits rhs)
	{
		TagBits tagBits = new TagBits(ref rhs);
		tagBits.Complement();
		return tagBits;
	}

	// Token: 0x04000675 RID: 1653
	private static Dictionary<Tag, int> tagTable = new Dictionary<Tag, int>();

	// Token: 0x04000676 RID: 1654
	private static List<Tag> inverseTagTable = new List<Tag>();

	// Token: 0x04000677 RID: 1655
	private const int Capacity = 320;

	// Token: 0x04000678 RID: 1656
	private ulong bits0;

	// Token: 0x04000679 RID: 1657
	private ulong bits1;

	// Token: 0x0400067A RID: 1658
	private ulong bits2;

	// Token: 0x0400067B RID: 1659
	private ulong bits3;

	// Token: 0x0400067C RID: 1660
	private ulong bits4;

	// Token: 0x0400067D RID: 1661
	public static TagBits None = default(TagBits);
}
