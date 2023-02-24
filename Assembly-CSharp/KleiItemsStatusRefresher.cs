using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AD4 RID: 2772
public static class KleiItemsStatusRefresher
{
	// Token: 0x06005501 RID: 21761 RVA: 0x001ECDA5 File Offset: 0x001EAFA5
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void Initialize()
	{
		KleiItems.AddInventoryRefreshCallback(new KleiItems.InventoryRefreshCallback(KleiItemsStatusRefresher.OnRefreshResponseFromServer));
	}

	// Token: 0x06005502 RID: 21762 RVA: 0x001ECDB8 File Offset: 0x001EAFB8
	public static void RequestRefreshFromServer()
	{
		if (!KleiItemsStatusRefresher.Active)
		{
			return;
		}
		double realtimeSinceStartupAsDouble = Time.realtimeSinceStartupAsDouble;
		if (realtimeSinceStartupAsDouble - KleiItemsStatusRefresher.realtimeOfLastServerRequest < 360.0)
		{
			return;
		}
		KleiItems.AddRequestInventoryRefresh();
		KleiItemsStatusRefresher.realtimeOfLastServerRequest = realtimeSinceStartupAsDouble;
	}

	// Token: 0x06005503 RID: 21763 RVA: 0x001ECDF1 File Offset: 0x001EAFF1
	private static void OnRefreshResponseFromServer()
	{
		KleiItemsStatusRefresher.Active = false;
		KleiItemsStatusRefresher.RefreshUI();
	}

	// Token: 0x06005504 RID: 21764 RVA: 0x001ECE00 File Offset: 0x001EB000
	public static void RefreshUI()
	{
		foreach (KleiItemsStatusRefresher.UIListener uilistener in KleiItemsStatusRefresher.listeners)
		{
			uilistener.Internal_RefreshUI();
		}
	}

	// Token: 0x06005505 RID: 21765 RVA: 0x001ECE50 File Offset: 0x001EB050
	public static KleiItemsStatusRefresher.UIListener AddOrGetListener(Component component)
	{
		return KleiItemsStatusRefresher.AddOrGetListener(component.gameObject);
	}

	// Token: 0x06005506 RID: 21766 RVA: 0x001ECE5D File Offset: 0x001EB05D
	public static KleiItemsStatusRefresher.UIListener AddOrGetListener(GameObject onGameObject)
	{
		return onGameObject.AddOrGet<KleiItemsStatusRefresher.UIListener>();
	}

	// Token: 0x040039C3 RID: 14787
	public static bool Active = false;

	// Token: 0x040039C4 RID: 14788
	public const double SECONDS_PER_MINUTE = 60.0;

	// Token: 0x040039C5 RID: 14789
	public const double MINIMUM_SECONDS_BETWEEN_REFRESH_REQUESTS = 360.0;

	// Token: 0x040039C6 RID: 14790
	public static double realtimeOfLastServerRequest = -720.0;

	// Token: 0x040039C7 RID: 14791
	public static HashSet<KleiItemsStatusRefresher.UIListener> listeners = new HashSet<KleiItemsStatusRefresher.UIListener>();

	// Token: 0x02001957 RID: 6487
	public class UIListener : MonoBehaviour
	{
		// Token: 0x06008FF2 RID: 36850 RVA: 0x00311A53 File Offset: 0x0030FC53
		public void Internal_RefreshUI()
		{
			if (this.refreshUIFn != null)
			{
				this.refreshUIFn();
			}
		}

		// Token: 0x06008FF3 RID: 36851 RVA: 0x00311A68 File Offset: 0x0030FC68
		public void OnRefreshUI(System.Action fn)
		{
			this.refreshUIFn = fn;
		}

		// Token: 0x06008FF4 RID: 36852 RVA: 0x00311A71 File Offset: 0x0030FC71
		private void OnEnable()
		{
			KleiItemsStatusRefresher.listeners.Add(this);
		}

		// Token: 0x06008FF5 RID: 36853 RVA: 0x00311A7F File Offset: 0x0030FC7F
		private void OnDisable()
		{
			KleiItemsStatusRefresher.listeners.Remove(this);
		}

		// Token: 0x04007419 RID: 29721
		private System.Action refreshUIFn;
	}
}
