using System;

namespace YamlDotNet.Core.Events
{
	// Token: 0x02000236 RID: 566
	public interface IParsingEventVisitor
	{
		// Token: 0x06001106 RID: 4358
		void Visit(AnchorAlias e);

		// Token: 0x06001107 RID: 4359
		void Visit(StreamStart e);

		// Token: 0x06001108 RID: 4360
		void Visit(StreamEnd e);

		// Token: 0x06001109 RID: 4361
		void Visit(DocumentStart e);

		// Token: 0x0600110A RID: 4362
		void Visit(DocumentEnd e);

		// Token: 0x0600110B RID: 4363
		void Visit(Scalar e);

		// Token: 0x0600110C RID: 4364
		void Visit(SequenceStart e);

		// Token: 0x0600110D RID: 4365
		void Visit(SequenceEnd e);

		// Token: 0x0600110E RID: 4366
		void Visit(MappingStart e);

		// Token: 0x0600110F RID: 4367
		void Visit(MappingEnd e);

		// Token: 0x06001110 RID: 4368
		void Visit(Comment e);
	}
}
