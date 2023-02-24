using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Database;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x020009F6 RID: 2550
public class CharacterContainer : KScreen, ITelepadDeliverableContainer
{
	// Token: 0x06004C67 RID: 19559 RVA: 0x001AD6FA File Offset: 0x001AB8FA
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x170005BF RID: 1471
	// (get) Token: 0x06004C68 RID: 19560 RVA: 0x001AD702 File Offset: 0x001AB902
	public MinionStartingStats Stats
	{
		get
		{
			return this.stats;
		}
	}

	// Token: 0x06004C69 RID: 19561 RVA: 0x001AD70C File Offset: 0x001AB90C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Initialize();
		this.characterNameTitle.OnStartedEditing += this.OnStartedEditing;
		this.characterNameTitle.OnNameChanged += this.OnNameChanged;
		this.reshuffleButton.onClick += delegate
		{
			this.Reshuffle(true);
		};
		List<IListableOption> list = new List<IListableOption>();
		foreach (SkillGroup skillGroup in new List<SkillGroup>(Db.Get().SkillGroups.resources))
		{
			list.Add(skillGroup);
		}
		this.archetypeDropDown.Initialize(list, new Action<IListableOption, object>(this.OnArchetypeEntryClick), new Func<IListableOption, IListableOption, object, int>(this.archetypeDropDownSort), new Action<DropDownEntry, object>(this.archetypeDropEntryRefreshAction), false, null);
		this.archetypeDropDown.CustomizeEmptyRow(Strings.Get("STRINGS.UI.CHARACTERCONTAINER_NOARCHETYPESELECTED"), this.noArchetypeIcon);
		base.StartCoroutine(this.DelayedGeneration());
	}

	// Token: 0x06004C6A RID: 19562 RVA: 0x001AD824 File Offset: 0x001ABA24
	public void ForceStopEditingTitle()
	{
		this.characterNameTitle.ForceStopEditing();
	}

	// Token: 0x06004C6B RID: 19563 RVA: 0x001AD831 File Offset: 0x001ABA31
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06004C6C RID: 19564 RVA: 0x001AD838 File Offset: 0x001ABA38
	private IEnumerator DelayedGeneration()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		this.GenerateCharacter(this.controller.IsStarterMinion, null);
		yield break;
	}

	// Token: 0x06004C6D RID: 19565 RVA: 0x001AD847 File Offset: 0x001ABA47
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.animController != null)
		{
			this.animController.gameObject.DeleteObject();
			this.animController = null;
		}
	}

	// Token: 0x06004C6E RID: 19566 RVA: 0x001AD874 File Offset: 0x001ABA74
	protected override void OnForcedCleanUp()
	{
		CharacterContainer.containers.Remove(this);
		base.OnForcedCleanUp();
	}

	// Token: 0x06004C6F RID: 19567 RVA: 0x001AD888 File Offset: 0x001ABA88
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.controller != null)
		{
			CharacterSelectionController characterSelectionController = this.controller;
			characterSelectionController.OnLimitReachedEvent = (System.Action)Delegate.Remove(characterSelectionController.OnLimitReachedEvent, new System.Action(this.OnCharacterSelectionLimitReached));
			CharacterSelectionController characterSelectionController2 = this.controller;
			characterSelectionController2.OnLimitUnreachedEvent = (System.Action)Delegate.Remove(characterSelectionController2.OnLimitUnreachedEvent, new System.Action(this.OnCharacterSelectionLimitUnReached));
			CharacterSelectionController characterSelectionController3 = this.controller;
			characterSelectionController3.OnReshuffleEvent = (Action<bool>)Delegate.Remove(characterSelectionController3.OnReshuffleEvent, new Action<bool>(this.Reshuffle));
		}
	}

	// Token: 0x06004C70 RID: 19568 RVA: 0x001AD920 File Offset: 0x001ABB20
	private void Initialize()
	{
		this.iconGroups = new List<GameObject>();
		this.traitEntries = new List<GameObject>();
		this.expectationLabels = new List<LocText>();
		this.aptitudeEntries = new List<GameObject>();
		if (CharacterContainer.containers == null)
		{
			CharacterContainer.containers = new List<CharacterContainer>();
		}
		CharacterContainer.containers.Add(this);
	}

	// Token: 0x06004C71 RID: 19569 RVA: 0x001AD975 File Offset: 0x001ABB75
	private void OnNameChanged(string newName)
	{
		this.stats.Name = newName;
		this.stats.personality.Name = newName;
		this.description.text = this.stats.personality.description;
	}

	// Token: 0x06004C72 RID: 19570 RVA: 0x001AD9AF File Offset: 0x001ABBAF
	private void OnStartedEditing()
	{
		KScreenManager.Instance.RefreshStack();
	}

	// Token: 0x06004C73 RID: 19571 RVA: 0x001AD9BC File Offset: 0x001ABBBC
	private void GenerateCharacter(bool is_starter, string guaranteedAptitudeID = null)
	{
		int num = 0;
		do
		{
			this.stats = new MinionStartingStats(is_starter, guaranteedAptitudeID, null, false);
			num++;
		}
		while (this.IsCharacterRedundant() && num < 20);
		if (this.animController != null)
		{
			UnityEngine.Object.Destroy(this.animController.gameObject);
			this.animController = null;
		}
		this.SetAnimator();
		this.SetInfoText();
		base.StartCoroutine(this.SetAttributes());
		this.selectButton.ClearOnClick();
		if (!this.controller.IsStarterMinion)
		{
			this.selectButton.enabled = true;
			this.selectButton.onClick += delegate
			{
				this.SelectDeliverable();
			};
		}
	}

	// Token: 0x06004C74 RID: 19572 RVA: 0x001ADA64 File Offset: 0x001ABC64
	private void SetAnimator()
	{
		if (this.animController == null)
		{
			this.animController = Util.KInstantiateUI(Assets.GetPrefab(new Tag("MinionSelectPreview")), this.contentBody.gameObject, false).GetComponent<KBatchedAnimController>();
			this.animController.gameObject.SetActive(true);
			this.animController.animScale = this.baseCharacterScale;
		}
		MinionConfig.ConfigureSymbols(this.animController.gameObject, true);
		this.stats.ApplyTraits(this.animController.gameObject);
		this.stats.ApplyRace(this.animController.gameObject);
		this.stats.ApplyAccessories(this.animController.gameObject);
		this.stats.ApplyOutfit(this.stats.personality, this.animController.gameObject);
		this.stats.ApplyJoyResponseOutfit(this.stats.personality, this.animController.gameObject);
		this.stats.ApplyExperience(this.animController.gameObject);
		HashedString idleAnim = this.GetIdleAnim(this.stats);
		this.idle_anim = Assets.GetAnim(idleAnim);
		if (this.idle_anim != null)
		{
			this.animController.AddAnimOverrides(this.idle_anim, 0f);
		}
		KAnimFile anim = Assets.GetAnim(new HashedString("crewSelect_fx_kanim"));
		if (anim != null)
		{
			this.animController.AddAnimOverrides(anim, 0f);
		}
		this.animController.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06004C75 RID: 19573 RVA: 0x001ADC00 File Offset: 0x001ABE00
	private HashedString GetIdleAnim(MinionStartingStats minionStartingStats)
	{
		List<HashedString> list = new List<HashedString>();
		foreach (KeyValuePair<HashedString, string[]> keyValuePair in CharacterContainer.traitIdleAnims)
		{
			foreach (Trait trait in minionStartingStats.Traits)
			{
				if (keyValuePair.Value.Contains(trait.Id))
				{
					list.Add(keyValuePair.Key);
				}
			}
			if (keyValuePair.Value.Contains(minionStartingStats.joyTrait.Id) || keyValuePair.Value.Contains(minionStartingStats.stressTrait.Id))
			{
				list.Add(keyValuePair.Key);
			}
		}
		if (list.Count > 0)
		{
			return list.ToArray()[UnityEngine.Random.Range(0, list.Count)];
		}
		return CharacterContainer.idleAnims[UnityEngine.Random.Range(0, CharacterContainer.idleAnims.Length)];
	}

	// Token: 0x06004C76 RID: 19574 RVA: 0x001ADD2C File Offset: 0x001ABF2C
	private void SetInfoText()
	{
		this.traitEntries.ForEach(delegate(GameObject tl)
		{
			UnityEngine.Object.Destroy(tl.gameObject);
		});
		this.traitEntries.Clear();
		this.characterNameTitle.SetTitle(this.stats.Name);
		for (int i = 1; i < this.stats.Traits.Count; i++)
		{
			Trait trait = this.stats.Traits[i];
			LocText locText = (trait.PositiveTrait ? this.goodTrait : this.badTrait);
			LocText locText2 = Util.KInstantiateUI<LocText>(locText.gameObject, locText.transform.parent.gameObject, false);
			locText2.gameObject.SetActive(true);
			locText2.text = this.stats.Traits[i].Name;
			locText2.color = (trait.PositiveTrait ? Constants.POSITIVE_COLOR : Constants.NEGATIVE_COLOR);
			locText2.GetComponent<ToolTip>().SetSimpleTooltip(trait.GetTooltip());
			for (int j = 0; j < trait.SelfModifiers.Count; j++)
			{
				GameObject gameObject = Util.KInstantiateUI(this.attributeLabelTrait.gameObject, locText.transform.parent.gameObject, false);
				gameObject.SetActive(true);
				LocText componentInChildren = gameObject.GetComponentInChildren<LocText>();
				string text = ((trait.SelfModifiers[j].Value > 0f) ? UI.CHARACTERCONTAINER_ATTRIBUTEMODIFIER_INCREASED : UI.CHARACTERCONTAINER_ATTRIBUTEMODIFIER_DECREASED);
				componentInChildren.text = string.Format(text, Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + trait.SelfModifiers[j].AttributeId.ToUpper() + ".NAME"));
				trait.SelfModifiers[j].AttributeId == "GermResistance";
				Klei.AI.Attribute attribute = Db.Get().Attributes.Get(trait.SelfModifiers[j].AttributeId);
				string text2 = attribute.Description;
				text2 = string.Concat(new string[]
				{
					text2,
					"\n\n",
					Strings.Get("STRINGS.DUPLICANTS.ATTRIBUTES." + trait.SelfModifiers[j].AttributeId.ToUpper() + ".NAME"),
					": ",
					trait.SelfModifiers[j].GetFormattedString()
				});
				List<AttributeConverter> convertersForAttribute = Db.Get().AttributeConverters.GetConvertersForAttribute(attribute);
				for (int k = 0; k < convertersForAttribute.Count; k++)
				{
					string text3 = convertersForAttribute[k].DescriptionFromAttribute(convertersForAttribute[k].multiplier * trait.SelfModifiers[j].Value, null);
					if (text3 != "")
					{
						text2 = text2 + "\n    • " + text3;
					}
				}
				componentInChildren.GetComponent<ToolTip>().SetSimpleTooltip(text2);
				this.traitEntries.Add(gameObject);
			}
			if (trait.disabledChoreGroups != null)
			{
				GameObject gameObject2 = Util.KInstantiateUI(this.attributeLabelTrait.gameObject, locText.transform.parent.gameObject, false);
				gameObject2.SetActive(true);
				LocText componentInChildren2 = gameObject2.GetComponentInChildren<LocText>();
				componentInChildren2.text = trait.GetDisabledChoresString(false);
				string text4 = "";
				string text5 = "";
				for (int l = 0; l < trait.disabledChoreGroups.Length; l++)
				{
					if (l > 0)
					{
						text4 += ", ";
						text5 += "\n";
					}
					text4 += trait.disabledChoreGroups[l].Name;
					text5 += trait.disabledChoreGroups[l].description;
				}
				componentInChildren2.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(DUPLICANTS.TRAITS.CANNOT_DO_TASK_TOOLTIP, text4, text5));
				this.traitEntries.Add(gameObject2);
			}
			if (trait.ignoredEffects != null && trait.ignoredEffects.Length != 0)
			{
				GameObject gameObject3 = Util.KInstantiateUI(this.attributeLabelTrait.gameObject, locText.transform.parent.gameObject, false);
				gameObject3.SetActive(true);
				LocText componentInChildren3 = gameObject3.GetComponentInChildren<LocText>();
				componentInChildren3.text = trait.GetIgnoredEffectsString(false);
				string text6 = "";
				string text7 = "";
				for (int m = 0; m < trait.ignoredEffects.Length; m++)
				{
					if (m > 0)
					{
						text6 += ", ";
						text7 += "\n";
					}
					text6 += Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + trait.ignoredEffects[m].ToUpper() + ".NAME");
					text7 += Strings.Get("STRINGS.DUPLICANTS.MODIFIERS." + trait.ignoredEffects[m].ToUpper() + ".CAUSE");
				}
				componentInChildren3.GetComponent<ToolTip>().SetSimpleTooltip(string.Format(DUPLICANTS.TRAITS.IGNORED_EFFECTS_TOOLTIP, text6, text7));
				this.traitEntries.Add(gameObject3);
			}
			StringEntry stringEntry;
			if (Strings.TryGet("STRINGS.DUPLICANTS.TRAITS." + trait.Id.ToUpper() + ".SHORT_DESC", out stringEntry))
			{
				GameObject gameObject4 = Util.KInstantiateUI(this.attributeLabelTrait.gameObject, locText.transform.parent.gameObject, false);
				gameObject4.SetActive(true);
				LocText componentInChildren4 = gameObject4.GetComponentInChildren<LocText>();
				componentInChildren4.text = stringEntry.String;
				componentInChildren4.GetComponent<ToolTip>().SetSimpleTooltip(Strings.Get("STRINGS.DUPLICANTS.TRAITS." + trait.Id.ToUpper() + ".SHORT_DESC_TOOLTIP"));
				this.traitEntries.Add(gameObject4);
			}
			this.traitEntries.Add(locText2.gameObject);
		}
		this.aptitudeEntries.ForEach(delegate(GameObject al)
		{
			UnityEngine.Object.Destroy(al.gameObject);
		});
		this.aptitudeEntries.Clear();
		this.expectationLabels.ForEach(delegate(LocText el)
		{
			UnityEngine.Object.Destroy(el.gameObject);
		});
		this.expectationLabels.Clear();
		foreach (KeyValuePair<SkillGroup, float> keyValuePair in this.stats.skillAptitudes)
		{
			if (keyValuePair.Value != 0f)
			{
				SkillGroup skillGroup = Db.Get().SkillGroups.Get(keyValuePair.Key.IdHash);
				if (skillGroup == null)
				{
					global::Debug.LogWarningFormat("Role group not found for aptitude: {0}", new object[] { keyValuePair.Key });
				}
				else
				{
					GameObject gameObject5 = Util.KInstantiateUI(this.aptitudeEntry.gameObject, this.aptitudeEntry.transform.parent.gameObject, false);
					LocText locText3 = Util.KInstantiateUI<LocText>(this.aptitudeLabel.gameObject, gameObject5, false);
					locText3.gameObject.SetActive(true);
					locText3.text = skillGroup.Name;
					string text8;
					if (skillGroup.choreGroupID != "")
					{
						ChoreGroup choreGroup = Db.Get().ChoreGroups.Get(skillGroup.choreGroupID);
						text8 = string.Format(DUPLICANTS.ROLES.GROUPS.APTITUDE_DESCRIPTION_CHOREGROUP, skillGroup.Name, DUPLICANTSTATS.APTITUDE_BONUS, choreGroup.description);
					}
					else
					{
						text8 = string.Format(DUPLICANTS.ROLES.GROUPS.APTITUDE_DESCRIPTION, skillGroup.Name, DUPLICANTSTATS.APTITUDE_BONUS);
					}
					locText3.GetComponent<ToolTip>().SetSimpleTooltip(text8);
					float num = (float)this.stats.StartingLevels[keyValuePair.Key.relevantAttributes[0].Id];
					LocText locText4 = Util.KInstantiateUI<LocText>(this.attributeLabelAptitude.gameObject, gameObject5, false);
					locText4.gameObject.SetActive(true);
					locText4.text = "+" + num.ToString() + " " + keyValuePair.Key.relevantAttributes[0].Name;
					string text9 = keyValuePair.Key.relevantAttributes[0].Description;
					text9 = string.Concat(new string[]
					{
						text9,
						"\n\n",
						keyValuePair.Key.relevantAttributes[0].Name,
						": +",
						num.ToString()
					});
					List<AttributeConverter> convertersForAttribute2 = Db.Get().AttributeConverters.GetConvertersForAttribute(keyValuePair.Key.relevantAttributes[0]);
					for (int n = 0; n < convertersForAttribute2.Count; n++)
					{
						text9 = text9 + "\n    • " + convertersForAttribute2[n].DescriptionFromAttribute(convertersForAttribute2[n].multiplier * num, null);
					}
					locText4.GetComponent<ToolTip>().SetSimpleTooltip(text9);
					gameObject5.gameObject.SetActive(true);
					this.aptitudeEntries.Add(gameObject5);
				}
			}
		}
		if (this.stats.stressTrait != null)
		{
			LocText locText5 = Util.KInstantiateUI<LocText>(this.expectationRight.gameObject, this.expectationRight.transform.parent.gameObject, false);
			locText5.gameObject.SetActive(true);
			locText5.text = string.Format(UI.CHARACTERCONTAINER_STRESSTRAIT, this.stats.stressTrait.Name);
			locText5.GetComponent<ToolTip>().SetSimpleTooltip(this.stats.stressTrait.GetTooltip());
			this.expectationLabels.Add(locText5);
		}
		if (this.stats.joyTrait != null)
		{
			LocText locText6 = Util.KInstantiateUI<LocText>(this.expectationRight.gameObject, this.expectationRight.transform.parent.gameObject, false);
			locText6.gameObject.SetActive(true);
			locText6.text = string.Format(UI.CHARACTERCONTAINER_JOYTRAIT, this.stats.joyTrait.Name);
			locText6.GetComponent<ToolTip>().SetSimpleTooltip(this.stats.joyTrait.GetTooltip());
			this.expectationLabels.Add(locText6);
		}
		this.description.text = this.stats.personality.description;
	}

	// Token: 0x06004C77 RID: 19575 RVA: 0x001AE7B0 File Offset: 0x001AC9B0
	private IEnumerator SetAttributes()
	{
		yield return null;
		this.iconGroups.ForEach(delegate(GameObject icg)
		{
			UnityEngine.Object.Destroy(icg);
		});
		this.iconGroups.Clear();
		List<AttributeInstance> list = new List<AttributeInstance>(this.animController.gameObject.GetAttributes().AttributeTable);
		list.RemoveAll((AttributeInstance at) => at.Attribute.ShowInUI != Klei.AI.Attribute.Display.Skill);
		list = list.OrderBy((AttributeInstance at) => at.Name).ToList<AttributeInstance>();
		for (int i = 0; i < list.Count; i++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.iconGroup.gameObject, this.iconGroup.transform.parent.gameObject, false);
			LocText componentInChildren = gameObject.GetComponentInChildren<LocText>();
			gameObject.SetActive(true);
			float totalValue = list[i].GetTotalValue();
			if (totalValue > 0f)
			{
				componentInChildren.color = Constants.POSITIVE_COLOR;
			}
			else if (totalValue == 0f)
			{
				componentInChildren.color = Constants.NEUTRAL_COLOR;
			}
			else
			{
				componentInChildren.color = Constants.NEGATIVE_COLOR;
			}
			componentInChildren.text = string.Format(UI.CHARACTERCONTAINER_SKILL_VALUE, GameUtil.AddPositiveSign(totalValue.ToString(), totalValue > 0f), list[i].Name);
			AttributeInstance attributeInstance = list[i];
			string text = attributeInstance.Description;
			if (attributeInstance.Attribute.converters.Count > 0)
			{
				text += "\n";
				foreach (AttributeConverter attributeConverter in attributeInstance.Attribute.converters)
				{
					AttributeConverterInstance converter = this.animController.gameObject.GetComponent<Klei.AI.AttributeConverters>().GetConverter(attributeConverter.Id);
					string text2 = converter.DescriptionFromAttribute(converter.Evaluate(), converter.gameObject);
					if (text2 != null)
					{
						text = text + "\n" + text2;
					}
				}
			}
			gameObject.GetComponent<ToolTip>().SetSimpleTooltip(text);
			this.iconGroups.Add(gameObject);
		}
		yield break;
	}

	// Token: 0x06004C78 RID: 19576 RVA: 0x001AE7C0 File Offset: 0x001AC9C0
	public void SelectDeliverable()
	{
		if (this.controller != null)
		{
			this.controller.AddDeliverable(this.stats);
		}
		if (MusicManager.instance.SongIsPlaying("Music_SelectDuplicant"))
		{
			MusicManager.instance.SetSongParameter("Music_SelectDuplicant", "songSection", 1f, true);
		}
		this.selectButton.GetComponent<ImageToggleState>().SetActive();
		this.selectButton.ClearOnClick();
		this.selectButton.onClick += delegate
		{
			this.DeselectDeliverable();
			if (MusicManager.instance.SongIsPlaying("Music_SelectDuplicant"))
			{
				MusicManager.instance.SetSongParameter("Music_SelectDuplicant", "songSection", 0f, true);
			}
		};
		this.selectedBorder.SetActive(true);
		this.titleBar.color = this.selectedTitleColor;
		this.animController.Play("cheer_pre", KAnim.PlayMode.Once, 1f, 0f);
		this.animController.Play("cheer_loop", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06004C79 RID: 19577 RVA: 0x001AE8A8 File Offset: 0x001ACAA8
	public void DeselectDeliverable()
	{
		if (this.controller != null)
		{
			this.controller.RemoveDeliverable(this.stats);
		}
		this.selectButton.GetComponent<ImageToggleState>().SetInactive();
		this.selectButton.Deselect();
		this.selectButton.ClearOnClick();
		this.selectButton.onClick += delegate
		{
			this.SelectDeliverable();
		};
		this.selectedBorder.SetActive(false);
		this.titleBar.color = this.deselectedTitleColor;
		this.animController.Queue("cheer_pst", KAnim.PlayMode.Once, 1f, 0f);
		this.animController.Queue("idle_default", KAnim.PlayMode.Loop, 1f, 0f);
	}

	// Token: 0x06004C7A RID: 19578 RVA: 0x001AE96E File Offset: 0x001ACB6E
	private void OnReplacedEvent(ITelepadDeliverable deliverable)
	{
		if (deliverable == this.stats)
		{
			this.DeselectDeliverable();
		}
	}

	// Token: 0x06004C7B RID: 19579 RVA: 0x001AE980 File Offset: 0x001ACB80
	private void OnCharacterSelectionLimitReached()
	{
		if (this.controller != null && this.controller.IsSelected(this.stats))
		{
			return;
		}
		this.selectButton.ClearOnClick();
		if (this.controller.AllowsReplacing)
		{
			this.selectButton.onClick += this.ReplaceCharacterSelection;
			return;
		}
		this.selectButton.onClick += this.CantSelectCharacter;
	}

	// Token: 0x06004C7C RID: 19580 RVA: 0x001AE9F6 File Offset: 0x001ACBF6
	private void CantSelectCharacter()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
	}

	// Token: 0x06004C7D RID: 19581 RVA: 0x001AEA08 File Offset: 0x001ACC08
	private void ReplaceCharacterSelection()
	{
		if (this.controller == null)
		{
			return;
		}
		this.controller.RemoveLast();
		this.SelectDeliverable();
	}

	// Token: 0x06004C7E RID: 19582 RVA: 0x001AEA2C File Offset: 0x001ACC2C
	private void OnCharacterSelectionLimitUnReached()
	{
		if (this.controller != null && this.controller.IsSelected(this.stats))
		{
			return;
		}
		this.selectButton.ClearOnClick();
		this.selectButton.onClick += delegate
		{
			this.SelectDeliverable();
		};
	}

	// Token: 0x06004C7F RID: 19583 RVA: 0x001AEA7D File Offset: 0x001ACC7D
	public void SetReshufflingState(bool enable)
	{
		this.reshuffleButton.gameObject.SetActive(enable);
		this.archetypeDropDown.gameObject.SetActive(enable);
	}

	// Token: 0x06004C80 RID: 19584 RVA: 0x001AEAA4 File Offset: 0x001ACCA4
	private void Reshuffle(bool is_starter)
	{
		if (this.controller != null && this.controller.IsSelected(this.stats))
		{
			this.DeselectDeliverable();
		}
		if (this.fxAnim != null)
		{
			this.fxAnim.Play("loop", KAnim.PlayMode.Once, 1f, 0f);
		}
		this.GenerateCharacter(is_starter, this.guaranteedAptitudeID);
	}

	// Token: 0x06004C81 RID: 19585 RVA: 0x001AEB14 File Offset: 0x001ACD14
	public void SetController(CharacterSelectionController csc)
	{
		if (csc == this.controller)
		{
			return;
		}
		this.controller = csc;
		CharacterSelectionController characterSelectionController = this.controller;
		characterSelectionController.OnLimitReachedEvent = (System.Action)Delegate.Combine(characterSelectionController.OnLimitReachedEvent, new System.Action(this.OnCharacterSelectionLimitReached));
		CharacterSelectionController characterSelectionController2 = this.controller;
		characterSelectionController2.OnLimitUnreachedEvent = (System.Action)Delegate.Combine(characterSelectionController2.OnLimitUnreachedEvent, new System.Action(this.OnCharacterSelectionLimitUnReached));
		CharacterSelectionController characterSelectionController3 = this.controller;
		characterSelectionController3.OnReshuffleEvent = (Action<bool>)Delegate.Combine(characterSelectionController3.OnReshuffleEvent, new Action<bool>(this.Reshuffle));
		CharacterSelectionController characterSelectionController4 = this.controller;
		characterSelectionController4.OnReplacedEvent = (Action<ITelepadDeliverable>)Delegate.Combine(characterSelectionController4.OnReplacedEvent, new Action<ITelepadDeliverable>(this.OnReplacedEvent));
	}

	// Token: 0x06004C82 RID: 19586 RVA: 0x001AEBD4 File Offset: 0x001ACDD4
	public void DisableSelectButton()
	{
		this.selectButton.soundPlayer.AcceptClickCondition = () => false;
		this.selectButton.GetComponent<ImageToggleState>().SetDisabled();
		this.selectButton.soundPlayer.Enabled = false;
	}

	// Token: 0x06004C83 RID: 19587 RVA: 0x001AEC31 File Offset: 0x001ACE31
	private bool IsCharacterRedundant()
	{
		return CharacterContainer.containers.Find((CharacterContainer c) => c != null && c.stats != null && c != this && c.stats.Name == this.stats.Name && c.stats.IsValid) != null || Components.LiveMinionIdentities.Items.Any((MinionIdentity id) => id.GetProperName() == this.stats.Name);
	}

	// Token: 0x06004C84 RID: 19588 RVA: 0x001AEC6E File Offset: 0x001ACE6E
	public string GetValueColor(bool isPositive)
	{
		if (!isPositive)
		{
			return "<color=#ff2222ff>";
		}
		return "<color=green>";
	}

	// Token: 0x06004C85 RID: 19589 RVA: 0x001AEC7E File Offset: 0x001ACE7E
	public override void OnPointerEnter(PointerEventData eventData)
	{
		this.scroll_rect.mouseIsOver = true;
		base.OnPointerEnter(eventData);
	}

	// Token: 0x06004C86 RID: 19590 RVA: 0x001AEC93 File Offset: 0x001ACE93
	public override void OnPointerExit(PointerEventData eventData)
	{
		this.scroll_rect.mouseIsOver = false;
		base.OnPointerExit(eventData);
	}

	// Token: 0x06004C87 RID: 19591 RVA: 0x001AECA8 File Offset: 0x001ACEA8
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.IsAction(global::Action.Escape) || e.IsAction(global::Action.MouseRight))
		{
			this.characterNameTitle.ForceStopEditing();
			this.controller.OnPressBack();
			this.archetypeDropDown.scrollRect.gameObject.SetActive(false);
		}
		if (!KInputManager.currentControllerIsGamepad)
		{
			e.Consumed = true;
			return;
		}
		if (this.archetypeDropDown.scrollRect.activeInHierarchy)
		{
			KScrollRect component = this.archetypeDropDown.scrollRect.GetComponent<KScrollRect>();
			Vector2 vector = component.rectTransform().InverseTransformPoint(KInputManager.GetMousePos());
			if (component.rectTransform().rect.Contains(vector))
			{
				component.mouseIsOver = true;
			}
			else
			{
				component.mouseIsOver = false;
			}
			component.OnKeyDown(e);
			return;
		}
		this.scroll_rect.OnKeyDown(e);
	}

	// Token: 0x06004C88 RID: 19592 RVA: 0x001AED78 File Offset: 0x001ACF78
	public override void OnKeyUp(KButtonEvent e)
	{
		if (!KInputManager.currentControllerIsGamepad)
		{
			e.Consumed = true;
			return;
		}
		if (this.archetypeDropDown.scrollRect.activeInHierarchy)
		{
			KScrollRect component = this.archetypeDropDown.scrollRect.GetComponent<KScrollRect>();
			Vector2 vector = component.rectTransform().InverseTransformPoint(KInputManager.GetMousePos());
			if (component.rectTransform().rect.Contains(vector))
			{
				component.mouseIsOver = true;
			}
			else
			{
				component.mouseIsOver = false;
			}
			component.OnKeyUp(e);
			return;
		}
		this.scroll_rect.OnKeyUp(e);
	}

	// Token: 0x06004C89 RID: 19593 RVA: 0x001AEE07 File Offset: 0x001AD007
	protected override void OnCmpEnable()
	{
		base.OnActivate();
		if (this.stats == null)
		{
			return;
		}
		this.SetAnimator();
	}

	// Token: 0x06004C8A RID: 19594 RVA: 0x001AEE1E File Offset: 0x001AD01E
	protected override void OnShow(bool show)
	{
		base.OnShow(show);
		this.characterNameTitle.ForceStopEditing();
	}

	// Token: 0x06004C8B RID: 19595 RVA: 0x001AEE34 File Offset: 0x001AD034
	private void OnArchetypeEntryClick(IListableOption skill, object data)
	{
		if (skill != null)
		{
			SkillGroup skillGroup = skill as SkillGroup;
			this.guaranteedAptitudeID = skillGroup.Id;
			this.selectedArchetypeIcon.sprite = Assets.GetSprite(skillGroup.archetypeIcon);
			this.Reshuffle(true);
			return;
		}
		this.guaranteedAptitudeID = null;
		this.selectedArchetypeIcon.sprite = this.dropdownArrowIcon;
		this.Reshuffle(true);
	}

	// Token: 0x06004C8C RID: 19596 RVA: 0x001AEE99 File Offset: 0x001AD099
	private int archetypeDropDownSort(IListableOption a, IListableOption b, object targetData)
	{
		if (b.Equals("Random"))
		{
			return -1;
		}
		return b.GetProperName().CompareTo(a.GetProperName());
	}

	// Token: 0x06004C8D RID: 19597 RVA: 0x001AEEBC File Offset: 0x001AD0BC
	private void archetypeDropEntryRefreshAction(DropDownEntry entry, object targetData)
	{
		if (entry.entryData != null)
		{
			SkillGroup skillGroup = entry.entryData as SkillGroup;
			entry.image.sprite = Assets.GetSprite(skillGroup.archetypeIcon);
		}
	}

	// Token: 0x04003247 RID: 12871
	[SerializeField]
	private GameObject contentBody;

	// Token: 0x04003248 RID: 12872
	[SerializeField]
	private LocText characterName;

	// Token: 0x04003249 RID: 12873
	[SerializeField]
	private EditableTitleBar characterNameTitle;

	// Token: 0x0400324A RID: 12874
	[SerializeField]
	private LocText characterJob;

	// Token: 0x0400324B RID: 12875
	public GameObject selectedBorder;

	// Token: 0x0400324C RID: 12876
	[SerializeField]
	private Image titleBar;

	// Token: 0x0400324D RID: 12877
	[SerializeField]
	private Color selectedTitleColor;

	// Token: 0x0400324E RID: 12878
	[SerializeField]
	private Color deselectedTitleColor;

	// Token: 0x0400324F RID: 12879
	[SerializeField]
	private KButton reshuffleButton;

	// Token: 0x04003250 RID: 12880
	private KBatchedAnimController animController;

	// Token: 0x04003251 RID: 12881
	[SerializeField]
	private GameObject iconGroup;

	// Token: 0x04003252 RID: 12882
	private List<GameObject> iconGroups;

	// Token: 0x04003253 RID: 12883
	[SerializeField]
	private LocText goodTrait;

	// Token: 0x04003254 RID: 12884
	[SerializeField]
	private LocText badTrait;

	// Token: 0x04003255 RID: 12885
	[SerializeField]
	private GameObject aptitudeEntry;

	// Token: 0x04003256 RID: 12886
	[SerializeField]
	private Transform aptitudeLabel;

	// Token: 0x04003257 RID: 12887
	[SerializeField]
	private Transform attributeLabelAptitude;

	// Token: 0x04003258 RID: 12888
	[SerializeField]
	private Transform attributeLabelTrait;

	// Token: 0x04003259 RID: 12889
	[SerializeField]
	private LocText expectationRight;

	// Token: 0x0400325A RID: 12890
	private List<LocText> expectationLabels;

	// Token: 0x0400325B RID: 12891
	[SerializeField]
	private DropDown archetypeDropDown;

	// Token: 0x0400325C RID: 12892
	[SerializeField]
	private Image selectedArchetypeIcon;

	// Token: 0x0400325D RID: 12893
	[SerializeField]
	private Sprite noArchetypeIcon;

	// Token: 0x0400325E RID: 12894
	[SerializeField]
	private Sprite dropdownArrowIcon;

	// Token: 0x0400325F RID: 12895
	private string guaranteedAptitudeID;

	// Token: 0x04003260 RID: 12896
	private List<GameObject> aptitudeEntries;

	// Token: 0x04003261 RID: 12897
	private List<GameObject> traitEntries;

	// Token: 0x04003262 RID: 12898
	[SerializeField]
	private LocText description;

	// Token: 0x04003263 RID: 12899
	[SerializeField]
	private KToggle selectButton;

	// Token: 0x04003264 RID: 12900
	[SerializeField]
	private KBatchedAnimController fxAnim;

	// Token: 0x04003265 RID: 12901
	private MinionStartingStats stats;

	// Token: 0x04003266 RID: 12902
	private CharacterSelectionController controller;

	// Token: 0x04003267 RID: 12903
	private static List<CharacterContainer> containers;

	// Token: 0x04003268 RID: 12904
	private KAnimFile idle_anim;

	// Token: 0x04003269 RID: 12905
	[HideInInspector]
	public bool addMinionToIdentityList = true;

	// Token: 0x0400326A RID: 12906
	[SerializeField]
	private Sprite enabledSpr;

	// Token: 0x0400326B RID: 12907
	[SerializeField]
	private KScrollRect scroll_rect;

	// Token: 0x0400326C RID: 12908
	private static readonly Dictionary<HashedString, string[]> traitIdleAnims = new Dictionary<HashedString, string[]>
	{
		{
			"anim_idle_food_kanim",
			new string[] { "Foodie" }
		},
		{
			"anim_idle_animal_lover_kanim",
			new string[] { "RanchingUp" }
		},
		{
			"anim_idle_loner_kanim",
			new string[] { "Loner" }
		},
		{
			"anim_idle_mole_hands_kanim",
			new string[] { "MoleHands" }
		},
		{
			"anim_idle_buff_kanim",
			new string[] { "StrongArm" }
		},
		{
			"anim_idle_distracted_kanim",
			new string[] { "CantResearch", "CantBuild", "CantCook", "CantDig" }
		},
		{
			"anim_idle_coaster_kanim",
			new string[] { "HappySinger" }
		}
	};

	// Token: 0x0400326D RID: 12909
	private static readonly HashedString[] idleAnims = new HashedString[] { "anim_idle_healthy_kanim", "anim_idle_susceptible_kanim", "anim_idle_keener_kanim", "anim_idle_fastfeet_kanim", "anim_idle_breatherdeep_kanim", "anim_idle_breathershallow_kanim" };

	// Token: 0x0400326E RID: 12910
	public float baseCharacterScale = 0.38f;

	// Token: 0x02001802 RID: 6146
	[Serializable]
	public struct ProfessionIcon
	{
		// Token: 0x04006EAA RID: 28330
		public string professionName;

		// Token: 0x04006EAB RID: 28331
		public Sprite iconImg;
	}
}
