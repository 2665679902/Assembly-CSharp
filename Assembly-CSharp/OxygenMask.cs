using System;
using UnityEngine;

// Token: 0x02000885 RID: 2181
public class OxygenMask : KMonoBehaviour, ISim200ms
{
	// Token: 0x06003E94 RID: 16020 RVA: 0x0015DEFF File Offset: 0x0015C0FF
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<OxygenMask>(608245985, OxygenMask.OnSuitTankDeltaDelegate);
	}

	// Token: 0x06003E95 RID: 16021 RVA: 0x0015DF18 File Offset: 0x0015C118
	private void CheckOxygenLevels(object data)
	{
		if (this.suitTank.IsEmpty())
		{
			Equippable component = base.GetComponent<Equippable>();
			if (component.assignee != null)
			{
				Ownables soleOwner = component.assignee.GetSoleOwner();
				if (soleOwner != null)
				{
					soleOwner.GetComponent<Equipment>().Unequip(component);
				}
			}
		}
	}

	// Token: 0x06003E96 RID: 16022 RVA: 0x0015DF64 File Offset: 0x0015C164
	public void Sim200ms(float dt)
	{
		if (base.GetComponent<Equippable>().assignee == null)
		{
			float num = this.leakRate * dt;
			float massAvailable = this.storage.GetMassAvailable(this.suitTank.elementTag);
			num = Mathf.Min(num, massAvailable);
			this.storage.DropSome(this.suitTank.elementTag, num, true, true, default(Vector3), true, false);
		}
		if (this.suitTank.IsEmpty())
		{
			Util.KDestroyGameObject(base.gameObject);
		}
	}

	// Token: 0x040028F3 RID: 10483
	private static readonly EventSystem.IntraObjectHandler<OxygenMask> OnSuitTankDeltaDelegate = new EventSystem.IntraObjectHandler<OxygenMask>(delegate(OxygenMask component, object data)
	{
		component.CheckOxygenLevels(data);
	});

	// Token: 0x040028F4 RID: 10484
	[MyCmpGet]
	private SuitTank suitTank;

	// Token: 0x040028F5 RID: 10485
	[MyCmpGet]
	private Storage storage;

	// Token: 0x040028F6 RID: 10486
	private float leakRate = 0.1f;
}
