using System;
using System.Collections.Generic;
using Database;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B90 RID: 2960
public class ArtableSelectionSideScreen : SideScreenContent
{
	// Token: 0x06005D06 RID: 23814 RVA: 0x00220088 File Offset: 0x0021E288
	public override bool IsValidForTarget(GameObject target)
	{
		Artable component = target.GetComponent<Artable>();
		return !(component == null) && !(component.CurrentStage == "Default");
	}

	// Token: 0x06005D07 RID: 23815 RVA: 0x002200BC File Offset: 0x0021E2BC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.applyButton.onClick += delegate
		{
			this.target.SetUserChosenTargetState(this.selectedStage);
			SelectTool.Instance.Select(null, true);
		};
		this.clearButton.onClick += delegate
		{
			this.selectedStage = "";
			this.target.SetDefault();
			SelectTool.Instance.Select(null, true);
		};
	}

	// Token: 0x06005D08 RID: 23816 RVA: 0x002200F4 File Offset: 0x0021E2F4
	public override void SetTarget(GameObject target)
	{
		if (this.workCompleteSub != -1)
		{
			target.Unsubscribe(this.workCompleteSub);
			this.workCompleteSub = -1;
		}
		base.SetTarget(target);
		this.target = target.GetComponent<Artable>();
		this.workCompleteSub = target.Subscribe(-2011693419, new Action<object>(this.OnRefreshTarget));
		this.OnRefreshTarget(null);
	}

	// Token: 0x06005D09 RID: 23817 RVA: 0x00220154 File Offset: 0x0021E354
	public override void ClearTarget()
	{
		this.target.Unsubscribe(-2011693419);
		this.workCompleteSub = -1;
		base.ClearTarget();
	}

	// Token: 0x06005D0A RID: 23818 RVA: 0x00220173 File Offset: 0x0021E373
	private void OnRefreshTarget(object data = null)
	{
		if (this.target == null)
		{
			return;
		}
		this.GenerateStateButtons();
		this.selectedStage = this.target.CurrentStage;
		this.RefreshButtons();
	}

	// Token: 0x06005D0B RID: 23819 RVA: 0x002201A4 File Offset: 0x0021E3A4
	public void GenerateStateButtons()
	{
		foreach (KeyValuePair<string, MultiToggle> keyValuePair in this.buttons)
		{
			Util.KDestroyGameObject(keyValuePair.Value.gameObject);
		}
		this.buttons.Clear();
		foreach (ArtableStage artableStage in Db.GetArtableStages().GetPrefabStages(this.target.GetComponent<KPrefabID>().PrefabID()))
		{
			if (!(artableStage.id == "Default"))
			{
				GameObject gameObject = Util.KInstantiateUI(this.stateButtonPrefab, this.buttonContainer.gameObject, true);
				Sprite sprite = artableStage.GetPermitPresentationInfo().sprite;
				MultiToggle component = gameObject.GetComponent<MultiToggle>();
				component.GetComponent<ToolTip>().SetSimpleTooltip(artableStage.Name);
				component.GetComponent<HierarchyReferences>().GetReference<Image>("Icon").sprite = sprite;
				this.buttons.Add(artableStage.id, component);
			}
		}
	}

	// Token: 0x06005D0C RID: 23820 RVA: 0x002202DC File Offset: 0x0021E4DC
	private void RefreshButtons()
	{
		List<ArtableStage> prefabStages = Db.GetArtableStages().GetPrefabStages(this.target.GetComponent<KPrefabID>().PrefabID());
		ArtableStage artableStage = prefabStages.Find((ArtableStage match) => match.id == this.target.CurrentStage);
		int num = 0;
		using (Dictionary<string, MultiToggle>.Enumerator enumerator = this.buttons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				ArtableSelectionSideScreen.<>c__DisplayClass16_0 CS$<>8__locals1 = new ArtableSelectionSideScreen.<>c__DisplayClass16_0();
				CS$<>8__locals1.<>4__this = this;
				CS$<>8__locals1.kvp = enumerator.Current;
				ArtableStage stage = prefabStages.Find((ArtableStage match) => match.id == CS$<>8__locals1.kvp.Key);
				if (stage != null && artableStage != null && stage.statusItem.StatusType != artableStage.statusItem.StatusType)
				{
					CS$<>8__locals1.kvp.Value.gameObject.SetActive(false);
				}
				else if (!stage.IsUnlocked())
				{
					CS$<>8__locals1.kvp.Value.gameObject.SetActive(false);
				}
				else
				{
					num++;
					CS$<>8__locals1.kvp.Value.gameObject.SetActive(true);
					CS$<>8__locals1.kvp.Value.ChangeState((this.selectedStage == CS$<>8__locals1.kvp.Key) ? 1 : 0);
					MultiToggle value = CS$<>8__locals1.kvp.Value;
					value.onClick = (System.Action)Delegate.Combine(value.onClick, new System.Action(delegate
					{
						CS$<>8__locals1.<>4__this.selectedStage = stage.id;
						CS$<>8__locals1.<>4__this.RefreshButtons();
					}));
				}
			}
		}
		this.scrollTransoform.GetComponent<LayoutElement>().preferredHeight = (float)((num > 3) ? 200 : 100);
	}

	// Token: 0x04003F98 RID: 16280
	private Artable target;

	// Token: 0x04003F99 RID: 16281
	public KButton applyButton;

	// Token: 0x04003F9A RID: 16282
	public KButton clearButton;

	// Token: 0x04003F9B RID: 16283
	public GameObject stateButtonPrefab;

	// Token: 0x04003F9C RID: 16284
	private Dictionary<string, MultiToggle> buttons = new Dictionary<string, MultiToggle>();

	// Token: 0x04003F9D RID: 16285
	[SerializeField]
	private RectTransform scrollTransoform;

	// Token: 0x04003F9E RID: 16286
	private string selectedStage = "";

	// Token: 0x04003F9F RID: 16287
	private const int INVALID_SUBSCRIPTION = -1;

	// Token: 0x04003FA0 RID: 16288
	private int workCompleteSub = -1;

	// Token: 0x04003FA1 RID: 16289
	[SerializeField]
	private RectTransform buttonContainer;
}
