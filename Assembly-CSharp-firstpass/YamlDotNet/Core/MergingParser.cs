using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	// Token: 0x0200020C RID: 524
	public sealed class MergingParser : IParser
	{
		// Token: 0x06001010 RID: 4112 RVA: 0x000406BB File Offset: 0x0003E8BB
		public MergingParser(IParser innerParser)
		{
			this._innerParser = innerParser;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000406DC File Offset: 0x0003E8DC
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000406E4 File Offset: 0x0003E8E4
		public ParsingEvent Current { get; private set; }

		// Token: 0x06001013 RID: 4115 RVA: 0x000406F0 File Offset: 0x0003E8F0
		public bool MoveNext()
		{
			if (this._currentIndex < 0)
			{
				while (this._innerParser.MoveNext())
				{
					this._allEvents.Add(this._innerParser.Current);
				}
				for (int i = this._allEvents.Count - 2; i >= 0; i--)
				{
					Scalar scalar = this._allEvents[i] as Scalar;
					if (scalar != null && scalar.Value == "<<")
					{
						AnchorAlias anchorAlias = this._allEvents[i + 1] as AnchorAlias;
						if (anchorAlias == null)
						{
							if (this._allEvents[i + 1] is SequenceStart)
							{
								List<IEnumerable<ParsingEvent>> list = new List<IEnumerable<ParsingEvent>>();
								bool flag = false;
								for (int j = i + 2; j < this._allEvents.Count; j++)
								{
									anchorAlias = this._allEvents[j] as AnchorAlias;
									if (anchorAlias != null)
									{
										list.Add(this.GetMappingEvents(anchorAlias.Value));
									}
									else if (this._allEvents[j] is SequenceEnd)
									{
										this._allEvents.RemoveRange(i, j - i + 1);
										this._allEvents.InsertRange(i, list.SelectMany((IEnumerable<ParsingEvent> e) => e));
										flag = true;
										break;
									}
								}
								if (flag)
								{
									goto IL_19D;
								}
							}
							throw new SemanticErrorException(scalar.Start, scalar.End, "Unrecognized merge key pattern");
						}
						IEnumerable<ParsingEvent> mappingEvents = this.GetMappingEvents(anchorAlias.Value);
						this._allEvents.RemoveRange(i, 2);
						this._allEvents.InsertRange(i, mappingEvents);
					}
					IL_19D:;
				}
			}
			int num = this._currentIndex + 1;
			if (num < this._allEvents.Count)
			{
				this.Current = this._allEvents[num];
				this._currentIndex = num;
				return true;
			}
			return false;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x000408D8 File Offset: 0x0003EAD8
		private IEnumerable<ParsingEvent> GetMappingEvents(string mappingAlias)
		{
			MergingParser.ParsingEventCloner cloner = new MergingParser.ParsingEventCloner();
			int nesting = 0;
			return (from e in this._allEvents.SkipWhile(delegate(ParsingEvent e)
				{
					MappingStart mappingStart = e as MappingStart;
					return mappingStart == null || mappingStart.Anchor != mappingAlias;
				}).Skip(1).TakeWhile((ParsingEvent e) => (nesting += e.NestingIncrease) >= 0)
				select cloner.Clone(e)).ToList<ParsingEvent>();
		}

		// Token: 0x040008C3 RID: 2243
		private readonly List<ParsingEvent> _allEvents = new List<ParsingEvent>();

		// Token: 0x040008C4 RID: 2244
		private readonly IParser _innerParser;

		// Token: 0x040008C5 RID: 2245
		private int _currentIndex = -1;

		// Token: 0x02000A68 RID: 2664
		private class ParsingEventCloner : IParsingEventVisitor
		{
			// Token: 0x060055B2 RID: 21938 RVA: 0x0009F3EA File Offset: 0x0009D5EA
			public ParsingEvent Clone(ParsingEvent e)
			{
				e.Accept(this);
				return this.clonedEvent;
			}

			// Token: 0x060055B3 RID: 21939 RVA: 0x0009F3F9 File Offset: 0x0009D5F9
			void IParsingEventVisitor.Visit(AnchorAlias e)
			{
				this.clonedEvent = new AnchorAlias(e.Value, e.Start, e.End);
			}

			// Token: 0x060055B4 RID: 21940 RVA: 0x0009F418 File Offset: 0x0009D618
			void IParsingEventVisitor.Visit(StreamStart e)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060055B5 RID: 21941 RVA: 0x0009F41F File Offset: 0x0009D61F
			void IParsingEventVisitor.Visit(StreamEnd e)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060055B6 RID: 21942 RVA: 0x0009F426 File Offset: 0x0009D626
			void IParsingEventVisitor.Visit(DocumentStart e)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060055B7 RID: 21943 RVA: 0x0009F42D File Offset: 0x0009D62D
			void IParsingEventVisitor.Visit(DocumentEnd e)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060055B8 RID: 21944 RVA: 0x0009F434 File Offset: 0x0009D634
			void IParsingEventVisitor.Visit(Scalar e)
			{
				this.clonedEvent = new Scalar(null, e.Tag, e.Value, e.Style, e.IsPlainImplicit, e.IsQuotedImplicit, e.Start, e.End);
			}

			// Token: 0x060055B9 RID: 21945 RVA: 0x0009F477 File Offset: 0x0009D677
			void IParsingEventVisitor.Visit(SequenceStart e)
			{
				this.clonedEvent = new SequenceStart(null, e.Tag, e.IsImplicit, e.Style, e.Start, e.End);
			}

			// Token: 0x060055BA RID: 21946 RVA: 0x0009F4A3 File Offset: 0x0009D6A3
			void IParsingEventVisitor.Visit(SequenceEnd e)
			{
				this.clonedEvent = new SequenceEnd(e.Start, e.End);
			}

			// Token: 0x060055BB RID: 21947 RVA: 0x0009F4BC File Offset: 0x0009D6BC
			void IParsingEventVisitor.Visit(MappingStart e)
			{
				this.clonedEvent = new MappingStart(null, e.Tag, e.IsImplicit, e.Style, e.Start, e.End);
			}

			// Token: 0x060055BC RID: 21948 RVA: 0x0009F4E8 File Offset: 0x0009D6E8
			void IParsingEventVisitor.Visit(MappingEnd e)
			{
				this.clonedEvent = new MappingEnd(e.Start, e.End);
			}

			// Token: 0x060055BD RID: 21949 RVA: 0x0009F501 File Offset: 0x0009D701
			void IParsingEventVisitor.Visit(Comment e)
			{
				throw new NotSupportedException();
			}

			// Token: 0x04002374 RID: 9076
			private ParsingEvent clonedEvent;
		}
	}
}
