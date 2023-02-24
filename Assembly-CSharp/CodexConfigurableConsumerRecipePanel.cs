using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A6D RID: 2669
public class CodexConfigurableConsumerRecipePanel : CodexWidget<CodexConfigurableConsumerRecipePanel>
{
	// Token: 0x060051A1 RID: 20897 RVA: 0x001D7515 File Offset: 0x001D5715
	public CodexConfigurableConsumerRecipePanel(IConfigurableConsumerOption data)
	{
		this.data = data;
	}

	// Token: 0x060051A2 RID: 20898 RVA: 0x001D7524 File Offset: 0x001D5724
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		HierarchyReferences component = contentGameObject.GetComponent<HierarchyReferences>();
		this.title = component.GetReference<LocText>("Title");
		this.result_description = component.GetReference<LocText>("ResultDescription");
		this.resultIcon = component.GetReference<Image>("ResultIcon");
		this.ingredient_original = component.GetReference<RectTransform>("IngredientPrefab").gameObject;
		this.ingredient_original.SetActive(false);
		CodexText codexText = new CodexText();
		LocText reference = this.ingredient_original.GetComponent<HierarchyReferences>().GetReference<LocText>("Name");
		codexText.ConfigureLabel(reference, textStyles);
		this.Clear();
		if (this.data != null)
		{
			this.title.text = this.data.GetName();
			this.result_description.text = this.data.GetDescription();
			this.result_description.color = Color.black;
			this.resultIcon.sprite = this.data.GetIcon();
			IConfigurableConsumerIngredient[] ingredients = this.data.GetIngredients();
			this._ingredientRows = new GameObject[ingredients.Length];
			for (int i = 0; i < this._ingredientRows.Length; i++)
			{
				this._ingredientRows[i] = this.CreateIngredientRow(ingredients[i]);
			}
		}
	}

	// Token: 0x060051A3 RID: 20899 RVA: 0x001D7650 File Offset: 0x001D5850
	public GameObject CreateIngredientRow(IConfigurableConsumerIngredient ingredient)
	{
		Tag[] idsets = ingredient.GetIDSets();
		if (this.ingredient_original != null && idsets.Length != 0)
		{
			GameObject gameObject = Util.KInstantiateUI(this.ingredient_original, this.ingredient_original.transform.parent.gameObject, true);
			HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
			global::Tuple<Sprite, Color> uisprite = Def.GetUISprite(idsets[0], "ui", false);
			component.GetReference<Image>("Icon").sprite = uisprite.first;
			component.GetReference<Image>("Icon").color = uisprite.second;
			component.GetReference<LocText>("Name").text = idsets[0].ProperName();
			component.GetReference<LocText>("Amount").text = GameUtil.GetFormattedByTag(idsets[0], ingredient.GetAmount(), GameUtil.TimeSlice.None);
			component.GetReference<LocText>("Amount").color = Color.black;
			return gameObject;
		}
		return null;
	}

	// Token: 0x060051A4 RID: 20900 RVA: 0x001D773C File Offset: 0x001D593C
	public void Clear()
	{
		if (this._ingredientRows != null)
		{
			for (int i = 0; i < this._ingredientRows.Length; i++)
			{
				UnityEngine.Object.Destroy(this._ingredientRows[i]);
			}
			this._ingredientRows = null;
		}
	}

	// Token: 0x040036B0 RID: 14000
	private LocText title;

	// Token: 0x040036B1 RID: 14001
	private LocText result_description;

	// Token: 0x040036B2 RID: 14002
	private Image resultIcon;

	// Token: 0x040036B3 RID: 14003
	private GameObject ingredient_original;

	// Token: 0x040036B4 RID: 14004
	private IConfigurableConsumerOption data;

	// Token: 0x040036B5 RID: 14005
	private GameObject[] _ingredientRows;
}
