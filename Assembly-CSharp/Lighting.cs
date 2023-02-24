using System;
using UnityEngine;

// Token: 0x020008C5 RID: 2245
[ExecuteInEditMode]
public class Lighting : MonoBehaviour
{
	// Token: 0x0600409B RID: 16539 RVA: 0x0016902C File Offset: 0x0016722C
	private void Awake()
	{
		Lighting.Instance = this;
	}

	// Token: 0x0600409C RID: 16540 RVA: 0x00169034 File Offset: 0x00167234
	private void OnDestroy()
	{
		Lighting.Instance = null;
	}

	// Token: 0x0600409D RID: 16541 RVA: 0x0016903C File Offset: 0x0016723C
	private Color PremultiplyAlpha(Color c)
	{
		return c * c.a;
	}

	// Token: 0x0600409E RID: 16542 RVA: 0x0016904A File Offset: 0x0016724A
	private void Start()
	{
		this.UpdateLighting();
	}

	// Token: 0x0600409F RID: 16543 RVA: 0x00169052 File Offset: 0x00167252
	private void Update()
	{
		this.UpdateLighting();
	}

	// Token: 0x060040A0 RID: 16544 RVA: 0x0016905C File Offset: 0x0016725C
	private void UpdateLighting()
	{
		Shader.SetGlobalInt(Lighting._liquidZ, -28);
		Shader.SetGlobalVector(Lighting._DigMapMapParameters, new Vector4(this.Settings.DigMapColour.r, this.Settings.DigMapColour.g, this.Settings.DigMapColour.b, this.Settings.DigMapScale));
		Shader.SetGlobalTexture(Lighting._DigDamageMap, this.Settings.DigDamageMap);
		Shader.SetGlobalTexture(Lighting._StateTransitionMap, this.Settings.StateTransitionMap);
		Shader.SetGlobalColor(Lighting._StateTransitionColor, this.Settings.StateTransitionColor);
		Shader.SetGlobalVector(Lighting._StateTransitionParameters, new Vector4(1f / this.Settings.StateTransitionUVScale, this.Settings.StateTransitionUVOffsetRate.x, this.Settings.StateTransitionUVOffsetRate.y, 0f));
		Shader.SetGlobalTexture(Lighting._FallingSolidMap, this.Settings.FallingSolidMap);
		Shader.SetGlobalColor(Lighting._FallingSolidColor, this.Settings.FallingSolidColor);
		Shader.SetGlobalVector(Lighting._FallingSolidParameters, new Vector4(1f / this.Settings.FallingSolidUVScale, this.Settings.FallingSolidUVOffsetRate.x, this.Settings.FallingSolidUVOffsetRate.y, 0f));
		Shader.SetGlobalColor(Lighting._WaterTrimColor, this.Settings.WaterTrimColor);
		Shader.SetGlobalVector(Lighting._WaterParameters2, new Vector4(this.Settings.WaterTrimSize, this.Settings.WaterAlphaTrimSize, 0f, this.Settings.WaterAlphaThreshold));
		Shader.SetGlobalVector(Lighting._WaterWaveParameters, new Vector4(this.Settings.WaterWaveAmplitude, this.Settings.WaterWaveFrequency, this.Settings.WaterWaveSpeed, 0f));
		Shader.SetGlobalVector(Lighting._WaterWaveParameters2, new Vector4(this.Settings.WaterWaveAmplitude2, this.Settings.WaterWaveFrequency2, this.Settings.WaterWaveSpeed2, 0f));
		Shader.SetGlobalVector(Lighting._WaterDetailParameters, new Vector4(this.Settings.WaterCubeMapScale, this.Settings.WaterDetailTiling, this.Settings.WaterColorScale, this.Settings.WaterDetailTiling2));
		Shader.SetGlobalVector(Lighting._WaterDistortionParameters, new Vector4(this.Settings.WaterDistortionScaleStart, this.Settings.WaterDistortionScaleEnd, this.Settings.WaterDepthColorOpacityStart, this.Settings.WaterDepthColorOpacityEnd));
		Shader.SetGlobalVector(Lighting._BloomParameters, new Vector4(this.Settings.BloomScale, 0f, 0f, 0f));
		Shader.SetGlobalVector(Lighting._LiquidParameters2, new Vector4(this.Settings.LiquidMin, this.Settings.LiquidMax, this.Settings.LiquidCutoff, this.Settings.LiquidTransparency));
		Shader.SetGlobalVector(Lighting._GridParameters, new Vector4(this.Settings.GridLineWidth, this.Settings.GridSize, this.Settings.GridMinIntensity, this.Settings.GridMaxIntensity));
		Shader.SetGlobalColor(Lighting._GridColor, this.Settings.GridColor);
		Shader.SetGlobalVector(Lighting._EdgeGlowParameters, new Vector4(this.Settings.EdgeGlowCutoffStart, this.Settings.EdgeGlowCutoffEnd, this.Settings.EdgeGlowIntensity, 0f));
		if (this.disableLighting)
		{
			Shader.SetGlobalVector(Lighting._SubstanceParameters, Vector4.one);
			Shader.SetGlobalVector(Lighting._TileEdgeParameters, Vector4.one);
		}
		else
		{
			Shader.SetGlobalVector(Lighting._SubstanceParameters, new Vector4(this.Settings.substanceEdgeParameters.intensity, this.Settings.substanceEdgeParameters.edgeIntensity, this.Settings.substanceEdgeParameters.directSunlightScale, this.Settings.substanceEdgeParameters.power));
			Shader.SetGlobalVector(Lighting._TileEdgeParameters, new Vector4(this.Settings.tileEdgeParameters.intensity, this.Settings.tileEdgeParameters.edgeIntensity, this.Settings.tileEdgeParameters.directSunlightScale, this.Settings.tileEdgeParameters.power));
		}
		float num = ((SimDebugView.Instance != null && SimDebugView.Instance.GetMode() == OverlayModes.Disease.ID) ? 1f : 0f);
		if (this.disableLighting)
		{
			Shader.SetGlobalVector(Lighting._AnimParameters, new Vector4(1f, this.Settings.WorldZoneAnimBlend, 0f, num));
		}
		else
		{
			Shader.SetGlobalVector(Lighting._AnimParameters, new Vector4(this.Settings.AnimIntensity, this.Settings.WorldZoneAnimBlend, 0f, num));
		}
		Shader.SetGlobalVector(Lighting._GasOpacity, new Vector4(this.Settings.GasMinOpacity, this.Settings.GasMaxOpacity, 0f, 0f));
		Shader.SetGlobalColor(Lighting._DarkenTintBackground, this.Settings.DarkenTints[0]);
		Shader.SetGlobalColor(Lighting._DarkenTintMidground, this.Settings.DarkenTints[1]);
		Shader.SetGlobalColor(Lighting._DarkenTintForeground, this.Settings.DarkenTints[2]);
		Shader.SetGlobalColor(Lighting._BrightenOverlay, this.Settings.BrightenOverlayColour);
		Shader.SetGlobalColor(Lighting._ColdFG, this.PremultiplyAlpha(this.Settings.ColdColours[2]));
		Shader.SetGlobalColor(Lighting._ColdMG, this.PremultiplyAlpha(this.Settings.ColdColours[1]));
		Shader.SetGlobalColor(Lighting._ColdBG, this.PremultiplyAlpha(this.Settings.ColdColours[0]));
		Shader.SetGlobalColor(Lighting._HotFG, this.PremultiplyAlpha(this.Settings.HotColours[2]));
		Shader.SetGlobalColor(Lighting._HotMG, this.PremultiplyAlpha(this.Settings.HotColours[1]));
		Shader.SetGlobalColor(Lighting._HotBG, this.PremultiplyAlpha(this.Settings.HotColours[0]));
		Shader.SetGlobalVector(Lighting._TemperatureParallax, this.Settings.TemperatureParallax);
		Shader.SetGlobalVector(Lighting._ColdUVOffset1, new Vector4(this.Settings.ColdBGUVOffset.x, this.Settings.ColdBGUVOffset.y, this.Settings.ColdMGUVOffset.x, this.Settings.ColdMGUVOffset.y));
		Shader.SetGlobalVector(Lighting._ColdUVOffset2, new Vector4(this.Settings.ColdFGUVOffset.x, this.Settings.ColdFGUVOffset.y, 0f, 0f));
		Shader.SetGlobalVector(Lighting._HotUVOffset1, new Vector4(this.Settings.HotBGUVOffset.x, this.Settings.HotBGUVOffset.y, this.Settings.HotMGUVOffset.x, this.Settings.HotMGUVOffset.y));
		Shader.SetGlobalVector(Lighting._HotUVOffset2, new Vector4(this.Settings.HotFGUVOffset.x, this.Settings.HotFGUVOffset.y, 0f, 0f));
		Shader.SetGlobalColor(Lighting._DustColour, this.PremultiplyAlpha(this.Settings.DustColour));
		Shader.SetGlobalVector(Lighting._DustInfo, new Vector4(this.Settings.DustScale, this.Settings.DustMovement.x, this.Settings.DustMovement.y, this.Settings.DustMovement.z));
		Shader.SetGlobalTexture(Lighting._DustTex, this.Settings.DustTex);
		Shader.SetGlobalVector(Lighting._DebugShowInfo, new Vector4(this.Settings.ShowDust, this.Settings.ShowGas, this.Settings.ShowShadow, this.Settings.ShowTemperature));
		Shader.SetGlobalVector(Lighting._HeatHazeParameters, this.Settings.HeatHazeParameters);
		Shader.SetGlobalTexture(Lighting._HeatHazeTexture, this.Settings.HeatHazeTexture);
		Shader.SetGlobalVector(Lighting._ShineParams, new Vector4(this.Settings.ShineCenter.x, this.Settings.ShineCenter.y, this.Settings.ShineRange.x, this.Settings.ShineRange.y));
		Shader.SetGlobalVector(Lighting._ShineParams2, new Vector4(this.Settings.ShineZoomSpeed, 0f, 0f, 0f));
		Shader.SetGlobalFloat(Lighting._WorldZoneGasBlend, this.Settings.WorldZoneGasBlend);
		Shader.SetGlobalFloat(Lighting._WorldZoneLiquidBlend, this.Settings.WorldZoneLiquidBlend);
		Shader.SetGlobalFloat(Lighting._WorldZoneForegroundBlend, this.Settings.WorldZoneForegroundBlend);
		Shader.SetGlobalFloat(Lighting._WorldZoneSimpleAnimBlend, this.Settings.WorldZoneSimpleAnimBlend);
		Shader.SetGlobalColor(Lighting._CharacterLitColour, this.Settings.characterLighting.litColour);
		Shader.SetGlobalColor(Lighting._CharacterUnlitColour, this.Settings.characterLighting.unlitColour);
		Shader.SetGlobalTexture(Lighting._BuildingDamagedTex, this.Settings.BuildingDamagedTex);
		Shader.SetGlobalVector(Lighting._BuildingDamagedUVParameters, this.Settings.BuildingDamagedUVParameters);
		Shader.SetGlobalTexture(Lighting._DiseaseOverlayTex, this.Settings.DiseaseOverlayTex);
		Shader.SetGlobalVector(Lighting._DiseaseOverlayTexInfo, this.Settings.DiseaseOverlayTexInfo);
		if (this.Settings.ShowRadiation)
		{
			Shader.SetGlobalColor(Lighting._RadHazeColor, this.PremultiplyAlpha(this.Settings.RadColor));
		}
		else
		{
			Shader.SetGlobalColor(Lighting._RadHazeColor, Color.clear);
		}
		Shader.SetGlobalVector(Lighting._RadUVOffset1, new Vector4(this.Settings.Rad1UVOffset.x, this.Settings.Rad1UVOffset.y, this.Settings.Rad2UVOffset.x, this.Settings.Rad2UVOffset.y));
		Shader.SetGlobalVector(Lighting._RadUVOffset2, new Vector4(this.Settings.Rad3UVOffset.x, this.Settings.Rad3UVOffset.y, this.Settings.Rad4UVOffset.x, this.Settings.Rad4UVOffset.y));
		Shader.SetGlobalVector(Lighting._RadUVScales, new Vector4(1f / this.Settings.RadUVScales.x, 1f / this.Settings.RadUVScales.y, 1f / this.Settings.RadUVScales.z, 1f / this.Settings.RadUVScales.w));
		Shader.SetGlobalVector(Lighting._RadRange1, new Vector4(this.Settings.Rad1Range.x, this.Settings.Rad1Range.y, this.Settings.Rad2Range.x, this.Settings.Rad2Range.y));
		Shader.SetGlobalVector(Lighting._RadRange2, new Vector4(this.Settings.Rad3Range.x, this.Settings.Rad3Range.y, this.Settings.Rad4Range.x, this.Settings.Rad4Range.y));
		if (LightBuffer.Instance != null && LightBuffer.Instance.Texture != null)
		{
			Shader.SetGlobalTexture(Lighting._LightBufferTex, LightBuffer.Instance.Texture);
		}
	}

	// Token: 0x04002A57 RID: 10839
	public global::LightingSettings Settings;

	// Token: 0x04002A58 RID: 10840
	public static Lighting Instance;

	// Token: 0x04002A59 RID: 10841
	[NonSerialized]
	public bool disableLighting;

	// Token: 0x04002A5A RID: 10842
	private static int _liquidZ = Shader.PropertyToID("_LiquidZ");

	// Token: 0x04002A5B RID: 10843
	private static int _DigMapMapParameters = Shader.PropertyToID("_DigMapMapParameters");

	// Token: 0x04002A5C RID: 10844
	private static int _DigDamageMap = Shader.PropertyToID("_DigDamageMap");

	// Token: 0x04002A5D RID: 10845
	private static int _StateTransitionMap = Shader.PropertyToID("_StateTransitionMap");

	// Token: 0x04002A5E RID: 10846
	private static int _StateTransitionColor = Shader.PropertyToID("_StateTransitionColor");

	// Token: 0x04002A5F RID: 10847
	private static int _StateTransitionParameters = Shader.PropertyToID("_StateTransitionParameters");

	// Token: 0x04002A60 RID: 10848
	private static int _FallingSolidMap = Shader.PropertyToID("_FallingSolidMap");

	// Token: 0x04002A61 RID: 10849
	private static int _FallingSolidColor = Shader.PropertyToID("_FallingSolidColor");

	// Token: 0x04002A62 RID: 10850
	private static int _FallingSolidParameters = Shader.PropertyToID("_FallingSolidParameters");

	// Token: 0x04002A63 RID: 10851
	private static int _WaterTrimColor = Shader.PropertyToID("_WaterTrimColor");

	// Token: 0x04002A64 RID: 10852
	private static int _WaterParameters2 = Shader.PropertyToID("_WaterParameters2");

	// Token: 0x04002A65 RID: 10853
	private static int _WaterWaveParameters = Shader.PropertyToID("_WaterWaveParameters");

	// Token: 0x04002A66 RID: 10854
	private static int _WaterWaveParameters2 = Shader.PropertyToID("_WaterWaveParameters2");

	// Token: 0x04002A67 RID: 10855
	private static int _WaterDetailParameters = Shader.PropertyToID("_WaterDetailParameters");

	// Token: 0x04002A68 RID: 10856
	private static int _WaterDistortionParameters = Shader.PropertyToID("_WaterDistortionParameters");

	// Token: 0x04002A69 RID: 10857
	private static int _BloomParameters = Shader.PropertyToID("_BloomParameters");

	// Token: 0x04002A6A RID: 10858
	private static int _LiquidParameters2 = Shader.PropertyToID("_LiquidParameters2");

	// Token: 0x04002A6B RID: 10859
	private static int _GridParameters = Shader.PropertyToID("_GridParameters");

	// Token: 0x04002A6C RID: 10860
	private static int _GridColor = Shader.PropertyToID("_GridColor");

	// Token: 0x04002A6D RID: 10861
	private static int _EdgeGlowParameters = Shader.PropertyToID("_EdgeGlowParameters");

	// Token: 0x04002A6E RID: 10862
	private static int _SubstanceParameters = Shader.PropertyToID("_SubstanceParameters");

	// Token: 0x04002A6F RID: 10863
	private static int _TileEdgeParameters = Shader.PropertyToID("_TileEdgeParameters");

	// Token: 0x04002A70 RID: 10864
	private static int _AnimParameters = Shader.PropertyToID("_AnimParameters");

	// Token: 0x04002A71 RID: 10865
	private static int _GasOpacity = Shader.PropertyToID("_GasOpacity");

	// Token: 0x04002A72 RID: 10866
	private static int _DarkenTintBackground = Shader.PropertyToID("_DarkenTintBackground");

	// Token: 0x04002A73 RID: 10867
	private static int _DarkenTintMidground = Shader.PropertyToID("_DarkenTintMidground");

	// Token: 0x04002A74 RID: 10868
	private static int _DarkenTintForeground = Shader.PropertyToID("_DarkenTintForeground");

	// Token: 0x04002A75 RID: 10869
	private static int _BrightenOverlay = Shader.PropertyToID("_BrightenOverlay");

	// Token: 0x04002A76 RID: 10870
	private static int _ColdFG = Shader.PropertyToID("_ColdFG");

	// Token: 0x04002A77 RID: 10871
	private static int _ColdMG = Shader.PropertyToID("_ColdMG");

	// Token: 0x04002A78 RID: 10872
	private static int _ColdBG = Shader.PropertyToID("_ColdBG");

	// Token: 0x04002A79 RID: 10873
	private static int _HotFG = Shader.PropertyToID("_HotFG");

	// Token: 0x04002A7A RID: 10874
	private static int _HotMG = Shader.PropertyToID("_HotMG");

	// Token: 0x04002A7B RID: 10875
	private static int _HotBG = Shader.PropertyToID("_HotBG");

	// Token: 0x04002A7C RID: 10876
	private static int _TemperatureParallax = Shader.PropertyToID("_TemperatureParallax");

	// Token: 0x04002A7D RID: 10877
	private static int _ColdUVOffset1 = Shader.PropertyToID("_ColdUVOffset1");

	// Token: 0x04002A7E RID: 10878
	private static int _ColdUVOffset2 = Shader.PropertyToID("_ColdUVOffset2");

	// Token: 0x04002A7F RID: 10879
	private static int _HotUVOffset1 = Shader.PropertyToID("_HotUVOffset1");

	// Token: 0x04002A80 RID: 10880
	private static int _HotUVOffset2 = Shader.PropertyToID("_HotUVOffset2");

	// Token: 0x04002A81 RID: 10881
	private static int _DustColour = Shader.PropertyToID("_DustColour");

	// Token: 0x04002A82 RID: 10882
	private static int _DustInfo = Shader.PropertyToID("_DustInfo");

	// Token: 0x04002A83 RID: 10883
	private static int _DustTex = Shader.PropertyToID("_DustTex");

	// Token: 0x04002A84 RID: 10884
	private static int _DebugShowInfo = Shader.PropertyToID("_DebugShowInfo");

	// Token: 0x04002A85 RID: 10885
	private static int _HeatHazeParameters = Shader.PropertyToID("_HeatHazeParameters");

	// Token: 0x04002A86 RID: 10886
	private static int _HeatHazeTexture = Shader.PropertyToID("_HeatHazeTexture");

	// Token: 0x04002A87 RID: 10887
	private static int _ShineParams = Shader.PropertyToID("_ShineParams");

	// Token: 0x04002A88 RID: 10888
	private static int _ShineParams2 = Shader.PropertyToID("_ShineParams2");

	// Token: 0x04002A89 RID: 10889
	private static int _WorldZoneGasBlend = Shader.PropertyToID("_WorldZoneGasBlend");

	// Token: 0x04002A8A RID: 10890
	private static int _WorldZoneLiquidBlend = Shader.PropertyToID("_WorldZoneLiquidBlend");

	// Token: 0x04002A8B RID: 10891
	private static int _WorldZoneForegroundBlend = Shader.PropertyToID("_WorldZoneForegroundBlend");

	// Token: 0x04002A8C RID: 10892
	private static int _WorldZoneSimpleAnimBlend = Shader.PropertyToID("_WorldZoneSimpleAnimBlend");

	// Token: 0x04002A8D RID: 10893
	private static int _CharacterLitColour = Shader.PropertyToID("_CharacterLitColour");

	// Token: 0x04002A8E RID: 10894
	private static int _CharacterUnlitColour = Shader.PropertyToID("_CharacterUnlitColour");

	// Token: 0x04002A8F RID: 10895
	private static int _BuildingDamagedTex = Shader.PropertyToID("_BuildingDamagedTex");

	// Token: 0x04002A90 RID: 10896
	private static int _BuildingDamagedUVParameters = Shader.PropertyToID("_BuildingDamagedUVParameters");

	// Token: 0x04002A91 RID: 10897
	private static int _DiseaseOverlayTex = Shader.PropertyToID("_DiseaseOverlayTex");

	// Token: 0x04002A92 RID: 10898
	private static int _DiseaseOverlayTexInfo = Shader.PropertyToID("_DiseaseOverlayTexInfo");

	// Token: 0x04002A93 RID: 10899
	private static int _RadHazeColor = Shader.PropertyToID("_RadHazeColor");

	// Token: 0x04002A94 RID: 10900
	private static int _RadUVOffset1 = Shader.PropertyToID("_RadUVOffset1");

	// Token: 0x04002A95 RID: 10901
	private static int _RadUVOffset2 = Shader.PropertyToID("_RadUVOffset2");

	// Token: 0x04002A96 RID: 10902
	private static int _RadUVScales = Shader.PropertyToID("_RadUVScales");

	// Token: 0x04002A97 RID: 10903
	private static int _RadRange1 = Shader.PropertyToID("_RadRange1");

	// Token: 0x04002A98 RID: 10904
	private static int _RadRange2 = Shader.PropertyToID("_RadRange2");

	// Token: 0x04002A99 RID: 10905
	private static int _LightBufferTex = Shader.PropertyToID("_LightBufferTex");
}
