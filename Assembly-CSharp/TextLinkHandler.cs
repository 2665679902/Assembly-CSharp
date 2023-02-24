using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000C0C RID: 3084
public class TextLinkHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler
{
	// Token: 0x060061C3 RID: 25027 RVA: 0x00241C6C File Offset: 0x0023FE6C
	public void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
		{
			return;
		}
		if (!this.text.AllowLinks)
		{
			return;
		}
		int num = TMP_TextUtilities.FindIntersectingLink(this.text, KInputManager.GetMousePos(), null);
		if (num != -1)
		{
			string text = CodexCache.FormatLinkID(this.text.textInfo.linkInfo[num].GetLinkID());
			if (!CodexCache.entries.ContainsKey(text))
			{
				SubEntry subEntry = CodexCache.FindSubEntry(text);
				if (subEntry == null || subEntry.disabled)
				{
					text = "PAGENOTFOUND";
				}
			}
			else if (CodexCache.entries[text].disabled)
			{
				text = "PAGENOTFOUND";
			}
			if (!ManagementMenu.Instance.codexScreen.gameObject.activeInHierarchy)
			{
				ManagementMenu.Instance.ToggleCodex();
			}
			ManagementMenu.Instance.codexScreen.ChangeArticle(text, true, default(Vector3), CodexScreen.HistoryDirection.NewArticle);
		}
	}

	// Token: 0x060061C4 RID: 25028 RVA: 0x00241D44 File Offset: 0x0023FF44
	private void Update()
	{
		this.CheckMouseOver();
		if (TextLinkHandler.hoveredText == this && this.text.AllowLinks)
		{
			PlayerController.Instance.ActiveTool.SetLinkCursor(this.hoverLink);
		}
	}

	// Token: 0x060061C5 RID: 25029 RVA: 0x00241D7B File Offset: 0x0023FF7B
	private void OnEnable()
	{
		this.CheckMouseOver();
	}

	// Token: 0x060061C6 RID: 25030 RVA: 0x00241D83 File Offset: 0x0023FF83
	private void OnDisable()
	{
		this.ClearState();
	}

	// Token: 0x060061C7 RID: 25031 RVA: 0x00241D8B File Offset: 0x0023FF8B
	private void Awake()
	{
		this.text = base.GetComponent<LocText>();
		if (this.text.AllowLinks && !this.text.raycastTarget)
		{
			this.text.raycastTarget = true;
		}
	}

	// Token: 0x060061C8 RID: 25032 RVA: 0x00241DBF File Offset: 0x0023FFBF
	public void OnPointerEnter(PointerEventData eventData)
	{
		this.SetMouseOver();
	}

	// Token: 0x060061C9 RID: 25033 RVA: 0x00241DC7 File Offset: 0x0023FFC7
	public void OnPointerExit(PointerEventData eventData)
	{
		this.ClearState();
	}

	// Token: 0x060061CA RID: 25034 RVA: 0x00241DD0 File Offset: 0x0023FFD0
	private void ClearState()
	{
		if (this == null || this.Equals(null))
		{
			return;
		}
		if (TextLinkHandler.hoveredText == this)
		{
			if (this.hoverLink && PlayerController.Instance != null && PlayerController.Instance.ActiveTool != null)
			{
				PlayerController.Instance.ActiveTool.SetLinkCursor(false);
			}
			TextLinkHandler.hoveredText = null;
			this.hoverLink = false;
		}
	}

	// Token: 0x060061CB RID: 25035 RVA: 0x00241E44 File Offset: 0x00240044
	public void CheckMouseOver()
	{
		if (this.text == null)
		{
			return;
		}
		if (TMP_TextUtilities.FindIntersectingLink(this.text, KInputManager.GetMousePos(), null) != -1)
		{
			this.SetMouseOver();
			this.hoverLink = true;
			return;
		}
		if (TextLinkHandler.hoveredText == this)
		{
			this.hoverLink = false;
		}
	}

	// Token: 0x060061CC RID: 25036 RVA: 0x00241E96 File Offset: 0x00240096
	private void SetMouseOver()
	{
		if (TextLinkHandler.hoveredText != null && TextLinkHandler.hoveredText != this)
		{
			TextLinkHandler.hoveredText.hoverLink = false;
		}
		TextLinkHandler.hoveredText = this;
	}

	// Token: 0x04004390 RID: 17296
	private static TextLinkHandler hoveredText;

	// Token: 0x04004391 RID: 17297
	[MyCmpGet]
	private LocText text;

	// Token: 0x04004392 RID: 17298
	private bool hoverLink;
}
