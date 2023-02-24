using System;
using System.Collections;
using System.Reflection;

namespace YamlDotNet.Helpers
{
	// Token: 0x020001EC RID: 492
	internal sealed class GenericCollectionToNonGenericAdapter : IList, ICollection, IEnumerable
	{
		// Token: 0x06000F19 RID: 3865 RVA: 0x0003CFA4 File Offset: 0x0003B1A4
		public GenericCollectionToNonGenericAdapter(object genericCollection, Type genericCollectionType, Type genericListType)
		{
			this.genericCollection = genericCollection;
			this.addMethod = genericCollectionType.GetPublicInstanceMethod("Add");
			this.countGetter = genericCollectionType.GetPublicProperty("Count").GetGetMethod();
			if (genericListType != null)
			{
				this.indexerSetter = genericListType.GetPublicProperty("Item").GetSetMethod();
			}
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0003D004 File Offset: 0x0003B204
		public int Add(object value)
		{
			int num = (int)this.countGetter.Invoke(this.genericCollection, null);
			this.addMethod.Invoke(this.genericCollection, new object[] { value });
			return num;
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003D046 File Offset: 0x0003B246
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0003D04D File Offset: 0x0003B24D
		public bool Contains(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003D054 File Offset: 0x0003B254
		public int IndexOf(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0003D05B File Offset: 0x0003B25B
		public void Insert(int index, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000F1F RID: 3871 RVA: 0x0003D062 File Offset: 0x0003B262
		public bool IsFixedSize
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0003D069 File Offset: 0x0003B269
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x0003D070 File Offset: 0x0003B270
		public void Remove(object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0003D077 File Offset: 0x0003B277
		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000187 RID: 391
		public object this[int index]
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.indexerSetter.Invoke(this.genericCollection, new object[] { index, value });
			}
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003D0AC File Offset: 0x0003B2AC
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0003D0B3 File Offset: 0x0003B2B3
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0003D0BA File Offset: 0x0003B2BA
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0003D0C1 File Offset: 0x0003B2C1
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this.genericCollection).GetEnumerator();
		}

		// Token: 0x0400085C RID: 2140
		private readonly object genericCollection;

		// Token: 0x0400085D RID: 2141
		private readonly MethodInfo addMethod;

		// Token: 0x0400085E RID: 2142
		private readonly MethodInfo indexerSetter;

		// Token: 0x0400085F RID: 2143
		private readonly MethodInfo countGetter;
	}
}
