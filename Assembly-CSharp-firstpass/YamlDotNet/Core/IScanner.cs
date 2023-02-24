using System;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	// Token: 0x02000207 RID: 519
	public interface IScanner
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000FED RID: 4077
		Mark CurrentPosition { get; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000FEE RID: 4078
		Token Current { get; }

		// Token: 0x06000FEF RID: 4079
		bool MoveNext();

		// Token: 0x06000FF0 RID: 4080
		bool MoveNextWithoutConsuming();

		// Token: 0x06000FF1 RID: 4081
		void ConsumeCurrent();
	}
}
