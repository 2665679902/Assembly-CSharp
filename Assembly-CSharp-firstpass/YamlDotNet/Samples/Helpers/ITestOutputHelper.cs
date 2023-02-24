using System;

namespace YamlDotNet.Samples.Helpers
{
	// Token: 0x020001FA RID: 506
	public interface ITestOutputHelper
	{
		// Token: 0x06000F75 RID: 3957
		void WriteLine();

		// Token: 0x06000F76 RID: 3958
		void WriteLine(string value);

		// Token: 0x06000F77 RID: 3959
		void WriteLine(string format, params object[] args);
	}
}
