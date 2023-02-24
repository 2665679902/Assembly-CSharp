using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000202 RID: 514
	[Serializable]
	public class ForwardAnchorNotSupportedException : YamlException
	{
		// Token: 0x06000FE2 RID: 4066 RVA: 0x000402CC File Offset: 0x0003E4CC
		public ForwardAnchorNotSupportedException()
		{
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000402D4 File Offset: 0x0003E4D4
		public ForwardAnchorNotSupportedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000402DD File Offset: 0x0003E4DD
		public ForwardAnchorNotSupportedException(Mark start, Mark end, string message)
			: base(start, end, message)
		{
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000402E8 File Offset: 0x0003E4E8
		public ForwardAnchorNotSupportedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
