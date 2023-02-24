using System;

namespace YamlDotNet.Core
{
	// Token: 0x020001FB RID: 507
	[Serializable]
	public class AnchorNotFoundException : YamlException
	{
		// Token: 0x06000F78 RID: 3960 RVA: 0x0003DBED File Offset: 0x0003BDED
		public AnchorNotFoundException()
		{
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003DBF5 File Offset: 0x0003BDF5
		public AnchorNotFoundException(string message)
			: base(message)
		{
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0003DBFE File Offset: 0x0003BDFE
		public AnchorNotFoundException(Mark start, Mark end, string message)
			: base(start, end, message)
		{
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0003DC09 File Offset: 0x0003BE09
		public AnchorNotFoundException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
