using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001CB RID: 459
	public sealed class NullNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E2A RID: 3626 RVA: 0x0003A9C0 File Offset: 0x00038BC0
		bool INodeDeserializer.Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			value = null;
			NodeEvent nodeEvent = parser.Peek<NodeEvent>();
			bool flag = nodeEvent != null && this.NodeIsNull(nodeEvent);
			if (flag)
			{
				parser.SkipThisAndNestedEvents();
			}
			return flag;
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003A9F0 File Offset: 0x00038BF0
		private bool NodeIsNull(NodeEvent nodeEvent)
		{
			if (nodeEvent.Tag == "tag:yaml.org,2002:null")
			{
				return true;
			}
			Scalar scalar = nodeEvent as Scalar;
			if (scalar == null || scalar.Style != ScalarStyle.Plain)
			{
				return false;
			}
			string value = scalar.Value;
			return value == "" || value == "~" || value == "null" || value == "Null" || value == "NULL";
		}
	}
}
