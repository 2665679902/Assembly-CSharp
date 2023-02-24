using System;
using System.Text;

// Token: 0x02000109 RID: 265
public class TagDescriptions
{
	// Token: 0x060008EF RID: 2287 RVA: 0x00023926 File Offset: 0x00021B26
	public TagDescriptions(string csv_data)
	{
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0002392E File Offset: 0x00021B2E
	public static string GetDescription(string tag)
	{
		return Strings.Get("STRINGS.MISC.TAGS." + tag.ToUpper());
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0002394A File Offset: 0x00021B4A
	public static string GetDescription(Tag tag)
	{
		return Strings.Get("STRINGS.MISC.TAGS." + tag.Name.ToUpper());
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0002396C File Offset: 0x00021B6C
	public static string ReplaceTags(string text)
	{
		int num = text.IndexOf('{');
		int num2 = text.IndexOf('}');
		if (0 <= num && num < num2)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int num3 = 0;
			while (0 <= num)
			{
				string text2 = text.Substring(num3, num - num3);
				stringBuilder.Append(text2);
				num2 = text.IndexOf('}', num);
				if (num >= num2)
				{
					break;
				}
				string description = TagDescriptions.GetDescription(text.Substring(num + 1, num2 - num - 1));
				stringBuilder.Append(description);
				num3 = num2 + 1;
				num = text.IndexOf('{', num2);
			}
			stringBuilder.Append(text.Substring(num3, text.Length - num3));
			return stringBuilder.ToString();
		}
		return text;
	}
}
