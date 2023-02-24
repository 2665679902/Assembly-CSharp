using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.Core.Events;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	// Token: 0x020001FF RID: 511
	public class Emitter : IEmitter
	{
		// Token: 0x06000F9F RID: 3999 RVA: 0x0003E0A4 File Offset: 0x0003C2A4
		public Emitter(TextWriter output)
			: this(output, 2)
		{
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003E0AE File Offset: 0x0003C2AE
		public Emitter(TextWriter output, int bestIndent)
			: this(output, bestIndent, int.MaxValue)
		{
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003E0BD File Offset: 0x0003C2BD
		public Emitter(TextWriter output, int bestIndent, int bestWidth)
			: this(output, bestIndent, bestWidth, false)
		{
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0003E0CC File Offset: 0x0003C2CC
		public Emitter(TextWriter output, int bestIndent, int bestWidth, bool isCanonical)
		{
			if (bestIndent < 2 || bestIndent > 9)
			{
				throw new ArgumentOutOfRangeException("bestIndent", string.Format(CultureInfo.InvariantCulture, "The bestIndent parameter must be between {0} and {1}.", 2, 9));
			}
			this.bestIndent = bestIndent;
			if (bestWidth <= bestIndent * 2)
			{
				throw new ArgumentOutOfRangeException("bestWidth", "The bestWidth parameter must be greater than bestIndent * 2.");
			}
			this.bestWidth = bestWidth;
			this.isCanonical = isCanonical;
			this.output = output;
			this.outputUsesUnicodeEncoding = this.IsUnicode(output.Encoding);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0003E1A4 File Offset: 0x0003C3A4
		public void Emit(ParsingEvent @event)
		{
			this.events.Enqueue(@event);
			while (!this.NeedMoreEvents())
			{
				ParsingEvent parsingEvent = this.events.Peek();
				try
				{
					this.AnalyzeEvent(parsingEvent);
					this.StateMachine(parsingEvent);
				}
				finally
				{
					this.events.Dequeue();
				}
			}
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0003E200 File Offset: 0x0003C400
		private bool NeedMoreEvents()
		{
			if (this.events.Count == 0)
			{
				return true;
			}
			EventType type = this.events.Peek().Type;
			int num;
			if (type != EventType.DocumentStart)
			{
				if (type != EventType.SequenceStart)
				{
					if (type != EventType.MappingStart)
					{
						return false;
					}
					num = 3;
				}
				else
				{
					num = 2;
				}
			}
			else
			{
				num = 1;
			}
			if (this.events.Count > num)
			{
				return false;
			}
			int num2 = 0;
			using (Queue<ParsingEvent>.Enumerator enumerator = this.events.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current.Type)
					{
					case EventType.DocumentStart:
					case EventType.SequenceStart:
					case EventType.MappingStart:
						num2++;
						break;
					case EventType.DocumentEnd:
					case EventType.SequenceEnd:
					case EventType.MappingEnd:
						num2--;
						break;
					}
					if (num2 == 0)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003E2E0 File Offset: 0x0003C4E0
		private void AnalyzeEvent(ParsingEvent evt)
		{
			this.anchorData.anchor = null;
			this.tagData.handle = null;
			this.tagData.suffix = null;
			YamlDotNet.Core.Events.AnchorAlias anchorAlias = evt as YamlDotNet.Core.Events.AnchorAlias;
			if (anchorAlias != null)
			{
				this.AnalyzeAnchor(anchorAlias.Value, true);
				return;
			}
			NodeEvent nodeEvent = evt as NodeEvent;
			if (nodeEvent != null)
			{
				YamlDotNet.Core.Events.Scalar scalar = evt as YamlDotNet.Core.Events.Scalar;
				if (scalar != null)
				{
					this.AnalyzeScalar(scalar);
				}
				this.AnalyzeAnchor(nodeEvent.Anchor, false);
				if (!string.IsNullOrEmpty(nodeEvent.Tag) && (this.isCanonical || nodeEvent.IsCanonical))
				{
					this.AnalyzeTag(nodeEvent.Tag);
				}
			}
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003E37A File Offset: 0x0003C57A
		private void AnalyzeAnchor(string anchor, bool isAlias)
		{
			this.anchorData.anchor = anchor;
			this.anchorData.isAlias = isAlias;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0003E394 File Offset: 0x0003C594
		private void AnalyzeScalar(YamlDotNet.Core.Events.Scalar scalar)
		{
			string value = scalar.Value;
			this.scalarData.value = value;
			if (value.Length != 0)
			{
				bool flag = false;
				bool flag2 = false;
				if (value.StartsWith("---", StringComparison.Ordinal) || value.StartsWith("...", StringComparison.Ordinal))
				{
					flag = true;
					flag2 = true;
				}
				CharacterAnalyzer<StringLookAheadBuffer> characterAnalyzer = new CharacterAnalyzer<StringLookAheadBuffer>(new StringLookAheadBuffer(value));
				bool flag3 = true;
				bool flag4 = characterAnalyzer.IsWhiteBreakOrZero(1);
				bool flag5 = false;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				bool flag10 = false;
				bool flag11 = false;
				bool flag12 = false;
				bool flag13 = false;
				bool flag14 = false;
				bool flag15 = !this.ValueIsRepresentableInOutputEncoding(value);
				bool flag16 = false;
				bool flag17 = true;
				while (!characterAnalyzer.EndOfInput)
				{
					if (flag17)
					{
						if (characterAnalyzer.Check("#,[]{}&*!|>\\\"%@`'", 0))
						{
							flag = true;
							flag2 = true;
							flag9 = characterAnalyzer.Check('\'', 0);
							flag16 |= characterAnalyzer.Check('\'', 0);
						}
						if (characterAnalyzer.Check("?:", 0))
						{
							flag = true;
							if (flag4)
							{
								flag2 = true;
							}
						}
						if (characterAnalyzer.Check('-', 0) && flag4)
						{
							flag = true;
							flag2 = true;
						}
					}
					else
					{
						if (characterAnalyzer.Check(",?[]{}", 0))
						{
							flag = true;
						}
						if (characterAnalyzer.Check(':', 0))
						{
							flag = true;
							if (flag4)
							{
								flag2 = true;
							}
						}
						if (characterAnalyzer.Check('#', 0) && flag3)
						{
							flag = true;
							flag2 = true;
						}
						flag16 |= characterAnalyzer.Check('\'', 0);
					}
					if (!flag15 && !characterAnalyzer.IsPrintable(0))
					{
						flag15 = true;
					}
					if (characterAnalyzer.IsBreak(0))
					{
						flag14 = true;
					}
					if (characterAnalyzer.IsSpace(0))
					{
						if (flag17)
						{
							flag5 = true;
						}
						if (characterAnalyzer.Buffer.Position >= characterAnalyzer.Buffer.Length - 1)
						{
							flag7 = true;
						}
						if (flag13)
						{
							flag10 = true;
						}
						flag12 = true;
						flag13 = false;
					}
					else if (characterAnalyzer.IsBreak(0))
					{
						if (flag17)
						{
							flag6 = true;
						}
						if (characterAnalyzer.Buffer.Position >= characterAnalyzer.Buffer.Length - 1)
						{
							flag8 = true;
						}
						if (flag12)
						{
							flag11 = true;
						}
						flag12 = false;
						flag13 = true;
					}
					else
					{
						flag12 = false;
						flag13 = false;
					}
					flag3 = characterAnalyzer.IsWhiteBreakOrZero(0);
					characterAnalyzer.Skip(1);
					if (!characterAnalyzer.EndOfInput)
					{
						flag4 = characterAnalyzer.IsWhiteBreakOrZero(1);
					}
					flag17 = false;
				}
				this.scalarData.isFlowPlainAllowed = true;
				this.scalarData.isBlockPlainAllowed = true;
				this.scalarData.isSingleQuotedAllowed = true;
				this.scalarData.isBlockAllowed = true;
				if (flag5 || flag6 || flag7 || flag8 || flag9)
				{
					this.scalarData.isFlowPlainAllowed = false;
					this.scalarData.isBlockPlainAllowed = false;
				}
				if (flag7)
				{
					this.scalarData.isBlockAllowed = false;
				}
				if (flag10)
				{
					this.scalarData.isFlowPlainAllowed = false;
					this.scalarData.isBlockPlainAllowed = false;
					this.scalarData.isSingleQuotedAllowed = false;
				}
				if (flag11 || flag15)
				{
					this.scalarData.isFlowPlainAllowed = false;
					this.scalarData.isBlockPlainAllowed = false;
					this.scalarData.isSingleQuotedAllowed = false;
					this.scalarData.isBlockAllowed = false;
				}
				this.scalarData.isMultiline = flag14;
				if (flag14)
				{
					this.scalarData.isFlowPlainAllowed = false;
					this.scalarData.isBlockPlainAllowed = false;
				}
				if (flag)
				{
					this.scalarData.isFlowPlainAllowed = false;
				}
				if (flag2)
				{
					this.scalarData.isBlockPlainAllowed = false;
				}
				this.scalarData.hasSingleQuotes = flag16;
				return;
			}
			if (scalar.Tag == "tag:yaml.org,2002:null")
			{
				this.scalarData.isMultiline = false;
				this.scalarData.isFlowPlainAllowed = false;
				this.scalarData.isBlockPlainAllowed = true;
				this.scalarData.isSingleQuotedAllowed = false;
				this.scalarData.isBlockAllowed = false;
				return;
			}
			this.scalarData.isMultiline = false;
			this.scalarData.isFlowPlainAllowed = false;
			this.scalarData.isBlockPlainAllowed = false;
			this.scalarData.isSingleQuotedAllowed = true;
			this.scalarData.isBlockAllowed = false;
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0003E73C File Offset: 0x0003C93C
		private bool ValueIsRepresentableInOutputEncoding(string value)
		{
			if (this.outputUsesUnicodeEncoding)
			{
				return true;
			}
			bool flag;
			try
			{
				byte[] bytes = this.output.Encoding.GetBytes(value);
				flag = this.output.Encoding.GetString(bytes, 0, bytes.Length).Equals(value);
			}
			catch (EncoderFallbackException)
			{
				flag = false;
			}
			catch (ArgumentOutOfRangeException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0003E7AC File Offset: 0x0003C9AC
		private bool IsUnicode(Encoding encoding)
		{
			return encoding is UTF8Encoding || encoding is UnicodeEncoding || encoding is UTF7Encoding || encoding is UTF8Encoding;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0003E7D4 File Offset: 0x0003C9D4
		private void AnalyzeTag(string tag)
		{
			this.tagData.handle = tag;
			foreach (TagDirective tagDirective in this.tagDirectives)
			{
				if (tag.StartsWith(tagDirective.Prefix, StringComparison.Ordinal))
				{
					this.tagData.handle = tagDirective.Handle;
					this.tagData.suffix = tag.Substring(tagDirective.Prefix.Length);
					break;
				}
			}
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0003E864 File Offset: 0x0003CA64
		private void StateMachine(ParsingEvent evt)
		{
			YamlDotNet.Core.Events.Comment comment = evt as YamlDotNet.Core.Events.Comment;
			if (comment != null)
			{
				this.EmitComment(comment);
				return;
			}
			switch (this.state)
			{
			case EmitterState.StreamStart:
				this.EmitStreamStart(evt);
				return;
			case EmitterState.StreamEnd:
				throw new YamlException("Expected nothing after STREAM-END");
			case EmitterState.FirstDocumentStart:
				this.EmitDocumentStart(evt, true);
				return;
			case EmitterState.DocumentStart:
				this.EmitDocumentStart(evt, false);
				return;
			case EmitterState.DocumentContent:
				this.EmitDocumentContent(evt);
				return;
			case EmitterState.DocumentEnd:
				this.EmitDocumentEnd(evt);
				return;
			case EmitterState.FlowSequenceFirstItem:
				this.EmitFlowSequenceItem(evt, true);
				return;
			case EmitterState.FlowSequenceItem:
				this.EmitFlowSequenceItem(evt, false);
				return;
			case EmitterState.FlowMappingFirstKey:
				this.EmitFlowMappingKey(evt, true);
				return;
			case EmitterState.FlowMappingKey:
				this.EmitFlowMappingKey(evt, false);
				return;
			case EmitterState.FlowMappingSimpleValue:
				this.EmitFlowMappingValue(evt, true);
				return;
			case EmitterState.FlowMappingValue:
				this.EmitFlowMappingValue(evt, false);
				return;
			case EmitterState.BlockSequenceFirstItem:
				this.EmitBlockSequenceItem(evt, true);
				return;
			case EmitterState.BlockSequenceItem:
				this.EmitBlockSequenceItem(evt, false);
				return;
			case EmitterState.BlockMappingFirstKey:
				this.EmitBlockMappingKey(evt, true);
				return;
			case EmitterState.BlockMappingKey:
				this.EmitBlockMappingKey(evt, false);
				return;
			case EmitterState.BlockMappingSimpleValue:
				this.EmitBlockMappingValue(evt, true);
				return;
			case EmitterState.BlockMappingValue:
				this.EmitBlockMappingValue(evt, false);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0003E983 File Offset: 0x0003CB83
		private void EmitComment(YamlDotNet.Core.Events.Comment comment)
		{
			if (comment.IsInline)
			{
				this.Write(' ');
			}
			else
			{
				this.WriteIndent();
			}
			this.Write("# ");
			this.Write(comment.Value);
			this.WriteBreak('\n');
			this.isIndentation = true;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003E9C3 File Offset: 0x0003CBC3
		private void EmitStreamStart(ParsingEvent evt)
		{
			if (!(evt is YamlDotNet.Core.Events.StreamStart))
			{
				throw new ArgumentException("Expected STREAM-START.", "evt");
			}
			this.indent = -1;
			this.column = 0;
			this.isWhitespace = true;
			this.isIndentation = true;
			this.state = EmitterState.FirstDocumentStart;
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0003EA00 File Offset: 0x0003CC00
		private void EmitDocumentStart(ParsingEvent evt, bool isFirst)
		{
			YamlDotNet.Core.Events.DocumentStart documentStart = evt as YamlDotNet.Core.Events.DocumentStart;
			if (documentStart != null)
			{
				bool flag = documentStart.IsImplicit && isFirst && !this.isCanonical;
				TagDirectiveCollection tagDirectiveCollection = this.NonDefaultTagsAmong(documentStart.Tags);
				if (!isFirst && !this.isDocumentEndWritten && (documentStart.Version != null || tagDirectiveCollection.Count > 0))
				{
					this.isDocumentEndWritten = false;
					this.WriteIndicator("...", true, false, false);
					this.WriteIndent();
				}
				if (documentStart.Version != null)
				{
					this.AnalyzeVersionDirective(documentStart.Version);
					flag = false;
					this.WriteIndicator("%YAML", true, false, false);
					this.WriteIndicator(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", 1, 1), true, false, false);
					this.WriteIndent();
				}
				foreach (TagDirective tagDirective in tagDirectiveCollection)
				{
					Emitter.AppendTagDirectiveTo(tagDirective, false, this.tagDirectives);
				}
				TagDirective[] array = Constants.DefaultTagDirectives;
				for (int i = 0; i < array.Length; i++)
				{
					Emitter.AppendTagDirectiveTo(array[i], true, this.tagDirectives);
				}
				if (tagDirectiveCollection.Count > 0)
				{
					flag = false;
					array = Constants.DefaultTagDirectives;
					for (int i = 0; i < array.Length; i++)
					{
						Emitter.AppendTagDirectiveTo(array[i], true, tagDirectiveCollection);
					}
					foreach (TagDirective tagDirective2 in tagDirectiveCollection)
					{
						this.WriteIndicator("%TAG", true, false, false);
						this.WriteTagHandle(tagDirective2.Handle);
						this.WriteTagContent(tagDirective2.Prefix, true);
						this.WriteIndent();
					}
				}
				if (this.CheckEmptyDocument())
				{
					flag = false;
				}
				if (!flag)
				{
					this.WriteIndent();
					this.WriteIndicator("---", true, false, false);
					if (this.isCanonical)
					{
						this.WriteIndent();
					}
				}
				this.state = EmitterState.DocumentContent;
				return;
			}
			if (evt is YamlDotNet.Core.Events.StreamEnd)
			{
				if (this.isOpenEnded)
				{
					this.WriteIndicator("...", true, false, false);
					this.WriteIndent();
				}
				this.state = EmitterState.StreamEnd;
				return;
			}
			throw new YamlException("Expected DOCUMENT-START or STREAM-END");
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		private TagDirectiveCollection NonDefaultTagsAmong(IEnumerable<TagDirective> tagCollection)
		{
			TagDirectiveCollection tagDirectiveCollection = new TagDirectiveCollection();
			if (tagCollection == null)
			{
				return tagDirectiveCollection;
			}
			foreach (TagDirective tagDirective in tagCollection)
			{
				Emitter.AppendTagDirectiveTo(tagDirective, false, tagDirectiveCollection);
			}
			foreach (TagDirective tagDirective2 in Constants.DefaultTagDirectives)
			{
				tagDirectiveCollection.Remove(tagDirective2);
			}
			return tagDirectiveCollection;
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0003ECA4 File Offset: 0x0003CEA4
		private void AnalyzeVersionDirective(VersionDirective versionDirective)
		{
			if (versionDirective.Version.Major != 1 || versionDirective.Version.Minor != 1)
			{
				throw new YamlException("Incompatible %YAML directive");
			}
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0003ECCD File Offset: 0x0003CECD
		private static void AppendTagDirectiveTo(TagDirective value, bool allowDuplicates, TagDirectiveCollection tagDirectives)
		{
			if (tagDirectives.Contains(value))
			{
				if (!allowDuplicates)
				{
					throw new YamlException("Duplicate %TAG directive.");
				}
			}
			else
			{
				tagDirectives.Add(value);
			}
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003ECED File Offset: 0x0003CEED
		private void EmitDocumentContent(ParsingEvent evt)
		{
			this.states.Push(EmitterState.DocumentEnd);
			this.EmitNode(evt, true, false, false);
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0003ED08 File Offset: 0x0003CF08
		private void EmitNode(ParsingEvent evt, bool isRoot, bool isMapping, bool isSimpleKey)
		{
			this.isRootContext = isRoot;
			this.isMappingContext = isMapping;
			this.isSimpleKeyContext = isSimpleKey;
			switch (evt.Type)
			{
			case EventType.Alias:
				this.EmitAlias();
				return;
			case EventType.Scalar:
				this.EmitScalar(evt);
				return;
			case EventType.SequenceStart:
				this.EmitSequenceStart(evt);
				return;
			case EventType.MappingStart:
				this.EmitMappingStart(evt);
				return;
			}
			throw new YamlException(string.Format("Expected SCALAR, SEQUENCE-START, MAPPING-START, or ALIAS, got {0}", evt.Type));
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0003ED89 File Offset: 0x0003CF89
		private void EmitAlias()
		{
			this.ProcessAnchor();
			this.state = this.states.Pop();
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0003EDA4 File Offset: 0x0003CFA4
		private void EmitScalar(ParsingEvent evt)
		{
			this.SelectScalarStyle(evt);
			this.ProcessAnchor();
			this.ProcessTag();
			this.IncreaseIndent(true, false);
			this.ProcessScalar();
			this.indent = this.indents.Pop();
			this.state = this.states.Pop();
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003EDF4 File Offset: 0x0003CFF4
		private void SelectScalarStyle(ParsingEvent evt)
		{
			YamlDotNet.Core.Events.Scalar scalar = (YamlDotNet.Core.Events.Scalar)evt;
			ScalarStyle scalarStyle = scalar.Style;
			bool flag = this.tagData.handle == null && this.tagData.suffix == null;
			if (flag && !scalar.IsPlainImplicit && !scalar.IsQuotedImplicit)
			{
				throw new YamlException("Neither tag nor isImplicit flags are specified.");
			}
			if (scalarStyle == ScalarStyle.Any)
			{
				scalarStyle = (this.scalarData.isMultiline ? ScalarStyle.Folded : ScalarStyle.Plain);
			}
			if (this.isCanonical)
			{
				scalarStyle = ScalarStyle.DoubleQuoted;
			}
			if (this.isSimpleKeyContext && this.scalarData.isMultiline)
			{
				scalarStyle = ScalarStyle.DoubleQuoted;
			}
			if (scalarStyle == ScalarStyle.Plain)
			{
				if ((this.flowLevel != 0 && !this.scalarData.isFlowPlainAllowed) || (this.flowLevel == 0 && !this.scalarData.isBlockPlainAllowed))
				{
					scalarStyle = ((this.scalarData.isSingleQuotedAllowed && !this.scalarData.hasSingleQuotes) ? ScalarStyle.SingleQuoted : ScalarStyle.DoubleQuoted);
				}
				if (string.IsNullOrEmpty(this.scalarData.value) && (this.flowLevel != 0 || this.isSimpleKeyContext))
				{
					scalarStyle = ScalarStyle.SingleQuoted;
				}
				if (flag && !scalar.IsPlainImplicit)
				{
					scalarStyle = ScalarStyle.SingleQuoted;
				}
			}
			if (scalarStyle == ScalarStyle.SingleQuoted && !this.scalarData.isSingleQuotedAllowed)
			{
				scalarStyle = ScalarStyle.DoubleQuoted;
			}
			if ((scalarStyle == ScalarStyle.Literal || scalarStyle == ScalarStyle.Folded) && (!this.scalarData.isBlockAllowed || this.flowLevel != 0 || this.isSimpleKeyContext))
			{
				scalarStyle = ScalarStyle.DoubleQuoted;
			}
			this.scalarData.style = scalarStyle;
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0003EF48 File Offset: 0x0003D148
		private void ProcessScalar()
		{
			switch (this.scalarData.style)
			{
			case ScalarStyle.Plain:
				this.WritePlainScalar(this.scalarData.value, !this.isSimpleKeyContext);
				return;
			case ScalarStyle.SingleQuoted:
				this.WriteSingleQuotedScalar(this.scalarData.value, !this.isSimpleKeyContext);
				return;
			case ScalarStyle.DoubleQuoted:
				this.WriteDoubleQuotedScalar(this.scalarData.value, !this.isSimpleKeyContext);
				return;
			case ScalarStyle.Literal:
				this.WriteLiteralScalar(this.scalarData.value);
				return;
			case ScalarStyle.Folded:
				this.WriteFoldedScalar(this.scalarData.value);
				return;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0003EFFC File Offset: 0x0003D1FC
		private void WritePlainScalar(string value, bool allowBreaks)
		{
			if (!this.isWhitespace)
			{
				this.Write(' ');
			}
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				char c2;
				if (Emitter.IsSpace(c))
				{
					if (allowBreaks && !flag && this.column > this.bestWidth && i + 1 < value.Length && value[i + 1] != ' ')
					{
						this.WriteIndent();
					}
					else
					{
						this.Write(c);
					}
					flag = true;
				}
				else if (Emitter.IsBreak(c, out c2))
				{
					if (!flag2 && c == '\n')
					{
						this.WriteBreak('\n');
					}
					this.WriteBreak(c2);
					this.isIndentation = true;
					flag2 = true;
				}
				else
				{
					if (flag2)
					{
						this.WriteIndent();
					}
					this.Write(c);
					this.isIndentation = false;
					flag = false;
					flag2 = false;
				}
			}
			this.isWhitespace = false;
			this.isIndentation = false;
			if (this.isRootContext)
			{
				this.isOpenEnded = true;
			}
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0003F0E8 File Offset: 0x0003D2E8
		private void WriteSingleQuotedScalar(string value, bool allowBreaks)
		{
			this.WriteIndicator("'", true, false, false);
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				char c2;
				if (c == ' ')
				{
					if (allowBreaks && !flag && this.column > this.bestWidth && i != 0 && i + 1 < value.Length && value[i + 1] != ' ')
					{
						this.WriteIndent();
					}
					else
					{
						this.Write(c);
					}
					flag = true;
				}
				else if (Emitter.IsBreak(c, out c2))
				{
					if (!flag2 && c == '\n')
					{
						this.WriteBreak('\n');
					}
					this.WriteBreak(c2);
					this.isIndentation = true;
					flag2 = true;
				}
				else
				{
					if (flag2)
					{
						this.WriteIndent();
					}
					if (c == '\'')
					{
						this.Write(c);
					}
					this.Write(c);
					this.isIndentation = false;
					flag = false;
					flag2 = false;
				}
			}
			this.WriteIndicator("'", false, false, false);
			this.isWhitespace = false;
			this.isIndentation = false;
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0003F1E0 File Offset: 0x0003D3E0
		private void WriteDoubleQuotedScalar(string value, bool allowBreaks)
		{
			this.WriteIndicator("\"", true, false, false);
			bool flag = false;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				char c2;
				if (!Emitter.IsPrintable(c) || Emitter.IsBreak(c, out c2) || c == '"' || c == '\\')
				{
					this.Write('\\');
					if (c <= '\\')
					{
						if (c <= '\u001b')
						{
							switch (c)
							{
							case '\0':
								this.Write('0');
								break;
							case '\u0001':
							case '\u0002':
							case '\u0003':
							case '\u0004':
							case '\u0005':
							case '\u0006':
								goto IL_1B1;
							case '\a':
								this.Write('a');
								break;
							case '\b':
								this.Write('b');
								break;
							case '\t':
								this.Write('t');
								break;
							case '\n':
								this.Write('n');
								break;
							case '\v':
								this.Write('v');
								break;
							case '\f':
								this.Write('f');
								break;
							case '\r':
								this.Write('r');
								break;
							default:
								if (c != '\u001b')
								{
									goto IL_1B1;
								}
								this.Write('e');
								break;
							}
						}
						else if (c != '"')
						{
							if (c != '\\')
							{
								goto IL_1B1;
							}
							this.Write('\\');
						}
						else
						{
							this.Write('"');
						}
					}
					else if (c <= '\u00a0')
					{
						if (c != '\u0085')
						{
							if (c != '\u00a0')
							{
								goto IL_1B1;
							}
							this.Write('_');
						}
						else
						{
							this.Write('N');
						}
					}
					else if (c != '\u2028')
					{
						if (c != '\u2029')
						{
							goto IL_1B1;
						}
						this.Write('P');
					}
					else
					{
						this.Write('L');
					}
					IL_264:
					flag = false;
					goto IL_2C1;
					IL_1B1:
					ushort num = (ushort)c;
					if (num <= 255)
					{
						this.Write('x');
						this.Write(num.ToString("X02", CultureInfo.InvariantCulture));
						goto IL_264;
					}
					if (!Emitter.IsHighSurrogate(c))
					{
						this.Write('u');
						this.Write(num.ToString("X04", CultureInfo.InvariantCulture));
						goto IL_264;
					}
					if (i + 1 < value.Length && Emitter.IsLowSurrogate(value[i + 1]))
					{
						this.Write('U');
						this.Write(char.ConvertToUtf32(c, value[i + 1]).ToString("X08", CultureInfo.InvariantCulture));
						i++;
						goto IL_264;
					}
					throw new SyntaxErrorException("While writing a quoted scalar, found an orphaned high surrogate.");
				}
				else if (c == ' ')
				{
					if (allowBreaks && !flag && this.column > this.bestWidth && i > 0 && i + 1 < value.Length)
					{
						this.WriteIndent();
						if (value[i + 1] == ' ')
						{
							this.Write('\\');
						}
					}
					else
					{
						this.Write(c);
					}
					flag = true;
				}
				else
				{
					this.Write(c);
					flag = false;
				}
				IL_2C1:;
			}
			this.WriteIndicator("\"", false, false, false);
			this.isWhitespace = false;
			this.isIndentation = false;
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0003F4DC File Offset: 0x0003D6DC
		private void WriteLiteralScalar(string value)
		{
			bool flag = true;
			this.WriteIndicator("|", true, false, false);
			this.WriteBlockScalarHints(value);
			this.WriteBreak('\n');
			this.isIndentation = true;
			this.isWhitespace = true;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				if (c != '\r' || i + 1 >= value.Length || value[i + 1] != '\n')
				{
					char c2;
					if (Emitter.IsBreak(c, out c2))
					{
						this.WriteBreak(c2);
						this.isIndentation = true;
						flag = true;
					}
					else
					{
						if (flag)
						{
							this.WriteIndent();
						}
						this.Write(c);
						this.isIndentation = false;
						flag = false;
					}
				}
			}
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0003F584 File Offset: 0x0003D784
		private void WriteFoldedScalar(string value)
		{
			bool flag = true;
			bool flag2 = true;
			this.WriteIndicator(">", true, false, false);
			this.WriteBlockScalarHints(value);
			this.WriteBreak('\n');
			this.isIndentation = true;
			this.isWhitespace = true;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value[i];
				char c2;
				if (Emitter.IsBreak(c, out c2))
				{
					if (!flag && !flag2 && c == '\n')
					{
						int num = 0;
						char c3;
						while (i + num < value.Length && Emitter.IsBreak(value[i + num], out c3))
						{
							num++;
						}
						if (i + num < value.Length && !Emitter.IsBlank(value[i + num]) && !Emitter.IsBreak(value[i + num], out c3))
						{
							this.WriteBreak('\n');
						}
					}
					this.WriteBreak(c2);
					this.isIndentation = true;
					flag = true;
				}
				else
				{
					if (flag)
					{
						this.WriteIndent();
						flag2 = Emitter.IsBlank(c);
					}
					if (!flag && c == ' ' && i + 1 < value.Length && value[i + 1] != ' ' && this.column > this.bestWidth)
					{
						this.WriteIndent();
					}
					else
					{
						this.Write(c);
					}
					this.isIndentation = false;
					flag = false;
				}
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0003F6C2 File Offset: 0x0003D8C2
		private static bool IsSpace(char character)
		{
			return character == ' ';
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0003F6C9 File Offset: 0x0003D8C9
		private static bool IsBreak(char character, out char breakChar)
		{
			if (character <= '\r')
			{
				if (character != '\n' && character != '\r')
				{
					goto IL_36;
				}
			}
			else if (character != '\u0085')
			{
				if (character != '\u2028' && character != '\u2029')
				{
					goto IL_36;
				}
				breakChar = character;
				return true;
			}
			breakChar = '\n';
			return true;
			IL_36:
			breakChar = '\0';
			return false;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0003F705 File Offset: 0x0003D905
		private static bool IsBlank(char character)
		{
			return character == ' ' || character == '\t';
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0003F714 File Offset: 0x0003D914
		private static bool IsPrintable(char character)
		{
			return character == '\t' || character == '\n' || character == '\r' || (character >= ' ' && character <= '~') || character == '\u0085' || (character >= '\u00a0' && character <= '\ud7ff') || (character >= '\ue000' && character <= '\ufffd');
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0003F769 File Offset: 0x0003D969
		private static bool IsHighSurrogate(char c)
		{
			return '\ud800' <= c && c <= '\udbff';
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0003F780 File Offset: 0x0003D980
		private static bool IsLowSurrogate(char c)
		{
			return '\udc00' <= c && c <= '\udfff';
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003F798 File Offset: 0x0003D998
		private void EmitSequenceStart(ParsingEvent evt)
		{
			this.ProcessAnchor();
			this.ProcessTag();
			SequenceStart sequenceStart = (SequenceStart)evt;
			if (this.flowLevel != 0 || this.isCanonical || sequenceStart.Style == SequenceStyle.Flow || this.CheckEmptySequence())
			{
				this.state = EmitterState.FlowSequenceFirstItem;
				return;
			}
			this.state = EmitterState.BlockSequenceFirstItem;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003F7EC File Offset: 0x0003D9EC
		private void EmitMappingStart(ParsingEvent evt)
		{
			this.ProcessAnchor();
			this.ProcessTag();
			MappingStart mappingStart = (MappingStart)evt;
			if (this.flowLevel != 0 || this.isCanonical || mappingStart.Style == MappingStyle.Flow || this.CheckEmptyMapping())
			{
				this.state = EmitterState.FlowMappingFirstKey;
				return;
			}
			this.state = EmitterState.BlockMappingFirstKey;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0003F840 File Offset: 0x0003DA40
		private void ProcessAnchor()
		{
			if (this.anchorData.anchor != null)
			{
				this.WriteIndicator(this.anchorData.isAlias ? "*" : "&", true, false, false);
				this.WriteAnchor(this.anchorData.anchor);
			}
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0003F890 File Offset: 0x0003DA90
		private void ProcessTag()
		{
			if (this.tagData.handle == null && this.tagData.suffix == null)
			{
				return;
			}
			if (this.tagData.handle != null)
			{
				this.WriteTagHandle(this.tagData.handle);
				if (this.tagData.suffix != null)
				{
					this.WriteTagContent(this.tagData.suffix, false);
					return;
				}
			}
			else
			{
				this.WriteIndicator("!<", true, false, false);
				this.WriteTagContent(this.tagData.suffix, false);
				this.WriteIndicator(">", false, false, false);
			}
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0003F924 File Offset: 0x0003DB24
		private void EmitDocumentEnd(ParsingEvent evt)
		{
			YamlDotNet.Core.Events.DocumentEnd documentEnd = evt as YamlDotNet.Core.Events.DocumentEnd;
			if (documentEnd != null)
			{
				this.WriteIndent();
				if (!documentEnd.IsImplicit)
				{
					this.WriteIndicator("...", true, false, false);
					this.WriteIndent();
					this.isDocumentEndWritten = true;
				}
				this.state = EmitterState.DocumentStart;
				this.tagDirectives.Clear();
				return;
			}
			throw new YamlException("Expected DOCUMENT-END.");
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0003F984 File Offset: 0x0003DB84
		private void EmitFlowSequenceItem(ParsingEvent evt, bool isFirst)
		{
			if (isFirst)
			{
				this.WriteIndicator("[", true, true, false);
				this.IncreaseIndent(true, false);
				this.flowLevel++;
			}
			if (evt is SequenceEnd)
			{
				this.flowLevel--;
				this.indent = this.indents.Pop();
				if (this.isCanonical && !isFirst)
				{
					this.WriteIndicator(",", false, false, false);
					this.WriteIndent();
				}
				this.WriteIndicator("]", false, false, false);
				this.state = this.states.Pop();
				return;
			}
			if (!isFirst)
			{
				this.WriteIndicator(",", false, false, false);
			}
			if (this.isCanonical || this.column > this.bestWidth)
			{
				this.WriteIndent();
			}
			this.states.Push(EmitterState.FlowSequenceItem);
			this.EmitNode(evt, false, false, false);
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0003FA64 File Offset: 0x0003DC64
		private void EmitFlowMappingKey(ParsingEvent evt, bool isFirst)
		{
			if (isFirst)
			{
				this.WriteIndicator("{", true, true, false);
				this.IncreaseIndent(true, false);
				this.flowLevel++;
			}
			if (evt is MappingEnd)
			{
				this.flowLevel--;
				this.indent = this.indents.Pop();
				if (this.isCanonical && !isFirst)
				{
					this.WriteIndicator(",", false, false, false);
					this.WriteIndent();
				}
				this.WriteIndicator("}", false, false, false);
				this.state = this.states.Pop();
				return;
			}
			if (!isFirst)
			{
				this.WriteIndicator(",", false, false, false);
			}
			if (this.isCanonical || this.column > this.bestWidth)
			{
				this.WriteIndent();
			}
			if (!this.isCanonical && this.CheckSimpleKey())
			{
				this.states.Push(EmitterState.FlowMappingSimpleValue);
				this.EmitNode(evt, false, true, true);
				return;
			}
			this.WriteIndicator("?", true, false, false);
			this.states.Push(EmitterState.FlowMappingValue);
			this.EmitNode(evt, false, true, false);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0003FB78 File Offset: 0x0003DD78
		private void EmitFlowMappingValue(ParsingEvent evt, bool isSimple)
		{
			if (isSimple)
			{
				this.WriteIndicator(":", false, false, false);
			}
			else
			{
				if (this.isCanonical || this.column > this.bestWidth)
				{
					this.WriteIndent();
				}
				this.WriteIndicator(":", true, false, false);
			}
			this.states.Push(EmitterState.FlowMappingKey);
			this.EmitNode(evt, false, true, false);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0003FBDC File Offset: 0x0003DDDC
		private void EmitBlockSequenceItem(ParsingEvent evt, bool isFirst)
		{
			if (isFirst)
			{
				this.IncreaseIndent(false, this.isMappingContext && !this.isIndentation);
			}
			if (evt is SequenceEnd)
			{
				this.indent = this.indents.Pop();
				this.state = this.states.Pop();
				return;
			}
			this.WriteIndent();
			this.WriteIndicator("-", true, false, true);
			this.states.Push(EmitterState.BlockSequenceItem);
			this.EmitNode(evt, false, false, false);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0003FC60 File Offset: 0x0003DE60
		private void EmitBlockMappingKey(ParsingEvent evt, bool isFirst)
		{
			if (isFirst)
			{
				this.IncreaseIndent(false, false);
			}
			if (evt is MappingEnd)
			{
				this.indent = this.indents.Pop();
				this.state = this.states.Pop();
				return;
			}
			this.WriteIndent();
			if (this.CheckSimpleKey())
			{
				this.states.Push(EmitterState.BlockMappingSimpleValue);
				this.EmitNode(evt, false, true, true);
				return;
			}
			this.WriteIndicator("?", true, false, true);
			this.states.Push(EmitterState.BlockMappingValue);
			this.EmitNode(evt, false, true, false);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0003FCEE File Offset: 0x0003DEEE
		private void EmitBlockMappingValue(ParsingEvent evt, bool isSimple)
		{
			if (isSimple)
			{
				this.WriteIndicator(":", false, false, false);
			}
			else
			{
				this.WriteIndent();
				this.WriteIndicator(":", true, false, true);
			}
			this.states.Push(EmitterState.BlockMappingKey);
			this.EmitNode(evt, false, true, false);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0003FD30 File Offset: 0x0003DF30
		private void IncreaseIndent(bool isFlow, bool isIndentless)
		{
			this.indents.Push(this.indent);
			if (this.indent < 0)
			{
				this.indent = (isFlow ? this.bestIndent : 0);
				return;
			}
			if (!isIndentless)
			{
				this.indent += this.bestIndent;
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0003FD80 File Offset: 0x0003DF80
		private bool CheckEmptyDocument()
		{
			int num = 0;
			foreach (ParsingEvent parsingEvent in this.events)
			{
				num++;
				if (num == 2)
				{
					YamlDotNet.Core.Events.Scalar scalar = parsingEvent as YamlDotNet.Core.Events.Scalar;
					if (scalar != null)
					{
						return string.IsNullOrEmpty(scalar.Value);
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003FDF4 File Offset: 0x0003DFF4
		private bool CheckSimpleKey()
		{
			if (this.events.Count < 1)
			{
				return false;
			}
			int num;
			switch (this.events.Peek().Type)
			{
			case EventType.Alias:
				num = this.SafeStringLength(this.anchorData.anchor);
				goto IL_13B;
			case EventType.Scalar:
				if (this.scalarData.isMultiline)
				{
					return false;
				}
				num = this.SafeStringLength(this.anchorData.anchor) + this.SafeStringLength(this.tagData.handle) + this.SafeStringLength(this.tagData.suffix) + this.SafeStringLength(this.scalarData.value);
				goto IL_13B;
			case EventType.SequenceStart:
				if (!this.CheckEmptySequence())
				{
					return false;
				}
				num = this.SafeStringLength(this.anchorData.anchor) + this.SafeStringLength(this.tagData.handle) + this.SafeStringLength(this.tagData.suffix);
				goto IL_13B;
			case EventType.MappingStart:
				if (!this.CheckEmptySequence())
				{
					return false;
				}
				num = this.SafeStringLength(this.anchorData.anchor) + this.SafeStringLength(this.tagData.handle) + this.SafeStringLength(this.tagData.suffix);
				goto IL_13B;
			}
			return false;
			IL_13B:
			return num <= 128;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003FF47 File Offset: 0x0003E147
		private int SafeStringLength(string value)
		{
			if (value != null)
			{
				return value.Length;
			}
			return 0;
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003FF54 File Offset: 0x0003E154
		private bool CheckEmptySequence()
		{
			if (this.events.Count < 2)
			{
				return false;
			}
			FakeList<ParsingEvent> fakeList = new FakeList<ParsingEvent>(this.events);
			return fakeList[0] is SequenceStart && fakeList[1] is SequenceEnd;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0003FF9C File Offset: 0x0003E19C
		private bool CheckEmptyMapping()
		{
			if (this.events.Count < 2)
			{
				return false;
			}
			FakeList<ParsingEvent> fakeList = new FakeList<ParsingEvent>(this.events);
			return fakeList[0] is MappingStart && fakeList[1] is MappingEnd;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0003FFE4 File Offset: 0x0003E1E4
		private void WriteBlockScalarHints(string value)
		{
			CharacterAnalyzer<StringLookAheadBuffer> characterAnalyzer = new CharacterAnalyzer<StringLookAheadBuffer>(new StringLookAheadBuffer(value));
			if (characterAnalyzer.IsSpace(0) || characterAnalyzer.IsBreak(0))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "{0}", this.bestIndent);
				this.WriteIndicator(text, false, false, false);
			}
			this.isOpenEnded = false;
			string text2 = null;
			if (value.Length == 0 || !characterAnalyzer.IsBreak(value.Length - 1))
			{
				text2 = "-";
			}
			else if (value.Length >= 2 && characterAnalyzer.IsBreak(value.Length - 2))
			{
				text2 = "+";
				this.isOpenEnded = true;
			}
			if (text2 != null)
			{
				this.WriteIndicator(text2, false, false, false);
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00040090 File Offset: 0x0003E290
		private void WriteIndicator(string indicator, bool needWhitespace, bool whitespace, bool indentation)
		{
			if (needWhitespace && !this.isWhitespace)
			{
				this.Write(' ');
			}
			this.Write(indicator);
			this.isWhitespace = whitespace;
			this.isIndentation = this.isIndentation && indentation;
			this.isOpenEnded = false;
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000400CC File Offset: 0x0003E2CC
		private void WriteIndent()
		{
			int num = Math.Max(this.indent, 0);
			if (!this.isIndentation || this.column > num || (this.column == num && !this.isWhitespace))
			{
				this.WriteBreak('\n');
			}
			while (this.column < num)
			{
				this.Write(' ');
			}
			this.isWhitespace = true;
			this.isIndentation = true;
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00040131 File Offset: 0x0003E331
		private void WriteAnchor(string value)
		{
			this.Write(value);
			this.isWhitespace = false;
			this.isIndentation = false;
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00040148 File Offset: 0x0003E348
		private void WriteTagHandle(string value)
		{
			if (!this.isWhitespace)
			{
				this.Write(' ');
			}
			this.Write(value);
			this.isWhitespace = false;
			this.isIndentation = false;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0004016F File Offset: 0x0003E36F
		private void WriteTagContent(string value, bool needsWhitespace)
		{
			if (needsWhitespace && !this.isWhitespace)
			{
				this.Write(' ');
			}
			this.Write(this.UrlEncode(value));
			this.isWhitespace = false;
			this.isIndentation = false;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0004019F File Offset: 0x0003E39F
		private string UrlEncode(string text)
		{
			return Emitter.uriReplacer.Replace(text, delegate(Match match)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (byte b in Encoding.UTF8.GetBytes(match.Value))
				{
					stringBuilder.AppendFormat("%{0:X02}", b);
				}
				return stringBuilder.ToString();
			});
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000401CB File Offset: 0x0003E3CB
		private void Write(char value)
		{
			this.output.Write(value);
			this.column++;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000401E7 File Offset: 0x0003E3E7
		private void Write(string value)
		{
			this.output.Write(value);
			this.column += value.Length;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00040208 File Offset: 0x0003E408
		private void WriteBreak(char breakCharacter = '\n')
		{
			if (breakCharacter == '\n')
			{
				this.output.WriteLine();
			}
			else
			{
				this.output.Write(breakCharacter);
			}
			this.column = 0;
		}

		// Token: 0x04000889 RID: 2185
		private const int MinBestIndent = 2;

		// Token: 0x0400088A RID: 2186
		private const int MaxBestIndent = 9;

		// Token: 0x0400088B RID: 2187
		private const int MaxAliasLength = 128;

		// Token: 0x0400088C RID: 2188
		private static readonly Regex uriReplacer = new Regex("[^0-9A-Za-z_\\-;?@=$~\\\\\\)\\]/:&+,\\.\\*\\(\\[!]", RegexOptions.Singleline);

		// Token: 0x0400088D RID: 2189
		private readonly TextWriter output;

		// Token: 0x0400088E RID: 2190
		private readonly bool outputUsesUnicodeEncoding;

		// Token: 0x0400088F RID: 2191
		private readonly bool isCanonical;

		// Token: 0x04000890 RID: 2192
		private readonly int bestIndent;

		// Token: 0x04000891 RID: 2193
		private readonly int bestWidth;

		// Token: 0x04000892 RID: 2194
		private EmitterState state;

		// Token: 0x04000893 RID: 2195
		private readonly Stack<EmitterState> states = new Stack<EmitterState>();

		// Token: 0x04000894 RID: 2196
		private readonly Queue<ParsingEvent> events = new Queue<ParsingEvent>();

		// Token: 0x04000895 RID: 2197
		private readonly Stack<int> indents = new Stack<int>();

		// Token: 0x04000896 RID: 2198
		private readonly TagDirectiveCollection tagDirectives = new TagDirectiveCollection();

		// Token: 0x04000897 RID: 2199
		private int indent;

		// Token: 0x04000898 RID: 2200
		private int flowLevel;

		// Token: 0x04000899 RID: 2201
		private bool isMappingContext;

		// Token: 0x0400089A RID: 2202
		private bool isSimpleKeyContext;

		// Token: 0x0400089B RID: 2203
		private bool isRootContext;

		// Token: 0x0400089C RID: 2204
		private int column;

		// Token: 0x0400089D RID: 2205
		private bool isWhitespace;

		// Token: 0x0400089E RID: 2206
		private bool isIndentation;

		// Token: 0x0400089F RID: 2207
		private bool isOpenEnded;

		// Token: 0x040008A0 RID: 2208
		private bool isDocumentEndWritten;

		// Token: 0x040008A1 RID: 2209
		private readonly Emitter.AnchorData anchorData = new Emitter.AnchorData();

		// Token: 0x040008A2 RID: 2210
		private readonly Emitter.TagData tagData = new Emitter.TagData();

		// Token: 0x040008A3 RID: 2211
		private readonly Emitter.ScalarData scalarData = new Emitter.ScalarData();

		// Token: 0x02000A64 RID: 2660
		private class AnchorData
		{
			// Token: 0x04002366 RID: 9062
			public string anchor;

			// Token: 0x04002367 RID: 9063
			public bool isAlias;
		}

		// Token: 0x02000A65 RID: 2661
		private class TagData
		{
			// Token: 0x04002368 RID: 9064
			public string handle;

			// Token: 0x04002369 RID: 9065
			public string suffix;
		}

		// Token: 0x02000A66 RID: 2662
		private class ScalarData
		{
			// Token: 0x0400236A RID: 9066
			public string value;

			// Token: 0x0400236B RID: 9067
			public bool isMultiline;

			// Token: 0x0400236C RID: 9068
			public bool isFlowPlainAllowed;

			// Token: 0x0400236D RID: 9069
			public bool isBlockPlainAllowed;

			// Token: 0x0400236E RID: 9070
			public bool isSingleQuotedAllowed;

			// Token: 0x0400236F RID: 9071
			public bool isBlockAllowed;

			// Token: 0x04002370 RID: 9072
			public bool hasSingleQuotes;

			// Token: 0x04002371 RID: 9073
			public ScalarStyle style;
		}
	}
}
