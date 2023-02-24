using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000186 RID: 390
	public interface INodeTypeResolver
	{
		// Token: 0x06000CF2 RID: 3314
		bool Resolve(NodeEvent nodeEvent, ref Type currentType);
	}
}
