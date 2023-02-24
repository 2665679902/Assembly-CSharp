using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200087A RID: 2170
public class OnDemandUpdater : MonoBehaviour
{
	// Token: 0x06003E50 RID: 15952 RVA: 0x0015C7D7 File Offset: 0x0015A9D7
	public static void DestroyInstance()
	{
		OnDemandUpdater.Instance = null;
	}

	// Token: 0x06003E51 RID: 15953 RVA: 0x0015C7DF File Offset: 0x0015A9DF
	private void Awake()
	{
		OnDemandUpdater.Instance = this;
	}

	// Token: 0x06003E52 RID: 15954 RVA: 0x0015C7E7 File Offset: 0x0015A9E7
	public void Register(IUpdateOnDemand updater)
	{
		if (!this.Updaters.Contains(updater))
		{
			this.Updaters.Add(updater);
		}
	}

	// Token: 0x06003E53 RID: 15955 RVA: 0x0015C803 File Offset: 0x0015AA03
	public void Unregister(IUpdateOnDemand updater)
	{
		if (this.Updaters.Contains(updater))
		{
			this.Updaters.Remove(updater);
		}
	}

	// Token: 0x06003E54 RID: 15956 RVA: 0x0015C820 File Offset: 0x0015AA20
	private void Update()
	{
		for (int i = 0; i < this.Updaters.Count; i++)
		{
			if (this.Updaters[i] != null)
			{
				this.Updaters[i].UpdateOnDemand();
			}
		}
	}

	// Token: 0x040028CB RID: 10443
	private List<IUpdateOnDemand> Updaters = new List<IUpdateOnDemand>();

	// Token: 0x040028CC RID: 10444
	public static OnDemandUpdater Instance;
}
