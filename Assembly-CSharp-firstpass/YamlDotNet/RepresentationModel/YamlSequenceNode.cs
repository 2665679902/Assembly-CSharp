using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E7 RID: 487
	[DebuggerDisplay("Count = {children.Count}")]
	[Serializable]
	public sealed class YamlSequenceNode : YamlNode, IEnumerable<YamlNode>, IEnumerable, IYamlConvertible
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0003C692 File Offset: 0x0003A892
		public IList<YamlNode> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0003C69A File Offset: 0x0003A89A
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x0003C6A2 File Offset: 0x0003A8A2
		public SequenceStyle Style { get; set; }

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0003C6AB File Offset: 0x0003A8AB
		internal YamlSequenceNode(IParser parser, DocumentLoadingState state)
		{
			this.Load(parser, state);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0003C6C8 File Offset: 0x0003A8C8
		private void Load(IParser parser, DocumentLoadingState state)
		{
			SequenceStart sequenceStart = parser.Expect<SequenceStart>();
			base.Load(sequenceStart, state);
			this.Style = sequenceStart.Style;
			bool flag = false;
			while (!parser.Accept<SequenceEnd>())
			{
				YamlNode yamlNode = YamlNode.ParseNode(parser, state);
				this.children.Add(yamlNode);
				flag |= yamlNode is YamlAliasNode;
			}
			if (flag)
			{
				state.AddNodeWithUnresolvedAliases(this);
			}
			parser.Expect<SequenceEnd>();
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0003C72D File Offset: 0x0003A92D
		public YamlSequenceNode()
		{
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0003C740 File Offset: 0x0003A940
		public YamlSequenceNode(params YamlNode[] children)
			: this(children)
		{
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003C74C File Offset: 0x0003A94C
		public YamlSequenceNode(IEnumerable<YamlNode> children)
		{
			foreach (YamlNode yamlNode in children)
			{
				this.children.Add(yamlNode);
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0003C7AC File Offset: 0x0003A9AC
		public void Add(YamlNode child)
		{
			this.children.Add(child);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0003C7BA File Offset: 0x0003A9BA
		public void Add(string child)
		{
			this.children.Add(new YamlScalarNode(child));
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0003C7D0 File Offset: 0x0003A9D0
		internal override void ResolveAliases(DocumentLoadingState state)
		{
			for (int i = 0; i < this.children.Count; i++)
			{
				if (this.children[i] is YamlAliasNode)
				{
					this.children[i] = state.GetNode(this.children[i].Anchor, true, this.children[i].Start, this.children[i].End);
				}
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0003C84C File Offset: 0x0003AA4C
		internal override void Emit(IEmitter emitter, EmitterState state)
		{
			emitter.Emit(new SequenceStart(base.Anchor, base.Tag, true, this.Style));
			foreach (YamlNode yamlNode in this.children)
			{
				yamlNode.Save(emitter, state);
			}
			emitter.Emit(new SequenceEnd());
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0003C8C4 File Offset: 0x0003AAC4
		public override void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0003C8D0 File Offset: 0x0003AAD0
		public override bool Equals(object obj)
		{
			YamlSequenceNode yamlSequenceNode = obj as YamlSequenceNode;
			if (yamlSequenceNode == null || !base.Equals(yamlSequenceNode) || this.children.Count != yamlSequenceNode.children.Count)
			{
				return false;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				if (!YamlNode.SafeEquals(this.children[i], yamlSequenceNode.children[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0003C944 File Offset: 0x0003AB44
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			foreach (YamlNode yamlNode in this.children)
			{
				num = YamlNode.CombineHashCodes(num, YamlNode.GetHashCode(yamlNode));
			}
			return num;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0003C9A0 File Offset: 0x0003ABA0
		internal override IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level)
		{
			level.Increment();
			yield return this;
			foreach (YamlNode yamlNode in this.children)
			{
				foreach (YamlNode yamlNode2 in yamlNode.SafeAllNodes(level))
				{
					yield return yamlNode2;
				}
				IEnumerator<YamlNode> enumerator2 = null;
			}
			IEnumerator<YamlNode> enumerator = null;
			level.Decrement();
			yield break;
			yield break;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0003C9B7 File Offset: 0x0003ABB7
		public override YamlNodeType NodeType
		{
			get
			{
				return YamlNodeType.Sequence;
			}
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0003C9BC File Offset: 0x0003ABBC
		internal override string ToString(RecursionLevel level)
		{
			if (!level.TryIncrement())
			{
				return "WARNING! INFINITE RECURSION!";
			}
			StringBuilder stringBuilder = new StringBuilder("[ ");
			foreach (YamlNode yamlNode in this.children)
			{
				if (stringBuilder.Length > 2)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(yamlNode.ToString(level));
			}
			stringBuilder.Append(" ]");
			level.Decrement();
			return stringBuilder.ToString();
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0003CA58 File Offset: 0x0003AC58
		public IEnumerator<YamlNode> GetEnumerator()
		{
			return this.Children.GetEnumerator();
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0003CA65 File Offset: 0x0003AC65
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0003CA6D File Offset: 0x0003AC6D
		void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
		{
			this.Load(parser, new DocumentLoadingState());
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0003CA7B File Offset: 0x0003AC7B
		void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
		{
			this.Emit(emitter, new EmitterState());
		}

		// Token: 0x04000859 RID: 2137
		private readonly IList<YamlNode> children = new List<YamlNode>();
	}
}
