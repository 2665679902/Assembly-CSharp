using System;
using KSerialization;
using UnityEngine;

// Token: 0x020009B6 RID: 2486
[AddComponentMenu("KMonoBehaviour/scripts/UserNameable")]
public class UserNameable : KMonoBehaviour
{
	// Token: 0x060049E5 RID: 18917 RVA: 0x0019E3B5 File Offset: 0x0019C5B5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (string.IsNullOrEmpty(this.savedName))
		{
			this.SetName(base.gameObject.GetProperName());
			return;
		}
		this.SetName(this.savedName);
	}

	// Token: 0x060049E6 RID: 18918 RVA: 0x0019E3E8 File Offset: 0x0019C5E8
	public void SetName(string name)
	{
		KSelectable component = base.GetComponent<KSelectable>();
		base.name = name;
		if (component != null)
		{
			component.SetName(name);
		}
		base.gameObject.name = name;
		NameDisplayScreen.Instance.UpdateName(base.gameObject);
		if (base.GetComponent<CommandModule>() != null)
		{
			SpacecraftManager.instance.GetSpacecraftFromLaunchConditionManager(base.GetComponent<LaunchConditionManager>()).SetRocketName(name);
		}
		else if (base.GetComponent<Clustercraft>() != null)
		{
			ClusterNameDisplayScreen.Instance.UpdateName(base.GetComponent<Clustercraft>());
		}
		this.savedName = name;
		base.Trigger(1102426921, name);
	}

	// Token: 0x0400308E RID: 12430
	[Serialize]
	public string savedName = "";
}
