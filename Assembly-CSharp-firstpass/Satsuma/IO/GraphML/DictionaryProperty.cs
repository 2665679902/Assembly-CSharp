using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Satsuma.IO.GraphML
{
	// Token: 0x0200028D RID: 653
	public abstract class DictionaryProperty<T> : GraphMLProperty, IClearable
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001433 RID: 5171 RVA: 0x0004DE21 File Offset: 0x0004C021
		// (set) Token: 0x06001434 RID: 5172 RVA: 0x0004DE29 File Offset: 0x0004C029
		public bool HasDefaultValue { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001435 RID: 5173 RVA: 0x0004DE32 File Offset: 0x0004C032
		// (set) Token: 0x06001436 RID: 5174 RVA: 0x0004DE3A File Offset: 0x0004C03A
		public T DefaultValue { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x0004DE43 File Offset: 0x0004C043
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x0004DE4B File Offset: 0x0004C04B
		public Dictionary<object, T> Values { get; private set; }

		// Token: 0x06001439 RID: 5177 RVA: 0x0004DE54 File Offset: 0x0004C054
		protected DictionaryProperty()
		{
			this.HasDefaultValue = false;
			this.Values = new Dictionary<object, T>();
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0004DE6E File Offset: 0x0004C06E
		public void Clear()
		{
			this.HasDefaultValue = false;
			this.Values.Clear();
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0004DE82 File Offset: 0x0004C082
		public bool TryGetValue(object key, out T result)
		{
			if (this.Values.TryGetValue(key, out result))
			{
				return true;
			}
			if (this.HasDefaultValue)
			{
				result = this.DefaultValue;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x0004DEB4 File Offset: 0x0004C0B4
		public override void ReadData(XElement x, object key)
		{
			if (x == null)
			{
				if (key == null)
				{
					this.HasDefaultValue = false;
					return;
				}
				this.Values.Remove(key);
				return;
			}
			else
			{
				T t = this.ReadValue(x);
				if (key == null)
				{
					this.HasDefaultValue = true;
					this.DefaultValue = t;
					return;
				}
				this.Values[key] = t;
				return;
			}
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x0004DF04 File Offset: 0x0004C104
		public override XElement WriteData(object key)
		{
			if (key == null)
			{
				if (!this.HasDefaultValue)
				{
					return null;
				}
				return this.WriteValue(this.DefaultValue);
			}
			else
			{
				T t;
				if (!this.Values.TryGetValue(key, out t))
				{
					return null;
				}
				return this.WriteValue(t);
			}
		}

		// Token: 0x0600143E RID: 5182
		protected abstract T ReadValue(XElement x);

		// Token: 0x0600143F RID: 5183
		protected abstract XElement WriteValue(T value);
	}
}
