using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Core.Events;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	// Token: 0x0200020D RID: 525
	public class Parser : IParser
	{
		// Token: 0x06001015 RID: 4117 RVA: 0x00040948 File Offset: 0x0003EB48
		private Token GetCurrentToken()
		{
			if (this.currentToken == null)
			{
				while (this.scanner.MoveNextWithoutConsuming())
				{
					this.currentToken = this.scanner.Current;
					YamlDotNet.Core.Tokens.Comment comment = this.currentToken as YamlDotNet.Core.Tokens.Comment;
					if (comment == null)
					{
						break;
					}
					this.pendingEvents.Enqueue(new YamlDotNet.Core.Events.Comment(comment.Value, comment.IsInline, comment.Start, comment.End));
				}
			}
			return this.currentToken;
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x000409BA File Offset: 0x0003EBBA
		public Parser(TextReader input)
			: this(new Scanner(input, true))
		{
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x000409C9 File Offset: 0x0003EBC9
		public Parser(IScanner scanner)
		{
			this.scanner = scanner;
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000409F9 File Offset: 0x0003EBF9
		public ParsingEvent Current
		{
			get
			{
				return this.currentEvent;
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x00040A04 File Offset: 0x0003EC04
		public bool MoveNext()
		{
			if (this.state == ParserState.StreamEnd)
			{
				this.currentEvent = null;
				return false;
			}
			if (this.pendingEvents.Count == 0)
			{
				this.pendingEvents.Enqueue(this.StateMachine());
			}
			this.currentEvent = this.pendingEvents.Dequeue();
			return true;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x00040A54 File Offset: 0x0003EC54
		private ParsingEvent StateMachine()
		{
			switch (this.state)
			{
			case ParserState.StreamStart:
				return this.ParseStreamStart();
			case ParserState.ImplicitDocumentStart:
				return this.ParseDocumentStart(true);
			case ParserState.DocumentStart:
				return this.ParseDocumentStart(false);
			case ParserState.DocumentContent:
				return this.ParseDocumentContent();
			case ParserState.DocumentEnd:
				return this.ParseDocumentEnd();
			case ParserState.BlockNode:
				return this.ParseNode(true, false);
			case ParserState.BlockNodeOrIndentlessSequence:
				return this.ParseNode(true, true);
			case ParserState.FlowNode:
				return this.ParseNode(false, false);
			case ParserState.BlockSequenceFirstEntry:
				return this.ParseBlockSequenceEntry(true);
			case ParserState.BlockSequenceEntry:
				return this.ParseBlockSequenceEntry(false);
			case ParserState.IndentlessSequenceEntry:
				return this.ParseIndentlessSequenceEntry();
			case ParserState.BlockMappingFirstKey:
				return this.ParseBlockMappingKey(true);
			case ParserState.BlockMappingKey:
				return this.ParseBlockMappingKey(false);
			case ParserState.BlockMappingValue:
				return this.ParseBlockMappingValue();
			case ParserState.FlowSequenceFirstEntry:
				return this.ParseFlowSequenceEntry(true);
			case ParserState.FlowSequenceEntry:
				return this.ParseFlowSequenceEntry(false);
			case ParserState.FlowSequenceEntryMappingKey:
				return this.ParseFlowSequenceEntryMappingKey();
			case ParserState.FlowSequenceEntryMappingValue:
				return this.ParseFlowSequenceEntryMappingValue();
			case ParserState.FlowSequenceEntryMappingEnd:
				return this.ParseFlowSequenceEntryMappingEnd();
			case ParserState.FlowMappingFirstKey:
				return this.ParseFlowMappingKey(true);
			case ParserState.FlowMappingKey:
				return this.ParseFlowMappingKey(false);
			case ParserState.FlowMappingValue:
				return this.ParseFlowMappingValue(false);
			case ParserState.FlowMappingEmptyValue:
				return this.ParseFlowMappingValue(true);
			}
			Debug.Assert(false, "Invalid state");
			throw new InvalidOperationException();
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x00040B96 File Offset: 0x0003ED96
		private void Skip()
		{
			if (this.currentToken != null)
			{
				this.currentToken = null;
				this.scanner.ConsumeCurrent();
			}
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x00040BB4 File Offset: 0x0003EDB4
		private ParsingEvent ParseStreamStart()
		{
			YamlDotNet.Core.Tokens.StreamStart streamStart = this.GetCurrentToken() as YamlDotNet.Core.Tokens.StreamStart;
			if (streamStart == null)
			{
				Token token = this.GetCurrentToken();
				throw new SemanticErrorException(token.Start, token.End, "Did not find expected <stream-start>.");
			}
			this.Skip();
			this.state = ParserState.ImplicitDocumentStart;
			return new YamlDotNet.Core.Events.StreamStart(streamStart.Start, streamStart.End);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x00040C0C File Offset: 0x0003EE0C
		private ParsingEvent ParseDocumentStart(bool isImplicit)
		{
			if (!isImplicit)
			{
				while (this.GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentEnd)
				{
					this.Skip();
				}
			}
			if (isImplicit && !(this.GetCurrentToken() is VersionDirective) && !(this.GetCurrentToken() is TagDirective) && !(this.GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentStart) && !(this.GetCurrentToken() is YamlDotNet.Core.Tokens.StreamEnd))
			{
				TagDirectiveCollection tagDirectiveCollection = new TagDirectiveCollection();
				this.ProcessDirectives(tagDirectiveCollection);
				this.states.Push(ParserState.DocumentEnd);
				this.state = ParserState.BlockNode;
				return new YamlDotNet.Core.Events.DocumentStart(null, tagDirectiveCollection, true, this.GetCurrentToken().Start, this.GetCurrentToken().End);
			}
			if (!(this.GetCurrentToken() is YamlDotNet.Core.Tokens.StreamEnd))
			{
				Mark start = this.GetCurrentToken().Start;
				TagDirectiveCollection tagDirectiveCollection2 = new TagDirectiveCollection();
				VersionDirective versionDirective = this.ProcessDirectives(tagDirectiveCollection2);
				Token token = this.GetCurrentToken();
				if (!(token is YamlDotNet.Core.Tokens.DocumentStart))
				{
					throw new SemanticErrorException(token.Start, token.End, "Did not find expected <document start>.");
				}
				this.states.Push(ParserState.DocumentEnd);
				this.state = ParserState.DocumentContent;
				ParsingEvent parsingEvent = new YamlDotNet.Core.Events.DocumentStart(versionDirective, tagDirectiveCollection2, false, start, token.End);
				this.Skip();
				return parsingEvent;
			}
			else
			{
				this.state = ParserState.StreamEnd;
				ParsingEvent parsingEvent2 = new YamlDotNet.Core.Events.StreamEnd(this.GetCurrentToken().Start, this.GetCurrentToken().End);
				if (this.scanner.MoveNextWithoutConsuming())
				{
					throw new InvalidOperationException("The scanner should contain no more tokens.");
				}
				return parsingEvent2;
			}
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x00040D58 File Offset: 0x0003EF58
		private VersionDirective ProcessDirectives(TagDirectiveCollection tags)
		{
			VersionDirective versionDirective = null;
			bool flag = false;
			VersionDirective versionDirective2;
			TagDirective tagDirective;
			for (;;)
			{
				if ((versionDirective2 = this.GetCurrentToken() as VersionDirective) != null)
				{
					if (versionDirective != null)
					{
						break;
					}
					if (versionDirective2.Version.Major != 1 || versionDirective2.Version.Minor != 1)
					{
						goto IL_49;
					}
					versionDirective = versionDirective2;
					flag = true;
				}
				else
				{
					if ((tagDirective = this.GetCurrentToken() as TagDirective) == null)
					{
						goto IL_AE;
					}
					if (tags.Contains(tagDirective.Handle))
					{
						goto Block_5;
					}
					tags.Add(tagDirective);
					flag = true;
				}
				this.Skip();
			}
			throw new SemanticErrorException(versionDirective2.Start, versionDirective2.End, "Found duplicate %YAML directive.");
			IL_49:
			throw new SemanticErrorException(versionDirective2.Start, versionDirective2.End, "Found incompatible YAML document.");
			Block_5:
			throw new SemanticErrorException(tagDirective.Start, tagDirective.End, "Found duplicate %TAG directive.");
			IL_AE:
			Parser.AddTagDirectives(tags, Constants.DefaultTagDirectives);
			if (flag)
			{
				this.tagDirectives.Clear();
			}
			Parser.AddTagDirectives(this.tagDirectives, tags);
			return versionDirective;
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x00040E3C File Offset: 0x0003F03C
		private static void AddTagDirectives(TagDirectiveCollection directives, IEnumerable<TagDirective> source)
		{
			foreach (TagDirective tagDirective in source)
			{
				if (!directives.Contains(tagDirective))
				{
					directives.Add(tagDirective);
				}
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x00040E90 File Offset: 0x0003F090
		private ParsingEvent ParseDocumentContent()
		{
			if (this.GetCurrentToken() is VersionDirective || this.GetCurrentToken() is TagDirective || this.GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentStart || this.GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentEnd || this.GetCurrentToken() is YamlDotNet.Core.Tokens.StreamEnd)
			{
				this.state = this.states.Pop();
				return Parser.ProcessEmptyScalar(this.scanner.CurrentPosition);
			}
			return this.ParseNode(true, false);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x00040F08 File Offset: 0x0003F108
		private static ParsingEvent ProcessEmptyScalar(Mark position)
		{
			return new YamlDotNet.Core.Events.Scalar(null, null, string.Empty, ScalarStyle.Plain, true, false, position, position);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x00040F1C File Offset: 0x0003F11C
		private ParsingEvent ParseNode(bool isBlock, bool isIndentlessSequence)
		{
			YamlDotNet.Core.Tokens.AnchorAlias anchorAlias = this.GetCurrentToken() as YamlDotNet.Core.Tokens.AnchorAlias;
			if (anchorAlias != null)
			{
				this.state = this.states.Pop();
				ParsingEvent parsingEvent = new YamlDotNet.Core.Events.AnchorAlias(anchorAlias.Value, anchorAlias.Start, anchorAlias.End);
				this.Skip();
				return parsingEvent;
			}
			Mark start = this.GetCurrentToken().Start;
			Anchor anchor = null;
			YamlDotNet.Core.Tokens.Tag tag = null;
			for (;;)
			{
				if (anchor == null && (anchor = this.GetCurrentToken() as Anchor) != null)
				{
					this.Skip();
				}
				else
				{
					if (tag != null || (tag = this.GetCurrentToken() as YamlDotNet.Core.Tokens.Tag) == null)
					{
						break;
					}
					this.Skip();
				}
			}
			string text = null;
			if (tag != null)
			{
				if (string.IsNullOrEmpty(tag.Handle))
				{
					text = tag.Suffix;
				}
				else
				{
					if (!this.tagDirectives.Contains(tag.Handle))
					{
						throw new SemanticErrorException(tag.Start, tag.End, "While parsing a node, find undefined tag handle.");
					}
					text = this.tagDirectives[tag.Handle].Prefix + tag.Suffix;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				text = null;
			}
			string text2 = ((anchor != null) ? (string.IsNullOrEmpty(anchor.Value) ? null : anchor.Value) : null);
			bool flag = string.IsNullOrEmpty(text);
			if (isIndentlessSequence && this.GetCurrentToken() is BlockEntry)
			{
				this.state = ParserState.IndentlessSequenceEntry;
				return new SequenceStart(text2, text, flag, SequenceStyle.Block, start, this.GetCurrentToken().End);
			}
			YamlDotNet.Core.Tokens.Scalar scalar = this.GetCurrentToken() as YamlDotNet.Core.Tokens.Scalar;
			if (scalar != null)
			{
				bool flag2 = false;
				bool flag3 = false;
				if ((scalar.Style == ScalarStyle.Plain && text == null) || text == "!")
				{
					flag2 = true;
				}
				else if (text == null)
				{
					flag3 = true;
				}
				this.state = this.states.Pop();
				ParsingEvent parsingEvent2 = new YamlDotNet.Core.Events.Scalar(text2, text, scalar.Value, scalar.Style, flag2, flag3, start, scalar.End);
				this.Skip();
				return parsingEvent2;
			}
			FlowSequenceStart flowSequenceStart = this.GetCurrentToken() as FlowSequenceStart;
			if (flowSequenceStart != null)
			{
				this.state = ParserState.FlowSequenceFirstEntry;
				return new SequenceStart(text2, text, flag, SequenceStyle.Flow, start, flowSequenceStart.End);
			}
			FlowMappingStart flowMappingStart = this.GetCurrentToken() as FlowMappingStart;
			if (flowMappingStart != null)
			{
				this.state = ParserState.FlowMappingFirstKey;
				return new MappingStart(text2, text, flag, MappingStyle.Flow, start, flowMappingStart.End);
			}
			if (isBlock)
			{
				BlockSequenceStart blockSequenceStart = this.GetCurrentToken() as BlockSequenceStart;
				if (blockSequenceStart != null)
				{
					this.state = ParserState.BlockSequenceFirstEntry;
					return new SequenceStart(text2, text, flag, SequenceStyle.Block, start, blockSequenceStart.End);
				}
				if (this.GetCurrentToken() is BlockMappingStart)
				{
					this.state = ParserState.BlockMappingFirstKey;
					return new MappingStart(text2, text, flag, MappingStyle.Block, start, this.GetCurrentToken().End);
				}
			}
			if (text2 != null || tag != null)
			{
				this.state = this.states.Pop();
				return new YamlDotNet.Core.Events.Scalar(text2, text, string.Empty, ScalarStyle.Plain, flag, false, start, this.GetCurrentToken().End);
			}
			Token token = this.GetCurrentToken();
			throw new SemanticErrorException(token.Start, token.End, "While parsing a node, did not find expected node content.");
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x00041208 File Offset: 0x0003F408
		private ParsingEvent ParseDocumentEnd()
		{
			bool flag = true;
			Mark start = this.GetCurrentToken().Start;
			Mark mark = start;
			if (this.GetCurrentToken() is YamlDotNet.Core.Tokens.DocumentEnd)
			{
				mark = this.GetCurrentToken().End;
				this.Skip();
				flag = false;
			}
			this.state = ParserState.DocumentStart;
			return new YamlDotNet.Core.Events.DocumentEnd(flag, start, mark);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x00041258 File Offset: 0x0003F458
		private ParsingEvent ParseBlockSequenceEntry(bool isFirst)
		{
			if (isFirst)
			{
				this.GetCurrentToken();
				this.Skip();
			}
			if (this.GetCurrentToken() is BlockEntry)
			{
				Mark end = this.GetCurrentToken().End;
				this.Skip();
				if (!(this.GetCurrentToken() is BlockEntry) && !(this.GetCurrentToken() is BlockEnd))
				{
					this.states.Push(ParserState.BlockSequenceEntry);
					return this.ParseNode(true, false);
				}
				this.state = ParserState.BlockSequenceEntry;
				return Parser.ProcessEmptyScalar(end);
			}
			else
			{
				if (this.GetCurrentToken() is BlockEnd)
				{
					this.state = this.states.Pop();
					ParsingEvent parsingEvent = new SequenceEnd(this.GetCurrentToken().Start, this.GetCurrentToken().End);
					this.Skip();
					return parsingEvent;
				}
				Token token = this.GetCurrentToken();
				throw new SemanticErrorException(token.Start, token.End, "While parsing a block collection, did not find expected '-' indicator.");
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x00041330 File Offset: 0x0003F530
		private ParsingEvent ParseIndentlessSequenceEntry()
		{
			if (!(this.GetCurrentToken() is BlockEntry))
			{
				this.state = this.states.Pop();
				return new SequenceEnd(this.GetCurrentToken().Start, this.GetCurrentToken().End);
			}
			Mark end = this.GetCurrentToken().End;
			this.Skip();
			if (!(this.GetCurrentToken() is BlockEntry) && !(this.GetCurrentToken() is Key) && !(this.GetCurrentToken() is Value) && !(this.GetCurrentToken() is BlockEnd))
			{
				this.states.Push(ParserState.IndentlessSequenceEntry);
				return this.ParseNode(true, false);
			}
			this.state = ParserState.IndentlessSequenceEntry;
			return Parser.ProcessEmptyScalar(end);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x000413E4 File Offset: 0x0003F5E4
		private ParsingEvent ParseBlockMappingKey(bool isFirst)
		{
			if (isFirst)
			{
				this.GetCurrentToken();
				this.Skip();
			}
			if (this.GetCurrentToken() is Key)
			{
				Mark end = this.GetCurrentToken().End;
				this.Skip();
				if (!(this.GetCurrentToken() is Key) && !(this.GetCurrentToken() is Value) && !(this.GetCurrentToken() is BlockEnd))
				{
					this.states.Push(ParserState.BlockMappingValue);
					return this.ParseNode(true, true);
				}
				this.state = ParserState.BlockMappingValue;
				return Parser.ProcessEmptyScalar(end);
			}
			else
			{
				if (this.GetCurrentToken() is BlockEnd)
				{
					this.state = this.states.Pop();
					ParsingEvent parsingEvent = new MappingEnd(this.GetCurrentToken().Start, this.GetCurrentToken().End);
					this.Skip();
					return parsingEvent;
				}
				Token token = this.GetCurrentToken();
				throw new SemanticErrorException(token.Start, token.End, "While parsing a block mapping, did not find expected key.");
			}
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x000414CC File Offset: 0x0003F6CC
		private ParsingEvent ParseBlockMappingValue()
		{
			if (!(this.GetCurrentToken() is Value))
			{
				this.state = ParserState.BlockMappingKey;
				return Parser.ProcessEmptyScalar(this.GetCurrentToken().Start);
			}
			Mark end = this.GetCurrentToken().End;
			this.Skip();
			if (!(this.GetCurrentToken() is Key) && !(this.GetCurrentToken() is Value) && !(this.GetCurrentToken() is BlockEnd))
			{
				this.states.Push(ParserState.BlockMappingKey);
				return this.ParseNode(true, true);
			}
			this.state = ParserState.BlockMappingKey;
			return Parser.ProcessEmptyScalar(end);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0004155C File Offset: 0x0003F75C
		private ParsingEvent ParseFlowSequenceEntry(bool isFirst)
		{
			if (isFirst)
			{
				this.GetCurrentToken();
				this.Skip();
			}
			if (!(this.GetCurrentToken() is FlowSequenceEnd))
			{
				if (!isFirst)
				{
					if (!(this.GetCurrentToken() is FlowEntry))
					{
						Token token = this.GetCurrentToken();
						throw new SemanticErrorException(token.Start, token.End, "While parsing a flow sequence, did not find expected ',' or ']'.");
					}
					this.Skip();
				}
				if (this.GetCurrentToken() is Key)
				{
					this.state = ParserState.FlowSequenceEntryMappingKey;
					ParsingEvent parsingEvent = new MappingStart(null, null, true, MappingStyle.Flow);
					this.Skip();
					return parsingEvent;
				}
				if (!(this.GetCurrentToken() is FlowSequenceEnd))
				{
					this.states.Push(ParserState.FlowSequenceEntry);
					return this.ParseNode(false, false);
				}
			}
			this.state = this.states.Pop();
			ParsingEvent parsingEvent2 = new SequenceEnd(this.GetCurrentToken().Start, this.GetCurrentToken().End);
			this.Skip();
			return parsingEvent2;
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x00041638 File Offset: 0x0003F838
		private ParsingEvent ParseFlowSequenceEntryMappingKey()
		{
			if (!(this.GetCurrentToken() is Value) && !(this.GetCurrentToken() is FlowEntry) && !(this.GetCurrentToken() is FlowSequenceEnd))
			{
				this.states.Push(ParserState.FlowSequenceEntryMappingValue);
				return this.ParseNode(false, false);
			}
			Mark end = this.GetCurrentToken().End;
			this.Skip();
			this.state = ParserState.FlowSequenceEntryMappingValue;
			return Parser.ProcessEmptyScalar(end);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x000416A0 File Offset: 0x0003F8A0
		private ParsingEvent ParseFlowSequenceEntryMappingValue()
		{
			if (this.GetCurrentToken() is Value)
			{
				this.Skip();
				if (!(this.GetCurrentToken() is FlowEntry) && !(this.GetCurrentToken() is FlowSequenceEnd))
				{
					this.states.Push(ParserState.FlowSequenceEntryMappingEnd);
					return this.ParseNode(false, false);
				}
			}
			this.state = ParserState.FlowSequenceEntryMappingEnd;
			return Parser.ProcessEmptyScalar(this.GetCurrentToken().Start);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x00041708 File Offset: 0x0003F908
		private ParsingEvent ParseFlowSequenceEntryMappingEnd()
		{
			this.state = ParserState.FlowSequenceEntry;
			return new MappingEnd(this.GetCurrentToken().Start, this.GetCurrentToken().End);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x00041730 File Offset: 0x0003F930
		private ParsingEvent ParseFlowMappingKey(bool isFirst)
		{
			if (isFirst)
			{
				this.GetCurrentToken();
				this.Skip();
			}
			if (!(this.GetCurrentToken() is FlowMappingEnd))
			{
				if (!isFirst)
				{
					if (!(this.GetCurrentToken() is FlowEntry))
					{
						Token token = this.GetCurrentToken();
						throw new SemanticErrorException(token.Start, token.End, "While parsing a flow mapping,  did not find expected ',' or '}'.");
					}
					this.Skip();
				}
				if (this.GetCurrentToken() is Key)
				{
					this.Skip();
					if (!(this.GetCurrentToken() is Value) && !(this.GetCurrentToken() is FlowEntry) && !(this.GetCurrentToken() is FlowMappingEnd))
					{
						this.states.Push(ParserState.FlowMappingValue);
						return this.ParseNode(false, false);
					}
					this.state = ParserState.FlowMappingValue;
					return Parser.ProcessEmptyScalar(this.GetCurrentToken().Start);
				}
				else if (!(this.GetCurrentToken() is FlowMappingEnd))
				{
					this.states.Push(ParserState.FlowMappingEmptyValue);
					return this.ParseNode(false, false);
				}
			}
			this.state = this.states.Pop();
			ParsingEvent parsingEvent = new MappingEnd(this.GetCurrentToken().Start, this.GetCurrentToken().End);
			this.Skip();
			return parsingEvent;
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x00041854 File Offset: 0x0003FA54
		private ParsingEvent ParseFlowMappingValue(bool isEmpty)
		{
			if (isEmpty)
			{
				this.state = ParserState.FlowMappingKey;
				return Parser.ProcessEmptyScalar(this.GetCurrentToken().Start);
			}
			if (this.GetCurrentToken() is Value)
			{
				this.Skip();
				if (!(this.GetCurrentToken() is FlowEntry) && !(this.GetCurrentToken() is FlowMappingEnd))
				{
					this.states.Push(ParserState.FlowMappingKey);
					return this.ParseNode(false, false);
				}
			}
			this.state = ParserState.FlowMappingKey;
			return Parser.ProcessEmptyScalar(this.GetCurrentToken().Start);
		}

		// Token: 0x040008C7 RID: 2247
		private readonly Stack<ParserState> states = new Stack<ParserState>();

		// Token: 0x040008C8 RID: 2248
		private readonly TagDirectiveCollection tagDirectives = new TagDirectiveCollection();

		// Token: 0x040008C9 RID: 2249
		private ParserState state;

		// Token: 0x040008CA RID: 2250
		private readonly IScanner scanner;

		// Token: 0x040008CB RID: 2251
		private ParsingEvent currentEvent;

		// Token: 0x040008CC RID: 2252
		private Token currentToken;

		// Token: 0x040008CD RID: 2253
		private readonly Parser.EventQueue pendingEvents = new Parser.EventQueue();

		// Token: 0x02000A6B RID: 2667
		private class EventQueue
		{
			// Token: 0x060055C6 RID: 21958 RVA: 0x0009F594 File Offset: 0x0009D794
			public void Enqueue(ParsingEvent @event)
			{
				EventType type = @event.Type;
				if (type == EventType.StreamStart || type == EventType.DocumentStart)
				{
					this.highPriorityEvents.Enqueue(@event);
					return;
				}
				this.normalPriorityEvents.Enqueue(@event);
			}

			// Token: 0x060055C7 RID: 21959 RVA: 0x0009F5C9 File Offset: 0x0009D7C9
			public ParsingEvent Dequeue()
			{
				if (this.highPriorityEvents.Count <= 0)
				{
					return this.normalPriorityEvents.Dequeue();
				}
				return this.highPriorityEvents.Dequeue();
			}

			// Token: 0x17000E88 RID: 3720
			// (get) Token: 0x060055C8 RID: 21960 RVA: 0x0009F5F0 File Offset: 0x0009D7F0
			public int Count
			{
				get
				{
					return this.highPriorityEvents.Count + this.normalPriorityEvents.Count;
				}
			}

			// Token: 0x0400237A RID: 9082
			private readonly Queue<ParsingEvent> highPriorityEvents = new Queue<ParsingEvent>();

			// Token: 0x0400237B RID: 9083
			private readonly Queue<ParsingEvent> normalPriorityEvents = new Queue<ParsingEvent>();
		}
	}
}
