using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000BCB RID: 3019
public class PixelPackSideScreen : SideScreenContent
{
	// Token: 0x06005EE9 RID: 24297 RVA: 0x0022AEE8 File Offset: 0x002290E8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (this.swatch_object_by_color.Count == 0)
		{
			this.InitializeColorSwatch();
		}
		this.PopulateColorSelections();
		this.copyActiveToStandbyButton.onClick += this.CopyActiveToStandby;
		this.copyStandbyToActiveButton.onClick += this.CopyStandbyToActive;
		this.swapColorsButton.onClick += this.SwapColors;
	}

	// Token: 0x06005EEA RID: 24298 RVA: 0x0022AF59 File Offset: 0x00229159
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<PixelPack>() != null;
	}

	// Token: 0x06005EEB RID: 24299 RVA: 0x0022AF67 File Offset: 0x00229167
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		this.targetPixelPack = target.GetComponent<PixelPack>();
		this.PopulateColorSelections();
		this.HighlightUsedColors();
	}

	// Token: 0x06005EEC RID: 24300 RVA: 0x0022AF88 File Offset: 0x00229188
	private void HighlightUsedColors()
	{
		if (this.swatch_object_by_color.Count == 0)
		{
			this.InitializeColorSwatch();
		}
		for (int i = 0; i < this.highlightedSwatchGameObjects.Count; i++)
		{
			this.highlightedSwatchGameObjects[i].GetComponent<HierarchyReferences>().GetReference("used").GetComponentInChildren<Image>()
				.gameObject.SetActive(false);
		}
		this.highlightedSwatchGameObjects.Clear();
		for (int j = 0; j < this.targetPixelPack.colorSettings.Count; j++)
		{
			this.swatch_object_by_color[this.targetPixelPack.colorSettings[j].activeColor].GetComponent<HierarchyReferences>().GetReference("used").gameObject.SetActive(true);
			this.swatch_object_by_color[this.targetPixelPack.colorSettings[j].standbyColor].GetComponent<HierarchyReferences>().GetReference("used").gameObject.SetActive(true);
			this.highlightedSwatchGameObjects.Add(this.swatch_object_by_color[this.targetPixelPack.colorSettings[j].activeColor]);
			this.highlightedSwatchGameObjects.Add(this.swatch_object_by_color[this.targetPixelPack.colorSettings[j].standbyColor]);
		}
	}

	// Token: 0x06005EED RID: 24301 RVA: 0x0022B0E4 File Offset: 0x002292E4
	private void PopulateColorSelections()
	{
		for (int i = 0; i < this.targetPixelPack.colorSettings.Count; i++)
		{
			int current_id = i;
			this.activeColors[i].GetComponent<Image>().color = this.targetPixelPack.colorSettings[i].activeColor;
			this.activeColors[i].GetComponent<KButton>().onClick += delegate
			{
				PixelPack.ColorPair colorPair = this.targetPixelPack.colorSettings[current_id];
				this.activeColors[current_id].GetComponent<Image>().color = this.paintingColor;
				colorPair.activeColor = this.paintingColor;
				this.targetPixelPack.colorSettings[current_id] = colorPair;
				this.targetPixelPack.UpdateColors();
				this.HighlightUsedColors();
			};
			this.standbyColors[i].GetComponent<Image>().color = this.targetPixelPack.colorSettings[i].standbyColor;
			this.standbyColors[i].GetComponent<KButton>().onClick += delegate
			{
				PixelPack.ColorPair colorPair2 = this.targetPixelPack.colorSettings[current_id];
				this.standbyColors[current_id].GetComponent<Image>().color = this.paintingColor;
				colorPair2.standbyColor = this.paintingColor;
				this.targetPixelPack.colorSettings[current_id] = colorPair2;
				this.targetPixelPack.UpdateColors();
				this.HighlightUsedColors();
			};
		}
	}

	// Token: 0x06005EEE RID: 24302 RVA: 0x0022B1C4 File Offset: 0x002293C4
	private void InitializeColorSwatch()
	{
		bool flag = false;
		for (int i = 0; i < this.colorSwatch.Count; i++)
		{
			GameObject swatch = Util.KInstantiateUI(this.swatchEntry, this.colorSwatchContainer, true);
			Image component = swatch.GetComponent<Image>();
			component.color = this.colorSwatch[i];
			KButton component2 = swatch.GetComponent<KButton>();
			Color color = this.colorSwatch[i];
			if (component.color == Color.black)
			{
				swatch.GetComponent<HierarchyReferences>().GetReference("selected").GetComponentInChildren<Image>()
					.color = Color.white;
			}
			if (!flag)
			{
				this.SelectColor(color, swatch);
				flag = true;
			}
			component2.onClick += delegate
			{
				this.SelectColor(color, swatch);
			};
			this.swatch_object_by_color[color] = swatch;
		}
	}

	// Token: 0x06005EEF RID: 24303 RVA: 0x0022B2C4 File Offset: 0x002294C4
	private void SelectColor(Color color, GameObject swatchEntry)
	{
		this.paintingColor = color;
		swatchEntry.GetComponent<HierarchyReferences>().GetReference("selected").gameObject.SetActive(true);
		if (this.selectedSwatchEntry != null && this.selectedSwatchEntry != swatchEntry)
		{
			this.selectedSwatchEntry.GetComponent<HierarchyReferences>().GetReference("selected").gameObject.SetActive(false);
		}
		this.selectedSwatchEntry = swatchEntry;
	}

	// Token: 0x06005EF0 RID: 24304 RVA: 0x0022B338 File Offset: 0x00229538
	private void CopyActiveToStandby()
	{
		for (int i = 0; i < this.targetPixelPack.colorSettings.Count; i++)
		{
			PixelPack.ColorPair colorPair = this.targetPixelPack.colorSettings[i];
			colorPair.standbyColor = colorPair.activeColor;
			this.targetPixelPack.colorSettings[i] = colorPair;
			this.standbyColors[i].GetComponent<Image>().color = colorPair.activeColor;
		}
		this.HighlightUsedColors();
		this.targetPixelPack.UpdateColors();
	}

	// Token: 0x06005EF1 RID: 24305 RVA: 0x0022B3C0 File Offset: 0x002295C0
	private void CopyStandbyToActive()
	{
		for (int i = 0; i < this.targetPixelPack.colorSettings.Count; i++)
		{
			PixelPack.ColorPair colorPair = this.targetPixelPack.colorSettings[i];
			colorPair.activeColor = colorPair.standbyColor;
			this.targetPixelPack.colorSettings[i] = colorPair;
			this.activeColors[i].GetComponent<Image>().color = colorPair.standbyColor;
		}
		this.HighlightUsedColors();
		this.targetPixelPack.UpdateColors();
	}

	// Token: 0x06005EF2 RID: 24306 RVA: 0x0022B448 File Offset: 0x00229648
	private void SwapColors()
	{
		for (int i = 0; i < this.targetPixelPack.colorSettings.Count; i++)
		{
			PixelPack.ColorPair colorPair = default(PixelPack.ColorPair);
			colorPair.activeColor = this.targetPixelPack.colorSettings[i].standbyColor;
			colorPair.standbyColor = this.targetPixelPack.colorSettings[i].activeColor;
			this.targetPixelPack.colorSettings[i] = colorPair;
			this.activeColors[i].GetComponent<Image>().color = colorPair.activeColor;
			this.standbyColors[i].GetComponent<Image>().color = colorPair.standbyColor;
		}
		this.HighlightUsedColors();
		this.targetPixelPack.UpdateColors();
	}

	// Token: 0x040040ED RID: 16621
	public PixelPack targetPixelPack;

	// Token: 0x040040EE RID: 16622
	public KButton copyActiveToStandbyButton;

	// Token: 0x040040EF RID: 16623
	public KButton copyStandbyToActiveButton;

	// Token: 0x040040F0 RID: 16624
	public KButton swapColorsButton;

	// Token: 0x040040F1 RID: 16625
	public GameObject colorSwatchContainer;

	// Token: 0x040040F2 RID: 16626
	public GameObject swatchEntry;

	// Token: 0x040040F3 RID: 16627
	public GameObject activeColorsContainer;

	// Token: 0x040040F4 RID: 16628
	public GameObject standbyColorsContainer;

	// Token: 0x040040F5 RID: 16629
	public List<GameObject> activeColors = new List<GameObject>();

	// Token: 0x040040F6 RID: 16630
	public List<GameObject> standbyColors = new List<GameObject>();

	// Token: 0x040040F7 RID: 16631
	public Color paintingColor;

	// Token: 0x040040F8 RID: 16632
	public GameObject selectedSwatchEntry;

	// Token: 0x040040F9 RID: 16633
	private Dictionary<Color, GameObject> swatch_object_by_color = new Dictionary<Color, GameObject>();

	// Token: 0x040040FA RID: 16634
	private List<GameObject> highlightedSwatchGameObjects = new List<GameObject>();

	// Token: 0x040040FB RID: 16635
	private List<Color> colorSwatch = new List<Color>
	{
		new Color(0.4862745f, 0.4862745f, 0.4862745f),
		new Color(0f, 0f, 0.9882353f),
		new Color(0f, 0f, 0.7372549f),
		new Color(0.26666668f, 0.15686275f, 0.7372549f),
		new Color(0.5803922f, 0f, 0.5176471f),
		new Color(0.65882355f, 0f, 0.1254902f),
		new Color(0.65882355f, 0.0627451f, 0f),
		new Color(0.53333336f, 0.078431375f, 0f),
		new Color(0.3137255f, 0.1882353f, 0f),
		new Color(0f, 0.47058824f, 0f),
		new Color(0f, 0.40784314f, 0f),
		new Color(0f, 0.34509805f, 0f),
		new Color(0f, 0.2509804f, 0.34509805f),
		new Color(0f, 0f, 0f),
		new Color(0.7372549f, 0.7372549f, 0.7372549f),
		new Color(0f, 0.47058824f, 0.972549f),
		new Color(0f, 0.34509805f, 0.972549f),
		new Color(0.40784314f, 0.26666668f, 0.9882353f),
		new Color(0.84705883f, 0f, 0.8f),
		new Color(0.89411765f, 0f, 0.34509805f),
		new Color(0.972549f, 0.21960784f, 0f),
		new Color(0.89411765f, 0.36078432f, 0.0627451f),
		new Color(0.6745098f, 0.4862745f, 0f),
		new Color(0f, 0.72156864f, 0f),
		new Color(0f, 0.65882355f, 0f),
		new Color(0f, 0.65882355f, 0.26666668f),
		new Color(0f, 0.53333336f, 0.53333336f),
		new Color(0f, 0f, 0f),
		new Color(0.972549f, 0.972549f, 0.972549f),
		new Color(0.23529412f, 0.7372549f, 0.9882353f),
		new Color(0.40784314f, 0.53333336f, 0.9882353f),
		new Color(0.59607846f, 0.47058824f, 0.972549f),
		new Color(0.972549f, 0.47058824f, 0.972549f),
		new Color(0.972549f, 0.34509805f, 0.59607846f),
		new Color(0.972549f, 0.47058824f, 0.34509805f),
		new Color(0.9882353f, 0.627451f, 0.26666668f),
		new Color(0.972549f, 0.72156864f, 0f),
		new Color(0.72156864f, 0.972549f, 0.09411765f),
		new Color(0.34509805f, 0.84705883f, 0.32941177f),
		new Color(0.34509805f, 0.972549f, 0.59607846f),
		new Color(0f, 0.9098039f, 0.84705883f),
		new Color(0.47058824f, 0.47058824f, 0.47058824f),
		new Color(0.9882353f, 0.9882353f, 0.9882353f),
		new Color(0.6431373f, 0.89411765f, 0.9882353f),
		new Color(0.72156864f, 0.72156864f, 0.972549f),
		new Color(0.84705883f, 0.72156864f, 0.972549f),
		new Color(0.972549f, 0.72156864f, 0.972549f),
		new Color(0.972549f, 0.72156864f, 0.7529412f),
		new Color(0.9411765f, 0.8156863f, 0.6901961f),
		new Color(0.9882353f, 0.8784314f, 0.65882355f),
		new Color(0.972549f, 0.84705883f, 0.47058824f),
		new Color(0.84705883f, 0.972549f, 0.47058824f),
		new Color(0.72156864f, 0.972549f, 0.72156864f),
		new Color(0.72156864f, 0.972549f, 0.84705883f),
		new Color(0f, 0.9882353f, 0.9882353f),
		new Color(0.84705883f, 0.84705883f, 0.84705883f)
	};
}
