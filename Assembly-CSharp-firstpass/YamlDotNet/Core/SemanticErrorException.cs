using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000213 RID: 531
	[Serializable]
	public class SemanticErrorException : YamlException
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x00043BD8 File Offset: 0x00041DD8
		public SemanticErrorException()
		{
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00043BE0 File Offset: 0x00041DE0
		public SemanticErrorException(string message)
			: base(message)
		{
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00043BE9 File Offset: 0x00041DE9
		public SemanticErrorException(Mark start, Mark end, string message)
			: base(start, end, message)
		{
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00043BF4 File Offset: 0x00041DF4
		public SemanticErrorException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
