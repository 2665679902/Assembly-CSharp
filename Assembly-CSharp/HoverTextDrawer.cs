using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A04 RID: 2564
public class HoverTextDrawer
{
	// Token: 0x06004CF5 RID: 19701 RVA: 0x001B1380 File Offset: 0x001AF580
	public HoverTextDrawer(HoverTextDrawer.Skin skin, RectTransform parent)
	{
		this.shadowBars = new HoverTextDrawer.Pool<Image>(skin.shadowBarWidget.gameObject, parent);
		this.selectBorders = new HoverTextDrawer.Pool<Image>(skin.selectBorderWidget.gameObject, parent);
		this.textWidgets = new HoverTextDrawer.Pool<LocText>(skin.textWidget.gameObject, parent);
		this.iconWidgets = new HoverTextDrawer.Pool<Image>(skin.iconWidget.gameObject, parent);
		this.skin = skin;
	}

	// Token: 0x06004CF6 RID: 19702 RVA: 0x001B13F6 File Offset: 0x001AF5F6
	public void SetEnabled(bool enabled)
	{
		this.shadowBars.SetEnabled(enabled);
		this.textWidgets.SetEnabled(enabled);
		this.iconWidgets.SetEnabled(enabled);
		this.selectBorders.SetEnabled(enabled);
	}

	// Token: 0x06004CF7 RID: 19703 RVA: 0x001B1428 File Offset: 0x001AF628
	public void BeginDrawing(Vector2 root_pos)
	{
		this.rootPos = root_pos + this.skin.baseOffset;
		if (this.skin.enableDebugOffset)
		{
			this.rootPos += this.skin.debugOffset;
		}
		this.currentPos = this.rootPos;
		this.textWidgets.BeginDrawing();
		this.iconWidgets.BeginDrawing();
		this.shadowBars.BeginDrawing();
		this.selectBorders.BeginDrawing();
		this.firstShadowBar = true;
		this.minLineHeight = 0;
	}

	// Token: 0x06004CF8 RID: 19704 RVA: 0x001B14BB File Offset: 0x001AF6BB
	public void EndDrawing()
	{
		this.shadowBars.EndDrawing();
		this.iconWidgets.EndDrawing();
		this.textWidgets.EndDrawing();
		this.selectBorders.EndDrawing();
	}

	// Token: 0x06004CF9 RID: 19705 RVA: 0x001B14EC File Offset: 0x001AF6EC
	public void DrawText(string text, TextStyleSetting style, Color color, bool override_color = true)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		LocText widget = this.textWidgets.Draw(this.currentPos).widget;
		Color color2 = Color.white;
		if (widget.textStyleSetting != style)
		{
			widget.textStyleSetting = style;
			widget.ApplySettings();
		}
		if (style != null)
		{
			color2 = style.textColor;
		}
		if (override_color)
		{
			color2 = color;
		}
		widget.color = color2;
		if (widget.text != text)
		{
			widget.text = text;
			widget.KForceUpdateDirty();
		}
		this.currentPos.x = this.currentPos.x + widget.renderedWidth;
		this.maxShadowX = Mathf.Max(this.currentPos.x, this.maxShadowX);
		this.minLineHeight = (int)Mathf.Max((float)this.minLineHeight, widget.renderedHeight);
	}

	// Token: 0x06004CFA RID: 19706 RVA: 0x001B15C1 File Offset: 0x001AF7C1
	public void DrawText(string text, TextStyleSetting style)
	{
		this.DrawText(text, style, Color.white, false);
	}

	// Token: 0x06004CFB RID: 19707 RVA: 0x001B15D1 File Offset: 0x001AF7D1
	public void AddIndent(int width = 36)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.currentPos.x = this.currentPos.x + (float)width;
	}

	// Token: 0x06004CFC RID: 19708 RVA: 0x001B15F4 File Offset: 0x001AF7F4
	public void NewLine(int min_height = 26)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.currentPos.y = this.currentPos.y - (float)Math.Max(min_height, this.minLineHeight);
		this.currentPos.x = this.rootPos.x;
		this.minLineHeight = 0;
	}

	// Token: 0x06004CFD RID: 19709 RVA: 0x001B1648 File Offset: 0x001AF848
	public void DrawIcon(Sprite icon, int min_width = 18)
	{
		this.DrawIcon(icon, Color.white, min_width, 2);
	}

	// Token: 0x06004CFE RID: 19710 RVA: 0x001B1658 File Offset: 0x001AF858
	public void DrawIcon(Sprite icon, Color color, int image_size = 18, int horizontal_spacing = 2)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.AddIndent(horizontal_spacing);
		HoverTextDrawer.Pool<Image>.Entry entry = this.iconWidgets.Draw(this.currentPos + this.skin.shadowImageOffset);
		entry.widget.sprite = icon;
		entry.widget.color = this.skin.shadowImageColor;
		entry.rect.sizeDelta = new Vector2((float)image_size, (float)image_size);
		HoverTextDrawer.Pool<Image>.Entry entry2 = this.iconWidgets.Draw(this.currentPos);
		entry2.widget.sprite = icon;
		entry2.widget.color = color;
		entry2.rect.sizeDelta = new Vector2((float)image_size, (float)image_size);
		this.AddIndent(horizontal_spacing);
		this.currentPos.x = this.currentPos.x + (float)image_size;
		this.maxShadowX = Mathf.Max(this.currentPos.x, this.maxShadowX);
	}

	// Token: 0x06004CFF RID: 19711 RVA: 0x001B1744 File Offset: 0x001AF944
	public void BeginShadowBar(bool selected = false)
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		if (this.firstShadowBar)
		{
			this.firstShadowBar = false;
		}
		else
		{
			this.NewLine(22);
		}
		this.isShadowBarSelected = selected;
		this.shadowStartPos = this.currentPos;
		this.maxShadowX = this.rootPos.x;
	}

	// Token: 0x06004D00 RID: 19712 RVA: 0x001B179C File Offset: 0x001AF99C
	public void EndShadowBar()
	{
		if (!this.skin.drawWidgets)
		{
			return;
		}
		this.NewLine(22);
		HoverTextDrawer.Pool<Image>.Entry entry = this.shadowBars.Draw(this.currentPos);
		entry.rect.anchoredPosition = this.shadowStartPos + new Vector2(-this.skin.shadowBarBorder.x, this.skin.shadowBarBorder.y);
		entry.rect.sizeDelta = new Vector2(this.maxShadowX - this.rootPos.x + this.skin.shadowBarBorder.x * 2f, this.shadowStartPos.y - this.currentPos.y + this.skin.shadowBarBorder.y * 2f);
		if (this.isShadowBarSelected)
		{
			HoverTextDrawer.Pool<Image>.Entry entry2 = this.selectBorders.Draw(this.currentPos);
			entry2.rect.anchoredPosition = this.shadowStartPos + new Vector2(-this.skin.shadowBarBorder.x - this.skin.selectBorder.x, this.skin.shadowBarBorder.y + this.skin.selectBorder.y);
			entry2.rect.sizeDelta = new Vector2(this.maxShadowX - this.rootPos.x + this.skin.shadowBarBorder.x * 2f + this.skin.selectBorder.x * 2f, this.shadowStartPos.y - this.currentPos.y + this.skin.shadowBarBorder.y * 2f + this.skin.selectBorder.y * 2f);
		}
	}

	// Token: 0x06004D01 RID: 19713 RVA: 0x001B1980 File Offset: 0x001AFB80
	public void Cleanup()
	{
		this.shadowBars.Cleanup();
		this.textWidgets.Cleanup();
		this.iconWidgets.Cleanup();
	}

	// Token: 0x040032BC RID: 12988
	public HoverTextDrawer.Skin skin;

	// Token: 0x040032BD RID: 12989
	private Vector2 currentPos;

	// Token: 0x040032BE RID: 12990
	private Vector2 rootPos;

	// Token: 0x040032BF RID: 12991
	private Vector2 shadowStartPos;

	// Token: 0x040032C0 RID: 12992
	private float maxShadowX;

	// Token: 0x040032C1 RID: 12993
	private bool firstShadowBar;

	// Token: 0x040032C2 RID: 12994
	private bool isShadowBarSelected;

	// Token: 0x040032C3 RID: 12995
	private int minLineHeight;

	// Token: 0x040032C4 RID: 12996
	private HoverTextDrawer.Pool<LocText> textWidgets;

	// Token: 0x040032C5 RID: 12997
	private HoverTextDrawer.Pool<Image> iconWidgets;

	// Token: 0x040032C6 RID: 12998
	private HoverTextDrawer.Pool<Image> shadowBars;

	// Token: 0x040032C7 RID: 12999
	private HoverTextDrawer.Pool<Image> selectBorders;

	// Token: 0x0200180F RID: 6159
	[Serializable]
	public class Skin
	{
		// Token: 0x04006EE1 RID: 28385
		public Vector2 baseOffset;

		// Token: 0x04006EE2 RID: 28386
		public LocText textWidget;

		// Token: 0x04006EE3 RID: 28387
		public Image iconWidget;

		// Token: 0x04006EE4 RID: 28388
		public Vector2 shadowImageOffset;

		// Token: 0x04006EE5 RID: 28389
		public Color shadowImageColor;

		// Token: 0x04006EE6 RID: 28390
		public Image shadowBarWidget;

		// Token: 0x04006EE7 RID: 28391
		public Image selectBorderWidget;

		// Token: 0x04006EE8 RID: 28392
		public Vector2 shadowBarBorder;

		// Token: 0x04006EE9 RID: 28393
		public Vector2 selectBorder;

		// Token: 0x04006EEA RID: 28394
		public bool drawWidgets;

		// Token: 0x04006EEB RID: 28395
		public bool enableProfiling;

		// Token: 0x04006EEC RID: 28396
		public bool enableDebugOffset;

		// Token: 0x04006EED RID: 28397
		public bool drawInProgressHoverText;

		// Token: 0x04006EEE RID: 28398
		public Vector2 debugOffset;
	}

	// Token: 0x02001810 RID: 6160
	private class Pool<WidgetType> where WidgetType : MonoBehaviour
	{
		// Token: 0x06008CB7 RID: 36023 RVA: 0x00303960 File Offset: 0x00301B60
		public Pool(GameObject prefab, RectTransform master_root)
		{
			this.prefab = prefab;
			GameObject gameObject = new GameObject(typeof(WidgetType).Name);
			this.root = gameObject.AddComponent<RectTransform>();
			this.root.SetParent(master_root);
			this.root.anchoredPosition = Vector2.zero;
			this.root.anchorMin = Vector2.zero;
			this.root.anchorMax = Vector2.one;
			this.root.sizeDelta = Vector2.zero;
			gameObject.AddComponent<CanvasGroup>();
		}

		// Token: 0x06008CB8 RID: 36024 RVA: 0x003039FC File Offset: 0x00301BFC
		public HoverTextDrawer.Pool<WidgetType>.Entry Draw(Vector2 pos)
		{
			HoverTextDrawer.Pool<WidgetType>.Entry entry;
			if (this.drawnWidgets < this.entries.Count)
			{
				entry = this.entries[this.drawnWidgets];
				if (!entry.widget.gameObject.activeSelf)
				{
					entry.widget.gameObject.SetActive(true);
				}
			}
			else
			{
				GameObject gameObject = Util.KInstantiateUI(this.prefab, this.root.gameObject, false);
				gameObject.SetActive(true);
				entry.widget = gameObject.GetComponent<WidgetType>();
				entry.rect = gameObject.GetComponent<RectTransform>();
				this.entries.Add(entry);
			}
			entry.rect.anchoredPosition = new Vector2(pos.x, pos.y);
			this.drawnWidgets++;
			return entry;
		}

		// Token: 0x06008CB9 RID: 36025 RVA: 0x00303ACD File Offset: 0x00301CCD
		public void BeginDrawing()
		{
			this.drawnWidgets = 0;
		}

		// Token: 0x06008CBA RID: 36026 RVA: 0x00303AD8 File Offset: 0x00301CD8
		public void EndDrawing()
		{
			for (int i = this.drawnWidgets; i < this.entries.Count; i++)
			{
				if (this.entries[i].widget.gameObject.activeSelf)
				{
					this.entries[i].widget.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x06008CBB RID: 36027 RVA: 0x00303B43 File Offset: 0x00301D43
		public void SetEnabled(bool enabled)
		{
			if (enabled)
			{
				this.root.gameObject.GetComponent<CanvasGroup>().alpha = 1f;
				return;
			}
			this.root.gameObject.GetComponent<CanvasGroup>().alpha = 0f;
		}

		// Token: 0x06008CBC RID: 36028 RVA: 0x00303B80 File Offset: 0x00301D80
		public void Cleanup()
		{
			foreach (HoverTextDrawer.Pool<WidgetType>.Entry entry in this.entries)
			{
				UnityEngine.Object.Destroy(entry.widget.gameObject);
			}
			this.entries.Clear();
		}

		// Token: 0x04006EEF RID: 28399
		private GameObject prefab;

		// Token: 0x04006EF0 RID: 28400
		private RectTransform root;

		// Token: 0x04006EF1 RID: 28401
		private List<HoverTextDrawer.Pool<WidgetType>.Entry> entries = new List<HoverTextDrawer.Pool<WidgetType>.Entry>();

		// Token: 0x04006EF2 RID: 28402
		private int drawnWidgets;

		// Token: 0x020020E1 RID: 8417
		public struct Entry
		{
			// Token: 0x04009278 RID: 37496
			public WidgetType widget;

			// Token: 0x04009279 RID: 37497
			public RectTransform rect;
		}
	}
}
