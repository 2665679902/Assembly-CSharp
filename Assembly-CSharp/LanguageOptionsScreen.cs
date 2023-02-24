using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using KMod;
using Steamworks;
using STRINGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AE7 RID: 2791
public class LanguageOptionsScreen : KModalScreen, SteamUGCService.IClient
{
	// Token: 0x0600556D RID: 21869 RVA: 0x001EE2EC File Offset: 0x001EC4EC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.dismissButton.onClick += this.Deactivate;
		this.dismissButton.GetComponent<HierarchyReferences>().GetReference<LocText>("Title").SetText(UI.FRONTEND.OPTIONS_SCREEN.BACK);
		this.closeButton.onClick += this.Deactivate;
		this.workshopButton.onClick += delegate
		{
			this.OnClickOpenWorkshop();
		};
		this.uninstallButton.onClick += delegate
		{
			this.OnClickUninstall();
		};
		this.uninstallButton.gameObject.SetActive(false);
		this.RebuildScreen();
	}

	// Token: 0x0600556E RID: 21870 RVA: 0x001EE398 File Offset: 0x001EC598
	private void RebuildScreen()
	{
		foreach (GameObject gameObject in this.buttons)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		this.buttons.Clear();
		this.uninstallButton.isInteractable = KPlayerPrefs.GetString(Localization.SELECTED_LANGUAGE_TYPE_KEY, Localization.SelectedLanguageType.None.ToString()) != Localization.SelectedLanguageType.None.ToString();
		this.RebuildPreinstalledButtons();
		this.RebuildUGCButtons();
	}

	// Token: 0x0600556F RID: 21871 RVA: 0x001EE438 File Offset: 0x001EC638
	private void RebuildPreinstalledButtons()
	{
		using (List<string>.Enumerator enumerator = Localization.PreinstalledLanguages.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string code = enumerator.Current;
				if (!(code != Localization.DEFAULT_LANGUAGE_CODE) || File.Exists(Localization.GetPreinstalledLocalizationFilePath(code)))
				{
					GameObject gameObject = Util.KInstantiateUI(this.languageButtonPrefab, this.preinstalledLanguagesContainer, false);
					gameObject.name = code + "_button";
					HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
					LocText reference = component.GetReference<LocText>("Title");
					reference.text = Localization.GetPreinstalledLocalizationTitle(code);
					reference.enabled = false;
					reference.enabled = true;
					Texture2D preinstalledLocalizationImage = Localization.GetPreinstalledLocalizationImage(code);
					if (preinstalledLocalizationImage != null)
					{
						component.GetReference<Image>("Image").sprite = Sprite.Create(preinstalledLocalizationImage, new Rect(Vector2.zero, new Vector2((float)preinstalledLocalizationImage.width, (float)preinstalledLocalizationImage.height)), Vector2.one * 0.5f);
					}
					gameObject.GetComponent<KButton>().onClick += delegate
					{
						this.ConfirmLanguagePreinstalledOrMod((code != Localization.DEFAULT_LANGUAGE_CODE) ? code : string.Empty, null);
					};
					this.buttons.Add(gameObject);
				}
			}
		}
	}

	// Token: 0x06005570 RID: 21872 RVA: 0x001EE5A8 File Offset: 0x001EC7A8
	protected override void OnActivate()
	{
		base.OnActivate();
		Global.Instance.modManager.Sanitize(base.gameObject);
		if (SteamUGCService.Instance != null)
		{
			SteamUGCService.Instance.AddClient(this);
		}
	}

	// Token: 0x06005571 RID: 21873 RVA: 0x001EE5DD File Offset: 0x001EC7DD
	protected override void OnDeactivate()
	{
		base.OnDeactivate();
		if (SteamUGCService.Instance != null)
		{
			SteamUGCService.Instance.RemoveClient(this);
		}
	}

	// Token: 0x06005572 RID: 21874 RVA: 0x001EE600 File Offset: 0x001EC800
	private void ConfirmLanguageChoiceDialog(string[] lines, bool is_template, System.Action install_language)
	{
		Localization.Locale locale = Localization.GetLocale(lines);
		Dictionary<string, string> translated_strings = Localization.ExtractTranslatedStrings(lines, is_template);
		TMP_FontAsset font = Localization.GetFont(locale.FontName);
		ConfirmDialogScreen screen = this.GetConfirmDialog();
		HashSet<MemberInfo> excluded_members = new HashSet<MemberInfo>(typeof(ConfirmDialogScreen).GetMember("cancelButton", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy));
		Localization.SetFont<ConfirmDialogScreen>(screen, font, locale.IsRightToLeft, excluded_members);
		Func<LocString, string> func = delegate(LocString loc_string)
		{
			string text2;
			if (!translated_strings.TryGetValue(loc_string.key.String, out text2))
			{
				return loc_string;
			}
			return text2;
		};
		ConfirmDialogScreen screen2 = screen;
		string text = func(UI.CONFIRMDIALOG.DIALOG_HEADER);
		screen2.PopupConfirmDialog(func(UI.FRONTEND.TRANSLATIONS_SCREEN.PLEASE_REBOOT), delegate
		{
			LanguageOptionsScreen.CleanUpSavedLanguageMod();
			install_language();
			App.instance.Restart();
		}, delegate
		{
			Localization.SetFont<ConfirmDialogScreen>(screen, Localization.FontAsset, Localization.IsRightToLeft, excluded_members);
		}, null, null, text, func(UI.FRONTEND.TRANSLATIONS_SCREEN.RESTART), UI.FRONTEND.TRANSLATIONS_SCREEN.CANCEL, null);
	}

	// Token: 0x06005573 RID: 21875 RVA: 0x001EE6E2 File Offset: 0x001EC8E2
	private void ConfirmPreinstalledLanguage(string selected_preinstalled_translation)
	{
		Localization.GetSelectedLanguageType();
	}

	// Token: 0x06005574 RID: 21876 RVA: 0x001EE6EC File Offset: 0x001EC8EC
	private void ConfirmLanguagePreinstalledOrMod(string selected_preinstalled_translation, string mod_id)
	{
		Localization.SelectedLanguageType selectedLanguageType = Localization.GetSelectedLanguageType();
		if (mod_id != null)
		{
			if (selectedLanguageType == Localization.SelectedLanguageType.UGC && mod_id == this.currentLanguageModId)
			{
				this.Deactivate();
				return;
			}
			string[] languageLinesForMod = LanguageOptionsScreen.GetLanguageLinesForMod(mod_id);
			this.ConfirmLanguageChoiceDialog(languageLinesForMod, false, delegate
			{
				LanguageOptionsScreen.SetCurrentLanguage(mod_id);
			});
			return;
		}
		else if (!string.IsNullOrEmpty(selected_preinstalled_translation))
		{
			string currentLanguageCode = Localization.GetCurrentLanguageCode();
			if (selectedLanguageType == Localization.SelectedLanguageType.Preinstalled && currentLanguageCode == selected_preinstalled_translation)
			{
				this.Deactivate();
				return;
			}
			string[] array = File.ReadAllLines(Localization.GetPreinstalledLocalizationFilePath(selected_preinstalled_translation), Encoding.UTF8);
			this.ConfirmLanguageChoiceDialog(array, false, delegate
			{
				Localization.LoadPreinstalledTranslation(selected_preinstalled_translation);
			});
			return;
		}
		else
		{
			if (selectedLanguageType == Localization.SelectedLanguageType.None)
			{
				this.Deactivate();
				return;
			}
			string[] array2 = File.ReadAllLines(Localization.GetDefaultLocalizationFilePath(), Encoding.UTF8);
			this.ConfirmLanguageChoiceDialog(array2, true, delegate
			{
				Localization.ClearLanguage();
			});
			return;
		}
	}

	// Token: 0x06005575 RID: 21877 RVA: 0x001EE7F6 File Offset: 0x001EC9F6
	private ConfirmDialogScreen GetConfirmDialog()
	{
		KScreen component = KScreenManager.AddChild(base.transform.parent.gameObject, ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<KScreen>();
		component.Activate();
		return component.GetComponent<ConfirmDialogScreen>();
	}

	// Token: 0x06005576 RID: 21878 RVA: 0x001EE82C File Offset: 0x001ECA2C
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.Consumed)
		{
			return;
		}
		if (e.TryConsume(global::Action.MouseRight))
		{
			this.Deactivate();
		}
		base.OnKeyDown(e);
	}

	// Token: 0x06005577 RID: 21879 RVA: 0x001EE850 File Offset: 0x001ECA50
	private void RebuildUGCButtons()
	{
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if ((mod.available_content & Content.Translation) != (Content)0 && mod.status == Mod.Status.Installed)
			{
				GameObject gameObject = Util.KInstantiateUI(this.languageButtonPrefab, this.ugcLanguagesContainer, false);
				gameObject.name = mod.title + "_button";
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				TMP_FontAsset font = Localization.GetFont(Localization.GetFontName(LanguageOptionsScreen.GetLanguageLinesForMod(mod)));
				LocText reference = component.GetReference<LocText>("Title");
				reference.SetText(string.Format(UI.FRONTEND.TRANSLATIONS_SCREEN.UGC_MOD_TITLE_FORMAT, mod.title));
				reference.font = font;
				Texture2D previewImage = mod.GetPreviewImage();
				if (previewImage != null)
				{
					component.GetReference<Image>("Image").sprite = Sprite.Create(previewImage, new Rect(Vector2.zero, new Vector2((float)previewImage.width, (float)previewImage.height)), Vector2.one * 0.5f);
				}
				string mod_id = mod.label.id;
				gameObject.GetComponent<KButton>().onClick += delegate
				{
					this.ConfirmLanguagePreinstalledOrMod(string.Empty, mod_id);
				};
				this.buttons.Add(gameObject);
			}
		}
	}

	// Token: 0x06005578 RID: 21880 RVA: 0x001EE9DC File Offset: 0x001ECBDC
	private void Uninstall()
	{
		this.GetConfirmDialog().PopupConfirmDialog(UI.FRONTEND.TRANSLATIONS_SCREEN.ARE_YOU_SURE, delegate
		{
			Localization.ClearLanguage();
			this.GetConfirmDialog().PopupConfirmDialog(UI.FRONTEND.TRANSLATIONS_SCREEN.PLEASE_REBOOT, new System.Action(App.instance.Restart), new System.Action(this.Deactivate), null, null, null, null, null, null);
		}, delegate
		{
		}, null, null, null, null, null, null);
	}

	// Token: 0x06005579 RID: 21881 RVA: 0x001EEA2F File Offset: 0x001ECC2F
	private void OnClickUninstall()
	{
		this.Uninstall();
	}

	// Token: 0x0600557A RID: 21882 RVA: 0x001EEA37 File Offset: 0x001ECC37
	private void OnClickOpenWorkshop()
	{
		App.OpenWebURL("http://steamcommunity.com/workshop/browse/?appid=457140&requiredtags[]=language");
	}

	// Token: 0x0600557B RID: 21883 RVA: 0x001EEA44 File Offset: 0x001ECC44
	public void UpdateMods(IEnumerable<PublishedFileId_t> added, IEnumerable<PublishedFileId_t> updated, IEnumerable<PublishedFileId_t> removed, IEnumerable<SteamUGCService.Mod> loaded_previews)
	{
		string savedLanguageMod = LanguageOptionsScreen.GetSavedLanguageMod();
		ulong num;
		if (ulong.TryParse(savedLanguageMod, out num))
		{
			PublishedFileId_t publishedFileId_t = (PublishedFileId_t)num;
			if (removed.Contains(publishedFileId_t))
			{
				global::Debug.Log("Unsubscribe detected for currently installed language mod [" + savedLanguageMod + "]");
				this.GetConfirmDialog().PopupConfirmDialog(UI.FRONTEND.TRANSLATIONS_SCREEN.PLEASE_REBOOT, delegate
				{
					Localization.ClearLanguage();
					App.instance.Restart();
				}, null, null, null, null, UI.FRONTEND.TRANSLATIONS_SCREEN.RESTART, null, null);
			}
			if (updated.Contains(publishedFileId_t))
			{
				global::Debug.Log("Download complete for currently installed language [" + savedLanguageMod + "] updating in background. Changes will happen next restart.");
			}
		}
		this.RebuildScreen();
	}

	// Token: 0x0600557C RID: 21884 RVA: 0x001EEAF1 File Offset: 0x001ECCF1
	public static string GetSavedLanguageMod()
	{
		return KPlayerPrefs.GetString("InstalledLanguage");
	}

	// Token: 0x0600557D RID: 21885 RVA: 0x001EEAFD File Offset: 0x001ECCFD
	public static void SetSavedLanguageMod(string mod_id)
	{
		KPlayerPrefs.SetString("InstalledLanguage", mod_id);
	}

	// Token: 0x0600557E RID: 21886 RVA: 0x001EEB0A File Offset: 0x001ECD0A
	public static void CleanUpSavedLanguageMod()
	{
		KPlayerPrefs.SetString("InstalledLanguage", null);
	}

	// Token: 0x1700063C RID: 1596
	// (get) Token: 0x0600557F RID: 21887 RVA: 0x001EEB17 File Offset: 0x001ECD17
	// (set) Token: 0x06005580 RID: 21888 RVA: 0x001EEB1F File Offset: 0x001ECD1F
	public string currentLanguageModId
	{
		get
		{
			return this._currentLanguageModId;
		}
		private set
		{
			this._currentLanguageModId = value;
			LanguageOptionsScreen.SetSavedLanguageMod(this._currentLanguageModId);
		}
	}

	// Token: 0x06005581 RID: 21889 RVA: 0x001EEB33 File Offset: 0x001ECD33
	public static bool SetCurrentLanguage(string mod_id)
	{
		LanguageOptionsScreen.CleanUpSavedLanguageMod();
		if (LanguageOptionsScreen.LoadTranslation(mod_id))
		{
			LanguageOptionsScreen.SetSavedLanguageMod(mod_id);
			return true;
		}
		return false;
	}

	// Token: 0x06005582 RID: 21890 RVA: 0x001EEB4C File Offset: 0x001ECD4C
	public static bool HasInstalledLanguage()
	{
		string currentModId = LanguageOptionsScreen.GetSavedLanguageMod();
		if (currentModId == null)
		{
			return false;
		}
		if (Global.Instance.modManager.mods.Find((Mod m) => m.label.id == currentModId) == null)
		{
			LanguageOptionsScreen.CleanUpSavedLanguageMod();
			return false;
		}
		return true;
	}

	// Token: 0x06005583 RID: 21891 RVA: 0x001EEBA0 File Offset: 0x001ECDA0
	public static string GetInstalledLanguageCode()
	{
		string text = "";
		string[] languageLinesForMod = LanguageOptionsScreen.GetLanguageLinesForMod(LanguageOptionsScreen.GetSavedLanguageMod());
		if (languageLinesForMod != null)
		{
			Localization.Locale locale = Localization.GetLocale(languageLinesForMod);
			if (locale != null)
			{
				text = locale.Code;
			}
		}
		return text;
	}

	// Token: 0x06005584 RID: 21892 RVA: 0x001EEBD4 File Offset: 0x001ECDD4
	private static bool LoadTranslation(string mod_id)
	{
		Mod mod = Global.Instance.modManager.mods.Find((Mod m) => m.label.id == mod_id);
		if (mod == null)
		{
			global::Debug.LogWarning("Tried loading a translation from a non-existent mod id: " + mod_id);
			return false;
		}
		string languageFilename = LanguageOptionsScreen.GetLanguageFilename(mod);
		return languageFilename != null && Localization.LoadLocalTranslationFile(Localization.SelectedLanguageType.UGC, languageFilename);
	}

	// Token: 0x06005585 RID: 21893 RVA: 0x001EEC3C File Offset: 0x001ECE3C
	private static string GetLanguageFilename(Mod mod)
	{
		global::Debug.Assert(mod.content_source.GetType() == typeof(KMod.Directory), "Can only load translations from extracted mods.");
		string text = Path.Combine(mod.ContentPath, "strings.po");
		if (!File.Exists(text))
		{
			global::Debug.LogWarning("GetLanguagFile: " + text + " missing for mod " + mod.label.title);
			return null;
		}
		return text;
	}

	// Token: 0x06005586 RID: 21894 RVA: 0x001EECAC File Offset: 0x001ECEAC
	private static string[] GetLanguageLinesForMod(string mod_id)
	{
		return LanguageOptionsScreen.GetLanguageLinesForMod(Global.Instance.modManager.mods.Find((Mod m) => m.label.id == mod_id));
	}

	// Token: 0x06005587 RID: 21895 RVA: 0x001EECEC File Offset: 0x001ECEEC
	private static string[] GetLanguageLinesForMod(Mod mod)
	{
		string languageFilename = LanguageOptionsScreen.GetLanguageFilename(mod);
		if (languageFilename == null)
		{
			return null;
		}
		string[] array = File.ReadAllLines(languageFilename, Encoding.UTF8);
		if (array == null || array.Length == 0)
		{
			global::Debug.LogWarning("Couldn't find any strings in the translation mod " + mod.label.title);
			return null;
		}
		return array;
	}

	// Token: 0x04003A00 RID: 14848
	private static readonly string[] poFile = new string[] { "strings.po" };

	// Token: 0x04003A01 RID: 14849
	public const string KPLAYER_PREFS_LANGUAGE_KEY = "InstalledLanguage";

	// Token: 0x04003A02 RID: 14850
	public const string TAG_LANGUAGE = "language";

	// Token: 0x04003A03 RID: 14851
	public KButton textButton;

	// Token: 0x04003A04 RID: 14852
	public KButton dismissButton;

	// Token: 0x04003A05 RID: 14853
	public KButton closeButton;

	// Token: 0x04003A06 RID: 14854
	public KButton workshopButton;

	// Token: 0x04003A07 RID: 14855
	public KButton uninstallButton;

	// Token: 0x04003A08 RID: 14856
	[Space]
	public GameObject languageButtonPrefab;

	// Token: 0x04003A09 RID: 14857
	public GameObject preinstalledLanguagesTitle;

	// Token: 0x04003A0A RID: 14858
	public GameObject preinstalledLanguagesContainer;

	// Token: 0x04003A0B RID: 14859
	public GameObject ugcLanguagesTitle;

	// Token: 0x04003A0C RID: 14860
	public GameObject ugcLanguagesContainer;

	// Token: 0x04003A0D RID: 14861
	private List<GameObject> buttons = new List<GameObject>();

	// Token: 0x04003A0E RID: 14862
	private string _currentLanguageModId;

	// Token: 0x04003A0F RID: 14863
	private System.DateTime currentLastModified;
}
