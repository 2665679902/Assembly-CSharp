using System;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001DF RID: 479
	public interface IYamlVisitor
	{
		// Token: 0x06000E6F RID: 3695
		void Visit(YamlStream stream);

		// Token: 0x06000E70 RID: 3696
		void Visit(YamlDocument document);

		// Token: 0x06000E71 RID: 3697
		void Visit(YamlScalarNode scalar);

		// Token: 0x06000E72 RID: 3698
		void Visit(YamlSequenceNode sequence);

		// Token: 0x06000E73 RID: 3699
		void Visit(YamlMappingNode mapping);
	}
}
