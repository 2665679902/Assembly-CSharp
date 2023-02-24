﻿using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Token: 0x02000A98 RID: 2712
public class EventInfoScreen : KModalScreen
{
	// Token: 0x06005326 RID: 21286 RVA: 0x001E278A File Offset: 0x001E098A
	public override bool IsModal()
	{
		return true;
	}

	// Token: 0x06005327 RID: 21287 RVA: 0x001E2790 File Offset: 0x001E0990
	public void SetEventData(EventInfoData data)
	{
		data.FinalizeText();
		this.eventHeader.text = data.title;
		this.eventDescriptionLabel.text = data.description;
		this.eventLocationLabel.text = data.location;
		this.eventTimeLabel.text = data.whenDescription;
		if (data.location.IsNullOrWhiteSpace() && data.location.IsNullOrWhiteSpace())
		{
			this.timeGroup.gameObject.SetActive(false);
		}
		if (data.options.Count == 0)
		{
			data.AddDefaultOption(null);
		}
		this.SetEventDataOptions(data);
		this.SetEventDataVisuals(data);
	}

	// Token: 0x06005328 RID: 21288 RVA: 0x001E2838 File Offset: 0x001E0A38
	private void SetEventDataOptions(EventInfoData data)
	{
		using (List<EventInfoData.Option>.Enumerator enumerator = data.options.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				EventInfoData.Option option = enumerator.Current;
				GameObject gameObject = global::Util.KInstantiateUI(this.optionPrefab, this.buttonsGroup, false);
				gameObject.name = "Option: " + option.mainText;
				KButton component = gameObject.GetComponent<KButton>();
				component.isInteractable = option.allowed;
				component.onClick += delegate
				{
					if (option.callback != null)
					{
						option.callback();
					}
					this.Deactivate();
				};
				if (!option.tooltip.IsNullOrWhiteSpace())
				{
					gameObject.GetComponent<ToolTip>().SetSimpleTooltip(option.tooltip);
				}
				else
				{
					gameObject.GetComponent<ToolTip>().enabled = false;
				}
				foreach (EventInfoData.OptionIcon optionIcon in option.informationIcons)
				{
					this.CreateOptionIcon(gameObject, optionIcon);
				}
				global::Util.KInstantiateUI(this.optionTextPrefab, gameObject, false).GetComponent<LocText>().text = ((option.description == null) ? ("<b>" + option.mainText + "</b>") : string.Concat(new string[] { "<b>", option.mainText, "</b>\n<i>(", option.description, ")</i>" }));
				foreach (EventInfoData.OptionIcon optionIcon2 in option.consequenceIcons)
				{
					this.CreateOptionIcon(gameObject, optionIcon2);
				}
				gameObject.SetActive(true);
			}
		}
	}

	// Token: 0x06005329 RID: 21289 RVA: 0x001E2A68 File Offset: 0x001E0C68
	public override void Deactivate()
	{
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().EventPopupSnapshot, STOP_MODE.ALLOWFADEOUT);
		base.Deactivate();
	}

	// Token: 0x0600532A RID: 21290 RVA: 0x001E2A88 File Offset: 0x001E0C88
	private void CreateOptionIcon(GameObject option, EventInfoData.OptionIcon optionIcon)
	{
		GameObject gameObject = global::Util.KInstantiateUI(this.optionIconPrefab, option, false);
		gameObject.GetComponent<ToolTip>().SetSimpleTooltip(optionIcon.tooltip);
		HierarchyReferences component = gameObject.GetComponent<HierarchyReferences>();
		Image reference = component.GetReference<Image>("Mask");
		Image reference2 = component.GetReference<Image>("Border");
		Image reference3 = component.GetReference<Image>("Icon");
		if (optionIcon.sprite != null)
		{
			reference3.transform.localScale *= optionIcon.scale;
		}
		Color32 color = Color.white;
		switch (optionIcon.containerType)
		{
		case EventInfoData.OptionIcon.ContainerType.Neutral:
			reference.sprite = Assets.GetSprite("container_fill_neutral");
			reference2.sprite = Assets.GetSprite("container_border_neutral");
			if (optionIcon.sprite == null)
			{
				optionIcon.sprite = Assets.GetSprite("knob");
			}
			color = GlobalAssets.Instance.colorSet.eventNeutral;
			break;
		case EventInfoData.OptionIcon.ContainerType.Positive:
			reference.sprite = Assets.GetSprite("container_fill_positive");
			reference2.sprite = Assets.GetSprite("container_border_positive");
			reference3.rectTransform.localPosition += Vector3.down * 1f;
			if (optionIcon.sprite == null)
			{
				optionIcon.sprite = Assets.GetSprite("icon_positive");
			}
			color = GlobalAssets.Instance.colorSet.eventPositive;
			break;
		case EventInfoData.OptionIcon.ContainerType.Negative:
			reference.sprite = Assets.GetSprite("container_fill_negative");
			reference2.sprite = Assets.GetSprite("container_border_negative");
			reference3.rectTransform.localPosition += Vector3.up * 1f;
			color = GlobalAssets.Instance.colorSet.eventNegative;
			if (optionIcon.sprite == null)
			{
				optionIcon.sprite = Assets.GetSprite("cancel");
			}
			break;
		case EventInfoData.OptionIcon.ContainerType.Information:
			reference.sprite = Assets.GetSprite("requirements");
			reference2.enabled = false;
			break;
		}
		reference.color = color;
		reference3.sprite = optionIcon.sprite;
		if (optionIcon.sprite == null)
		{
			reference3.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600532B RID: 21291 RVA: 0x001E2CF0 File Offset: 0x001E0EF0
	private void SetEventDataVisuals(EventInfoData data)
	{
		this.createdAnimations.ForEach(delegate(KBatchedAnimController x)
		{
			UnityEngine.Object.Destroy(x);
		});
		this.createdAnimations.Clear();
		KAnimFile anim = Assets.GetAnim(data.animFileName);
		if (anim == null)
		{
			global::Debug.LogWarning("Event " + data.title + " has no anim data");
			return;
		}
		KBatchedAnimController component = this.CreateAnimLayer(this.midgroundGroup, anim, data.mainAnim, null, null, null).transform.GetComponent<KBatchedAnimController>();
		if (data.minions != null)
		{
			for (int i = 0; i < data.minions.Length; i++)
			{
				if (data.minions[i] == null)
				{
					DebugUtil.LogWarningArgs(new object[] { string.Format("EventInfoScreen unable to display minion {0}", i) });
				}
				string text = string.Format("dupe{0:D2}", i + 1);
				if (component.HasAnimation(text))
				{
					this.CreateAnimLayer(this.midgroundGroup, anim, text, data.minions[i], null, null);
				}
			}
		}
		if (data.artifact != null)
		{
			string text2 = "artifact";
			if (component.HasAnimation(text2))
			{
				this.CreateAnimLayer(this.midgroundGroup, anim, text2, null, data.artifact, null);
			}
		}
	}

	// Token: 0x0600532C RID: 21292 RVA: 0x001E2E50 File Offset: 0x001E1050
	private GameObject CreateAnimLayer(Transform parent, KAnimFile animFile, HashedString animName, GameObject minion = null, GameObject artifact = null, string targetSymbol = null)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.animPrefab, parent);
		KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
		this.createdAnimations.Add(component);
		if (minion != null)
		{
			component.AnimFiles = new KAnimFile[]
			{
				Assets.GetAnim("body_comp_default_kanim"),
				Assets.GetAnim("head_swap_kanim"),
				Assets.GetAnim("body_swap_kanim"),
				animFile
			};
		}
		else
		{
			component.AnimFiles = new KAnimFile[] { animFile };
		}
		if (minion != null)
		{
			SymbolOverrideController component2 = component.GetComponent<SymbolOverrideController>();
			if (this.loadMinionFromPersonalities)
			{
				component.GetComponent<UIDupeSymbolOverride>().Apply(minion.GetComponent<MinionIdentity>());
			}
			else
			{
				foreach (SymbolOverrideController.SymbolEntry symbolEntry in minion.GetComponent<SymbolOverrideController>().GetSymbolOverrides)
				{
					component2.AddSymbolOverride(symbolEntry.targetSymbol, symbolEntry.sourceSymbol, symbolEntry.priority);
				}
			}
			MinionConfig.ConfigureSymbols(gameObject, true);
		}
		if (artifact != null)
		{
			SymbolOverrideController component3 = component.GetComponent<SymbolOverrideController>();
			KBatchedAnimController component4 = artifact.GetComponent<KBatchedAnimController>();
			string text = component4.initialAnim;
			text = text.Replace("idle_", "artifact_");
			text = text.Replace("_loop", "");
			KAnim.Build.Symbol symbol = component4.AnimFiles[0].GetData().build.GetSymbol(text);
			if (symbol != null)
			{
				component3.AddSymbolOverride("snapTo_artifact", symbol, 0);
			}
		}
		if (targetSymbol != null)
		{
			gameObject.AddOrGet<KBatchedAnimTracker>().symbol = targetSymbol;
		}
		gameObject.SetActive(true);
		component.Play(animName, KAnim.PlayMode.Loop, 1f, 0f);
		component.animScale = this.baseCharacterScale;
		return gameObject;
	}

	// Token: 0x0600532D RID: 21293 RVA: 0x001E3014 File Offset: 0x001E1214
	public static EventInfoScreen ShowPopup(EventInfoData eventInfoData)
	{
		EventInfoScreen eventInfoScreen = (EventInfoScreen)KScreenManager.Instance.StartScreen(ScreenPrefabs.Instance.eventInfoScreen.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject);
		eventInfoScreen.SetEventData(eventInfoData);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().EventPopupSnapshot);
		if (eventInfoData.showCallback != null)
		{
			eventInfoData.showCallback();
		}
		if (eventInfoData.clickFocus != null)
		{
			WorldContainer myWorld = eventInfoData.clickFocus.gameObject.GetMyWorld();
			if (myWorld != null && myWorld.IsDiscovered)
			{
				CameraController.Instance.ActiveWorldStarWipe(myWorld.id, eventInfoData.clickFocus.position, 10f, null);
			}
		}
		return eventInfoScreen;
	}

	// Token: 0x0600532E RID: 21294 RVA: 0x001E30D0 File Offset: 0x001E12D0
	public static Notification CreateNotification(EventInfoData eventInfoData, Notification.ClickCallback clickCallback = null)
	{
		if (eventInfoData == null)
		{
			DebugUtil.LogWarningArgs(new object[] { "eventPopup is null in CreateStandardEventNotification" });
			return null;
		}
		eventInfoData.FinalizeText();
		Notification notification = new Notification(eventInfoData.title, NotificationType.Event, null, null, false, 0f, null, null, eventInfoData.clickFocus, true, false, false);
		if (clickCallback == null)
		{
			notification.customClickCallback = delegate(object data)
			{
				EventInfoScreen.ShowPopup(eventInfoData);
			};
		}
		else
		{
			notification.customClickCallback = clickCallback;
		}
		return notification;
	}

	// Token: 0x0400384B RID: 14411
	[SerializeField]
	private float baseCharacterScale = 0.0057f;

	// Token: 0x0400384C RID: 14412
	[FormerlySerializedAs("midgroundPrefab")]
	[FormerlySerializedAs("mid")]
	[Header("Prefabs")]
	[SerializeField]
	private GameObject animPrefab;

	// Token: 0x0400384D RID: 14413
	[SerializeField]
	private GameObject optionPrefab;

	// Token: 0x0400384E RID: 14414
	[SerializeField]
	private GameObject optionIconPrefab;

	// Token: 0x0400384F RID: 14415
	[SerializeField]
	private GameObject optionTextPrefab;

	// Token: 0x04003850 RID: 14416
	[Header("Groups")]
	[SerializeField]
	private Transform midgroundGroup;

	// Token: 0x04003851 RID: 14417
	[SerializeField]
	private GameObject timeGroup;

	// Token: 0x04003852 RID: 14418
	[SerializeField]
	private GameObject buttonsGroup;

	// Token: 0x04003853 RID: 14419
	[SerializeField]
	private GameObject chainGroup;

	// Token: 0x04003854 RID: 14420
	[Header("Text")]
	[SerializeField]
	private LocText eventHeader;

	// Token: 0x04003855 RID: 14421
	[SerializeField]
	private LocText eventTimeLabel;

	// Token: 0x04003856 RID: 14422
	[SerializeField]
	private LocText eventLocationLabel;

	// Token: 0x04003857 RID: 14423
	[SerializeField]
	private LocText eventDescriptionLabel;

	// Token: 0x04003858 RID: 14424
	[SerializeField]
	private bool loadMinionFromPersonalities = true;

	// Token: 0x04003859 RID: 14425
	[SerializeField]
	private LocText chainCount;

	// Token: 0x0400385A RID: 14426
	[Header("Button Colour Styles")]
	[SerializeField]
	private ColorStyleSetting neutralButtonSetting;

	// Token: 0x0400385B RID: 14427
	[SerializeField]
	private ColorStyleSetting badButtonSetting;

	// Token: 0x0400385C RID: 14428
	[SerializeField]
	private ColorStyleSetting goodButtonSetting;

	// Token: 0x0400385D RID: 14429
	private List<KBatchedAnimController> createdAnimations = new List<KBatchedAnimController>();
}
