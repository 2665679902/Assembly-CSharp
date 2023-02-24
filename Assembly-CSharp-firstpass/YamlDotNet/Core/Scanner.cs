using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace YamlDotNet.Core
{
	// Token: 0x02000212 RID: 530
	[Serializable]
	public class Scanner : IScanner
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00041A60 File Offset: 0x0003FC60
		// (set) Token: 0x0600103A RID: 4154 RVA: 0x00041A68 File Offset: 0x0003FC68
		public bool SkipComments { get; private set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00041A71 File Offset: 0x0003FC71
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x00041A79 File Offset: 0x0003FC79
		public Token Current { get; private set; }

		// Token: 0x0600103D RID: 4157 RVA: 0x00041A84 File Offset: 0x0003FC84
		public Scanner(TextReader input, bool skipComments = true)
		{
			this.analyzer = new CharacterAnalyzer<LookAheadBuffer>(new LookAheadBuffer(input, 8));
			this.cursor = new Cursor();
			this.SkipComments = skipComments;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x00041AE3 File Offset: 0x0003FCE3
		public Mark CurrentPosition
		{
			get
			{
				return this.cursor.Mark();
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00041AF0 File Offset: 0x0003FCF0
		public bool MoveNext()
		{
			if (this.Current != null)
			{
				this.ConsumeCurrent();
			}
			return this.MoveNextWithoutConsuming();
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00041B08 File Offset: 0x0003FD08
		public bool MoveNextWithoutConsuming()
		{
			if (!this.tokenAvailable && !this.streamEndProduced)
			{
				this.FetchMoreTokens();
			}
			if (this.tokens.Count > 0)
			{
				this.Current = this.tokens.Dequeue();
				this.tokenAvailable = false;
				return true;
			}
			this.Current = null;
			return false;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00041B5B File Offset: 0x0003FD5B
		public void ConsumeCurrent()
		{
			this.tokensParsed++;
			this.tokenAvailable = false;
			this.previous = this.Current;
			this.Current = null;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x00041B85 File Offset: 0x0003FD85
		private char ReadCurrentCharacter()
		{
			char c = this.analyzer.Peek(0);
			this.Skip();
			return c;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00041B99 File Offset: 0x0003FD99
		private char ReadLine()
		{
			if (this.analyzer.Check("\r\n\u0085", 0))
			{
				this.SkipLine();
				return '\n';
			}
			char c = this.analyzer.Peek(0);
			this.SkipLine();
			return c;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00041BCC File Offset: 0x0003FDCC
		private void FetchMoreTokens()
		{
			for (;;)
			{
				bool flag = false;
				if (this.tokens.Count == 0)
				{
					flag = true;
				}
				else
				{
					this.StaleSimpleKeys();
					foreach (SimpleKey simpleKey in this.simpleKeys)
					{
						if (simpleKey.IsPossible && simpleKey.TokenNumber == this.tokensParsed)
						{
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					break;
				}
				this.FetchNextToken();
			}
			this.tokenAvailable = true;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x00041C60 File Offset: 0x0003FE60
		private static bool StartsWith(StringBuilder what, char start)
		{
			return what.Length > 0 && what[0] == start;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x00041C78 File Offset: 0x0003FE78
		private void StaleSimpleKeys()
		{
			foreach (SimpleKey simpleKey in this.simpleKeys)
			{
				if (simpleKey.IsPossible && (simpleKey.Line < this.cursor.Line || simpleKey.Index + 1024 < this.cursor.Index))
				{
					if (simpleKey.IsRequired)
					{
						Mark mark = this.cursor.Mark();
						throw new SyntaxErrorException(mark, mark, "While scanning a simple key, could not find expected ':'.");
					}
					simpleKey.IsPossible = false;
				}
			}
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x00041D20 File Offset: 0x0003FF20
		private void FetchNextToken()
		{
			if (!this.streamStartProduced)
			{
				this.FetchStreamStart();
				return;
			}
			this.ScanToNextToken();
			this.StaleSimpleKeys();
			this.UnrollIndent(this.cursor.LineOffset);
			this.analyzer.Buffer.Cache(4);
			if (this.analyzer.Buffer.EndOfInput)
			{
				this.FetchStreamEnd();
				return;
			}
			if (this.cursor.LineOffset == 0 && this.analyzer.Check('%', 0))
			{
				this.FetchDirective();
				return;
			}
			if (this.cursor.LineOffset == 0 && this.analyzer.Check('-', 0) && this.analyzer.Check('-', 1) && this.analyzer.Check('-', 2) && this.analyzer.IsWhiteBreakOrZero(3))
			{
				this.FetchDocumentIndicator(true);
				return;
			}
			if (this.cursor.LineOffset == 0 && this.analyzer.Check('.', 0) && this.analyzer.Check('.', 1) && this.analyzer.Check('.', 2) && this.analyzer.IsWhiteBreakOrZero(3))
			{
				this.FetchDocumentIndicator(false);
				return;
			}
			if (this.analyzer.Check('[', 0))
			{
				this.FetchFlowCollectionStart(true);
				return;
			}
			if (this.analyzer.Check('{', 0))
			{
				this.FetchFlowCollectionStart(false);
				return;
			}
			if (this.analyzer.Check(']', 0))
			{
				this.FetchFlowCollectionEnd(true);
				return;
			}
			if (this.analyzer.Check('}', 0))
			{
				this.FetchFlowCollectionEnd(false);
				return;
			}
			if (this.analyzer.Check(',', 0))
			{
				this.FetchFlowEntry();
				return;
			}
			if (this.analyzer.Check('-', 0) && this.analyzer.IsWhiteBreakOrZero(1))
			{
				this.FetchBlockEntry();
				return;
			}
			if (this.analyzer.Check('?', 0) && (this.flowLevel > 0 || this.analyzer.IsWhiteBreakOrZero(1)))
			{
				this.FetchKey();
				return;
			}
			if (this.analyzer.Check(':', 0) && (this.flowLevel > 0 || this.analyzer.IsWhiteBreakOrZero(1)))
			{
				this.FetchValue();
				return;
			}
			if (this.analyzer.Check('*', 0))
			{
				this.FetchAnchor(true);
				return;
			}
			if (this.analyzer.Check('&', 0))
			{
				this.FetchAnchor(false);
				return;
			}
			if (this.analyzer.Check('!', 0))
			{
				this.FetchTag();
				return;
			}
			if (this.analyzer.Check('|', 0) && this.flowLevel == 0)
			{
				this.FetchBlockScalar(true);
				return;
			}
			if (this.analyzer.Check('>', 0) && this.flowLevel == 0)
			{
				this.FetchBlockScalar(false);
				return;
			}
			if (this.analyzer.Check('\'', 0))
			{
				this.FetchFlowScalar(true);
				return;
			}
			if (this.analyzer.Check('"', 0))
			{
				this.FetchFlowScalar(false);
				return;
			}
			if ((!this.analyzer.IsWhiteBreakOrZero(0) && !this.analyzer.Check("-?:,[]{}#&*!|>'\"%@`", 0)) || (this.analyzer.Check('-', 0) && !this.analyzer.IsWhite(1)) || (this.flowLevel == 0 && this.analyzer.Check("?:", 0) && !this.analyzer.IsWhiteBreakOrZero(1)))
			{
				this.FetchPlainScalar();
				return;
			}
			Mark mark = this.cursor.Mark();
			this.Skip();
			Mark mark2 = this.cursor.Mark();
			throw new SyntaxErrorException(mark, mark2, "While scanning for the next token, find character that cannot start any token.");
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x000420A5 File Offset: 0x000402A5
		private bool CheckWhiteSpace()
		{
			return this.analyzer.Check(' ', 0) || ((this.flowLevel > 0 || !this.simpleKeyAllowed) && this.analyzer.Check('\t', 0));
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x000420DC File Offset: 0x000402DC
		private bool IsDocumentIndicator()
		{
			if (this.cursor.LineOffset == 0 && this.analyzer.IsWhiteBreakOrZero(3))
			{
				bool flag = this.analyzer.Check('-', 0) && this.analyzer.Check('-', 1) && this.analyzer.Check('-', 2);
				bool flag2 = this.analyzer.Check('.', 0) && this.analyzer.Check('.', 1) && this.analyzer.Check('.', 2);
				return flag || flag2;
			}
			return false;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0004216B File Offset: 0x0004036B
		private void Skip()
		{
			this.cursor.Skip();
			this.analyzer.Buffer.Skip(1);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0004218C File Offset: 0x0004038C
		private void SkipLine()
		{
			if (this.analyzer.IsCrLf(0))
			{
				this.cursor.SkipLineByOffset(2);
				this.analyzer.Buffer.Skip(2);
				return;
			}
			if (this.analyzer.IsBreak(0))
			{
				this.cursor.SkipLineByOffset(1);
				this.analyzer.Buffer.Skip(1);
				return;
			}
			if (!this.analyzer.IsZero(0))
			{
				throw new InvalidOperationException("Not at a break.");
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0004220A File Offset: 0x0004040A
		private void ScanToNextToken()
		{
			for (;;)
			{
				if (!this.CheckWhiteSpace())
				{
					this.ProcessComment();
					if (!this.analyzer.IsBreak(0))
					{
						break;
					}
					this.SkipLine();
					if (this.flowLevel == 0)
					{
						this.simpleKeyAllowed = true;
					}
				}
				else
				{
					this.Skip();
				}
			}
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00042248 File Offset: 0x00040448
		private void ProcessComment()
		{
			if (this.analyzer.Check('#', 0))
			{
				Mark mark = this.cursor.Mark();
				this.Skip();
				while (this.analyzer.IsSpace(0))
				{
					this.Skip();
				}
				StringBuilder stringBuilder = new StringBuilder();
				while (!this.analyzer.IsBreakOrZero(0))
				{
					stringBuilder.Append(this.ReadCurrentCharacter());
				}
				if (!this.SkipComments)
				{
					bool flag = this.previous != null && this.previous.End.Line == mark.Line && !(this.previous is StreamStart);
					this.tokens.Enqueue(new Comment(stringBuilder.ToString(), flag, mark, this.cursor.Mark()));
				}
			}
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00042314 File Offset: 0x00040514
		private void FetchStreamStart()
		{
			this.simpleKeys.Push(new SimpleKey());
			this.simpleKeyAllowed = true;
			this.streamStartProduced = true;
			Mark mark = this.cursor.Mark();
			this.tokens.Enqueue(new StreamStart(mark, mark));
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x00042360 File Offset: 0x00040560
		private void UnrollIndent(int column)
		{
			if (this.flowLevel != 0)
			{
				return;
			}
			while (this.indent > column)
			{
				Mark mark = this.cursor.Mark();
				this.tokens.Enqueue(new BlockEnd(mark, mark));
				this.indent = this.indents.Pop();
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x000423B0 File Offset: 0x000405B0
		private void FetchStreamEnd()
		{
			this.cursor.ForceSkipLineAfterNonBreak();
			this.UnrollIndent(-1);
			this.RemoveSimpleKey();
			this.simpleKeyAllowed = false;
			this.streamEndProduced = true;
			Mark mark = this.cursor.Mark();
			this.tokens.Enqueue(new StreamEnd(mark, mark));
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x00042404 File Offset: 0x00040604
		private void FetchDirective()
		{
			this.UnrollIndent(-1);
			this.RemoveSimpleKey();
			this.simpleKeyAllowed = false;
			Token token = this.ScanDirective();
			this.tokens.Enqueue(token);
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x00042438 File Offset: 0x00040638
		private Token ScanDirective()
		{
			Mark mark = this.cursor.Mark();
			this.Skip();
			string text = this.ScanDirectiveName(mark);
			if (text != null)
			{
				Token token;
				if (!(text == "YAML"))
				{
					if (!(text == "TAG"))
					{
						goto IL_4D;
					}
					token = this.ScanTagDirectiveValue(mark);
				}
				else
				{
					token = this.ScanVersionDirectiveValue(mark);
				}
				while (this.analyzer.IsWhite(0))
				{
					this.Skip();
				}
				this.ProcessComment();
				if (!this.analyzer.IsBreakOrZero(0))
				{
					throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a directive, did not find expected comment or line break.");
				}
				if (this.analyzer.IsBreak(0))
				{
					this.SkipLine();
				}
				return token;
			}
			IL_4D:
			throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a directive, find uknown directive name.");
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00042500 File Offset: 0x00040700
		private void FetchDocumentIndicator(bool isStartToken)
		{
			this.UnrollIndent(-1);
			this.RemoveSimpleKey();
			this.simpleKeyAllowed = false;
			Mark mark = this.cursor.Mark();
			this.Skip();
			this.Skip();
			this.Skip();
			Token token = (isStartToken ? new DocumentStart(mark, this.cursor.Mark()) : new DocumentEnd(mark, mark));
			this.tokens.Enqueue(token);
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0004256C File Offset: 0x0004076C
		private void FetchFlowCollectionStart(bool isSequenceToken)
		{
			this.SaveSimpleKey();
			this.IncreaseFlowLevel();
			this.simpleKeyAllowed = true;
			Mark mark = this.cursor.Mark();
			this.Skip();
			Token token;
			if (isSequenceToken)
			{
				token = new FlowSequenceStart(mark, mark);
			}
			else
			{
				token = new FlowMappingStart(mark, mark);
			}
			this.tokens.Enqueue(token);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000425BF File Offset: 0x000407BF
		private void IncreaseFlowLevel()
		{
			this.simpleKeys.Push(new SimpleKey());
			this.flowLevel++;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x000425E0 File Offset: 0x000407E0
		private void FetchFlowCollectionEnd(bool isSequenceToken)
		{
			this.RemoveSimpleKey();
			this.DecreaseFlowLevel();
			this.simpleKeyAllowed = false;
			Mark mark = this.cursor.Mark();
			this.Skip();
			Token token;
			if (isSequenceToken)
			{
				token = new FlowSequenceEnd(mark, mark);
			}
			else
			{
				token = new FlowMappingEnd(mark, mark);
			}
			this.tokens.Enqueue(token);
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00042633 File Offset: 0x00040833
		private void DecreaseFlowLevel()
		{
			Debug.Assert(this.flowLevel > 0, "Could flowLevel be zero when this method is called?");
			if (this.flowLevel > 0)
			{
				this.flowLevel--;
				this.simpleKeys.Pop();
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x0004266C File Offset: 0x0004086C
		private void FetchFlowEntry()
		{
			this.RemoveSimpleKey();
			this.simpleKeyAllowed = true;
			Mark mark = this.cursor.Mark();
			this.Skip();
			this.tokens.Enqueue(new FlowEntry(mark, this.cursor.Mark()));
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x000426B4 File Offset: 0x000408B4
		private void FetchBlockEntry()
		{
			if (this.flowLevel == 0)
			{
				if (!this.simpleKeyAllowed)
				{
					Mark mark = this.cursor.Mark();
					throw new SyntaxErrorException(mark, mark, "Block sequence entries are not allowed in this context.");
				}
				this.RollIndent(this.cursor.LineOffset, -1, true, this.cursor.Mark());
			}
			this.RemoveSimpleKey();
			this.simpleKeyAllowed = true;
			Mark mark2 = this.cursor.Mark();
			this.Skip();
			this.tokens.Enqueue(new BlockEntry(mark2, this.cursor.Mark()));
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00042744 File Offset: 0x00040944
		private void FetchKey()
		{
			if (this.flowLevel == 0)
			{
				if (!this.simpleKeyAllowed)
				{
					Mark mark = this.cursor.Mark();
					throw new SyntaxErrorException(mark, mark, "Mapping keys are not allowed in this context.");
				}
				this.RollIndent(this.cursor.LineOffset, -1, false, this.cursor.Mark());
			}
			this.RemoveSimpleKey();
			this.simpleKeyAllowed = this.flowLevel == 0;
			Mark mark2 = this.cursor.Mark();
			this.Skip();
			this.tokens.Enqueue(new Key(mark2, this.cursor.Mark()));
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x000427DC File Offset: 0x000409DC
		private void FetchValue()
		{
			SimpleKey simpleKey = this.simpleKeys.Peek();
			if (simpleKey.IsPossible)
			{
				this.tokens.Insert(simpleKey.TokenNumber - this.tokensParsed, new Key(simpleKey.Mark, simpleKey.Mark));
				this.RollIndent(simpleKey.LineOffset, simpleKey.TokenNumber, false, simpleKey.Mark);
				simpleKey.IsPossible = false;
				this.simpleKeyAllowed = false;
			}
			else
			{
				if (this.flowLevel == 0)
				{
					if (!this.simpleKeyAllowed)
					{
						Mark mark = this.cursor.Mark();
						throw new SyntaxErrorException(mark, mark, "Mapping values are not allowed in this context.");
					}
					this.RollIndent(this.cursor.LineOffset, -1, false, this.cursor.Mark());
				}
				this.simpleKeyAllowed = this.flowLevel == 0;
			}
			Mark mark2 = this.cursor.Mark();
			this.Skip();
			this.tokens.Enqueue(new Value(mark2, this.cursor.Mark()));
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000428D4 File Offset: 0x00040AD4
		private void RollIndent(int column, int number, bool isSequence, Mark position)
		{
			if (this.flowLevel > 0)
			{
				return;
			}
			if (this.indent < column)
			{
				this.indents.Push(this.indent);
				this.indent = column;
				Token token;
				if (isSequence)
				{
					token = new BlockSequenceStart(position, position);
				}
				else
				{
					token = new BlockMappingStart(position, position);
				}
				if (number == -1)
				{
					this.tokens.Enqueue(token);
					return;
				}
				this.tokens.Insert(number - this.tokensParsed, token);
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x0004294A File Offset: 0x00040B4A
		private void FetchAnchor(bool isAlias)
		{
			this.SaveSimpleKey();
			this.simpleKeyAllowed = false;
			this.tokens.Enqueue(this.ScanAnchor(isAlias));
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0004296C File Offset: 0x00040B6C
		private Token ScanAnchor(bool isAlias)
		{
			Mark mark = this.cursor.Mark();
			this.Skip();
			StringBuilder stringBuilder = new StringBuilder();
			while (this.analyzer.IsAlphaNumericDashOrUnderscore(0))
			{
				stringBuilder.Append(this.ReadCurrentCharacter());
			}
			if (stringBuilder.Length == 0 || (!this.analyzer.IsWhiteBreakOrZero(0) && !this.analyzer.Check("?:,]}%@`", 0)))
			{
				throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning an anchor or alias, did not find expected alphabetic or numeric character.");
			}
			if (isAlias)
			{
				return new AnchorAlias(stringBuilder.ToString(), mark, this.cursor.Mark());
			}
			return new Anchor(stringBuilder.ToString(), mark, this.cursor.Mark());
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00042A20 File Offset: 0x00040C20
		private void FetchTag()
		{
			this.SaveSimpleKey();
			this.simpleKeyAllowed = false;
			this.tokens.Enqueue(this.ScanTag());
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00042A40 File Offset: 0x00040C40
		private Token ScanTag()
		{
			Mark mark = this.cursor.Mark();
			string text;
			string text2;
			if (this.analyzer.Check('<', 1))
			{
				text = string.Empty;
				this.Skip();
				this.Skip();
				text2 = this.ScanTagUri(null, mark);
				if (!this.analyzer.Check('>', 0))
				{
					throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a tag, did not find the expected '>'.");
				}
				this.Skip();
			}
			else
			{
				string text3 = this.ScanTagHandle(false, mark);
				if (text3.Length > 1 && text3[0] == '!' && text3[text3.Length - 1] == '!')
				{
					text = text3;
					text2 = this.ScanTagUri(null, mark);
				}
				else
				{
					text2 = this.ScanTagUri(text3, mark);
					text = "!";
					if (text2.Length == 0)
					{
						text2 = text;
						text = string.Empty;
					}
				}
			}
			if (!this.analyzer.IsWhiteBreakOrZero(0))
			{
				throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a tag, did not find expected whitespace or line break.");
			}
			return new YamlDotNet.Core.Tokens.Tag(text, text2, mark, this.cursor.Mark());
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00042B46 File Offset: 0x00040D46
		private void FetchBlockScalar(bool isLiteral)
		{
			this.RemoveSimpleKey();
			this.simpleKeyAllowed = true;
			this.tokens.Enqueue(this.ScanBlockScalar(isLiteral));
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x00042B68 File Offset: 0x00040D68
		private Token ScanBlockScalar(bool isLiteral)
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			bool flag = false;
			Mark mark = this.cursor.Mark();
			this.Skip();
			if (this.analyzer.Check("+-", 0))
			{
				num = (this.analyzer.Check('+', 0) ? 1 : (-1));
				this.Skip();
				if (this.analyzer.IsDigit(0))
				{
					if (this.analyzer.Check('0', 0))
					{
						throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a block scalar, find an intendation indicator equal to 0.");
					}
					num2 = this.analyzer.AsDigit(0);
					this.Skip();
				}
			}
			else if (this.analyzer.IsDigit(0))
			{
				if (this.analyzer.Check('0', 0))
				{
					throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a block scalar, find an intendation indicator equal to 0.");
				}
				num2 = this.analyzer.AsDigit(0);
				this.Skip();
				if (this.analyzer.Check("+-", 0))
				{
					num = (this.analyzer.Check('+', 0) ? 1 : (-1));
					this.Skip();
				}
			}
			while (this.analyzer.IsWhite(0))
			{
				this.Skip();
			}
			this.ProcessComment();
			if (!this.analyzer.IsBreakOrZero(0))
			{
				throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a block scalar, did not find expected comment or line break.");
			}
			if (this.analyzer.IsBreak(0))
			{
				this.SkipLine();
			}
			Mark mark2 = this.cursor.Mark();
			if (num2 != 0)
			{
				num3 = ((this.indent >= 0) ? (this.indent + num2) : num2);
			}
			num3 = this.ScanBlockScalarBreaks(num3, stringBuilder3, mark, ref mark2);
			while (this.cursor.LineOffset == num3 && !this.analyzer.IsZero(0))
			{
				bool flag2 = this.analyzer.IsWhite(0);
				if (!isLiteral && Scanner.StartsWith(stringBuilder2, '\n') && !flag && !flag2)
				{
					if (stringBuilder3.Length == 0)
					{
						stringBuilder.Append(' ');
					}
					stringBuilder2.Length = 0;
				}
				else
				{
					stringBuilder.Append(stringBuilder2.ToString());
					stringBuilder2.Length = 0;
				}
				stringBuilder.Append(stringBuilder3.ToString());
				stringBuilder3.Length = 0;
				flag = this.analyzer.IsWhite(0);
				while (!this.analyzer.IsBreakOrZero(0))
				{
					stringBuilder.Append(this.ReadCurrentCharacter());
				}
				char c = this.ReadLine();
				if (c != '\0')
				{
					stringBuilder2.Append(c);
				}
				num3 = this.ScanBlockScalarBreaks(num3, stringBuilder3, mark, ref mark2);
			}
			if (num != -1)
			{
				stringBuilder.Append(stringBuilder2);
			}
			if (num == 1)
			{
				stringBuilder.Append(stringBuilder3);
			}
			ScalarStyle scalarStyle = (isLiteral ? ScalarStyle.Literal : ScalarStyle.Folded);
			return new Scalar(stringBuilder.ToString(), scalarStyle, mark, mark2);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00042E38 File Offset: 0x00041038
		private int ScanBlockScalarBreaks(int currentIndent, StringBuilder breaks, Mark start, ref Mark end)
		{
			int num = 0;
			end = this.cursor.Mark();
			for (;;)
			{
				if ((currentIndent != 0 && this.cursor.LineOffset >= currentIndent) || !this.analyzer.IsSpace(0))
				{
					if (this.cursor.LineOffset > num)
					{
						num = this.cursor.LineOffset;
					}
					if ((currentIndent == 0 || this.cursor.LineOffset < currentIndent) && this.analyzer.IsTab(0))
					{
						break;
					}
					if (!this.analyzer.IsBreak(0))
					{
						goto IL_B5;
					}
					breaks.Append(this.ReadLine());
					end = this.cursor.Mark();
				}
				else
				{
					this.Skip();
				}
			}
			throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a block scalar, find a tab character where an intendation space is expected.");
			IL_B5:
			if (currentIndent == 0)
			{
				currentIndent = Math.Max(num, Math.Max(this.indent + 1, 1));
			}
			return currentIndent;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x00042F14 File Offset: 0x00041114
		private void FetchFlowScalar(bool isSingleQuoted)
		{
			this.SaveSimpleKey();
			this.simpleKeyAllowed = false;
			this.tokens.Enqueue(this.ScanFlowScalar(isSingleQuoted));
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x00042F38 File Offset: 0x00041138
		private Token ScanFlowScalar(bool isSingleQuoted)
		{
			Mark mark = this.cursor.Mark();
			this.Skip();
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			StringBuilder stringBuilder4 = new StringBuilder();
			while (!this.IsDocumentIndicator())
			{
				if (this.analyzer.IsZero(0))
				{
					throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a quoted scalar, find unexpected end of stream.");
				}
				bool flag = false;
				while (!this.analyzer.IsWhiteBreakOrZero(0))
				{
					if (isSingleQuoted && this.analyzer.Check('\'', 0) && this.analyzer.Check('\'', 1))
					{
						stringBuilder.Append('\'');
						this.Skip();
						this.Skip();
					}
					else
					{
						if (this.analyzer.Check(isSingleQuoted ? '\'' : '"', 0))
						{
							break;
						}
						if (!isSingleQuoted && this.analyzer.Check('\\', 0) && this.analyzer.IsBreak(1))
						{
							this.Skip();
							this.SkipLine();
							flag = true;
							break;
						}
						if (!isSingleQuoted && this.analyzer.Check('\\', 0))
						{
							int num = 0;
							char c = this.analyzer.Peek(1);
							if (c != 'U')
							{
								if (c != 'u')
								{
									if (c == 'x')
									{
										num = 2;
									}
									else
									{
										char c2;
										if (!Scanner.simpleEscapeCodes.TryGetValue(c, out c2))
										{
											throw new SyntaxErrorException(mark, this.cursor.Mark(), "While parsing a quoted scalar, find unknown escape character.");
										}
										stringBuilder.Append(c2);
									}
								}
								else
								{
									num = 4;
								}
							}
							else
							{
								num = 8;
							}
							this.Skip();
							this.Skip();
							if (num > 0)
							{
								int num2 = 0;
								for (int i = 0; i < num; i++)
								{
									if (!this.analyzer.IsHex(i))
									{
										throw new SyntaxErrorException(mark, this.cursor.Mark(), "While parsing a quoted scalar, did not find expected hexdecimal number.");
									}
									num2 = (num2 << 4) + this.analyzer.AsHex(i);
								}
								if ((num2 >= 55296 && num2 <= 57343) || num2 > 1114111)
								{
									throw new SyntaxErrorException(mark, this.cursor.Mark(), "While parsing a quoted scalar, find invalid Unicode character escape code.");
								}
								stringBuilder.Append(char.ConvertFromUtf32(num2));
								for (int j = 0; j < num; j++)
								{
									this.Skip();
								}
							}
						}
						else
						{
							stringBuilder.Append(this.ReadCurrentCharacter());
						}
					}
				}
				if (this.analyzer.Check(isSingleQuoted ? '\'' : '"', 0))
				{
					this.Skip();
					return new Scalar(stringBuilder.ToString(), isSingleQuoted ? ScalarStyle.SingleQuoted : ScalarStyle.DoubleQuoted, mark, this.cursor.Mark());
				}
				while (this.analyzer.IsWhite(0) || this.analyzer.IsBreak(0))
				{
					if (this.analyzer.IsWhite(0))
					{
						if (!flag)
						{
							stringBuilder2.Append(this.ReadCurrentCharacter());
						}
						else
						{
							this.Skip();
						}
					}
					else if (!flag)
					{
						stringBuilder2.Length = 0;
						stringBuilder3.Append(this.ReadLine());
						flag = true;
					}
					else
					{
						stringBuilder4.Append(this.ReadLine());
					}
				}
				if (flag)
				{
					if (Scanner.StartsWith(stringBuilder3, '\n'))
					{
						if (stringBuilder4.Length == 0)
						{
							stringBuilder.Append(' ');
						}
						else
						{
							stringBuilder.Append(stringBuilder4.ToString());
						}
					}
					else
					{
						stringBuilder.Append(stringBuilder3.ToString());
						stringBuilder.Append(stringBuilder4.ToString());
					}
					stringBuilder3.Length = 0;
					stringBuilder4.Length = 0;
				}
				else
				{
					stringBuilder.Append(stringBuilder2.ToString());
					stringBuilder2.Length = 0;
				}
			}
			throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a quoted scalar, find unexpected document indicator.");
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x000432C8 File Offset: 0x000414C8
		private void FetchPlainScalar()
		{
			this.SaveSimpleKey();
			this.simpleKeyAllowed = false;
			this.tokens.Enqueue(this.ScanPlainScalar());
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x000432E8 File Offset: 0x000414E8
		private Token ScanPlainScalar()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			StringBuilder stringBuilder4 = new StringBuilder();
			bool flag = false;
			int num = this.indent + 1;
			Mark mark = this.cursor.Mark();
			Mark mark2 = mark;
			while (!this.IsDocumentIndicator())
			{
				if (this.analyzer.Check('#', 0))
				{
					break;
				}
				while (!this.analyzer.IsWhiteBreakOrZero(0))
				{
					if (this.flowLevel > 0 && this.analyzer.Check(':', 0) && !this.analyzer.IsWhiteBreakOrZero(1))
					{
						throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a plain scalar, find unexpected ':'.");
					}
					if ((this.analyzer.Check(':', 0) && this.analyzer.IsWhiteBreakOrZero(1)) || (this.flowLevel > 0 && this.analyzer.Check(",:?[]{}", 0)))
					{
						break;
					}
					if (flag || stringBuilder2.Length > 0)
					{
						if (flag)
						{
							if (Scanner.StartsWith(stringBuilder3, '\n'))
							{
								if (stringBuilder4.Length == 0)
								{
									stringBuilder.Append(' ');
								}
								else
								{
									stringBuilder.Append(stringBuilder4);
								}
							}
							else
							{
								stringBuilder.Append(stringBuilder3);
								stringBuilder.Append(stringBuilder4);
							}
							stringBuilder3.Length = 0;
							stringBuilder4.Length = 0;
							flag = false;
						}
						else
						{
							stringBuilder.Append(stringBuilder2);
							stringBuilder2.Length = 0;
						}
					}
					stringBuilder.Append(this.ReadCurrentCharacter());
					mark2 = this.cursor.Mark();
				}
				if (!this.analyzer.IsWhite(0) && !this.analyzer.IsBreak(0))
				{
					break;
				}
				while (this.analyzer.IsWhite(0) || this.analyzer.IsBreak(0))
				{
					if (this.analyzer.IsWhite(0))
					{
						if (flag && this.cursor.LineOffset < num && this.analyzer.IsTab(0))
						{
							throw new SyntaxErrorException(mark, this.cursor.Mark(), "While scanning a plain scalar, find a tab character that violate intendation.");
						}
						if (!flag)
						{
							stringBuilder2.Append(this.ReadCurrentCharacter());
						}
						else
						{
							this.Skip();
						}
					}
					else if (!flag)
					{
						stringBuilder2.Length = 0;
						stringBuilder3.Append(this.ReadLine());
						flag = true;
					}
					else
					{
						stringBuilder4.Append(this.ReadLine());
					}
				}
				if (this.flowLevel == 0 && this.cursor.LineOffset < num)
				{
					break;
				}
			}
			if (flag)
			{
				this.simpleKeyAllowed = true;
			}
			return new Scalar(stringBuilder.ToString(), ScalarStyle.Plain, mark, mark2);
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00043570 File Offset: 0x00041770
		private void RemoveSimpleKey()
		{
			SimpleKey simpleKey = this.simpleKeys.Peek();
			if (simpleKey.IsPossible && simpleKey.IsRequired)
			{
				throw new SyntaxErrorException(simpleKey.Mark, simpleKey.Mark, "While scanning a simple key, could not find expected ':'.");
			}
			simpleKey.IsPossible = false;
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x000435B8 File Offset: 0x000417B8
		private string ScanDirectiveName(Mark start)
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (this.analyzer.IsAlphaNumericDashOrUnderscore(0))
			{
				stringBuilder.Append(this.ReadCurrentCharacter());
			}
			if (stringBuilder.Length == 0)
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a directive, could not find expected directive name.");
			}
			if (!this.analyzer.IsWhiteBreakOrZero(0))
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a directive, find unexpected non-alphabetical character.");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x00043632 File Offset: 0x00041832
		private void SkipWhitespaces()
		{
			while (this.analyzer.IsWhite(0))
			{
				this.Skip();
			}
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0004364C File Offset: 0x0004184C
		private Token ScanVersionDirectiveValue(Mark start)
		{
			this.SkipWhitespaces();
			int num = this.ScanVersionDirectiveNumber(start);
			if (!this.analyzer.Check('.', 0))
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a %YAML directive, did not find expected digit or '.' character.");
			}
			this.Skip();
			int num2 = this.ScanVersionDirectiveNumber(start);
			return new VersionDirective(new Version(num, num2), start, start);
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x000436A8 File Offset: 0x000418A8
		private Token ScanTagDirectiveValue(Mark start)
		{
			this.SkipWhitespaces();
			string text = this.ScanTagHandle(true, start);
			if (!this.analyzer.IsWhite(0))
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a %TAG directive, did not find expected whitespace.");
			}
			this.SkipWhitespaces();
			string text2 = this.ScanTagUri(null, start);
			if (!this.analyzer.IsWhiteBreakOrZero(0))
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a %TAG directive, did not find expected whitespace or line break.");
			}
			return new TagDirective(text, text2, start, start);
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x00043724 File Offset: 0x00041924
		private string ScanTagUri(string head, Mark start)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (head != null && head.Length > 1)
			{
				stringBuilder.Append(head.Substring(1));
			}
			while (this.analyzer.IsAlphaNumericDashOrUnderscore(0) || this.analyzer.Check(";/?:@&=+$,.!~*'()[]%", 0))
			{
				if (this.analyzer.Check('%', 0))
				{
					stringBuilder.Append(this.ScanUriEscapes(start));
				}
				else if (this.analyzer.Check('+', 0))
				{
					stringBuilder.Append(' ');
					this.Skip();
				}
				else
				{
					stringBuilder.Append(this.ReadCurrentCharacter());
				}
			}
			if (stringBuilder.Length == 0)
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While parsing a tag, did not find expected tag URI.");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000437E8 File Offset: 0x000419E8
		private string ScanUriEscapes(Mark start)
		{
			byte[] array = null;
			int num = 0;
			int num2 = 0;
			while (this.analyzer.Check('%', 0) && this.analyzer.IsHex(1) && this.analyzer.IsHex(2))
			{
				int num3 = (this.analyzer.AsHex(1) << 4) + this.analyzer.AsHex(2);
				if (num2 == 0)
				{
					num2 = (((num3 & 128) == 0) ? 1 : (((num3 & 224) == 192) ? 2 : (((num3 & 240) == 224) ? 3 : (((num3 & 248) == 240) ? 4 : 0))));
					if (num2 == 0)
					{
						throw new SyntaxErrorException(start, this.cursor.Mark(), "While parsing a tag, find an incorrect leading UTF-8 octet.");
					}
					array = new byte[num2];
				}
				else if ((num3 & 192) != 128)
				{
					throw new SyntaxErrorException(start, this.cursor.Mark(), "While parsing a tag, find an incorrect trailing UTF-8 octet.");
				}
				array[num++] = (byte)num3;
				this.Skip();
				this.Skip();
				this.Skip();
				if (--num2 <= 0)
				{
					string @string = Encoding.UTF8.GetString(array, 0, num);
					if (@string.Length == 0 || @string.Length > 2)
					{
						throw new SyntaxErrorException(start, this.cursor.Mark(), "While parsing a tag, find an incorrect UTF-8 sequence.");
					}
					return @string;
				}
			}
			throw new SyntaxErrorException(start, this.cursor.Mark(), "While parsing a tag, did not find URI escaped octet.");
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0004394C File Offset: 0x00041B4C
		private string ScanTagHandle(bool isDirective, Mark start)
		{
			if (!this.analyzer.Check('!', 0))
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a tag, did not find expected '!'.");
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ReadCurrentCharacter());
			while (this.analyzer.IsAlphaNumericDashOrUnderscore(0))
			{
				stringBuilder.Append(this.ReadCurrentCharacter());
			}
			if (this.analyzer.Check('!', 0))
			{
				stringBuilder.Append(this.ReadCurrentCharacter());
			}
			else if (isDirective && (stringBuilder.Length != 1 || stringBuilder[0] != '!'))
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While parsing a tag directive, did not find expected '!'.");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x00043A04 File Offset: 0x00041C04
		private int ScanVersionDirectiveNumber(Mark start)
		{
			int num = 0;
			int num2 = 0;
			while (this.analyzer.IsDigit(0))
			{
				if (++num2 > 9)
				{
					throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a %YAML directive, find extremely long version number.");
				}
				num = num * 10 + this.analyzer.AsDigit(0);
				this.Skip();
			}
			if (num2 == 0)
			{
				throw new SyntaxErrorException(start, this.cursor.Mark(), "While scanning a %YAML directive, did not find expected version number.");
			}
			return num;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x00043A78 File Offset: 0x00041C78
		private void SaveSimpleKey()
		{
			bool flag = this.flowLevel == 0 && this.indent == this.cursor.LineOffset;
			Debug.Assert(this.simpleKeyAllowed || !flag, "Can't require a simple key and disallow it at the same time.");
			if (this.simpleKeyAllowed)
			{
				SimpleKey simpleKey = new SimpleKey(true, flag, this.tokensParsed + this.tokens.Count, this.cursor);
				this.RemoveSimpleKey();
				this.simpleKeys.Pop();
				this.simpleKeys.Push(simpleKey);
			}
		}

		// Token: 0x040008F0 RID: 2288
		private const int MaxVersionNumberLength = 9;

		// Token: 0x040008F1 RID: 2289
		private const int MaxBufferLength = 8;

		// Token: 0x040008F2 RID: 2290
		private static readonly IDictionary<char, char> simpleEscapeCodes = new SortedDictionary<char, char>
		{
			{ '0', '\0' },
			{ 'a', '\a' },
			{ 'b', '\b' },
			{ 't', '\t' },
			{ '\t', '\t' },
			{ 'n', '\n' },
			{ 'v', '\v' },
			{ 'f', '\f' },
			{ 'r', '\r' },
			{ 'e', '\u001b' },
			{ ' ', ' ' },
			{ '"', '"' },
			{ '\'', '\'' },
			{ '\\', '\\' },
			{ 'N', '\u0085' },
			{ '_', '\u00a0' },
			{ 'L', '\u2028' },
			{ 'P', '\u2029' }
		};

		// Token: 0x040008F3 RID: 2291
		private readonly Stack<int> indents = new Stack<int>();

		// Token: 0x040008F4 RID: 2292
		private readonly InsertionQueue<Token> tokens = new InsertionQueue<Token>();

		// Token: 0x040008F5 RID: 2293
		private readonly Stack<SimpleKey> simpleKeys = new Stack<SimpleKey>();

		// Token: 0x040008F6 RID: 2294
		private readonly CharacterAnalyzer<LookAheadBuffer> analyzer;

		// Token: 0x040008F7 RID: 2295
		private readonly Cursor cursor;

		// Token: 0x040008F8 RID: 2296
		private bool streamStartProduced;

		// Token: 0x040008F9 RID: 2297
		private bool streamEndProduced;

		// Token: 0x040008FA RID: 2298
		private int indent = -1;

		// Token: 0x040008FB RID: 2299
		private bool simpleKeyAllowed;

		// Token: 0x040008FC RID: 2300
		private int flowLevel;

		// Token: 0x040008FD RID: 2301
		private int tokensParsed;

		// Token: 0x040008FE RID: 2302
		private bool tokenAvailable;

		// Token: 0x040008FF RID: 2303
		private Token previous;
	}
}
