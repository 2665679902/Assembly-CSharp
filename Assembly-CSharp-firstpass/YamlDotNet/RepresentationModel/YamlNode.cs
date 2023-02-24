using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E3 RID: 483
	[Serializable]
	public abstract class YamlNode
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x0003C2E0 File Offset: 0x0003A4E0
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x0003C2E8 File Offset: 0x0003A4E8
		public string Anchor { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x0003C2F1 File Offset: 0x0003A4F1
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x0003C2F9 File Offset: 0x0003A4F9
		public string Tag { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x0003C302 File Offset: 0x0003A502
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x0003C30A File Offset: 0x0003A50A
		public Mark Start { get; private set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003C313 File Offset: 0x0003A513
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x0003C31B File Offset: 0x0003A51B
		public Mark End { get; private set; }

		// Token: 0x06000EAA RID: 3754 RVA: 0x0003C324 File Offset: 0x0003A524
		internal void Load(NodeEvent yamlEvent, DocumentLoadingState state)
		{
			this.Tag = yamlEvent.Tag;
			if (yamlEvent.Anchor != null)
			{
				this.Anchor = yamlEvent.Anchor;
				state.AddAnchor(this);
			}
			this.Start = yamlEvent.Start;
			this.End = yamlEvent.End;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x0003C370 File Offset: 0x0003A570
		internal static YamlNode ParseNode(IParser parser, DocumentLoadingState state)
		{
			if (parser.Accept<Scalar>())
			{
				return new YamlScalarNode(parser, state);
			}
			if (parser.Accept<SequenceStart>())
			{
				return new YamlSequenceNode(parser, state);
			}
			if (parser.Accept<MappingStart>())
			{
				return new YamlMappingNode(parser, state);
			}
			if (parser.Accept<AnchorAlias>())
			{
				AnchorAlias anchorAlias = parser.Expect<AnchorAlias>();
				return state.GetNode(anchorAlias.Value, false, anchorAlias.Start, anchorAlias.End) ?? new YamlAliasNode(anchorAlias.Value);
			}
			throw new ArgumentException("The current event is of an unsupported type.", "events");
		}

		// Token: 0x06000EAC RID: 3756
		internal abstract void ResolveAliases(DocumentLoadingState state);

		// Token: 0x06000EAD RID: 3757 RVA: 0x0003C3F4 File Offset: 0x0003A5F4
		internal void Save(IEmitter emitter, EmitterState state)
		{
			if (!string.IsNullOrEmpty(this.Anchor) && !state.EmittedAnchors.Add(this.Anchor))
			{
				emitter.Emit(new AnchorAlias(this.Anchor));
				return;
			}
			this.Emit(emitter, state);
		}

		// Token: 0x06000EAE RID: 3758
		internal abstract void Emit(IEmitter emitter, EmitterState state);

		// Token: 0x06000EAF RID: 3759
		public abstract void Accept(IYamlVisitor visitor);

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0003C430 File Offset: 0x0003A630
		protected bool Equals(YamlNode other)
		{
			return YamlNode.SafeEquals(this.Tag, other.Tag);
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x0003C443 File Offset: 0x0003A643
		protected static bool SafeEquals(object first, object second)
		{
			if (first != null)
			{
				return first.Equals(second);
			}
			return second == null || second.Equals(first);
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x0003C45C File Offset: 0x0003A65C
		public override int GetHashCode()
		{
			return YamlNode.GetHashCode(this.Tag);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x0003C469 File Offset: 0x0003A669
		protected static int GetHashCode(object value)
		{
			if (value != null)
			{
				return value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x0003C476 File Offset: 0x0003A676
		protected static int CombineHashCodes(int h1, int h2)
		{
			return ((h1 << 5) + h1) ^ h2;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003C480 File Offset: 0x0003A680
		public override string ToString()
		{
			RecursionLevel recursionLevel = new RecursionLevel(1000);
			return this.ToString(recursionLevel);
		}

		// Token: 0x06000EB6 RID: 3766
		internal abstract string ToString(RecursionLevel level);

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0003C4A0 File Offset: 0x0003A6A0
		public IEnumerable<YamlNode> AllNodes
		{
			get
			{
				RecursionLevel recursionLevel = new RecursionLevel(1000);
				return this.SafeAllNodes(recursionLevel);
			}
		}

		// Token: 0x06000EB8 RID: 3768
		internal abstract IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level);

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000EB9 RID: 3769
		public abstract YamlNodeType NodeType { get; }

		// Token: 0x06000EBA RID: 3770 RVA: 0x0003C4BF File Offset: 0x0003A6BF
		public static implicit operator YamlNode(string value)
		{
			return new YamlScalarNode(value);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0003C4C7 File Offset: 0x0003A6C7
		public static implicit operator YamlNode(string[] sequence)
		{
			return new YamlSequenceNode(sequence.Select((string i) => i));
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0003C4F3 File Offset: 0x0003A6F3
		public static explicit operator string(YamlNode scalar)
		{
			return ((YamlScalarNode)scalar).Value;
		}

		// Token: 0x1700017C RID: 380
		public YamlNode this[int index]
		{
			get
			{
				return ((YamlSequenceNode)this).Children[index];
			}
		}

		// Token: 0x1700017D RID: 381
		public YamlNode this[YamlNode key]
		{
			get
			{
				return ((YamlMappingNode)this).Children[key];
			}
		}

		// Token: 0x0400084C RID: 2124
		private const int MaximumRecursionLevel = 1000;

		// Token: 0x0400084D RID: 2125
		internal const string MaximumRecursionLevelReachedToStringValue = "WARNING! INFINITE RECURSION!";
	}
}
