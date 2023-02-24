using System;
using STRINGS;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000D1B RID: 3355
	public class OVERLAY
	{
		// Token: 0x02001B6A RID: 7018
		public class TEMPERATURE_LEGEND
		{
			// Token: 0x04007BB8 RID: 31672
			public static readonly LegendEntry MAXHOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.MAXHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x04007BB9 RID: 31673
			public static readonly LegendEntry EXTREMEHOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMEHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x04007BBA RID: 31674
			public static readonly LegendEntry VERYHOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYHOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(1f, 0f, 0f), null, null, true);

			// Token: 0x04007BBB RID: 31675
			public static readonly LegendEntry HOT = new LegendEntry(UI.OVERLAYS.TEMPERATURE.HOT, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 1f, 0f), null, null, true);

			// Token: 0x04007BBC RID: 31676
			public static readonly LegendEntry TEMPERATE = new LegendEntry(UI.OVERLAYS.TEMPERATURE.TEMPERATE, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x04007BBD RID: 31677
			public static readonly LegendEntry COLD = new LegendEntry(UI.OVERLAYS.TEMPERATURE.COLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 1f), null, null, true);

			// Token: 0x04007BBE RID: 31678
			public static readonly LegendEntry VERYCOLD = new LegendEntry(UI.OVERLAYS.TEMPERATURE.VERYCOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 1f), null, null, true);

			// Token: 0x04007BBF RID: 31679
			public static readonly LegendEntry EXTREMECOLD = new LegendEntry(UI.OVERLAYS.TEMPERATURE.EXTREMECOLD, UI.OVERLAYS.TEMPERATURE.TOOLTIPS.TEMPERATURE, new Color(0f, 0f, 0f), null, null, true);
		}

		// Token: 0x02001B6B RID: 7019
		public class HEATFLOW_LEGEND
		{
			// Token: 0x04007BC0 RID: 31680
			public static readonly LegendEntry HEATING = new LegendEntry(UI.OVERLAYS.HEATFLOW.HEATING, UI.OVERLAYS.HEATFLOW.TOOLTIPS.HEATING, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x04007BC1 RID: 31681
			public static readonly LegendEntry NEUTRAL = new LegendEntry(UI.OVERLAYS.HEATFLOW.NEUTRAL, UI.OVERLAYS.HEATFLOW.TOOLTIPS.NEUTRAL, new Color(0f, 0f, 0f), null, null, true);

			// Token: 0x04007BC2 RID: 31682
			public static readonly LegendEntry COOLING = new LegendEntry(UI.OVERLAYS.HEATFLOW.COOLING, UI.OVERLAYS.HEATFLOW.TOOLTIPS.COOLING, new Color(0f, 0f, 0f), null, null, true);
		}

		// Token: 0x02001B6C RID: 7020
		public class POWER_LEGEND
		{
			// Token: 0x04007BC3 RID: 31683
			public const float WATTAGE_WARNING_THRESHOLD = 0.75f;
		}
	}
}
