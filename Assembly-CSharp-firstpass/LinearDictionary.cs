using System;
using System.Collections.Generic;

// Token: 0x020000CB RID: 203
public class LinearDictionary<Key, Val> where Key : IEquatable<Key>
{
	// Token: 0x060007C4 RID: 1988 RVA: 0x0001FED4 File Offset: 0x0001E0D4
	private int GetIdx(Key key)
	{
		int count = this.keys.Count;
		int num = -1;
		for (int i = 0; i < count; i++)
		{
			Key key2 = this.keys[i];
			if (key2.Equals(key))
			{
				num = i;
				break;
			}
		}
		return num;
	}

	// Token: 0x170000D8 RID: 216
	public Val this[Key key]
	{
		get
		{
			Val val = default(Val);
			int idx = this.GetIdx(key);
			if (idx != -1)
			{
				val = this.values[idx];
			}
			return val;
		}
		set
		{
			int idx = this.GetIdx(key);
			if (idx != -1)
			{
				this.values[idx] = value;
				return;
			}
			this.keys.Add(key);
			this.values.Add(value);
		}
	}

	// Token: 0x04000611 RID: 1553
	private List<Key> keys = new List<Key>();

	// Token: 0x04000612 RID: 1554
	private List<Val> values = new List<Val>();
}
