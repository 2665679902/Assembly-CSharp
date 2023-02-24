using System;

namespace MIConvexHull
{
	// Token: 0x0200049B RID: 1179
	internal class SimpleList<T>
	{
		// Token: 0x170002E2 RID: 738
		public T this[int i]
		{
			get
			{
				return this.items[i];
			}
			set
			{
				this.items[i] = value;
			}
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x00069840 File Offset: 0x00067A40
		private void EnsureCapacity()
		{
			if (this.capacity == 0)
			{
				this.capacity = 32;
				this.items = new T[32];
				return;
			}
			T[] array = new T[this.capacity * 2];
			Array.Copy(this.items, array, this.capacity);
			this.capacity = 2 * this.capacity;
			this.items = array;
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000698A0 File Offset: 0x00067AA0
		public void Add(T item)
		{
			if (this.Count + 1 > this.capacity)
			{
				this.EnsureCapacity();
			}
			T[] array = this.items;
			int count = this.Count;
			this.Count = count + 1;
			array[count] = item;
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x000698E0 File Offset: 0x00067AE0
		public void Push(T item)
		{
			if (this.Count + 1 > this.capacity)
			{
				this.EnsureCapacity();
			}
			T[] array = this.items;
			int count = this.Count;
			this.Count = count + 1;
			array[count] = item;
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x00069920 File Offset: 0x00067B20
		public T Pop()
		{
			T[] array = this.items;
			int num = this.Count - 1;
			this.Count = num;
			return array[num];
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x00069949 File Offset: 0x00067B49
		public void Clear()
		{
			this.Count = 0;
		}

		// Token: 0x0400119E RID: 4510
		private int capacity;

		// Token: 0x0400119F RID: 4511
		public int Count;

		// Token: 0x040011A0 RID: 4512
		private T[] items;
	}
}
