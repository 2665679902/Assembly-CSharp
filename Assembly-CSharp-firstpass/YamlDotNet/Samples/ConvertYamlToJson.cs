using System;
using System.IO;
using YamlDotNet.Samples.Helpers;
using YamlDotNet.Serialization;

namespace YamlDotNet.Samples
{
	// Token: 0x020001EE RID: 494
	public class ConvertYamlToJson
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		public ConvertYamlToJson(ITestOutputHelper output)
		{
			this.output = output;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003D1B8 File Offset: 0x0003B3B8
		[Sample(Title = "Convert YAML to JSON", Description = "Shows how to convert a YAML document to JSON.")]
		public void Main()
		{
			StringReader stringReader = new StringReader("\nscalar: a scalar\nsequence:\n  - one\n  - two\n");
			object obj = new DeserializerBuilder().Build().Deserialize(stringReader);
			string text = new SerializerBuilder().JsonCompatible().Build().Serialize(obj);
			this.output.WriteLine(text);
		}

		// Token: 0x04000863 RID: 2147
		private readonly ITestOutputHelper output;
	}
}
