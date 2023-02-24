using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009ED RID: 2541
public class UIGameObjectPool
{
	// Token: 0x170005B7 RID: 1463
	// (get) Token: 0x06004BF3 RID: 19443 RVA: 0x001AA059 File Offset: 0x001A8259
	public int ActiveElementsCount
	{
		get
		{
			return this.activeElements.Count;
		}
	}

	// Token: 0x170005B8 RID: 1464
	// (get) Token: 0x06004BF4 RID: 19444 RVA: 0x001AA066 File Offset: 0x001A8266
	public int FreeElementsCount
	{
		get
		{
			return this.freeElements.Count;
		}
	}

	// Token: 0x170005B9 RID: 1465
	// (get) Token: 0x06004BF5 RID: 19445 RVA: 0x001AA073 File Offset: 0x001A8273
	public int TotalElementsCount
	{
		get
		{
			return this.ActiveElementsCount + this.FreeElementsCount;
		}
	}

	// Token: 0x06004BF6 RID: 19446 RVA: 0x001AA082 File Offset: 0x001A8282
	public UIGameObjectPool(GameObject prefab)
	{
		this.prefab = prefab;
		this.freeElements = new List<GameObject>();
		this.activeElements = new List<GameObject>();
	}

	// Token: 0x06004BF7 RID: 19447 RVA: 0x001AA0C0 File Offset: 0x001A82C0
	public GameObject GetFreeElement(GameObject instantiateParent = null, bool forceActive = false)
	{
		if (this.freeElements.Count == 0)
		{
			this.activeElements.Add(Util.KInstantiateUI(this.prefab.gameObject, instantiateParent, false));
		}
		else
		{
			GameObject gameObject = this.freeElements[0];
			this.activeElements.Add(gameObject);
			if (gameObject.transform.parent != instantiateParent)
			{
				gameObject.transform.SetParent(instantiateParent.transform);
			}
			this.freeElements.RemoveAt(0);
		}
		GameObject gameObject2 = this.activeElements[this.activeElements.Count - 1];
		if (gameObject2.gameObject.activeInHierarchy != forceActive)
		{
			gameObject2.gameObject.SetActive(forceActive);
		}
		return gameObject2;
	}

	// Token: 0x06004BF8 RID: 19448 RVA: 0x001AA178 File Offset: 0x001A8378
	public void ClearElement(GameObject element)
	{
		if (!this.activeElements.Contains(element))
		{
			object obj = (this.freeElements.Contains(element) ? (element.name + ": The element provided is already inactive") : (element.name + ": The element provided does not belong to this pool"));
			element.SetActive(false);
			if (this.disabledElementParent != null)
			{
				element.transform.SetParent(this.disabledElementParent);
			}
			global::Debug.LogError(obj);
			return;
		}
		if (this.disabledElementParent != null)
		{
			element.transform.SetParent(this.disabledElementParent);
		}
		element.SetActive(false);
		this.freeElements.Add(element);
		this.activeElements.Remove(element);
	}

	// Token: 0x06004BF9 RID: 19449 RVA: 0x001AA230 File Offset: 0x001A8430
	public void ClearAll()
	{
		while (this.activeElements.Count > 0)
		{
			if (this.disabledElementParent != null)
			{
				this.activeElements[0].transform.SetParent(this.disabledElementParent);
			}
			this.activeElements[0].SetActive(false);
			this.freeElements.Add(this.activeElements[0]);
			this.activeElements.RemoveAt(0);
		}
	}

	// Token: 0x06004BFA RID: 19450 RVA: 0x001AA2AC File Offset: 0x001A84AC
	public void DestroyAll()
	{
		this.DestroyAllActive();
		this.DestroyAllFree();
	}

	// Token: 0x06004BFB RID: 19451 RVA: 0x001AA2BA File Offset: 0x001A84BA
	public void DestroyAllActive()
	{
		this.activeElements.ForEach(delegate(GameObject ae)
		{
			UnityEngine.Object.Destroy(ae);
		});
		this.activeElements.Clear();
	}

	// Token: 0x06004BFC RID: 19452 RVA: 0x001AA2F1 File Offset: 0x001A84F1
	public void DestroyAllFree()
	{
		this.freeElements.ForEach(delegate(GameObject ae)
		{
			UnityEngine.Object.Destroy(ae);
		});
		this.freeElements.Clear();
	}

	// Token: 0x06004BFD RID: 19453 RVA: 0x001AA328 File Offset: 0x001A8528
	public void ForEachActiveElement(Action<GameObject> predicate)
	{
		this.activeElements.ForEach(predicate);
	}

	// Token: 0x06004BFE RID: 19454 RVA: 0x001AA336 File Offset: 0x001A8536
	public void ForEachFreeElement(Action<GameObject> predicate)
	{
		this.freeElements.ForEach(predicate);
	}

	// Token: 0x0400320C RID: 12812
	private GameObject prefab;

	// Token: 0x0400320D RID: 12813
	private List<GameObject> freeElements = new List<GameObject>();

	// Token: 0x0400320E RID: 12814
	private List<GameObject> activeElements = new List<GameObject>();

	// Token: 0x0400320F RID: 12815
	public Transform disabledElementParent;
}
