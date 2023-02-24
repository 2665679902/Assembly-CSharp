using System;

namespace YamlDotNet.Core
{
	// Token: 0x0200020B RID: 523
	[Serializable]
	public class MaximumRecursionLevelReachedException : YamlException
	{
		// Token: 0x0600100C RID: 4108 RVA: 0x00040695 File Offset: 0x0003E895
		public MaximumRecursionLevelReachedException()
		{
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0004069D File Offset: 0x0003E89D
		public MaximumRecursionLevelReachedException(string message)
			: base(message)
		{
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x000406A6 File Offset: 0x0003E8A6
		public MaximumRecursionLevelReachedException(Mark start, Mark end, string message)
			: base(start, end, message)
		{
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x000406B1 File Offset: 0x0003E8B1
		public MaximumRecursionLevelReachedException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
