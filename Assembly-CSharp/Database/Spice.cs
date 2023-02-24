using System;
using Klei.AI;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CB3 RID: 3251
	public class Spice : Resource
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x060065EC RID: 26092 RVA: 0x00271E6F File Offset: 0x0027006F
		// (set) Token: 0x060065ED RID: 26093 RVA: 0x00271E77 File Offset: 0x00270077
		public AttributeModifier StatBonus { get; private set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x060065EE RID: 26094 RVA: 0x00271E80 File Offset: 0x00270080
		// (set) Token: 0x060065EF RID: 26095 RVA: 0x00271E88 File Offset: 0x00270088
		public AttributeModifier FoodModifier { get; private set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x060065F0 RID: 26096 RVA: 0x00271E91 File Offset: 0x00270091
		// (set) Token: 0x060065F1 RID: 26097 RVA: 0x00271E99 File Offset: 0x00270099
		public AttributeModifier CalorieModifier { get; private set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060065F2 RID: 26098 RVA: 0x00271EA2 File Offset: 0x002700A2
		// (set) Token: 0x060065F3 RID: 26099 RVA: 0x00271EAA File Offset: 0x002700AA
		public Color PrimaryColor { get; private set; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x060065F4 RID: 26100 RVA: 0x00271EB3 File Offset: 0x002700B3
		// (set) Token: 0x060065F5 RID: 26101 RVA: 0x00271EBB File Offset: 0x002700BB
		public Color SecondaryColor { get; private set; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x060065F7 RID: 26103 RVA: 0x00271ECD File Offset: 0x002700CD
		// (set) Token: 0x060065F6 RID: 26102 RVA: 0x00271EC4 File Offset: 0x002700C4
		public string Image { get; private set; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060065F9 RID: 26105 RVA: 0x00271EDE File Offset: 0x002700DE
		// (set) Token: 0x060065F8 RID: 26104 RVA: 0x00271ED5 File Offset: 0x002700D5
		public string[] DlcIds { get; private set; } = DlcManager.AVAILABLE_ALL_VERSIONS;

		// Token: 0x060065FA RID: 26106 RVA: 0x00271EE8 File Offset: 0x002700E8
		public Spice(ResourceSet parent, string id, Spice.Ingredient[] ingredients, Color primaryColor, Color secondaryColor, AttributeModifier foodMod = null, AttributeModifier statBonus = null, string imageName = "unknown", string[] dlcID = null)
			: base(id, parent, null)
		{
			if (dlcID != null)
			{
				this.DlcIds = dlcID;
			}
			this.StatBonus = statBonus;
			this.FoodModifier = foodMod;
			this.Ingredients = ingredients;
			this.Image = imageName;
			this.PrimaryColor = primaryColor;
			this.SecondaryColor = secondaryColor;
			for (int i = 0; i < this.Ingredients.Length; i++)
			{
				this.TotalKG += this.Ingredients[i].AmountKG;
			}
		}

		// Token: 0x04004A45 RID: 19013
		public readonly Spice.Ingredient[] Ingredients;

		// Token: 0x04004A46 RID: 19014
		public readonly float TotalKG;

		// Token: 0x02001B31 RID: 6961
		public class Ingredient : IConfigurableConsumerIngredient
		{
			// Token: 0x060095C9 RID: 38345 RVA: 0x00321D0B File Offset: 0x0031FF0B
			public float GetAmount()
			{
				return this.AmountKG;
			}

			// Token: 0x060095CA RID: 38346 RVA: 0x00321D13 File Offset: 0x0031FF13
			public Tag[] GetIDSets()
			{
				return this.IngredientSet;
			}

			// Token: 0x04007AD6 RID: 31446
			public Tag[] IngredientSet;

			// Token: 0x04007AD7 RID: 31447
			public float AmountKG;
		}
	}
}
