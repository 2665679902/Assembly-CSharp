using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeTypeResolvers
{
	// Token: 0x020001C4 RID: 452
	public sealed class TypeNameInTagNodeTypeResolver : INodeTypeResolver
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x0003A4E9 File Offset: 0x000386E9
		bool INodeTypeResolver.Resolve(NodeEvent nodeEvent, ref Type currentType)
		{
			if (!string.IsNullOrEmpty(nodeEvent.Tag))
			{
				currentType = Type.GetType(nodeEvent.Tag.Substring(1), false);
				return currentType != null;
			}
			return false;
		}
	}
}
