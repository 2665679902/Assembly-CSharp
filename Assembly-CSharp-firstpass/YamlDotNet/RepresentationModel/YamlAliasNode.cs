using System;
using System.Collections.Generic;
using YamlDotNet.Core;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E0 RID: 480
	[Serializable]
	internal class YamlAliasNode : YamlNode
	{
		// Token: 0x06000E74 RID: 3700 RVA: 0x0003B9F1 File Offset: 0x00039BF1
		internal YamlAliasNode(string anchor)
		{
			base.Anchor = anchor;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x0003BA00 File Offset: 0x00039C00
		internal override void ResolveAliases(DocumentLoadingState state)
		{
			throw new NotSupportedException("Resolving an alias on an alias node does not make sense");
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003BA0C File Offset: 0x00039C0C
		internal override void Emit(IEmitter emitter, EmitterState state)
		{
			throw new NotSupportedException("A YamlAliasNode is an implementation detail and should never be saved.");
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003BA18 File Offset: 0x00039C18
		public override void Accept(IYamlVisitor visitor)
		{
			throw new NotSupportedException("A YamlAliasNode is an implementation detail and should never be visited.");
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003BA24 File Offset: 0x00039C24
		public override bool Equals(object obj)
		{
			YamlAliasNode yamlAliasNode = obj as YamlAliasNode;
			return yamlAliasNode != null && base.Equals(yamlAliasNode) && YamlNode.SafeEquals(base.Anchor, yamlAliasNode.Anchor);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003BA57 File Offset: 0x00039C57
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003BA5F File Offset: 0x00039C5F
		internal override string ToString(RecursionLevel level)
		{
			return "*" + base.Anchor;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0003BA71 File Offset: 0x00039C71
		internal override IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level)
		{
			yield return this;
			yield break;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0003BA81 File Offset: 0x00039C81
		public override YamlNodeType NodeType
		{
			get
			{
				return YamlNodeType.Alias;
			}
		}
	}
}
