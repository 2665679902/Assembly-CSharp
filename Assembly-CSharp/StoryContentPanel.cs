using System;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.CustomSettings;
using ProcGen;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C07 RID: 3079
public class StoryContentPanel : KMonoBehaviour
{
	// Token: 0x06006195 RID: 24981 RVA: 0x00240740 File Offset: 0x0023E940
	public List<string> GetActiveStories()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair in this.storyStates)
		{
			if (keyValuePair.Value == StoryContentPanel.StoryState.Guaranteed)
			{
				list.Add(keyValuePair.Key);
			}
		}
		return list;
	}

	// Token: 0x06006196 RID: 24982 RVA: 0x002407AC File Offset: 0x0023E9AC
	public void Init()
	{
		this.SpawnRows();
		this.RefreshRows();
		this.RefreshDescriptionPanel();
		this.SelectDefault();
		CustomGameSettings.Instance.OnStorySettingChanged += this.OnStorySettingChanged;
	}

	// Token: 0x06006197 RID: 24983 RVA: 0x002407DC File Offset: 0x0023E9DC
	public void Cleanup()
	{
		CustomGameSettings.Instance.OnStorySettingChanged -= this.OnStorySettingChanged;
	}

	// Token: 0x06006198 RID: 24984 RVA: 0x002407F4 File Offset: 0x0023E9F4
	private void OnStorySettingChanged(SettingConfig config, SettingLevel level)
	{
		this.storyStates[config.id] = ((level.id == "Guaranteed") ? StoryContentPanel.StoryState.Guaranteed : StoryContentPanel.StoryState.Forbidden);
		this.RefreshStoryDisplay(config.id);
	}

	// Token: 0x06006199 RID: 24985 RVA: 0x0024082C File Offset: 0x0023EA2C
	private void SpawnRows()
	{
		using (List<Story>.Enumerator enumerator = Db.Get().Stories.resources.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Story story = enumerator.Current;
				GameObject gameObject = global::Util.KInstantiateUI(this.storyRowPrefab, this.storyRowContainer, true);
				HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("Label").SetText(Strings.Get(story.StoryTrait.name));
				MultiToggle component2 = gameObject.GetComponent<MultiToggle>();
				component2.onClick = (System.Action)Delegate.Combine(component2.onClick, new System.Action(delegate
				{
					this.SelectRow(story.Id);
				}));
				this.storyRows.Add(story.Id, gameObject);
				component.GetReference<Image>("Icon").sprite = Assets.GetSprite(story.StoryTrait.icon);
				MultiToggle reference = component.GetReference<MultiToggle>("checkbox");
				reference.onClick = (System.Action)Delegate.Combine(reference.onClick, new System.Action(delegate
				{
					this.IncrementStorySetting(story.Id, true);
					this.RefreshStoryDisplay(story.Id);
				}));
				this.storyStates.Add(story.Id, this._defaultStoryState);
			}
		}
		this.RefreshAllStoryStates();
		this.mainScreen.RefreshStoryLabel();
	}

	// Token: 0x0600619A RID: 24986 RVA: 0x002409A8 File Offset: 0x0023EBA8
	private void SelectRow(string id)
	{
		this.selectedStoryId = id;
		this.RefreshRows();
		this.RefreshDescriptionPanel();
	}

	// Token: 0x0600619B RID: 24987 RVA: 0x002409C0 File Offset: 0x0023EBC0
	public void SelectDefault()
	{
		foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair in this.storyStates)
		{
			if (keyValuePair.Value == StoryContentPanel.StoryState.Guaranteed)
			{
				this.SelectRow(keyValuePair.Key);
				return;
			}
		}
		using (Dictionary<string, StoryContentPanel.StoryState>.Enumerator enumerator = this.storyStates.GetEnumerator())
		{
			if (enumerator.MoveNext())
			{
				KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair2 = enumerator.Current;
				this.SelectRow(keyValuePair2.Key);
			}
		}
	}

	// Token: 0x0600619C RID: 24988 RVA: 0x00240A70 File Offset: 0x0023EC70
	private void IncrementStorySetting(string storyId, bool forward = true)
	{
		int num = (int)this.storyStates[storyId];
		num += (forward ? 1 : (-1));
		if (num < 0)
		{
			num += 2;
		}
		num %= 2;
		this.SetStoryState(storyId, (StoryContentPanel.StoryState)num);
		this.mainScreen.RefreshRowsAndDescriptions();
	}

	// Token: 0x0600619D RID: 24989 RVA: 0x00240AB4 File Offset: 0x0023ECB4
	private void SetStoryState(string storyId, StoryContentPanel.StoryState state)
	{
		this.storyStates[storyId] = state;
		SettingConfig settingConfig = CustomGameSettings.Instance.StorySettings[storyId];
		CustomGameSettings.Instance.SetStorySetting(settingConfig, this.storyStates[storyId] == StoryContentPanel.StoryState.Guaranteed);
	}

	// Token: 0x0600619E RID: 24990 RVA: 0x00240AFC File Offset: 0x0023ECFC
	public void SelectRandomStories(int min = 3, int max = 3, bool useBias = false)
	{
		int num = UnityEngine.Random.Range(min, max);
		List<Story> list = new List<Story>(Db.Get().Stories.resources);
		List<Story> list2 = new List<Story>();
		list.Shuffle<Story>();
		int num2 = 0;
		while (num2 < num && list.Count - 1 >= num2)
		{
			list2.Add(list[num2]);
			num2++;
		}
		float num3 = 0.7f;
		int num4 = list2.Count((Story x) => x.IsNew());
		if (useBias && num4 == 0 && UnityEngine.Random.value < num3)
		{
			List<Story> list3 = Db.Get().Stories.resources.Where((Story x) => x.IsNew()).ToList<Story>();
			list3.Shuffle<Story>();
			if (list3.Count > 0)
			{
				list2.RemoveAt(0);
				list2.Add(list3[0]);
			}
		}
		foreach (Story story in list)
		{
			this.SetStoryState(story.Id, list2.Contains(story) ? StoryContentPanel.StoryState.Guaranteed : StoryContentPanel.StoryState.Forbidden);
		}
		this.RefreshAllStoryStates();
		this.mainScreen.RefreshRowsAndDescriptions();
	}

	// Token: 0x0600619F RID: 24991 RVA: 0x00240C60 File Offset: 0x0023EE60
	private void RefreshAllStoryStates()
	{
		foreach (string text in this.storyRows.Keys)
		{
			this.RefreshStoryDisplay(text);
		}
	}

	// Token: 0x060061A0 RID: 24992 RVA: 0x00240CB8 File Offset: 0x0023EEB8
	private void RefreshStoryDisplay(string id)
	{
		MultiToggle reference = this.storyRows[id].GetComponent<HierarchyReferences>().GetReference<MultiToggle>("checkbox");
		StoryContentPanel.StoryState storyState = this.storyStates[id];
		if (storyState == StoryContentPanel.StoryState.Forbidden)
		{
			reference.ChangeState(0);
			return;
		}
		if (storyState != StoryContentPanel.StoryState.Guaranteed)
		{
			return;
		}
		reference.ChangeState(1);
	}

	// Token: 0x060061A1 RID: 24993 RVA: 0x00240D08 File Offset: 0x0023EF08
	private void RefreshRows()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.storyRows)
		{
			keyValuePair.Value.GetComponent<MultiToggle>().ChangeState((keyValuePair.Key == this.selectedStoryId) ? 1 : 0);
		}
	}

	// Token: 0x060061A2 RID: 24994 RVA: 0x00240D80 File Offset: 0x0023EF80
	private void RefreshDescriptionPanel()
	{
		if (this.selectedStoryId.IsNullOrWhiteSpace())
		{
			this.selectedStoryTitleLabel.SetText("");
			this.selectedStoryDescriptionLabel.SetText("");
			return;
		}
		WorldTrait storyTrait = Db.Get().Stories.GetStoryTrait(this.selectedStoryId, true);
		this.selectedStoryTitleLabel.SetText(Strings.Get(storyTrait.name));
		this.selectedStoryDescriptionLabel.SetText(Strings.Get(storyTrait.description));
		string text = storyTrait.icon.Replace("_icon", "_image");
		this.selectedStoryImage.sprite = Assets.GetSprite(text);
	}

	// Token: 0x060061A3 RID: 24995 RVA: 0x00240E34 File Offset: 0x0023F034
	public string GetTraitsString(bool tooltip = false)
	{
		int num = 0;
		int num2 = 3;
		foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair in this.storyStates)
		{
			if (keyValuePair.Value == StoryContentPanel.StoryState.Guaranteed)
			{
				num++;
			}
		}
		string text = UI.FRONTEND.COLONYDESTINATIONSCREEN.STORY_TRAITS_HEADER;
		string text2;
		if (num != 0)
		{
			if (num != 1)
			{
				text2 = string.Format(UI.FRONTEND.COLONYDESTINATIONSCREEN.TRAIT_COUNT, num);
			}
			else
			{
				text2 = UI.FRONTEND.COLONYDESTINATIONSCREEN.SINGLE_TRAIT;
			}
		}
		else
		{
			text2 = UI.FRONTEND.COLONYDESTINATIONSCREEN.NO_TRAITS;
		}
		text = text + ": " + text2;
		if (num > num2)
		{
			text = text + " " + UI.FRONTEND.COLONYDESTINATIONSCREEN.TOO_MANY_TRAITS_WARNING;
		}
		if (tooltip)
		{
			foreach (KeyValuePair<string, StoryContentPanel.StoryState> keyValuePair2 in this.storyStates)
			{
				if (keyValuePair2.Value == StoryContentPanel.StoryState.Guaranteed)
				{
					WorldTrait storyTrait = Db.Get().Stories.Get(keyValuePair2.Key).StoryTrait;
					text = string.Concat(new string[]
					{
						text,
						"\n\n<b>",
						Strings.Get(storyTrait.name).String,
						"</b>\n",
						Strings.Get(storyTrait.description).String
					});
				}
			}
			if (num > num2)
			{
				text = text + "\n\n" + UI.FRONTEND.COLONYDESTINATIONSCREEN.TOO_MANY_TRAITS_WARNING_TOOLTIP;
			}
		}
		return text;
	}

	// Token: 0x04004370 RID: 17264
	[SerializeField]
	private GameObject storyRowPrefab;

	// Token: 0x04004371 RID: 17265
	[SerializeField]
	private GameObject storyRowContainer;

	// Token: 0x04004372 RID: 17266
	private Dictionary<string, GameObject> storyRows = new Dictionary<string, GameObject>();

	// Token: 0x04004373 RID: 17267
	public const int DEFAULT_RANDOMIZE_STORY_COUNT = 3;

	// Token: 0x04004374 RID: 17268
	private Dictionary<string, StoryContentPanel.StoryState> storyStates = new Dictionary<string, StoryContentPanel.StoryState>();

	// Token: 0x04004375 RID: 17269
	private string selectedStoryId = "";

	// Token: 0x04004376 RID: 17270
	[SerializeField]
	private ColonyDestinationSelectScreen mainScreen;

	// Token: 0x04004377 RID: 17271
	[Header("Trait Count")]
	[Header("SelectedStory")]
	[SerializeField]
	private Image selectedStoryImage;

	// Token: 0x04004378 RID: 17272
	[SerializeField]
	private LocText selectedStoryTitleLabel;

	// Token: 0x04004379 RID: 17273
	[SerializeField]
	private LocText selectedStoryDescriptionLabel;

	// Token: 0x0400437A RID: 17274
	[SerializeField]
	private Sprite spriteForbidden;

	// Token: 0x0400437B RID: 17275
	[SerializeField]
	private Sprite spritePossible;

	// Token: 0x0400437C RID: 17276
	[SerializeField]
	private Sprite spriteGuaranteed;

	// Token: 0x0400437D RID: 17277
	private StoryContentPanel.StoryState _defaultStoryState;

	// Token: 0x0400437E RID: 17278
	private List<string> storyTraitSettings = new List<string> { "None", "Few", "Lots" };

	// Token: 0x02001AA5 RID: 6821
	private enum StoryState
	{
		// Token: 0x04007826 RID: 30758
		Forbidden,
		// Token: 0x04007827 RID: 30759
		Guaranteed,
		// Token: 0x04007828 RID: 30760
		LENGTH
	}
}
