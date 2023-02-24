using System;
using System.Collections.Generic;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeTypeResolvers
{
	// Token: 0x020001C3 RID: 451
	public sealed class TagNodeTypeResolver : INodeTypeResolver
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x0003A495 File Offset: 0x00038695
		public TagNodeTypeResolver(IDictionary<string, Type> tagMappings)
		{
			if (tagMappings == null)
			{
				throw new ArgumentNullException("tagMappings");
			}
			this.tagMappings = tagMappings;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x0003A4B4 File Offset: 0x000386B4
		bool INodeTypeResolver.Resolve(NodeEvent nodeEvent, ref Type currentType)
		{
			Type type;
			if (!string.IsNullOrEmpty(nodeEvent.Tag) && this.tagMappings.TryGetValue(nodeEvent.Tag, out type))
			{
				currentType = type;
				return true;
			}
			return false;
		}

		// Token: 0x04000833 RID: 2099
		private readonly IDictionary<string, Type> tagMappings;
	}
}
