using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Samples.Helpers;
using YamlDotNet.Serialization;

namespace YamlDotNet.Samples
{
	// Token: 0x020001F0 RID: 496
	public class DeserializingMultipleDocuments
	{
		// Token: 0x06000F3F RID: 3903 RVA: 0x0003D44C File Offset: 0x0003B64C
		public DeserializingMultipleDocuments(ITestOutputHelper output)
		{
			this.output = output;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0003D45C File Offset: 0x0003B65C
		[Sample(Title = "Deserializing multiple documents", Description = "Explains how to load multiple YAML documents from a stream.")]
		public void Main()
		{
			TextReader textReader = new StringReader("---\n- Prisoner\n- Goblet\n- Phoenix\n---\n- Memoirs\n- Snow \n- Ghost\t\t\n...");
			Deserializer deserializer = new DeserializerBuilder().Build();
			Parser parser = new Parser(textReader);
			parser.Expect<StreamStart>();
			while (parser.Accept<DocumentStart>())
			{
				List<string> list = deserializer.Deserialize<List<string>>(parser);
				this.output.WriteLine("## Document");
				foreach (string text in list)
				{
					this.output.WriteLine(text);
				}
			}
		}

		// Token: 0x04000866 RID: 2150
		private readonly ITestOutputHelper output;

		// Token: 0x04000867 RID: 2151
		private const string Document = "---\n- Prisoner\n- Goblet\n- Phoenix\n---\n- Memoirs\n- Snow \n- Ghost\t\t\n...";
	}
}
