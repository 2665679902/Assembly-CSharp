using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace YamlDotNet.Helpers
{
	// Token: 0x020001ED RID: 493
	internal sealed class GenericDictionaryToNonGenericAdapter : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06000F2A RID: 3882 RVA: 0x0003D0DA File Offset: 0x0003B2DA
		public GenericDictionaryToNonGenericAdapter(object genericDictionary, Type genericDictionaryType)
		{
			this.genericDictionary = genericDictionary;
			this.genericDictionaryType = genericDictionaryType;
			this.indexerSetter = genericDictionaryType.GetPublicProperty("Item").GetSetMethod();
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0003D106 File Offset: 0x0003B306
		public void Add(object key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x0003D10D File Offset: 0x0003B30D
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x0003D114 File Offset: 0x0003B314
		public bool Contains(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x0003D11B File Offset: 0x0003B31B
		public IDictionaryEnumerator GetEnumerator()
		{
			return new GenericDictionaryToNonGenericAdapter.DictionaryEnumerator(this.genericDictionary, this.genericDictionaryType);
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0003D12E File Offset: 0x0003B32E
		public bool IsFixedSize
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0003D135 File Offset: 0x0003B335
		public bool IsReadOnly
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0003D13C File Offset: 0x0003B33C
		public ICollection Keys
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x0003D143 File Offset: 0x0003B343
		public void Remove(object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0003D14A File Offset: 0x0003B34A
		public ICollection Values
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700018F RID: 399
		public object this[object key]
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				this.indexerSetter.Invoke(this.genericDictionary, new object[] { key, value });
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0003D17A File Offset: 0x0003B37A
		public void CopyTo(Array array, int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0003D181 File Offset: 0x0003B381
		public int Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0003D188 File Offset: 0x0003B388
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x0003D18F File Offset: 0x0003B38F
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x0003D196 File Offset: 0x0003B396
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.genericDictionary).GetEnumerator();
		}

		// Token: 0x04000860 RID: 2144
		private readonly object genericDictionary;

		// Token: 0x04000861 RID: 2145
		private readonly Type genericDictionaryType;

		// Token: 0x04000862 RID: 2146
		private readonly MethodInfo indexerSetter;

		// Token: 0x02000A5E RID: 2654
		private class DictionaryEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x0600557B RID: 21883 RVA: 0x0009F104 File Offset: 0x0009D304
			public DictionaryEnumerator(object genericDictionary, Type genericDictionaryType)
			{
				Type[] genericArguments = genericDictionaryType.GetGenericArguments();
				Type type = typeof(KeyValuePair<, >).MakeGenericType(genericArguments);
				this.getKeyMethod = type.GetPublicProperty("Key").GetGetMethod();
				this.getValueMethod = type.GetPublicProperty("Value").GetGetMethod();
				this.enumerator = ((IEnumerable)genericDictionary).GetEnumerator();
			}

			// Token: 0x17000E74 RID: 3700
			// (get) Token: 0x0600557C RID: 21884 RVA: 0x0009F16C File Offset: 0x0009D36C
			public DictionaryEntry Entry
			{
				get
				{
					return new DictionaryEntry(this.Key, this.Value);
				}
			}

			// Token: 0x17000E75 RID: 3701
			// (get) Token: 0x0600557D RID: 21885 RVA: 0x0009F17F File Offset: 0x0009D37F
			public object Key
			{
				get
				{
					return this.getKeyMethod.Invoke(this.enumerator.Current, null);
				}
			}

			// Token: 0x17000E76 RID: 3702
			// (get) Token: 0x0600557E RID: 21886 RVA: 0x0009F198 File Offset: 0x0009D398
			public object Value
			{
				get
				{
					return this.getValueMethod.Invoke(this.enumerator.Current, null);
				}
			}

			// Token: 0x17000E77 RID: 3703
			// (get) Token: 0x0600557F RID: 21887 RVA: 0x0009F1B1 File Offset: 0x0009D3B1
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x06005580 RID: 21888 RVA: 0x0009F1BE File Offset: 0x0009D3BE
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06005581 RID: 21889 RVA: 0x0009F1CB File Offset: 0x0009D3CB
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04002352 RID: 9042
			private readonly IEnumerator enumerator;

			// Token: 0x04002353 RID: 9043
			private readonly MethodInfo getKeyMethod;

			// Token: 0x04002354 RID: 9044
			private readonly MethodInfo getValueMethod;
		}
	}
}
