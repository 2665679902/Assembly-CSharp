using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000368 RID: 872
public class UIPrefabLocalPool
{
	// Token: 0x060011BD RID: 4541 RVA: 0x0005E108 File Offset: 0x0005C308
	public UIPrefabLocalPool(GameObject sourcePrefab, GameObject parent)
	{
		this.sourcePrefab = sourcePrefab;
		this.parent = parent;
	}

	// Token: 0x060011BE RID: 4542 RVA: 0x0005E134 File Offset: 0x0005C334
	public GameObject Borrow()
	{
		GameObject gameObject;
		if (this.availableInstances.Count == 0)
		{
			gameObject = Util.KInstantiateUI(this.sourcePrefab, this.parent, true);
		}
		else
		{
			gameObject = this.availableInstances.First<KeyValuePair<int, GameObject>>().Value;
			this.availableInstances.Remove(gameObject.GetInstanceID());
		}
		this.checkedOutInstances.Add(gameObject.GetInstanceID(), gameObject);
		gameObject.SetActive(true);
		gameObject.transform.SetAsLastSibling();
		return gameObject;
	}

	// Token: 0x060011BF RID: 4543 RVA: 0x0005E1AE File Offset: 0x0005C3AE
	public void Return(GameObject instance)
	{
		this.checkedOutInstances.Remove(instance.GetInstanceID());
		this.availableInstances.Add(instance.GetInstanceID(), instance);
		instance.SetActive(false);
	}

	// Token: 0x060011C0 RID: 4544 RVA: 0x0005E1DC File Offset: 0x0005C3DC
	public void ReturnAll()
	{
		foreach (KeyValuePair<int, GameObject> keyValuePair in this.checkedOutInstances)
		{
			int num;
			GameObject gameObject;
			keyValuePair.Deconstruct(out num, out gameObject);
			int num2 = num;
			GameObject gameObject2 = gameObject;
			this.availableInstances.Add(num2, gameObject2);
			gameObject2.SetActive(false);
		}
		this.checkedOutInstances.Clear();
	}

	// Token: 0x060011C1 RID: 4545 RVA: 0x0005E254 File Offset: 0x0005C454
	public IEnumerable<GameObject> GetBorrowedObjects()
	{
		return this.checkedOutInstances.Values;
	}

	// Token: 0x0400098F RID: 2447
	public readonly GameObject sourcePrefab;

	// Token: 0x04000990 RID: 2448
	public readonly GameObject parent;

	// Token: 0x04000991 RID: 2449
	private Dictionary<int, GameObject> checkedOutInstances = new Dictionary<int, GameObject>();

	// Token: 0x04000992 RID: 2450
	private Dictionary<int, GameObject> availableInstances = new Dictionary<int, GameObject>();
}
