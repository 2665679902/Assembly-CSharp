using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000B6D RID: 2925
public class ResearchScreenSideBar : KScreen
{
	// Token: 0x06005B75 RID: 23413 RVA: 0x002146FC File Offset: 0x002128FC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.PopualteProjects();
		this.PopulateFilterButtons();
		this.RefreshCategoriesContentExpanded();
		this.RefreshWidgets();
		this.searchBox.onValueChanged.AddListener(new UnityAction<string>(this.UpdateCurrentSearch));
		KInputTextField kinputTextField = this.searchBox;
		kinputTextField.onFocus = (System.Action)Delegate.Combine(kinputTextField.onFocus, new System.Action(delegate
		{
			base.isEditing = true;
		}));
		this.searchBox.onEndEdit.AddListener(delegate(string value)
		{
			base.isEditing = false;
		});
		this.clearSearchButton.onClick += delegate
		{
			this.searchBox.text = "";
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.filterButtons)
			{
				this.filterStates[keyValuePair.Key] = false;
				this.filterButtons[keyValuePair.Key].GetComponent<MultiToggle>().ChangeState(this.filterStates[keyValuePair.Key] ? 1 : 0);
			}
		};
		this.ConfigCompletionFilters();
		base.ConsumeMouseScroll = true;
		Game.Instance.Subscribe(-107300940, new Action<object>(this.UpdateProjectFilter));
	}

	// Token: 0x06005B76 RID: 23414 RVA: 0x002147C8 File Offset: 0x002129C8
	private void Update()
	{
		for (int i = 0; i < Math.Min(this.QueuedActivations.Count, this.activationPerFrame); i++)
		{
			this.QueuedActivations[i].SetActive(true);
		}
		this.QueuedActivations.RemoveRange(0, Math.Min(this.QueuedActivations.Count, this.activationPerFrame));
		for (int j = 0; j < Math.Min(this.QueuedDeactivations.Count, this.activationPerFrame); j++)
		{
			this.QueuedDeactivations[j].SetActive(false);
		}
		this.QueuedDeactivations.RemoveRange(0, Math.Min(this.QueuedDeactivations.Count, this.activationPerFrame));
	}

	// Token: 0x06005B77 RID: 23415 RVA: 0x00214880 File Offset: 0x00212A80
	private void ConfigCompletionFilters()
	{
		MultiToggle multiToggle = this.allFilter;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(delegate
		{
			this.SetCompletionFilter(ResearchScreenSideBar.CompletionState.All);
		}));
		MultiToggle multiToggle2 = this.completedFilter;
		multiToggle2.onClick = (System.Action)Delegate.Combine(multiToggle2.onClick, new System.Action(delegate
		{
			this.SetCompletionFilter(ResearchScreenSideBar.CompletionState.Completed);
		}));
		MultiToggle multiToggle3 = this.availableFilter;
		multiToggle3.onClick = (System.Action)Delegate.Combine(multiToggle3.onClick, new System.Action(delegate
		{
			this.SetCompletionFilter(ResearchScreenSideBar.CompletionState.Available);
		}));
		this.SetCompletionFilter(ResearchScreenSideBar.CompletionState.All);
	}

	// Token: 0x06005B78 RID: 23416 RVA: 0x0021490C File Offset: 0x00212B0C
	private void SetCompletionFilter(ResearchScreenSideBar.CompletionState state)
	{
		this.completionFilter = state;
		this.allFilter.GetComponent<MultiToggle>().ChangeState((this.completionFilter == ResearchScreenSideBar.CompletionState.All) ? 1 : 0);
		this.completedFilter.GetComponent<MultiToggle>().ChangeState((this.completionFilter == ResearchScreenSideBar.CompletionState.Completed) ? 1 : 0);
		this.availableFilter.GetComponent<MultiToggle>().ChangeState((this.completionFilter == ResearchScreenSideBar.CompletionState.Available) ? 1 : 0);
		this.UpdateProjectFilter(null);
	}

	// Token: 0x06005B79 RID: 23417 RVA: 0x0021497D File Offset: 0x00212B7D
	public override float GetSortKey()
	{
		if (base.isEditing)
		{
			return 50f;
		}
		return 21f;
	}

	// Token: 0x06005B7A RID: 23418 RVA: 0x00214994 File Offset: 0x00212B94
	public override void OnKeyDown(KButtonEvent e)
	{
		if (this.researchScreen != null && this.researchScreen.canvas && !this.researchScreen.canvas.enabled)
		{
			return;
		}
		if (base.isEditing)
		{
			e.Consumed = true;
			return;
		}
		if (!e.Consumed)
		{
			Vector2 vector = base.transform.rectTransform().InverseTransformPoint(KInputManager.GetMousePos());
			if (vector.x >= 0f && vector.x <= base.transform.rectTransform().rect.width)
			{
				if (e.TryConsume(global::Action.MouseRight))
				{
					return;
				}
				if (e.TryConsume(global::Action.MouseLeft))
				{
					return;
				}
				if (!KInputManager.currentControllerIsGamepad)
				{
					if (e.TryConsume(global::Action.ZoomIn))
					{
						return;
					}
					e.TryConsume(global::Action.ZoomOut);
					return;
				}
			}
		}
	}

	// Token: 0x06005B7B RID: 23419 RVA: 0x00214A62 File Offset: 0x00212C62
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		this.RefreshWidgets();
	}

	// Token: 0x06005B7C RID: 23420 RVA: 0x00214A74 File Offset: 0x00212C74
	private void UpdateCurrentSearch(string newValue)
	{
		if (base.isEditing)
		{
			foreach (KeyValuePair<string, GameObject> keyValuePair in this.filterButtons)
			{
				this.filterStates[keyValuePair.Key] = false;
				keyValuePair.Value.GetComponent<MultiToggle>().ChangeState(0);
			}
		}
		this.currentSearchString = newValue;
		this.UpdateProjectFilter(null);
	}

	// Token: 0x06005B7D RID: 23421 RVA: 0x00214AFC File Offset: 0x00212CFC
	private void UpdateProjectFilter(object data = null)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.projectCategories)
		{
			dictionary.Add(keyValuePair.Key, false);
		}
		this.RefreshProjectsActive();
		foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.projectTechs)
		{
			if ((keyValuePair2.Value.activeSelf || this.QueuedActivations.Contains(keyValuePair2.Value)) && !this.QueuedDeactivations.Contains(keyValuePair2.Value))
			{
				dictionary[Db.Get().Techs.Get(keyValuePair2.Key).category] = true;
				this.categoryExpanded[Db.Get().Techs.Get(keyValuePair2.Key).category] = true;
			}
		}
		foreach (KeyValuePair<string, bool> keyValuePair3 in dictionary)
		{
			this.ChangeGameObjectActive(this.projectCategories[keyValuePair3.Key], keyValuePair3.Value);
		}
		this.RefreshCategoriesContentExpanded();
	}

	// Token: 0x06005B7E RID: 23422 RVA: 0x00214C80 File Offset: 0x00212E80
	private void RefreshProjectsActive()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.projectTechs)
		{
			bool flag = this.CheckTechPassesFilters(keyValuePair.Key);
			this.ChangeGameObjectActive(keyValuePair.Value, flag);
			this.researchScreen.GetEntry(Db.Get().Techs.Get(keyValuePair.Key)).UpdateFilterState(flag);
			foreach (KeyValuePair<string, GameObject> keyValuePair2 in this.projectTechItems[keyValuePair.Key])
			{
				bool flag2 = this.CheckTechItemPassesFilters(keyValuePair2.Key);
				HierarchyReferences component = keyValuePair2.Value.GetComponent<HierarchyReferences>();
				component.GetReference<LocText>("Label").color = (flag2 ? Color.white : Color.grey);
				component.GetReference<Image>("Icon").color = (flag2 ? Color.white : new Color(1f, 1f, 1f, 0.5f));
			}
		}
	}

	// Token: 0x06005B7F RID: 23423 RVA: 0x00214DE4 File Offset: 0x00212FE4
	private void RefreshCategoriesContentExpanded()
	{
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.projectCategories)
		{
			keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<RectTransform>("Content").gameObject.SetActive(this.categoryExpanded[keyValuePair.Key]);
			keyValuePair.Value.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").ChangeState(this.categoryExpanded[keyValuePair.Key] ? 1 : 0);
		}
	}

	// Token: 0x06005B80 RID: 23424 RVA: 0x00214E98 File Offset: 0x00213098
	private void PopualteProjects()
	{
		List<global::Tuple<global::Tuple<string, GameObject>, int>> list = new List<global::Tuple<global::Tuple<string, GameObject>, int>>();
		for (int i = 0; i < Db.Get().Techs.Count; i++)
		{
			Tech tech = (Tech)Db.Get().Techs.GetResource(i);
			if (!this.projectCategories.ContainsKey(tech.category))
			{
				string categoryID = tech.category;
				GameObject gameObject = Util.KInstantiateUI(this.techCategoryPrefabAlt, this.projectsContainer, true);
				gameObject.name = categoryID;
				gameObject.GetComponent<HierarchyReferences>().GetReference<LocText>("Label").SetText(Strings.Get("STRINGS.RESEARCH.TREES.TITLE" + categoryID.ToUpper()));
				this.categoryExpanded.Add(categoryID, false);
				this.projectCategories.Add(categoryID, gameObject);
				gameObject.GetComponent<HierarchyReferences>().GetReference<MultiToggle>("Toggle").onClick = delegate
				{
					this.categoryExpanded[categoryID] = !this.categoryExpanded[categoryID];
					this.RefreshCategoriesContentExpanded();
				};
			}
			GameObject gameObject2 = this.SpawnTechWidget(tech.Id, this.projectCategories[tech.category]);
			list.Add(new global::Tuple<global::Tuple<string, GameObject>, int>(new global::Tuple<string, GameObject>(tech.Id, gameObject2), tech.tier));
			this.projectTechs.Add(tech.Id, gameObject2);
			gameObject2.GetComponent<ToolTip>().SetSimpleTooltip(tech.desc);
			MultiToggle component = gameObject2.GetComponent<MultiToggle>();
			component.onEnter = (System.Action)Delegate.Combine(component.onEnter, new System.Action(delegate
			{
				this.researchScreen.TurnEverythingOff();
				this.researchScreen.GetEntry(tech).OnHover(true, tech);
			}));
			MultiToggle component2 = gameObject2.GetComponent<MultiToggle>();
			component2.onExit = (System.Action)Delegate.Combine(component2.onExit, new System.Action(delegate
			{
				this.researchScreen.TurnEverythingOff();
			}));
		}
		foreach (KeyValuePair<string, GameObject> keyValuePair in this.projectTechs)
		{
			Transform reference = this.projectCategories[Db.Get().Techs.Get(keyValuePair.Key).category].GetComponent<HierarchyReferences>().GetReference<Transform>("Content");
			this.projectTechs[keyValuePair.Key].transform.SetParent(reference);
		}
		list.Sort((global::Tuple<global::Tuple<string, GameObject>, int> a, global::Tuple<global::Tuple<string, GameObject>, int> b) => a.second.CompareTo(b.second));
		foreach (global::Tuple<global::Tuple<string, GameObject>, int> tuple in list)
		{
			tuple.first.second.transform.SetAsLastSibling();
		}
	}

	// Token: 0x06005B81 RID: 23425 RVA: 0x002151A8 File Offset: 0x002133A8
	private void PopulateFilterButtons()
	{
		using (Dictionary<string, List<Tag>>.Enumerator enumerator = this.filterPresets.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, List<Tag>> kvp = enumerator.Current;
				GameObject gameObject = Util.KInstantiateUI(this.filterButtonPrefab, this.searchFiltersContainer, true);
				this.filterButtons.Add(kvp.Key, gameObject);
				this.filterStates.Add(kvp.Key, false);
				MultiToggle toggle = gameObject.GetComponent<MultiToggle>();
				gameObject.GetComponentInChildren<LocText>().SetText(Strings.Get("STRINGS.UI.RESEARCHSCREEN.FILTER_BUTTONS." + kvp.Key.ToUpper()));
				MultiToggle toggle2 = toggle;
				toggle2.onClick = (System.Action)Delegate.Combine(toggle2.onClick, new System.Action(delegate
				{
					foreach (KeyValuePair<string, GameObject> keyValuePair in this.filterButtons)
					{
						if (keyValuePair.Key != kvp.Key)
						{
							this.filterStates[keyValuePair.Key] = false;
							this.filterButtons[keyValuePair.Key].GetComponent<MultiToggle>().ChangeState(this.filterStates[keyValuePair.Key] ? 1 : 0);
						}
					}
					this.filterStates[kvp.Key] = !this.filterStates[kvp.Key];
					toggle.ChangeState(this.filterStates[kvp.Key] ? 1 : 0);
					if (this.filterStates[kvp.Key])
					{
						StringEntry stringEntry = Strings.Get("STRINGS.UI.RESEARCHSCREEN.FILTER_BUTTONS." + kvp.Key.ToUpper());
						this.searchBox.text = stringEntry.String;
						return;
					}
					this.searchBox.text = "";
				}));
			}
		}
	}

	// Token: 0x06005B82 RID: 23426 RVA: 0x002152B0 File Offset: 0x002134B0
	public void RefreshQueue()
	{
	}

	// Token: 0x06005B83 RID: 23427 RVA: 0x002152B4 File Offset: 0x002134B4
	private void RefreshWidgets()
	{
		List<TechInstance> researchQueue = Research.Instance.GetResearchQueue();
		using (Dictionary<string, GameObject>.Enumerator enumerator = this.projectTechs.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				KeyValuePair<string, GameObject> kvp = enumerator.Current;
				if (Db.Get().Techs.Get(kvp.Key).IsComplete())
				{
					kvp.Value.GetComponent<MultiToggle>().ChangeState(2);
				}
				else if (researchQueue.Find((TechInstance match) => match.tech.Id == kvp.Key) != null)
				{
					kvp.Value.GetComponent<MultiToggle>().ChangeState(1);
				}
				else
				{
					kvp.Value.GetComponent<MultiToggle>().ChangeState(0);
				}
			}
		}
	}

	// Token: 0x06005B84 RID: 23428 RVA: 0x00215398 File Offset: 0x00213598
	private void RefreshWidgetProgressBars(string techID, GameObject widget)
	{
		HierarchyReferences component = widget.GetComponent<HierarchyReferences>();
		ResearchPointInventory progressInventory = Research.Instance.GetTechInstance(techID).progressInventory;
		int num = 0;
		for (int i = 0; i < Research.Instance.researchTypes.Types.Count; i++)
		{
			if (Research.Instance.GetTechInstance(techID).tech.costsByResearchTypeID.ContainsKey(Research.Instance.researchTypes.Types[i].id) && Research.Instance.GetTechInstance(techID).tech.costsByResearchTypeID[Research.Instance.researchTypes.Types[i].id] > 0f)
			{
				HierarchyReferences component2 = component.GetReference<RectTransform>("BarRows").GetChild(1 + num).GetComponent<HierarchyReferences>();
				float num2 = progressInventory.PointsByTypeID[Research.Instance.researchTypes.Types[i].id] / Research.Instance.GetTechInstance(techID).tech.costsByResearchTypeID[Research.Instance.researchTypes.Types[i].id];
				RectTransform rectTransform = component2.GetReference<Image>("Bar").rectTransform;
				rectTransform.sizeDelta = new Vector2(rectTransform.parent.rectTransform().rect.width * num2, rectTransform.sizeDelta.y);
				component2.GetReference<LocText>("Label").SetText(progressInventory.PointsByTypeID[Research.Instance.researchTypes.Types[i].id].ToString() + "/" + Research.Instance.GetTechInstance(techID).tech.costsByResearchTypeID[Research.Instance.researchTypes.Types[i].id].ToString());
				num++;
			}
		}
	}

	// Token: 0x06005B85 RID: 23429 RVA: 0x002155A0 File Offset: 0x002137A0
	private GameObject SpawnTechWidget(string techID, GameObject parentContainer)
	{
		GameObject gameObject = Util.KInstantiateUI(this.techWidgetRootAltPrefab, parentContainer, true);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		gameObject.name = Db.Get().Techs.Get(techID).Name;
		component.GetReference<LocText>("Label").SetText(Db.Get().Techs.Get(techID).Name);
		if (!this.projectTechItems.ContainsKey(techID))
		{
			this.projectTechItems.Add(techID, new Dictionary<string, GameObject>());
		}
		RectTransform reference = component.GetReference<RectTransform>("UnlockContainer");
		System.Action <>9__0;
		foreach (TechItem techItem in Db.Get().Techs.Get(techID).unlockedItems)
		{
			GameObject gameObject2 = Util.KInstantiateUI(this.techItemPrefab, reference.gameObject, true);
			gameObject2.GetComponentsInChildren<Image>()[1].sprite = techItem.UISprite();
			gameObject2.GetComponentsInChildren<LocText>()[0].SetText(techItem.Name);
			MultiToggle component2 = gameObject2.GetComponent<MultiToggle>();
			Delegate onClick = component2.onClick;
			System.Action action;
			if ((action = <>9__0) == null)
			{
				action = (<>9__0 = delegate
				{
					this.researchScreen.ZoomToTech(techID);
				});
			}
			component2.onClick = (System.Action)Delegate.Combine(onClick, action);
			gameObject2.GetComponentsInChildren<Image>()[0].color = (this.evenRow ? this.evenRowColor : this.oddRowColor);
			this.evenRow = !this.evenRow;
			if (!this.projectTechItems[techID].ContainsKey(techItem.Id))
			{
				this.projectTechItems[techID].Add(techItem.Id, gameObject2);
			}
		}
		MultiToggle component3 = gameObject.GetComponent<MultiToggle>();
		component3.onClick = (System.Action)Delegate.Combine(component3.onClick, new System.Action(delegate
		{
			this.researchScreen.ZoomToTech(techID);
		}));
		return gameObject;
	}

	// Token: 0x06005B86 RID: 23430 RVA: 0x002157D0 File Offset: 0x002139D0
	private void ChangeGameObjectActive(GameObject target, bool targetActiveState)
	{
		if (target.activeSelf != targetActiveState)
		{
			if (targetActiveState)
			{
				this.QueuedActivations.Add(target);
				if (this.QueuedDeactivations.Contains(target))
				{
					this.QueuedDeactivations.Remove(target);
					return;
				}
			}
			else
			{
				this.QueuedDeactivations.Add(target);
				if (this.QueuedActivations.Contains(target))
				{
					this.QueuedActivations.Remove(target);
				}
			}
		}
	}

	// Token: 0x06005B87 RID: 23431 RVA: 0x00215838 File Offset: 0x00213A38
	private bool CheckTechItemPassesFilters(string techItemID)
	{
		TechItem techItem = Db.Get().TechItems.Get(techItemID);
		bool flag = true;
		switch (this.completionFilter)
		{
		case ResearchScreenSideBar.CompletionState.Available:
			flag = flag && !techItem.IsComplete() && techItem.ParentTech.ArePrerequisitesComplete();
			break;
		case ResearchScreenSideBar.CompletionState.Completed:
			flag = flag && techItem.IsComplete();
			break;
		}
		if (!flag)
		{
			return flag;
		}
		flag = flag && ResearchScreen.TechItemPassesSearchFilter(techItemID, this.currentSearchString);
		foreach (KeyValuePair<string, bool> keyValuePair in this.filterStates)
		{
		}
		return flag;
	}

	// Token: 0x06005B88 RID: 23432 RVA: 0x002158F8 File Offset: 0x00213AF8
	private bool CheckTechPassesFilters(string techID)
	{
		Tech tech = Db.Get().Techs.Get(techID);
		bool flag = true;
		switch (this.completionFilter)
		{
		case ResearchScreenSideBar.CompletionState.Available:
			flag = flag && !tech.IsComplete() && tech.ArePrerequisitesComplete();
			break;
		case ResearchScreenSideBar.CompletionState.Completed:
			flag = flag && tech.IsComplete();
			break;
		}
		if (!flag)
		{
			return flag;
		}
		flag = flag && ResearchScreen.TechPassesSearchFilter(techID, this.currentSearchString);
		foreach (KeyValuePair<string, bool> keyValuePair in this.filterStates)
		{
		}
		return flag;
	}

	// Token: 0x04003E39 RID: 15929
	[Header("Containers")]
	[SerializeField]
	private GameObject queueContainer;

	// Token: 0x04003E3A RID: 15930
	[SerializeField]
	private GameObject projectsContainer;

	// Token: 0x04003E3B RID: 15931
	[SerializeField]
	private GameObject searchFiltersContainer;

	// Token: 0x04003E3C RID: 15932
	[Header("Prefabs")]
	[SerializeField]
	private GameObject headerTechTypePrefab;

	// Token: 0x04003E3D RID: 15933
	[SerializeField]
	private GameObject filterButtonPrefab;

	// Token: 0x04003E3E RID: 15934
	[SerializeField]
	private GameObject techWidgetRootPrefab;

	// Token: 0x04003E3F RID: 15935
	[SerializeField]
	private GameObject techWidgetRootAltPrefab;

	// Token: 0x04003E40 RID: 15936
	[SerializeField]
	private GameObject techItemPrefab;

	// Token: 0x04003E41 RID: 15937
	[SerializeField]
	private GameObject techWidgetUnlockedItemPrefab;

	// Token: 0x04003E42 RID: 15938
	[SerializeField]
	private GameObject techWidgetRowPrefab;

	// Token: 0x04003E43 RID: 15939
	[SerializeField]
	private GameObject techCategoryPrefab;

	// Token: 0x04003E44 RID: 15940
	[SerializeField]
	private GameObject techCategoryPrefabAlt;

	// Token: 0x04003E45 RID: 15941
	[Header("Other references")]
	[SerializeField]
	private KInputTextField searchBox;

	// Token: 0x04003E46 RID: 15942
	[SerializeField]
	private MultiToggle allFilter;

	// Token: 0x04003E47 RID: 15943
	[SerializeField]
	private MultiToggle availableFilter;

	// Token: 0x04003E48 RID: 15944
	[SerializeField]
	private MultiToggle completedFilter;

	// Token: 0x04003E49 RID: 15945
	[SerializeField]
	private ResearchScreen researchScreen;

	// Token: 0x04003E4A RID: 15946
	[SerializeField]
	private KButton clearSearchButton;

	// Token: 0x04003E4B RID: 15947
	[SerializeField]
	private Color evenRowColor;

	// Token: 0x04003E4C RID: 15948
	[SerializeField]
	private Color oddRowColor;

	// Token: 0x04003E4D RID: 15949
	private ResearchScreenSideBar.CompletionState completionFilter;

	// Token: 0x04003E4E RID: 15950
	private Dictionary<string, bool> filterStates = new Dictionary<string, bool>();

	// Token: 0x04003E4F RID: 15951
	private Dictionary<string, bool> categoryExpanded = new Dictionary<string, bool>();

	// Token: 0x04003E50 RID: 15952
	private string currentSearchString = "";

	// Token: 0x04003E51 RID: 15953
	private Dictionary<string, GameObject> queueTechs = new Dictionary<string, GameObject>();

	// Token: 0x04003E52 RID: 15954
	private Dictionary<string, GameObject> projectTechs = new Dictionary<string, GameObject>();

	// Token: 0x04003E53 RID: 15955
	private Dictionary<string, GameObject> projectCategories = new Dictionary<string, GameObject>();

	// Token: 0x04003E54 RID: 15956
	private Dictionary<string, GameObject> filterButtons = new Dictionary<string, GameObject>();

	// Token: 0x04003E55 RID: 15957
	private Dictionary<string, Dictionary<string, GameObject>> projectTechItems = new Dictionary<string, Dictionary<string, GameObject>>();

	// Token: 0x04003E56 RID: 15958
	private Dictionary<string, List<Tag>> filterPresets = new Dictionary<string, List<Tag>>
	{
		{
			"Oxygen",
			new List<Tag>()
		},
		{
			"Food",
			new List<Tag>()
		},
		{
			"Water",
			new List<Tag>()
		},
		{
			"Power",
			new List<Tag>()
		},
		{
			"Morale",
			new List<Tag>()
		},
		{
			"Ranching",
			new List<Tag>()
		},
		{
			"Filter",
			new List<Tag>()
		},
		{
			"Tile",
			new List<Tag>()
		},
		{
			"Transport",
			new List<Tag>()
		},
		{
			"Automation",
			new List<Tag>()
		},
		{
			"Medicine",
			new List<Tag>()
		},
		{
			"Rocket",
			new List<Tag>()
		}
	};

	// Token: 0x04003E57 RID: 15959
	private List<GameObject> QueuedActivations = new List<GameObject>();

	// Token: 0x04003E58 RID: 15960
	private List<GameObject> QueuedDeactivations = new List<GameObject>();

	// Token: 0x04003E59 RID: 15961
	[SerializeField]
	private int activationPerFrame = 5;

	// Token: 0x04003E5A RID: 15962
	private bool evenRow;

	// Token: 0x02001A16 RID: 6678
	private enum CompletionState
	{
		// Token: 0x04007676 RID: 30326
		All,
		// Token: 0x04007677 RID: 30327
		Available,
		// Token: 0x04007678 RID: 30328
		Completed
	}
}
