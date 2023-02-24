using System;
using System.Collections.Generic;
using System.Globalization;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	public class YamlDocument
	{
		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0003BA84 File Offset: 0x00039C84
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0003BA8C File Offset: 0x00039C8C
		public YamlNode RootNode { get; private set; }

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003BA95 File Offset: 0x00039C95
		public YamlDocument(YamlNode rootNode)
		{
			this.RootNode = rootNode;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0003BAA4 File Offset: 0x00039CA4
		public YamlDocument(string rootNode)
		{
			this.RootNode = new YamlScalarNode(rootNode);
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x0003BAB8 File Offset: 0x00039CB8
		internal YamlDocument(IParser parser)
		{
			DocumentLoadingState documentLoadingState = new DocumentLoadingState();
			parser.Expect<DocumentStart>();
			while (!parser.Accept<DocumentEnd>())
			{
				Debug.Assert(this.RootNode == null);
				this.RootNode = YamlNode.ParseNode(parser, documentLoadingState);
				if (this.RootNode is YamlAliasNode)
				{
					throw new YamlException();
				}
			}
			documentLoadingState.ResolveAliases();
			parser.Expect<DocumentEnd>();
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x0003BB1D File Offset: 0x00039D1D
		private void AssignAnchors()
		{
			new YamlDocument.AnchorAssigningVisitor().AssignAnchors(this);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x0003BB2A File Offset: 0x00039D2A
		internal void Save(IEmitter emitter, bool assignAnchors = true)
		{
			if (assignAnchors)
			{
				this.AssignAnchors();
			}
			emitter.Emit(new DocumentStart());
			this.RootNode.Save(emitter, new EmitterState());
			emitter.Emit(new DocumentEnd(false));
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x0003BB5D File Offset: 0x00039D5D
		public void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0003BB66 File Offset: 0x00039D66
		public IEnumerable<YamlNode> AllNodes
		{
			get
			{
				return this.RootNode.AllNodes;
			}
		}

		// Token: 0x02000A59 RID: 2649
		private class AnchorAssigningVisitor : YamlVisitorBase
		{
			// Token: 0x06005555 RID: 21845 RVA: 0x0009E918 File Offset: 0x0009CB18
			public void AssignAnchors(YamlDocument document)
			{
				this.existingAnchors.Clear();
				this.visitedNodes.Clear();
				document.Accept(this);
				Random random = new Random();
				foreach (KeyValuePair<YamlNode, bool> keyValuePair in this.visitedNodes)
				{
					if (keyValuePair.Value)
					{
						string text;
						if (!string.IsNullOrEmpty(keyValuePair.Key.Anchor) && !this.existingAnchors.Contains(keyValuePair.Key.Anchor))
						{
							text = keyValuePair.Key.Anchor;
						}
						else
						{
							do
							{
								text = random.Next().ToString(CultureInfo.InvariantCulture);
							}
							while (this.existingAnchors.Contains(text));
						}
						this.existingAnchors.Add(text);
						keyValuePair.Key.Anchor = text;
					}
				}
			}

			// Token: 0x06005556 RID: 21846 RVA: 0x0009EA10 File Offset: 0x0009CC10
			private bool VisitNodeAndFindDuplicates(YamlNode node)
			{
				bool flag;
				if (this.visitedNodes.TryGetValue(node, out flag))
				{
					if (!flag)
					{
						this.visitedNodes[node] = true;
					}
					return !flag;
				}
				this.visitedNodes.Add(node, false);
				return false;
			}

			// Token: 0x06005557 RID: 21847 RVA: 0x0009EA50 File Offset: 0x0009CC50
			public override void Visit(YamlScalarNode scalar)
			{
				this.VisitNodeAndFindDuplicates(scalar);
			}

			// Token: 0x06005558 RID: 21848 RVA: 0x0009EA5A File Offset: 0x0009CC5A
			public override void Visit(YamlMappingNode mapping)
			{
				if (!this.VisitNodeAndFindDuplicates(mapping))
				{
					base.Visit(mapping);
				}
			}

			// Token: 0x06005559 RID: 21849 RVA: 0x0009EA6C File Offset: 0x0009CC6C
			public override void Visit(YamlSequenceNode sequence)
			{
				if (!this.VisitNodeAndFindDuplicates(sequence))
				{
					base.Visit(sequence);
				}
			}

			// Token: 0x04002339 RID: 9017
			private readonly HashSet<string> existingAnchors = new HashSet<string>();

			// Token: 0x0400233A RID: 9018
			private readonly Dictionary<YamlNode, bool> visitedNodes = new Dictionary<YamlNode, bool>();
		}
	}
}
