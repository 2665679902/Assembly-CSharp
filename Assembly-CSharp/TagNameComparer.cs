using System;
using System.Collections.Generic;

// Token: 0x02000786 RID: 1926
public class TagNameComparer : IComparer<Tag>
{
	// Token: 0x0600354A RID: 13642 RVA: 0x0012555D File Offset: 0x0012375D
	public TagNameComparer()
	{
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x00125565 File Offset: 0x00123765
	public TagNameComparer(Tag firstTag)
	{
		this.firstTag = firstTag;
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x00125574 File Offset: 0x00123774
	public int Compare(Tag x, Tag y)
	{
		if (x == y)
		{
			return 0;
		}
		if (this.firstTag.IsValid)
		{
			if (x == this.firstTag && y != this.firstTag)
			{
				return 1;
			}
			if (x != this.firstTag && y == this.firstTag)
			{
				return -1;
			}
		}
		return x.ProperNameStripLink().CompareTo(y.ProperNameStripLink());
	}

	// Token: 0x040022F3 RID: 8947
	private Tag firstTag;
}
