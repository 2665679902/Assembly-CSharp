using System;
using STRINGS;
using UnityEngine;

// Token: 0x020009A0 RID: 2464
[AddComponentMenu("KMonoBehaviour/scripts/SuitEquipper")]
public class SuitEquipper : KMonoBehaviour
{
	// Token: 0x0600491E RID: 18718 RVA: 0x001999C5 File Offset: 0x00197BC5
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.Subscribe<SuitEquipper>(493375141, SuitEquipper.OnRefreshUserMenuDelegate);
	}

	// Token: 0x0600491F RID: 18719 RVA: 0x001999E0 File Offset: 0x00197BE0
	private void OnRefreshUserMenu(object data)
	{
		foreach (AssignableSlotInstance assignableSlotInstance in base.GetComponent<MinionIdentity>().GetEquipment().Slots)
		{
			EquipmentSlotInstance equipmentSlotInstance = (EquipmentSlotInstance)assignableSlotInstance;
			Equippable equippable = equipmentSlotInstance.assignable as Equippable;
			if (equippable && equippable.unequippable)
			{
				string text = string.Format(UI.USERMENUACTIONS.UNEQUIP.NAME, equippable.def.GenericName);
				Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("iconDown", text, delegate
				{
					equippable.Unassign();
				}, global::Action.NumActions, null, null, null, "", true), 2f);
			}
		}
	}

	// Token: 0x06004920 RID: 18720 RVA: 0x00199AD4 File Offset: 0x00197CD4
	public Equippable IsWearingAirtightSuit()
	{
		Equippable equippable = null;
		foreach (AssignableSlotInstance assignableSlotInstance in base.GetComponent<MinionIdentity>().GetEquipment().Slots)
		{
			Equippable equippable2 = ((EquipmentSlotInstance)assignableSlotInstance).assignable as Equippable;
			if (equippable2 && equippable2.GetComponent<KPrefabID>().HasTag(GameTags.AirtightSuit) && equippable2.isEquipped)
			{
				equippable = equippable2;
				break;
			}
		}
		return equippable;
	}

	// Token: 0x0400300C RID: 12300
	private static readonly EventSystem.IntraObjectHandler<SuitEquipper> OnRefreshUserMenuDelegate = new EventSystem.IntraObjectHandler<SuitEquipper>(delegate(SuitEquipper component, object data)
	{
		component.OnRefreshUserMenu(data);
	});
}
