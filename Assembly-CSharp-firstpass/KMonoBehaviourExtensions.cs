using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000BC RID: 188
public static class KMonoBehaviourExtensions
{
	// Token: 0x06000709 RID: 1801 RVA: 0x0001E517 File Offset: 0x0001C717
	public static int Subscribe(this GameObject go, int hash, Action<object> handler)
	{
		return go.GetComponent<KMonoBehaviour>().Subscribe(hash, handler);
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0001E526 File Offset: 0x0001C726
	public static void Subscribe(this GameObject go, GameObject target, int hash, Action<object> handler)
	{
		go.GetComponent<KMonoBehaviour>().Subscribe(target, hash, handler);
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0001E538 File Offset: 0x0001C738
	public static void Unsubscribe(this GameObject go, int hash, Action<object> handler)
	{
		KMonoBehaviour component = go.GetComponent<KMonoBehaviour>();
		if (component != null)
		{
			component.Unsubscribe(hash, handler);
		}
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0001E560 File Offset: 0x0001C760
	public static void Unsubscribe(this GameObject go, int id)
	{
		KMonoBehaviour component = go.GetComponent<KMonoBehaviour>();
		if (component != null)
		{
			component.Unsubscribe(id);
		}
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0001E584 File Offset: 0x0001C784
	public static void Unsubscribe(this GameObject go, GameObject target, int hash, Action<object> handler)
	{
		KMonoBehaviour component = go.GetComponent<KMonoBehaviour>();
		if (component != null)
		{
			component.Unsubscribe(target, hash, handler);
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0001E5AC File Offset: 0x0001C7AC
	public static T GetComponentInChildrenOnly<T>(this GameObject go) where T : Component
	{
		foreach (T t in go.GetComponentsInChildren<T>())
		{
			if (t.gameObject != go)
			{
				return t;
			}
		}
		return default(T);
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0001E5F4 File Offset: 0x0001C7F4
	public static T[] GetComponentsInChildrenOnly<T>(this GameObject go) where T : Component
	{
		List<T> list = new List<T>();
		list.AddRange(go.GetComponentsInChildren<T>());
		list.RemoveAll((T t) => t.gameObject == go);
		return list.ToArray();
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0001E63C File Offset: 0x0001C83C
	public static void SetAlpha(this Image img, float alpha)
	{
		Color color = img.color;
		color.a = alpha;
		img.color = color;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0001E660 File Offset: 0x0001C860
	public static void SetAlpha(this Text txt, float alpha)
	{
		Color color = txt.color;
		color.a = alpha;
		txt.color = color;
	}
}
