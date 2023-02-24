using System;
using System.Globalization;

namespace YamlDotNet.Serialization
{
	// Token: 0x020001A5 RID: 421
	internal static class YamlFormatter
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x00038B06 File Offset: 0x00036D06
		public static string FormatNumber(object number)
		{
			return Convert.ToString(number, YamlFormatter.NumberFormat);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00038B13 File Offset: 0x00036D13
		public static string FormatNumber(double number)
		{
			return number.ToString("G17", YamlFormatter.NumberFormat);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00038B26 File Offset: 0x00036D26
		public static string FormatNumber(float number)
		{
			return number.ToString("G17", YamlFormatter.NumberFormat);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x00038B39 File Offset: 0x00036D39
		public static string FormatBoolean(object boolean)
		{
			if (!boolean.Equals(true))
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x00038B54 File Offset: 0x00036D54
		public static string FormatDateTime(object dateTime)
		{
			return ((DateTime)dateTime).ToString("o", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x00038B7C File Offset: 0x00036D7C
		public static string FormatTimeSpan(object timeSpan)
		{
			return ((TimeSpan)timeSpan).ToString();
		}

		// Token: 0x0400080F RID: 2063
		public static readonly NumberFormatInfo NumberFormat = new NumberFormatInfo
		{
			CurrencyDecimalSeparator = ".",
			CurrencyGroupSeparator = "_",
			CurrencyGroupSizes = new int[] { 3 },
			CurrencySymbol = string.Empty,
			CurrencyDecimalDigits = 99,
			NumberDecimalSeparator = ".",
			NumberGroupSeparator = "_",
			NumberGroupSizes = new int[] { 3 },
			NumberDecimalDigits = 99,
			NaNSymbol = ".nan",
			PositiveInfinitySymbol = ".inf",
			NegativeInfinitySymbol = "-.inf"
		};
	}
}
