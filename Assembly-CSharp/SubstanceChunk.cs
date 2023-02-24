using System;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200099D RID: 2461
[SkipSaveFileSerialization]
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/SubstanceChunk")]
public class SubstanceChunk : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x0600490A RID: 18698 RVA: 0x00199670 File Offset: 0x00197870
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Color color = base.GetComponent<PrimaryElement>().Element.substance.colour;
		color.a = 1f;
		base.GetComponent<KBatchedAnimController>().SetSymbolTint(SubstanceChunk.symbolToTint, color);
	}

	// Token: 0x0600490B RID: 18699 RVA: 0x001996BC File Offset: 0x001978BC
	private void OnRefreshUserMenu(object data)
	{
		Game.Instance.userMenu.AddButton(base.gameObject, new KIconButtonMenu.ButtonInfo("action_deconstruct", UI.USERMENUACTIONS.RELEASEELEMENT.NAME, new System.Action(this.OnRelease), global::Action.NumActions, null, null, null, UI.USERMENUACTIONS.RELEASEELEMENT.TOOLTIP, true), 1f);
	}

	// Token: 0x0600490C RID: 18700 RVA: 0x00199718 File Offset: 0x00197918
	private void OnRelease()
	{
		int num = Grid.PosToCell(base.transform.GetPosition());
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		if (component.Mass > 0f)
		{
			SimMessages.AddRemoveSubstance(num, component.ElementID, CellEventLogger.Instance.ExhaustSimUpdate, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, true, -1);
		}
		base.gameObject.DeleteObject();
	}

	// Token: 0x04003006 RID: 12294
	private static readonly KAnimHashedString symbolToTint = new KAnimHashedString("substance_tinter");
}
