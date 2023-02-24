using System;
using System.Collections;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	// Token: 0x020001C7 RID: 455
	public sealed class ArrayNodeDeserializer : INodeDeserializer
	{
		// Token: 0x06000E20 RID: 3616 RVA: 0x0003A554 File Offset: 0x00038754
		bool INodeDeserializer.Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object> nestedObjectDeserializer, out object value)
		{
			if (!expectedType.IsArray)
			{
				value = false;
				return false;
			}
			Type elementType = expectedType.GetElementType();
			ArrayNodeDeserializer.ArrayList arrayList = new ArrayNodeDeserializer.ArrayList();
			CollectionNodeDeserializer.DeserializeHelper(elementType, parser, nestedObjectDeserializer, arrayList, true);
			Array array = Array.CreateInstance(elementType, arrayList.Count);
			arrayList.CopyTo(array, 0);
			value = array;
			return true;
		}

		// Token: 0x02000A4F RID: 2639
		private sealed class ArrayList : IList, ICollection, IEnumerable
		{
			// Token: 0x0600552C RID: 21804 RVA: 0x0009E5FA File Offset: 0x0009C7FA
			public ArrayList()
			{
				this.Clear();
			}

			// Token: 0x0600552D RID: 21805 RVA: 0x0009E608 File Offset: 0x0009C808
			public int Add(object value)
			{
				if (this.count == this.data.Length)
				{
					Array.Resize<object>(ref this.data, this.data.Length * 2);
				}
				this.data[this.count] = value;
				int num = this.count;
				this.count = num + 1;
				return num;
			}

			// Token: 0x0600552E RID: 21806 RVA: 0x0009E659 File Offset: 0x0009C859
			public void Clear()
			{
				this.data = new object[10];
				this.count = 0;
			}

			// Token: 0x0600552F RID: 21807 RVA: 0x0009E66F File Offset: 0x0009C86F
			public bool Contains(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005530 RID: 21808 RVA: 0x0009E676 File Offset: 0x0009C876
			public int IndexOf(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005531 RID: 21809 RVA: 0x0009E67D File Offset: 0x0009C87D
			public void Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000E66 RID: 3686
			// (get) Token: 0x06005532 RID: 21810 RVA: 0x0009E684 File Offset: 0x0009C884
			public bool IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000E67 RID: 3687
			// (get) Token: 0x06005533 RID: 21811 RVA: 0x0009E687 File Offset: 0x0009C887
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06005534 RID: 21812 RVA: 0x0009E68A File Offset: 0x0009C88A
			public void Remove(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06005535 RID: 21813 RVA: 0x0009E691 File Offset: 0x0009C891
			public void RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000E68 RID: 3688
			public object this[int index]
			{
				get
				{
					return this.data[index];
				}
				set
				{
					this.data[index] = value;
				}
			}

			// Token: 0x06005538 RID: 21816 RVA: 0x0009E6AD File Offset: 0x0009C8AD
			public void CopyTo(Array array, int index)
			{
				Array.Copy(this.data, 0, array, index, this.count);
			}

			// Token: 0x17000E69 RID: 3689
			// (get) Token: 0x06005539 RID: 21817 RVA: 0x0009E6C3 File Offset: 0x0009C8C3
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17000E6A RID: 3690
			// (get) Token: 0x0600553A RID: 21818 RVA: 0x0009E6CB File Offset: 0x0009C8CB
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000E6B RID: 3691
			// (get) Token: 0x0600553B RID: 21819 RVA: 0x0009E6CE File Offset: 0x0009C8CE
			public object SyncRoot
			{
				get
				{
					return this.data;
				}
			}

			// Token: 0x0600553C RID: 21820 RVA: 0x0009E6D6 File Offset: 0x0009C8D6
			public IEnumerator GetEnumerator()
			{
				int num;
				for (int i = 0; i < this.count; i = num)
				{
					yield return this.data[i];
					num = i + 1;
				}
				yield break;
			}

			// Token: 0x04002324 RID: 8996
			private object[] data;

			// Token: 0x04002325 RID: 8997
			private int count;
		}
	}
}
