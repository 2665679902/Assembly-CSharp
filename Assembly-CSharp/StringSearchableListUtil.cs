using System;
using System.Linq;

// Token: 0x02000367 RID: 871
public static class StringSearchableListUtil
{
	// Token: 0x060011BA RID: 4538 RVA: 0x0005E048 File Offset: 0x0005C248
	public static bool DoAnyTagsMatchFilter(string[] lowercaseTags, in string filter)
	{
		string text = filter.Trim().ToLowerInvariant();
		string[] array = text.Split(new char[] { ' ' });
		for (int i = 0; i < lowercaseTags.Length; i++)
		{
			string tag = lowercaseTags[i];
			if (StringSearchableListUtil.DoesTagMatchFilter(tag, text))
			{
				return true;
			}
			if (array.Select((string f) => StringSearchableListUtil.DoesTagMatchFilter(tag, f)).All((bool result) => result))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060011BB RID: 4539 RVA: 0x0005E0E3 File Offset: 0x0005C2E3
	public static bool DoesTagMatchFilter(string lowercaseTag, in string filter)
	{
		return string.IsNullOrWhiteSpace(filter) || lowercaseTag.Contains(filter);
	}

	// Token: 0x060011BC RID: 4540 RVA: 0x0005E0FD File Offset: 0x0005C2FD
	public static bool ShouldUseFilter(string filter)
	{
		return !string.IsNullOrWhiteSpace(filter);
	}
}
