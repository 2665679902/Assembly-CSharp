using System;
using System.Text.RegularExpressions;

namespace YamlDotNet.Serialization.Utilities
{
	// Token: 0x020001AE RID: 430
	internal static class StringExtensions
	{
		// Token: 0x06000DB1 RID: 3505 RVA: 0x00039138 File Offset: 0x00037338
		private static string ToCamelOrPascalCase(string str, Func<char, char> firstLetterTransform)
		{
			string text = Regex.Replace(str, "([_\\-])(?<char>[a-z])", (Match match) => match.Groups["char"].Value.ToUpperInvariant(), RegexOptions.IgnoreCase);
			return firstLetterTransform(text[0]).ToString() + text.Substring(1);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00039192 File Offset: 0x00037392
		public static string ToCamelCase(this string str)
		{
			return StringExtensions.ToCamelOrPascalCase(str, new Func<char, char>(char.ToLowerInvariant));
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x000391A6 File Offset: 0x000373A6
		public static string ToPascalCase(this string str)
		{
			return StringExtensions.ToCamelOrPascalCase(str, new Func<char, char>(char.ToUpperInvariant));
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x000391BC File Offset: 0x000373BC
		public static string FromCamelCase(this string str, string separator)
		{
			str = char.ToLower(str[0]).ToString() + str.Substring(1);
			str = Regex.Replace(str.ToCamelCase(), "(?<char>[A-Z])", (Match match) => separator + match.Groups["char"].Value.ToLowerInvariant());
			return str;
		}
	}
}
