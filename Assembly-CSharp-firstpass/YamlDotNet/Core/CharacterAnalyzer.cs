using System;

namespace YamlDotNet.Core
{
	// Token: 0x020001FC RID: 508
	[Serializable]
	internal class CharacterAnalyzer<TBuffer> where TBuffer : ILookAheadBuffer
	{
		// Token: 0x06000F7C RID: 3964 RVA: 0x0003DC13 File Offset: 0x0003BE13
		public CharacterAnalyzer(TBuffer buffer)
		{
			this.buffer = buffer;
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0003DC22 File Offset: 0x0003BE22
		public TBuffer Buffer
		{
			get
			{
				return this.buffer;
			}
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		public bool EndOfInput
		{
			get
			{
				TBuffer tbuffer = this.buffer;
				return tbuffer.EndOfInput;
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0003DC50 File Offset: 0x0003BE50
		public char Peek(int offset)
		{
			TBuffer tbuffer = this.buffer;
			return tbuffer.Peek(offset);
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0003DC74 File Offset: 0x0003BE74
		public void Skip(int length)
		{
			TBuffer tbuffer = this.buffer;
			tbuffer.Skip(length);
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0003DC98 File Offset: 0x0003BE98
		public bool IsAlphaNumericDashOrUnderscore(int offset = 0)
		{
			TBuffer tbuffer = this.buffer;
			char c = tbuffer.Peek(offset);
			return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '_' || c == '-';
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003DCE8 File Offset: 0x0003BEE8
		public bool IsAscii(int offset = 0)
		{
			TBuffer tbuffer = this.buffer;
			return tbuffer.Peek(offset) <= '\u007f';
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003DD14 File Offset: 0x0003BF14
		public bool IsPrintable(int offset = 0)
		{
			TBuffer tbuffer = this.buffer;
			char c = tbuffer.Peek(offset);
			return c == '\t' || c == '\n' || c == '\r' || (c >= ' ' && c <= '~') || c == '\u0085' || (c >= '\u00a0' && c <= '\ud7ff') || (c >= '\ue000' && c <= '\ufffd');
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003DD80 File Offset: 0x0003BF80
		public bool IsDigit(int offset = 0)
		{
			TBuffer tbuffer = this.buffer;
			char c = tbuffer.Peek(offset);
			return c >= '0' && c <= '9';
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0003DDB4 File Offset: 0x0003BFB4
		public int AsDigit(int offset = 0)
		{
			TBuffer tbuffer = this.buffer;
			return (int)(tbuffer.Peek(offset) - '0');
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0003DDDC File Offset: 0x0003BFDC
		public bool IsHex(int offset)
		{
			TBuffer tbuffer = this.buffer;
			char c = tbuffer.Peek(offset);
			return (c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f');
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0003DE24 File Offset: 0x0003C024
		public int AsHex(int offset)
		{
			TBuffer tbuffer = this.buffer;
			char c = tbuffer.Peek(offset);
			if (c <= '9')
			{
				return (int)(c - '0');
			}
			if (c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			return (int)(c - 'a' + '\n');
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0003DE65 File Offset: 0x0003C065
		public bool IsSpace(int offset = 0)
		{
			return this.Check(' ', offset);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0003DE70 File Offset: 0x0003C070
		public bool IsZero(int offset = 0)
		{
			return this.Check('\0', offset);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0003DE7A File Offset: 0x0003C07A
		public bool IsTab(int offset = 0)
		{
			return this.Check('\t', offset);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0003DE85 File Offset: 0x0003C085
		public bool IsWhite(int offset = 0)
		{
			return this.IsSpace(offset) || this.IsTab(offset);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0003DE99 File Offset: 0x0003C099
		public bool IsBreak(int offset = 0)
		{
			return this.Check("\r\n\u0085\u2028\u2029", offset);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0003DEA7 File Offset: 0x0003C0A7
		public bool IsCrLf(int offset = 0)
		{
			return this.Check('\r', offset) && this.Check('\n', offset + 1);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0003DEC1 File Offset: 0x0003C0C1
		public bool IsBreakOrZero(int offset = 0)
		{
			return this.IsBreak(offset) || this.IsZero(offset);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0003DED5 File Offset: 0x0003C0D5
		public bool IsWhiteBreakOrZero(int offset = 0)
		{
			return this.IsWhite(offset) || this.IsBreakOrZero(offset);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		public bool Check(char expected, int offset = 0)
		{
			TBuffer tbuffer = this.buffer;
			return tbuffer.Peek(offset) == expected;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003DF14 File Offset: 0x0003C114
		public bool Check(string expectedCharacters, int offset = 0)
		{
			Debug.Assert(expectedCharacters.Length > 1, "Use Check(char, int) instead.");
			TBuffer tbuffer = this.buffer;
			char c = tbuffer.Peek(offset);
			return expectedCharacters.IndexOf(c) != -1;
		}

		// Token: 0x04000880 RID: 2176
		private readonly TBuffer buffer;
	}
}
