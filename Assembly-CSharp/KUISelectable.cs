using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000ACC RID: 2764
[AddComponentMenu("KMonoBehaviour/scripts/KUISelectable")]
public class KUISelectable : KMonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
{
	// Token: 0x060054B4 RID: 21684 RVA: 0x001EBA84 File Offset: 0x001E9C84
	protected override void OnPrefabInit()
	{
	}

	// Token: 0x060054B5 RID: 21685 RVA: 0x001EBA86 File Offset: 0x001E9C86
	protected override void OnSpawn()
	{
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClick));
	}

	// Token: 0x060054B6 RID: 21686 RVA: 0x001EBAA4 File Offset: 0x001E9CA4
	public void SetTarget(GameObject target)
	{
		this.target = target;
	}

	// Token: 0x060054B7 RID: 21687 RVA: 0x001EBAAD File Offset: 0x001E9CAD
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (this.target != null)
		{
			SelectTool.Instance.SetHoverOverride(this.target.GetComponent<KSelectable>());
		}
	}

	// Token: 0x060054B8 RID: 21688 RVA: 0x001EBAD2 File Offset: 0x001E9CD2
	public void OnPointerExit(PointerEventData eventData)
	{
		SelectTool.Instance.SetHoverOverride(null);
	}

	// Token: 0x060054B9 RID: 21689 RVA: 0x001EBADF File Offset: 0x001E9CDF
	private void OnClick()
	{
		if (this.target != null)
		{
			SelectTool.Instance.Select(this.target.GetComponent<KSelectable>(), false);
		}
	}

	// Token: 0x060054BA RID: 21690 RVA: 0x001EBB05 File Offset: 0x001E9D05
	protected override void OnCmpDisable()
	{
		if (SelectTool.Instance != null)
		{
			SelectTool.Instance.SetHoverOverride(null);
		}
	}

	// Token: 0x04003995 RID: 14741
	private GameObject target;
}
