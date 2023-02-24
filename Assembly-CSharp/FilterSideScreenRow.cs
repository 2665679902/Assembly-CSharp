using System;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BB3 RID: 2995
[AddComponentMenu("KMonoBehaviour/scripts/FilterSideScreenRow")]
public class FilterSideScreenRow : KMonoBehaviour
{
	// Token: 0x17000690 RID: 1680
	// (get) Token: 0x06005E27 RID: 24103 RVA: 0x002268B0 File Offset: 0x00224AB0
	// (set) Token: 0x06005E28 RID: 24104 RVA: 0x002268B8 File Offset: 0x00224AB8
	public new Tag tag { get; private set; }

	// Token: 0x17000691 RID: 1681
	// (get) Token: 0x06005E29 RID: 24105 RVA: 0x002268C1 File Offset: 0x00224AC1
	// (set) Token: 0x06005E2A RID: 24106 RVA: 0x002268C9 File Offset: 0x00224AC9
	public bool isSelected { get; private set; }

	// Token: 0x06005E2B RID: 24107 RVA: 0x002268D4 File Offset: 0x00224AD4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.regularColor = this.outline.color;
		if (this.button != null)
		{
			this.button.onPointerEnter += delegate
			{
				if (!this.isSelected)
				{
					this.outline.color = this.outlineHighLightColor;
				}
			};
			this.button.onPointerExit += delegate
			{
				if (!this.isSelected)
				{
					this.outline.color = this.regularColor;
				}
			};
		}
	}

	// Token: 0x06005E2C RID: 24108 RVA: 0x00226934 File Offset: 0x00224B34
	public void SetTag(Tag tag)
	{
		this.tag = tag;
		this.SetText((tag == GameTags.Void) ? UI.UISIDESCREENS.FILTERSIDESCREEN.NO_SELECTION.text : tag.ProperName());
	}

	// Token: 0x06005E2D RID: 24109 RVA: 0x00226962 File Offset: 0x00224B62
	private void SetText(string assignmentStr)
	{
		this.labelText.text = ((!string.IsNullOrEmpty(assignmentStr)) ? assignmentStr : "-");
	}

	// Token: 0x06005E2E RID: 24110 RVA: 0x0022697F File Offset: 0x00224B7F
	public void SetSelected(bool selected)
	{
		this.isSelected = selected;
		this.outline.color = (selected ? this.outlineHighLightColor : this.outlineDefaultColor);
		this.BG.color = (selected ? this.BGHighLightColor : Color.white);
	}

	// Token: 0x04004065 RID: 16485
	[SerializeField]
	private LocText labelText;

	// Token: 0x04004066 RID: 16486
	[SerializeField]
	private Image BG;

	// Token: 0x04004067 RID: 16487
	[SerializeField]
	private Image outline;

	// Token: 0x04004068 RID: 16488
	[SerializeField]
	private Color outlineHighLightColor = new Color32(168, 74, 121, byte.MaxValue);

	// Token: 0x04004069 RID: 16489
	[SerializeField]
	private Color BGHighLightColor = new Color32(168, 74, 121, 80);

	// Token: 0x0400406A RID: 16490
	[SerializeField]
	private Color outlineDefaultColor = new Color32(204, 204, 204, byte.MaxValue);

	// Token: 0x0400406B RID: 16491
	private Color regularColor = Color.white;

	// Token: 0x0400406C RID: 16492
	[SerializeField]
	public KButton button;
}
