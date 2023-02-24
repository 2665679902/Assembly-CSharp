using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A65 RID: 2661
public class CodexImage : CodexWidget<CodexImage>
{
	// Token: 0x1700060A RID: 1546
	// (get) Token: 0x06005174 RID: 20852 RVA: 0x001D6FC4 File Offset: 0x001D51C4
	// (set) Token: 0x06005175 RID: 20853 RVA: 0x001D6FCC File Offset: 0x001D51CC
	public Sprite sprite { get; set; }

	// Token: 0x1700060B RID: 1547
	// (get) Token: 0x06005176 RID: 20854 RVA: 0x001D6FD5 File Offset: 0x001D51D5
	// (set) Token: 0x06005177 RID: 20855 RVA: 0x001D6FDD File Offset: 0x001D51DD
	public Color color { get; set; }

	// Token: 0x1700060C RID: 1548
	// (get) Token: 0x06005179 RID: 20857 RVA: 0x001D6FF9 File Offset: 0x001D51F9
	// (set) Token: 0x06005178 RID: 20856 RVA: 0x001D6FE6 File Offset: 0x001D51E6
	public string spriteName
	{
		get
		{
			return "--> " + ((this.sprite == null) ? "NULL" : this.sprite.ToString());
		}
		set
		{
			this.sprite = Assets.GetSprite(value);
		}
	}

	// Token: 0x1700060D RID: 1549
	// (get) Token: 0x0600517B RID: 20859 RVA: 0x001D708C File Offset: 0x001D528C
	// (set) Token: 0x0600517A RID: 20858 RVA: 0x001D7028 File Offset: 0x001D5228
	public string batchedAnimPrefabSourceID
	{
		get
		{
			return "--> " + ((this.sprite == null) ? "NULL" : this.sprite.ToString());
		}
		set
		{
			GameObject prefab = Assets.GetPrefab(value);
			KBatchedAnimController kbatchedAnimController = ((prefab != null) ? prefab.GetComponent<KBatchedAnimController>() : null);
			KAnimFile kanimFile = ((kbatchedAnimController != null) ? kbatchedAnimController.AnimFiles[0] : null);
			this.sprite = ((kanimFile != null) ? Def.GetUISpriteFromMultiObjectAnim(kanimFile, "ui", false, "") : null);
		}
	}

	// Token: 0x0600517C RID: 20860 RVA: 0x001D70B8 File Offset: 0x001D52B8
	public CodexImage()
	{
		this.color = Color.white;
	}

	// Token: 0x0600517D RID: 20861 RVA: 0x001D70CB File Offset: 0x001D52CB
	public CodexImage(int preferredWidth, int preferredHeight, Sprite sprite, Color color)
		: base(preferredWidth, preferredHeight)
	{
		this.sprite = sprite;
		this.color = color;
	}

	// Token: 0x0600517E RID: 20862 RVA: 0x001D70E4 File Offset: 0x001D52E4
	public CodexImage(int preferredWidth, int preferredHeight, Sprite sprite)
		: this(preferredWidth, preferredHeight, sprite, Color.white)
	{
	}

	// Token: 0x0600517F RID: 20863 RVA: 0x001D70F4 File Offset: 0x001D52F4
	public CodexImage(int preferredWidth, int preferredHeight, global::Tuple<Sprite, Color> coloredSprite)
		: this(preferredWidth, preferredHeight, coloredSprite.first, coloredSprite.second)
	{
	}

	// Token: 0x06005180 RID: 20864 RVA: 0x001D710A File Offset: 0x001D530A
	public CodexImage(global::Tuple<Sprite, Color> coloredSprite)
		: this(-1, -1, coloredSprite)
	{
	}

	// Token: 0x06005181 RID: 20865 RVA: 0x001D7115 File Offset: 0x001D5315
	public void ConfigureImage(Image image)
	{
		image.sprite = this.sprite;
		image.color = this.color;
	}

	// Token: 0x06005182 RID: 20866 RVA: 0x001D712F File Offset: 0x001D532F
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureImage(contentGameObject.GetComponent<Image>());
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
