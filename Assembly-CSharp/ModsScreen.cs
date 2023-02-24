using System;
using System.Collections.Generic;
using System.Linq;
using KMod;
using STRINGS;
using UnityEngine;

// Token: 0x02000A0E RID: 2574
public class ModsScreen : KModalScreen
{
	// Token: 0x06004DA0 RID: 19872 RVA: 0x001B6208 File Offset: 0x001B4408
	protected override void OnActivate()
	{
		base.OnActivate();
		this.closeButtonTitle.onClick += this.Exit;
		this.closeButton.onClick += this.Exit;
		System.Action action = delegate
		{
			App.OpenWebURL("http://steamcommunity.com/workshop/browse/?appid=457140");
		};
		this.workshopButton.onClick += action;
		this.UpdateToggleAllButton();
		this.toggleAllButton.onClick += this.OnToggleAllClicked;
		Global.Instance.modManager.Sanitize(base.gameObject);
		this.mod_footprint.Clear();
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if (mod.IsEnabledForActiveDlc())
			{
				this.mod_footprint.Add(mod.label);
				if ((mod.loaded_content & (Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation)) == (mod.available_content & (Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation)))
				{
					mod.Uncrash();
				}
			}
		}
		this.BuildDisplay();
		Manager modManager = Global.Instance.modManager;
		modManager.on_update = (Manager.OnUpdate)Delegate.Combine(modManager.on_update, new Manager.OnUpdate(this.RebuildDisplay));
	}

	// Token: 0x06004DA1 RID: 19873 RVA: 0x001B6360 File Offset: 0x001B4560
	protected override void OnDeactivate()
	{
		Manager modManager = Global.Instance.modManager;
		modManager.on_update = (Manager.OnUpdate)Delegate.Remove(modManager.on_update, new Manager.OnUpdate(this.RebuildDisplay));
		base.OnDeactivate();
	}

	// Token: 0x06004DA2 RID: 19874 RVA: 0x001B6394 File Offset: 0x001B4594
	private void Exit()
	{
		Global.Instance.modManager.Save();
		if (!Global.Instance.modManager.MatchFootprint(this.mod_footprint, Content.LayerableFiles | Content.Strings | Content.DLL | Content.Translation | Content.Animation))
		{
			Global.Instance.modManager.RestartDialog(UI.FRONTEND.MOD_DIALOGS.MODS_SCREEN_CHANGES.TITLE, UI.FRONTEND.MOD_DIALOGS.MODS_SCREEN_CHANGES.MESSAGE, new System.Action(this.Deactivate), true, base.gameObject, null);
		}
		else
		{
			this.Deactivate();
		}
		Global.Instance.modManager.events.Clear();
	}

	// Token: 0x06004DA3 RID: 19875 RVA: 0x001B641E File Offset: 0x001B461E
	private void RebuildDisplay(object change_source)
	{
		if (change_source != this)
		{
			this.BuildDisplay();
		}
	}

	// Token: 0x06004DA4 RID: 19876 RVA: 0x001B642A File Offset: 0x001B462A
	private bool ShouldDisplayMod(Mod mod)
	{
		return mod.status != Mod.Status.NotInstalled && mod.status != Mod.Status.UninstallPending && !mod.HasOnlyTranslationContent();
	}

	// Token: 0x06004DA5 RID: 19877 RVA: 0x001B6448 File Offset: 0x001B4648
	private void BuildDisplay()
	{
		foreach (ModsScreen.DisplayedMod displayedMod in this.displayedMods)
		{
			if (displayedMod.rect_transform != null)
			{
				UnityEngine.Object.Destroy(displayedMod.rect_transform.gameObject);
			}
		}
		this.displayedMods.Clear();
		ModsScreen.ModOrderingDragListener modOrderingDragListener = new ModsScreen.ModOrderingDragListener(this, this.displayedMods);
		for (int num = 0; num != Global.Instance.modManager.mods.Count; num++)
		{
			Mod mod = Global.Instance.modManager.mods[num];
			if (this.ShouldDisplayMod(mod))
			{
				HierarchyReferences hierarchyReferences = Util.KInstantiateUI<HierarchyReferences>(this.entryPrefab, this.entryParent.gameObject, false);
				this.displayedMods.Add(new ModsScreen.DisplayedMod
				{
					rect_transform = hierarchyReferences.gameObject.GetComponent<RectTransform>(),
					mod_index = num
				});
				hierarchyReferences.GetComponent<DragMe>().listener = modOrderingDragListener;
				LocText reference = hierarchyReferences.GetReference<LocText>("Title");
				string text = mod.title;
				hierarchyReferences.name = mod.title;
				if (mod.available_content == (Content)0)
				{
					switch (mod.contentCompatability)
					{
					case ModContentCompatability.NoContent:
						text += UI.FRONTEND.MODS.CONTENT_FAILURE.NO_CONTENT;
						goto IL_1AD;
					case ModContentCompatability.OldAPI:
						text += UI.FRONTEND.MODS.CONTENT_FAILURE.OLD_API;
						goto IL_1AD;
					}
					text += UI.FRONTEND.MODS.CONTENT_FAILURE.DISABLED_CONTENT.Replace("{Content}", ModsScreen.GetDlcName(DlcManager.GetHighestActiveDlcId()));
				}
				IL_1AD:
				reference.text = text;
				LocText reference2 = hierarchyReferences.GetReference<LocText>("Version");
				if (mod.packagedModInfo != null && mod.packagedModInfo.version != null && mod.packagedModInfo.version.Length > 0)
				{
					string text2 = mod.packagedModInfo.version;
					if (text2.StartsWith("V"))
					{
						text2 = "v" + text2.Substring(1, text2.Length - 1);
					}
					else if (!text2.StartsWith("v"))
					{
						text2 = "v" + text2;
					}
					reference2.text = text2;
					reference2.gameObject.SetActive(true);
				}
				else
				{
					reference2.gameObject.SetActive(false);
				}
				hierarchyReferences.GetReference<ToolTip>("Description").toolTip = mod.description;
				if (mod.crash_count != 0)
				{
					reference.color = Color.Lerp(Color.white, Color.red, (float)mod.crash_count / 3f);
				}
				KButton reference3 = hierarchyReferences.GetReference<KButton>("ManageButton");
				reference3.GetComponentInChildren<LocText>().text = (mod.IsLocal ? UI.FRONTEND.MODS.MANAGE_LOCAL : UI.FRONTEND.MODS.MANAGE);
				reference3.isInteractable = mod.is_managed;
				if (reference3.isInteractable)
				{
					reference3.GetComponent<ToolTip>().toolTip = mod.manage_tooltip;
					reference3.onClick += mod.on_managed;
				}
				KImage reference4 = hierarchyReferences.GetReference<KImage>("BG");
				MultiToggle toggle = hierarchyReferences.GetReference<MultiToggle>("EnabledToggle");
				toggle.ChangeState(mod.IsEnabledForActiveDlc() ? 1 : 0);
				if (mod.available_content != (Content)0)
				{
					reference4.defaultState = KImage.ColorSelector.Inactive;
					reference4.ColorState = KImage.ColorSelector.Inactive;
					MultiToggle toggle2 = toggle;
					toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
					{
						this.OnToggleClicked(toggle, mod.label);
					}));
					toggle.GetComponent<ToolTip>().OnToolTip = () => mod.IsEnabledForActiveDlc() ? UI.FRONTEND.MODS.TOOLTIPS.ENABLED : UI.FRONTEND.MODS.TOOLTIPS.DISABLED;
				}
				else
				{
					reference4.defaultState = KImage.ColorSelector.Disabled;
					reference4.ColorState = KImage.ColorSelector.Disabled;
				}
				hierarchyReferences.gameObject.SetActive(true);
			}
		}
		foreach (ModsScreen.DisplayedMod displayedMod2 in this.displayedMods)
		{
			displayedMod2.rect_transform.gameObject.SetActive(true);
		}
		int count = this.displayedMods.Count;
	}

	// Token: 0x06004DA6 RID: 19878 RVA: 0x001B68FC File Offset: 0x001B4AFC
	private static string GetDlcName(string dlcId)
	{
		if (dlcId != null)
		{
			if (dlcId == "EXPANSION1_ID")
			{
				return UI.DLC1.NAME_ITAL;
			}
			if (dlcId != null && dlcId.Length != 0)
			{
			}
		}
		return UI.VANILLA.NAME_ITAL;
	}

	// Token: 0x06004DA7 RID: 19879 RVA: 0x001B6930 File Offset: 0x001B4B30
	private void OnToggleClicked(MultiToggle toggle, Label mod)
	{
		Manager modManager = Global.Instance.modManager;
		bool flag = modManager.IsModEnabled(mod);
		flag = !flag;
		toggle.ChangeState(flag ? 1 : 0);
		modManager.EnableMod(mod, flag, this);
		this.UpdateToggleAllButton();
	}

	// Token: 0x06004DA8 RID: 19880 RVA: 0x001B6970 File Offset: 0x001B4B70
	private bool AreAnyModsDisabled()
	{
		return Global.Instance.modManager.mods.Any((Mod mod) => !mod.IsEmpty() && !mod.IsEnabledForActiveDlc() && this.ShouldDisplayMod(mod));
	}

	// Token: 0x06004DA9 RID: 19881 RVA: 0x001B6992 File Offset: 0x001B4B92
	private void UpdateToggleAllButton()
	{
		this.toggleAllButton.GetComponentInChildren<LocText>().text = (this.AreAnyModsDisabled() ? UI.FRONTEND.MODS.ENABLE_ALL : UI.FRONTEND.MODS.DISABLE_ALL);
	}

	// Token: 0x06004DAA RID: 19882 RVA: 0x001B69C0 File Offset: 0x001B4BC0
	private void OnToggleAllClicked()
	{
		bool flag = this.AreAnyModsDisabled();
		Manager modManager = Global.Instance.modManager;
		foreach (Mod mod in modManager.mods)
		{
			if (this.ShouldDisplayMod(mod))
			{
				modManager.EnableMod(mod.label, flag, this);
			}
		}
		this.BuildDisplay();
		this.UpdateToggleAllButton();
	}

	// Token: 0x0400332A RID: 13098
	[SerializeField]
	private KButton closeButtonTitle;

	// Token: 0x0400332B RID: 13099
	[SerializeField]
	private KButton closeButton;

	// Token: 0x0400332C RID: 13100
	[SerializeField]
	private KButton toggleAllButton;

	// Token: 0x0400332D RID: 13101
	[SerializeField]
	private KButton workshopButton;

	// Token: 0x0400332E RID: 13102
	[SerializeField]
	private GameObject entryPrefab;

	// Token: 0x0400332F RID: 13103
	[SerializeField]
	private Transform entryParent;

	// Token: 0x04003330 RID: 13104
	private List<ModsScreen.DisplayedMod> displayedMods = new List<ModsScreen.DisplayedMod>();

	// Token: 0x04003331 RID: 13105
	private List<Label> mod_footprint = new List<Label>();

	// Token: 0x02001824 RID: 6180
	private struct DisplayedMod
	{
		// Token: 0x04006F45 RID: 28485
		public RectTransform rect_transform;

		// Token: 0x04006F46 RID: 28486
		public int mod_index;
	}

	// Token: 0x02001825 RID: 6181
	private class ModOrderingDragListener : DragMe.IDragListener
	{
		// Token: 0x06008CF8 RID: 36088 RVA: 0x003046C7 File Offset: 0x003028C7
		public ModOrderingDragListener(ModsScreen screen, List<ModsScreen.DisplayedMod> mods)
		{
			this.screen = screen;
			this.mods = mods;
		}

		// Token: 0x06008CF9 RID: 36089 RVA: 0x003046E4 File Offset: 0x003028E4
		public void OnBeginDrag(Vector2 pos)
		{
			this.startDragIdx = this.GetDragIdx(pos, false);
		}

		// Token: 0x06008CFA RID: 36090 RVA: 0x003046F4 File Offset: 0x003028F4
		public void OnEndDrag(Vector2 pos)
		{
			if (this.startDragIdx < 0)
			{
				return;
			}
			int dragIdx = this.GetDragIdx(pos, true);
			if (dragIdx != this.startDragIdx)
			{
				int mod_index = this.mods[this.startDragIdx].mod_index;
				int num = ((0 <= dragIdx && dragIdx < this.mods.Count) ? this.mods[dragIdx].mod_index : (-1));
				Global.Instance.modManager.Reinsert(mod_index, num, dragIdx >= this.mods.Count, this);
				this.screen.BuildDisplay();
			}
		}

		// Token: 0x06008CFB RID: 36091 RVA: 0x0030478C File Offset: 0x0030298C
		private int GetDragIdx(Vector2 pos, bool halfPosition)
		{
			int num = -1;
			for (int i = 0; i < this.mods.Count; i++)
			{
				Vector2 vector;
				RectTransformUtility.ScreenPointToLocalPointInRectangle(this.mods[i].rect_transform, pos, null, out vector);
				if (!halfPosition)
				{
					vector += this.mods[i].rect_transform.rect.min;
				}
				if (vector.y >= 0f)
				{
					break;
				}
				num = i;
			}
			return num;
		}

		// Token: 0x04006F47 RID: 28487
		private List<ModsScreen.DisplayedMod> mods;

		// Token: 0x04006F48 RID: 28488
		private ModsScreen screen;

		// Token: 0x04006F49 RID: 28489
		private int startDragIdx = -1;
	}
}
