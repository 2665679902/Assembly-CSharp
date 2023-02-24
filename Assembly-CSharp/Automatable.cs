using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000451 RID: 1105
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Automatable")]
public class Automatable : KMonoBehaviour
{
	// Token: 0x060017E1 RID: 6113 RVA: 0x0007D101 File Offset: 0x0007B301
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<Automatable>(-905833192, Automatable.OnCopySettingsDelegate);
	}

	// Token: 0x060017E2 RID: 6114 RVA: 0x0007D11C File Offset: 0x0007B31C
	private void OnCopySettings(object data)
	{
		Automatable component = ((GameObject)data).GetComponent<Automatable>();
		if (component != null)
		{
			this.automationOnly = component.automationOnly;
		}
	}

	// Token: 0x060017E3 RID: 6115 RVA: 0x0007D14A File Offset: 0x0007B34A
	public bool GetAutomationOnly()
	{
		return this.automationOnly;
	}

	// Token: 0x060017E4 RID: 6116 RVA: 0x0007D152 File Offset: 0x0007B352
	public void SetAutomationOnly(bool only)
	{
		this.automationOnly = only;
	}

	// Token: 0x060017E5 RID: 6117 RVA: 0x0007D15B File Offset: 0x0007B35B
	public bool AllowedByAutomation(bool is_transfer_arm)
	{
		return !this.GetAutomationOnly() || is_transfer_arm;
	}

	// Token: 0x04000D3F RID: 3391
	[Serialize]
	private bool automationOnly = true;

	// Token: 0x04000D40 RID: 3392
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04000D41 RID: 3393
	private static readonly EventSystem.IntraObjectHandler<Automatable> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<Automatable>(delegate(Automatable component, object data)
	{
		component.OnCopySettings(data);
	});
}
