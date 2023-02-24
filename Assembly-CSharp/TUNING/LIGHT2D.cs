using System;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000D2D RID: 3373
	public class LIGHT2D
	{
		// Token: 0x04004D38 RID: 19768
		public const int SUNLIGHT_MAX_DEFAULT = 80000;

		// Token: 0x04004D39 RID: 19769
		public static readonly Color LIGHT_BLUE = new Color(0.38f, 0.61f, 1f, 1f);

		// Token: 0x04004D3A RID: 19770
		public static readonly Color LIGHT_PURPLE = new Color(0.9f, 0.4f, 0.74f, 1f);

		// Token: 0x04004D3B RID: 19771
		public static readonly Color LIGHT_YELLOW = new Color(0.57f, 0.55f, 0.44f, 1f);

		// Token: 0x04004D3C RID: 19772
		public static readonly Color LIGHT_OVERLAY = new Color(0.56f, 0.56f, 0.56f, 1f);

		// Token: 0x04004D3D RID: 19773
		public static readonly Vector2 DEFAULT_DIRECTION = new Vector2(0f, -1f);

		// Token: 0x04004D3E RID: 19774
		public const int FLOORLAMP_LUX = 1000;

		// Token: 0x04004D3F RID: 19775
		public const float FLOORLAMP_RANGE = 4f;

		// Token: 0x04004D40 RID: 19776
		public const float FLOORLAMP_ANGLE = 0f;

		// Token: 0x04004D41 RID: 19777
		public const global::LightShape FLOORLAMP_SHAPE = global::LightShape.Circle;

		// Token: 0x04004D42 RID: 19778
		public static readonly Color FLOORLAMP_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D43 RID: 19779
		public static readonly Color FLOORLAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D44 RID: 19780
		public static readonly Vector2 FLOORLAMP_OFFSET = new Vector2(0.05f, 1.5f);

		// Token: 0x04004D45 RID: 19781
		public static readonly Vector2 FLOORLAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04004D46 RID: 19782
		public const float CEILINGLIGHT_RANGE = 8f;

		// Token: 0x04004D47 RID: 19783
		public const float CEILINGLIGHT_ANGLE = 2.6f;

		// Token: 0x04004D48 RID: 19784
		public const global::LightShape CEILINGLIGHT_SHAPE = global::LightShape.Cone;

		// Token: 0x04004D49 RID: 19785
		public static readonly Color CEILINGLIGHT_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D4A RID: 19786
		public static readonly Color CEILINGLIGHT_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D4B RID: 19787
		public static readonly Vector2 CEILINGLIGHT_OFFSET = new Vector2(0.05f, 0.65f);

		// Token: 0x04004D4C RID: 19788
		public static readonly Vector2 CEILINGLIGHT_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04004D4D RID: 19789
		public const int CEILINGLIGHT_LUX = 1800;

		// Token: 0x04004D4E RID: 19790
		public const int SUNLAMP_LUX = 40000;

		// Token: 0x04004D4F RID: 19791
		public const float SUNLAMP_RANGE = 16f;

		// Token: 0x04004D50 RID: 19792
		public const float SUNLAMP_ANGLE = 5.2f;

		// Token: 0x04004D51 RID: 19793
		public const global::LightShape SUNLAMP_SHAPE = global::LightShape.Cone;

		// Token: 0x04004D52 RID: 19794
		public static readonly Color SUNLAMP_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D53 RID: 19795
		public static readonly Color SUNLAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D54 RID: 19796
		public static readonly Vector2 SUNLAMP_OFFSET = new Vector2(0f, 3.5f);

		// Token: 0x04004D55 RID: 19797
		public static readonly Vector2 SUNLAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04004D56 RID: 19798
		public static readonly Color LIGHT_PREVIEW_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D57 RID: 19799
		public const float HEADQUARTERS_RANGE = 5f;

		// Token: 0x04004D58 RID: 19800
		public const global::LightShape HEADQUARTERS_SHAPE = global::LightShape.Circle;

		// Token: 0x04004D59 RID: 19801
		public static readonly Color HEADQUARTERS_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D5A RID: 19802
		public static readonly Color HEADQUARTERS_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D5B RID: 19803
		public static readonly Vector2 HEADQUARTERS_OFFSET = new Vector2(0.5f, 3f);

		// Token: 0x04004D5C RID: 19804
		public static readonly Vector2 EXOBASE_HEADQUARTERS_OFFSET = new Vector2(0f, 2.5f);

		// Token: 0x04004D5D RID: 19805
		public const float ENGINE_RANGE = 10f;

		// Token: 0x04004D5E RID: 19806
		public const global::LightShape ENGINE_SHAPE = global::LightShape.Circle;

		// Token: 0x04004D5F RID: 19807
		public const int ENGINE_LUX = 80000;

		// Token: 0x04004D60 RID: 19808
		public const float WALLLIGHT_RANGE = 4f;

		// Token: 0x04004D61 RID: 19809
		public const float WALLLIGHT_ANGLE = 0f;

		// Token: 0x04004D62 RID: 19810
		public const global::LightShape WALLLIGHT_SHAPE = global::LightShape.Circle;

		// Token: 0x04004D63 RID: 19811
		public static readonly Color WALLLIGHT_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D64 RID: 19812
		public static readonly Color WALLLIGHT_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D65 RID: 19813
		public static readonly Vector2 WALLLIGHT_OFFSET = new Vector2(0f, 0.5f);

		// Token: 0x04004D66 RID: 19814
		public static readonly Vector2 WALLLIGHT_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04004D67 RID: 19815
		public const float LIGHTBUG_RANGE = 5f;

		// Token: 0x04004D68 RID: 19816
		public const float LIGHTBUG_ANGLE = 0f;

		// Token: 0x04004D69 RID: 19817
		public const global::LightShape LIGHTBUG_SHAPE = global::LightShape.Circle;

		// Token: 0x04004D6A RID: 19818
		public const int LIGHTBUG_LUX = 1800;

		// Token: 0x04004D6B RID: 19819
		public static readonly Color LIGHTBUG_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D6C RID: 19820
		public static readonly Color LIGHTBUG_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D6D RID: 19821
		public static readonly Color LIGHTBUG_COLOR_ORANGE = new Color(0.5686275f, 0.48235294f, 0.4392157f, 1f);

		// Token: 0x04004D6E RID: 19822
		public static readonly Color LIGHTBUG_COLOR_PURPLE = new Color(0.49019608f, 0.4392157f, 0.5686275f, 1f);

		// Token: 0x04004D6F RID: 19823
		public static readonly Color LIGHTBUG_COLOR_PINK = new Color(0.5686275f, 0.4392157f, 0.5686275f, 1f);

		// Token: 0x04004D70 RID: 19824
		public static readonly Color LIGHTBUG_COLOR_BLUE = new Color(0.4392157f, 0.4862745f, 0.5686275f, 1f);

		// Token: 0x04004D71 RID: 19825
		public static readonly Color LIGHTBUG_COLOR_CRYSTAL = new Color(0.5137255f, 0.6666667f, 0.6666667f, 1f);

		// Token: 0x04004D72 RID: 19826
		public static readonly Color LIGHTBUG_COLOR_GREEN = new Color(0.43137255f, 1f, 0.53333336f, 1f);

		// Token: 0x04004D73 RID: 19827
		public static readonly Vector2 LIGHTBUG_OFFSET = new Vector2(0.05f, 0.25f);

		// Token: 0x04004D74 RID: 19828
		public static readonly Vector2 LIGHTBUG_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04004D75 RID: 19829
		public const int PLASMALAMP_LUX = 666;

		// Token: 0x04004D76 RID: 19830
		public const float PLASMALAMP_RANGE = 2f;

		// Token: 0x04004D77 RID: 19831
		public const float PLASMALAMP_ANGLE = 0f;

		// Token: 0x04004D78 RID: 19832
		public const global::LightShape PLASMALAMP_SHAPE = global::LightShape.Circle;

		// Token: 0x04004D79 RID: 19833
		public static readonly Color PLASMALAMP_COLOR = LIGHT2D.LIGHT_PURPLE;

		// Token: 0x04004D7A RID: 19834
		public static readonly Color PLASMALAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D7B RID: 19835
		public static readonly Vector2 PLASMALAMP_OFFSET = new Vector2(0.05f, 0.5f);

		// Token: 0x04004D7C RID: 19836
		public static readonly Vector2 PLASMALAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04004D7D RID: 19837
		public const int MAGMALAMP_LUX = 666;

		// Token: 0x04004D7E RID: 19838
		public const float MAGMALAMP_RANGE = 2f;

		// Token: 0x04004D7F RID: 19839
		public const float MAGMALAMP_ANGLE = 0f;

		// Token: 0x04004D80 RID: 19840
		public const global::LightShape MAGMALAMP_SHAPE = global::LightShape.Cone;

		// Token: 0x04004D81 RID: 19841
		public static readonly Color MAGMALAMP_COLOR = LIGHT2D.LIGHT_YELLOW;

		// Token: 0x04004D82 RID: 19842
		public static readonly Color MAGMALAMP_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D83 RID: 19843
		public static readonly Vector2 MAGMALAMP_OFFSET = new Vector2(0.05f, 0.33f);

		// Token: 0x04004D84 RID: 19844
		public static readonly Vector2 MAGMALAMP_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;

		// Token: 0x04004D85 RID: 19845
		public const int BIOLUMROCK_LUX = 666;

		// Token: 0x04004D86 RID: 19846
		public const float BIOLUMROCK_RANGE = 2f;

		// Token: 0x04004D87 RID: 19847
		public const float BIOLUMROCK_ANGLE = 0f;

		// Token: 0x04004D88 RID: 19848
		public const global::LightShape BIOLUMROCK_SHAPE = global::LightShape.Cone;

		// Token: 0x04004D89 RID: 19849
		public static readonly Color BIOLUMROCK_COLOR = LIGHT2D.LIGHT_BLUE;

		// Token: 0x04004D8A RID: 19850
		public static readonly Color BIOLUMROCK_OVERLAYCOLOR = LIGHT2D.LIGHT_OVERLAY;

		// Token: 0x04004D8B RID: 19851
		public static readonly Vector2 BIOLUMROCK_OFFSET = new Vector2(0.05f, 0.33f);

		// Token: 0x04004D8C RID: 19852
		public static readonly Vector2 BIOLUMROCK_DIRECTION = LIGHT2D.DEFAULT_DIRECTION;
	}
}
