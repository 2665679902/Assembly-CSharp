using System;
using System.Collections.Generic;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E9 RID: 489
	[Obsolete("Use YamlVisitorBase")]
	public abstract class YamlVisitor : IYamlVisitor
	{
		// Token: 0x06000EF8 RID: 3832 RVA: 0x0003CC06 File Offset: 0x0003AE06
		protected virtual void Visit(YamlStream stream)
		{
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0003CC08 File Offset: 0x0003AE08
		protected virtual void Visited(YamlStream stream)
		{
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0003CC0A File Offset: 0x0003AE0A
		protected virtual void Visit(YamlDocument document)
		{
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0003CC0C File Offset: 0x0003AE0C
		protected virtual void Visited(YamlDocument document)
		{
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0003CC0E File Offset: 0x0003AE0E
		protected virtual void Visit(YamlScalarNode scalar)
		{
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0003CC10 File Offset: 0x0003AE10
		protected virtual void Visited(YamlScalarNode scalar)
		{
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0003CC12 File Offset: 0x0003AE12
		protected virtual void Visit(YamlSequenceNode sequence)
		{
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0003CC14 File Offset: 0x0003AE14
		protected virtual void Visited(YamlSequenceNode sequence)
		{
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0003CC16 File Offset: 0x0003AE16
		protected virtual void Visit(YamlMappingNode mapping)
		{
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0003CC18 File Offset: 0x0003AE18
		protected virtual void Visited(YamlMappingNode mapping)
		{
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0003CC1C File Offset: 0x0003AE1C
		protected virtual void VisitChildren(YamlStream stream)
		{
			foreach (YamlDocument yamlDocument in stream.Documents)
			{
				yamlDocument.Accept(this);
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0003CC68 File Offset: 0x0003AE68
		protected virtual void VisitChildren(YamlDocument document)
		{
			if (document.RootNode != null)
			{
				document.RootNode.Accept(this);
			}
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0003CC80 File Offset: 0x0003AE80
		protected virtual void VisitChildren(YamlSequenceNode sequence)
		{
			foreach (YamlNode yamlNode in sequence.Children)
			{
				yamlNode.Accept(this);
			}
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0003CCCC File Offset: 0x0003AECC
		protected virtual void VisitChildren(YamlMappingNode mapping)
		{
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in mapping.Children)
			{
				keyValuePair.Key.Accept(this);
				keyValuePair.Value.Accept(this);
			}
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0003CD2C File Offset: 0x0003AF2C
		void IYamlVisitor.Visit(YamlStream stream)
		{
			this.Visit(stream);
			this.VisitChildren(stream);
			this.Visited(stream);
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0003CD43 File Offset: 0x0003AF43
		void IYamlVisitor.Visit(YamlDocument document)
		{
			this.Visit(document);
			this.VisitChildren(document);
			this.Visited(document);
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0003CD5A File Offset: 0x0003AF5A
		void IYamlVisitor.Visit(YamlScalarNode scalar)
		{
			this.Visit(scalar);
			this.Visited(scalar);
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0003CD6A File Offset: 0x0003AF6A
		void IYamlVisitor.Visit(YamlSequenceNode sequence)
		{
			this.Visit(sequence);
			this.VisitChildren(sequence);
			this.Visited(sequence);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003CD81 File Offset: 0x0003AF81
		void IYamlVisitor.Visit(YamlMappingNode mapping)
		{
			this.Visit(mapping);
			this.VisitChildren(mapping);
			this.Visited(mapping);
		}
	}
}
