using System;
using System.Collections.Generic;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001EA RID: 490
	public abstract class YamlVisitorBase : IYamlVisitor
	{
		// Token: 0x06000F0C RID: 3852 RVA: 0x0003CDA0 File Offset: 0x0003AFA0
		public virtual void Visit(YamlStream stream)
		{
			this.VisitChildren(stream);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0003CDA9 File Offset: 0x0003AFA9
		public virtual void Visit(YamlDocument document)
		{
			this.VisitChildren(document);
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0003CDB2 File Offset: 0x0003AFB2
		public virtual void Visit(YamlScalarNode scalar)
		{
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0003CDB4 File Offset: 0x0003AFB4
		public virtual void Visit(YamlSequenceNode sequence)
		{
			this.VisitChildren(sequence);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0003CDBD File Offset: 0x0003AFBD
		public virtual void Visit(YamlMappingNode mapping)
		{
			this.VisitChildren(mapping);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0003CDC6 File Offset: 0x0003AFC6
		protected virtual void VisitPair(YamlNode key, YamlNode value)
		{
			key.Accept(this);
			value.Accept(this);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0003CDD8 File Offset: 0x0003AFD8
		protected virtual void VisitChildren(YamlStream stream)
		{
			foreach (YamlDocument yamlDocument in stream.Documents)
			{
				yamlDocument.Accept(this);
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0003CE24 File Offset: 0x0003B024
		protected virtual void VisitChildren(YamlDocument document)
		{
			if (document.RootNode != null)
			{
				document.RootNode.Accept(this);
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0003CE3C File Offset: 0x0003B03C
		protected virtual void VisitChildren(YamlSequenceNode sequence)
		{
			foreach (YamlNode yamlNode in sequence.Children)
			{
				yamlNode.Accept(this);
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0003CE88 File Offset: 0x0003B088
		protected virtual void VisitChildren(YamlMappingNode mapping)
		{
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in mapping.Children)
			{
				this.VisitPair(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}
}
