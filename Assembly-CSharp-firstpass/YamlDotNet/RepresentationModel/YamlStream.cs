using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001E8 RID: 488
	[Serializable]
	public class YamlStream : IEnumerable<YamlDocument>, IEnumerable
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0003CA89 File Offset: 0x0003AC89
		public IList<YamlDocument> Documents
		{
			get
			{
				return this.documents;
			}
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0003CA91 File Offset: 0x0003AC91
		public YamlStream()
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0003CAA4 File Offset: 0x0003ACA4
		public YamlStream(params YamlDocument[] documents)
			: this(documents)
		{
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0003CAB0 File Offset: 0x0003ACB0
		public YamlStream(IEnumerable<YamlDocument> documents)
		{
			foreach (YamlDocument yamlDocument in documents)
			{
				this.documents.Add(yamlDocument);
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0003CB10 File Offset: 0x0003AD10
		public void Add(YamlDocument document)
		{
			this.documents.Add(document);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0003CB1E File Offset: 0x0003AD1E
		public void Load(TextReader input)
		{
			this.Load(new Parser(input));
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0003CB2C File Offset: 0x0003AD2C
		public void Load(IParser parser)
		{
			this.documents.Clear();
			parser.Expect<StreamStart>();
			while (!parser.Accept<StreamEnd>())
			{
				YamlDocument yamlDocument = new YamlDocument(parser);
				this.documents.Add(yamlDocument);
			}
			parser.Expect<StreamEnd>();
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0003CB6F File Offset: 0x0003AD6F
		public void Save(TextWriter output)
		{
			this.Save(output, true);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0003CB7C File Offset: 0x0003AD7C
		public void Save(TextWriter output, bool assignAnchors)
		{
			IEmitter emitter = new Emitter(output);
			emitter.Emit(new StreamStart());
			foreach (YamlDocument yamlDocument in this.documents)
			{
				yamlDocument.Save(emitter, assignAnchors);
			}
			emitter.Emit(new StreamEnd());
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0003CBE8 File Offset: 0x0003ADE8
		public void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0003CBF1 File Offset: 0x0003ADF1
		public IEnumerator<YamlDocument> GetEnumerator()
		{
			return this.documents.GetEnumerator();
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0003CBFE File Offset: 0x0003ADFE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400085B RID: 2139
		private readonly IList<YamlDocument> documents = new List<YamlDocument>();
	}
}
