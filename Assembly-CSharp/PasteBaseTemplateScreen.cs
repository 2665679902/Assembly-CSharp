using System;
using System.Collections.Generic;
using System.IO;
using Klei;
using ProcGen;
using STRINGS;
using UnityEngine;

// Token: 0x02000B54 RID: 2900
public class PasteBaseTemplateScreen : KScreen
{
	// Token: 0x06005A15 RID: 23061 RVA: 0x00209ABA File Offset: 0x00207CBA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		PasteBaseTemplateScreen.Instance = this;
		TemplateCache.Init();
		this.button_directory_up.onClick += this.UpDirectory;
		base.ConsumeMouseScroll = true;
		this.RefreshStampButtons();
	}

	// Token: 0x06005A16 RID: 23062 RVA: 0x00209AF1 File Offset: 0x00207CF1
	protected override void OnForcedCleanUp()
	{
		PasteBaseTemplateScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06005A17 RID: 23063 RVA: 0x00209B00 File Offset: 0x00207D00
	[ContextMenu("Refresh")]
	public void RefreshStampButtons()
	{
		this.directory_path_text.text = this.m_CurrentDirectory;
		this.button_directory_up.isInteractable = this.m_CurrentDirectory != PasteBaseTemplateScreen.NO_DIRECTORY;
		foreach (GameObject gameObject in this.m_template_buttons)
		{
			UnityEngine.Object.Destroy(gameObject);
		}
		this.m_template_buttons.Clear();
		global::Debug.Log("Changing directory to " + this.m_CurrentDirectory);
		if (this.m_CurrentDirectory == PasteBaseTemplateScreen.NO_DIRECTORY)
		{
			this.directory_path_text.text = "";
			using (List<string>.Enumerator enumerator2 = DlcManager.RELEASE_ORDER.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					string dlcId = enumerator2.Current;
					if (DlcManager.IsContentActive(dlcId))
					{
						GameObject gameObject2 = global::Util.KInstantiateUI(this.prefab_directory_button, this.button_list_container, true);
						gameObject2.GetComponent<KButton>().onClick += delegate
						{
							this.UpdateDirectory(SettingsCache.GetScope(dlcId));
						};
						gameObject2.GetComponentInChildren<LocText>().text = ((dlcId == "") ? UI.DEBUG_TOOLS.SAVE_BASE_TEMPLATE.BASE_GAME_FOLDER_NAME.text : SettingsCache.GetScope(dlcId));
						this.m_template_buttons.Add(gameObject2);
					}
				}
			}
			return;
		}
		string[] directories = Directory.GetDirectories(TemplateCache.RewriteTemplatePath(this.m_CurrentDirectory));
		for (int i = 0; i < directories.Length; i++)
		{
			string text = directories[i];
			string directory_name = System.IO.Path.GetFileNameWithoutExtension(text);
			GameObject gameObject3 = global::Util.KInstantiateUI(this.prefab_directory_button, this.button_list_container, true);
			gameObject3.GetComponent<KButton>().onClick += delegate
			{
				this.UpdateDirectory(directory_name);
			};
			gameObject3.GetComponentInChildren<LocText>().text = directory_name;
			this.m_template_buttons.Add(gameObject3);
		}
		ListPool<FileHandle, PasteBaseTemplateScreen>.PooledList pooledList = ListPool<FileHandle, PasteBaseTemplateScreen>.Allocate();
		FileSystem.GetFiles(TemplateCache.RewriteTemplatePath(this.m_CurrentDirectory), "*.yaml", pooledList);
		foreach (FileHandle fileHandle in pooledList)
		{
			string file_path_no_extension = System.IO.Path.GetFileNameWithoutExtension(fileHandle.full_path);
			GameObject gameObject4 = global::Util.KInstantiateUI(this.prefab_paste_button, this.button_list_container, true);
			gameObject4.GetComponent<KButton>().onClick += delegate
			{
				this.OnClickPasteButton(file_path_no_extension);
			};
			gameObject4.GetComponentInChildren<LocText>().text = file_path_no_extension;
			this.m_template_buttons.Add(gameObject4);
		}
	}

	// Token: 0x06005A18 RID: 23064 RVA: 0x00209DEC File Offset: 0x00207FEC
	private void UpdateDirectory(string relativePath)
	{
		if (this.m_CurrentDirectory == PasteBaseTemplateScreen.NO_DIRECTORY)
		{
			this.m_CurrentDirectory = "";
		}
		this.m_CurrentDirectory = FileSystem.CombineAndNormalize(new string[] { this.m_CurrentDirectory, relativePath });
		this.RefreshStampButtons();
	}

	// Token: 0x06005A19 RID: 23065 RVA: 0x00209E3C File Offset: 0x0020803C
	private void UpDirectory()
	{
		int num = this.m_CurrentDirectory.LastIndexOf("/");
		if (num > 0)
		{
			this.m_CurrentDirectory = this.m_CurrentDirectory.Substring(0, num);
		}
		else
		{
			string text;
			string text2;
			SettingsCache.GetDlcIdAndPath(this.m_CurrentDirectory, out text, out text2);
			if (text2.IsNullOrWhiteSpace())
			{
				this.m_CurrentDirectory = PasteBaseTemplateScreen.NO_DIRECTORY;
			}
			else
			{
				this.m_CurrentDirectory = SettingsCache.GetScope(text);
			}
		}
		this.RefreshStampButtons();
	}

	// Token: 0x06005A1A RID: 23066 RVA: 0x00209EAC File Offset: 0x002080AC
	private void OnClickPasteButton(string template_name)
	{
		if (template_name == null)
		{
			return;
		}
		string text = FileSystem.CombineAndNormalize(new string[] { this.m_CurrentDirectory, template_name });
		DebugTool.Instance.DeactivateTool(null);
		DebugBaseTemplateButton.Instance.ClearSelection();
		DebugBaseTemplateButton.Instance.nameField.text = text;
		TemplateContainer template = TemplateCache.GetTemplate(text);
		StampTool.Instance.Activate(template, true, false);
	}

	// Token: 0x04003CE8 RID: 15592
	public static PasteBaseTemplateScreen Instance;

	// Token: 0x04003CE9 RID: 15593
	public GameObject button_list_container;

	// Token: 0x04003CEA RID: 15594
	public GameObject prefab_paste_button;

	// Token: 0x04003CEB RID: 15595
	public GameObject prefab_directory_button;

	// Token: 0x04003CEC RID: 15596
	public KButton button_directory_up;

	// Token: 0x04003CED RID: 15597
	public LocText directory_path_text;

	// Token: 0x04003CEE RID: 15598
	private List<GameObject> m_template_buttons = new List<GameObject>();

	// Token: 0x04003CEF RID: 15599
	private static readonly string NO_DIRECTORY = "NONE";

	// Token: 0x04003CF0 RID: 15600
	private string m_CurrentDirectory = PasteBaseTemplateScreen.NO_DIRECTORY;
}
