using System;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200068C RID: 1676
[AddComponentMenu("KMonoBehaviour/scripts/ClothingWearer")]
public class ClothingWearer : KMonoBehaviour
{
	// Token: 0x06002D61 RID: 11617 RVA: 0x000EE294 File Offset: 0x000EC494
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.decorProvider = base.GetComponent<DecorProvider>();
		if (this.decorModifier == null)
		{
			this.decorModifier = new AttributeModifier("Decor", 0f, DUPLICANTS.MODIFIERS.CLOTHING.NAME, false, false, false);
		}
		if (this.conductivityModifier == null)
		{
			AttributeInstance attributeInstance = base.gameObject.GetAttributes().Get("ThermalConductivityBarrier");
			this.conductivityModifier = new AttributeModifier("ThermalConductivityBarrier", ClothingWearer.ClothingInfo.BASIC_CLOTHING.conductivityMod, DUPLICANTS.MODIFIERS.CLOTHING.NAME, false, false, false);
			attributeInstance.Add(this.conductivityModifier);
		}
	}

	// Token: 0x06002D62 RID: 11618 RVA: 0x000EE32C File Offset: 0x000EC52C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.decorProvider.decor.Add(this.decorModifier);
		this.decorProvider.decorRadius.Add(new AttributeModifier(Db.Get().BuildingAttributes.DecorRadius.Id, 3f, null, false, false, true));
		Traits component = base.GetComponent<Traits>();
		string text = UI.OVERLAYS.DECOR.CLOTHING;
		if (component != null)
		{
			if (component.HasTrait("DecorUp"))
			{
				text = UI.OVERLAYS.DECOR.CLOTHING_TRAIT_DECORUP;
			}
			else if (component.HasTrait("DecorDown"))
			{
				text = UI.OVERLAYS.DECOR.CLOTHING_TRAIT_DECORDOWN;
			}
		}
		this.decorProvider.overrideName = string.Format(text, base.gameObject.GetProperName());
		if (this.currentClothing == null)
		{
			this.ChangeToDefaultClothes();
			return;
		}
		this.ChangeClothes(this.currentClothing);
	}

	// Token: 0x06002D63 RID: 11619 RVA: 0x000EE40C File Offset: 0x000EC60C
	public void ChangeClothes(ClothingWearer.ClothingInfo clothingInfo)
	{
		this.decorProvider.baseRadius = 3f;
		this.currentClothing = clothingInfo;
		this.conductivityModifier.Description = clothingInfo.name;
		this.conductivityModifier.SetValue(this.currentClothing.conductivityMod);
		this.decorModifier.SetValue((float)this.currentClothing.decorMod);
	}

	// Token: 0x06002D64 RID: 11620 RVA: 0x000EE46E File Offset: 0x000EC66E
	public void ChangeToDefaultClothes()
	{
		this.ChangeClothes(new ClothingWearer.ClothingInfo(ClothingWearer.ClothingInfo.BASIC_CLOTHING.name, ClothingWearer.ClothingInfo.BASIC_CLOTHING.decorMod, ClothingWearer.ClothingInfo.BASIC_CLOTHING.conductivityMod, ClothingWearer.ClothingInfo.BASIC_CLOTHING.homeostasisEfficiencyMultiplier));
	}

	// Token: 0x04001AF4 RID: 6900
	private DecorProvider decorProvider;

	// Token: 0x04001AF5 RID: 6901
	private AttributeModifier decorModifier;

	// Token: 0x04001AF6 RID: 6902
	private AttributeModifier conductivityModifier;

	// Token: 0x04001AF7 RID: 6903
	[Serialize]
	public ClothingWearer.ClothingInfo currentClothing;

	// Token: 0x02001356 RID: 4950
	public class ClothingInfo
	{
		// Token: 0x06007D81 RID: 32129 RVA: 0x002D42FB File Offset: 0x002D24FB
		public ClothingInfo(string _name, int _decor, float _temperature, float _homeostasisEfficiencyMultiplier)
		{
			this.name = _name;
			this.decorMod = _decor;
			this.conductivityMod = _temperature;
			this.homeostasisEfficiencyMultiplier = _homeostasisEfficiencyMultiplier;
		}

		// Token: 0x0400603A RID: 24634
		[Serialize]
		public string name = "";

		// Token: 0x0400603B RID: 24635
		[Serialize]
		public int decorMod;

		// Token: 0x0400603C RID: 24636
		[Serialize]
		public float conductivityMod;

		// Token: 0x0400603D RID: 24637
		[Serialize]
		public float homeostasisEfficiencyMultiplier;

		// Token: 0x0400603E RID: 24638
		public static readonly ClothingWearer.ClothingInfo BASIC_CLOTHING = new ClothingWearer.ClothingInfo(EQUIPMENT.PREFABS.COOL_VEST.GENERICNAME, -5, 0.0025f, -1.25f);

		// Token: 0x0400603F RID: 24639
		public static readonly ClothingWearer.ClothingInfo WARM_CLOTHING = new ClothingWearer.ClothingInfo(EQUIPMENT.PREFABS.WARM_VEST.NAME, -10, 0.01f, -1.25f);

		// Token: 0x04006040 RID: 24640
		public static readonly ClothingWearer.ClothingInfo COOL_CLOTHING = new ClothingWearer.ClothingInfo(EQUIPMENT.PREFABS.COOL_VEST.NAME, -10, 0.0005f, 0f);

		// Token: 0x04006041 RID: 24641
		public static readonly ClothingWearer.ClothingInfo FANCY_CLOTHING = new ClothingWearer.ClothingInfo(EQUIPMENT.PREFABS.FUNKY_VEST.NAME, 30, 0.0025f, -1.25f);

		// Token: 0x04006042 RID: 24642
		public static readonly ClothingWearer.ClothingInfo CUSTOM_CLOTHING = new ClothingWearer.ClothingInfo(EQUIPMENT.PREFABS.CUSTOMCLOTHING.NAME, 40, 0.0025f, -1.25f);

		// Token: 0x04006043 RID: 24643
		public static readonly ClothingWearer.ClothingInfo SLEEP_CLINIC_PAJAMAS = new ClothingWearer.ClothingInfo(EQUIPMENT.PREFABS.CUSTOMCLOTHING.NAME, 40, 0.0025f, -1.25f);
	}
}
