using System;
using UnityEngine;

// Token: 0x0200067C RID: 1660
public class CellSelectionInstantiator : MonoBehaviour
{
	// Token: 0x06002CBC RID: 11452 RVA: 0x000EA7BC File Offset: 0x000E89BC
	private void Awake()
	{
		GameObject gameObject = Util.KInstantiate(this.CellSelectionPrefab, null, "WorldSelectionCollider");
		GameObject gameObject2 = Util.KInstantiate(this.CellSelectionPrefab, null, "WorldSelectionCollider");
		CellSelectionObject component = gameObject.GetComponent<CellSelectionObject>();
		CellSelectionObject component2 = gameObject2.GetComponent<CellSelectionObject>();
		component.alternateSelectionObject = component2;
		component2.alternateSelectionObject = component;
	}

	// Token: 0x04001AA6 RID: 6822
	public GameObject CellSelectionPrefab;
}
