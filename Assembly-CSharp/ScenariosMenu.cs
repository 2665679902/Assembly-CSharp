using System;
using System.Collections.Generic;
using System.IO;
using Steamworks;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B79 RID: 2937
public class ScenariosMenu : KModalScreen, SteamUGCService.IClient
{
	// Token: 0x06005C54 RID: 23636 RVA: 0x0021C654 File Offset: 0x0021A854
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.dismissButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.dismissButton.GetComponent<HierarchyReferences>().GetReference<LocText>("Title").SetText(UI.FRONTEND.OPTIONS_SCREEN.BACK);
		this.closeButton.onClick += delegate
		{
			this.Deactivate();
		};
		this.workshopButton.onClick += delegate
		{
			this.OnClickOpenWorkshop();
		};
		this.RebuildScreen();
	}

	// Token: 0x06005C55 RID: 23637 RVA: 0x0021C6D8 File Offset: 0x0021A8D8
	private void RebuildScreen()
	{
		foreach (GameObject gameObject in this.buttons)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		this.buttons.Clear();
		this.RebuildUGCButtons();
	}

	// Token: 0x06005C56 RID: 23638 RVA: 0x0021C73C File Offset: 0x0021A93C
	private void RebuildUGCButtons()
	{
		ListPool<SteamUGCService.Mod, ScenariosMenu>.PooledList pooledList = ListPool<SteamUGCService.Mod, ScenariosMenu>.Allocate();
		bool flag = pooledList.Count > 0;
		this.noScenariosText.gameObject.SetActive(!flag);
		this.contentRoot.gameObject.SetActive(flag);
		bool flag2 = true;
		if (pooledList.Count != 0)
		{
			for (int i = 0; i < pooledList.Count; i++)
			{
				GameObject gameObject = Util.KInstantiateUI(this.ugcButtonPrefab, this.ugcContainer, false);
				gameObject.name = pooledList[i].title + "_button";
				gameObject.gameObject.SetActive(true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("Title").SetText(pooledList[i].title);
				Texture2D previewImage = pooledList[i].previewImage;
				if (previewImage != null)
				{
					component.GetReference<Image>("Image").sprite = Sprite.Create(previewImage, new Rect(Vector2.zero, new Vector2((float)previewImage.width, (float)previewImage.height)), Vector2.one * 0.5f);
				}
				KButton component2 = gameObject.GetComponent<KButton>();
				int num = i;
				PublishedFileId_t item = pooledList[num].fileId;
				component2.onClick += delegate
				{
					this.ShowDetails(item);
				};
				component2.onDoubleClick += delegate
				{
					this.LoadScenario(item);
				};
				this.buttons.Add(gameObject);
				if (item == this.activeItem)
				{
					flag2 = false;
				}
			}
		}
		if (flag2)
		{
			this.HideDetails();
		}
		pooledList.Recycle();
	}

	// Token: 0x06005C57 RID: 23639 RVA: 0x0021C8E8 File Offset: 0x0021AAE8
	private void LoadScenario(PublishedFileId_t item)
	{
		ulong num;
		string text;
		uint num2;
		SteamUGC.GetItemInstallInfo(item, out num, out text, 1024U, out num2);
		DebugUtil.LogArgs(new object[] { "LoadScenario", text, num, num2 });
		System.DateTime dateTime;
		byte[] bytesFromZip = SteamUGCService.GetBytesFromZip(item, new string[] { ".sav" }, out dateTime, false);
		string text2 = Path.Combine(SaveLoader.GetSavePrefix(), "scenario.sav");
		File.WriteAllBytes(text2, bytesFromZip);
		SaveLoader.SetActiveSaveFilePath(text2);
		Time.timeScale = 0f;
		App.LoadScene("backend");
	}

	// Token: 0x06005C58 RID: 23640 RVA: 0x0021C979 File Offset: 0x0021AB79
	private ConfirmDialogScreen GetConfirmDialog()
	{
		KScreen component = KScreenManager.AddChild(base.transform.parent.gameObject, ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<KScreen>();
		component.Activate();
		return component.GetComponent<ConfirmDialogScreen>();
	}

	// Token: 0x06005C59 RID: 23641 RVA: 0x0021C9B0 File Offset: 0x0021ABB0
	private void ShowDetails(PublishedFileId_t item)
	{
		this.activeItem = item;
		SteamUGCService.Mod mod = SteamUGCService.Instance.FindMod(item);
		if (mod != null)
		{
			this.scenarioTitle.text = mod.title;
			this.scenarioDetails.text = mod.description;
		}
		this.loadScenarioButton.onClick += delegate
		{
			this.LoadScenario(item);
		};
		this.detailsRoot.gameObject.SetActive(true);
	}

	// Token: 0x06005C5A RID: 23642 RVA: 0x0021CA3B File Offset: 0x0021AC3B
	private void HideDetails()
	{
		this.detailsRoot.gameObject.SetActive(false);
	}

	// Token: 0x06005C5B RID: 23643 RVA: 0x0021CA4E File Offset: 0x0021AC4E
	protected override void OnActivate()
	{
		base.OnActivate();
		SteamUGCService.Instance.AddClient(this);
		this.HideDetails();
	}

	// Token: 0x06005C5C RID: 23644 RVA: 0x0021CA67 File Offset: 0x0021AC67
	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		SteamUGCService.Instance.RemoveClient(this);
	}

	// Token: 0x06005C5D RID: 23645 RVA: 0x0021CA7A File Offset: 0x0021AC7A
	private void OnClickOpenWorkshop()
	{
		App.OpenWebURL("http://steamcommunity.com/workshop/browse/?appid=457140&requiredtags[]=scenario");
	}

	// Token: 0x06005C5E RID: 23646 RVA: 0x0021CA86 File Offset: 0x0021AC86
	public void UpdateMods(IEnumerable<PublishedFileId_t> added, IEnumerable<PublishedFileId_t> updated, IEnumerable<PublishedFileId_t> removed, IEnumerable<SteamUGCService.Mod> loaded_previews)
	{
		this.RebuildScreen();
	}

	// Token: 0x04003F10 RID: 16144
	public const string TAG_SCENARIO = "scenario";

	// Token: 0x04003F11 RID: 16145
	public KButton textButton;

	// Token: 0x04003F12 RID: 16146
	public KButton dismissButton;

	// Token: 0x04003F13 RID: 16147
	public KButton closeButton;

	// Token: 0x04003F14 RID: 16148
	public KButton workshopButton;

	// Token: 0x04003F15 RID: 16149
	public KButton loadScenarioButton;

	// Token: 0x04003F16 RID: 16150
	[Space]
	public GameObject ugcContainer;

	// Token: 0x04003F17 RID: 16151
	public GameObject ugcButtonPrefab;

	// Token: 0x04003F18 RID: 16152
	public LocText noScenariosText;

	// Token: 0x04003F19 RID: 16153
	public RectTransform contentRoot;

	// Token: 0x04003F1A RID: 16154
	public RectTransform detailsRoot;

	// Token: 0x04003F1B RID: 16155
	public LocText scenarioTitle;

	// Token: 0x04003F1C RID: 16156
	public LocText scenarioDetails;

	// Token: 0x04003F1D RID: 16157
	private PublishedFileId_t activeItem;

	// Token: 0x04003F1E RID: 16158
	private List<GameObject> buttons = new List<GameObject>();
}
