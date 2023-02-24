using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000816 RID: 2070
[AddComponentMenu("KMonoBehaviour/scripts/MinionGroupProber")]
public class MinionGroupProber : KMonoBehaviour, IGroupProber
{
	// Token: 0x06003C09 RID: 15369 RVA: 0x0014D5F8 File Offset: 0x0014B7F8
	public static void DestroyInstance()
	{
		MinionGroupProber.Instance = null;
	}

	// Token: 0x06003C0A RID: 15370 RVA: 0x0014D600 File Offset: 0x0014B800
	public static MinionGroupProber Get()
	{
		return MinionGroupProber.Instance;
	}

	// Token: 0x06003C0B RID: 15371 RVA: 0x0014D607 File Offset: 0x0014B807
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MinionGroupProber.Instance = this;
		this.cells = new Dictionary<object, short>[Grid.CellCount];
	}

	// Token: 0x06003C0C RID: 15372 RVA: 0x0014D628 File Offset: 0x0014B828
	private bool IsReachable_AssumeLock(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		Dictionary<object, short> dictionary = this.cells[cell];
		if (dictionary == null)
		{
			return false;
		}
		bool flag = false;
		foreach (KeyValuePair<object, short> keyValuePair in dictionary)
		{
			object key = keyValuePair.Key;
			short value = keyValuePair.Value;
			KeyValuePair<short, short> keyValuePair2;
			if (this.valid_serial_nos.TryGetValue(key, out keyValuePair2) && (value == keyValuePair2.Key || value == keyValuePair2.Value))
			{
				flag = true;
				break;
			}
			this.pending_removals.Add(key);
		}
		foreach (object obj in this.pending_removals)
		{
			dictionary.Remove(obj);
			if (dictionary.Count == 0)
			{
				this.cells[cell] = null;
			}
		}
		this.pending_removals.Clear();
		return flag;
	}

	// Token: 0x06003C0D RID: 15373 RVA: 0x0014D738 File Offset: 0x0014B938
	public bool IsReachable(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		bool flag = false;
		object obj = this.access;
		lock (obj)
		{
			flag = this.IsReachable_AssumeLock(cell);
		}
		return flag;
	}

	// Token: 0x06003C0E RID: 15374 RVA: 0x0014D788 File Offset: 0x0014B988
	public bool IsReachable(int cell, CellOffset[] offsets)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		bool flag = false;
		object obj = this.access;
		lock (obj)
		{
			foreach (CellOffset cellOffset in offsets)
			{
				if (this.IsReachable_AssumeLock(Grid.OffsetCell(cell, cellOffset)))
				{
					flag = true;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x06003C0F RID: 15375 RVA: 0x0014D804 File Offset: 0x0014BA04
	public bool IsAllReachable(int cell, CellOffset[] offsets)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		bool flag = false;
		object obj = this.access;
		lock (obj)
		{
			if (this.IsReachable_AssumeLock(cell))
			{
				flag = true;
			}
			else
			{
				foreach (CellOffset cellOffset in offsets)
				{
					if (this.IsReachable_AssumeLock(Grid.OffsetCell(cell, cellOffset)))
					{
						flag = true;
						break;
					}
				}
			}
		}
		return flag;
	}

	// Token: 0x06003C10 RID: 15376 RVA: 0x0014D88C File Offset: 0x0014BA8C
	public bool IsReachable(Workable workable)
	{
		return this.IsReachable(Grid.PosToCell(workable), workable.GetOffsets());
	}

	// Token: 0x06003C11 RID: 15377 RVA: 0x0014D8A0 File Offset: 0x0014BAA0
	public void Occupy(object prober, short serial_no, IEnumerable<int> cells)
	{
		object obj = this.access;
		lock (obj)
		{
			foreach (int num in cells)
			{
				if (this.cells[num] == null)
				{
					this.cells[num] = new Dictionary<object, short>();
				}
				this.cells[num][prober] = serial_no;
			}
		}
	}

	// Token: 0x06003C12 RID: 15378 RVA: 0x0014D930 File Offset: 0x0014BB30
	public void SetValidSerialNos(object prober, short previous_serial_no, short serial_no)
	{
		object obj = this.access;
		lock (obj)
		{
			this.valid_serial_nos[prober] = new KeyValuePair<short, short>(previous_serial_no, serial_no);
		}
	}

	// Token: 0x06003C13 RID: 15379 RVA: 0x0014D980 File Offset: 0x0014BB80
	public bool ReleaseProber(object prober)
	{
		object obj = this.access;
		bool flag2;
		lock (obj)
		{
			flag2 = this.valid_serial_nos.Remove(prober);
		}
		return flag2;
	}

	// Token: 0x04002715 RID: 10005
	private static MinionGroupProber Instance;

	// Token: 0x04002716 RID: 10006
	private Dictionary<object, short>[] cells;

	// Token: 0x04002717 RID: 10007
	private Dictionary<object, KeyValuePair<short, short>> valid_serial_nos = new Dictionary<object, KeyValuePair<short, short>>();

	// Token: 0x04002718 RID: 10008
	private List<object> pending_removals = new List<object>();

	// Token: 0x04002719 RID: 10009
	private readonly object access = new object();
}
