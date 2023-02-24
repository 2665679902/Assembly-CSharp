using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000B2C RID: 2860
[AddComponentMenu("KMonoBehaviour/scripts/MinionEquipmentPanel")]
public class MinionEquipmentPanel : KMonoBehaviour
{
	// Token: 0x06005843 RID: 22595 RVA: 0x001FF141 File Offset: 0x001FD341
	public MinionEquipmentPanel()
	{
		this.refreshDelegate = new Action<object>(this.Refresh);
	}

	// Token: 0x06005844 RID: 22596 RVA: 0x001FF168 File Offset: 0x001FD368
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.roomPanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.roomPanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.PERSONALITY.EQUIPMENT.GROUPNAME_ROOMS;
		this.roomPanel.SetActive(true);
		this.ownablePanel = Util.KInstantiateUI(ScreenPrefabs.Instance.CollapsableContentPanel, base.gameObject, false);
		this.ownablePanel.GetComponent<CollapsibleDetailContentPanel>().HeaderLabel.text = UI.DETAILTABS.PERSONALITY.EQUIPMENT.GROUPNAME_OWNABLE;
		this.ownablePanel.SetActive(true);
	}

	// Token: 0x06005845 RID: 22597 RVA: 0x001FF20C File Offset: 0x001FD40C
	public void SetSelectedMinion(GameObject minion)
	{
		if (this.SelectedMinion != null)
		{
			this.SelectedMinion.Unsubscribe(-448952673, this.refreshDelegate);
			this.SelectedMinion.Unsubscribe(-1285462312, this.refreshDelegate);
			this.SelectedMinion.Unsubscribe(-1585839766, this.refreshDelegate);
		}
		this.SelectedMinion = minion;
		this.SelectedMinion.Subscribe(-448952673, this.refreshDelegate);
		this.SelectedMinion.Subscribe(-1285462312, this.refreshDelegate);
		this.SelectedMinion.Subscribe(-1585839766, this.refreshDelegate);
		this.Refresh(null);
	}

	// Token: 0x06005846 RID: 22598 RVA: 0x001FF2BC File Offset: 0x001FD4BC
	public void Refresh(object data = null)
	{
		if (this.SelectedMinion == null)
		{
			return;
		}
		this.Build();
	}

	// Token: 0x06005847 RID: 22599 RVA: 0x001FF2D4 File Offset: 0x001FD4D4
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.SelectedMinion != null)
		{
			this.SelectedMinion.Unsubscribe(-448952673, this.refreshDelegate);
			this.SelectedMinion.Unsubscribe(-1285462312, this.refreshDelegate);
			this.SelectedMinion.Unsubscribe(-1585839766, this.refreshDelegate);
		}
	}

	// Token: 0x06005848 RID: 22600 RVA: 0x001FF338 File Offset: 0x001FD538
	private GameObject AddOrGetLabel(Dictionary<string, GameObject> labels, GameObject panel, string id)
	{
		GameObject gameObject;
		if (labels.ContainsKey(id))
		{
			gameObject = labels[id];
		}
		else
		{
			gameObject = Util.KInstantiate(this.labelTemplate, panel.GetComponent<CollapsibleDetailContentPanel>().Content.gameObject, null);
			gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
			labels[id] = gameObject;
		}
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06005849 RID: 22601 RVA: 0x001FF3A6 File Offset: 0x001FD5A6
	private void Build()
	{
		this.ShowAssignables(this.SelectedMinion.GetComponent<MinionIdentity>().GetSoleOwner(), this.roomPanel);
		this.ShowAssignables(this.SelectedMinion.GetComponent<MinionIdentity>().GetEquipment(), this.ownablePanel);
	}

	// Token: 0x0600584A RID: 22602 RVA: 0x001FF3E0 File Offset: 0x001FD5E0
	private void ShowAssignables(Assignables assignables, GameObject panel)
	{
		bool flag = false;
		foreach (AssignableSlotInstance assignableSlotInstance in assignables.Slots)
		{
			if (assignableSlotInstance.slot.showInUI)
			{
				GameObject gameObject = this.AddOrGetLabel(this.labels, panel, assignableSlotInstance.slot.Name);
				if (assignableSlotInstance.IsAssigned())
				{
					gameObject.SetActive(true);
					flag = true;
					string text = (assignableSlotInstance.IsAssigned() ? assignableSlotInstance.assignable.GetComponent<KSelectable>().GetName() : UI.DETAILTABS.PERSONALITY.EQUIPMENT.NO_ASSIGNABLES.text);
					gameObject.GetComponent<LocText>().text = string.Format("{0}: {1}", assignableSlotInstance.slot.Name, text);
					gameObject.GetComponent<ToolTip>().toolTip = string.Format(UI.DETAILTABS.PERSONALITY.EQUIPMENT.ASSIGNED_TOOLTIP, text, this.GetAssignedEffectsString(assignableSlotInstance), this.SelectedMinion.name);
				}
				else
				{
					gameObject.SetActive(false);
					gameObject.GetComponent<LocText>().text = UI.DETAILTABS.PERSONALITY.EQUIPMENT.NO_ASSIGNABLES;
					gameObject.GetComponent<ToolTip>().toolTip = UI.DETAILTABS.PERSONALITY.EQUIPMENT.NO_ASSIGNABLES_TOOLTIP;
				}
			}
		}
		if (assignables is Ownables)
		{
			if (!flag)
			{
				GameObject gameObject2 = this.AddOrGetLabel(this.labels, panel, "NothingAssigned");
				this.labels["NothingAssigned"].SetActive(true);
				gameObject2.GetComponent<LocText>().text = UI.DETAILTABS.PERSONALITY.EQUIPMENT.NO_ASSIGNABLES;
				gameObject2.GetComponent<ToolTip>().toolTip = string.Format(UI.DETAILTABS.PERSONALITY.EQUIPMENT.NO_ASSIGNABLES_TOOLTIP, this.SelectedMinion.name);
			}
			else if (this.labels.ContainsKey("NothingAssigned"))
			{
				this.labels["NothingAssigned"].SetActive(false);
			}
		}
		if (assignables is Equipment)
		{
			if (!flag)
			{
				GameObject gameObject3 = this.AddOrGetLabel(this.labels, panel, "NoSuitAssigned");
				this.labels["NoSuitAssigned"].SetActive(true);
				gameObject3.GetComponent<LocText>().text = UI.DETAILTABS.PERSONALITY.EQUIPMENT.NOEQUIPMENT;
				gameObject3.GetComponent<ToolTip>().toolTip = string.Format(UI.DETAILTABS.PERSONALITY.EQUIPMENT.NOEQUIPMENT_TOOLTIP, this.SelectedMinion.name);
				return;
			}
			if (this.labels.ContainsKey("NoSuitAssigned"))
			{
				this.labels["NoSuitAssigned"].SetActive(false);
			}
		}
	}

	// Token: 0x0600584B RID: 22603 RVA: 0x001FF658 File Offset: 0x001FD858
	private string GetAssignedEffectsString(AssignableSlotInstance slot)
	{
		string text = "";
		List<Descriptor> list = new List<Descriptor>();
		list.AddRange(GameUtil.GetGameObjectEffects(slot.assignable.gameObject, false));
		if (list.Count > 0)
		{
			text += "\n";
			foreach (Descriptor descriptor in list)
			{
				text = text + "  • " + descriptor.IndentedText() + "\n";
			}
		}
		return text;
	}

	// Token: 0x04003BBA RID: 15290
	public GameObject SelectedMinion;

	// Token: 0x04003BBB RID: 15291
	public GameObject labelTemplate;

	// Token: 0x04003BBC RID: 15292
	private GameObject roomPanel;

	// Token: 0x04003BBD RID: 15293
	private GameObject ownablePanel;

	// Token: 0x04003BBE RID: 15294
	private Storage storage;

	// Token: 0x04003BBF RID: 15295
	private Dictionary<string, GameObject> labels = new Dictionary<string, GameObject>();

	// Token: 0x04003BC0 RID: 15296
	private Action<object> refreshDelegate;
}
