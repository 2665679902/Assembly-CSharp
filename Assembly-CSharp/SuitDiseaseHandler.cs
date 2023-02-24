using System;
using UnityEngine;

// Token: 0x0200099F RID: 2463
[AddComponentMenu("KMonoBehaviour/scripts/SuitDiseaseHandler")]
public class SuitDiseaseHandler : KMonoBehaviour
{
	// Token: 0x06004916 RID: 18710 RVA: 0x001998AD File Offset: 0x00197AAD
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<SuitDiseaseHandler>(-1617557748, SuitDiseaseHandler.OnEquippedDelegate);
		base.Subscribe<SuitDiseaseHandler>(-170173755, SuitDiseaseHandler.OnUnequippedDelegate);
	}

	// Token: 0x06004917 RID: 18711 RVA: 0x001998D8 File Offset: 0x00197AD8
	private PrimaryElement GetPrimaryElement(object data)
	{
		GameObject targetGameObject = ((Equipment)data).GetComponent<MinionAssignablesProxy>().GetTargetGameObject();
		if (targetGameObject)
		{
			return targetGameObject.GetComponent<PrimaryElement>();
		}
		return null;
	}

	// Token: 0x06004918 RID: 18712 RVA: 0x00199908 File Offset: 0x00197B08
	private void OnEquipped(object data)
	{
		PrimaryElement primaryElement = this.GetPrimaryElement(data);
		if (primaryElement != null)
		{
			primaryElement.ForcePermanentDiseaseContainer(true);
			primaryElement.RedirectDisease(base.gameObject);
		}
	}

	// Token: 0x06004919 RID: 18713 RVA: 0x0019993C File Offset: 0x00197B3C
	private void OnUnequipped(object data)
	{
		PrimaryElement primaryElement = this.GetPrimaryElement(data);
		if (primaryElement != null)
		{
			primaryElement.ForcePermanentDiseaseContainer(false);
			primaryElement.RedirectDisease(null);
		}
	}

	// Token: 0x0600491A RID: 18714 RVA: 0x00199968 File Offset: 0x00197B68
	private void OnModifyDiseaseCount(int delta, string reason)
	{
		base.GetComponent<PrimaryElement>().ModifyDiseaseCount(delta, reason);
	}

	// Token: 0x0600491B RID: 18715 RVA: 0x00199977 File Offset: 0x00197B77
	private void OnAddDisease(byte disease_idx, int delta, string reason)
	{
		base.GetComponent<PrimaryElement>().AddDisease(disease_idx, delta, reason);
	}

	// Token: 0x0400300A RID: 12298
	private static readonly EventSystem.IntraObjectHandler<SuitDiseaseHandler> OnEquippedDelegate = new EventSystem.IntraObjectHandler<SuitDiseaseHandler>(delegate(SuitDiseaseHandler component, object data)
	{
		component.OnEquipped(data);
	});

	// Token: 0x0400300B RID: 12299
	private static readonly EventSystem.IntraObjectHandler<SuitDiseaseHandler> OnUnequippedDelegate = new EventSystem.IntraObjectHandler<SuitDiseaseHandler>(delegate(SuitDiseaseHandler component, object data)
	{
		component.OnUnequipped(data);
	});
}
