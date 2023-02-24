using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009EE RID: 2542
public class UIPool<T> where T : MonoBehaviour
{
	// Token: 0x170005BA RID: 1466
	// (get) Token: 0x06004BFF RID: 19455 RVA: 0x001AA344 File Offset: 0x001A8544
	public int ActiveElementsCount
	{
		get
		{
			return this.activeElements.Count;
		}
	}

	// Token: 0x170005BB RID: 1467
	// (get) Token: 0x06004C00 RID: 19456 RVA: 0x001AA351 File Offset: 0x001A8551
	public int FreeElementsCount
	{
		get
		{
			return this.freeElements.Count;
		}
	}

	// Token: 0x170005BC RID: 1468
	// (get) Token: 0x06004C01 RID: 19457 RVA: 0x001AA35E File Offset: 0x001A855E
	public int TotalElementsCount
	{
		get
		{
			return this.ActiveElementsCount + this.FreeElementsCount;
		}
	}

	// Token: 0x06004C02 RID: 19458 RVA: 0x001AA36D File Offset: 0x001A856D
	public UIPool(T prefab)
	{
		this.prefab = prefab;
		this.freeElements = new List<T>();
		this.activeElements = new List<T>();
	}

	// Token: 0x06004C03 RID: 19459 RVA: 0x001AA3A8 File Offset: 0x001A85A8
	public T GetFreeElement(GameObject instantiateParent = null, bool forceActive = false)
	{
		if (this.freeElements.Count == 0)
		{
			this.activeElements.Add(Util.KInstantiateUI<T>(this.prefab.gameObject, instantiateParent, false));
		}
		else
		{
			T t = this.freeElements[0];
			this.activeElements.Add(t);
			if (t.transform.parent != instantiateParent)
			{
				t.transform.SetParent(instantiateParent.transform);
			}
			this.freeElements.RemoveAt(0);
		}
		T t2 = this.activeElements[this.activeElements.Count - 1];
		if (t2.gameObject.activeInHierarchy != forceActive)
		{
			t2.gameObject.SetActive(forceActive);
		}
		return t2;
	}

	// Token: 0x06004C04 RID: 19460 RVA: 0x001AA478 File Offset: 0x001A8678
	public void ClearElement(T element)
	{
		if (!this.activeElements.Contains(element))
		{
			global::Debug.LogError(this.freeElements.Contains(element) ? "The element provided is already inactive" : "The element provided does not belong to this pool");
			return;
		}
		if (this.disabledElementParent != null)
		{
			element.gameObject.transform.SetParent(this.disabledElementParent);
		}
		element.gameObject.SetActive(false);
		this.freeElements.Add(element);
		this.activeElements.Remove(element);
	}

	// Token: 0x06004C05 RID: 19461 RVA: 0x001AA508 File Offset: 0x001A8708
	public void ClearAll()
	{
		while (this.activeElements.Count > 0)
		{
			if (this.disabledElementParent != null)
			{
				this.activeElements[0].gameObject.transform.SetParent(this.disabledElementParent);
			}
			this.activeElements[0].gameObject.SetActive(false);
			this.freeElements.Add(this.activeElements[0]);
			this.activeElements.RemoveAt(0);
		}
	}

	// Token: 0x06004C06 RID: 19462 RVA: 0x001AA59B File Offset: 0x001A879B
	public void DestroyAll()
	{
		this.DestroyAllActive();
		this.DestroyAllFree();
	}

	// Token: 0x06004C07 RID: 19463 RVA: 0x001AA5A9 File Offset: 0x001A87A9
	public void DestroyAllActive()
	{
		this.activeElements.ForEach(delegate(T ae)
		{
			UnityEngine.Object.Destroy(ae.gameObject);
		});
		this.activeElements.Clear();
	}

	// Token: 0x06004C08 RID: 19464 RVA: 0x001AA5E0 File Offset: 0x001A87E0
	public void DestroyAllFree()
	{
		this.freeElements.ForEach(delegate(T ae)
		{
			UnityEngine.Object.Destroy(ae.gameObject);
		});
		this.freeElements.Clear();
	}

	// Token: 0x06004C09 RID: 19465 RVA: 0x001AA617 File Offset: 0x001A8817
	public void ForEachActiveElement(Action<T> predicate)
	{
		this.activeElements.ForEach(predicate);
	}

	// Token: 0x06004C0A RID: 19466 RVA: 0x001AA625 File Offset: 0x001A8825
	public void ForEachFreeElement(Action<T> predicate)
	{
		this.freeElements.ForEach(predicate);
	}

	// Token: 0x04003210 RID: 12816
	private T prefab;

	// Token: 0x04003211 RID: 12817
	private List<T> freeElements = new List<T>();

	// Token: 0x04003212 RID: 12818
	private List<T> activeElements = new List<T>();

	// Token: 0x04003213 RID: 12819
	public Transform disabledElementParent;
}
