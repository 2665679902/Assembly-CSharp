using System;
using System.Collections;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000A5E RID: 2654
public class CodexScreen : KScreen
{
	// Token: 0x170005FB RID: 1531
	// (get) Token: 0x06005128 RID: 20776 RVA: 0x001D4E82 File Offset: 0x001D3082
	// (set) Token: 0x06005129 RID: 20777 RVA: 0x001D4E8A File Offset: 0x001D308A
	public string activeEntryID
	{
		get
		{
			return this._activeEntryID;
		}
		private set
		{
			this._activeEntryID = value;
		}
	}

	// Token: 0x0600512A RID: 20778 RVA: 0x001D4E94 File Offset: 0x001D3094
	protected override void OnActivate()
	{
		base.ConsumeMouseScroll = true;
		base.OnActivate();
		this.closeButton.onClick += delegate
		{
			ManagementMenu.Instance.CloseAll();
		};
		this.clearSearchButton.onClick += delegate
		{
			this.searchInputField.text = "";
		};
		if (string.IsNullOrEmpty(this.activeEntryID))
		{
			this.ChangeArticle("HOME", false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
		}
		this.searchInputField.onValueChanged.AddListener(delegate(string value)
		{
			this.FilterSearch(value);
		});
		KInputTextField kinputTextField = this.searchInputField;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(delegate
		{
			this.editingSearch = true;
		}));
		this.searchInputField.onEndEdit.AddListener(delegate(string value)
		{
			this.editingSearch = false;
		});
	}

	// Token: 0x0600512B RID: 20779 RVA: 0x001D4F71 File Offset: 0x001D3171
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.editingSearch)
		{
			e.Consumed = true;
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600512C RID: 20780 RVA: 0x001D4F89 File Offset: 0x001D3189
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x0600512D RID: 20781 RVA: 0x001D4F90 File Offset: 0x001D3190
	public void RefreshTutorialMessages()
	{
		if (!this.HasFocus)
		{
			return;
		}
		string text = CodexCache.FormatLinkID("MISCELLANEOUSTIPS");
		CodexEntry codexEntry;
		if (CodexCache.entries.TryGetValue(text, out codexEntry))
		{
			for (int i = 0; i < codexEntry.subEntries.Count; i++)
			{
				for (int j = 0; j < codexEntry.subEntries[i].contentContainers.Count; j++)
				{
					for (int k = 0; k < codexEntry.subEntries[i].contentContainers[j].content.Count; k++)
					{
						CodexText codexText = codexEntry.subEntries[i].contentContainers[j].content[k] as CodexText;
						if (codexText != null && codexText.messageID == MISC.NOTIFICATIONS.BASICCONTROLS.NAME)
						{
							if (KInputManager.currentControllerIsGamepad)
							{
								codexText.text = MISC.NOTIFICATIONS.BASICCONTROLS.MESSAGEBODYALT;
							}
							else
							{
								codexText.text = MISC.NOTIFICATIONS.BASICCONTROLS.MESSAGEBODY;
							}
							if (!string.IsNullOrEmpty(this.activeEntryID))
							{
								this.ChangeArticle("MISCELLANEOUSTIPS0", false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x0600512E RID: 20782 RVA: 0x001D50D4 File Offset: 0x001D32D4
	private void CodexScreenInit()
	{
		this.textStyles[CodexTextStyle.Title] = this.textStyleTitle;
		this.textStyles[CodexTextStyle.Subtitle] = this.textStyleSubtitle;
		this.textStyles[CodexTextStyle.Body] = this.textStyleBody;
		this.textStyles[CodexTextStyle.BodyWhite] = this.textStyleBodyWhite;
		this.SetupPrefabs();
		this.PopulatePools();
		this.CategorizeEntries();
		this.FilterSearch("");
		this.backButtonButton.onClick += this.HistoryStepBack;
		this.backButtonButton.soundPlayer.AcceptClickCondition = () => this.currentHistoryIdx > 0;
		this.fwdButtonButton.onClick += this.HistoryStepForward;
		this.fwdButtonButton.soundPlayer.AcceptClickCondition = () => this.currentHistoryIdx < this.history.Count - 1;
		Game.Instance.Subscribe(1594320620, delegate(object val)
		{
			if (!base.gameObject.activeSelf)
			{
				return;
			}
			this.FilterSearch(this.searchInputField.text);
			if (!string.IsNullOrEmpty(this.activeEntryID))
			{
				this.ChangeArticle(this.activeEntryID, false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
			}
		});
		KInputManager.InputChange.AddListener(new UnityAction(this.RefreshTutorialMessages));
	}

	// Token: 0x0600512F RID: 20783 RVA: 0x001D51E0 File Offset: 0x001D33E0
	private void SetupPrefabs()
	{
		this.contentContainerPool = new UIGameObjectPool(this.prefabContentContainer);
		this.contentContainerPool.disabledElementParent = this.widgetPool;
		this.ContentPrefabs[typeof(CodexText)] = this.prefabTextWidget;
		this.ContentPrefabs[typeof(CodexTextWithTooltip)] = this.prefabTextWithTooltipWidget;
		this.ContentPrefabs[typeof(CodexImage)] = this.prefabImageWidget;
		this.ContentPrefabs[typeof(CodexDividerLine)] = this.prefabDividerLineWidget;
		this.ContentPrefabs[typeof(CodexSpacer)] = this.prefabSpacer;
		this.ContentPrefabs[typeof(CodexLabelWithIcon)] = this.prefabLabelWithIcon;
		this.ContentPrefabs[typeof(CodexLabelWithLargeIcon)] = this.prefabLabelWithLargeIcon;
		this.ContentPrefabs[typeof(CodexContentLockedIndicator)] = this.prefabContentLocked;
		this.ContentPrefabs[typeof(CodexLargeSpacer)] = this.prefabLargeSpacer;
		this.ContentPrefabs[typeof(CodexVideo)] = this.prefabVideoWidget;
		this.ContentPrefabs[typeof(CodexIndentedLabelWithIcon)] = this.prefabIndentedLabelWithIcon;
		this.ContentPrefabs[typeof(CodexRecipePanel)] = this.prefabRecipePanel;
		this.ContentPrefabs[typeof(CodexConfigurableConsumerRecipePanel)] = this.PrefabConfigurableConsumerRecipePanel;
		this.ContentPrefabs[typeof(CodexConversionPanel)] = this.prefabConversionPanel;
		this.ContentPrefabs[typeof(CodexCollapsibleHeader)] = this.prefabCollapsibleHeader;
	}

	// Token: 0x06005130 RID: 20784 RVA: 0x001D53A4 File Offset: 0x001D35A4
	private List<CodexEntry> FilterSearch(string input)
	{
		this.searchResults.Clear();
		this.subEntrySearchResults.Clear();
		input = input.ToLower();
		foreach (KeyValuePair<string, CodexEntry> keyValuePair in CodexCache.entries)
		{
			bool flag = false;
			string[] dlcIds = keyValuePair.Value.GetDlcIds();
			for (int i = 0; i < dlcIds.Length; i++)
			{
				if (DlcManager.IsContentActive(dlcIds[i]))
				{
					flag = true;
					break;
				}
			}
			string[] forbiddenDLCs = keyValuePair.Value.GetForbiddenDLCs();
			for (int j = 0; j < forbiddenDLCs.Length; j++)
			{
				if (DlcManager.IsContentActive(forbiddenDLCs[j]))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				if (input == "")
				{
					if (!keyValuePair.Value.searchOnly)
					{
						this.searchResults.Add(keyValuePair.Value);
					}
				}
				else if (input == keyValuePair.Value.name.ToLower() || input.Contains(keyValuePair.Value.name.ToLower()) || keyValuePair.Value.name.ToLower().Contains(input))
				{
					this.searchResults.Add(keyValuePair.Value);
				}
			}
		}
		foreach (KeyValuePair<string, SubEntry> keyValuePair2 in CodexCache.subEntries)
		{
			if (input == keyValuePair2.Value.name.ToLower() || input.Contains(keyValuePair2.Value.name.ToLower()) || keyValuePair2.Value.name.ToLower().Contains(input))
			{
				this.subEntrySearchResults.Add(keyValuePair2.Value);
			}
		}
		this.FilterEntries(input != "");
		return this.searchResults;
	}

	// Token: 0x06005131 RID: 20785 RVA: 0x001D55D0 File Offset: 0x001D37D0
	private bool HasUnlockedCategoryEntries(string entryID)
	{
		foreach (ContentContainer contentContainer in CodexCache.entries[entryID].contentContainers)
		{
			if (string.IsNullOrEmpty(contentContainer.lockID) || Game.Instance.unlocks.IsUnlocked(contentContainer.lockID))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06005132 RID: 20786 RVA: 0x001D5654 File Offset: 0x001D3854
	private void FilterEntries(bool allowOpenCategories = true)
	{
		foreach (KeyValuePair<CodexEntry, GameObject> keyValuePair in this.entryButtons)
		{
			keyValuePair.Value.SetActive(this.searchResults.Contains(keyValuePair.Key) && this.HasUnlockedCategoryEntries(keyValuePair.Key.id));
		}
		foreach (KeyValuePair<SubEntry, GameObject> keyValuePair2 in this.subEntryButtons)
		{
			keyValuePair2.Value.SetActive(this.subEntrySearchResults.Contains(keyValuePair2.Key));
		}
		foreach (GameObject gameObject in this.categoryHeaders)
		{
			bool flag = false;
			Transform transform = gameObject.transform.Find("Content");
			for (int i = 0; i < transform.childCount; i++)
			{
				if (transform.GetChild(i).gameObject.activeSelf)
				{
					flag = true;
				}
			}
			gameObject.SetActive(flag);
			if (allowOpenCategories)
			{
				if (flag)
				{
					this.ToggleCategoryOpen(gameObject, true);
				}
			}
			else
			{
				this.ToggleCategoryOpen(gameObject, false);
			}
		}
	}

	// Token: 0x06005133 RID: 20787 RVA: 0x001D57D4 File Offset: 0x001D39D4
	private void ToggleCategoryOpen(GameObject header, bool open)
	{
		header.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("ExpandToggle").ChangeState(open ? 1 : 0);
		header.GetComponent<HierarchyReferences>().GetReference("Content").gameObject.SetActive(open);
	}

	// Token: 0x06005134 RID: 20788 RVA: 0x001D5810 File Offset: 0x001D3A10
	private void PopulatePools()
	{
		foreach (KeyValuePair<Type, GameObject> keyValuePair in this.ContentPrefabs)
		{
			UIGameObjectPool uigameObjectPool = new UIGameObjectPool(keyValuePair.Value);
			uigameObjectPool.disabledElementParent = this.widgetPool;
			this.ContentUIPools[keyValuePair.Key] = uigameObjectPool;
		}
	}

	// Token: 0x06005135 RID: 20789 RVA: 0x001D5888 File Offset: 0x001D3A88
	private GameObject NewCategoryHeader(KeyValuePair<string, CodexEntry> entryKVP, Dictionary<string, GameObject> categories)
	{
		if (entryKVP.Value.category == "")
		{
			entryKVP.Value.category = "Root";
		}
		GameObject categoryHeader = Util.KInstantiateUI(this.prefabCategoryHeader, this.navigatorContent.gameObject, true);
		GameObject categoryContent = categoryHeader.GetComponent<HierarchyReferences>().GetReference("Content").gameObject;
		categories.Add(entryKVP.Value.category, categoryContent);
		LocText reference = categoryHeader.GetComponent<HierarchyReferences>().GetReference<LocText>("Label");
		if (CodexCache.entries.ContainsKey(entryKVP.Value.category))
		{
			reference.text = CodexCache.entries[entryKVP.Value.category].name;
		}
		else
		{
			reference.text = Strings.Get("STRINGS.UI.CODEX.CATEGORYNAMES." + entryKVP.Value.category.ToUpper());
		}
		this.categoryHeaders.Add(categoryHeader);
		categoryContent.SetActive(false);
		categoryHeader.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("ExpandToggle").onClick = delegate
		{
			this.ToggleCategoryOpen(categoryHeader, !categoryContent.activeSelf);
		};
		return categoryHeader;
	}

	// Token: 0x06005136 RID: 20790 RVA: 0x001D59E8 File Offset: 0x001D3BE8
	private void CategorizeEntries()
	{
		GameObject gameObject = this.navigatorContent.gameObject;
		Dictionary<string, GameObject> dictionary = new Dictionary<string, GameObject>();
		List<global::Tuple<string, CodexEntry>> list = new List<global::Tuple<string, CodexEntry>>();
		foreach (KeyValuePair<string, CodexEntry> keyValuePair in CodexCache.entries)
		{
			if (string.IsNullOrEmpty(keyValuePair.Value.sortString))
			{
				keyValuePair.Value.sortString = UI.StripLinkFormatting(Strings.Get(keyValuePair.Value.title));
			}
			list.Add(new global::Tuple<string, CodexEntry>(keyValuePair.Key, keyValuePair.Value));
		}
		list.Sort((global::Tuple<string, CodexEntry> a, global::Tuple<string, CodexEntry> b) => string.Compare(a.second.sortString, b.second.sortString));
		for (int i = 0; i < list.Count; i++)
		{
			global::Tuple<string, CodexEntry> tuple = list[i];
			string text = tuple.second.category;
			if (text == "" || text == "Root")
			{
				text = "Root";
			}
			if (!dictionary.ContainsKey(text))
			{
				this.NewCategoryHeader(new KeyValuePair<string, CodexEntry>(tuple.first, tuple.second), dictionary);
			}
			GameObject gameObject2 = Util.KInstantiateUI(this.prefabNavigatorEntry, dictionary[text], true);
			string id = tuple.second.id;
			gameObject2.GetComponent<KButton>().onClick += delegate
			{
				this.ChangeArticle(id, false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
			};
			if (string.IsNullOrEmpty(tuple.second.name))
			{
				tuple.second.name = Strings.Get(tuple.second.title);
			}
			gameObject2.GetComponentInChildren<LocText>().text = tuple.second.name;
			this.entryButtons.Add(tuple.second, gameObject2);
			foreach (SubEntry subEntry in tuple.second.subEntries)
			{
				GameObject gameObject3 = Util.KInstantiateUI(this.prefabNavigatorEntry, dictionary[text], true);
				string subEntryId = subEntry.id;
				gameObject3.GetComponent<KButton>().onClick += delegate
				{
					this.ChangeArticle(subEntryId, false, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
				};
				if (string.IsNullOrEmpty(subEntry.name))
				{
					subEntry.name = Strings.Get(subEntry.title);
				}
				gameObject3.GetComponentInChildren<LocText>().text = subEntry.name;
				this.subEntryButtons.Add(subEntry, gameObject3);
				CodexCache.subEntries.Add(subEntry.id, subEntry);
			}
		}
		foreach (KeyValuePair<string, CodexEntry> keyValuePair2 in CodexCache.entries)
		{
			if (CodexCache.entries.ContainsKey(keyValuePair2.Value.category) && CodexCache.entries.ContainsKey(CodexCache.entries[keyValuePair2.Value.category].category))
			{
				keyValuePair2.Value.searchOnly = true;
			}
		}
		List<KeyValuePair<string, GameObject>> list2 = new List<KeyValuePair<string, GameObject>>();
		foreach (KeyValuePair<string, GameObject> keyValuePair3 in dictionary)
		{
			list2.Add(keyValuePair3);
		}
		list2.Sort((KeyValuePair<string, GameObject> a, KeyValuePair<string, GameObject> b) => string.Compare(a.Value.name, b.Value.name));
		for (int j = 0; j < list2.Count; j++)
		{
			list2[j].Value.transform.parent.SetSiblingIndex(j);
		}
		CodexScreen.SetupCategory(dictionary, "PLANTS");
		CodexScreen.SetupCategory(dictionary, "CREATURES");
		CodexScreen.SetupCategory(dictionary, "NOTICES");
		CodexScreen.SetupCategory(dictionary, "RESEARCHNOTES");
		CodexScreen.SetupCategory(dictionary, "JOURNALS");
		CodexScreen.SetupCategory(dictionary, "EMAILS");
		CodexScreen.SetupCategory(dictionary, "INVESTIGATIONS");
		CodexScreen.SetupCategory(dictionary, "MYLOG");
		CodexScreen.SetupCategory(dictionary, "LESSONS");
		CodexScreen.SetupCategory(dictionary, "Root");
	}

	// Token: 0x06005137 RID: 20791 RVA: 0x001D5E7C File Offset: 0x001D407C
	private static void SetupCategory(Dictionary<string, GameObject> categories, string category_name)
	{
		if (!categories.ContainsKey(category_name))
		{
			return;
		}
		categories[category_name].transform.parent.SetAsFirstSibling();
	}

	// Token: 0x06005138 RID: 20792 RVA: 0x001D5EA0 File Offset: 0x001D40A0
	public void ChangeArticle(string id, bool playClickSound = false, Vector3 targetPosition = default(Vector3), CodexScreen.HistoryDirection historyMovement = CodexScreen.HistoryDirection.NewArticle)
	{
		global::Debug.Assert(id != null);
		if (playClickSound)
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click", false));
		}
		if (this.contentContainerPool == null)
		{
			this.CodexScreenInit();
		}
		string text = "";
		SubEntry subEntry = null;
		if (!CodexCache.entries.ContainsKey(id))
		{
			subEntry = CodexCache.FindSubEntry(id);
			if (subEntry != null && !subEntry.disabled)
			{
				id = subEntry.parentEntryID.ToUpper();
				text = UI.StripLinkFormatting(subEntry.name);
			}
			else
			{
				id = "PAGENOTFOUND";
			}
		}
		if (CodexCache.entries[id].disabled)
		{
			id = "PAGENOTFOUND";
		}
		if (string.IsNullOrEmpty(text))
		{
			text = UI.StripLinkFormatting(CodexCache.entries[id].name);
		}
		ICodexWidget codexWidget = null;
		CodexCache.entries[id].GetFirstWidget();
		RectTransform rectTransform = null;
		if (subEntry != null)
		{
			foreach (ContentContainer contentContainer in CodexCache.entries[id].contentContainers)
			{
				if (contentContainer == subEntry.contentContainers[0])
				{
					codexWidget = contentContainer.content[0];
					break;
				}
			}
		}
		int num = 0;
		string text2 = "";
		while (this.contentContainers.transform.childCount > 0)
		{
			while (!string.IsNullOrEmpty(text2) && CodexCache.entries[this.activeEntryID].contentContainers[num].lockID == text2)
			{
				num++;
			}
			GameObject gameObject = this.contentContainers.transform.GetChild(0).gameObject;
			int num2 = 0;
			while (gameObject.transform.childCount > 0)
			{
				GameObject gameObject2 = gameObject.transform.GetChild(0).gameObject;
				Type type;
				if (gameObject2.name == "PrefabContentLocked")
				{
					text2 = CodexCache.entries[this.activeEntryID].contentContainers[num].lockID;
					type = typeof(CodexContentLockedIndicator);
				}
				else
				{
					type = CodexCache.entries[this.activeEntryID].contentContainers[num].content[num2].GetType();
				}
				this.ContentUIPools[type].ClearElement(gameObject2);
				num2++;
			}
			this.contentContainerPool.ClearElement(this.contentContainers.transform.GetChild(0).gameObject);
			num++;
		}
		bool flag = CodexCache.entries[id] is CategoryEntry;
		this.activeEntryID = id;
		if (CodexCache.entries[id].contentContainers == null)
		{
			CodexCache.entries[id].CreateContentContainerCollection();
		}
		bool flag2 = false;
		string text3 = "";
		for (int i = 0; i < CodexCache.entries[id].contentContainers.Count; i++)
		{
			ContentContainer contentContainer2 = CodexCache.entries[id].contentContainers[i];
			if (!string.IsNullOrEmpty(contentContainer2.lockID) && !Game.Instance.unlocks.IsUnlocked(contentContainer2.lockID))
			{
				if (text3 != contentContainer2.lockID)
				{
					GameObject gameObject3 = this.contentContainerPool.GetFreeElement(this.contentContainers.gameObject, true).gameObject;
					this.ConfigureContentContainer(contentContainer2, gameObject3, flag && flag2);
					text3 = contentContainer2.lockID;
					GameObject gameObject4 = this.ContentUIPools[typeof(CodexContentLockedIndicator)].GetFreeElement(gameObject3, true).gameObject;
				}
			}
			else
			{
				GameObject gameObject3 = this.contentContainerPool.GetFreeElement(this.contentContainers.gameObject, true).gameObject;
				this.ConfigureContentContainer(contentContainer2, gameObject3, flag && flag2);
				flag2 = !flag2;
				if (contentContainer2.content != null)
				{
					foreach (ICodexWidget codexWidget2 in contentContainer2.content)
					{
						GameObject gameObject5 = this.ContentUIPools[codexWidget2.GetType()].GetFreeElement(gameObject3, true).gameObject;
						codexWidget2.Configure(gameObject5, this.displayPane, this.textStyles);
						if (codexWidget2 == codexWidget)
						{
							rectTransform = gameObject5.rectTransform();
						}
					}
				}
			}
		}
		string text4 = "";
		string text5 = id;
		int num3 = 0;
		while (text5 != CodexCache.FormatLinkID("HOME") && num3 < 10)
		{
			num3++;
			if (text5 != null)
			{
				if (text5 != id)
				{
					text4 = text4.Insert(0, CodexCache.entries[text5].name + " > ");
				}
				else
				{
					text4 = text4.Insert(0, CodexCache.entries[text5].name);
				}
				text5 = CodexCache.entries[text5].parentId;
			}
			else
			{
				text5 = CodexCache.entries[CodexCache.FormatLinkID("HOME")].id;
				text4 = text4.Insert(0, CodexCache.entries[text5].name + " > ");
			}
		}
		this.currentLocationText.text = ((text4 == "") ? ("<b>" + UI.StripLinkFormatting(CodexCache.entries["HOME"].name) + "</b>") : text4);
		if (this.history.Count == 0)
		{
			this.history.Add(new CodexScreen.HistoryEntry(id, Vector3.zero, text));
			this.currentHistoryIdx = 0;
		}
		else if (historyMovement == CodexScreen.HistoryDirection.Back)
		{
			this.history[this.currentHistoryIdx].position = this.displayPane.transform.localPosition;
			this.currentHistoryIdx--;
		}
		else if (historyMovement == CodexScreen.HistoryDirection.Forward)
		{
			this.history[this.currentHistoryIdx].position = this.displayPane.transform.localPosition;
			this.currentHistoryIdx++;
		}
		else if (historyMovement == CodexScreen.HistoryDirection.NewArticle || historyMovement == CodexScreen.HistoryDirection.Up)
		{
			if (this.currentHistoryIdx == this.history.Count - 1)
			{
				this.history.Add(new CodexScreen.HistoryEntry(this.activeEntryID, Vector3.zero, text));
				this.history[this.currentHistoryIdx].position = this.displayPane.transform.localPosition;
				this.currentHistoryIdx++;
			}
			else
			{
				for (int j = this.history.Count - 1; j > this.currentHistoryIdx; j--)
				{
					this.history.RemoveAt(j);
				}
				this.history.Add(new CodexScreen.HistoryEntry(this.activeEntryID, Vector3.zero, text));
				this.history[this.history.Count - 2].position = this.displayPane.transform.localPosition;
				this.currentHistoryIdx++;
			}
		}
		if (this.currentHistoryIdx > 0)
		{
			this.backButtonButton.GetComponent<Image>().color = Color.black;
			this.backButton.text = UI.FormatAsLink(string.Format(UI.CODEX.BACK_BUTTON, UI.StripLinkFormatting(CodexCache.entries[this.history[this.history.Count - 2].id].name)), CodexCache.entries[this.history[this.history.Count - 2].id].id);
			this.backButtonButton.GetComponent<ToolTip>().toolTip = string.Format(UI.CODEX.BACK_BUTTON_TOOLTIP, this.history[this.currentHistoryIdx - 1].name);
		}
		else
		{
			this.backButtonButton.GetComponent<Image>().color = Color.grey;
			this.backButton.text = UI.StripLinkFormatting(GameUtil.ColourizeString(Color.grey, string.Format(UI.CODEX.BACK_BUTTON, CodexCache.entries["HOME"].name)));
			this.backButtonButton.GetComponent<ToolTip>().toolTip = UI.CODEX.BACK_BUTTON_NO_HISTORY_TOOLTIP;
		}
		if (this.currentHistoryIdx < this.history.Count - 1)
		{
			this.fwdButtonButton.GetComponent<Image>().color = Color.black;
			this.fwdButtonButton.GetComponent<ToolTip>().toolTip = string.Format(UI.CODEX.FORWARD_BUTTON_TOOLTIP, this.history[this.currentHistoryIdx + 1].name);
		}
		else
		{
			this.fwdButtonButton.GetComponent<Image>().color = Color.grey;
			this.fwdButtonButton.GetComponent<ToolTip>().toolTip = UI.CODEX.FORWARD_BUTTON_NO_HISTORY_TOOLTIP;
		}
		if (targetPosition != Vector3.zero)
		{
			if (this.scrollToTargetRoutine != null)
			{
				base.StopCoroutine(this.scrollToTargetRoutine);
			}
			this.scrollToTargetRoutine = base.StartCoroutine(this.ScrollToTarget(targetPosition));
			return;
		}
		if (rectTransform != null)
		{
			if (this.scrollToTargetRoutine != null)
			{
				base.StopCoroutine(this.scrollToTargetRoutine);
			}
			this.scrollToTargetRoutine = base.StartCoroutine(this.ScrollToTarget(rectTransform));
			return;
		}
		this.displayScrollRect.content.SetLocalPosition(Vector3.zero);
	}

	// Token: 0x06005139 RID: 20793 RVA: 0x001D6834 File Offset: 0x001D4A34
	private void HistoryStepBack()
	{
		if (this.currentHistoryIdx == 0)
		{
			return;
		}
		this.ChangeArticle(this.history[this.currentHistoryIdx - 1].id, false, this.history[this.currentHistoryIdx - 1].position, CodexScreen.HistoryDirection.Back);
	}

	// Token: 0x0600513A RID: 20794 RVA: 0x001D6884 File Offset: 0x001D4A84
	private void HistoryStepForward()
	{
		if (this.currentHistoryIdx == this.history.Count - 1)
		{
			return;
		}
		this.ChangeArticle(this.history[this.currentHistoryIdx + 1].id, false, this.history[this.currentHistoryIdx + 1].position, CodexScreen.HistoryDirection.Forward);
	}

	// Token: 0x0600513B RID: 20795 RVA: 0x001D68E0 File Offset: 0x001D4AE0
	private void HistoryStepUp()
	{
		if (string.IsNullOrEmpty(CodexCache.entries[this.activeEntryID].parentId))
		{
			return;
		}
		this.ChangeArticle(CodexCache.entries[this.activeEntryID].parentId, false, default(Vector3), CodexScreen.HistoryDirection.Up);
	}

	// Token: 0x0600513C RID: 20796 RVA: 0x001D6930 File Offset: 0x001D4B30
	private IEnumerator ScrollToTarget(RectTransform targetWidgetTransform)
	{
		yield return 0;
		this.displayScrollRect.content.SetLocalPosition(Vector3.down * (this.displayScrollRect.content.InverseTransformPoint(targetWidgetTransform.GetPosition()).y + 12f));
		this.scrollToTargetRoutine = null;
		yield break;
	}

	// Token: 0x0600513D RID: 20797 RVA: 0x001D6946 File Offset: 0x001D4B46
	private IEnumerator ScrollToTarget(Vector3 position)
	{
		yield return 0;
		this.displayScrollRect.content.SetLocalPosition(position);
		this.scrollToTargetRoutine = null;
		yield break;
	}

	// Token: 0x0600513E RID: 20798 RVA: 0x001D695C File Offset: 0x001D4B5C
	public void FocusContainer(ContentContainer target)
	{
		if (target == null || target.go == null)
		{
			return;
		}
		RectTransform rectTransform = target.go.transform.GetChild(0) as RectTransform;
		if (rectTransform == null)
		{
			return;
		}
		if (this.scrollToTargetRoutine != null)
		{
			base.StopCoroutine(this.scrollToTargetRoutine);
		}
		this.scrollToTargetRoutine = base.StartCoroutine(this.ScrollToTarget(rectTransform));
	}

	// Token: 0x0600513F RID: 20799 RVA: 0x001D69C4 File Offset: 0x001D4BC4
	private void ConfigureContentContainer(ContentContainer container, GameObject containerGameObject, bool bgColor = false)
	{
		container.go = containerGameObject;
		LayoutGroup layoutGroup = containerGameObject.GetComponent<LayoutGroup>();
		if (layoutGroup != null)
		{
			UnityEngine.Object.DestroyImmediate(layoutGroup);
		}
		if (!Game.Instance.unlocks.IsUnlocked(container.lockID) && !string.IsNullOrEmpty(container.lockID))
		{
			layoutGroup = containerGameObject.AddComponent<VerticalLayoutGroup>();
			(layoutGroup as HorizontalOrVerticalLayoutGroup).childForceExpandHeight = ((layoutGroup as HorizontalOrVerticalLayoutGroup).childForceExpandWidth = false);
			(layoutGroup as HorizontalOrVerticalLayoutGroup).spacing = 8f;
			return;
		}
		switch (container.contentLayout)
		{
		case ContentContainer.ContentLayout.Vertical:
			layoutGroup = containerGameObject.AddComponent<VerticalLayoutGroup>();
			(layoutGroup as HorizontalOrVerticalLayoutGroup).childForceExpandHeight = ((layoutGroup as HorizontalOrVerticalLayoutGroup).childForceExpandWidth = false);
			(layoutGroup as HorizontalOrVerticalLayoutGroup).spacing = 8f;
			return;
		case ContentContainer.ContentLayout.Horizontal:
			layoutGroup = containerGameObject.AddComponent<HorizontalLayoutGroup>();
			layoutGroup.childAlignment = TextAnchor.MiddleLeft;
			(layoutGroup as HorizontalOrVerticalLayoutGroup).childForceExpandHeight = ((layoutGroup as HorizontalOrVerticalLayoutGroup).childForceExpandWidth = false);
			(layoutGroup as HorizontalOrVerticalLayoutGroup).spacing = 8f;
			return;
		case ContentContainer.ContentLayout.Grid:
			layoutGroup = containerGameObject.AddComponent<GridLayoutGroup>();
			(layoutGroup as GridLayoutGroup).constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			(layoutGroup as GridLayoutGroup).constraintCount = 4;
			(layoutGroup as GridLayoutGroup).cellSize = new Vector2(128f, 180f);
			(layoutGroup as GridLayoutGroup).spacing = new Vector2(6f, 6f);
			return;
		case ContentContainer.ContentLayout.GridTwoColumn:
			layoutGroup = containerGameObject.AddComponent<GridLayoutGroup>();
			(layoutGroup as GridLayoutGroup).constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			(layoutGroup as GridLayoutGroup).constraintCount = 2;
			(layoutGroup as GridLayoutGroup).cellSize = new Vector2(264f, 32f);
			(layoutGroup as GridLayoutGroup).spacing = new Vector2(0f, 12f);
			return;
		case ContentContainer.ContentLayout.GridTwoColumnTall:
			layoutGroup = containerGameObject.AddComponent<GridLayoutGroup>();
			(layoutGroup as GridLayoutGroup).constraint = GridLayoutGroup.Constraint.FixedColumnCount;
			(layoutGroup as GridLayoutGroup).constraintCount = 2;
			(layoutGroup as GridLayoutGroup).cellSize = new Vector2(264f, 64f);
			(layoutGroup as GridLayoutGroup).spacing = new Vector2(0f, 12f);
			return;
		default:
			return;
		}
	}

	// Token: 0x04003666 RID: 13926
	private string _activeEntryID;

	// Token: 0x04003667 RID: 13927
	private Dictionary<Type, UIGameObjectPool> ContentUIPools = new Dictionary<Type, UIGameObjectPool>();

	// Token: 0x04003668 RID: 13928
	private Dictionary<Type, GameObject> ContentPrefabs = new Dictionary<Type, GameObject>();

	// Token: 0x04003669 RID: 13929
	private List<GameObject> categoryHeaders = new List<GameObject>();

	// Token: 0x0400366A RID: 13930
	private Dictionary<CodexEntry, GameObject> entryButtons = new Dictionary<CodexEntry, GameObject>();

	// Token: 0x0400366B RID: 13931
	private Dictionary<SubEntry, GameObject> subEntryButtons = new Dictionary<SubEntry, GameObject>();

	// Token: 0x0400366C RID: 13932
	private UIGameObjectPool contentContainerPool;

	// Token: 0x0400366D RID: 13933
	[SerializeField]
	private KScrollRect displayScrollRect;

	// Token: 0x0400366E RID: 13934
	[SerializeField]
	private RectTransform scrollContentPane;

	// Token: 0x0400366F RID: 13935
	private bool editingSearch;

	// Token: 0x04003670 RID: 13936
	private List<CodexScreen.HistoryEntry> history = new List<CodexScreen.HistoryEntry>();

	// Token: 0x04003671 RID: 13937
	private int currentHistoryIdx;

	// Token: 0x04003672 RID: 13938
	[Header("Hierarchy")]
	[SerializeField]
	private Transform navigatorContent;

	// Token: 0x04003673 RID: 13939
	[SerializeField]
	private Transform displayPane;

	// Token: 0x04003674 RID: 13940
	[SerializeField]
	private Transform contentContainers;

	// Token: 0x04003675 RID: 13941
	[SerializeField]
	private Transform widgetPool;

	// Token: 0x04003676 RID: 13942
	[SerializeField]
	private KButton closeButton;

	// Token: 0x04003677 RID: 13943
	[SerializeField]
	private KInputTextField searchInputField;

	// Token: 0x04003678 RID: 13944
	[SerializeField]
	private KButton clearSearchButton;

	// Token: 0x04003679 RID: 13945
	[SerializeField]
	private LocText backButton;

	// Token: 0x0400367A RID: 13946
	[SerializeField]
	private KButton backButtonButton;

	// Token: 0x0400367B RID: 13947
	[SerializeField]
	private KButton fwdButtonButton;

	// Token: 0x0400367C RID: 13948
	[SerializeField]
	private LocText currentLocationText;

	// Token: 0x0400367D RID: 13949
	[Header("Prefabs")]
	[SerializeField]
	private GameObject prefabNavigatorEntry;

	// Token: 0x0400367E RID: 13950
	[SerializeField]
	private GameObject prefabCategoryHeader;

	// Token: 0x0400367F RID: 13951
	[SerializeField]
	private GameObject prefabContentContainer;

	// Token: 0x04003680 RID: 13952
	[SerializeField]
	private GameObject prefabTextWidget;

	// Token: 0x04003681 RID: 13953
	[SerializeField]
	private GameObject prefabTextWithTooltipWidget;

	// Token: 0x04003682 RID: 13954
	[SerializeField]
	private GameObject prefabImageWidget;

	// Token: 0x04003683 RID: 13955
	[SerializeField]
	private GameObject prefabDividerLineWidget;

	// Token: 0x04003684 RID: 13956
	[SerializeField]
	private GameObject prefabSpacer;

	// Token: 0x04003685 RID: 13957
	[SerializeField]
	private GameObject prefabLargeSpacer;

	// Token: 0x04003686 RID: 13958
	[SerializeField]
	private GameObject prefabLabelWithIcon;

	// Token: 0x04003687 RID: 13959
	[SerializeField]
	private GameObject prefabLabelWithLargeIcon;

	// Token: 0x04003688 RID: 13960
	[SerializeField]
	private GameObject prefabContentLocked;

	// Token: 0x04003689 RID: 13961
	[SerializeField]
	private GameObject prefabVideoWidget;

	// Token: 0x0400368A RID: 13962
	[SerializeField]
	private GameObject prefabIndentedLabelWithIcon;

	// Token: 0x0400368B RID: 13963
	[SerializeField]
	private GameObject prefabRecipePanel;

	// Token: 0x0400368C RID: 13964
	[SerializeField]
	private GameObject PrefabConfigurableConsumerRecipePanel;

	// Token: 0x0400368D RID: 13965
	[SerializeField]
	private GameObject prefabConversionPanel;

	// Token: 0x0400368E RID: 13966
	[SerializeField]
	private GameObject prefabCollapsibleHeader;

	// Token: 0x0400368F RID: 13967
	[Header("Text Styles")]
	[SerializeField]
	private TextStyleSetting textStyleTitle;

	// Token: 0x04003690 RID: 13968
	[SerializeField]
	private TextStyleSetting textStyleSubtitle;

	// Token: 0x04003691 RID: 13969
	[SerializeField]
	private TextStyleSetting textStyleBody;

	// Token: 0x04003692 RID: 13970
	[SerializeField]
	private TextStyleSetting textStyleBodyWhite;

	// Token: 0x04003693 RID: 13971
	private Dictionary<CodexTextStyle, TextStyleSetting> textStyles = new Dictionary<CodexTextStyle, TextStyleSetting>();

	// Token: 0x04003694 RID: 13972
	private List<CodexEntry> searchResults = new List<CodexEntry>();

	// Token: 0x04003695 RID: 13973
	private List<SubEntry> subEntrySearchResults = new List<SubEntry>();

	// Token: 0x04003696 RID: 13974
	private Coroutine scrollToTargetRoutine;

	// Token: 0x020018F0 RID: 6384
	public enum PlanCategory
	{
		// Token: 0x040072CF RID: 29391
		Home,
		// Token: 0x040072D0 RID: 29392
		Tips,
		// Token: 0x040072D1 RID: 29393
		MyLog,
		// Token: 0x040072D2 RID: 29394
		Investigations,
		// Token: 0x040072D3 RID: 29395
		Emails,
		// Token: 0x040072D4 RID: 29396
		Journals,
		// Token: 0x040072D5 RID: 29397
		ResearchNotes,
		// Token: 0x040072D6 RID: 29398
		Creatures,
		// Token: 0x040072D7 RID: 29399
		Plants,
		// Token: 0x040072D8 RID: 29400
		Food,
		// Token: 0x040072D9 RID: 29401
		Tech,
		// Token: 0x040072DA RID: 29402
		Diseases,
		// Token: 0x040072DB RID: 29403
		Roles,
		// Token: 0x040072DC RID: 29404
		Buildings,
		// Token: 0x040072DD RID: 29405
		Elements
	}

	// Token: 0x020018F1 RID: 6385
	public enum HistoryDirection
	{
		// Token: 0x040072DF RID: 29407
		Back,
		// Token: 0x040072E0 RID: 29408
		Forward,
		// Token: 0x040072E1 RID: 29409
		Up,
		// Token: 0x040072E2 RID: 29410
		NewArticle
	}

	// Token: 0x020018F2 RID: 6386
	public class HistoryEntry
	{
		// Token: 0x06008ECE RID: 36558 RVA: 0x0030EE0F File Offset: 0x0030D00F
		public HistoryEntry(string entry, Vector3 pos, string articleName)
		{
			this.id = entry;
			this.position = pos;
			this.name = articleName;
		}

		// Token: 0x040072E3 RID: 29411
		public string id;

		// Token: 0x040072E4 RID: 29412
		public Vector3 position;

		// Token: 0x040072E5 RID: 29413
		public string name;
	}
}
