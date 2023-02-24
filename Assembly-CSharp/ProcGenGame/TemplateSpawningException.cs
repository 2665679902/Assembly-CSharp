using System;

namespace ProcGenGame
{
	// Token: 0x02000C42 RID: 3138
	public class TemplateSpawningException : Exception
	{
		// Token: 0x06006347 RID: 25415 RVA: 0x0024E2B6 File Offset: 0x0024C4B6
		public TemplateSpawningException(string message, string userMessage)
			: base(message)
		{
			this.userMessage = userMessage;
		}

		// Token: 0x040044E6 RID: 17638
		public readonly string userMessage;
	}
}
