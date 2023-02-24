using System;
using System.Collections.Generic;

// Token: 0x0200010B RID: 267
public class TagManager
{
	// Token: 0x06000905 RID: 2309 RVA: 0x000240E4 File Offset: 0x000222E4
	public static Tag Create(string tag_string)
	{
		Tag tag = default(Tag);
		tag.Name = tag_string;
		if (!TagManager.ProperNames.ContainsKey(tag))
		{
			TagManager.ProperNames[tag] = "";
			TagManager.ProperNamesNoLinks[tag] = "";
		}
		return tag;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00024130 File Offset: 0x00022330
	public static Tag Create(string tag_string, string proper_name)
	{
		Tag tag = TagManager.Create(tag_string);
		if (string.IsNullOrEmpty(proper_name))
		{
			DebugUtil.Assert(false, "Attempting to set proper name for tag: " + tag_string + "to null or empty.");
		}
		TagManager.ProperNames[tag] = proper_name;
		TagManager.ProperNamesNoLinks[tag] = TagManager.StripLinkFormatting(proper_name);
		return tag;
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00024180 File Offset: 0x00022380
	public static Tag[] Create(IList<string> strings)
	{
		Tag[] array = new Tag[strings.Count];
		for (int i = 0; i < strings.Count; i++)
		{
			array[i] = TagManager.Create(strings[i]);
		}
		return array;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x000241C0 File Offset: 0x000223C0
	public static void FillMissingProperNames()
	{
		foreach (Tag tag in new List<Tag>(TagManager.ProperNames.Keys))
		{
			if (string.IsNullOrEmpty(TagManager.ProperNames[tag]))
			{
				TagManager.ProperNames[tag] = TagDescriptions.GetDescription(tag.Name);
				TagManager.ProperNamesNoLinks[tag] = TagManager.StripLinkFormatting(TagManager.ProperNames[tag]);
			}
		}
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0002425C File Offset: 0x0002245C
	public static string GetProperName(Tag tag, bool stripLink = false)
	{
		string text = null;
		if (stripLink && TagManager.ProperNamesNoLinks.TryGetValue(tag, out text))
		{
			return text;
		}
		if (!stripLink && TagManager.ProperNames.TryGetValue(tag, out text))
		{
			return text;
		}
		text = tag.Name;
		return text;
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0002429C File Offset: 0x0002249C
	public static string StripLinkFormatting(string text)
	{
		string text2 = text;
		try
		{
			while (text2.Contains("<link="))
			{
				int num = text2.IndexOf("</link>");
				if (num > -1)
				{
					text2 = text2.Remove(num, 7);
				}
				else
				{
					Debug.LogWarningFormat("String has no closing link tag: {0}", Array.Empty<object>());
				}
				int num2 = text2.IndexOf("<link=");
				if (num2 != -1)
				{
					text2 = text2.Remove(num2, 7);
				}
				else
				{
					Debug.LogWarningFormat("String has no open link tag: {0}", Array.Empty<object>());
				}
				int num3 = text2.IndexOf("\">");
				if (num3 != -1)
				{
					text2 = text2.Remove(num2, num3 - num2 + 2);
				}
				else
				{
					Debug.LogWarningFormat("String has no open link tag: {0}", Array.Empty<object>());
				}
			}
		}
		catch
		{
			Debug.Log("STRIP LINK FORMATTING FAILED ON: " + text);
			text2 = text;
		}
		return text2;
	}

	// Token: 0x0400067E RID: 1662
	private static Dictionary<Tag, string> ProperNames = new Dictionary<Tag, string>();

	// Token: 0x0400067F RID: 1663
	private static Dictionary<Tag, string> ProperNamesNoLinks = new Dictionary<Tag, string>();

	// Token: 0x04000680 RID: 1664
	public static readonly Tag Invalid = default(Tag);
}
