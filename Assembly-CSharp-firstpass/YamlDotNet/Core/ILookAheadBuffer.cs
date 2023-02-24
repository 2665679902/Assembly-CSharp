using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000205 RID: 517
	internal interface ILookAheadBuffer
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000FE8 RID: 4072
		bool EndOfInput { get; }

		// Token: 0x06000FE9 RID: 4073
		char Peek(int offset);

		// Token: 0x06000FEA RID: 4074
		void Skip(int length);
	}
}
