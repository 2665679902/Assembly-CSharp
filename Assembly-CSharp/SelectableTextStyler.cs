using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000B85 RID: 2949
public class SelectableTextStyler : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, IPointerClickHandler
{
	// Token: 0x06005CAC RID: 23724 RVA: 0x0021E3B5 File Offset: 0x0021C5B5
	private void Start()
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Normal);
	}

	// Token: 0x06005CAD RID: 23725 RVA: 0x0021E3C4 File Offset: 0x0021C5C4
	private void SetState(SelectableTextStyler.State state, SelectableTextStyler.HoverState hover_state)
	{
		if (state == SelectableTextStyler.State.Normal)
		{
			if (hover_state != SelectableTextStyler.HoverState.Normal)
			{
				if (hover_state == SelectableTextStyler.HoverState.Hovered)
				{
					this.target.textStyleSetting = this.normalHovered;
				}
			}
			else
			{
				this.target.textStyleSetting = this.normalNormal;
			}
		}
		this.target.ApplySettings();
	}

	// Token: 0x06005CAE RID: 23726 RVA: 0x0021E401 File Offset: 0x0021C601
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Hovered);
	}

	// Token: 0x06005CAF RID: 23727 RVA: 0x0021E410 File Offset: 0x0021C610
	public void OnPointerExit(PointerEventData eventData)
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Normal);
	}

	// Token: 0x06005CB0 RID: 23728 RVA: 0x0021E41F File Offset: 0x0021C61F
	public void OnPointerClick(PointerEventData eventData)
	{
		this.SetState(this.state, SelectableTextStyler.HoverState.Normal);
	}

	// Token: 0x04003F59 RID: 16217
	[SerializeField]
	private LocText target;

	// Token: 0x04003F5A RID: 16218
	[SerializeField]
	private SelectableTextStyler.State state;

	// Token: 0x04003F5B RID: 16219
	[SerializeField]
	private TextStyleSetting normalNormal;

	// Token: 0x04003F5C RID: 16220
	[SerializeField]
	private TextStyleSetting normalHovered;

	// Token: 0x02001A44 RID: 6724
	public enum State
	{
		// Token: 0x04007718 RID: 30488
		Normal
	}

	// Token: 0x02001A45 RID: 6725
	public enum HoverState
	{
		// Token: 0x0400771A RID: 30490
		Normal,
		// Token: 0x0400771B RID: 30491
		Hovered
	}
}
