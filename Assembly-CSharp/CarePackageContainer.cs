using System;
using System.Collections;
using System.Collections.Generic;
using Database;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020009F5 RID: 2549
public class CarePackageContainer : KScreen, ITelepadDeliverableContainer
{
	// Token: 0x06004C3F RID: 19519 RVA: 0x001AC836 File Offset: 0x001AAA36
	public GameObject GetGameObject()
	{
		return base.gameObject;
	}

	// Token: 0x170005BE RID: 1470
	// (get) Token: 0x06004C40 RID: 19520 RVA: 0x001AC83E File Offset: 0x001AAA3E
	public CarePackageInfo Info
	{
		get
		{
			return this.info;
		}
	}

	// Token: 0x06004C41 RID: 19521 RVA: 0x001AC846 File Offset: 0x001AAA46
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.Initialize();
		base.StartCoroutine(this.DelayedGeneration());
	}

	// Token: 0x06004C42 RID: 19522 RVA: 0x001AC861 File Offset: 0x001AAA61
	public override float GetSortKey()
	{
		return 50f;
	}

	// Token: 0x06004C43 RID: 19523 RVA: 0x001AC868 File Offset: 0x001AAA68
	private IEnumerator DelayedGeneration()
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		if (this.controller != null)
		{
			this.GenerateCharacter(this.controller.IsStarterMinion);
		}
		yield break;
	}

	// Token: 0x06004C44 RID: 19524 RVA: 0x001AC877 File Offset: 0x001AAA77
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		if (this.animController != null)
		{
			this.animController.gameObject.DeleteObject();
			this.animController = null;
		}
	}

	// Token: 0x06004C45 RID: 19525 RVA: 0x001AC8A4 File Offset: 0x001AAAA4
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

	// Token: 0x06004C46 RID: 19526 RVA: 0x001AC93A File Offset: 0x001AAB3A
	private void Initialize()
	{
		this.professionIconMap = new Dictionary<string, Sprite>();
		this.professionIcons.ForEach(delegate(CarePackageContainer.ProfessionIcon ic)
		{
			this.professionIconMap.Add(ic.professionName, ic.iconImg);
		});
		if (CarePackageContainer.containers == null)
		{
			CarePackageContainer.containers = new List<ITelepadDeliverableContainer>();
		}
		CarePackageContainer.containers.Add(this);
	}

	// Token: 0x06004C47 RID: 19527 RVA: 0x001AC97C File Offset: 0x001AAB7C
	private void GenerateCharacter(bool is_starter)
	{
		int num = 0;
		do
		{
			this.info = Immigration.Instance.RandomCarePackage();
			num++;
		}
		while (this.IsCharacterRedundant() && num < 20);
		if (this.animController != null)
		{
			UnityEngine.Object.Destroy(this.animController.gameObject);
			this.animController = null;
		}
		this.carePackageInstanceData = new CarePackageContainer.CarePackageInstanceData();
		this.carePackageInstanceData.info = this.info;
		if (this.info.facadeID == "SELECTRANDOM")
		{
			this.carePackageInstanceData.facadeID = Db.GetEquippableFacades().resources.FindAll((EquippableFacadeResource match) => match.DefID == this.info.id).GetRandom<EquippableFacadeResource>().Id;
		}
		else
		{
			this.carePackageInstanceData.facadeID = this.info.facadeID;
		}
		this.SetAnimator();
		this.SetInfoText();
		this.selectButton.ClearOnClick();
		if (!this.controller.IsStarterMinion)
		{
			this.selectButton.onClick += delegate
			{
				this.SelectDeliverable();
			};
		}
	}

	// Token: 0x06004C48 RID: 19528 RVA: 0x001ACA88 File Offset: 0x001AAC88
	private void SetAnimator()
	{
		GameObject prefab = Assets.GetPrefab(this.info.id.ToTag());
		EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(this.info.id);
		int num;
		if (ElementLoader.FindElementByName(this.info.id) != null)
		{
			num = 1;
		}
		else if (foodInfo != null)
		{
			num = (int)(this.info.quantity % foodInfo.CaloriesPerUnit);
		}
		else
		{
			num = (int)this.info.quantity;
		}
		if (prefab != null)
		{
			for (int i = 0; i < num; i++)
			{
				GameObject gameObject = Util.KInstantiateUI(this.contentBody, this.contentBody.transform.parent.gameObject, false);
				gameObject.SetActive(true);
				Image component = gameObject.GetComponent<Image>();
				global::Tuple<Sprite, Color> tuple;
				if (!this.carePackageInstanceData.facadeID.IsNullOrWhiteSpace())
				{
					tuple = Def.GetUISprite(prefab.PrefabID(), this.carePackageInstanceData.facadeID);
				}
				else
				{
					tuple = Def.GetUISprite(prefab, "ui", false);
				}
				component.sprite = tuple.first;
				component.color = tuple.second;
				this.entryIcons.Add(gameObject);
				if (num > 1)
				{
					int num2;
					int num3;
					int num4;
					if (num % 2 == 1)
					{
						num2 = Mathf.CeilToInt((float)(num / 2));
						num3 = num2 - i;
						num4 = ((num3 > 0) ? 1 : (-1));
						num3 = Mathf.Abs(num3);
					}
					else
					{
						num2 = num / 2 - 1;
						if (i <= num2)
						{
							num3 = Mathf.Abs(num2 - i);
							num4 = -1;
						}
						else
						{
							num3 = Mathf.Abs(num2 + 1 - i);
							num4 = 1;
						}
					}
					int num5 = 0;
					if (num % 2 == 0)
					{
						num5 = ((i <= num2) ? (-6) : 6);
						gameObject.transform.SetPosition(gameObject.transform.position += new Vector3((float)num5, 0f, 0f));
					}
					gameObject.transform.localScale = new Vector3(1f - (float)num3 * 0.1f, 1f - (float)num3 * 0.1f, 1f);
					gameObject.transform.Rotate(0f, 0f, 3f * (float)num3 * (float)num4);
					gameObject.transform.SetPosition(gameObject.transform.position + new Vector3(25f * (float)num3 * (float)num4, 5f * (float)num3) + new Vector3((float)num5, 0f, 0f));
					gameObject.GetComponent<Canvas>().sortingOrder = num - num3;
				}
			}
			return;
		}
		GameObject gameObject2 = Util.KInstantiateUI(this.contentBody, this.contentBody.transform.parent.gameObject, false);
		gameObject2.SetActive(true);
		Image component2 = gameObject2.GetComponent<Image>();
		component2.sprite = Def.GetUISpriteFromMultiObjectAnim(ElementLoader.GetElement(this.info.id.ToTag()).substance.anim, "ui", false, "");
		component2.color = ElementLoader.GetElement(this.info.id.ToTag()).substance.uiColour;
		this.entryIcons.Add(gameObject2);
	}

	// Token: 0x06004C49 RID: 19529 RVA: 0x001ACDB8 File Offset: 0x001AAFB8
	private string GetSpawnableName()
	{
		GameObject prefab = Assets.GetPrefab(this.info.id);
		if (prefab == null)
		{
			Element element = ElementLoader.FindElementByName(this.info.id);
			if (element != null)
			{
				return element.substance.name;
			}
			return "";
		}
		else
		{
			if (string.IsNullOrEmpty(this.carePackageInstanceData.facadeID))
			{
				return prefab.GetProperName();
			}
			return EquippableFacade.GetNameOverride(this.carePackageInstanceData.info.id, this.carePackageInstanceData.facadeID);
		}
	}

	// Token: 0x06004C4A RID: 19530 RVA: 0x001ACE44 File Offset: 0x001AB044
	private string GetSpawnableQuantityOnly()
	{
		if (ElementLoader.GetElement(this.info.id.ToTag()) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT_ONLY, GameUtil.GetFormattedMass(this.info.quantity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		}
		if (EdiblesManager.GetFoodInfo(this.info.id) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT_ONLY, GameUtil.GetFormattedCaloriesForItem(this.info.id, this.info.quantity, GameUtil.TimeSlice.None, true));
		}
		return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT_ONLY, this.info.quantity.ToString());
	}

	// Token: 0x06004C4B RID: 19531 RVA: 0x001ACEF8 File Offset: 0x001AB0F8
	private string GetCurrentQuantity(WorldInventory inventory)
	{
		if (ElementLoader.GetElement(this.info.id.ToTag()) != null)
		{
			float amount = inventory.GetAmount(this.info.id.ToTag(), false);
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_CURRENT_AMOUNT, GameUtil.GetFormattedMass(amount, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
		}
		if (EdiblesManager.GetFoodInfo(this.info.id) != null)
		{
			float num = RationTracker.Get().CountRationsByFoodType(this.info.id, inventory, true);
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_CURRENT_AMOUNT, GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true));
		}
		float amount2 = inventory.GetAmount(this.info.id.ToTag(), false);
		return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_CURRENT_AMOUNT, amount2.ToString());
	}

	// Token: 0x06004C4C RID: 19532 RVA: 0x001ACFC4 File Offset: 0x001AB1C4
	private string GetSpawnableQuantity()
	{
		if (ElementLoader.GetElement(this.info.id.ToTag()) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_QUANTITY, GameUtil.GetFormattedMass(this.info.quantity, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"), Assets.GetPrefab(this.info.id).GetProperName());
		}
		if (EdiblesManager.GetFoodInfo(this.info.id) != null)
		{
			return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_QUANTITY, GameUtil.GetFormattedCaloriesForItem(this.info.id, this.info.quantity, GameUtil.TimeSlice.None, true), Assets.GetPrefab(this.info.id).GetProperName());
		}
		return string.Format(UI.IMMIGRANTSCREEN.CARE_PACKAGE_ELEMENT_COUNT, Assets.GetPrefab(this.info.id).GetProperName(), this.info.quantity.ToString());
	}

	// Token: 0x06004C4D RID: 19533 RVA: 0x001AD0C4 File Offset: 0x001AB2C4
	private string GetSpawnableDescription()
	{
		Element element = ElementLoader.GetElement(this.info.id.ToTag());
		if (element != null)
		{
			return element.Description();
		}
		GameObject prefab = Assets.GetPrefab(this.info.id);
		if (prefab == null)
		{
			return "";
		}
		InfoDescription component = prefab.GetComponent<InfoDescription>();
		if (component != null)
		{
			return component.description;
		}
		return prefab.GetProperName();
	}

	// Token: 0x06004C4E RID: 19534 RVA: 0x001AD134 File Offset: 0x001AB334
	private void SetInfoText()
	{
		this.characterName.SetText(this.GetSpawnableName());
		this.description.SetText(this.GetSpawnableDescription());
		this.itemName.SetText(this.GetSpawnableName());
		this.quantity.SetText(this.GetSpawnableQuantityOnly());
		this.currentQuantity.SetText(this.GetCurrentQuantity(ClusterManager.Instance.activeWorld.worldInventory));
	}

	// Token: 0x06004C4F RID: 19535 RVA: 0x001AD1A8 File Offset: 0x001AB3A8
	public void SelectDeliverable()
	{
		if (this.controller != null)
		{
			this.controller.AddDeliverable(this.carePackageInstanceData);
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
	}

	// Token: 0x06004C50 RID: 19536 RVA: 0x001AD250 File Offset: 0x001AB450
	public void DeselectDeliverable()
	{
		if (this.controller != null)
		{
			this.controller.RemoveDeliverable(this.carePackageInstanceData);
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
	}

	// Token: 0x06004C51 RID: 19537 RVA: 0x001AD2D6 File Offset: 0x001AB4D6
	private void OnReplacedEvent(ITelepadDeliverable stats)
	{
		if (stats == this.carePackageInstanceData)
		{
			this.DeselectDeliverable();
		}
	}

	// Token: 0x06004C52 RID: 19538 RVA: 0x001AD2E8 File Offset: 0x001AB4E8
	private void OnCharacterSelectionLimitReached()
	{
		if (this.controller != null && this.controller.IsSelected(this.info))
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

	// Token: 0x06004C53 RID: 19539 RVA: 0x001AD35E File Offset: 0x001AB55E
	private void CantSelectCharacter()
	{
		KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
	}

	// Token: 0x06004C54 RID: 19540 RVA: 0x001AD370 File Offset: 0x001AB570
	private void ReplaceCharacterSelection()
	{
		if (this.controller == null)
		{
			return;
		}
		this.controller.RemoveLast();
		this.SelectDeliverable();
	}

	// Token: 0x06004C55 RID: 19541 RVA: 0x001AD394 File Offset: 0x001AB594
	private void OnCharacterSelectionLimitUnReached()
	{
		if (this.controller != null && this.controller.IsSelected(this.info))
		{
			return;
		}
		this.selectButton.ClearOnClick();
		this.selectButton.onClick += delegate
		{
			this.SelectDeliverable();
		};
	}

	// Token: 0x06004C56 RID: 19542 RVA: 0x001AD3E5 File Offset: 0x001AB5E5
	public void SetReshufflingState(bool enable)
	{
		this.reshuffleButton.gameObject.SetActive(enable);
	}

	// Token: 0x06004C57 RID: 19543 RVA: 0x001AD3F8 File Offset: 0x001AB5F8
	private void Reshuffle(bool is_starter)
	{
		if (this.controller != null && this.controller.IsSelected(this.info))
		{
			this.DeselectDeliverable();
		}
		this.ClearEntryIcons();
		this.GenerateCharacter(is_starter);
	}

	// Token: 0x06004C58 RID: 19544 RVA: 0x001AD430 File Offset: 0x001AB630
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

	// Token: 0x06004C59 RID: 19545 RVA: 0x001AD4F0 File Offset: 0x001AB6F0
	public void DisableSelectButton()
	{
		this.selectButton.soundPlayer.AcceptClickCondition = () => false;
		this.selectButton.GetComponent<ImageToggleState>().SetDisabled();
		this.selectButton.soundPlayer.Enabled = false;
	}

	// Token: 0x06004C5A RID: 19546 RVA: 0x001AD550 File Offset: 0x001AB750
	private bool IsCharacterRedundant()
	{
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in CarePackageContainer.containers)
		{
			if (telepadDeliverableContainer != this)
			{
				CarePackageContainer carePackageContainer = telepadDeliverableContainer as CarePackageContainer;
				if (carePackageContainer != null && carePackageContainer.info == this.info)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06004C5B RID: 19547 RVA: 0x001AD5C4 File Offset: 0x001AB7C4
	public string GetValueColor(bool isPositive)
	{
		if (!isPositive)
		{
			return "<color=#ff2222ff>";
		}
		return "<color=green>";
	}

	// Token: 0x06004C5C RID: 19548 RVA: 0x001AD5D4 File Offset: 0x001AB7D4
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.IsAction(global::Action.Escape))
		{
			this.controller.OnPressBack();
		}
		if (!KInputManager.currentControllerIsGamepad)
		{
			e.Consumed = true;
		}
	}

	// Token: 0x06004C5D RID: 19549 RVA: 0x001AD5F8 File Offset: 0x001AB7F8
	public override void OnKeyUp(KButtonEvent e)
	{
		if (!KInputManager.currentControllerIsGamepad)
		{
			e.Consumed = true;
		}
	}

	// Token: 0x06004C5E RID: 19550 RVA: 0x001AD608 File Offset: 0x001AB808
	protected override void OnCmpEnable()
	{
		base.OnActivate();
		if (this.info == null)
		{
			return;
		}
		this.ClearEntryIcons();
		this.SetAnimator();
		this.SetInfoText();
	}

	// Token: 0x06004C5F RID: 19551 RVA: 0x001AD62C File Offset: 0x001AB82C
	private void ClearEntryIcons()
	{
		for (int i = 0; i < this.entryIcons.Count; i++)
		{
			UnityEngine.Object.Destroy(this.entryIcons[i]);
		}
	}

	// Token: 0x04003231 RID: 12849
	[Header("UI References")]
	[SerializeField]
	private GameObject contentBody;

	// Token: 0x04003232 RID: 12850
	[SerializeField]
	private LocText characterName;

	// Token: 0x04003233 RID: 12851
	public GameObject selectedBorder;

	// Token: 0x04003234 RID: 12852
	[SerializeField]
	private Image titleBar;

	// Token: 0x04003235 RID: 12853
	[SerializeField]
	private Color selectedTitleColor;

	// Token: 0x04003236 RID: 12854
	[SerializeField]
	private Color deselectedTitleColor;

	// Token: 0x04003237 RID: 12855
	[SerializeField]
	private KButton reshuffleButton;

	// Token: 0x04003238 RID: 12856
	private KBatchedAnimController animController;

	// Token: 0x04003239 RID: 12857
	[SerializeField]
	private LocText itemName;

	// Token: 0x0400323A RID: 12858
	[SerializeField]
	private LocText quantity;

	// Token: 0x0400323B RID: 12859
	[SerializeField]
	private LocText currentQuantity;

	// Token: 0x0400323C RID: 12860
	[SerializeField]
	private LocText description;

	// Token: 0x0400323D RID: 12861
	[SerializeField]
	private KToggle selectButton;

	// Token: 0x0400323E RID: 12862
	private CarePackageInfo info;

	// Token: 0x0400323F RID: 12863
	public CarePackageContainer.CarePackageInstanceData carePackageInstanceData;

	// Token: 0x04003240 RID: 12864
	private CharacterSelectionController controller;

	// Token: 0x04003241 RID: 12865
	private static List<ITelepadDeliverableContainer> containers;

	// Token: 0x04003242 RID: 12866
	[SerializeField]
	private Sprite enabledSpr;

	// Token: 0x04003243 RID: 12867
	[SerializeField]
	private List<CarePackageContainer.ProfessionIcon> professionIcons;

	// Token: 0x04003244 RID: 12868
	private Dictionary<string, Sprite> professionIconMap;

	// Token: 0x04003245 RID: 12869
	public float baseCharacterScale = 0.38f;

	// Token: 0x04003246 RID: 12870
	private List<GameObject> entryIcons = new List<GameObject>();

	// Token: 0x020017FE RID: 6142
	[Serializable]
	public struct ProfessionIcon
	{
		// Token: 0x04006EA1 RID: 28321
		public string professionName;

		// Token: 0x04006EA2 RID: 28322
		public Sprite iconImg;
	}

	// Token: 0x020017FF RID: 6143
	public class CarePackageInstanceData : ITelepadDeliverable
	{
		// Token: 0x06008C86 RID: 35974 RVA: 0x00302BAA File Offset: 0x00300DAA
		public GameObject Deliver(Vector3 position)
		{
			GameObject gameObject = this.info.Deliver(position);
			gameObject.GetComponent<CarePackage>().SetFacade(this.facadeID);
			return gameObject;
		}

		// Token: 0x04006EA3 RID: 28323
		public CarePackageInfo info;

		// Token: 0x04006EA4 RID: 28324
		public string facadeID;
	}
}
