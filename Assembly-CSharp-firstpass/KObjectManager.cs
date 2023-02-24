using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000BE RID: 190
public class KObjectManager : MonoBehaviour
{
	// Token: 0x170000CC RID: 204
	// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001E717 File Offset: 0x0001C917
	// (set) Token: 0x0600071A RID: 1818 RVA: 0x0001E71E File Offset: 0x0001C91E
	public static KObjectManager Instance { get; private set; }

	// Token: 0x0600071B RID: 1819 RVA: 0x0001E726 File Offset: 0x0001C926
	public static void DestroyInstance()
	{
		KObjectManager.Instance = null;
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0001E72E File Offset: 0x0001C92E
	private void Awake()
	{
		global::Debug.Assert(KObjectManager.Instance == null);
		KObjectManager.Instance = this;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0001E746 File Offset: 0x0001C946
	private void OnDestroy()
	{
		global::Debug.Assert(KObjectManager.Instance != null);
		global::Debug.Assert(KObjectManager.Instance == this);
		this.Cleanup();
		KObjectManager.Instance = null;
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0001E774 File Offset: 0x0001C974
	public void Cleanup()
	{
		foreach (KeyValuePair<int, KObject> keyValuePair in this.objects)
		{
			keyValuePair.Value.OnCleanUp();
		}
		this.objects.Clear();
		this.pendingDestroys.Clear();
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0001E7E4 File Offset: 0x0001C9E4
	public KObject GetOrCreateObject(GameObject go)
	{
		int instanceID = go.GetInstanceID();
		KObject kobject = null;
		if (!this.objects.TryGetValue(instanceID, out kobject))
		{
			kobject = new KObject(go);
			this.objects[instanceID] = kobject;
		}
		return kobject;
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0001E820 File Offset: 0x0001CA20
	public KObject Get(GameObject go)
	{
		KObject kobject = null;
		this.objects.TryGetValue(go.GetInstanceID(), out kobject);
		return kobject;
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0001E844 File Offset: 0x0001CA44
	public void QueueDestroy(KObject obj)
	{
		int id = obj.id;
		if (!this.pendingDestroys.Contains(id))
		{
			this.pendingDestroys.Add(id);
		}
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0001E874 File Offset: 0x0001CA74
	private void LateUpdate()
	{
		for (int i = 0; i < this.pendingDestroys.Count; i++)
		{
			int num = this.pendingDestroys[i];
			KObject kobject = null;
			if (this.objects.TryGetValue(num, out kobject))
			{
				this.objects.Remove(num);
				kobject.OnCleanUp();
			}
		}
		this.pendingDestroys.Clear();
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
	public void DumpEventData()
	{
	}

	// Token: 0x040005DC RID: 1500
	private Dictionary<int, KObject> objects = new Dictionary<int, KObject>();

	// Token: 0x040005DD RID: 1501
	private List<int> pendingDestroys = new List<int>();
}
