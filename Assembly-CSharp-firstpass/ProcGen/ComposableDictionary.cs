using System;
using System.Collections.Generic;

namespace ProcGen
{
	// Token: 0x020004D5 RID: 1237
	[Serializable]
	public class ComposableDictionary<Key, Value> : IMerge<ComposableDictionary<Key, Value>>
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x00074D56 File Offset: 0x00072F56
		// (set) Token: 0x06003524 RID: 13604 RVA: 0x00074D5E File Offset: 0x00072F5E
		public Dictionary<Key, Value> add { get; private set; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x00074D67 File Offset: 0x00072F67
		// (set) Token: 0x06003526 RID: 13606 RVA: 0x00074D6F File Offset: 0x00072F6F
		public List<Key> remove { get; private set; }

		// Token: 0x06003527 RID: 13607 RVA: 0x00074D78 File Offset: 0x00072F78
		public ComposableDictionary()
		{
			this.add = new Dictionary<Key, Value>();
			this.remove = new List<Key>();
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x00074D96 File Offset: 0x00072F96
		private void VerifyConsolidated()
		{
			DebugUtil.Assert(this.remove.Count == 0, "needs to be Consolidate()d before being used");
		}

		// Token: 0x17000364 RID: 868
		public Value this[Key key]
		{
			get
			{
				this.VerifyConsolidated();
				return this.add[key];
			}
			set
			{
				this.add[key] = value;
			}
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x0600352B RID: 13611 RVA: 0x00074DD3 File Offset: 0x00072FD3
		public ICollection<Key> Keys
		{
			get
			{
				this.VerifyConsolidated();
				return this.add.Keys;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x00074DE6 File Offset: 0x00072FE6
		public ICollection<Value> Values
		{
			get
			{
				this.VerifyConsolidated();
				return this.add.Values;
			}
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x00074DF9 File Offset: 0x00072FF9
		public void Add(Key key, Value value)
		{
			this.add.Add(key, value);
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x00074E08 File Offset: 0x00073008
		public void Add(KeyValuePair<Key, Value> pair)
		{
			this.Add(pair.Key, pair.Value);
		}

		// Token: 0x0600352F RID: 13615 RVA: 0x00074E1E File Offset: 0x0007301E
		public bool Remove(Key key)
		{
			this.add.Remove(key);
			return true;
		}

		// Token: 0x06003530 RID: 13616 RVA: 0x00074E2E File Offset: 0x0007302E
		public void Clear()
		{
			this.add.Clear();
		}

		// Token: 0x06003531 RID: 13617 RVA: 0x00074E3B File Offset: 0x0007303B
		public bool ContainsKey(Key key)
		{
			this.VerifyConsolidated();
			return this.add.ContainsKey(key);
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x00074E4F File Offset: 0x0007304F
		public bool TryGetValue(Key key, out Value value)
		{
			this.VerifyConsolidated();
			return this.add.TryGetValue(key, out value);
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x00074E64 File Offset: 0x00073064
		public int Count
		{
			get
			{
				this.VerifyConsolidated();
				return this.add.Count;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x00074E77 File Offset: 0x00073077
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003535 RID: 13621 RVA: 0x00074E7A File Offset: 0x0007307A
		public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator()
		{
			this.VerifyConsolidated();
			return this.add.GetEnumerator();
		}

		// Token: 0x06003536 RID: 13622 RVA: 0x00074E94 File Offset: 0x00073094
		public ComposableDictionary<Key, Value> Merge(ComposableDictionary<Key, Value> other)
		{
			this.VerifyConsolidated();
			foreach (Key key in other.remove)
			{
				this.add.Remove(key);
			}
			foreach (KeyValuePair<Key, Value> keyValuePair in other.add)
			{
				if (this.add.ContainsKey(keyValuePair.Key))
				{
					object[] array = new object[2];
					array[0] = "Overwriting entry {0}";
					int num = 1;
					Key key2 = keyValuePair.Key;
					array[num] = key2.ToString();
					DebugUtil.LogArgs(array);
				}
				this.add.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return this;
		}
	}
}
