using System;
using UnityEngine;

// Token: 0x02000490 RID: 1168
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/KSelectable")]
public class KSelectable : KMonoBehaviour
{
	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0008BA57 File Offset: 0x00089C57
	public bool IsSelected
	{
		get
		{
			return this.selected;
		}
	}

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0008BA5F File Offset: 0x00089C5F
	// (set) Token: 0x06001A27 RID: 6695 RVA: 0x0008BA71 File Offset: 0x00089C71
	public bool IsSelectable
	{
		get
		{
			return this.selectable && base.isActiveAndEnabled;
		}
		set
		{
			this.selectable = value;
		}
	}

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0008BA7A File Offset: 0x00089C7A
	public bool DisableSelectMarker
	{
		get
		{
			return this.disableSelectMarker;
		}
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x0008BA84 File Offset: 0x00089C84
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.statusItemGroup = new StatusItemGroup(base.gameObject);
		base.GetComponent<KPrefabID>() != null;
		if (this.entityName == null || this.entityName.Length <= 0)
		{
			this.SetName(base.name);
		}
		if (this.entityGender == null)
		{
			this.entityGender = "NB";
		}
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x0008BAEC File Offset: 0x00089CEC
	public virtual string GetName()
	{
		if (this.entityName == null || this.entityName == "" || this.entityName.Length <= 0)
		{
			global::Debug.Log("Warning Item has blank name!", base.gameObject);
			return base.name;
		}
		return this.entityName;
	}

	// Token: 0x06001A2B RID: 6699 RVA: 0x0008BB3E File Offset: 0x00089D3E
	public void SetStatusIndicatorOffset(Vector3 offset)
	{
		if (this.statusItemGroup == null)
		{
			return;
		}
		this.statusItemGroup.SetOffset(offset);
	}

	// Token: 0x06001A2C RID: 6700 RVA: 0x0008BB55 File Offset: 0x00089D55
	public void SetName(string name)
	{
		this.entityName = name;
	}

	// Token: 0x06001A2D RID: 6701 RVA: 0x0008BB5E File Offset: 0x00089D5E
	public void SetGender(string Gender)
	{
		this.entityGender = Gender;
	}

	// Token: 0x06001A2E RID: 6702 RVA: 0x0008BB68 File Offset: 0x00089D68
	public float GetZoom()
	{
		Bounds bounds = Util.GetBounds(base.gameObject);
		return 1.05f * Mathf.Max(bounds.extents.x, bounds.extents.y);
	}

	// Token: 0x06001A2F RID: 6703 RVA: 0x0008BBA4 File Offset: 0x00089DA4
	public Vector3 GetPortraitLocation()
	{
		return Util.GetBounds(base.gameObject).center;
	}

	// Token: 0x06001A30 RID: 6704 RVA: 0x0008BBC4 File Offset: 0x00089DC4
	private void ClearHighlight()
	{
		base.Trigger(-1201923725, false);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.HighlightColour = new Color(0f, 0f, 0f, 0f);
		}
	}

	// Token: 0x06001A31 RID: 6705 RVA: 0x0008BC18 File Offset: 0x00089E18
	private void ApplyHighlight(float highlight)
	{
		base.Trigger(-1201923725, true);
		KBatchedAnimController component = base.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			component.HighlightColour = new Color(highlight, highlight, highlight, highlight);
		}
	}

	// Token: 0x06001A32 RID: 6706 RVA: 0x0008BC5C File Offset: 0x00089E5C
	public void Select()
	{
		this.selected = true;
		this.ClearHighlight();
		this.ApplyHighlight(0.2f);
		base.Trigger(-1503271301, true);
		if (base.GetComponent<LoopingSounds>() != null)
		{
			base.GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		if (base.transform.GetComponentInParent<LoopingSounds>() != null)
		{
			base.transform.GetComponentInParent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		int childCount = base.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			int childCount2 = base.transform.GetChild(i).childCount;
			for (int j = 0; j < childCount2; j++)
			{
				if (base.transform.GetChild(i).transform.GetChild(j).GetComponent<LoopingSounds>() != null)
				{
					base.transform.GetChild(i).transform.GetChild(j).GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
				}
			}
		}
		this.UpdateWorkerSelection(this.selected);
		this.UpdateWorkableSelection(this.selected);
	}

	// Token: 0x06001A33 RID: 6707 RVA: 0x0008BD74 File Offset: 0x00089F74
	public void Unselect()
	{
		if (this.selected)
		{
			this.selected = false;
			this.ClearHighlight();
			base.Trigger(-1503271301, false);
		}
		if (base.GetComponent<LoopingSounds>() != null)
		{
			base.GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		if (base.transform.GetComponentInParent<LoopingSounds>() != null)
		{
			base.transform.GetComponentInParent<LoopingSounds>().UpdateObjectSelection(this.selected);
		}
		foreach (object obj in base.transform)
		{
			Transform transform = (Transform)obj;
			if (transform.GetComponent<LoopingSounds>() != null)
			{
				transform.GetComponent<LoopingSounds>().UpdateObjectSelection(this.selected);
			}
		}
		this.UpdateWorkerSelection(this.selected);
		this.UpdateWorkableSelection(this.selected);
	}

	// Token: 0x06001A34 RID: 6708 RVA: 0x0008BE6C File Offset: 0x0008A06C
	public void Hover(bool playAudio)
	{
		this.ClearHighlight();
		if (!DebugHandler.HideUI)
		{
			this.ApplyHighlight(0.25f);
		}
		if (playAudio)
		{
			this.PlayHoverSound();
		}
	}

	// Token: 0x06001A35 RID: 6709 RVA: 0x0008BE8F File Offset: 0x0008A08F
	private void PlayHoverSound()
	{
		if (CellSelectionObject.IsSelectionObject(base.gameObject))
		{
			return;
		}
		UISounds.PlaySound(UISounds.Sound.Object_Mouseover);
	}

	// Token: 0x06001A36 RID: 6710 RVA: 0x0008BEA5 File Offset: 0x0008A0A5
	public void Unhover()
	{
		if (!this.selected)
		{
			this.ClearHighlight();
		}
	}

	// Token: 0x06001A37 RID: 6711 RVA: 0x0008BEB5 File Offset: 0x0008A0B5
	public Guid ToggleStatusItem(StatusItem status_item, bool on, object data = null)
	{
		if (on)
		{
			return this.AddStatusItem(status_item, data);
		}
		return this.RemoveStatusItem(status_item, false);
	}

	// Token: 0x06001A38 RID: 6712 RVA: 0x0008BECB File Offset: 0x0008A0CB
	public Guid ToggleStatusItem(StatusItem status_item, Guid guid, bool show, object data = null)
	{
		if (show)
		{
			if (guid != Guid.Empty)
			{
				return guid;
			}
			return this.AddStatusItem(status_item, data);
		}
		else
		{
			if (guid != Guid.Empty)
			{
				return this.RemoveStatusItem(guid, false);
			}
			return guid;
		}
	}

	// Token: 0x06001A39 RID: 6713 RVA: 0x0008BF00 File Offset: 0x0008A100
	public Guid SetStatusItem(StatusItemCategory category, StatusItem status_item, object data = null)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		return this.statusItemGroup.SetStatusItem(category, status_item, data);
	}

	// Token: 0x06001A3A RID: 6714 RVA: 0x0008BF1E File Offset: 0x0008A11E
	public Guid ReplaceStatusItem(Guid guid, StatusItem status_item, object data = null)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		if (guid != Guid.Empty)
		{
			this.statusItemGroup.RemoveStatusItem(guid, false);
		}
		return this.AddStatusItem(status_item, data);
	}

	// Token: 0x06001A3B RID: 6715 RVA: 0x0008BF51 File Offset: 0x0008A151
	public Guid AddStatusItem(StatusItem status_item, object data = null)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		return this.statusItemGroup.AddStatusItem(status_item, data, null);
	}

	// Token: 0x06001A3C RID: 6716 RVA: 0x0008BF6F File Offset: 0x0008A16F
	public Guid RemoveStatusItem(StatusItem status_item, bool immediate = false)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		this.statusItemGroup.RemoveStatusItem(status_item, immediate);
		return Guid.Empty;
	}

	// Token: 0x06001A3D RID: 6717 RVA: 0x0008BF92 File Offset: 0x0008A192
	public Guid RemoveStatusItem(Guid guid, bool immediate = false)
	{
		if (this.statusItemGroup == null)
		{
			return Guid.Empty;
		}
		this.statusItemGroup.RemoveStatusItem(guid, immediate);
		return Guid.Empty;
	}

	// Token: 0x06001A3E RID: 6718 RVA: 0x0008BFB5 File Offset: 0x0008A1B5
	public bool HasStatusItem(StatusItem status_item)
	{
		return this.statusItemGroup != null && this.statusItemGroup.HasStatusItem(status_item);
	}

	// Token: 0x06001A3F RID: 6719 RVA: 0x0008BFCD File Offset: 0x0008A1CD
	public StatusItemGroup.Entry GetStatusItem(StatusItemCategory category)
	{
		return this.statusItemGroup.GetStatusItem(category);
	}

	// Token: 0x06001A40 RID: 6720 RVA: 0x0008BFDB File Offset: 0x0008A1DB
	public StatusItemGroup GetStatusItemGroup()
	{
		return this.statusItemGroup;
	}

	// Token: 0x06001A41 RID: 6721 RVA: 0x0008BFE4 File Offset: 0x0008A1E4
	public void UpdateWorkerSelection(bool selected)
	{
		Workable[] components = base.GetComponents<Workable>();
		if (components.Length != 0)
		{
			for (int i = 0; i < components.Length; i++)
			{
				if (components[i].worker != null && components[i].GetComponent<LoopingSounds>() != null)
				{
					components[i].GetComponent<LoopingSounds>().UpdateObjectSelection(selected);
				}
			}
		}
	}

	// Token: 0x06001A42 RID: 6722 RVA: 0x0008C038 File Offset: 0x0008A238
	public void UpdateWorkableSelection(bool selected)
	{
		Worker component = base.GetComponent<Worker>();
		if (component != null && component.workable != null)
		{
			Workable workable = base.GetComponent<Worker>().workable;
			if (workable.GetComponent<LoopingSounds>() != null)
			{
				workable.GetComponent<LoopingSounds>().UpdateObjectSelection(selected);
			}
		}
	}

	// Token: 0x06001A43 RID: 6723 RVA: 0x0008C089 File Offset: 0x0008A289
	protected override void OnLoadLevel()
	{
		this.OnCleanUp();
		base.OnLoadLevel();
	}

	// Token: 0x06001A44 RID: 6724 RVA: 0x0008C098 File Offset: 0x0008A298
	protected override void OnCleanUp()
	{
		if (this.statusItemGroup != null)
		{
			this.statusItemGroup.Destroy();
			this.statusItemGroup = null;
		}
		if (this.selected && SelectTool.Instance != null)
		{
			if (SelectTool.Instance.selected == this)
			{
				SelectTool.Instance.Select(null, true);
			}
			else
			{
				this.Unselect();
			}
		}
		base.OnCleanUp();
	}

	// Token: 0x04000E97 RID: 3735
	private const float hoverHighlight = 0.25f;

	// Token: 0x04000E98 RID: 3736
	private const float selectHighlight = 0.2f;

	// Token: 0x04000E99 RID: 3737
	public string entityName;

	// Token: 0x04000E9A RID: 3738
	public string entityGender;

	// Token: 0x04000E9B RID: 3739
	private bool selected;

	// Token: 0x04000E9C RID: 3740
	[SerializeField]
	private bool selectable = true;

	// Token: 0x04000E9D RID: 3741
	[SerializeField]
	private bool disableSelectMarker;

	// Token: 0x04000E9E RID: 3742
	private StatusItemGroup statusItemGroup;
}
