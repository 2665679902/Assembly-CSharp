using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006C RID: 108
public class KTreeItem : MonoBehaviour
{
	// Token: 0x14000022 RID: 34
	// (add) Token: 0x06000471 RID: 1137 RVA: 0x00016540 File Offset: 0x00014740
	// (remove) Token: 0x06000472 RID: 1138 RVA: 0x00016578 File Offset: 0x00014778
	public event KTreeItem.StateChanged onOpenChanged;

	// Token: 0x14000023 RID: 35
	// (add) Token: 0x06000473 RID: 1139 RVA: 0x000165B0 File Offset: 0x000147B0
	// (remove) Token: 0x06000474 RID: 1140 RVA: 0x000165E8 File Offset: 0x000147E8
	public event KTreeItem.StateChanged onCheckChanged;

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001661D File Offset: 0x0001481D
	// (set) Token: 0x06000476 RID: 1142 RVA: 0x0001662A File Offset: 0x0001482A
	public string text
	{
		get
		{
			return this.label.text;
		}
		set
		{
			base.name = value;
			this.label.text = value;
		}
	}

	// Token: 0x17000098 RID: 152
	// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001663F File Offset: 0x0001483F
	// (set) Token: 0x06000478 RID: 1144 RVA: 0x00016651 File Offset: 0x00014851
	public bool checkboxEnabled
	{
		get
		{
			return this.checkbox.gameObject.activeSelf;
		}
		set
		{
			this.checkbox.gameObject.SetActive(value);
		}
	}

	// Token: 0x17000099 RID: 153
	// (get) Token: 0x06000479 RID: 1145 RVA: 0x00016664 File Offset: 0x00014864
	// (set) Token: 0x0600047A RID: 1146 RVA: 0x00016671 File Offset: 0x00014871
	public bool checkboxChecked
	{
		get
		{
			return this.checkbox.isOn;
		}
		set
		{
			this.checkbox.isOn = value;
		}
	}

	// Token: 0x1700009A RID: 154
	// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001667F File Offset: 0x0001487F
	// (set) Token: 0x0600047C RID: 1148 RVA: 0x00016687 File Offset: 0x00014887
	public bool opened
	{
		get
		{
			return this.childrenVisible;
		}
		set
		{
			this.childrenVisible = value;
			this.UpdateOpened();
		}
	}

	// Token: 0x1700009B RID: 155
	// (get) Token: 0x0600047D RID: 1149 RVA: 0x00016696 File Offset: 0x00014896
	public IList<KTreeItem> children
	{
		get
		{
			return this.childItems;
		}
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x0001669E File Offset: 0x0001489E
	private void Awake()
	{
		this.UpdateOpened();
		this.SetImageAlpha(0f);
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x000166B4 File Offset: 0x000148B4
	private void SetImageAlpha(float a)
	{
		Color color = this.openedImage.color;
		color.a = a;
		this.openedImage.color = color;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x000166E1 File Offset: 0x000148E1
	public void AddChild(KTreeItem child)
	{
		this.childItems.Add(child);
		child.transform.SetParent(this.childrenRoot.transform, false);
		child.parent = this;
		this.SetImageAlpha(1f);
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00016718 File Offset: 0x00014918
	public void RemoveChild(KTreeItem child)
	{
		this.childItems.Remove(child);
		if (this.childItems.Count == 0)
		{
			this.SetImageAlpha(0f);
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x0001673F File Offset: 0x0001493F
	public void ToggleOpened()
	{
		this.opened = !this.opened;
		this.UpdateOpened();
		if (this.onOpenChanged != null)
		{
			this.onOpenChanged(this, this.opened);
		}
	}

	// Token: 0x06000483 RID: 1155 RVA: 0x00016770 File Offset: 0x00014970
	public void ToggleChecked()
	{
		if (this.onCheckChanged != null)
		{
			this.onCheckChanged(this, this.checkboxChecked);
		}
	}

	// Token: 0x06000484 RID: 1156 RVA: 0x0001678C File Offset: 0x0001498C
	private void UpdateOpened()
	{
		this.openedImage.sprite = (this.opened ? this.spriteOpen : this.spriteClosed);
		this.childrenRoot.SetActive(this.opened);
	}

	// Token: 0x040004BB RID: 1211
	[SerializeField]
	private bool childrenVisible;

	// Token: 0x040004BC RID: 1212
	[SerializeField]
	private bool checkVisible;

	// Token: 0x040004BD RID: 1213
	[SerializeField]
	private bool isChecked;

	// Token: 0x040004BE RID: 1214
	[SerializeField]
	private Sprite spriteOpen;

	// Token: 0x040004BF RID: 1215
	[SerializeField]
	private Sprite spriteClosed;

	// Token: 0x040004C0 RID: 1216
	[SerializeField]
	private Image openedImage;

	// Token: 0x040004C1 RID: 1217
	[SerializeField]
	private Text label;

	// Token: 0x040004C2 RID: 1218
	[SerializeField]
	private Toggle checkbox;

	// Token: 0x040004C3 RID: 1219
	[SerializeField]
	private GameObject childrenRoot;

	// Token: 0x040004C4 RID: 1220
	[NonSerialized]
	public object userData;

	// Token: 0x040004C5 RID: 1221
	private List<KTreeItem> childItems = new List<KTreeItem>();

	// Token: 0x040004C8 RID: 1224
	[NonSerialized]
	public KTreeItem parent;

	// Token: 0x020009BF RID: 2495
	// (Invoke) Token: 0x0600536D RID: 21357
	public delegate void StateChanged(KTreeItem item, bool value);
}
