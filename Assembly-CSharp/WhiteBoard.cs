using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009D0 RID: 2512
public class WhiteBoard : KGameObjectComponentManager<WhiteBoard.Data>, IKComponentManager
{
	// Token: 0x06004AAB RID: 19115 RVA: 0x001A22F0 File Offset: 0x001A04F0
	public HandleVector<int>.Handle Add(GameObject go)
	{
		return base.Add(go, new WhiteBoard.Data
		{
			keyValueStore = new Dictionary<HashedString, object>()
		});
	}

	// Token: 0x06004AAC RID: 19116 RVA: 0x001A231C File Offset: 0x001A051C
	protected override void OnCleanUp(HandleVector<int>.Handle h)
	{
		WhiteBoard.Data data = base.GetData(h);
		data.keyValueStore.Clear();
		data.keyValueStore = null;
		base.SetData(h, data);
	}

	// Token: 0x06004AAD RID: 19117 RVA: 0x001A234C File Offset: 0x001A054C
	public bool HasValue(HandleVector<int>.Handle h, HashedString key)
	{
		return h.IsValid() && base.GetData(h).keyValueStore.ContainsKey(key);
	}

	// Token: 0x06004AAE RID: 19118 RVA: 0x001A236B File Offset: 0x001A056B
	public object GetValue(HandleVector<int>.Handle h, HashedString key)
	{
		return base.GetData(h).keyValueStore[key];
	}

	// Token: 0x06004AAF RID: 19119 RVA: 0x001A2380 File Offset: 0x001A0580
	public void SetValue(HandleVector<int>.Handle h, HashedString key, object value)
	{
		if (!h.IsValid())
		{
			return;
		}
		WhiteBoard.Data data = base.GetData(h);
		data.keyValueStore[key] = value;
		base.SetData(h, data);
	}

	// Token: 0x06004AB0 RID: 19120 RVA: 0x001A23B4 File Offset: 0x001A05B4
	public void RemoveValue(HandleVector<int>.Handle h, HashedString key)
	{
		if (!h.IsValid())
		{
			return;
		}
		WhiteBoard.Data data = base.GetData(h);
		data.keyValueStore.Remove(key);
		base.SetData(h, data);
	}

	// Token: 0x020017D1 RID: 6097
	public struct Data
	{
		// Token: 0x04006E1D RID: 28189
		public Dictionary<HashedString, object> keyValueStore;
	}
}
