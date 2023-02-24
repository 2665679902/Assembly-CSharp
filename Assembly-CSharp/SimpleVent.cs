using System;
using UnityEngine;

// Token: 0x020004CC RID: 1228
[AddComponentMenu("KMonoBehaviour/scripts/SimpleVent")]
public class SimpleVent : KMonoBehaviour
{
	// Token: 0x06001C78 RID: 7288 RVA: 0x00097C09 File Offset: 0x00095E09
	protected override void OnPrefabInit()
	{
		base.Subscribe<SimpleVent>(-592767678, SimpleVent.OnChangedDelegate);
		base.Subscribe<SimpleVent>(-111137758, SimpleVent.OnChangedDelegate);
	}

	// Token: 0x06001C79 RID: 7289 RVA: 0x00097C2D File Offset: 0x00095E2D
	protected override void OnSpawn()
	{
		this.OnChanged(null);
	}

	// Token: 0x06001C7A RID: 7290 RVA: 0x00097C38 File Offset: 0x00095E38
	private void OnChanged(object data)
	{
		if (this.operational.IsFunctional)
		{
			base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, Db.Get().BuildingStatusItems.Normal, this);
			return;
		}
		base.GetComponent<KSelectable>().SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
	}

	// Token: 0x04001006 RID: 4102
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04001007 RID: 4103
	private static readonly EventSystem.IntraObjectHandler<SimpleVent> OnChangedDelegate = new EventSystem.IntraObjectHandler<SimpleVent>(delegate(SimpleVent component, object data)
	{
		component.OnChanged(data);
	});
}
