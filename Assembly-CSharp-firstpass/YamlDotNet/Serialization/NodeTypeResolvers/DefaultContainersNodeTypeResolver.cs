using System;
using System.Collections.Generic;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeTypeResolvers
{
	// Token: 0x020001C2 RID: 450
	public sealed class DefaultContainersNodeTypeResolver : INodeTypeResolver
	{
		// Token: 0x06000E16 RID: 3606 RVA: 0x0003A440 File Offset: 0x00038640
		bool INodeTypeResolver.Resolve(NodeEvent nodeEvent, ref Type currentType)
		{
			if (currentType == typeof(object))
			{
				if (nodeEvent is SequenceStart)
				{
					currentType = typeof(List<object>);
					return true;
				}
				if (nodeEvent is MappingStart)
				{
					currentType = typeof(Dictionary<object, object>);
					return true;
				}
			}
			return false;
		}
	}
}
