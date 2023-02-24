using System;
using System.Linq;
using Database;
using UnityEngine;

// Token: 0x02000AA0 RID: 2720
public class FullBodyUIMinionWidget : KMonoBehaviour
{
	// Token: 0x17000630 RID: 1584
	// (get) Token: 0x06005355 RID: 21333 RVA: 0x001E3BC1 File Offset: 0x001E1DC1
	// (set) Token: 0x06005356 RID: 21334 RVA: 0x001E3BC9 File Offset: 0x001E1DC9
	public KBatchedAnimController animController { get; private set; }

	// Token: 0x06005357 RID: 21335 RVA: 0x001E3BD2 File Offset: 0x001E1DD2
	protected override void OnSpawn()
	{
		this.TrySpawnDisplayMinion();
	}

	// Token: 0x06005358 RID: 21336 RVA: 0x001E3BDC File Offset: 0x001E1DDC
	private void TrySpawnDisplayMinion()
	{
		if (this.animController == null)
		{
			this.animController = Util.KInstantiateUI(Assets.GetPrefab(new Tag("FullMinionUIPortrait")), this.duplicantAnimAnchor.gameObject, false).GetComponent<KBatchedAnimController>();
			this.animController.gameObject.SetActive(true);
			this.animController.animScale = 0.38f;
		}
	}

	// Token: 0x06005359 RID: 21337 RVA: 0x001E3C44 File Offset: 0x001E1E44
	private void InitializeAnimator()
	{
		this.TrySpawnDisplayMinion();
		this.animController.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
		Accessorizer component = this.animController.GetComponent<Accessorizer>();
		for (int i = component.GetAccessories().Count - 1; i >= 0; i--)
		{
			component.RemoveAccessory(component.GetAccessories()[i].Get());
		}
	}

	// Token: 0x0600535A RID: 21338 RVA: 0x001E3CB4 File Offset: 0x001E1EB4
	public void SetDefaultPortraitAnimator()
	{
		MinionIdentity minionIdentity = ((Components.MinionIdentities.Count > 0) ? Components.MinionIdentities[0] : null);
		HashedString hashedString = ((minionIdentity != null) ? minionIdentity.personalityResourceId : Db.Get().Personalities.resources.GetRandom<Personality>().Id);
		this.InitializeAnimator();
		this.animController.GetComponent<Accessorizer>().ApplyMinionPersonality(Db.Get().Personalities.Get(hashedString));
		Accessorizer accessorizer = ((minionIdentity != null) ? minionIdentity.GetComponent<Accessorizer>() : null);
		KAnim.Build.Symbol symbol = null;
		KAnim.Build.Symbol symbol2 = null;
		if (accessorizer)
		{
			symbol = accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol;
			symbol2 = Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol;
		}
		this.UpdateHatOverride(null, symbol, symbol2);
		this.UpdateClothingOverride(this.animController.GetComponent<SymbolOverrideController>(), minionIdentity, null);
	}

	// Token: 0x0600535B RID: 21339 RVA: 0x001E3DDC File Offset: 0x001E1FDC
	public void SetPortraitAnimator(IAssignableIdentity assignableIdentity)
	{
		if (assignableIdentity == null || assignableIdentity.IsNull())
		{
			this.SetDefaultPortraitAnimator();
			return;
		}
		this.InitializeAnimator();
		string text = "";
		MinionIdentity minionIdentity;
		StoredMinionIdentity storedMinionIdentity;
		this.GetMinionIdentity(assignableIdentity, out minionIdentity, out storedMinionIdentity);
		Accessorizer accessorizer = null;
		Accessorizer component = this.animController.GetComponent<Accessorizer>();
		KAnim.Build.Symbol symbol = null;
		KAnim.Build.Symbol symbol2 = null;
		if (minionIdentity != null)
		{
			accessorizer = minionIdentity.GetComponent<Accessorizer>();
			foreach (ResourceRef<Accessory> resourceRef in accessorizer.GetAccessories())
			{
				component.AddAccessory(resourceRef.Get());
			}
			text = minionIdentity.GetComponent<MinionResume>().CurrentHat;
			symbol = accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol;
			symbol2 = Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(accessorizer.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol;
		}
		else if (storedMinionIdentity != null)
		{
			foreach (ResourceRef<Accessory> resourceRef2 in storedMinionIdentity.accessories)
			{
				component.AddAccessory(resourceRef2.Get());
			}
			text = storedMinionIdentity.currentHat;
			symbol = storedMinionIdentity.GetAccessory(Db.Get().AccessorySlots.Hair).symbol;
			symbol2 = Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(storedMinionIdentity.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol;
		}
		this.UpdateHatOverride(text, symbol, symbol2);
		this.UpdateClothingOverride(this.animController.GetComponent<SymbolOverrideController>(), minionIdentity, storedMinionIdentity);
	}

	// Token: 0x0600535C RID: 21340 RVA: 0x001E3FEC File Offset: 0x001E21EC
	private void UpdateHatOverride(string current_hat, KAnim.Build.Symbol hair_symbol, KAnim.Build.Symbol hat_hair_symbol)
	{
		AccessorySlot hat = Db.Get().AccessorySlots.Hat;
		this.animController.SetSymbolVisiblity(hat.targetSymbolId, !string.IsNullOrEmpty(current_hat));
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.Hair.targetSymbolId, string.IsNullOrEmpty(current_hat));
		this.animController.SetSymbolVisiblity(Db.Get().AccessorySlots.HatHair.targetSymbolId, !string.IsNullOrEmpty(current_hat));
		SymbolOverrideController component = this.animController.GetComponent<SymbolOverrideController>();
		if (hair_symbol != null)
		{
			component.AddSymbolOverride("snapto_hair_always", hair_symbol, 1);
		}
		if (hat_hair_symbol != null)
		{
			component.AddSymbolOverride(Db.Get().AccessorySlots.HatHair.targetSymbolId, hat_hair_symbol, 1);
		}
	}

	// Token: 0x0600535D RID: 21341 RVA: 0x001E40C4 File Offset: 0x001E22C4
	private void UpdateClothingOverride(SymbolOverrideController symbolOverrideController, MinionIdentity identity, StoredMinionIdentity storedMinionIdentity)
	{
		string[] array = null;
		if (identity != null)
		{
			array = identity.GetComponent<WearableAccessorizer>().GetClothingItemIds();
		}
		else if (storedMinionIdentity != null)
		{
			array = storedMinionIdentity.GetClothingItemIds();
		}
		if (array != null)
		{
			this.animController.GetComponent<WearableAccessorizer>().ApplyClothingItems(array.Select((string i) => Db.Get().Permits.ClothingItems.Get(i)));
		}
	}

	// Token: 0x0600535E RID: 21342 RVA: 0x001E4132 File Offset: 0x001E2332
	public void UpdateEquipment(Equippable equippable, KAnimFile animFile)
	{
		this.animController.GetComponent<WearableAccessorizer>().ApplyEquipment(equippable, animFile);
	}

	// Token: 0x0600535F RID: 21343 RVA: 0x001E4146 File Offset: 0x001E2346
	private void GetMinionIdentity(IAssignableIdentity assignableIdentity, out MinionIdentity minionIdentity, out StoredMinionIdentity storedMinionIdentity)
	{
		if (assignableIdentity is MinionAssignablesProxy)
		{
			minionIdentity = ((MinionAssignablesProxy)assignableIdentity).GetTargetGameObject().GetComponent<MinionIdentity>();
			storedMinionIdentity = ((MinionAssignablesProxy)assignableIdentity).GetTargetGameObject().GetComponent<StoredMinionIdentity>();
			return;
		}
		minionIdentity = assignableIdentity as MinionIdentity;
		storedMinionIdentity = assignableIdentity as StoredMinionIdentity;
	}

	// Token: 0x04003879 RID: 14457
	[SerializeField]
	private GameObject duplicantAnimAnchor;

	// Token: 0x0400387B RID: 14459
	public const float UI_MINION_PORTRAIT_ANIM_SCALE = 0.38f;
}
