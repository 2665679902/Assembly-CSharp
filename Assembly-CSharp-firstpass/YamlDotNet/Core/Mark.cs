using System;

namespace YamlDotNet.Core
{
	// Token: 0x0200020A RID: 522
	[Serializable]
	public class Mark : IEquatable<Mark>, IComparable<Mark>, IComparable
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x000404C5 File Offset: 0x0003E6C5
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x000404CD File Offset: 0x0003E6CD
		public int Index { get; private set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x000404D6 File Offset: 0x0003E6D6
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x000404DE File Offset: 0x0003E6DE
		public int Line { get; private set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x000404E7 File Offset: 0x0003E6E7
		// (set) Token: 0x06001002 RID: 4098 RVA: 0x000404EF File Offset: 0x0003E6EF
		public int Column { get; private set; }

		// Token: 0x06001003 RID: 4099 RVA: 0x000404F8 File Offset: 0x0003E6F8
		public Mark()
		{
			this.Line = 1;
			this.Column = 1;
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x00040510 File Offset: 0x0003E710
		public Mark(int index, int line, int column)
		{
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be greater than or equal to zero.");
			}
			if (line < 1)
			{
				throw new ArgumentOutOfRangeException("line", "Line must be greater than or equal to 1.");
			}
			if (column < 1)
			{
				throw new ArgumentOutOfRangeException("column", "Column must be greater than or equal to 1.");
			}
			this.Index = index;
			this.Line = line;
			this.Column = column;
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x00040574 File Offset: 0x0003E774
		public override string ToString()
		{
			return string.Format("Line: {0}, Col: {1}, Idx: {2}", this.Line, this.Column, this.Index);
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x000405A1 File Offset: 0x0003E7A1
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Mark);
		}

		// Token: 0x06001007 RID: 4103 RVA: 0x000405AF File Offset: 0x0003E7AF
		public bool Equals(Mark other)
		{
			return other != null && this.Index == other.Index && this.Line == other.Line && this.Column == other.Column;
		}

		// Token: 0x06001008 RID: 4104 RVA: 0x000405E0 File Offset: 0x0003E7E0
		public override int GetHashCode()
		{
			return HashCode.CombineHashCodes(this.Index.GetHashCode(), HashCode.CombineHashCodes(this.Line.GetHashCode(), this.Column.GetHashCode()));
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x00040621 File Offset: 0x0003E821
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return this.CompareTo(obj as Mark);
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x00040640 File Offset: 0x0003E840
		public int CompareTo(Mark other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			int num = this.Line.CompareTo(other.Line);
			if (num == 0)
			{
				num = this.Column.CompareTo(other.Column);
			}
			return num;
		}

		// Token: 0x040008BF RID: 2239
		public static readonly Mark Empty = new Mark();
	}
}
