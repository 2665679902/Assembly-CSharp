using System;
using System.Collections;
using System.Collections.Generic;
using Klei;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000B70 RID: 2928
[AddComponentMenu("KMonoBehaviour/scripts/ResourceEntry")]
public class ResourceEntry : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISim4000ms
{
	// Token: 0x06005BAB RID: 23467 RVA: 0x0021683C File Offset: 0x00214A3C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.QuantityLabel.color = this.AvailableColor;
		this.NameLabel.color = this.AvailableColor;
		this.button.onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x06005BAC RID: 23468 RVA: 0x0021688D File Offset: 0x00214A8D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.tooltip.OnToolTip = new Func<string>(this.OnToolTip);
		this.RefreshChart();
	}

	// Token: 0x06005BAD RID: 23469 RVA: 0x002168B4 File Offset: 0x00214AB4
	private void OnClick()
	{
		this.lastClickTime = Time.unscaledTime;
		if (this.cachedPickupables == null)
		{
			this.cachedPickupables = ClusterManager.Instance.activeWorld.worldInventory.CreatePickupablesList(this.Resource);
			base.StartCoroutine(this.ClearCachedPickupablesAfterThreshold());
		}
		if (this.cachedPickupables == null)
		{
			return;
		}
		Pickupable pickupable = null;
		for (int i = 0; i < this.cachedPickupables.Count; i++)
		{
			this.selectionIdx++;
			int num = this.selectionIdx % this.cachedPickupables.Count;
			pickupable = this.cachedPickupables[num];
			if (pickupable != null && !pickupable.HasTag(GameTags.StoredPrivate))
			{
				break;
			}
		}
		if (pickupable != null)
		{
			Transform transform = pickupable.transform;
			if (pickupable.storage != null)
			{
				transform = pickupable.storage.transform;
			}
			SelectTool.Instance.SelectAndFocus(transform.transform.GetPosition(), transform.GetComponent<KSelectable>(), Vector3.zero);
			for (int j = 0; j < this.cachedPickupables.Count; j++)
			{
				Pickupable pickupable2 = this.cachedPickupables[j];
				if (pickupable2 != null)
				{
					KAnimControllerBase component = pickupable2.GetComponent<KAnimControllerBase>();
					if (component != null)
					{
						component.HighlightColour = this.HighlightColor;
					}
				}
			}
		}
	}

	// Token: 0x06005BAE RID: 23470 RVA: 0x00216A0B File Offset: 0x00214C0B
	private IEnumerator ClearCachedPickupablesAfterThreshold()
	{
		while (this.cachedPickupables != null && this.lastClickTime != 0f && Time.unscaledTime - this.lastClickTime < 10f)
		{
			yield return SequenceUtil.WaitForSeconds(1f);
		}
		this.cachedPickupables = null;
		yield break;
	}

	// Token: 0x06005BAF RID: 23471 RVA: 0x00216A1C File Offset: 0x00214C1C
	public void GetAmounts(EdiblesManager.FoodInfo food_info, bool doExtras, out float available, out float total, out float reserved)
	{
		available = ClusterManager.Instance.activeWorld.worldInventory.GetAmount(this.Resource, false);
		total = (doExtras ? ClusterManager.Instance.activeWorld.worldInventory.GetTotalAmount(this.Resource, false) : 0f);
		reserved = (doExtras ? MaterialNeeds.GetAmount(this.Resource, ClusterManager.Instance.activeWorldId, false) : 0f);
		if (food_info != null)
		{
			available *= food_info.CaloriesPerUnit;
			total *= food_info.CaloriesPerUnit;
			reserved *= food_info.CaloriesPerUnit;
		}
	}

	// Token: 0x06005BB0 RID: 23472 RVA: 0x00216ABC File Offset: 0x00214CBC
	private void GetAmounts(bool doExtras, out float available, out float total, out float reserved)
	{
		EdiblesManager.FoodInfo foodInfo = ((this.Measure == GameUtil.MeasureUnit.kcal) ? EdiblesManager.GetFoodInfo(this.Resource.Name) : null);
		this.GetAmounts(foodInfo, doExtras, out available, out total, out reserved);
	}

	// Token: 0x06005BB1 RID: 23473 RVA: 0x00216AF4 File Offset: 0x00214CF4
	public void UpdateValue()
	{
		this.SetName(this.Resource.ProperName());
		bool allowInsufficientMaterialBuild = GenericGameSettings.instance.allowInsufficientMaterialBuild;
		float num;
		float num2;
		float num3;
		this.GetAmounts(allowInsufficientMaterialBuild, out num, out num2, out num3);
		if (this.currentQuantity != num)
		{
			this.currentQuantity = num;
			this.QuantityLabel.text = ResourceCategoryScreen.QuantityTextForMeasure(num, this.Measure);
		}
		Color color = this.AvailableColor;
		if (num3 > num2)
		{
			color = this.OverdrawnColor;
		}
		else if (num == 0f)
		{
			color = this.UnavailableColor;
		}
		if (this.QuantityLabel.color != color)
		{
			this.QuantityLabel.color = color;
		}
		if (this.NameLabel.color != color)
		{
			this.NameLabel.color = color;
		}
	}

	// Token: 0x06005BB2 RID: 23474 RVA: 0x00216BBC File Offset: 0x00214DBC
	private string OnToolTip()
	{
		float num;
		float num2;
		float num3;
		this.GetAmounts(true, out num, out num2, out num3);
		string text = this.NameLabel.text + "\n";
		text += string.Format(UI.RESOURCESCREEN.AVAILABLE_TOOLTIP, ResourceCategoryScreen.QuantityTextForMeasure(num, this.Measure), ResourceCategoryScreen.QuantityTextForMeasure(num3, this.Measure), ResourceCategoryScreen.QuantityTextForMeasure(num2, this.Measure));
		float delta = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, this.Resource).GetDelta(150f);
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

	// Token: 0x06005BB3 RID: 23475 RVA: 0x00216CB2 File Offset: 0x00214EB2
	public void SetName(string name)
	{
		this.NameLabel.text = name;
	}

	// Token: 0x06005BB4 RID: 23476 RVA: 0x00216CC0 File Offset: 0x00214EC0
	public void SetTag(Tag t, GameUtil.MeasureUnit measure)
	{
		this.Resource = t;
		this.Measure = measure;
		this.cachedPickupables = null;
	}

	// Token: 0x06005BB5 RID: 23477 RVA: 0x00216CD8 File Offset: 0x00214ED8
	private void Hover(bool is_hovering)
	{
		if (ClusterManager.Instance.activeWorld.worldInventory == null)
		{
			return;
		}
		if (is_hovering)
		{
			this.Background.color = this.BackgroundHoverColor;
		}
		else
		{
			this.Background.color = new Color(0f, 0f, 0f, 0f);
		}
		ICollection<Pickupable> pickupables = ClusterManager.Instance.activeWorld.worldInventory.GetPickupables(this.Resource, false);
		if (pickupables == null)
		{
			return;
		}
		foreach (Pickupable pickupable in pickupables)
		{
			if (!(pickupable == null))
			{
				KAnimControllerBase component = pickupable.GetComponent<KAnimControllerBase>();
				if (!(component == null))
				{
					if (is_hovering)
					{
						component.HighlightColour = this.HighlightColor;
					}
					else
					{
						component.HighlightColour = Color.black;
					}
				}
			}
		}
	}

	// Token: 0x06005BB6 RID: 23478 RVA: 0x00216DCC File Offset: 0x00214FCC
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.Hover(true);
	}

	// Token: 0x06005BB7 RID: 23479 RVA: 0x00216DD5 File Offset: 0x00214FD5
	public void OnPointerExit(PointerEventData eventData)
	{
		this.Hover(false);
	}

	// Token: 0x06005BB8 RID: 23480 RVA: 0x00216DE0 File Offset: 0x00214FE0
	public void SetSprite(Tag t)
	{
		Element element = ElementLoader.FindElementByName(this.Resource.Name);
		if (element != null)
		{
			Sprite uispriteFromMultiObjectAnim = Def.GetUISpriteFromMultiObjectAnim(element.substance.anim, "ui", false, "");
			if (uispriteFromMultiObjectAnim != null)
			{
				this.image.sprite = uispriteFromMultiObjectAnim;
			}
		}
	}

	// Token: 0x06005BB9 RID: 23481 RVA: 0x00216E32 File Offset: 0x00215032
	public void SetSprite(Sprite sprite)
	{
		this.image.sprite = sprite;
	}

	// Token: 0x06005BBA RID: 23482 RVA: 0x00216E40 File Offset: 0x00215040
	public void Sim4000ms(float dt)
	{
		this.RefreshChart();
	}

	// Token: 0x06005BBB RID: 23483 RVA: 0x00216E48 File Offset: 0x00215048
	private void RefreshChart()
	{
		if (this.sparkChart != null)
		{
			ResourceTracker resourceStatistic = TrackerTool.Instance.GetResourceStatistic(ClusterManager.Instance.activeWorldId, this.Resource);
			this.sparkChart.GetComponentInChildren<LineLayer>().RefreshLine(resourceStatistic.ChartableData(3000f), "resourceAmount");
			this.sparkChart.GetComponentInChildren<SparkLayer>().SetColor(Constants.NEUTRAL_COLOR);
		}
	}

	// Token: 0x04003E7F RID: 15999
	public Tag Resource;

	// Token: 0x04003E80 RID: 16000
	public GameUtil.MeasureUnit Measure;

	// Token: 0x04003E81 RID: 16001
	public LocText NameLabel;

	// Token: 0x04003E82 RID: 16002
	public LocText QuantityLabel;

	// Token: 0x04003E83 RID: 16003
	public Image image;

	// Token: 0x04003E84 RID: 16004
	[SerializeField]
	private Color AvailableColor;

	// Token: 0x04003E85 RID: 16005
	[SerializeField]
	private Color UnavailableColor;

	// Token: 0x04003E86 RID: 16006
	[SerializeField]
	private Color OverdrawnColor;

	// Token: 0x04003E87 RID: 16007
	[SerializeField]
	private Color HighlightColor;

	// Token: 0x04003E88 RID: 16008
	[SerializeField]
	private Color BackgroundHoverColor;

	// Token: 0x04003E89 RID: 16009
	[SerializeField]
	private Image Background;

	// Token: 0x04003E8A RID: 16010
	[MyCmpGet]
	private ToolTip tooltip;

	// Token: 0x04003E8B RID: 16011
	[MyCmpReq]
	private Button button;

	// Token: 0x04003E8C RID: 16012
	public GameObject sparkChart;

	// Token: 0x04003E8D RID: 16013
	private const float CLICK_RESET_TIME_THRESHOLD = 10f;

	// Token: 0x04003E8E RID: 16014
	private int selectionIdx;

	// Token: 0x04003E8F RID: 16015
	private float lastClickTime;

	// Token: 0x04003E90 RID: 16016
	private List<Pickupable> cachedPickupables;

	// Token: 0x04003E91 RID: 16017
	private float currentQuantity = float.MinValue;
}
