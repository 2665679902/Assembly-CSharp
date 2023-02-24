using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000AF5 RID: 2805
[AddComponentMenu("KMonoBehaviour/scripts/CrewPortrait")]
[Serializable]
public class CrewPortrait : KMonoBehaviour
{
	// Token: 0x17000643 RID: 1603
	// (get) Token: 0x06005626 RID: 22054 RVA: 0x001F2CC5 File Offset: 0x001F0EC5
	// (set) Token: 0x06005627 RID: 22055 RVA: 0x001F2CCD File Offset: 0x001F0ECD
	public IAssignableIdentity identityObject { get; private set; }

	// Token: 0x06005628 RID: 22056 RVA: 0x001F2CD6 File Offset: 0x001F0ED6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		if (this.startTransparent)
		{
			base.StartCoroutine(this.AlphaIn());
		}
		this.requiresRefresh = true;
	}

	// Token: 0x06005629 RID: 22057 RVA: 0x001F2CFA File Offset: 0x001F0EFA
	private IEnumerator AlphaIn()
	{
		this.SetAlpha(0f);
		for (float i = 0f; i < 1f; i += Time.unscaledDeltaTime * 4f)
		{
			this.SetAlpha(i);
			yield return 0;
		}
		this.SetAlpha(1f);
		yield break;
	}

	// Token: 0x0600562A RID: 22058 RVA: 0x001F2D09 File Offset: 0x001F0F09
	private void OnRoleChanged(object data)
	{
		if (this.controller == null)
		{
			return;
		}
		CrewPortrait.RefreshHat(this.identityObject, this.controller);
	}

	// Token: 0x0600562B RID: 22059 RVA: 0x001F2D2C File Offset: 0x001F0F2C
	private void RegisterEvents()
	{
		if (this.areEventsRegistered)
		{
			return;
		}
		KMonoBehaviour kmonoBehaviour = this.identityObject as KMonoBehaviour;
		if (kmonoBehaviour == null)
		{
			return;
		}
		kmonoBehaviour.Subscribe(540773776, new Action<object>(this.OnRoleChanged));
		this.areEventsRegistered = true;
	}

	// Token: 0x0600562C RID: 22060 RVA: 0x001F2D78 File Offset: 0x001F0F78
	private void UnregisterEvents()
	{
		if (!this.areEventsRegistered)
		{
			return;
		}
		this.areEventsRegistered = false;
		KMonoBehaviour kmonoBehaviour = this.identityObject as KMonoBehaviour;
		if (kmonoBehaviour == null)
		{
			return;
		}
		kmonoBehaviour.Unsubscribe(540773776, new Action<object>(this.OnRoleChanged));
	}

	// Token: 0x0600562D RID: 22061 RVA: 0x001F2DC2 File Offset: 0x001F0FC2
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.RegisterEvents();
		this.ForceRefresh();
	}

	// Token: 0x0600562E RID: 22062 RVA: 0x001F2DD6 File Offset: 0x001F0FD6
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.UnregisterEvents();
	}

	// Token: 0x0600562F RID: 22063 RVA: 0x001F2DE4 File Offset: 0x001F0FE4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.UnregisterEvents();
	}

	// Token: 0x06005630 RID: 22064 RVA: 0x001F2DF4 File Offset: 0x001F0FF4
	public void SetIdentityObject(IAssignableIdentity identity, bool jobEnabled = true)
	{
		this.UnregisterEvents();
		this.identityObject = identity;
		this.RegisterEvents();
		this.targetImage.enabled = true;
		if (this.identityObject != null)
		{
			this.targetImage.enabled = false;
		}
		if (this.useLabels && (identity is MinionIdentity || identity is MinionAssignablesProxy))
		{
			this.SetDuplicantJobTitleActive(jobEnabled);
		}
		this.requiresRefresh = true;
	}

	// Token: 0x06005631 RID: 22065 RVA: 0x001F2E5C File Offset: 0x001F105C
	public void SetSubTitle(string newTitle)
	{
		if (this.subTitle != null)
		{
			if (string.IsNullOrEmpty(newTitle))
			{
				this.subTitle.gameObject.SetActive(false);
				return;
			}
			this.subTitle.gameObject.SetActive(true);
			this.subTitle.SetText(newTitle);
		}
	}

	// Token: 0x06005632 RID: 22066 RVA: 0x001F2EAE File Offset: 0x001F10AE
	public void SetDuplicantJobTitleActive(bool state)
	{
		if (this.duplicantJob != null && this.duplicantJob.gameObject.activeInHierarchy != state)
		{
			this.duplicantJob.gameObject.SetActive(state);
		}
	}

	// Token: 0x06005633 RID: 22067 RVA: 0x001F2EE2 File Offset: 0x001F10E2
	public void ForceRefresh()
	{
		this.requiresRefresh = true;
	}

	// Token: 0x06005634 RID: 22068 RVA: 0x001F2EEB File Offset: 0x001F10EB
	public void Update()
	{
		if (this.requiresRefresh && (this.controller == null || this.controller.enabled))
		{
			this.requiresRefresh = false;
			this.Rebuild();
		}
	}

	// Token: 0x06005635 RID: 22069 RVA: 0x001F2F20 File Offset: 0x001F1120
	private void Rebuild()
	{
		if (this.controller == null)
		{
			this.controller = base.GetComponentInChildren<KBatchedAnimController>();
			if (this.controller == null)
			{
				if (this.targetImage != null)
				{
					this.targetImage.enabled = true;
				}
				global::Debug.LogWarning("Controller for [" + base.name + "] null");
				return;
			}
		}
		CrewPortrait.SetPortraitData(this.identityObject, this.controller, this.useDefaultExpression);
		if (this.useLabels && this.duplicantName != null)
		{
			this.duplicantName.SetText((!this.identityObject.IsNullOrDestroyed()) ? this.identityObject.GetProperName() : "");
			if (this.identityObject is MinionIdentity && this.duplicantJob != null)
			{
				this.duplicantJob.SetText((this.identityObject != null) ? (this.identityObject as MinionIdentity).GetComponent<MinionResume>().GetSkillsSubtitle() : "");
				this.duplicantJob.GetComponent<ToolTip>().toolTip = (this.identityObject as MinionIdentity).GetComponent<MinionResume>().GetSkillsSubtitle();
			}
		}
	}

	// Token: 0x06005636 RID: 22070 RVA: 0x001F3058 File Offset: 0x001F1258
	private static void RefreshHat(IAssignableIdentity identityObject, KBatchedAnimController controller)
	{
		string text = "";
		MinionIdentity minionIdentity = identityObject as MinionIdentity;
		if (minionIdentity != null)
		{
			text = minionIdentity.GetComponent<MinionResume>().CurrentHat;
		}
		else if (identityObject as StoredMinionIdentity != null)
		{
			text = (identityObject as StoredMinionIdentity).currentHat;
		}
		MinionResume.ApplyHat(text, controller);
	}

	// Token: 0x06005637 RID: 22071 RVA: 0x001F30AC File Offset: 0x001F12AC
	public static void SetPortraitData(IAssignableIdentity identityObject, KBatchedAnimController controller, bool useDefaultExpression = true)
	{
		if (identityObject == null)
		{
			controller.gameObject.SetActive(false);
			return;
		}
		MinionIdentity minionIdentity = identityObject as MinionIdentity;
		if (minionIdentity == null)
		{
			MinionAssignablesProxy minionAssignablesProxy = identityObject as MinionAssignablesProxy;
			if (minionAssignablesProxy != null && minionAssignablesProxy.target != null)
			{
				minionIdentity = minionAssignablesProxy.target as MinionIdentity;
			}
		}
		controller.gameObject.SetActive(true);
		controller.Play("ui_idle", KAnim.PlayMode.Once, 1f, 0f);
		SymbolOverrideController component = controller.GetComponent<SymbolOverrideController>();
		component.RemoveAllSymbolOverrides(0);
		if (minionIdentity != null)
		{
			Accessorizer component2 = minionIdentity.GetComponent<Accessorizer>();
			foreach (AccessorySlot accessorySlot in Db.Get().AccessorySlots.resources)
			{
				Accessory accessory = component2.GetAccessory(accessorySlot);
				if (accessory != null)
				{
					component.AddSymbolOverride(accessorySlot.targetSymbolId, accessory.symbol, 0);
					controller.SetSymbolVisiblity(accessorySlot.targetSymbolId, true);
				}
				else
				{
					controller.SetSymbolVisiblity(accessorySlot.targetSymbolId, false);
				}
			}
			component.AddSymbolOverride(Db.Get().AccessorySlots.HatHair.targetSymbolId, Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(component2.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol, 1);
			CrewPortrait.RefreshHat(minionIdentity, controller);
		}
		else
		{
			StoredMinionIdentity storedMinionIdentity = identityObject as StoredMinionIdentity;
			if (storedMinionIdentity == null)
			{
				MinionAssignablesProxy minionAssignablesProxy2 = identityObject as MinionAssignablesProxy;
				if (minionAssignablesProxy2 != null && minionAssignablesProxy2.target != null)
				{
					storedMinionIdentity = minionAssignablesProxy2.target as StoredMinionIdentity;
				}
			}
			if (!(storedMinionIdentity != null))
			{
				controller.gameObject.SetActive(false);
				return;
			}
			foreach (AccessorySlot accessorySlot2 in Db.Get().AccessorySlots.resources)
			{
				Accessory accessory2 = storedMinionIdentity.GetAccessory(accessorySlot2);
				if (accessory2 != null)
				{
					component.AddSymbolOverride(accessorySlot2.targetSymbolId, accessory2.symbol, 0);
					controller.SetSymbolVisiblity(accessorySlot2.targetSymbolId, true);
				}
				else
				{
					controller.SetSymbolVisiblity(accessorySlot2.targetSymbolId, false);
				}
			}
			component.AddSymbolOverride(Db.Get().AccessorySlots.HatHair.targetSymbolId, Db.Get().AccessorySlots.HatHair.Lookup("hat_" + HashCache.Get().Get(storedMinionIdentity.GetAccessory(Db.Get().AccessorySlots.Hair).symbol.hash)).symbol, 1);
			CrewPortrait.RefreshHat(storedMinionIdentity, controller);
		}
		float num = 0.25f;
		controller.animScale = num;
		string text = "ui_idle";
		controller.Play(text, KAnim.PlayMode.Loop, 1f, 0f);
		controller.SetSymbolVisiblity("snapTo_neck", false);
		controller.SetSymbolVisiblity("snapTo_goggles", false);
	}

	// Token: 0x06005638 RID: 22072 RVA: 0x001F3400 File Offset: 0x001F1600
	public void SetAlpha(float value)
	{
		if (this.controller == null)
		{
			return;
		}
		if ((float)this.controller.TintColour.a != value)
		{
			this.controller.TintColour = new Color(1f, 1f, 1f, value);
		}
	}

	// Token: 0x04003AA2 RID: 15010
	public Image targetImage;

	// Token: 0x04003AA3 RID: 15011
	public bool startTransparent;

	// Token: 0x04003AA4 RID: 15012
	public bool useLabels = true;

	// Token: 0x04003AA5 RID: 15013
	[SerializeField]
	public KBatchedAnimController controller;

	// Token: 0x04003AA6 RID: 15014
	public float animScaleBase = 0.2f;

	// Token: 0x04003AA7 RID: 15015
	public LocText duplicantName;

	// Token: 0x04003AA8 RID: 15016
	public LocText duplicantJob;

	// Token: 0x04003AA9 RID: 15017
	public LocText subTitle;

	// Token: 0x04003AAA RID: 15018
	public bool useDefaultExpression = true;

	// Token: 0x04003AAB RID: 15019
	private bool requiresRefresh;

	// Token: 0x04003AAC RID: 15020
	private bool areEventsRegistered;
}
