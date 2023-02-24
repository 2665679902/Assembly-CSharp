using System;

namespace YamlDotNet.Samples.Helpers
{
	// Token: 0x020001F9 RID: 505
	public static class TestOutputHelperExtensions
	{
		// Token: 0x06000F74 RID: 3956 RVA: 0x0003DBE0 File Offset: 0x0003BDE0
		public static void WriteLine(this ITestOutputHelper output)
		{
			output.WriteLine(string.Empty);
		}
	}
}
