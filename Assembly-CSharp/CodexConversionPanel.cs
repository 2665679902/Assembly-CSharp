using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A70 RID: 2672
public class CodexConversionPanel : CodexWidget<CodexConversionPanel>
{
	// Token: 0x060051AF RID: 20911 RVA: 0x001D8010 File Offset: 0x001D6210
	public CodexConversionPanel(string title, Tag ctag, float inputAmount, bool inputContinuous, Tag ptag, float outputAmount, bool outputContinuous, GameObject converter)
	{
		this.title = title;
		this.ins = new ElementUsage[]
		{
			new ElementUsage(ctag, inputAmount, inputContinuous)
		};
		this.outs = new ElementUsage[]
		{
			new ElementUsage(ptag, outputAmount, outputContinuous)
		};
		this.Converter = converter;
	}

	// Token: 0x060051B0 RID: 20912 RVA: 0x001D8064 File Offset: 0x001D6264
	public CodexConversionPanel(string title, ElementUsage[] ins, ElementUsage[] outs, GameObject converter)
	{
		this.title = title;
		this.ins = ((ins != null) ? ins : new ElementUsage[0]);
		this.outs = ((outs != null) ? outs : new ElementUsage[0]);
		this.Converter = converter;
	}

	// Token: 0x060051B1 RID: 20913 RVA: 0x001D80A0 File Offset: 0x001D62A0
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		HierarchyReferences component = contentGameObject.GetComponent<HierarchyReferences>();
		this.label = component.GetReference<LocText>("Title");
		this.materialPrefab = component.GetReference<RectTransform>("MaterialPrefab").gameObject;
		this.fabricatorPrefab = component.GetReference<RectTransform>("FabricatorPrefab").gameObject;
		this.ingredientsContainer = component.GetReference<RectTransform>("IngredientsContainer").gameObject;
		this.resultsContainer = component.GetReference<RectTransform>("ResultsContainer").gameObject;
		this.fabricatorContainer = component.GetReference<RectTransform>("FabricatorContainer").gameObject;
		this.arrow1 = component.GetReference<RectTransform>("Arrow1").gameObject;
		this.arrow2 = component.GetReference<RectTransform>("Arrow2").gameObject;
		this.ClearPanel();
		this.ConfigureConversion();
	}

	// Token: 0x060051B2 RID: 20914 RVA: 0x001D816C File Offset: 0x001D636C
	private global::Tuple<Sprite, Color> GetUISprite(Tag tag)
	{
		if (ElementLoader.GetElement(tag) != null)
		{
			return Def.GetUISprite(ElementLoader.GetElement(tag), "ui", false);
		}
		if (Assets.GetPrefab(tag) != null)
		{
			return Def.GetUISprite(Assets.GetPrefab(tag), "ui", false);
		}
		if (Assets.GetSprite(tag.Name) != null)
		{
			return new global::Tuple<Sprite, Color>(Assets.GetSprite(tag.Name), Color.white);
		}
		return null;
	}

	// Token: 0x060051B3 RID: 20915 RVA: 0x001D81EC File Offset: 0x001D63EC
	private void ConfigureConversion()
	{
		this.label.text = this.title;
		bool flag = false;
		ElementUsage[] array = this.ins;
		for (int i = 0; i < array.Length; i++)
		{
			ElementUsage elementUsage = array[i];
			Tag tag2 = elementUsage.tag;
			if (!(tag2 == Tag.Invalid))
			{
				float amount = elementUsage.amount;
				flag = true;
				HierarchyReferences component = Util.KInstantiateUI(this.materialPrefab, this.ingredientsContainer, true).GetComponent<HierarchyReferences>();
				global::Tuple<Sprite, Color> uisprite = this.GetUISprite(tag2);
				if (uisprite != null)
				{
					component.GetReference<Image>("Icon").sprite = uisprite.first;
					component.GetReference<Image>("Icon").color = uisprite.second;
				}
				GameUtil.TimeSlice timeSlice = (elementUsage.continuous ? GameUtil.TimeSlice.PerCycle : GameUtil.TimeSlice.None);
				component.GetReference<LocText>("Amount").text = GameUtil.GetFormattedByTag(tag2, amount, timeSlice);
				component.GetReference<LocText>("Amount").color = Color.black;
				string text = tag2.ProperName();
				GameObject prefab = Assets.GetPrefab(tag2);
				if (prefab && prefab.GetComponent<Edible>() != null)
				{
					text = text + "\n    • " + string.Format(UI.GAMEOBJECTEFFECTS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(prefab.GetComponent<Edible>().GetQuality()));
				}
				component.GetReference<ToolTip>("Tooltip").toolTip = text;
				component.GetReference<KButton>("Button").onClick += delegate
				{
					ManagementMenu.Instance.codexScreen.ChangeArticle(UI.ExtractLinkID(tag2.ProperName()), false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
				};
			}
		}
		this.arrow1.SetActive(flag);
		string name = this.Converter.PrefabID().Name;
		HierarchyReferences component2 = Util.KInstantiateUI(this.fabricatorPrefab, this.fabricatorContainer, true).GetComponent<HierarchyReferences>();
		global::Tuple<Sprite, Color> uisprite2 = Def.GetUISprite(name, "ui", false);
		component2.GetReference<Image>("Icon").sprite = uisprite2.first;
		component2.GetReference<Image>("Icon").color = uisprite2.second;
		component2.GetReference<ToolTip>("Tooltip").toolTip = this.Converter.GetProperName();
		component2.GetReference<KButton>("Button").onClick += delegate
		{
			ManagementMenu.Instance.codexScreen.ChangeArticle(UI.ExtractLinkID(this.Converter.GetProperName()), false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
		};
		bool flag2 = false;
		array = this.outs;
		for (int i = 0; i < array.Length; i++)
		{
			ElementUsage elementUsage2 = array[i];
			Tag tag = elementUsage2.tag;
			if (!(tag == Tag.Invalid))
			{
				float amount2 = elementUsage2.amount;
				flag2 = true;
				HierarchyReferences component3 = Util.KInstantiateUI(this.materialPrefab, this.resultsContainer, true).GetComponent<HierarchyReferences>();
				global::Tuple<Sprite, Color> uisprite3 = this.GetUISprite(tag);
				if (uisprite3 != null)
				{
					component3.GetReference<Image>("Icon").sprite = uisprite3.first;
					component3.GetReference<Image>("Icon").color = uisprite3.second;
				}
				GameUtil.TimeSlice timeSlice2 = (elementUsage2.continuous ? GameUtil.TimeSlice.PerCycle : GameUtil.TimeSlice.None);
				component3.GetReference<LocText>("Amount").text = GameUtil.GetFormattedByTag(tag, amount2, timeSlice2);
				component3.GetReference<LocText>("Amount").color = Color.black;
				string text2 = tag.ProperName();
				GameObject prefab2 = Assets.GetPrefab(tag);
				if (prefab2 && prefab2.GetComponent<Edible>() != null)
				{
					text2 = text2 + "\n    • " + string.Format(UI.GAMEOBJECTEFFECTS.FOOD_QUALITY, GameUtil.GetFormattedFoodQuality(prefab2.GetComponent<Edible>().GetQuality()));
				}
				component3.GetReference<ToolTip>("Tooltip").toolTip = text2;
				component3.GetReference<KButton>("Button").onClick += delegate
				{
					ManagementMenu.Instance.codexScreen.ChangeArticle(UI.ExtractLinkID(tag.ProperName()), false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
				};
			}
		}
		this.arrow2.SetActive(flag2);
	}

	// Token: 0x060051B4 RID: 20916 RVA: 0x001D85E8 File Offset: 0x001D67E8
	private void ClearPanel()
	{
		foreach (object obj in this.ingredientsContainer.transform)
		{
			UnityEngine.Object.Destroy(((Transform)obj).gameObject);
		}
		foreach (object obj2 in this.resultsContainer.transform)
		{
			UnityEngine.Object.Destroy(((Transform)obj2).gameObject);
		}
		foreach (object obj3 in this.fabricatorContainer.transform)
		{
			UnityEngine.Object.Destroy(((Transform)obj3).gameObject);
		}
	}

	// Token: 0x040036C2 RID: 14018
	private LocText label;

	// Token: 0x040036C3 RID: 14019
	private GameObject materialPrefab;

	// Token: 0x040036C4 RID: 14020
	private GameObject fabricatorPrefab;

	// Token: 0x040036C5 RID: 14021
	private GameObject ingredientsContainer;

	// Token: 0x040036C6 RID: 14022
	private GameObject resultsContainer;

	// Token: 0x040036C7 RID: 14023
	private GameObject fabricatorContainer;

	// Token: 0x040036C8 RID: 14024
	private GameObject arrow1;

	// Token: 0x040036C9 RID: 14025
	private GameObject arrow2;

	// Token: 0x040036CA RID: 14026
	private string title;

	// Token: 0x040036CB RID: 14027
	private ElementUsage[] ins;

	// Token: 0x040036CC RID: 14028
	private ElementUsage[] outs;

	// Token: 0x040036CD RID: 14029
	private GameObject Converter;
}
