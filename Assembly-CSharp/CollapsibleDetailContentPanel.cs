using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A8B RID: 2699
[AddComponentMenu("KMonoBehaviour/scripts/CollapsibleDetailContentPanel")]
public class CollapsibleDetailContentPanel : KMonoBehaviour
{
	// Token: 0x060052A7 RID: 21159 RVA: 0x001DE1B4 File Offset: 0x001DC3B4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.collapseButton;
		multiToggle.onClick = (System.Action)Delegate.Combine(multiToggle.onClick, new System.Action(this.ToggleOpen));
		this.ArrowIcon.SetActive();
		this.log = new LoggerFSS("detailpanel", 35);
		this.labels = new Dictionary<string, CollapsibleDetailContentPanel.Label<DetailLabel>>();
		this.buttonLabels = new Dictionary<string, CollapsibleDetailContentPanel.Label<DetailLabelWithButton>>();
		this.Commit();
	}

	// Token: 0x060052A8 RID: 21160 RVA: 0x001DE227 File Offset: 0x001DC427
	public void SetTitle(string title)
	{
		this.HeaderLabel.text = title;
	}

	// Token: 0x060052A9 RID: 21161 RVA: 0x001DE238 File Offset: 0x001DC438
	public void Commit()
	{
		int num = 0;
		foreach (CollapsibleDetailContentPanel.Label<DetailLabel> label in this.labels.Values)
		{
			if (label.used)
			{
				num++;
				if (!label.obj.gameObject.activeSelf)
				{
					label.obj.gameObject.SetActive(true);
				}
			}
			else if (!label.used && label.obj.gameObject.activeSelf)
			{
				label.obj.gameObject.SetActive(false);
			}
			label.used = false;
		}
		foreach (CollapsibleDetailContentPanel.Label<DetailLabelWithButton> label2 in this.buttonLabels.Values)
		{
			if (label2.used)
			{
				num++;
				if (!label2.obj.gameObject.activeSelf)
				{
					label2.obj.gameObject.SetActive(true);
				}
			}
			else if (!label2.used && label2.obj.gameObject.activeSelf)
			{
				label2.obj.gameObject.SetActive(false);
			}
			label2.used = false;
		}
		if (base.gameObject.activeSelf && num == 0)
		{
			base.gameObject.SetActive(false);
			return;
		}
		if (!base.gameObject.activeSelf && num > 0)
		{
			base.gameObject.SetActive(true);
		}
	}

	// Token: 0x060052AA RID: 21162 RVA: 0x001DE3D4 File Offset: 0x001DC5D4
	public void SetLabel(string id, string text, string tooltip)
	{
		CollapsibleDetailContentPanel.Label<DetailLabel> label;
		if (!this.labels.TryGetValue(id, out label))
		{
			label = new CollapsibleDetailContentPanel.Label<DetailLabel>
			{
				used = true,
				obj = Util.KInstantiateUI(this.labelTemplate.gameObject, this.Content.gameObject, false).GetComponent<DetailLabel>()
			};
			label.obj.gameObject.name = id;
			this.labels[id] = label;
		}
		label.obj.label.AllowLinks = true;
		label.obj.label.text = text;
		label.obj.toolTip.toolTip = tooltip;
		label.used = true;
	}

	// Token: 0x060052AB RID: 21163 RVA: 0x001DE480 File Offset: 0x001DC680
	public void SetLabelWithButton(string id, string text, string tooltip, string buttonText, string buttonTooltip, System.Action buttonCb)
	{
		CollapsibleDetailContentPanel.Label<DetailLabelWithButton> label;
		if (!this.buttonLabels.TryGetValue(id, out label))
		{
			label = new CollapsibleDetailContentPanel.Label<DetailLabelWithButton>
			{
				used = true,
				obj = Util.KInstantiateUI(this.labelWithActionButtonTemplate.gameObject, this.Content.gameObject, false).GetComponent<DetailLabelWithButton>()
			};
			label.obj.gameObject.name = id;
			this.buttonLabels[id] = label;
		}
		label.obj.label.AllowLinks = true;
		label.obj.label.text = text;
		label.obj.toolTip.toolTip = tooltip;
		label.obj.buttonLabel.text = buttonText;
		label.obj.buttonToolTip.toolTip = buttonTooltip;
		label.obj.button.ClearOnClick();
		label.obj.button.onClick += buttonCb;
		label.used = true;
	}

	// Token: 0x060052AC RID: 21164 RVA: 0x001DE570 File Offset: 0x001DC770
	private void ToggleOpen()
	{
		bool flag = this.scalerMask.gameObject.activeSelf;
		flag = !flag;
		this.scalerMask.gameObject.SetActive(flag);
		if (flag)
		{
			this.ArrowIcon.SetActive();
			this.ForceLocTextsMeshRebuild();
			return;
		}
		this.ArrowIcon.SetInactive();
	}

	// Token: 0x060052AD RID: 21165 RVA: 0x001DE5C4 File Offset: 0x001DC7C4
	public void ForceLocTextsMeshRebuild()
	{
		LocText[] componentsInChildren = base.GetComponentsInChildren<LocText>();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			componentsInChildren[i].ForceMeshUpdate();
		}
	}

	// Token: 0x040037DF RID: 14303
	public ImageToggleState ArrowIcon;

	// Token: 0x040037E0 RID: 14304
	public LocText HeaderLabel;

	// Token: 0x040037E1 RID: 14305
	public MultiToggle collapseButton;

	// Token: 0x040037E2 RID: 14306
	public Transform Content;

	// Token: 0x040037E3 RID: 14307
	public ScalerMask scalerMask;

	// Token: 0x040037E4 RID: 14308
	[Space(10f)]
	public DetailLabel labelTemplate;

	// Token: 0x040037E5 RID: 14309
	public DetailLabelWithButton labelWithActionButtonTemplate;

	// Token: 0x040037E6 RID: 14310
	private Dictionary<string, CollapsibleDetailContentPanel.Label<DetailLabel>> labels;

	// Token: 0x040037E7 RID: 14311
	private Dictionary<string, CollapsibleDetailContentPanel.Label<DetailLabelWithButton>> buttonLabels;

	// Token: 0x040037E8 RID: 14312
	private LoggerFSS log;

	// Token: 0x02001912 RID: 6418
	private class Label<T>
	{
		// Token: 0x04007335 RID: 29493
		public T obj;

		// Token: 0x04007336 RID: 29494
		public bool used;
	}
}
