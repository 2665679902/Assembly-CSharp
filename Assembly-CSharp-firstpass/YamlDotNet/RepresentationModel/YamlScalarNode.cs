using System;
using System.Collections.Generic;
using System.Diagnostics;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E6 RID: 486
	[DebuggerDisplay("{Value}")]
	[Serializable]
	public sealed class YamlScalarNode : YamlNode, IYamlConvertible
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0003C544 File Offset: 0x0003A744
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x0003C54C File Offset: 0x0003A74C
		public string Value { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0003C555 File Offset: 0x0003A755
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x0003C55D File Offset: 0x0003A75D
		public ScalarStyle Style { get; set; }

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0003C566 File Offset: 0x0003A766
		internal YamlScalarNode(IParser parser, DocumentLoadingState state)
		{
			this.Load(parser, state);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0003C578 File Offset: 0x0003A778
		private void Load(IParser parser, DocumentLoadingState state)
		{
			Scalar scalar = parser.Expect<Scalar>();
			base.Load(scalar, state);
			this.Value = scalar.Value;
			this.Style = scalar.Style;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0003C5AC File Offset: 0x0003A7AC
		public YamlScalarNode()
		{
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
		public YamlScalarNode(string value)
		{
			this.Value = value;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0003C5C3 File Offset: 0x0003A7C3
		internal override void ResolveAliases(DocumentLoadingState state)
		{
			throw new NotSupportedException("Resolving an alias on a scalar node does not make sense");
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0003C5CF File Offset: 0x0003A7CF
		internal override void Emit(IEmitter emitter, EmitterState state)
		{
			emitter.Emit(new Scalar(base.Anchor, base.Tag, this.Value, this.Style, base.Tag == null, false));
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0003C5FE File Offset: 0x0003A7FE
		public override void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0003C608 File Offset: 0x0003A808
		public override bool Equals(object obj)
		{
			YamlScalarNode yamlScalarNode = obj as YamlScalarNode;
			return yamlScalarNode != null && base.Equals(yamlScalarNode) && YamlNode.SafeEquals(this.Value, yamlScalarNode.Value);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0003C63B File Offset: 0x0003A83B
		public override int GetHashCode()
		{
			return YamlNode.CombineHashCodes(base.GetHashCode(), YamlNode.GetHashCode(this.Value));
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0003C653 File Offset: 0x0003A853
		public static explicit operator string(YamlScalarNode value)
		{
			return value.Value;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0003C65B File Offset: 0x0003A85B
		internal override string ToString(RecursionLevel level)
		{
			return this.Value;
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0003C663 File Offset: 0x0003A863
		internal override IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level)
		{
			yield return this;
			yield break;
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0003C673 File Offset: 0x0003A873
		public override YamlNodeType NodeType
		{
			get
			{
				return YamlNodeType.Scalar;
			}
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0003C676 File Offset: 0x0003A876
		void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
		{
			this.Load(parser, new DocumentLoadingState());
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0003C684 File Offset: 0x0003A884
		void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
		{
			this.Emit(emitter, new EmitterState());
		}
	}
}
