using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeTypeResolvers
{
	// Token: 0x020001C6 RID: 454
	public sealed class YamlSerializableTypeResolver : INodeTypeResolver
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x0003A539 File Offset: 0x00038739
		public bool Resolve(NodeEvent nodeEvent, ref Type currentType)
		{
			return typeof(IYamlSerializable).IsAssignableFrom(currentType);
		}
	}
}
