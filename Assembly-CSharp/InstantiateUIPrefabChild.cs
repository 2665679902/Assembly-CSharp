using System;
using UnityEngine;

// Token: 0x02000AC1 RID: 2753
[AddComponentMenu("KMonoBehaviour/scripts/InstantiateUIPrefabChild")]
public class InstantiateUIPrefabChild : KMonoBehaviour
{
	// Token: 0x06005439 RID: 21561 RVA: 0x001E93C7 File Offset: 0x001E75C7
	protected override void OnPrefabInit()
	{
		if (this.InstantiateOnAwake)
		{
			this.Instantiate();
		}
	}

	// Token: 0x0600543A RID: 21562 RVA: 0x001E93D8 File Offset: 0x001E75D8
	public void Instantiate()
	{
		if (this.alreadyInstantiated)
		{
			global::Debug.LogWarning(base.gameObject.name + "trying to instantiate UI prefabs multiple times.");
			return;
		}
		this.alreadyInstantiated = true;
		foreach (GameObject gameObject in this.prefabs)
		{
			if (!(gameObject == null))
			{
				Vector3 vector = gameObject.rectTransform().anchoredPosition;
				GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(gameObject);
				gameObject2.transform.SetParent(base.transform);
				gameObject2.rectTransform().anchoredPosition = vector;
				gameObject2.rectTransform().localScale = Vector3.one;
				if (this.setAsFirstSibling)
				{
					gameObject2.transform.SetAsFirstSibling();
				}
			}
		}
	}

	// Token: 0x0400393F RID: 14655
	public GameObject[] prefabs;

	// Token: 0x04003940 RID: 14656
	public bool InstantiateOnAwake = true;

	// Token: 0x04003941 RID: 14657
	private bool alreadyInstantiated;

	// Token: 0x04003942 RID: 14658
	public bool setAsFirstSibling;
}
