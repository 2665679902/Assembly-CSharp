using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using ArabicSupport;
using Klei;
using KMod;
using Steamworks;
using STRINGS;
using TMPro;
using UnityEngine;

// Token: 0x020007F8 RID: 2040
public static class Localization
{
	// Token: 0x1700042E RID: 1070
	// (get) Token: 0x06003AE3 RID: 15075 RVA: 0x00146BFB File Offset: 0x00144DFB
	public static TMP_FontAsset FontAsset
	{
		get
		{
			return Localization.sFontAsset;
		}
	}

	// Token: 0x1700042F RID: 1071
	// (get) Token: 0x06003AE4 RID: 15076 RVA: 0x00146C02 File Offset: 0x00144E02
	public static bool IsRightToLeft
	{
		get
		{
			return Localization.sLocale != null && Localization.sLocale.IsRightToLeft;
		}
	}

	// Token: 0x06003AE5 RID: 15077 RVA: 0x00146C18 File Offset: 0x00144E18
	private static IEnumerable<Type> CollectLocStringTreeRoots(string locstrings_namespace, Assembly assembly)
	{
		return from type in assembly.GetTypes()
			where type.IsClass && type.Namespace == locstrings_namespace && !type.IsNested
			select type;
	}

	// Token: 0x06003AE6 RID: 15078 RVA: 0x00146C4C File Offset: 0x00144E4C
	private static Dictionary<string, object> MakeRuntimeLocStringTree(Type locstring_tree_root)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (FieldInfo fieldInfo in locstring_tree_root.GetFields())
		{
			if (!(fieldInfo.FieldType != typeof(LocString)))
			{
				LocString locString = (LocString)fieldInfo.GetValue(null);
				if (locString == null)
				{
					global::Debug.LogError("Tried to generate LocString for " + fieldInfo.Name + " but it is null so skipping");
				}
				else
				{
					dictionary[fieldInfo.Name] = locString.text;
				}
			}
		}
		foreach (Type type in locstring_tree_root.GetNestedTypes())
		{
			Dictionary<string, object> dictionary2 = Localization.MakeRuntimeLocStringTree(type);
			if (dictionary2.Count > 0)
			{
				dictionary[type.Name] = dictionary2;
			}
		}
		return dictionary;
	}

	// Token: 0x06003AE7 RID: 15079 RVA: 0x00146D10 File Offset: 0x00144F10
	private static void WriteStringsTemplate(string path, StreamWriter writer, Dictionary<string, object> runtime_locstring_tree)
	{
		List<string> list = new List<string>(runtime_locstring_tree.Keys);
		list.Sort();
		foreach (string text in list)
		{
			string text2 = path + "." + text;
			object obj = runtime_locstring_tree[text];
			if (obj.GetType() != typeof(string))
			{
				Localization.WriteStringsTemplate(text2, writer, obj as Dictionary<string, object>);
			}
			else
			{
				string text3 = obj as string;
				text3 = text3.Replace("\\", "\\\\");
				text3 = text3.Replace("\"", "\\\"");
				text3 = text3.Replace("\n", "\\n");
				if (text3.Contains("’") || text3.Contains("“") || text3.Contains("”") || text3.Contains("…"))
				{
					UnityEngine.Debug.LogError("Smart quotes or ellipses detected in " + text2);
				}
				text3 = text3.Replace("’", "'");
				text3 = text3.Replace("“", "\\\"");
				text3 = text3.Replace("”", "\\\"");
				text3 = text3.Replace("…", "...");
				writer.WriteLine("#. " + text2);
				writer.WriteLine("msgctxt \"{0}\"", text2);
				writer.WriteLine("msgid \"" + text3 + "\"");
				writer.WriteLine("msgstr \"\"");
				writer.WriteLine("");
			}
		}
	}

	// Token: 0x06003AE8 RID: 15080 RVA: 0x00146ED8 File Offset: 0x001450D8
	public static void GenerateStringsTemplate(string locstrings_namespace, Assembly assembly, string output_filename, Dictionary<string, object> current_runtime_locstring_forest)
	{
		Dictionary<string, object> dictionary = new Dictionary<string, object>();
		foreach (Type type in Localization.CollectLocStringTreeRoots(locstrings_namespace, assembly))
		{
			Dictionary<string, object> dictionary2 = Localization.MakeRuntimeLocStringTree(type);
			if (dictionary2.Count > 0)
			{
				dictionary[type.Name] = dictionary2;
			}
		}
		if (current_runtime_locstring_forest != null)
		{
			dictionary.Concat(current_runtime_locstring_forest);
		}
		using (StreamWriter streamWriter = new StreamWriter(output_filename, false, new UTF8Encoding(false)))
		{
			streamWriter.WriteLine("msgid \"\"");
			streamWriter.WriteLine("msgstr \"\"");
			streamWriter.WriteLine("\"Application: Oxygen Not Included\"");
			streamWriter.WriteLine("\"POT Version: 2.0\"");
			streamWriter.WriteLine("");
			Localization.WriteStringsTemplate(locstrings_namespace, streamWriter, dictionary);
		}
		DebugUtil.LogArgs(new object[] { "Generated " + output_filename });
	}

	// Token: 0x06003AE9 RID: 15081 RVA: 0x00146FD4 File Offset: 0x001451D4
	public static void GenerateStringsTemplate(Type locstring_tree_root, string output_folder)
	{
		output_folder = FileSystem.Normalize(output_folder);
		if (!FileUtil.CreateDirectory(output_folder, 5))
		{
			return;
		}
		Localization.GenerateStringsTemplate(locstring_tree_root.Namespace, Assembly.GetAssembly(locstring_tree_root), FileSystem.Normalize(Path.Combine(output_folder, string.Format("{0}_template.pot", locstring_tree_root.Namespace.ToLower()))), null);
	}

	// Token: 0x06003AEA RID: 15082 RVA: 0x00147028 File Offset: 0x00145228
	public static void Initialize()
	{
		DebugUtil.LogArgs(new object[] { "Localization.Initialize!" });
		bool flag = false;
		switch (Localization.GetSelectedLanguageType())
		{
		case Localization.SelectedLanguageType.None:
			Localization.sFontAsset = Localization.GetFont(Localization.GetDefaultLocale().FontName);
			break;
		case Localization.SelectedLanguageType.Preinstalled:
		{
			string currentLanguageCode = Localization.GetCurrentLanguageCode();
			if (!string.IsNullOrEmpty(currentLanguageCode))
			{
				DebugUtil.LogArgs(new object[] { "Localization Initialize... Preinstalled localization" });
				DebugUtil.LogArgs(new object[] { " -> ", currentLanguageCode });
				Localization.LoadPreinstalledTranslation(currentLanguageCode);
			}
			else
			{
				flag = true;
			}
			break;
		}
		case Localization.SelectedLanguageType.UGC:
			if (LanguageOptionsScreen.HasInstalledLanguage())
			{
				DebugUtil.LogArgs(new object[] { "Localization Initialize... Mod-based localization" });
				string savedLanguageMod = LanguageOptionsScreen.GetSavedLanguageMod();
				if (LanguageOptionsScreen.SetCurrentLanguage(savedLanguageMod))
				{
					DebugUtil.LogArgs(new object[] { " -> Loaded language from mod: " + savedLanguageMod });
				}
				else
				{
					DebugUtil.LogArgs(new object[] { " -> Failed to load language from mod: " + savedLanguageMod });
				}
			}
			else
			{
				flag = true;
			}
			break;
		}
		if (flag)
		{
			Localization.ClearLanguage();
		}
	}

	// Token: 0x06003AEB RID: 15083 RVA: 0x0014712C File Offset: 0x0014532C
	public static void VerifyTranslationModSubscription(GameObject context)
	{
		if (Localization.GetSelectedLanguageType() != Localization.SelectedLanguageType.UGC)
		{
			return;
		}
		if (!SteamManager.Initialized)
		{
			return;
		}
		if (LanguageOptionsScreen.HasInstalledLanguage())
		{
			return;
		}
		PublishedFileId_t publishedFileId_t = new PublishedFileId_t((ulong)KPlayerPrefs.GetInt("InstalledLanguage", (int)PublishedFileId_t.Invalid.m_PublishedFileId));
		Label label = new Label
		{
			distribution_platform = Label.DistributionPlatform.Steam,
			id = publishedFileId_t.ToString()
		};
		string text = UI.FRONTEND.TRANSLATIONS_SCREEN.UNKNOWN;
		foreach (Mod mod in Global.Instance.modManager.mods)
		{
			if (mod.label.Match(label))
			{
				text = mod.title;
				break;
			}
		}
		Localization.ClearLanguage();
		KScreen component = KScreenManager.AddChild(context, ScreenPrefabs.Instance.ConfirmDialogScreen.gameObject).GetComponent<KScreen>();
		component.Activate();
		ConfirmDialogScreen component2 = component.GetComponent<ConfirmDialogScreen>();
		string text2 = UI.CONFIRMDIALOG.DIALOG_HEADER;
		string text3 = string.Format(UI.FRONTEND.TRANSLATIONS_SCREEN.MISSING_LANGUAGE_PACK, text);
		string text4 = UI.FRONTEND.TRANSLATIONS_SCREEN.RESTART;
		component2.PopupConfirmDialog(text3, new System.Action(App.instance.Restart), null, null, null, text2, text4, null, null);
	}

	// Token: 0x06003AEC RID: 15084 RVA: 0x00147274 File Offset: 0x00145474
	public static void LoadPreinstalledTranslation(string code)
	{
		if (!string.IsNullOrEmpty(code) && code != Localization.DEFAULT_LANGUAGE_CODE)
		{
			string preinstalledLocalizationFilePath = Localization.GetPreinstalledLocalizationFilePath(code);
			if (Localization.LoadLocalTranslationFile(Localization.SelectedLanguageType.Preinstalled, preinstalledLocalizationFilePath))
			{
				KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_CODE_KEY, code);
				return;
			}
		}
		else
		{
			Localization.ClearLanguage();
		}
	}

	// Token: 0x06003AED RID: 15085 RVA: 0x001472B7 File Offset: 0x001454B7
	public static bool LoadLocalTranslationFile(Localization.SelectedLanguageType source, string path)
	{
		if (!File.Exists(path))
		{
			return false;
		}
		bool flag = Localization.LoadTranslationFromLines(File.ReadAllLines(path, Encoding.UTF8));
		if (flag)
		{
			KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_TYPE_KEY, source.ToString());
			return flag;
		}
		Localization.ClearLanguage();
		return flag;
	}

	// Token: 0x06003AEE RID: 15086 RVA: 0x001472F4 File Offset: 0x001454F4
	private static bool LoadTranslationFromLines(string[] lines)
	{
		if (lines == null || lines.Length == 0)
		{
			return false;
		}
		Localization.sLocale = Localization.GetLocale(lines);
		DebugUtil.LogArgs(new object[]
		{
			" -> Locale is now ",
			Localization.sLocale.ToString()
		});
		bool flag = Localization.LoadTranslation(lines, false);
		if (flag)
		{
			Localization.currentFontName = Localization.GetFontName(lines);
			Localization.SwapToLocalizedFont(Localization.currentFontName);
		}
		return flag;
	}

	// Token: 0x06003AEF RID: 15087 RVA: 0x00147358 File Offset: 0x00145558
	public static bool LoadTranslation(string[] lines, bool isTemplate = false)
	{
		bool flag;
		try
		{
			Localization.OverloadStrings(Localization.ExtractTranslatedStrings(lines, isTemplate));
			flag = true;
		}
		catch (Exception ex)
		{
			DebugUtil.LogWarningArgs(new object[] { ex });
			flag = false;
		}
		return flag;
	}

	// Token: 0x06003AF0 RID: 15088 RVA: 0x0014739C File Offset: 0x0014559C
	public static Dictionary<string, string> LoadStringsFile(string path, bool isTemplate)
	{
		return Localization.ExtractTranslatedStrings(File.ReadAllLines(path, Encoding.UTF8), isTemplate);
	}

	// Token: 0x06003AF1 RID: 15089 RVA: 0x001473B0 File Offset: 0x001455B0
	public static Dictionary<string, string> ExtractTranslatedStrings(string[] lines, bool isTemplate = false)
	{
		Dictionary<string, string> dictionary = new Dictionary<string, string>();
		Localization.Entry entry = default(Localization.Entry);
		string text = (isTemplate ? "msgid" : "msgstr");
		for (int i = 0; i < lines.Length; i++)
		{
			string text2 = lines[i];
			if (text2 == null || text2.Length == 0)
			{
				entry = default(Localization.Entry);
			}
			else
			{
				string text3 = Localization.GetParameter("msgctxt", i, lines);
				if (text3 != null)
				{
					entry.msgctxt = text3;
				}
				text3 = Localization.GetParameter(text, i, lines);
				if (text3 != null)
				{
					entry.msgstr = text3;
				}
			}
			if (entry.IsPopulated)
			{
				dictionary[entry.msgctxt] = entry.msgstr;
				entry = default(Localization.Entry);
			}
		}
		return dictionary;
	}

	// Token: 0x06003AF2 RID: 15090 RVA: 0x0014745C File Offset: 0x0014565C
	private static string FixupString(string result)
	{
		result = result.Replace("\\n", "\n");
		result = result.Replace("\\\"", "\"");
		result = result.Replace("<style=“", "<style=\"");
		result = result.Replace("”>", "\">");
		result = result.Replace("<color=^p", "<color=#");
		return result;
	}

	// Token: 0x06003AF3 RID: 15091 RVA: 0x001474C4 File Offset: 0x001456C4
	private static string GetParameter(string key, int idx, string[] all_lines)
	{
		if (!all_lines[idx].StartsWith(key))
		{
			return null;
		}
		List<string> list = new List<string>();
		string text = all_lines[idx];
		text = text.Substring(key.Length + 1, text.Length - key.Length - 1);
		list.Add(text);
		for (int i = idx + 1; i < all_lines.Length; i++)
		{
			string text2 = all_lines[i];
			if (!text2.StartsWith("\""))
			{
				break;
			}
			list.Add(text2);
		}
		string text3 = "";
		foreach (string text4 in list)
		{
			if (text4.EndsWith("\r"))
			{
				text4 = text4.Substring(0, text4.Length - 1);
			}
			text4 = text4.Substring(1, text4.Length - 2);
			text4 = Localization.FixupString(text4);
			text3 += text4;
		}
		return text3;
	}

	// Token: 0x06003AF4 RID: 15092 RVA: 0x001475C4 File Offset: 0x001457C4
	private static void AddAssembly(string locstrings_namespace, Assembly assembly)
	{
		List<Assembly> list;
		if (!Localization.translatable_assemblies.TryGetValue(locstrings_namespace, out list))
		{
			list = new List<Assembly>();
			Localization.translatable_assemblies.Add(locstrings_namespace, list);
		}
		list.Add(assembly);
	}

	// Token: 0x06003AF5 RID: 15093 RVA: 0x001475F9 File Offset: 0x001457F9
	public static void AddAssembly(Assembly assembly)
	{
		Localization.AddAssembly("STRINGS", assembly);
	}

	// Token: 0x06003AF6 RID: 15094 RVA: 0x00147608 File Offset: 0x00145808
	public static void RegisterForTranslation(Type locstring_tree_root)
	{
		Assembly assembly = Assembly.GetAssembly(locstring_tree_root);
		Localization.AddAssembly(locstring_tree_root.Namespace, assembly);
		string text = locstring_tree_root.Namespace + ".";
		foreach (Type type in Localization.CollectLocStringTreeRoots(locstring_tree_root.Namespace, assembly))
		{
			LocString.CreateLocStringKeys(type, text);
		}
	}

	// Token: 0x06003AF7 RID: 15095 RVA: 0x00147680 File Offset: 0x00145880
	public static void OverloadStrings(Dictionary<string, string> translated_strings)
	{
		string text = "";
		string text2 = "";
		string text3 = "";
		foreach (KeyValuePair<string, List<Assembly>> keyValuePair in Localization.translatable_assemblies)
		{
			foreach (Assembly assembly in keyValuePair.Value)
			{
				foreach (Type type in Localization.CollectLocStringTreeRoots(keyValuePair.Key, assembly))
				{
					string text4 = keyValuePair.Key + "." + type.Name;
					Localization.OverloadStrings(translated_strings, text4, type, ref text, ref text2, ref text3);
				}
			}
		}
		if (!string.IsNullOrEmpty(text))
		{
			DebugUtil.LogArgs(new object[] { "TRANSLATION ERROR! The following have missing or mismatched parameters:\n" + text });
		}
		if (!string.IsNullOrEmpty(text2))
		{
			DebugUtil.LogArgs(new object[] { "TRANSLATION ERROR! The following have mismatched <link> tags:\n" + text2 });
		}
		if (!string.IsNullOrEmpty(text3))
		{
			DebugUtil.LogArgs(new object[] { "TRANSLATION ERROR! The following do not have the same amount of <link> tags as the english string which can cause nested link errors:\n" + text3 });
		}
	}

	// Token: 0x06003AF8 RID: 15096 RVA: 0x001477F4 File Offset: 0x001459F4
	public static void OverloadStrings(Dictionary<string, string> translated_strings, string path, Type locstring_hierarchy, ref string parameter_errors, ref string link_errors, ref string link_count_errors)
	{
		foreach (FieldInfo fieldInfo in locstring_hierarchy.GetFields())
		{
			if (!(fieldInfo.FieldType != typeof(LocString)))
			{
				string text = path + "." + fieldInfo.Name;
				string text2 = null;
				if (translated_strings.TryGetValue(text, out text2))
				{
					LocString locString = (LocString)fieldInfo.GetValue(null);
					LocString locString2 = new LocString(text2, text);
					if (!Localization.AreParametersPreserved(locString.text, text2))
					{
						parameter_errors = parameter_errors + "\t" + text + "\n";
					}
					else if (!Localization.HasSameOrLessLinkCountAsEnglish(locString.text, text2))
					{
						link_count_errors = link_count_errors + "\t" + text + "\n";
					}
					else if (!Localization.HasMatchingLinkTags(text2, 0))
					{
						link_errors = link_errors + "\t" + text + "\n";
					}
					else
					{
						fieldInfo.SetValue(null, locString2);
					}
				}
			}
		}
		foreach (Type type in locstring_hierarchy.GetNestedTypes())
		{
			string text3 = path + "." + type.Name;
			Localization.OverloadStrings(translated_strings, text3, type, ref parameter_errors, ref link_errors, ref link_count_errors);
		}
	}

	// Token: 0x06003AF9 RID: 15097 RVA: 0x0014792E File Offset: 0x00145B2E
	public static string GetDefaultLocalizationFilePath()
	{
		return Path.Combine(Application.streamingAssetsPath, "strings/strings_template.pot");
	}

	// Token: 0x06003AFA RID: 15098 RVA: 0x0014793F File Offset: 0x00145B3F
	public static string GetModLocalizationFilePath()
	{
		return Path.Combine(Application.streamingAssetsPath, "strings/strings.po");
	}

	// Token: 0x06003AFB RID: 15099 RVA: 0x00147950 File Offset: 0x00145B50
	public static string GetPreinstalledLocalizationFilePath(string code)
	{
		string text = "strings/strings_preinstalled_" + code + ".po";
		return Path.Combine(Application.streamingAssetsPath, text);
	}

	// Token: 0x06003AFC RID: 15100 RVA: 0x00147979 File Offset: 0x00145B79
	public static string GetPreinstalledLocalizationTitle(string code)
	{
		return Strings.Get("STRINGS.UI.FRONTEND.TRANSLATIONS_SCREEN.PREINSTALLED_LANGUAGES." + code.ToUpper());
	}

	// Token: 0x06003AFD RID: 15101 RVA: 0x00147998 File Offset: 0x00145B98
	public static Texture2D GetPreinstalledLocalizationImage(string code)
	{
		string text = Path.Combine(Application.streamingAssetsPath, "strings/preinstalled_icon_" + code + ".png");
		if (File.Exists(text))
		{
			byte[] array = File.ReadAllBytes(text);
			Texture2D texture2D = new Texture2D(2, 2);
			texture2D.LoadImage(array);
			return texture2D;
		}
		return null;
	}

	// Token: 0x06003AFE RID: 15102 RVA: 0x001479E0 File Offset: 0x00145BE0
	public static void SetLocale(Localization.Locale locale)
	{
		Localization.sLocale = locale;
		DebugUtil.LogArgs(new object[]
		{
			" -> Locale is now ",
			Localization.sLocale.ToString()
		});
	}

	// Token: 0x06003AFF RID: 15103 RVA: 0x00147A08 File Offset: 0x00145C08
	public static Localization.Locale GetLocale()
	{
		return Localization.sLocale;
	}

	// Token: 0x06003B00 RID: 15104 RVA: 0x00147A10 File Offset: 0x00145C10
	private static string GetFontParam(string line)
	{
		string text = null;
		if (line.StartsWith("\"Font:"))
		{
			text = line.Substring("\"Font:".Length).Trim();
			text = text.Replace("\\n", "");
			text = text.Replace("\"", "");
		}
		return text;
	}

	// Token: 0x06003B01 RID: 15105 RVA: 0x00147A68 File Offset: 0x00145C68
	public static string GetCurrentLanguageCode()
	{
		switch (Localization.GetSelectedLanguageType())
		{
		case Localization.SelectedLanguageType.None:
			return Localization.DEFAULT_LANGUAGE_CODE;
		case Localization.SelectedLanguageType.Preinstalled:
			return KPlayerPrefs.GetString(Localization.SELECTED_LANGUAGE_CODE_KEY);
		case Localization.SelectedLanguageType.UGC:
			return LanguageOptionsScreen.GetInstalledLanguageCode();
		default:
			return "";
		}
	}

	// Token: 0x06003B02 RID: 15106 RVA: 0x00147AAC File Offset: 0x00145CAC
	public static Localization.SelectedLanguageType GetSelectedLanguageType()
	{
		return (Localization.SelectedLanguageType)Enum.Parse(typeof(Localization.SelectedLanguageType), KPlayerPrefs.GetString(Localization.SELECTED_LANGUAGE_TYPE_KEY, Localization.SelectedLanguageType.None.ToString()), true);
	}

	// Token: 0x06003B03 RID: 15107 RVA: 0x00147AE8 File Offset: 0x00145CE8
	private static string GetLanguageCode(string line)
	{
		string text = null;
		if (line.StartsWith("\"Language:"))
		{
			text = line.Substring("\"Language:".Length).Trim();
			text = text.Replace("\\n", "");
			text = text.Replace("\"", "");
		}
		return text;
	}

	// Token: 0x06003B04 RID: 15108 RVA: 0x00147B40 File Offset: 0x00145D40
	private static Localization.Locale GetLocaleForCode(string code)
	{
		Localization.Locale locale = null;
		foreach (Localization.Locale locale2 in Localization.Locales)
		{
			if (locale2.MatchesCode(code))
			{
				locale = locale2;
				break;
			}
		}
		return locale;
	}

	// Token: 0x06003B05 RID: 15109 RVA: 0x00147B9C File Offset: 0x00145D9C
	public static Localization.Locale GetLocale(string[] lines)
	{
		Localization.Locale locale = null;
		string text = null;
		if (lines != null && lines.Length != 0)
		{
			foreach (string text2 in lines)
			{
				if (text2 != null && text2.Length != 0)
				{
					text = Localization.GetLanguageCode(text2);
					if (text != null)
					{
						locale = Localization.GetLocaleForCode(text);
					}
					if (text != null)
					{
						break;
					}
				}
			}
		}
		if (locale == null)
		{
			locale = Localization.GetDefaultLocale();
		}
		if (text != null && locale.Code == "")
		{
			locale.SetCode(text);
		}
		return locale;
	}

	// Token: 0x06003B06 RID: 15110 RVA: 0x00147C11 File Offset: 0x00145E11
	private static string GetFontName(string filename)
	{
		return Localization.GetFontName(File.ReadAllLines(filename, Encoding.UTF8));
	}

	// Token: 0x06003B07 RID: 15111 RVA: 0x00147C24 File Offset: 0x00145E24
	public static Localization.Locale GetDefaultLocale()
	{
		Localization.Locale locale = null;
		foreach (Localization.Locale locale2 in Localization.Locales)
		{
			if (locale2.Lang == Localization.Language.Unspecified)
			{
				locale = new Localization.Locale(locale2);
				break;
			}
		}
		return locale;
	}

	// Token: 0x06003B08 RID: 15112 RVA: 0x00147C84 File Offset: 0x00145E84
	public static string GetDefaultFontName()
	{
		string text = null;
		foreach (Localization.Locale locale in Localization.Locales)
		{
			if (locale.Lang == Localization.Language.Unspecified)
			{
				text = locale.FontName;
				break;
			}
		}
		return text;
	}

	// Token: 0x06003B09 RID: 15113 RVA: 0x00147CE4 File Offset: 0x00145EE4
	public static string ValidateFontName(string fontName)
	{
		foreach (Localization.Locale locale in Localization.Locales)
		{
			if (locale.MatchesFont(fontName))
			{
				return locale.FontName;
			}
		}
		return null;
	}

	// Token: 0x06003B0A RID: 15114 RVA: 0x00147D44 File Offset: 0x00145F44
	public static string GetFontName(string[] lines)
	{
		string text = null;
		foreach (string text2 in lines)
		{
			if (text2 != null && text2.Length != 0)
			{
				string fontParam = Localization.GetFontParam(text2);
				if (fontParam != null)
				{
					text = Localization.ValidateFontName(fontParam);
				}
			}
			if (text != null)
			{
				break;
			}
		}
		if (text == null)
		{
			if (Localization.sLocale != null)
			{
				text = Localization.sLocale.FontName;
			}
			else
			{
				text = Localization.GetDefaultFontName();
			}
		}
		return text;
	}

	// Token: 0x06003B0B RID: 15115 RVA: 0x00147DA7 File Offset: 0x00145FA7
	public static void SwapToLocalizedFont()
	{
		Localization.SwapToLocalizedFont(Localization.currentFontName);
	}

	// Token: 0x06003B0C RID: 15116 RVA: 0x00147DB4 File Offset: 0x00145FB4
	public static bool SwapToLocalizedFont(string fontname)
	{
		if (string.IsNullOrEmpty(fontname))
		{
			return false;
		}
		Localization.sFontAsset = Localization.GetFont(fontname);
		foreach (TextStyleSetting textStyleSetting in Resources.FindObjectsOfTypeAll<TextStyleSetting>())
		{
			if (textStyleSetting != null)
			{
				textStyleSetting.sdfFont = Localization.sFontAsset;
			}
		}
		bool isRightToLeft = Localization.IsRightToLeft;
		foreach (LocText locText in Resources.FindObjectsOfTypeAll<LocText>())
		{
			if (locText != null)
			{
				locText.SwapFont(Localization.sFontAsset, isRightToLeft);
			}
		}
		return true;
	}

	// Token: 0x06003B0D RID: 15117 RVA: 0x00147E3C File Offset: 0x0014603C
	private static bool SetFont(Type target_type, object target, TMP_FontAsset font, bool is_right_to_left, HashSet<MemberInfo> excluded_members)
	{
		if (target_type == null || target == null || font == null)
		{
			return false;
		}
		foreach (FieldInfo fieldInfo in target_type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy))
		{
			if (!excluded_members.Contains(fieldInfo))
			{
				if (fieldInfo.FieldType == typeof(TextStyleSetting))
				{
					((TextStyleSetting)fieldInfo.GetValue(target)).sdfFont = font;
				}
				else if (fieldInfo.FieldType == typeof(LocText))
				{
					((LocText)fieldInfo.GetValue(target)).SwapFont(font, is_right_to_left);
				}
				else if (fieldInfo.FieldType == typeof(GameObject))
				{
					foreach (Component component in ((GameObject)fieldInfo.GetValue(target)).GetComponents<Component>())
					{
						Localization.SetFont(component.GetType(), component, font, is_right_to_left, excluded_members);
					}
				}
				else if (fieldInfo.MemberType == MemberTypes.Field && fieldInfo.FieldType != fieldInfo.DeclaringType)
				{
					Localization.SetFont(fieldInfo.FieldType, fieldInfo.GetValue(target), font, is_right_to_left, excluded_members);
				}
			}
		}
		return true;
	}

	// Token: 0x06003B0E RID: 15118 RVA: 0x00147F75 File Offset: 0x00146175
	public static bool SetFont<T>(T target, TMP_FontAsset font, bool is_right_to_left, HashSet<MemberInfo> excluded_members)
	{
		return Localization.SetFont(typeof(T), target, font, is_right_to_left, excluded_members);
	}

	// Token: 0x06003B0F RID: 15119 RVA: 0x00147F90 File Offset: 0x00146190
	public static TMP_FontAsset GetFont(string fontname)
	{
		foreach (TMP_FontAsset tmp_FontAsset in Resources.FindObjectsOfTypeAll<TMP_FontAsset>())
		{
			if (tmp_FontAsset.name == fontname)
			{
				return tmp_FontAsset;
			}
		}
		return null;
	}

	// Token: 0x06003B10 RID: 15120 RVA: 0x00147FC8 File Offset: 0x001461C8
	private static bool HasSameOrLessTokenCount(string english_string, string translated_string, string token)
	{
		int num = english_string.Split(new string[] { token }, StringSplitOptions.None).Length;
		int num2 = translated_string.Split(new string[] { token }, StringSplitOptions.None).Length;
		return num >= num2;
	}

	// Token: 0x06003B11 RID: 15121 RVA: 0x00148004 File Offset: 0x00146204
	private static bool HasSameOrLessLinkCountAsEnglish(string english_string, string translated_string)
	{
		return Localization.HasSameOrLessTokenCount(english_string, translated_string, "<link") && Localization.HasSameOrLessTokenCount(english_string, translated_string, "</link");
	}

	// Token: 0x06003B12 RID: 15122 RVA: 0x00148024 File Offset: 0x00146224
	private static bool HasMatchingLinkTags(string str, int idx = 0)
	{
		int num = str.IndexOf("<link", idx);
		int num2 = str.IndexOf("</link", idx);
		if (num == -1 && num2 == -1)
		{
			return true;
		}
		if (num == -1 && num2 != -1)
		{
			return false;
		}
		if (num != -1 && num2 == -1)
		{
			return false;
		}
		if (num2 < num)
		{
			return false;
		}
		int num3 = str.IndexOf("<link", num + 1);
		return (num < 0 || num3 == -1 || num3 >= num2) && Localization.HasMatchingLinkTags(str, num2 + 1);
	}

	// Token: 0x06003B13 RID: 15123 RVA: 0x00148098 File Offset: 0x00146298
	private static bool AreParametersPreserved(string old_string, string new_string)
	{
		MatchCollection matchCollection = Regex.Matches(old_string, "({.[^}]*?})(?!.*\\1)");
		MatchCollection matchCollection2 = Regex.Matches(new_string, "({.[^}]*?})(?!.*\\1)");
		bool flag = false;
		if (matchCollection == null && matchCollection2 == null)
		{
			flag = true;
		}
		else if (matchCollection != null && matchCollection2 != null && matchCollection.Count == matchCollection2.Count)
		{
			flag = true;
			foreach (object obj in matchCollection)
			{
				string text = obj.ToString();
				bool flag2 = false;
				foreach (object obj2 in matchCollection2)
				{
					string text2 = obj2.ToString();
					if (text == text2)
					{
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x06003B14 RID: 15124 RVA: 0x00148190 File Offset: 0x00146390
	public static bool HasDirtyWords(string str)
	{
		return Localization.FilterDirtyWords(str) != str;
	}

	// Token: 0x06003B15 RID: 15125 RVA: 0x0014819E File Offset: 0x0014639E
	public static string FilterDirtyWords(string str)
	{
		return DistributionPlatform.Inst.ApplyWordFilter(str);
	}

	// Token: 0x06003B16 RID: 15126 RVA: 0x001481AB File Offset: 0x001463AB
	public static string GetFileDateFormat(int format_idx)
	{
		return "{" + format_idx.ToString() + ":dd / MMM / yyyy}";
	}

	// Token: 0x06003B17 RID: 15127 RVA: 0x001481C4 File Offset: 0x001463C4
	public static void ClearLanguage()
	{
		DebugUtil.LogArgs(new object[] { " -> Clearing selected language! Either it didn't load correct or returning to english by menu." });
		Localization.sFontAsset = null;
		Localization.sLocale = null;
		KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_TYPE_KEY, Localization.SelectedLanguageType.None.ToString());
		KPlayerPrefs.SetString(Localization.SELECTED_LANGUAGE_CODE_KEY, "");
		Localization.SwapToLocalizedFont(Localization.GetDefaultLocale().FontName);
		string defaultLocalizationFilePath = Localization.GetDefaultLocalizationFilePath();
		if (File.Exists(defaultLocalizationFilePath))
		{
			Localization.LoadTranslation(File.ReadAllLines(defaultLocalizationFilePath, Encoding.UTF8), true);
		}
		LanguageOptionsScreen.CleanUpSavedLanguageMod();
	}

	// Token: 0x06003B18 RID: 15128 RVA: 0x00148250 File Offset: 0x00146450
	private static string ReverseText(string source)
	{
		char[] array = new char[] { '\n' };
		string[] array2 = source.Split(array);
		string text = "";
		int num = 0;
		foreach (string text2 in array2)
		{
			num++;
			char[] array4 = new char[text2.Length];
			for (int j = 0; j < text2.Length; j++)
			{
				array4[array4.Length - 1 - j] = text2[j];
			}
			text += new string(array4);
			if (num < array2.Length)
			{
				text += "\n";
			}
		}
		return text;
	}

	// Token: 0x06003B19 RID: 15129 RVA: 0x001482F4 File Offset: 0x001464F4
	public static string Fixup(string text)
	{
		if (Localization.sLocale != null && text != null && text != "" && Localization.sLocale.Lang == Localization.Language.Arabic)
		{
			return Localization.ReverseText(ArabicFixer.Fix(text));
		}
		return text;
	}

	// Token: 0x04002691 RID: 9873
	private static TMP_FontAsset sFontAsset = null;

	// Token: 0x04002692 RID: 9874
	private static readonly List<Localization.Locale> Locales = new List<Localization.Locale>
	{
		new Localization.Locale(Localization.Language.Chinese, Localization.Direction.LeftToRight, "zh", "NotoSansCJKsc-Regular"),
		new Localization.Locale(Localization.Language.Japanese, Localization.Direction.LeftToRight, "ja", "NotoSansCJKjp-Regular"),
		new Localization.Locale(Localization.Language.Korean, Localization.Direction.LeftToRight, "ko", "NotoSansCJKkr-Regular"),
		new Localization.Locale(Localization.Language.Russian, Localization.Direction.LeftToRight, "ru", "RobotoCondensed-Regular"),
		new Localization.Locale(Localization.Language.Thai, Localization.Direction.LeftToRight, "th", "NotoSansThai-Regular"),
		new Localization.Locale(Localization.Language.Arabic, Localization.Direction.RightToLeft, "ar", "NotoNaskhArabic-Regular"),
		new Localization.Locale(Localization.Language.Hebrew, Localization.Direction.RightToLeft, "he", "NotoSansHebrew-Regular"),
		new Localization.Locale(Localization.Language.Unspecified, Localization.Direction.LeftToRight, "", "RobotoCondensed-Regular")
	};

	// Token: 0x04002693 RID: 9875
	private static Localization.Locale sLocale = null;

	// Token: 0x04002694 RID: 9876
	private static string currentFontName = null;

	// Token: 0x04002695 RID: 9877
	public static string DEFAULT_LANGUAGE_CODE = "en";

	// Token: 0x04002696 RID: 9878
	public static readonly List<string> PreinstalledLanguages = new List<string>
	{
		Localization.DEFAULT_LANGUAGE_CODE,
		"zh_klei",
		"ko_klei",
		"ru_klei"
	};

	// Token: 0x04002697 RID: 9879
	public static string SELECTED_LANGUAGE_TYPE_KEY = "SelectedLanguageType";

	// Token: 0x04002698 RID: 9880
	public static string SELECTED_LANGUAGE_CODE_KEY = "SelectedLanguageCode";

	// Token: 0x04002699 RID: 9881
	private static Dictionary<string, List<Assembly>> translatable_assemblies = new Dictionary<string, List<Assembly>>();

	// Token: 0x0400269A RID: 9882
	public const BindingFlags non_static_data_member_fields = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

	// Token: 0x0400269B RID: 9883
	private const string start_link_token = "<link";

	// Token: 0x0400269C RID: 9884
	private const string end_link_token = "</link";

	// Token: 0x0200154E RID: 5454
	public enum Language
	{
		// Token: 0x04006659 RID: 26201
		Chinese,
		// Token: 0x0400665A RID: 26202
		Japanese,
		// Token: 0x0400665B RID: 26203
		Korean,
		// Token: 0x0400665C RID: 26204
		Russian,
		// Token: 0x0400665D RID: 26205
		Thai,
		// Token: 0x0400665E RID: 26206
		Arabic,
		// Token: 0x0400665F RID: 26207
		Hebrew,
		// Token: 0x04006660 RID: 26208
		Unspecified
	}

	// Token: 0x0200154F RID: 5455
	public enum Direction
	{
		// Token: 0x04006662 RID: 26210
		LeftToRight,
		// Token: 0x04006663 RID: 26211
		RightToLeft
	}

	// Token: 0x02001550 RID: 5456
	public class Locale
	{
		// Token: 0x06008396 RID: 33686 RVA: 0x002E7CAE File Offset: 0x002E5EAE
		public Locale(Localization.Locale other)
		{
			this.mLanguage = other.mLanguage;
			this.mDirection = other.mDirection;
			this.mCode = other.mCode;
			this.mFontName = other.mFontName;
		}

		// Token: 0x06008397 RID: 33687 RVA: 0x002E7CE6 File Offset: 0x002E5EE6
		public Locale(Localization.Language language, Localization.Direction direction, string code, string fontName)
		{
			this.mLanguage = language;
			this.mDirection = direction;
			this.mCode = code.ToLower();
			this.mFontName = fontName;
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06008398 RID: 33688 RVA: 0x002E7D10 File Offset: 0x002E5F10
		public Localization.Language Lang
		{
			get
			{
				return this.mLanguage;
			}
		}

		// Token: 0x06008399 RID: 33689 RVA: 0x002E7D18 File Offset: 0x002E5F18
		public void SetCode(string code)
		{
			this.mCode = code;
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600839A RID: 33690 RVA: 0x002E7D21 File Offset: 0x002E5F21
		public string Code
		{
			get
			{
				return this.mCode;
			}
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600839B RID: 33691 RVA: 0x002E7D29 File Offset: 0x002E5F29
		public string FontName
		{
			get
			{
				return this.mFontName;
			}
		}

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x0600839C RID: 33692 RVA: 0x002E7D31 File Offset: 0x002E5F31
		public bool IsRightToLeft
		{
			get
			{
				return this.mDirection == Localization.Direction.RightToLeft;
			}
		}

		// Token: 0x0600839D RID: 33693 RVA: 0x002E7D3C File Offset: 0x002E5F3C
		public bool MatchesCode(string language_code)
		{
			return language_code.ToLower().Contains(this.mCode);
		}

		// Token: 0x0600839E RID: 33694 RVA: 0x002E7D4F File Offset: 0x002E5F4F
		public bool MatchesFont(string fontname)
		{
			return fontname.ToLower() == this.mFontName.ToLower();
		}

		// Token: 0x0600839F RID: 33695 RVA: 0x002E7D68 File Offset: 0x002E5F68
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.mCode,
				":",
				this.mLanguage.ToString(),
				":",
				this.mDirection.ToString(),
				":",
				this.mFontName
			});
		}

		// Token: 0x04006664 RID: 26212
		private Localization.Language mLanguage;

		// Token: 0x04006665 RID: 26213
		private string mCode;

		// Token: 0x04006666 RID: 26214
		private string mFontName;

		// Token: 0x04006667 RID: 26215
		private Localization.Direction mDirection;
	}

	// Token: 0x02001551 RID: 5457
	private struct Entry
	{
		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x060083A0 RID: 33696 RVA: 0x002E7DD2 File Offset: 0x002E5FD2
		public bool IsPopulated
		{
			get
			{
				return this.msgctxt != null && this.msgstr != null && this.msgstr.Length > 0;
			}
		}

		// Token: 0x04006668 RID: 26216
		public string msgctxt;

		// Token: 0x04006669 RID: 26217
		public string msgstr;
	}

	// Token: 0x02001552 RID: 5458
	public enum SelectedLanguageType
	{
		// Token: 0x0400666B RID: 26219
		None,
		// Token: 0x0400666C RID: 26220
		Preinstalled,
		// Token: 0x0400666D RID: 26221
		UGC
	}
}
