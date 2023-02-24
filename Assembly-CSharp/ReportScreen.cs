using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000B66 RID: 2918
public class ReportScreen : KScreen
{
	// Token: 0x17000672 RID: 1650
	// (get) Token: 0x06005B16 RID: 23318 RVA: 0x00211004 File Offset: 0x0020F204
	// (set) Token: 0x06005B17 RID: 23319 RVA: 0x0021100B File Offset: 0x0020F20B
	public static ReportScreen Instance { get; private set; }

	// Token: 0x06005B18 RID: 23320 RVA: 0x00211013 File Offset: 0x0020F213
	public static void DestroyInstance()
	{
		ReportScreen.Instance = null;
	}

	// Token: 0x06005B19 RID: 23321 RVA: 0x0021101C File Offset: 0x0020F21C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		ReportScreen.Instance = this;
		this.closeButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		this.prevButton.onClick += delegate
		{
			this.ShowReport(this.currentReport.day - 1);
		};
		this.nextButton.onClick += delegate
		{
			this.ShowReport(this.currentReport.day + 1);
		};
		this.summaryButton.onClick += delegate
		{
			RetiredColonyData currentColonyRetiredColonyData = RetireColonyUtility.GetCurrentColonyRetiredColonyData();
			MainMenu.ActivateRetiredColoniesScreenFromData(PauseScreen.Instance.transform.parent.gameObject, currentColonyRetiredColonyData);
		};
		base.ConsumeMouseScroll = true;
	}

	// Token: 0x06005B1A RID: 23322 RVA: 0x002110BE File Offset: 0x0020F2BE
	protected override void OnSpawn()
	{
		base.OnSpawn();
	}

	// Token: 0x06005B1B RID: 23323 RVA: 0x002110C6 File Offset: 0x0020F2C6
	protected override void OnShow(bool bShow)
	{
		base.OnShow(bShow);
		if (ReportManager.Instance != null)
		{
			this.currentReport = ReportManager.Instance.TodaysReport;
		}
	}

	// Token: 0x06005B1C RID: 23324 RVA: 0x002110EC File Offset: 0x0020F2EC
	public void SetTitle(string title)
	{
		this.title.text = title;
	}

	// Token: 0x06005B1D RID: 23325 RVA: 0x002110FA File Offset: 0x0020F2FA
	public override void ScreenUpdate(bool b)
	{
		base.ScreenUpdate(b);
		this.Refresh();
	}

	// Token: 0x06005B1E RID: 23326 RVA: 0x0021110C File Offset: 0x0020F30C
	private void Refresh()
	{
		global::Debug.Assert(this.currentReport != null);
		if (this.currentReport.day == ReportManager.Instance.TodaysReport.day)
		{
			this.SetTitle(string.Format(UI.ENDOFDAYREPORT.DAY_TITLE_TODAY, this.currentReport.day));
		}
		else if (this.currentReport.day == ReportManager.Instance.TodaysReport.day - 1)
		{
			this.SetTitle(string.Format(UI.ENDOFDAYREPORT.DAY_TITLE_YESTERDAY, this.currentReport.day));
		}
		else
		{
			this.SetTitle(string.Format(UI.ENDOFDAYREPORT.DAY_TITLE, this.currentReport.day));
		}
		bool flag = this.currentReport.day < ReportManager.Instance.TodaysReport.day;
		this.nextButton.isInteractable = flag;
		if (flag)
		{
			this.nextButton.GetComponent<ToolTip>().toolTip = string.Format(UI.ENDOFDAYREPORT.DAY_TITLE, this.currentReport.day + 1);
			this.nextButton.GetComponent<ToolTip>().enabled = true;
		}
		else
		{
			this.nextButton.GetComponent<ToolTip>().enabled = false;
		}
		flag = this.currentReport.day > 1;
		this.prevButton.isInteractable = flag;
		if (flag)
		{
			this.prevButton.GetComponent<ToolTip>().toolTip = string.Format(UI.ENDOFDAYREPORT.DAY_TITLE, this.currentReport.day - 1);
			this.prevButton.GetComponent<ToolTip>().enabled = true;
		}
		else
		{
			this.prevButton.GetComponent<ToolTip>().enabled = false;
		}
		this.AddSpacer(0);
		int num = 1;
		foreach (KeyValuePair<ReportManager.ReportType, ReportManager.ReportGroup> keyValuePair in ReportManager.Instance.ReportGroups)
		{
			ReportManager.ReportEntry entry = this.currentReport.GetEntry(keyValuePair.Key);
			if (num != keyValuePair.Value.group)
			{
				num = keyValuePair.Value.group;
				this.AddSpacer(num);
			}
			bool flag2 = entry.accumulate != 0f || keyValuePair.Value.reportIfZero;
			if (keyValuePair.Value.isHeader)
			{
				this.CreateHeader(keyValuePair.Value);
			}
			else if (flag2)
			{
				this.CreateOrUpdateLine(entry, keyValuePair.Value, flag2);
			}
		}
	}

	// Token: 0x06005B1F RID: 23327 RVA: 0x002113A8 File Offset: 0x0020F5A8
	public void ShowReport(int day)
	{
		this.currentReport = ReportManager.Instance.FindReport(day);
		global::Debug.Assert(this.currentReport != null, "Can't find report for day: " + day.ToString());
		this.Refresh();
	}

	// Token: 0x06005B20 RID: 23328 RVA: 0x002113E0 File Offset: 0x0020F5E0
	private GameObject AddSpacer(int group)
	{
		GameObject gameObject;
		if (this.lineItems.ContainsKey(group.ToString()))
		{
			gameObject = this.lineItems[group.ToString()];
		}
		else
		{
			gameObject = Util.KInstantiateUI(this.lineItemSpacer, this.contentFolder, false);
			gameObject.name = "Spacer" + group.ToString();
			this.lineItems[group.ToString()] = gameObject;
		}
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06005B21 RID: 23329 RVA: 0x00211460 File Offset: 0x0020F660
	private GameObject CreateHeader(ReportManager.ReportGroup reportGroup)
	{
		GameObject gameObject = null;
		this.lineItems.TryGetValue(reportGroup.stringKey, out gameObject);
		if (gameObject == null)
		{
			gameObject = Util.KInstantiateUI(this.lineItemHeader, this.contentFolder, true);
			gameObject.name = "LineItemHeader" + this.lineItems.Count.ToString();
			this.lineItems[reportGroup.stringKey] = gameObject;
		}
		gameObject.SetActive(true);
		gameObject.GetComponent<ReportScreenHeader>().SetMainEntry(reportGroup);
		return gameObject;
	}

	// Token: 0x06005B22 RID: 23330 RVA: 0x002114E8 File Offset: 0x0020F6E8
	private GameObject CreateOrUpdateLine(ReportManager.ReportEntry entry, ReportManager.ReportGroup reportGroup, bool is_line_active)
	{
		GameObject gameObject = null;
		this.lineItems.TryGetValue(reportGroup.stringKey, out gameObject);
		if (!is_line_active)
		{
			if (gameObject != null && gameObject.activeSelf)
			{
				gameObject.SetActive(false);
			}
		}
		else
		{
			if (gameObject == null)
			{
				gameObject = Util.KInstantiateUI(this.lineItem, this.contentFolder, true);
				gameObject.name = "LineItem" + this.lineItems.Count.ToString();
				this.lineItems[reportGroup.stringKey] = gameObject;
			}
			gameObject.SetActive(true);
			gameObject.GetComponent<ReportScreenEntry>().SetMainEntry(entry, reportGroup);
		}
		return gameObject;
	}

	// Token: 0x06005B23 RID: 23331 RVA: 0x0021158E File Offset: 0x0020F78E
	private void OnClickClose()
	{
		base.PlaySound3D(GlobalAssets.GetSound("HUD_Click_Close", false));
		this.Show(false);
	}

	// Token: 0x04003DC1 RID: 15809
	[SerializeField]
	private LocText title;

	// Token: 0x04003DC2 RID: 15810
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003DC3 RID: 15811
	[SerializeField]
	private KButton prevButton;

	// Token: 0x04003DC4 RID: 15812
	[SerializeField]
	private KButton nextButton;

	// Token: 0x04003DC5 RID: 15813
	[SerializeField]
	private KButton summaryButton;

	// Token: 0x04003DC6 RID: 15814
	[SerializeField]
	private GameObject lineItem;

	// Token: 0x04003DC7 RID: 15815
	[SerializeField]
	private GameObject lineItemSpacer;

	// Token: 0x04003DC8 RID: 15816
	[SerializeField]
	private GameObject lineItemHeader;

	// Token: 0x04003DC9 RID: 15817
	[SerializeField]
	private GameObject contentFolder;

	// Token: 0x04003DCA RID: 15818
	private Dictionary<string, GameObject> lineItems = new Dictionary<string, GameObject>();

	// Token: 0x04003DCB RID: 15819
	private ReportManager.DailyReport currentReport;
}
