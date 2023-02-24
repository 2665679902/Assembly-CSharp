﻿using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Samples.Helpers;

namespace YamlDotNet.Samples
{
	// Token: 0x020001F1 RID: 497
	public class LoadingAYamlStream
	{
		// Token: 0x06000F41 RID: 3905 RVA: 0x0003D4F4 File Offset: 0x0003B6F4
		public LoadingAYamlStream(ITestOutputHelper output)
		{
			this.output = output;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0003D504 File Offset: 0x0003B704
		[Sample(Title = "Loading a YAML Stream", Description = "Explains how to load YAML using the representation model.")]
		public void Main()
		{
			StringReader stringReader = new StringReader("---\n            receipt:    Oz-Ware Purchase Invoice\n            date:        2007-08-06\n            customer:\n                given:   Dorothy\n                family:  Gale\n\n            items:\n                - part_no:   A4786\n                  descrip:   Water Bucket (Filled)\n                  price:     1.47\n                  quantity:  4\n\n                - part_no:   E1628\n                  descrip:   High Heeled \"Ruby\" Slippers\n                  price:     100.27\n                  quantity:  1\n\n            bill-to:  &id001\n                street: |\n                        123 Tornado Alley\n                        Suite 16\n                city:   East Westville\n                state:  KS\n\n            ship-to:  *id001\n\n            specialDelivery:  >\n                Follow the Yellow Brick\n                Road to the Emerald City.\n                Pay no attention to the\n                man behind the curtain.\n...");
			YamlStream yamlStream = new YamlStream();
			yamlStream.Load(stringReader);
			YamlMappingNode yamlMappingNode = (YamlMappingNode)yamlStream.Documents[0].RootNode;
			foreach (KeyValuePair<YamlNode, YamlNode> keyValuePair in yamlMappingNode.Children)
			{
				this.output.WriteLine(((YamlScalarNode)keyValuePair.Key).Value);
			}
			foreach (YamlNode yamlNode in ((YamlSequenceNode)yamlMappingNode.Children[new YamlScalarNode("items")]))
			{
				YamlMappingNode yamlMappingNode2 = (YamlMappingNode)yamlNode;
				this.output.WriteLine("{0}\t{1}", new object[]
				{
					yamlMappingNode2.Children[new YamlScalarNode("part_no")],
					yamlMappingNode2.Children[new YamlScalarNode("descrip")]
				});
			}
		}

		// Token: 0x04000868 RID: 2152
		private readonly ITestOutputHelper output;

		// Token: 0x04000869 RID: 2153
		private const string Document = "---\n            receipt:    Oz-Ware Purchase Invoice\n            date:        2007-08-06\n            customer:\n                given:   Dorothy\n                family:  Gale\n\n            items:\n                - part_no:   A4786\n                  descrip:   Water Bucket (Filled)\n                  price:     1.47\n                  quantity:  4\n\n                - part_no:   E1628\n                  descrip:   High Heeled \"Ruby\" Slippers\n                  price:     100.27\n                  quantity:  1\n\n            bill-to:  &id001\n                street: |\n                        123 Tornado Alley\n                        Suite 16\n                city:   East Westville\n                state:  KS\n\n            ship-to:  *id001\n\n            specialDelivery:  >\n                Follow the Yellow Brick\n                Road to the Emerald City.\n                Pay no attention to the\n                man behind the curtain.\n...";
	}
}
