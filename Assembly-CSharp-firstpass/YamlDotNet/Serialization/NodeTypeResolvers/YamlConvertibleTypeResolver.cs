using System;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeTypeResolvers
{
	// Token: 0x020001C5 RID: 453
	public sealed class YamlConvertibleTypeResolver : INodeTypeResolver
	{
		// Token: 0x06000E1C RID: 3612 RVA: 0x0003A51E File Offset: 0x0003871E
		public bool Resolve(NodeEvent nodeEvent, ref Type currentType)
		{
			return typeof(IYamlConvertible).IsAssignableFrom(currentType);
		}
	}
}
