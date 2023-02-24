using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C27 RID: 3111
[AddComponentMenu("KMonoBehaviour/scripts/BreakdownListRow")]
public class BreakdownListRow : KMonoBehaviour
{
	// Token: 0x06006273 RID: 25203 RVA: 0x00245668 File Offset: 0x00243868
	public void ShowData(string name, string value)
	{
		base.gameObject.transform.localScale = Vector3.one;
		this.nameLabel.text = name;
		this.valueLabel.text = value;
		this.dotOutlineImage.gameObject.SetActive(true);
		Vector2 vector = Vector2.one * 0.6f;
		this.dotOutlineImage.rectTransform.localScale.Set(vector.x, vector.y, 1f);
		this.dotInsideImage.gameObject.SetActive(true);
		this.dotInsideImage.color = BreakdownListRow.statusColour[0];
		this.iconImage.gameObject.SetActive(false);
		this.checkmarkImage.gameObject.SetActive(false);
		this.SetHighlighted(false);
		this.SetImportant(false);
	}

	// Token: 0x06006274 RID: 25204 RVA: 0x00245744 File Offset: 0x00243944
	public void ShowStatusData(string name, string value, BreakdownListRow.Status dotColor)
	{
		this.ShowData(name, value);
		this.dotOutlineImage.gameObject.SetActive(true);
		this.dotInsideImage.gameObject.SetActive(true);
		this.iconImage.gameObject.SetActive(false);
		this.checkmarkImage.gameObject.SetActive(false);
		this.SetStatusColor(dotColor);
	}

	// Token: 0x06006275 RID: 25205 RVA: 0x002457A4 File Offset: 0x002439A4
	public void SetStatusColor(BreakdownListRow.Status dotColor)
	{
		this.checkmarkImage.gameObject.SetActive(dotColor > BreakdownListRow.Status.Default);
		this.checkmarkImage.color = BreakdownListRow.statusColour[(int)dotColor];
		switch (dotColor)
		{
		case BreakdownListRow.Status.Red:
			this.checkmarkImage.sprite = this.statusFailureIcon;
			return;
		case BreakdownListRow.Status.Green:
			this.checkmarkImage.sprite = this.statusSuccessIcon;
			return;
		case BreakdownListRow.Status.Yellow:
			this.checkmarkImage.sprite = this.statusWarningIcon;
			return;
		default:
			return;
		}
	}

	// Token: 0x06006276 RID: 25206 RVA: 0x00245828 File Offset: 0x00243A28
	public void ShowCheckmarkData(string name, string value, BreakdownListRow.Status status)
	{
		this.ShowData(name, value);
		this.dotOutlineImage.gameObject.SetActive(true);
		this.dotOutlineImage.rectTransform.localScale = Vector3.one;
		this.dotInsideImage.gameObject.SetActive(true);
		this.iconImage.gameObject.SetActive(false);
		this.SetStatusColor(status);
	}

	// Token: 0x06006277 RID: 25207 RVA: 0x0024588C File Offset: 0x00243A8C
	public void ShowIconData(string name, string value, Sprite sprite)
	{
		this.ShowData(name, value);
		this.dotOutlineImage.gameObject.SetActive(false);
		this.dotInsideImage.gameObject.SetActive(false);
		this.iconImage.gameObject.SetActive(true);
		this.checkmarkImage.gameObject.SetActive(false);
		this.iconImage.sprite = sprite;
		this.iconImage.color = Color.white;
	}

	// Token: 0x06006278 RID: 25208 RVA: 0x00245901 File Offset: 0x00243B01
	public void ShowIconData(string name, string value, Sprite sprite, Color spriteColor)
	{
		this.ShowIconData(name, value, sprite);
		this.iconImage.color = spriteColor;
	}

	// Token: 0x06006279 RID: 25209 RVA: 0x0024591C File Offset: 0x00243B1C
	public void SetHighlighted(bool highlighted)
	{
		this.isHighlighted = highlighted;
		Vector2 vector = Vector2.one * 0.8f;
		this.dotOutlineImage.rectTransform.localScale.Set(vector.x, vector.y, 1f);
		this.nameLabel.alpha = (this.isHighlighted ? 0.9f : 0.5f);
		this.valueLabel.alpha = (this.isHighlighted ? 0.9f : 0.5f);
	}

	// Token: 0x0600627A RID: 25210 RVA: 0x002459A8 File Offset: 0x00243BA8
	public void SetDisabled(bool disabled)
	{
		this.isDisabled = disabled;
		this.nameLabel.alpha = (this.isDisabled ? 0.4f : 0.5f);
		this.valueLabel.alpha = (this.isDisabled ? 0.4f : 0.5f);
	}

	// Token: 0x0600627B RID: 25211 RVA: 0x002459FC File Offset: 0x00243BFC
	public void SetImportant(bool important)
	{
		this.isImportant = important;
		this.dotOutlineImage.rectTransform.localScale = Vector3.one;
		this.nameLabel.alpha = (this.isImportant ? 1f : 0.5f);
		this.valueLabel.alpha = (this.isImportant ? 1f : 0.5f);
		this.nameLabel.fontStyle = (this.isImportant ? FontStyles.Bold : FontStyles.Normal);
		this.valueLabel.fontStyle = (this.isImportant ? FontStyles.Bold : FontStyles.Normal);
	}

	// Token: 0x0600627C RID: 25212 RVA: 0x00245A94 File Offset: 0x00243C94
	public void HideIcon()
	{
		this.dotOutlineImage.gameObject.SetActive(false);
		this.dotInsideImage.gameObject.SetActive(false);
		this.iconImage.gameObject.SetActive(false);
		this.checkmarkImage.gameObject.SetActive(false);
	}

	// Token: 0x0600627D RID: 25213 RVA: 0x00245AE5 File Offset: 0x00243CE5
	public void AddTooltip(string tooltipText)
	{
		if (this.tooltip == null)
		{
			this.tooltip = base.gameObject.AddComponent<ToolTip>();
		}
		this.tooltip.SetSimpleTooltip(tooltipText);
	}

	// Token: 0x0600627E RID: 25214 RVA: 0x00245B12 File Offset: 0x00243D12
	public void ClearTooltip()
	{
		if (this.tooltip != null)
		{
			this.tooltip.ClearMultiStringTooltip();
		}
	}

	// Token: 0x0600627F RID: 25215 RVA: 0x00245B2D File Offset: 0x00243D2D
	public void SetValue(string value)
	{
		this.valueLabel.text = value;
	}

	// Token: 0x04004429 RID: 17449
	private static Color[] statusColour = new Color[]
	{
		new Color(0.34117648f, 0.36862746f, 0.45882353f, 1f),
		new Color(0.72156864f, 0.38431373f, 0f, 1f),
		new Color(0.38431373f, 0.72156864f, 0f, 1f),
		new Color(0.72156864f, 0.72156864f, 0f, 1f)
	};

	// Token: 0x0400442A RID: 17450
	public Image dotOutlineImage;

	// Token: 0x0400442B RID: 17451
	public Image dotInsideImage;

	// Token: 0x0400442C RID: 17452
	public Image iconImage;

	// Token: 0x0400442D RID: 17453
	public Image checkmarkImage;

	// Token: 0x0400442E RID: 17454
	public LocText nameLabel;

	// Token: 0x0400442F RID: 17455
	public LocText valueLabel;

	// Token: 0x04004430 RID: 17456
	private bool isHighlighted;

	// Token: 0x04004431 RID: 17457
	private bool isDisabled;

	// Token: 0x04004432 RID: 17458
	private bool isImportant;

	// Token: 0x04004433 RID: 17459
	private ToolTip tooltip;

	// Token: 0x04004434 RID: 17460
	[SerializeField]
	private Sprite statusSuccessIcon;

	// Token: 0x04004435 RID: 17461
	[SerializeField]
	private Sprite statusWarningIcon;

	// Token: 0x04004436 RID: 17462
	[SerializeField]
	private Sprite statusFailureIcon;

	// Token: 0x02001AB3 RID: 6835
	public enum Status
	{
		// Token: 0x04007868 RID: 30824
		Default,
		// Token: 0x04007869 RID: 30825
		Red,
		// Token: 0x0400786A RID: 30826
		Green,
		// Token: 0x0400786B RID: 30827
		Yellow
	}
}
