using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000B28 RID: 2856
public class MeterScreen : KScreen, IRender1000ms
{
	// Token: 0x17000655 RID: 1621
	// (get) Token: 0x060057FD RID: 22525 RVA: 0x001FDB69 File Offset: 0x001FBD69
	// (set) Token: 0x060057FE RID: 22526 RVA: 0x001FDB70 File Offset: 0x001FBD70
	public static MeterScreen Instance { get; private set; }

	// Token: 0x060057FF RID: 22527 RVA: 0x001FDB78 File Offset: 0x001FBD78
	public static void DestroyInstance()
	{
		MeterScreen.Instance = null;
	}

	// Token: 0x17000656 RID: 1622
	// (get) Token: 0x06005800 RID: 22528 RVA: 0x001FDB80 File Offset: 0x001FBD80
	public bool StartValuesSet
	{
		get
		{
			return this.startValuesSet;
		}
	}

	// Token: 0x06005801 RID: 22529 RVA: 0x001FDB88 File Offset: 0x001FBD88
	protected override void OnPrefabInit()
	{
		MeterScreen.Instance = this;
	}

	// Token: 0x06005802 RID: 22530 RVA: 0x001FDB90 File Offset: 0x001FBD90
	protected override void OnSpawn()
	{
		this.StressTooltip.OnToolTip = new Func<string>(this.OnStressTooltip);
		this.SickTooltip.OnToolTip = new Func<string>(this.OnSickTooltip);
		this.RationsTooltip.OnToolTip = new Func<string>(this.OnRationsTooltip);
		this.RedAlertTooltip.OnToolTip = new Func<string>(this.OnRedAlertTooltip);
		MultiToggle redAlertButton = this.RedAlertButton;
		redAlertButton.onClick = (System.Action)Delegate.Combine(redAlertButton.onClick, new System.Action(delegate
		{
			this.OnRedAlertClick();
		}));
		Game.Instance.Subscribe(1983128072, delegate(object data)
		{
			this.Refresh();
		});
		Game.Instance.Subscribe(1585324898, delegate(object data)
		{
			this.RefreshRedAlertButtonState();
		});
		Game.Instance.Subscribe(-1393151672, delegate(object data)
		{
			this.RefreshRedAlertButtonState();
		});
	}

	// Token: 0x06005803 RID: 22531 RVA: 0x001FDC74 File Offset: 0x001FBE74
	private void OnRedAlertClick()
	{
		bool flag = !ClusterManager.Instance.activeWorld.AlertManager.IsRedAlertToggledOn();
		ClusterManager.Instance.activeWorld.AlertManager.ToggleRedAlert(flag);
		if (flag)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Open", false));
			return;
		}
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click_Close", false));
	}

	// Token: 0x06005804 RID: 22532 RVA: 0x001FDCD2 File Offset: 0x001FBED2
	private void RefreshRedAlertButtonState()
	{
		this.RedAlertButton.ChangeState(ClusterManager.Instance.activeWorld.IsRedAlert() ? 1 : 0);
	}

	// Token: 0x06005805 RID: 22533 RVA: 0x001FDCF4 File Offset: 0x001FBEF4
	public void Render1000ms(float dt)
	{
		this.Refresh();
	}

	// Token: 0x06005806 RID: 22534 RVA: 0x001FDCFC File Offset: 0x001FBEFC
	public void InitializeValues()
	{
		if (this.startValuesSet)
		{
			return;
		}
		this.startValuesSet = true;
		this.Refresh();
	}

	// Token: 0x06005807 RID: 22535 RVA: 0x001FDD14 File Offset: 0x001FBF14
	private void Refresh()
	{
		this.RefreshWorldMinionIdentities();
		this.RefreshMinions();
		this.RefreshRations();
		this.RefreshStress();
		this.RefreshSick();
		this.RefreshRedAlertButtonState();
	}

	// Token: 0x06005808 RID: 22536 RVA: 0x001FDD3C File Offset: 0x001FBF3C
	private void RefreshWorldMinionIdentities()
	{
		this.worldLiveMinionIdentities = new List<MinionIdentity>(from x in Components.LiveMinionIdentities.GetWorldItems(ClusterManager.Instance.activeWorldId, false)
			where !x.IsNullOrDestroyed()
			select x);
	}

	// Token: 0x06005809 RID: 22537 RVA: 0x001FDD8D File Offset: 0x001FBF8D
	private List<MinionIdentity> GetWorldMinionIdentities()
	{
		if (this.worldLiveMinionIdentities == null)
		{
			this.RefreshWorldMinionIdentities();
		}
		return this.worldLiveMinionIdentities;
	}

	// Token: 0x0600580A RID: 22538 RVA: 0x001FDDA4 File Offset: 0x001FBFA4
	private void RefreshMinions()
	{
		int count = Components.LiveMinionIdentities.Count;
		int count2 = this.GetWorldMinionIdentities().Count;
		if (count2 == this.cachedMinionCount)
		{
			return;
		}
		this.cachedMinionCount = count2;
		string text;
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			ClusterGridEntity component = ClusterManager.Instance.activeWorld.GetComponent<ClusterGridEntity>();
			text = string.Format(UI.TOOLTIPS.METERSCREEN_POPULATION_CLUSTER, component.Name, count2, count);
			this.currentMinions.text = string.Format("{0}/{1}", count2, count);
		}
		else
		{
			this.currentMinions.text = string.Format("{0}", count);
			text = string.Format(UI.TOOLTIPS.METERSCREEN_POPULATION, count.ToString("0"));
		}
		this.MinionsTooltip.ClearMultiStringTooltip();
		this.MinionsTooltip.AddMultiStringTooltip(text, this.ToolTipStyle_Header);
	}

	// Token: 0x0600580B RID: 22539 RVA: 0x001FDE90 File Offset: 0x001FC090
	private void RefreshSick()
	{
		int num = this.CountSickDupes();
		this.SickText.text = num.ToString();
	}

	// Token: 0x0600580C RID: 22540 RVA: 0x001FDEB8 File Offset: 0x001FC0B8
	private void RefreshRations()
	{
		if (this.RationsText != null && RationTracker.Get() != null)
		{
			long num = (long)RationTracker.Get().CountRations(null, ClusterManager.Instance.activeWorld.worldInventory, true);
			if (this.cachedCalories != num)
			{
				this.RationsText.text = GameUtil.GetFormattedCalories((float)num, GameUtil.TimeSlice.None, true);
				this.cachedCalories = num;
			}
		}
		this.rationsSpark.GetComponentInChildren<SparkLayer>().SetColor(((float)this.cachedCalories > (float)this.GetWorldMinionIdentities().Count * 1000000f) ? Constants.NEUTRAL_COLOR : Constants.NEGATIVE_COLOR);
		this.rationsSpark.GetComponentInChildren<LineLayer>().RefreshLine(TrackerTool.Instance.GetWorldTracker<KCalTracker>(ClusterManager.Instance.activeWorldId).ChartableData(600f), "kcal");
	}

	// Token: 0x0600580D RID: 22541 RVA: 0x001FDF8C File Offset: 0x001FC18C
	private IList<MinionIdentity> GetStressedMinions()
	{
		Amount stress_amount = Db.Get().Amounts.Stress;
		return (from x in new List<MinionIdentity>(this.GetWorldMinionIdentities())
			where !x.IsNullOrDestroyed()
			orderby stress_amount.Lookup(x).value descending
			select x).ToList<MinionIdentity>();
	}

	// Token: 0x0600580E RID: 22542 RVA: 0x001FDFFC File Offset: 0x001FC1FC
	private string OnStressTooltip()
	{
		float maxStressInActiveWorld = GameUtil.GetMaxStressInActiveWorld();
		this.StressTooltip.ClearMultiStringTooltip();
		this.StressTooltip.AddMultiStringTooltip(string.Format(UI.TOOLTIPS.METERSCREEN_AVGSTRESS, Mathf.Round(maxStressInActiveWorld).ToString() + "%"), this.ToolTipStyle_Header);
		Amount stress = Db.Get().Amounts.Stress;
		IList<MinionIdentity> stressedMinions = this.GetStressedMinions();
		for (int i = 0; i < stressedMinions.Count; i++)
		{
			MinionIdentity minionIdentity = stressedMinions[i];
			AmountInstance amountInstance = stress.Lookup(minionIdentity);
			this.AddToolTipAmountPercentLine(this.StressTooltip, amountInstance, minionIdentity, i == this.stressDisplayInfo.selectedIndex);
		}
		return "";
	}

	// Token: 0x0600580F RID: 22543 RVA: 0x001FE0B8 File Offset: 0x001FC2B8
	private string OnSickTooltip()
	{
		int num = this.CountSickDupes();
		List<MinionIdentity> worldMinionIdentities = this.GetWorldMinionIdentities();
		this.SickTooltip.ClearMultiStringTooltip();
		this.SickTooltip.AddMultiStringTooltip(string.Format(UI.TOOLTIPS.METERSCREEN_SICK_DUPES, num.ToString()), this.ToolTipStyle_Header);
		for (int i = 0; i < worldMinionIdentities.Count; i++)
		{
			MinionIdentity minionIdentity = worldMinionIdentities[i];
			if (!minionIdentity.IsNullOrDestroyed())
			{
				string text = minionIdentity.GetComponent<KSelectable>().GetName();
				Sicknesses sicknesses = minionIdentity.GetComponent<MinionModifiers>().sicknesses;
				if (sicknesses.IsInfected())
				{
					text += " (";
					int num2 = 0;
					foreach (SicknessInstance sicknessInstance in sicknesses)
					{
						text = text + ((num2 > 0) ? ", " : "") + sicknessInstance.modifier.Name;
						num2++;
					}
					text += ")";
				}
				bool flag = i == this.immunityDisplayInfo.selectedIndex;
				this.AddToolTipLine(this.SickTooltip, text, flag);
			}
		}
		return "";
	}

	// Token: 0x06005810 RID: 22544 RVA: 0x001FE200 File Offset: 0x001FC400
	private int CountSickDupes()
	{
		int num = 0;
		foreach (MinionIdentity minionIdentity in this.GetWorldMinionIdentities())
		{
			if (!minionIdentity.IsNullOrDestroyed() && minionIdentity.GetComponent<MinionModifiers>().sicknesses.IsInfected())
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06005811 RID: 22545 RVA: 0x001FE270 File Offset: 0x001FC470
	private void AddToolTipLine(ToolTip tooltip, string str, bool selected)
	{
		if (selected)
		{
			tooltip.AddMultiStringTooltip("<color=#F0B310FF>" + str + "</color>", this.ToolTipStyle_Property);
			return;
		}
		tooltip.AddMultiStringTooltip(str, this.ToolTipStyle_Property);
	}

	// Token: 0x06005812 RID: 22546 RVA: 0x001FE2A0 File Offset: 0x001FC4A0
	private void AddToolTipAmountPercentLine(ToolTip tooltip, AmountInstance amount, MinionIdentity id, bool selected)
	{
		string text = id.GetComponent<KSelectable>().GetName() + ":  " + Mathf.Round(amount.value).ToString() + "%";
		this.AddToolTipLine(tooltip, text, selected);
	}

	// Token: 0x06005813 RID: 22547 RVA: 0x001FE2E8 File Offset: 0x001FC4E8
	private string OnRationsTooltip()
	{
		this.rationsDict.Clear();
		float num = RationTracker.Get().CountRations(this.rationsDict, ClusterManager.Instance.activeWorld.worldInventory, true);
		this.RationsText.text = GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true);
		this.RationsTooltip.ClearMultiStringTooltip();
		this.RationsTooltip.AddMultiStringTooltip(string.Format(UI.TOOLTIPS.METERSCREEN_MEALHISTORY, GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true)), this.ToolTipStyle_Header);
		this.RationsTooltip.AddMultiStringTooltip("", this.ToolTipStyle_Property);
		foreach (KeyValuePair<string, float> keyValuePair in this.rationsDict.OrderByDescending(delegate(KeyValuePair<string, float> x)
		{
			EdiblesManager.FoodInfo foodInfo2 = EdiblesManager.GetFoodInfo(x.Key);
			return x.Value * ((foodInfo2 != null) ? foodInfo2.CaloriesPerUnit : (-1f));
		}).ToDictionary((KeyValuePair<string, float> t) => t.Key, (KeyValuePair<string, float> t) => t.Value))
		{
			EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(keyValuePair.Key);
			this.RationsTooltip.AddMultiStringTooltip((foodInfo != null) ? string.Format("{0}: {1}", foodInfo.Name, GameUtil.GetFormattedCalories(keyValuePair.Value * foodInfo.CaloriesPerUnit, GameUtil.TimeSlice.None, true)) : string.Format(UI.TOOLTIPS.METERSCREEN_INVALID_FOOD_TYPE, keyValuePair.Key), this.ToolTipStyle_Property);
		}
		return "";
	}

	// Token: 0x06005814 RID: 22548 RVA: 0x001FE488 File Offset: 0x001FC688
	private string OnRedAlertTooltip()
	{
		this.RedAlertTooltip.ClearMultiStringTooltip();
		this.RedAlertTooltip.AddMultiStringTooltip(UI.TOOLTIPS.RED_ALERT_TITLE, this.ToolTipStyle_Header);
		this.RedAlertTooltip.AddMultiStringTooltip(UI.TOOLTIPS.RED_ALERT_CONTENT, this.ToolTipStyle_Property);
		return "";
	}

	// Token: 0x06005815 RID: 22549 RVA: 0x001FE4DC File Offset: 0x001FC6DC
	private void RefreshStress()
	{
		float maxStressInActiveWorld = GameUtil.GetMaxStressInActiveWorld();
		this.StressText.text = Mathf.Round(maxStressInActiveWorld).ToString();
		WorldTracker worldTracker = TrackerTool.Instance.GetWorldTracker<StressTracker>(ClusterManager.Instance.activeWorldId);
		this.stressSpark.GetComponentInChildren<SparkLayer>().SetColor((worldTracker.GetCurrentValue() >= STRESS.ACTING_OUT_RESET) ? Constants.NEGATIVE_COLOR : Constants.NEUTRAL_COLOR);
		this.stressSpark.GetComponentInChildren<LineLayer>().RefreshLine(worldTracker.ChartableData(600f), "stressData");
	}

	// Token: 0x06005816 RID: 22550 RVA: 0x001FE568 File Offset: 0x001FC768
	public void OnClickStress(BaseEventData base_ev_data)
	{
		IList<MinionIdentity> stressedMinions = this.GetStressedMinions();
		this.UpdateDisplayInfo(base_ev_data, ref this.stressDisplayInfo, stressedMinions);
		this.OnStressTooltip();
		this.StressTooltip.forceRefresh = true;
	}

	// Token: 0x06005817 RID: 22551 RVA: 0x001FE59D File Offset: 0x001FC79D
	private IList<MinionIdentity> GetSickMinions()
	{
		return this.GetWorldMinionIdentities();
	}

	// Token: 0x06005818 RID: 22552 RVA: 0x001FE5A8 File Offset: 0x001FC7A8
	public void OnClickImmunity(BaseEventData base_ev_data)
	{
		IList<MinionIdentity> sickMinions = this.GetSickMinions();
		this.UpdateDisplayInfo(base_ev_data, ref this.immunityDisplayInfo, sickMinions);
		this.OnSickTooltip();
		this.SickTooltip.forceRefresh = true;
	}

	// Token: 0x06005819 RID: 22553 RVA: 0x001FE5E0 File Offset: 0x001FC7E0
	private void UpdateDisplayInfo(BaseEventData base_ev_data, ref MeterScreen.DisplayInfo display_info, IList<MinionIdentity> minions)
	{
		PointerEventData pointerEventData = base_ev_data as PointerEventData;
		if (pointerEventData == null)
		{
			return;
		}
		List<MinionIdentity> worldMinionIdentities = this.GetWorldMinionIdentities();
		PointerEventData.InputButton button = pointerEventData.button;
		if (button != PointerEventData.InputButton.Left)
		{
			if (button != PointerEventData.InputButton.Right)
			{
				return;
			}
			display_info.selectedIndex = -1;
		}
		else
		{
			if (worldMinionIdentities.Count < display_info.selectedIndex)
			{
				display_info.selectedIndex = -1;
			}
			if (worldMinionIdentities.Count > 0)
			{
				display_info.selectedIndex = (display_info.selectedIndex + 1) % worldMinionIdentities.Count;
				MinionIdentity minionIdentity = minions[display_info.selectedIndex];
				SelectTool.Instance.SelectAndFocus(minionIdentity.transform.GetPosition(), minionIdentity.GetComponent<KSelectable>(), new Vector3(5f, 0f, 0f));
				return;
			}
		}
	}

	// Token: 0x04003B83 RID: 15235
	[SerializeField]
	private LocText currentMinions;

	// Token: 0x04003B85 RID: 15237
	public ToolTip MinionsTooltip;

	// Token: 0x04003B86 RID: 15238
	public LocText StressText;

	// Token: 0x04003B87 RID: 15239
	public ToolTip StressTooltip;

	// Token: 0x04003B88 RID: 15240
	public GameObject stressSpark;

	// Token: 0x04003B89 RID: 15241
	public LocText RationsText;

	// Token: 0x04003B8A RID: 15242
	public ToolTip RationsTooltip;

	// Token: 0x04003B8B RID: 15243
	public GameObject rationsSpark;

	// Token: 0x04003B8C RID: 15244
	public LocText SickText;

	// Token: 0x04003B8D RID: 15245
	public ToolTip SickTooltip;

	// Token: 0x04003B8E RID: 15246
	public TextStyleSetting ToolTipStyle_Header;

	// Token: 0x04003B8F RID: 15247
	public TextStyleSetting ToolTipStyle_Property;

	// Token: 0x04003B90 RID: 15248
	private bool startValuesSet;

	// Token: 0x04003B91 RID: 15249
	public MultiToggle RedAlertButton;

	// Token: 0x04003B92 RID: 15250
	public ToolTip RedAlertTooltip;

	// Token: 0x04003B93 RID: 15251
	private MeterScreen.DisplayInfo stressDisplayInfo = new MeterScreen.DisplayInfo
	{
		selectedIndex = -1
	};

	// Token: 0x04003B94 RID: 15252
	private MeterScreen.DisplayInfo immunityDisplayInfo = new MeterScreen.DisplayInfo
	{
		selectedIndex = -1
	};

	// Token: 0x04003B95 RID: 15253
	private List<MinionIdentity> worldLiveMinionIdentities;

	// Token: 0x04003B96 RID: 15254
	private int cachedMinionCount = -1;

	// Token: 0x04003B97 RID: 15255
	private long cachedCalories = -1L;

	// Token: 0x04003B98 RID: 15256
	private Dictionary<string, float> rationsDict = new Dictionary<string, float>();

	// Token: 0x020019B5 RID: 6581
	private struct DisplayInfo
	{
		// Token: 0x04007505 RID: 29957
		public int selectedIndex;
	}
}
