using System;

namespace YamlDotNet.Core
{
	// Token: 0x02000215 RID: 533
	[Serializable]
	internal class StringLookAheadBuffer : ILookAheadBuffer
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00043CA2 File Offset: 0x00041EA2
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00043CAA File Offset: 0x00041EAA
		public int Position { get; private set; }

		// Token: 0x06001085 RID: 4229 RVA: 0x00043CB3 File Offset: 0x00041EB3
		public StringLookAheadBuffer(string value)
		{
			this.value = value;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00043CC2 File Offset: 0x00041EC2
		public int Length
		{
			get
			{
				return this.value.Length;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x00043CCF File Offset: 0x00041ECF
		public bool EndOfInput
		{
			get
			{
				return this.IsOutside(this.Position);
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00043CE0 File Offset: 0x00041EE0
		public char Peek(int offset)
		{
			int num = this.Position + offset;
			if (!this.IsOutside(num))
			{
				return this.value[num];
			}
			return '\0';
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00043D0D File Offset: 0x00041F0D
		private bool IsOutside(int index)
		{
			return index >= this.value.Length;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00043D20 File Offset: 0x00041F20
		public void Skip(int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", "The length must be positive.");
			}
			this.Position += length;
		}

		// Token: 0x04000906 RID: 2310
		private readonly string value;
	}
}
