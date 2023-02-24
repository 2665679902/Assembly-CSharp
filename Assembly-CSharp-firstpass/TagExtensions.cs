using System;
using System.Collections.Generic;

// Token: 0x02000108 RID: 264
public static class TagExtensions
{
	// Token: 0x060008EB RID: 2283 RVA: 0x0002387D File Offset: 0x00021A7D
	public static Tag ToTag(this string str)
	{
		return new Tag(str);
	}

	// Token: 0x060008EC RID: 2284 RVA: 0x00023888 File Offset: 0x00021A88
	public static Tag[] ToTagArray(this string[] strArray)
	{
		Tag[] array = new Tag[strArray.Length];
		for (int i = 0; i < strArray.Length; i++)
		{
			array[i] = strArray[i].ToTag();
		}
		return array;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x000238BC File Offset: 0x00021ABC
	public static List<Tag> ToTagList(this string[] strArray)
	{
		List<Tag> list = new List<Tag>();
		foreach (string text in strArray)
		{
			list.Add(text.ToTag());
		}
		return list;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x000238F0 File Offset: 0x00021AF0
	public static List<Tag> ToTagList(this List<string> strList)
	{
		List<Tag> tagList = new List<Tag>();
		strList.ForEach(delegate(string str)
		{
			tagList.Add(str.ToTag());
		});
		return tagList;
	}
}
