using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	// Token: 0x02000204 RID: 516
	public interface IEmitter
	{
		// Token: 0x06000FE7 RID: 4071
		void Emit(ParsingEvent @event);
	}
}
