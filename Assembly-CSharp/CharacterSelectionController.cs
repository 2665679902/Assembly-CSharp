using System;
using System.Collections.Generic;
using Klei.CustomSettings;
using UnityEngine;

// Token: 0x020009F7 RID: 2551
public class CharacterSelectionController : KModalScreen
{
	// Token: 0x170005C0 RID: 1472
	// (get) Token: 0x06004C97 RID: 19607 RVA: 0x001AF145 File Offset: 0x001AD345
	// (set) Token: 0x06004C98 RID: 19608 RVA: 0x001AF14D File Offset: 0x001AD34D
	public bool IsStarterMinion { get; set; }

	// Token: 0x170005C1 RID: 1473
	// (get) Token: 0x06004C99 RID: 19609 RVA: 0x001AF156 File Offset: 0x001AD356
	public bool AllowsReplacing
	{
		get
		{
			return this.allowsReplacing;
		}
	}

	// Token: 0x06004C9A RID: 19610 RVA: 0x001AF15E File Offset: 0x001AD35E
	protected virtual void OnProceed()
	{
	}

	// Token: 0x06004C9B RID: 19611 RVA: 0x001AF160 File Offset: 0x001AD360
	protected virtual void OnDeliverableAdded()
	{
	}

	// Token: 0x06004C9C RID: 19612 RVA: 0x001AF162 File Offset: 0x001AD362
	protected virtual void OnDeliverableRemoved()
	{
	}

	// Token: 0x06004C9D RID: 19613 RVA: 0x001AF164 File Offset: 0x001AD364
	protected virtual void OnLimitReached()
	{
	}

	// Token: 0x06004C9E RID: 19614 RVA: 0x001AF166 File Offset: 0x001AD366
	protected virtual void OnLimitUnreached()
	{
	}

	// Token: 0x06004C9F RID: 19615 RVA: 0x001AF168 File Offset: 0x001AD368
	protected virtual void InitializeContainers()
	{
		this.DisableProceedButton();
		if (this.containers != null && this.containers.Count > 0)
		{
			return;
		}
		this.OnReplacedEvent = null;
		this.containers = new List<ITelepadDeliverableContainer>();
		if (this.IsStarterMinion || CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.CarePackages).id != "Enabled")
		{
			this.numberOfDuplicantOptions = 3;
			this.numberOfCarePackageOptions = 0;
		}
		else
		{
			this.numberOfCarePackageOptions = ((UnityEngine.Random.Range(0, 101) > 70) ? 2 : 1);
			this.numberOfDuplicantOptions = 4 - this.numberOfCarePackageOptions;
		}
		for (int i = 0; i < this.numberOfDuplicantOptions; i++)
		{
			CharacterContainer characterContainer = Util.KInstantiateUI<CharacterContainer>(this.containerPrefab.gameObject, this.containerParent, false);
			characterContainer.SetController(this);
			this.containers.Add(characterContainer);
		}
		for (int j = 0; j < this.numberOfCarePackageOptions; j++)
		{
			CarePackageContainer carePackageContainer = Util.KInstantiateUI<CarePackageContainer>(this.carePackageContainerPrefab.gameObject, this.containerParent, false);
			carePackageContainer.SetController(this);
			this.containers.Add(carePackageContainer);
			carePackageContainer.gameObject.transform.SetSiblingIndex(UnityEngine.Random.Range(0, carePackageContainer.transform.parent.childCount));
		}
		this.selectedDeliverables = new List<ITelepadDeliverable>();
	}

	// Token: 0x06004CA0 RID: 19616 RVA: 0x001AF2AC File Offset: 0x001AD4AC
	public virtual void OnPressBack()
	{
		foreach (ITelepadDeliverableContainer telepadDeliverableContainer in this.containers)
		{
			CharacterContainer characterContainer = telepadDeliverableContainer as CharacterContainer;
			if (characterContainer != null)
			{
				characterContainer.ForceStopEditingTitle();
			}
		}
		this.Show(false);
	}

	// Token: 0x06004CA1 RID: 19617 RVA: 0x001AF314 File Offset: 0x001AD514
	public void RemoveLast()
	{
		if (this.selectedDeliverables == null || this.selectedDeliverables.Count == 0)
		{
			return;
		}
		ITelepadDeliverable telepadDeliverable = this.selectedDeliverables[this.selectedDeliverables.Count - 1];
		if (this.OnReplacedEvent != null)
		{
			this.OnReplacedEvent(telepadDeliverable);
		}
	}

	// Token: 0x06004CA2 RID: 19618 RVA: 0x001AF364 File Offset: 0x001AD564
	public void AddDeliverable(ITelepadDeliverable deliverable)
	{
		if (this.selectedDeliverables.Contains(deliverable))
		{
			global::Debug.Log("Tried to add the same minion twice.");
			return;
		}
		if (this.selectedDeliverables.Count >= this.selectableCount)
		{
			global::Debug.LogError("Tried to add minions beyond the allowed limit");
			return;
		}
		this.selectedDeliverables.Add(deliverable);
		this.OnDeliverableAdded();
		if (this.selectedDeliverables.Count == this.selectableCount)
		{
			this.EnableProceedButton();
			if (this.OnLimitReachedEvent != null)
			{
				this.OnLimitReachedEvent();
			}
			this.OnLimitReached();
		}
	}

	// Token: 0x06004CA3 RID: 19619 RVA: 0x001AF3EC File Offset: 0x001AD5EC
	public void RemoveDeliverable(ITelepadDeliverable deliverable)
	{
		bool flag = this.selectedDeliverables.Count >= this.selectableCount;
		this.selectedDeliverables.Remove(deliverable);
		this.OnDeliverableRemoved();
		if (flag && this.selectedDeliverables.Count < this.selectableCount)
		{
			this.DisableProceedButton();
			if (this.OnLimitUnreachedEvent != null)
			{
				this.OnLimitUnreachedEvent();
			}
			this.OnLimitUnreached();
		}
	}

	// Token: 0x06004CA4 RID: 19620 RVA: 0x001AF456 File Offset: 0x001AD656
	public bool IsSelected(ITelepadDeliverable deliverable)
	{
		return this.selectedDeliverables.Contains(deliverable);
	}

	// Token: 0x06004CA5 RID: 19621 RVA: 0x001AF464 File Offset: 0x001AD664
	protected void EnableProceedButton()
	{
		this.proceedButton.isInteractable = true;
		this.proceedButton.ClearOnClick();
		this.proceedButton.onClick += delegate
		{
			this.OnProceed();
		};
	}

	// Token: 0x06004CA6 RID: 19622 RVA: 0x001AF494 File Offset: 0x001AD694
	protected void DisableProceedButton()
	{
		this.proceedButton.ClearOnClick();
		this.proceedButton.isInteractable = false;
		this.proceedButton.onClick += delegate
		{
			KMonoBehaviour.PlaySound(GlobalAssets.GetSound("Negative", false));
		};
	}

	// Token: 0x0400326F RID: 12911
	[SerializeField]
	private CharacterContainer containerPrefab;

	// Token: 0x04003270 RID: 12912
	[SerializeField]
	private CarePackageContainer carePackageContainerPrefab;

	// Token: 0x04003271 RID: 12913
	[SerializeField]
	private GameObject containerParent;

	// Token: 0x04003272 RID: 12914
	[SerializeField]
	protected KButton proceedButton;

	// Token: 0x04003273 RID: 12915
	protected int numberOfDuplicantOptions = 3;

	// Token: 0x04003274 RID: 12916
	protected int numberOfCarePackageOptions;

	// Token: 0x04003275 RID: 12917
	[SerializeField]
	protected int selectableCount;

	// Token: 0x04003276 RID: 12918
	[SerializeField]
	private bool allowsReplacing;

	// Token: 0x04003278 RID: 12920
	protected List<ITelepadDeliverable> selectedDeliverables;

	// Token: 0x04003279 RID: 12921
	protected List<ITelepadDeliverableContainer> containers;

	// Token: 0x0400327A RID: 12922
	public System.Action OnLimitReachedEvent;

	// Token: 0x0400327B RID: 12923
	public System.Action OnLimitUnreachedEvent;

	// Token: 0x0400327C RID: 12924
	public Action<bool> OnReshuffleEvent;

	// Token: 0x0400327D RID: 12925
	public Action<ITelepadDeliverable> OnReplacedEvent;

	// Token: 0x0400327E RID: 12926
	public System.Action OnProceedEvent;
}
