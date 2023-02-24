using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200044F RID: 1103
[AddComponentMenu("KMonoBehaviour/scripts/AutoDisinfectableManager")]
public class AutoDisinfectableManager : KMonoBehaviour, ISim1000ms
{
	// Token: 0x060017D9 RID: 6105 RVA: 0x0007CF3F File Offset: 0x0007B13F
	public static void DestroyInstance()
	{
		AutoDisinfectableManager.Instance = null;
	}

	// Token: 0x060017DA RID: 6106 RVA: 0x0007CF47 File Offset: 0x0007B147
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		AutoDisinfectableManager.Instance = this;
	}

	// Token: 0x060017DB RID: 6107 RVA: 0x0007CF55 File Offset: 0x0007B155
	public void AddAutoDisinfectable(AutoDisinfectable auto_disinfectable)
	{
		this.autoDisinfectables.Add(auto_disinfectable);
	}

	// Token: 0x060017DC RID: 6108 RVA: 0x0007CF63 File Offset: 0x0007B163
	public void RemoveAutoDisinfectable(AutoDisinfectable auto_disinfectable)
	{
		auto_disinfectable.CancelChore();
		this.autoDisinfectables.Remove(auto_disinfectable);
	}

	// Token: 0x060017DD RID: 6109 RVA: 0x0007CF78 File Offset: 0x0007B178
	public void Sim1000ms(float dt)
	{
		for (int i = 0; i < this.autoDisinfectables.Count; i++)
		{
			this.autoDisinfectables[i].RefreshChore();
		}
	}

	// Token: 0x04000D37 RID: 3383
	private List<AutoDisinfectable> autoDisinfectables = new List<AutoDisinfectable>();

	// Token: 0x04000D38 RID: 3384
	public static AutoDisinfectableManager Instance;
}
