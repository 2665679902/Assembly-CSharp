using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000216 RID: 534
	[Serializable]
	public class SyntaxErrorException : YamlException
	{
		// Token: 0x0600108B RID: 4235 RVA: 0x00043D44 File Offset: 0x00041F44
		public SyntaxErrorException()
		{
		}

		// Token: 0x0600108C RID: 4236 RVA: 0x00043D4C File Offset: 0x00041F4C
		public SyntaxErrorException(string message)
			: base(message)
		{
		}

		// Token: 0x0600108D RID: 4237 RVA: 0x00043D55 File Offset: 0x00041F55
		public SyntaxErrorException(Mark start, Mark end, string message)
			: base(start, end, message)
		{
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x00043D60 File Offset: 0x00041F60
		public SyntaxErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
