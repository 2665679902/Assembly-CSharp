using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000B6F RID: 2927
public class ResourceCategoryScreen : KScreen
{
	// Token: 0x06005BA3 RID: 23459 RVA: 0x0021649C File Offset: 0x0021469C
	public static void DestroyInstance()
	{
		ResourceCategoryScreen.Instance = null;
	}

	// Token: 0x06005BA4 RID: 23460 RVA: 0x002164A4 File Offset: 0x002146A4
	protected override void OnActivate()
	{
		base.OnActivate();
		ResourceCategoryScreen.Instance = this;
		base.ConsumeMouseScroll = true;
		MultiToggle hiderButton = this.HiderButton;
		hiderButton.onClick = (System.Action)Delegate.Combine(hiderButton.onClick, new System.Action(this.OnHiderClick));
		this.OnHiderClick();
		this.CreateTagSetHeaders(GameTags.MaterialCategories, GameUtil.MeasureUnit.mass);
		this.CreateTagSetHeaders(GameTags.CalorieCategories, GameUtil.MeasureUnit.kcal);
		this.CreateTagSetHeaders(GameTags.UnitCategories, GameUtil.MeasureUnit.quantity);
		if (!this.DisplayedCategories.ContainsKey(GameTags.Miscellaneous))
		{
			ResourceCategoryHeader resourceCategoryHeader = this.NewCategoryHeader(GameTags.Miscellaneous, GameUtil.MeasureUnit.mass);
			this.DisplayedCategories.Add(GameTags.Miscellaneous, resourceCategoryHeader);
		}
		this.DisplayedCategoryKeys = this.DisplayedCategories.Keys.ToArray<Tag>();
	}

	// Token: 0x06005BA5 RID: 23461 RVA: 0x0021655C File Offset: 0x0021475C
	private void CreateTagSetHeaders(IEnumerable<Tag> set, GameUtil.MeasureUnit measure)
	{
		foreach (Tag tag in set)
		{
			ResourceCategoryHeader resourceCategoryHeader = this.NewCategoryHeader(tag, measure);
			this.DisplayedCategories.Add(tag, resourceCategoryHeader);
		}
	}

	// Token: 0x06005BA6 RID: 23462 RVA: 0x002165B4 File Offset: 0x002147B4
	private void OnHiderClick()
	{
		this.HiderButton.NextState();
		if (this.HiderButton.CurrentState == 0)
		{
			this.targetContentHideHeight = 0f;
			return;
		}
		this.targetContentHideHeight = Mathf.Min(((float)Screen.height - this.maxHeightPadding) / GameScreenManager.Instance.ssOverlayCanvas.GetComponent<KCanvasScaler>().GetCanvasScale(), this.CategoryContainer.rectTransform().rect.height);
	}

	// Token: 0x06005BA7 RID: 23463 RVA: 0x0021662C File Offset: 0x0021482C
	private void Update()
	{
		if (ClusterManager.Instance.activeWorld.worldInventory == null)
		{
			return;
		}
		if (this.HideTarget.minHeight != this.targetContentHideHeight)
		{
			float num = this.HideTarget.minHeight;
			float num2 = this.targetContentHideHeight - num;
			num2 = Mathf.Clamp(num2 * this.HideSpeedFactor * Time.unscaledDeltaTime, (num2 > 0f) ? (-num2) : num2, (num2 > 0f) ? num2 : (-num2));
			num += num2;
			this.HideTarget.minHeight = num;
		}
		for (int i = 0; i < 1; i++)
		{
			Tag tag = this.DisplayedCategoryKeys[this.categoryUpdatePacer];
			ResourceCategoryHeader resourceCategoryHeader = this.DisplayedCategories[tag];
			if (DiscoveredResources.Instance.IsDiscovered(tag) && !resourceCategoryHeader.gameObject.activeInHierarchy)
			{
				resourceCategoryHeader.gameObject.SetActive(true);
			}
			resourceCategoryHeader.UpdateContents();
			this.categoryUpdatePacer = (this.categoryUpdatePacer + 1) % this.DisplayedCategoryKeys.Length;
		}
		if (this.HiderButton.CurrentState != 0)
		{
			this.targetContentHideHeight = Mathf.Min(((float)Screen.height - this.maxHeightPadding) / GameScreenManager.Instance.ssOverlayCanvas.GetComponent<KCanvasScaler>().GetCanvasScale(), this.CategoryContainer.rectTransform().rect.height);
		}
		if (MeterScreen.Instance != null && !MeterScreen.Instance.StartValuesSet)
		{
			MeterScreen.Instance.InitializeValues();
		}
	}

	// Token: 0x06005BA8 RID: 23464 RVA: 0x0021679F File Offset: 0x0021499F
	private ResourceCategoryHeader NewCategoryHeader(Tag categoryTag, GameUtil.MeasureUnit measure)
	{
		GameObject gameObject = Util.KInstantiateUI(this.Prefab_CategoryBar, this.CategoryContainer.gameObject, false);
		gameObject.name = "CategoryHeader_" + categoryTag.Name;
		ResourceCategoryHeader component = gameObject.GetComponent<ResourceCategoryHeader>();
		component.SetTag(categoryTag, measure);
		return component;
	}

	// Token: 0x06005BA9 RID: 23465 RVA: 0x002167DC File Offset: 0x002149DC
	public static string QuantityTextForMeasure(float quantity, GameUtil.MeasureUnit measure)
	{
		switch (measure)
		{
		case GameUtil.MeasureUnit.mass:
			return GameUtil.GetFormattedMass(quantity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
		case GameUtil.MeasureUnit.kcal:
			return GameUtil.GetFormattedCalories(quantity, GameUtil.TimeSlice.None, true);
		}
		return quantity.ToString();
	}

	// Token: 0x04003E74 RID: 15988
	public static ResourceCategoryScreen Instance;

	// Token: 0x04003E75 RID: 15989
	public GameObject Prefab_CategoryBar;

	// Token: 0x04003E76 RID: 15990
	public Transform CategoryContainer;

	// Token: 0x04003E77 RID: 15991
	public MultiToggle HiderButton;

	// Token: 0x04003E78 RID: 15992
	public KLayoutElement HideTarget;

	// Token: 0x04003E79 RID: 15993
	private float HideSpeedFactor = 12f;

	// Token: 0x04003E7A RID: 15994
	private float maxHeightPadding = 480f;

	// Token: 0x04003E7B RID: 15995
	private float targetContentHideHeight;

	// Token: 0x04003E7C RID: 15996
	public Dictionary<Tag, ResourceCategoryHeader> DisplayedCategories = new Dictionary<Tag, ResourceCategoryHeader>();

	// Token: 0x04003E7D RID: 15997
	private Tag[] DisplayedCategoryKeys;

	// Token: 0x04003E7E RID: 15998
	private int categoryUpdatePacer;
}
