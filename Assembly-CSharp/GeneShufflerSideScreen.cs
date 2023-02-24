using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000BB5 RID: 2997
public class GeneShufflerSideScreen : SideScreenContent
{
	// Token: 0x06005E37 RID: 24119 RVA: 0x00226D53 File Offset: 0x00224F53
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.button.onClick += this.OnButtonClick;
		this.Refresh();
	}

	// Token: 0x06005E38 RID: 24120 RVA: 0x00226D78 File Offset: 0x00224F78
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<GeneShuffler>() != null;
	}

	// Token: 0x06005E39 RID: 24121 RVA: 0x00226D88 File Offset: 0x00224F88
	public override void SetTarget(GameObject target)
	{
		GeneShuffler component = target.GetComponent<GeneShuffler>();
		if (component == null)
		{
			global::Debug.LogError("Target doesn't have a GeneShuffler associated with it.");
			return;
		}
		this.target = component;
		this.Refresh();
	}

	// Token: 0x06005E3A RID: 24122 RVA: 0x00226DC0 File Offset: 0x00224FC0
	private void OnButtonClick()
	{
		if (this.target.WorkComplete)
		{
			this.target.SetWorkTime(0f);
			return;
		}
		if (this.target.IsConsumed)
		{
			this.target.RequestRecharge(!this.target.RechargeRequested);
			this.Refresh();
		}
	}

	// Token: 0x06005E3B RID: 24123 RVA: 0x00226E18 File Offset: 0x00225018
	private void Refresh()
	{
		if (!(this.target != null))
		{
			this.contents.SetActive(false);
			return;
		}
		if (this.target.WorkComplete)
		{
			this.contents.SetActive(true);
			this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.COMPLETE;
			this.button.gameObject.SetActive(true);
			this.buttonLabel.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.BUTTON;
			return;
		}
		if (this.target.IsConsumed)
		{
			this.contents.SetActive(true);
			this.button.gameObject.SetActive(true);
			if (this.target.RechargeRequested)
			{
				this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.CONSUMED_WAITING;
				this.buttonLabel.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.BUTTON_RECHARGE_CANCEL;
				return;
			}
			this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.CONSUMED;
			this.buttonLabel.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.BUTTON_RECHARGE;
			return;
		}
		else
		{
			if (this.target.IsWorking)
			{
				this.contents.SetActive(true);
				this.label.text = UI.UISIDESCREENS.GENESHUFFLERSIDESREEN.UNDERWAY;
				this.button.gameObject.SetActive(false);
				return;
			}
			this.contents.SetActive(false);
			return;
		}
	}

	// Token: 0x04004074 RID: 16500
	[SerializeField]
	private LocText label;

	// Token: 0x04004075 RID: 16501
	[SerializeField]
	private KButton button;

	// Token: 0x04004076 RID: 16502
	[SerializeField]
	private LocText buttonLabel;

	// Token: 0x04004077 RID: 16503
	[SerializeField]
	private GeneShuffler target;

	// Token: 0x04004078 RID: 16504
	[SerializeField]
	private GameObject contents;
}
