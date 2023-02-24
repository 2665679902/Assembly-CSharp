using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	// Token: 0x02000206 RID: 518
	public interface IParser
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000FEB RID: 4075
		ParsingEvent Current { get; }

		// Token: 0x06000FEC RID: 4076
		bool MoveNext();
	}
}
