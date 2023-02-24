using System;
using Database;
using UnityEngine;

// Token: 0x02000AD2 RID: 2770
public class KleiItemDropScreen_PermitVis_DupeEquipment : KMonoBehaviour, IKleiItemDropScreen_PermitVis_Target
{
	// Token: 0x060054FD RID: 21757 RVA: 0x001ECC30 File Offset: 0x001EAE30
	public void ConfigureWith(PermitResource permit, PermitPresentationInfo permitPresInfo)
	{
		this.dupeKAnim.GetComponent<UIDupeRandomizer>().Randomize();
		KAnimFile anim = Assets.GetAnim((permit as EquippableFacadeResource).BuildOverride);
		this.dupeKAnim.AddAnimOverrides(anim, 0f);
		KAnimHashedString kanimHashedString = new KAnimHashedString("snapto_neck");
		KAnim.Build.Symbol symbol = anim.GetData().build.GetSymbol(kanimHashedString);
		if (symbol != null)
		{
			this.dupeKAnim.GetComponent<SymbolOverrideController>().AddSymbolOverride(kanimHashedString, symbol, 6);
			this.dupeKAnim.SetSymbolVisiblity(kanimHashedString, true);
		}
		else
		{
			this.dupeKAnim.GetComponent<SymbolOverrideController>().RemoveSymbolOverride(kanimHashedString, 6);
			this.dupeKAnim.SetSymbolVisiblity(kanimHashedString, false);
		}
		this.dupeKAnim.Play("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
		this.dupeKAnim.Queue("cheer_pre", KAnim.PlayMode.Once, 1f, 0f);
		this.dupeKAnim.Queue("cheer_loop", KAnim.PlayMode.Once, 1f, 0f);
		this.dupeKAnim.Queue("cheer_pst", KAnim.PlayMode.Once, 1f, 0f);
		this.dupeKAnim.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x040039C0 RID: 14784
	[SerializeField]
	private KBatchedAnimController droppedItemKAnim;

	// Token: 0x040039C1 RID: 14785
	[SerializeField]
	private KBatchedAnimController dupeKAnim;
}
