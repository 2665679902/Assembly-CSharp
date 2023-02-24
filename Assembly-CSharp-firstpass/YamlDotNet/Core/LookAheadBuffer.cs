using System;
using System.IO;

namespace YamlDotNet.Core
{
	// Token: 0x02000209 RID: 521
	[Serializable]
	public class LookAheadBuffer : ILookAheadBuffer
	{
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00040365 File Offset: 0x0003E565
		public LookAheadBuffer(TextReader input, int capacity)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (capacity < 1)
			{
				throw new ArgumentOutOfRangeException("capacity", "The capacity must be positive.");
			}
			this.input = input;
			this.buffer = new char[capacity];
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x000403A2 File Offset: 0x0003E5A2
		public bool EndOfInput
		{
			get
			{
				return this.endOfInput && this.count == 0;
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000403B8 File Offset: 0x0003E5B8
		private int GetIndexForOffset(int offset)
		{
			int num = this.firstIndex + offset;
			if (num >= this.buffer.Length)
			{
				num -= this.buffer.Length;
			}
			return num;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000403E8 File Offset: 0x0003E5E8
		public char Peek(int offset)
		{
			if (offset < 0 || offset >= this.buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset", "The offset must be betwwen zero and the capacity of the buffer.");
			}
			this.Cache(offset);
			if (offset < this.count)
			{
				return this.buffer[this.GetIndexForOffset(offset)];
			}
			return '\0';
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x00040434 File Offset: 0x0003E634
		public void Cache(int length)
		{
			while (length >= this.count)
			{
				int num = this.input.Read();
				if (num < 0)
				{
					this.endOfInput = true;
					return;
				}
				int indexForOffset = this.GetIndexForOffset(this.count);
				this.buffer[indexForOffset] = (char)num;
				this.count++;
			}
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0004048B File Offset: 0x0003E68B
		public void Skip(int length)
		{
			if (length < 1 || length > this.count)
			{
				throw new ArgumentOutOfRangeException("length", "The length must be between 1 and the number of characters in the buffer. Use the Peek() and / or Cache() methods to fill the buffer.");
			}
			this.firstIndex = this.GetIndexForOffset(length);
			this.count -= length;
		}

		// Token: 0x040008BA RID: 2234
		private readonly TextReader input;

		// Token: 0x040008BB RID: 2235
		private readonly char[] buffer;

		// Token: 0x040008BC RID: 2236
		private int firstIndex;

		// Token: 0x040008BD RID: 2237
		private int count;

		// Token: 0x040008BE RID: 2238
		private bool endOfInput;
	}
}
