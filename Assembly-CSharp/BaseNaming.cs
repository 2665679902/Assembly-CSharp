using System;
using System.IO;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020009F0 RID: 2544
[AddComponentMenu("KMonoBehaviour/scripts/BaseNaming")]
public class BaseNaming : KMonoBehaviour
{
	// Token: 0x06004C0D RID: 19469 RVA: 0x001AA730 File Offset: 0x001A8930
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.GenerateBaseName();
		this.shuffleBaseNameButton.onClick += this.GenerateBaseName;
		this.inputField.onEndEdit.AddListener(new UnityAction<string>(this.OnEndEdit));
		this.inputField.onValueChanged.AddListener(new UnityAction<string>(this.OnEditing));
		this.minionSelectScreen = base.GetComponent<MinionSelectScreen>();
	}

	// Token: 0x06004C0E RID: 19470 RVA: 0x001AA7A4 File Offset: 0x001A89A4
	private bool CheckBaseName(string newName)
	{
		if (string.IsNullOrEmpty(newName))
		{
			return true;
		}
		string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		if (this.minionSelectScreen != null)
		{
			bool flag = false;
			try
			{
				bool flag2 = Directory.Exists(Path.Combine(savePrefixAndCreateFolder, newName));
				bool flag3 = cloudSavePrefix != null && Directory.Exists(Path.Combine(cloudSavePrefix, newName));
				flag = flag2 || flag3;
			}
			catch (Exception ex)
			{
				flag = true;
				global::Debug.Log(string.Format("Base Naming / Warning / {0}", ex));
			}
			if (flag)
			{
				this.minionSelectScreen.SetProceedButtonActive(false, string.Format(UI.IMMIGRANTSCREEN.DUPLICATE_COLONY_NAME, newName));
				return false;
			}
			this.minionSelectScreen.SetProceedButtonActive(true, null);
		}
		return true;
	}

	// Token: 0x06004C0F RID: 19471 RVA: 0x001AA854 File Offset: 0x001A8A54
	private void OnEditing(string newName)
	{
		Util.ScrubInputField(this.inputField, false, false);
		this.CheckBaseName(this.inputField.text);
	}

	// Token: 0x06004C10 RID: 19472 RVA: 0x001AA878 File Offset: 0x001A8A78
	private void OnEndEdit(string newName)
	{
		if (Localization.HasDirtyWords(newName))
		{
			this.inputField.text = this.GenerateBaseNameString();
			newName = this.inputField.text;
		}
		if (string.IsNullOrEmpty(newName))
		{
			return;
		}
		if (newName.EndsWith(" "))
		{
			newName = newName.TrimEnd(new char[] { ' ' });
		}
		if (!this.CheckBaseName(newName))
		{
			return;
		}
		this.inputField.text = newName;
		SaveGame.Instance.SetBaseName(newName);
		string text = Path.ChangeExtension(newName, ".sav");
		string savePrefixAndCreateFolder = SaveLoader.GetSavePrefixAndCreateFolder();
		string cloudSavePrefix = SaveLoader.GetCloudSavePrefix();
		string text2 = savePrefixAndCreateFolder;
		if (SaveLoader.GetCloudSavesAvailable() && Game.Instance.SaveToCloudActive && cloudSavePrefix != null)
		{
			text2 = cloudSavePrefix;
		}
		SaveLoader.SetActiveSaveFilePath(Path.Combine(text2, newName, text));
	}

	// Token: 0x06004C11 RID: 19473 RVA: 0x001AA934 File Offset: 0x001A8B34
	private void GenerateBaseName()
	{
		string text = this.GenerateBaseNameString();
		((LocText)this.inputField.placeholder).text = text;
		this.inputField.text = text;
		this.OnEndEdit(text);
	}

	// Token: 0x06004C12 RID: 19474 RVA: 0x001AA974 File Offset: 0x001A8B74
	private string GenerateBaseNameString()
	{
		string text = LocString.GetStrings(typeof(NAMEGEN.COLONY.FORMATS)).GetRandom<string>();
		text = this.ReplaceStringWithRandom(text, "{noun}", LocString.GetStrings(typeof(NAMEGEN.COLONY.NOUN)));
		string[] strings = LocString.GetStrings(typeof(NAMEGEN.COLONY.ADJECTIVE));
		text = this.ReplaceStringWithRandom(text, "{adjective}", strings);
		text = this.ReplaceStringWithRandom(text, "{adjective2}", strings);
		text = this.ReplaceStringWithRandom(text, "{adjective3}", strings);
		return this.ReplaceStringWithRandom(text, "{adjective4}", strings);
	}

	// Token: 0x06004C13 RID: 19475 RVA: 0x001AA9FB File Offset: 0x001A8BFB
	private string ReplaceStringWithRandom(string fullString, string replacementKey, string[] replacementValues)
	{
		if (!fullString.Contains(replacementKey))
		{
			return fullString;
		}
		return fullString.Replace(replacementKey, replacementValues.GetRandom<string>());
	}

	// Token: 0x04003214 RID: 12820
	[SerializeField]
	private KInputTextField inputField;

	// Token: 0x04003215 RID: 12821
	[SerializeField]
	private KButton shuffleBaseNameButton;

	// Token: 0x04003216 RID: 12822
	private MinionSelectScreen minionSelectScreen;
}
