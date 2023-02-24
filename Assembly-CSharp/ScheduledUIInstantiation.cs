using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000B83 RID: 2947
[AddComponentMenu("KMonoBehaviour/scripts/ScheduledUIInstantiation")]
public class ScheduledUIInstantiation : KMonoBehaviour
{
	// Token: 0x06005CA6 RID: 23718 RVA: 0x0021E1EF File Offset: 0x0021C3EF
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.InstantiateOnAwake)
		{
			this.InstantiateElements(null);
			return;
		}
		Game.Instance.Subscribe((int)this.InstantiationEvent, new Action<object>(this.InstantiateElements));
	}

	// Token: 0x06005CA7 RID: 23719 RVA: 0x0021E224 File Offset: 0x0021C424
	public void InstantiateElements(object data)
	{
		if (this.completed)
		{
			return;
		}
		this.completed = true;
		foreach (ScheduledUIInstantiation.Instantiation instantiation in this.UIElements)
		{
			foreach (GameObject gameObject in instantiation.prefabs)
			{
				if (DlcManager.IsContentActive(instantiation.RequiredDlcId))
				{
					Vector3 vector = gameObject.rectTransform().anchoredPosition;
					GameObject gameObject2 = Util.KInstantiateUI(gameObject, instantiation.parent.gameObject, false);
					gameObject2.rectTransform().anchoredPosition = vector;
					gameObject2.rectTransform().localScale = Vector3.one;
					this.instantiatedObjects.Add(gameObject2);
				}
			}
		}
		if (!this.InstantiateOnAwake)
		{
			base.Unsubscribe((int)this.InstantiationEvent, new Action<object>(this.InstantiateElements));
		}
	}

	// Token: 0x06005CA8 RID: 23720 RVA: 0x0021E30C File Offset: 0x0021C50C
	public T GetInstantiatedObject<T>() where T : Component
	{
		for (int i = 0; i < this.instantiatedObjects.Count; i++)
		{
			if (this.instantiatedObjects[i].GetComponent(typeof(T)) != null)
			{
				return this.instantiatedObjects[i].GetComponent(typeof(T)) as T;
			}
		}
		return default(T);
	}

	// Token: 0x04003F54 RID: 16212
	public ScheduledUIInstantiation.Instantiation[] UIElements;

	// Token: 0x04003F55 RID: 16213
	public bool InstantiateOnAwake;

	// Token: 0x04003F56 RID: 16214
	public GameHashes InstantiationEvent = GameHashes.StartGameUser;

	// Token: 0x04003F57 RID: 16215
	private bool completed;

	// Token: 0x04003F58 RID: 16216
	private List<GameObject> instantiatedObjects = new List<GameObject>();

	// Token: 0x02001A43 RID: 6723
	[Serializable]
	public struct Instantiation
	{
		// Token: 0x04007712 RID: 30482
		public string Name;

		// Token: 0x04007713 RID: 30483
		public string Comment;

		// Token: 0x04007714 RID: 30484
		public GameObject[] prefabs;

		// Token: 0x04007715 RID: 30485
		public Transform parent;

		// Token: 0x04007716 RID: 30486
		public string RequiredDlcId;
	}
}
