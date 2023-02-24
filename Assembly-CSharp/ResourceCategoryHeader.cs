using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000B6E RID: 2926
[AddComponentMenu("KMonoBehaviour/scripts/ResourceCategoryHeader")]
public class ResourceCategoryHeader : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISim4000ms
{
	// Token: 0x06005B91 RID: 23441 RVA: 0x00215BE8 File Offset: 0x00213DE8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.EntryContainer.SetParent(base.transform.parent);
		this.EntryContainer.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
		this.EntryContainer.localScale = Vector3.one;
		this.mButton = base.GetComponent<Button>();
		this.mButton.onClick.AddListener(delegate
		{
			this.ToggleOpen(true);
		});
		this.SetInteractable(this.anyDiscovered);
		this.SetActiveColor(false);
	}

	// Token: 0x06005B92 RID: 23442 RVA: 0x00215C74 File Offset: 0x00213E74
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.tooltip.OnToolTip = new Func<string>(this.OnTooltip);
		this.UpdateContents();
		this.RefreshChart();
	}

	// Token: 0x06005B93 RID: 23443 RVA: 0x00215C9F File Offset: 0x00213E9F
	private void SetInteractable(bool state)
	{
		if (!state)
		{
			this.SetOpen(false);
			this.expandArrow.SetDisabled();
			return;
		}
		if (!this.IsOpen)
		{
			this.expandArrow.SetInactive();
			return;
		}
		this.expandArrow.SetActive();
	}

	// Token: 0x06005B94 RID: 23444 RVA: 0x00215CD8 File Offset: 0x00213ED8
	private void SetActiveColor(bool state)
	{
		if (state)
		{
			this.elements.QuantityText.color = this.TextColor_Interactable;
			this.elements.LabelText.color = this.TextColor_Interactable;
			this.expandArrow.ActiveColour = this.TextColor_Interactable;
			this.expandArrow.InactiveColour = this.TextColor_Interactable;
			this.expandArrow.TargetImage.color = this.TextColor_Interactable;
			return;
		}
		this.elements.LabelText.color = this.TextColor_NonInteractable;
		this.elements.QuantityText.color = this.TextColor_NonInteractable;
		this.expandArrow.ActiveColour = this.TextColor_NonInteractable;
		this.expandArrow.InactiveColour = this.TextColor_NonInteractable;
		this.expandArrow.TargetImage.color = this.TextColor_NonInteractable;
	}

	// Token: 0x06005B95 RID: 23445 RVA: 0x00215DB4 File Offset: 0x00213FB4
	public void SetTag(Tag t, GameUtil.MeasureUnit measure)
	{
		this.ResourceCategoryTag = t;
		this.Measure = measure;
		this.elements.LabelText.text = t.ProperName();
		if (SaveGame.Instance.expandedResourceTags.Contains(this.ResourceCategoryTag))
		{
			this.anyDiscovered = true;
			this.ToggleOpen(false);
		}
	}

	// Token: 0x06005B96 RID: 23446 RVA: 0x00215E0C File Offset: 0x0021400C
	private void ToggleOpen(bool play_sound)
	{
		if (!this.anyDiscovered)
		{
			if (play_sound)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
			}
			return;
		}
		if (!this.IsOpen)
		{
			if (play_sound)
			{
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Open", false));
			}
			this.SetOpen(true);
			this.elements.LabelText.fontSize = (float)this.maximizedFontSize;
			this.elements.QuantityText.fontSize = (float)this.maximizedFontSize;
			return;
		}
		if (play_sound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
		}
		this.SetOpen(false);
		this.elements.LabelText.fontSize = (float)this.minimizedFontSize;
		this.elements.QuantityText.fontSize = (float)this.minimizedFontSize;
	}

	// Token: 0x06005B97 RID: 23447 RVA: 0x00215ED0 File Offset: 0x002140D0
	private void Hover(bool is_hovering)
	{
		this.Background.color = (is_hovering ? this.BackgroundHoverColor : new Color(0f, 0f, 0f, 0f));
		ICollection<Pickupable> collection = null;
		if (ClusterManager.Instance.activeWorld.worldInventory != null)
		{
			collection = ClusterManager.Instance.activeWorld.worldInventory.GetPickupables(this.ResourceCategoryTag, false);
		}
		if (collection == null)
		{
			return;
		}
		foreach (Pickupable pickupable in collection)
		{
			if (!(pickupable == null))
			{
				KAnimControllerBase component = pickupable.GetComponent<KAnimControllerBase>();
				if (!(component == null))
				{
					component.HighlightColour = (is_hovering ? this.highlightColour : Color.black);
				}
			}
		}
	}

	// Token: 0x06005B98 RID: 23448 RVA: 0x00215FB0 File Offset: 0x002141B0
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.Hover(true);
	}

	// Token: 0x06005B99 RID: 23449 RVA: 0x00215FB9 File Offset: 0x002141B9
	public void OnPointerExit(PointerEventData eventData)
	{
		this.Hover(false);
	}

	// Token: 0x06005B9A RID: 23450 RVA: 0x00215FC4 File Offset: 0x002141C4
	public void SetOpen(bool open)
	{
		this.IsOpen = open;
		if (open)
		{
			this.expandArrow.SetActive();
			if (!SaveGame.Instance.expandedResourceTags.Contains(this.ResourceCategoryTag))
			{
				SaveGame.Instance.expandedResourceTags.Add(this.ResourceCategoryTag);
			}
		}
		else
		{
			this.expandArrow.SetInactive();
			SaveGame.Instance.expandedResourceTags.Remove(this.ResourceCategoryTag);
		}
		this.EntryContainer.gameObject.SetActive(this.IsOpen);
	}

	// Token: 0x06005B9B RID: 23451 RVA: 0x0021604C File Offset: 0x0021424C
	private void GetAmounts(bool doExtras, out float available, out float total, out float reserved)
	{
		available = 0f;
		total = 0f;
		reserved = 0f;
		HashSet<Tag> hashSet = null;
		if (!DiscoveredResources.Instance.TryGetDiscoveredResourcesFromTag(this.ResourceCategoryTag, out hashSet))
		{
			return;
		}
		ListPool<Tag, ResourceCategoryHeader>.PooledList pooledList = ListPool<Tag, ResourceCategoryHeader>.Allocate();
		foreach (Tag tag in hashSet)
		{
			EdiblesManager.FoodInfo foodInfo = null;
			if (this.Measure == GameUtil.MeasureUnit.kcal)
			{
				foodInfo = EdiblesManager.GetFoodInfo(tag.Name);
				if (foodInfo == null)
				{
					pooledList.Add(tag);
					continue;
				}
			}
			this.anyDiscovered = true;
			ResourceEntry resourceEntry = null;
			if (!this.ResourcesDiscovered.TryGetValue(tag, out resourceEntry))
			{
				resourceEntry = this.NewResourceEntry(tag, this.Measure);
				this.ResourcesDiscovered.Add(tag, resourceEntry);
			}
			float num;
			float num2;
			float num3;
			resourceEntry.GetAmounts(foodInfo, doExtras, out num, out num2, out num3);
			available += num;
			total += num2;
			reserved += num3;
		}
		foreach (Tag tag2 in pooledList)
		{
			hashSet.Remove(tag2);
		}
		pooledList.Recycle();
	}

	// Token: 0x06005B9C RID: 23452 RVA: 0x00216198 File Offset: 0x00214398
	public void UpdateContents()
	{
		float num;
		float num2;
		float num3;
		this.GetAmounts(false, out num, out num2, out num3);
		if (num != this.cachedAvailable || num2 != this.cachedTotal || num3 != this.cachedReserved)
		{
			if (this.quantityString == null || this.currentQuantity != num)
			{
				switch (this.Measure)
				{
				case GameUtil.MeasureUnit.mass:
					this.quantityString = GameUtil.GetFormattedMass(num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}");
					break;
				case GameUtil.MeasureUnit.kcal:
					this.quantityString = GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true);
					break;
				case GameUtil.MeasureUnit.quantity:
					this.quantityString = num.ToString();
					break;
				}
				this.elements.QuantityText.text = this.quantityString;
				this.currentQuantity = num;
			}
			this.cachedAvailable = num;
			this.cachedTotal = num2;
			this.cachedReserved = num3;
		}
		foreach (KeyValuePair<Tag, ResourceEntry> keyValuePair in this.ResourcesDiscovered)
		{
			keyValuePair.Value.UpdateValue();
		}
		this.SetActiveColor(num > 0f);
		this.SetInteractable(this.anyDiscovered);
	}

	// Token: 0x06005B9D RID: 23453 RVA: 0x002162C8 File Offset: 0x002144C8
	private string OnTooltip()
	{
		float num;
		float num2;
		float num3;
		this.GetAmounts(true, out num, out num2, out num3);
		string text = this.elements.LabelText.text + "\n";
		text += string.Format(UI.RESOURCESCREEN.AVAILABLE_TOOLTIP, ResourceCategoryScreen.QuantityTextForMeasure(num, this.Measure), ResourceCategoryScreen.QuantityTextForMeasure(num3, this.Measure), ResourceCategoryScreen.QuantityTextForMeasure(num2, this.Measure));
		float delta = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, this.ResourceCategoryTag).GetDelta(150f);
		if (delta != 0f)
		{
			text = text + "\n\n" + string.Format(UI.RESOURCESCREEN.TREND_TOOLTIP, (delta > 0f) ? UI.RESOURCESCREEN.INCREASING_STR : UI.RESOURCESCREEN.DECREASING_STR, GameUtil.GetFormattedMass(Mathf.Abs(delta), GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		}
		else
		{
			text = text + "\n\n" + UI.RESOURCESCREEN.TREND_TOOLTIP_NO_CHANGE;
		}
		return text;
	}

	// Token: 0x06005B9E RID: 23454 RVA: 0x002163C3 File Offset: 0x002145C3
	private ResourceEntry NewResourceEntry(Tag resourceTag, GameUtil.MeasureUnit measure)
	{
		ResourceEntry component = Util.KInstantiateUI(this.Prefab_ResourceEntry, this.EntryContainer.gameObject, true).GetComponent<ResourceEntry>();
		component.SetTag(resourceTag, measure);
		return component;
	}

	// Token: 0x06005B9F RID: 23455 RVA: 0x002163E9 File Offset: 0x002145E9
	public void Sim4000ms(float dt)
	{
		this.RefreshChart();
	}

	// Token: 0x06005BA0 RID: 23456 RVA: 0x002163F4 File Offset: 0x002145F4
	private void RefreshChart()
	{
		if (this.sparkChart != null)
		{
			ResourceTracker resourceStatistic = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, this.ResourceCategoryTag);
			this.sparkChart.GetComponentInChildren<LineLayer>().RefreshLine(resourceStatistic.ChartableData(3000f), "resourceAmount");
			this.sparkChart.GetComponentInChildren<SparkLayer>().SetColor(Constants.NEUTRAL_COLOR);
		}
	}

	// Token: 0x04003E5B RID: 15963
	public GameObject Prefab_ResourceEntry;

	// Token: 0x04003E5C RID: 15964
	public Transform EntryContainer;

	// Token: 0x04003E5D RID: 15965
	public Tag ResourceCategoryTag;

	// Token: 0x04003E5E RID: 15966
	public GameUtil.MeasureUnit Measure;

	// Token: 0x04003E5F RID: 15967
	public bool IsOpen;

	// Token: 0x04003E60 RID: 15968
	public ImageToggleState expandArrow;

	// Token: 0x04003E61 RID: 15969
	private Button mButton;

	// Token: 0x04003E62 RID: 15970
	public Dictionary<Tag, ResourceEntry> ResourcesDiscovered = new Dictionary<Tag, ResourceEntry>();

	// Token: 0x04003E63 RID: 15971
	public ResourceCategoryHeader.ElementReferences elements;

	// Token: 0x04003E64 RID: 15972
	public Color TextColor_Interactable;

	// Token: 0x04003E65 RID: 15973
	public Color TextColor_NonInteractable;

	// Token: 0x04003E66 RID: 15974
	private string quantityString;

	// Token: 0x04003E67 RID: 15975
	private float currentQuantity;

	// Token: 0x04003E68 RID: 15976
	private bool anyDiscovered;

	// Token: 0x04003E69 RID: 15977
	public const float chartHistoryLength = 3000f;

	// Token: 0x04003E6A RID: 15978
	[MyCmpGet]
	private ToolTip tooltip;

	// Token: 0x04003E6B RID: 15979
	[SerializeField]
	private int minimizedFontSize;

	// Token: 0x04003E6C RID: 15980
	[SerializeField]
	private int maximizedFontSize;

	// Token: 0x04003E6D RID: 15981
	[SerializeField]
	private Color highlightColour;

	// Token: 0x04003E6E RID: 15982
	[SerializeField]
	private Color BackgroundHoverColor;

	// Token: 0x04003E6F RID: 15983
	[SerializeField]
	private Image Background;

	// Token: 0x04003E70 RID: 15984
	public GameObject sparkChart;

	// Token: 0x04003E71 RID: 15985
	private float cachedAvailable = float.MinValue;

	// Token: 0x04003E72 RID: 15986
	private float cachedTotal = float.MinValue;

	// Token: 0x04003E73 RID: 15987
	private float cachedReserved = float.MinValue;

	// Token: 0x02001A1D RID: 6685
	[Serializable]
	public struct ElementReferences
	{
		// Token: 0x04007686 RID: 30342
		public LocText LabelText;

		// Token: 0x04007687 RID: 30343
		public LocText QuantityText;
	}
}
