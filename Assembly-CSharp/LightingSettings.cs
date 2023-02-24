using System;
using UnityEngine;

// Token: 0x020008C7 RID: 2247
public class LightingSettings : ScriptableObject
{
	// Token: 0x04002AA1 RID: 10913
	[Header("Global")]
	public bool UpdateLightSettings;

	// Token: 0x04002AA2 RID: 10914
	public float BloomScale;

	// Token: 0x04002AA3 RID: 10915
	public Color32 LightColour = Color.white;

	// Token: 0x04002AA4 RID: 10916
	[Header("Digging")]
	public float DigMapScale;

	// Token: 0x04002AA5 RID: 10917
	public Color DigMapColour;

	// Token: 0x04002AA6 RID: 10918
	public Texture2D DigDamageMap;

	// Token: 0x04002AA7 RID: 10919
	[Header("State Transition")]
	public Texture2D StateTransitionMap;

	// Token: 0x04002AA8 RID: 10920
	public Color StateTransitionColor;

	// Token: 0x04002AA9 RID: 10921
	public float StateTransitionUVScale;

	// Token: 0x04002AAA RID: 10922
	public Vector2 StateTransitionUVOffsetRate;

	// Token: 0x04002AAB RID: 10923
	[Header("Falling Solids")]
	public Texture2D FallingSolidMap;

	// Token: 0x04002AAC RID: 10924
	public Color FallingSolidColor;

	// Token: 0x04002AAD RID: 10925
	public float FallingSolidUVScale;

	// Token: 0x04002AAE RID: 10926
	public Vector2 FallingSolidUVOffsetRate;

	// Token: 0x04002AAF RID: 10927
	[Header("Metal Shine")]
	public Vector2 ShineCenter;

	// Token: 0x04002AB0 RID: 10928
	public Vector2 ShineRange;

	// Token: 0x04002AB1 RID: 10929
	public float ShineZoomSpeed;

	// Token: 0x04002AB2 RID: 10930
	[Header("Water")]
	public Color WaterTrimColor;

	// Token: 0x04002AB3 RID: 10931
	public float WaterTrimSize;

	// Token: 0x04002AB4 RID: 10932
	public float WaterAlphaTrimSize;

	// Token: 0x04002AB5 RID: 10933
	public float WaterAlphaThreshold;

	// Token: 0x04002AB6 RID: 10934
	public float WaterCubesAlphaThreshold;

	// Token: 0x04002AB7 RID: 10935
	public float WaterWaveAmplitude;

	// Token: 0x04002AB8 RID: 10936
	public float WaterWaveFrequency;

	// Token: 0x04002AB9 RID: 10937
	public float WaterWaveSpeed;

	// Token: 0x04002ABA RID: 10938
	public float WaterDetailSpeed;

	// Token: 0x04002ABB RID: 10939
	public float WaterDetailTiling;

	// Token: 0x04002ABC RID: 10940
	public float WaterDetailTiling2;

	// Token: 0x04002ABD RID: 10941
	public Vector2 WaterDetailDirection;

	// Token: 0x04002ABE RID: 10942
	public float WaterWaveAmplitude2;

	// Token: 0x04002ABF RID: 10943
	public float WaterWaveFrequency2;

	// Token: 0x04002AC0 RID: 10944
	public float WaterWaveSpeed2;

	// Token: 0x04002AC1 RID: 10945
	public float WaterCubeMapScale;

	// Token: 0x04002AC2 RID: 10946
	public float WaterColorScale;

	// Token: 0x04002AC3 RID: 10947
	public float WaterDistortionScaleStart;

	// Token: 0x04002AC4 RID: 10948
	public float WaterDistortionScaleEnd;

	// Token: 0x04002AC5 RID: 10949
	public float WaterDepthColorOpacityStart;

	// Token: 0x04002AC6 RID: 10950
	public float WaterDepthColorOpacityEnd;

	// Token: 0x04002AC7 RID: 10951
	[Header("Liquid")]
	public float LiquidMin;

	// Token: 0x04002AC8 RID: 10952
	public float LiquidMax;

	// Token: 0x04002AC9 RID: 10953
	public float LiquidCutoff;

	// Token: 0x04002ACA RID: 10954
	public float LiquidTransparency;

	// Token: 0x04002ACB RID: 10955
	public float LiquidAmountOffset;

	// Token: 0x04002ACC RID: 10956
	public float LiquidMaxMass;

	// Token: 0x04002ACD RID: 10957
	[Header("Grid")]
	public float GridLineWidth;

	// Token: 0x04002ACE RID: 10958
	public float GridSize;

	// Token: 0x04002ACF RID: 10959
	public float GridMaxIntensity;

	// Token: 0x04002AD0 RID: 10960
	public float GridMinIntensity;

	// Token: 0x04002AD1 RID: 10961
	public Color GridColor;

	// Token: 0x04002AD2 RID: 10962
	[Header("Terrain")]
	public float EdgeGlowCutoffStart;

	// Token: 0x04002AD3 RID: 10963
	public float EdgeGlowCutoffEnd;

	// Token: 0x04002AD4 RID: 10964
	public float EdgeGlowIntensity;

	// Token: 0x04002AD5 RID: 10965
	public int BackgroundLayers;

	// Token: 0x04002AD6 RID: 10966
	public float BackgroundBaseParallax;

	// Token: 0x04002AD7 RID: 10967
	public float BackgroundLayerParallax;

	// Token: 0x04002AD8 RID: 10968
	public float BackgroundDarkening;

	// Token: 0x04002AD9 RID: 10969
	public float BackgroundClip;

	// Token: 0x04002ADA RID: 10970
	public float BackgroundUVScale;

	// Token: 0x04002ADB RID: 10971
	public global::LightingSettings.EdgeLighting substanceEdgeParameters;

	// Token: 0x04002ADC RID: 10972
	public global::LightingSettings.EdgeLighting tileEdgeParameters;

	// Token: 0x04002ADD RID: 10973
	public float AnimIntensity;

	// Token: 0x04002ADE RID: 10974
	public float GasMinOpacity;

	// Token: 0x04002ADF RID: 10975
	public float GasMaxOpacity;

	// Token: 0x04002AE0 RID: 10976
	public Color[] DarkenTints;

	// Token: 0x04002AE1 RID: 10977
	public global::LightingSettings.LightingColours characterLighting;

	// Token: 0x04002AE2 RID: 10978
	public Color BrightenOverlayColour;

	// Token: 0x04002AE3 RID: 10979
	public Color[] ColdColours;

	// Token: 0x04002AE4 RID: 10980
	public Color[] HotColours;

	// Token: 0x04002AE5 RID: 10981
	[Header("Temperature Overlay Effects")]
	public Vector4 TemperatureParallax;

	// Token: 0x04002AE6 RID: 10982
	public Texture2D EmberTex;

	// Token: 0x04002AE7 RID: 10983
	public Texture2D FrostTex;

	// Token: 0x04002AE8 RID: 10984
	public Texture2D Thermal1Tex;

	// Token: 0x04002AE9 RID: 10985
	public Texture2D Thermal2Tex;

	// Token: 0x04002AEA RID: 10986
	public Vector2 ColdFGUVOffset;

	// Token: 0x04002AEB RID: 10987
	public Vector2 ColdMGUVOffset;

	// Token: 0x04002AEC RID: 10988
	public Vector2 ColdBGUVOffset;

	// Token: 0x04002AED RID: 10989
	public Vector2 HotFGUVOffset;

	// Token: 0x04002AEE RID: 10990
	public Vector2 HotMGUVOffset;

	// Token: 0x04002AEF RID: 10991
	public Vector2 HotBGUVOffset;

	// Token: 0x04002AF0 RID: 10992
	public Texture2D DustTex;

	// Token: 0x04002AF1 RID: 10993
	public Color DustColour;

	// Token: 0x04002AF2 RID: 10994
	public float DustScale;

	// Token: 0x04002AF3 RID: 10995
	public Vector3 DustMovement;

	// Token: 0x04002AF4 RID: 10996
	public float ShowGas;

	// Token: 0x04002AF5 RID: 10997
	public float ShowTemperature;

	// Token: 0x04002AF6 RID: 10998
	public float ShowDust;

	// Token: 0x04002AF7 RID: 10999
	public float ShowShadow;

	// Token: 0x04002AF8 RID: 11000
	public Vector4 HeatHazeParameters;

	// Token: 0x04002AF9 RID: 11001
	public Texture2D HeatHazeTexture;

	// Token: 0x04002AFA RID: 11002
	[Header("Biome")]
	public float WorldZoneGasBlend;

	// Token: 0x04002AFB RID: 11003
	public float WorldZoneLiquidBlend;

	// Token: 0x04002AFC RID: 11004
	public float WorldZoneForegroundBlend;

	// Token: 0x04002AFD RID: 11005
	public float WorldZoneSimpleAnimBlend;

	// Token: 0x04002AFE RID: 11006
	public float WorldZoneAnimBlend;

	// Token: 0x04002AFF RID: 11007
	[Header("FX")]
	public Color32 SmokeDamageTint;

	// Token: 0x04002B00 RID: 11008
	[Header("Building Damage")]
	public Texture2D BuildingDamagedTex;

	// Token: 0x04002B01 RID: 11009
	public Vector4 BuildingDamagedUVParameters;

	// Token: 0x04002B02 RID: 11010
	[Header("Disease")]
	public Texture2D DiseaseOverlayTex;

	// Token: 0x04002B03 RID: 11011
	public Vector4 DiseaseOverlayTexInfo;

	// Token: 0x04002B04 RID: 11012
	[Header("Conduits")]
	public ConduitFlowVisualizer.Tuning GasConduit;

	// Token: 0x04002B05 RID: 11013
	public ConduitFlowVisualizer.Tuning LiquidConduit;

	// Token: 0x04002B06 RID: 11014
	public SolidConduitFlowVisualizer.Tuning SolidConduit;

	// Token: 0x04002B07 RID: 11015
	[Header("Radiation Overlay")]
	public bool ShowRadiation;

	// Token: 0x04002B08 RID: 11016
	public Texture2D Radiation1Tex;

	// Token: 0x04002B09 RID: 11017
	public Texture2D Radiation2Tex;

	// Token: 0x04002B0A RID: 11018
	public Texture2D Radiation3Tex;

	// Token: 0x04002B0B RID: 11019
	public Texture2D Radiation4Tex;

	// Token: 0x04002B0C RID: 11020
	public Texture2D NoiseTex;

	// Token: 0x04002B0D RID: 11021
	public Color RadColor;

	// Token: 0x04002B0E RID: 11022
	public Vector2 Rad1UVOffset;

	// Token: 0x04002B0F RID: 11023
	public Vector2 Rad2UVOffset;

	// Token: 0x04002B10 RID: 11024
	public Vector2 Rad3UVOffset;

	// Token: 0x04002B11 RID: 11025
	public Vector2 Rad4UVOffset;

	// Token: 0x04002B12 RID: 11026
	public Vector4 RadUVScales;

	// Token: 0x04002B13 RID: 11027
	public Vector2 Rad1Range;

	// Token: 0x04002B14 RID: 11028
	public Vector2 Rad2Range;

	// Token: 0x04002B15 RID: 11029
	public Vector2 Rad3Range;

	// Token: 0x04002B16 RID: 11030
	public Vector2 Rad4Range;

	// Token: 0x02001691 RID: 5777
	[Serializable]
	public struct EdgeLighting
	{
		// Token: 0x04006A30 RID: 27184
		public float intensity;

		// Token: 0x04006A31 RID: 27185
		public float edgeIntensity;

		// Token: 0x04006A32 RID: 27186
		public float directSunlightScale;

		// Token: 0x04006A33 RID: 27187
		public float power;
	}

	// Token: 0x02001692 RID: 5778
	public enum TintLayers
	{
		// Token: 0x04006A35 RID: 27189
		Background,
		// Token: 0x04006A36 RID: 27190
		Midground,
		// Token: 0x04006A37 RID: 27191
		Foreground,
		// Token: 0x04006A38 RID: 27192
		NumLayers
	}

	// Token: 0x02001693 RID: 5779
	[Serializable]
	public struct LightingColours
	{
		// Token: 0x04006A39 RID: 27193
		public Color32 litColour;

		// Token: 0x04006A3A RID: 27194
		public Color32 unlitColour;
	}
}
