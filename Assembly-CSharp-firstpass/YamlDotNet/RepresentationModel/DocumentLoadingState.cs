using System;
using System.Collections.Generic;
using System.Globalization;
using YamlDotNet.Core;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001DD RID: 477
	internal class DocumentLoadingState
	{
		// Token: 0x06000E68 RID: 3688 RVA: 0x0003B8C8 File Offset: 0x00039AC8
		public void AddAnchor(YamlNode node)
		{
			if (node.Anchor == null)
			{
				throw new ArgumentException("The specified node does not have an anchor");
			}
			if (this.anchors.ContainsKey(node.Anchor))
			{
				this.anchors[node.Anchor] = node;
				return;
			}
			this.anchors.Add(node.Anchor, node);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0003B920 File Offset: 0x00039B20
		public YamlNode GetNode(string anchor, bool throwException, Mark start, Mark end)
		{
			YamlNode yamlNode;
			if (this.anchors.TryGetValue(anchor, out yamlNode))
			{
				return yamlNode;
			}
			if (throwException)
			{
				throw new AnchorNotFoundException(start, end, string.Format(CultureInfo.InvariantCulture, "The anchor '{0}' does not exists", anchor));
			}
			return null;
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x0003B95C File Offset: 0x00039B5C
		public void AddNodeWithUnresolvedAliases(YamlNode node)
		{
			this.nodesWithUnresolvedAliases.Add(node);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003B96C File Offset: 0x00039B6C
		public void ResolveAliases()
		{
			foreach (YamlNode yamlNode in this.nodesWithUnresolvedAliases)
			{
				yamlNode.ResolveAliases(this);
			}
		}

		// Token: 0x04000846 RID: 2118
		private readonly IDictionary<string, YamlNode> anchors = new Dictionary<string, YamlNode>();

		// Token: 0x04000847 RID: 2119
		private readonly IList<YamlNode> nodesWithUnresolvedAliases = new List<YamlNode>();
	}
}
