using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// Token: 0x02000A76 RID: 2678
public class ColorSet : ScriptableObject
{
	// Token: 0x06005205 RID: 20997 RVA: 0x001DA2B0 File Offset: 0x001D84B0
	private void Init()
	{
		if (this.namedLookup == null)
		{
			this.namedLookup = new Dictionary<string, Color32>();
			foreach (FieldInfo fieldInfo in typeof(ColorSet).GetFields())
			{
				if (fieldInfo.FieldType == typeof(Color32))
				{
					this.namedLookup[fieldInfo.Name] = (Color32)fieldInfo.GetValue(this);
				}
			}
		}
	}

	// Token: 0x06005206 RID: 20998 RVA: 0x001DA326 File Offset: 0x001D8526
	public Color32 GetColorByName(string name)
	{
		this.Init();
		return this.namedLookup[name];
	}

	// Token: 0x06005207 RID: 20999 RVA: 0x001DA33A File Offset: 0x001D853A
	public void RefreshLookup()
	{
		this.namedLookup = null;
		this.Init();
	}

	// Token: 0x06005208 RID: 21000 RVA: 0x001DA349 File Offset: 0x001D8549
	public bool IsDefaultColorSet()
	{
		return Array.IndexOf<ColorSet>(GlobalAssets.Instance.colorSetOptions, this) == 0;
	}

	// Token: 0x04003705 RID: 14085
	public string settingName;

	// Token: 0x04003706 RID: 14086
	[Header("Logic")]
	public Color32 logicOn;

	// Token: 0x04003707 RID: 14087
	public Color32 logicOff;

	// Token: 0x04003708 RID: 14088
	public Color32 logicDisconnected;

	// Token: 0x04003709 RID: 14089
	public Color32 logicOnText;

	// Token: 0x0400370A RID: 14090
	public Color32 logicOffText;

	// Token: 0x0400370B RID: 14091
	public Color32 logicOnSidescreen;

	// Token: 0x0400370C RID: 14092
	public Color32 logicOffSidescreen;

	// Token: 0x0400370D RID: 14093
	[Header("Decor")]
	public Color32 decorPositive;

	// Token: 0x0400370E RID: 14094
	public Color32 decorNegative;

	// Token: 0x0400370F RID: 14095
	public Color32 decorBaseline;

	// Token: 0x04003710 RID: 14096
	public Color32 decorHighlightPositive;

	// Token: 0x04003711 RID: 14097
	public Color32 decorHighlightNegative;

	// Token: 0x04003712 RID: 14098
	[Header("Crop Overlay")]
	public Color32 cropHalted;

	// Token: 0x04003713 RID: 14099
	public Color32 cropGrowing;

	// Token: 0x04003714 RID: 14100
	public Color32 cropGrown;

	// Token: 0x04003715 RID: 14101
	[Header("Harvest Overlay")]
	public Color32 harvestEnabled;

	// Token: 0x04003716 RID: 14102
	public Color32 harvestDisabled;

	// Token: 0x04003717 RID: 14103
	[Header("Gameplay Events")]
	public Color32 eventPositive;

	// Token: 0x04003718 RID: 14104
	public Color32 eventNegative;

	// Token: 0x04003719 RID: 14105
	public Color32 eventNeutral;

	// Token: 0x0400371A RID: 14106
	[Header("Notifications")]
	public Color32 NotificationBad;

	// Token: 0x0400371B RID: 14107
	public Color32 NotificationEvent;

	// Token: 0x0400371C RID: 14108
	[Header("Info Screen Status Items")]
	public Color32 statusItemBad;

	// Token: 0x0400371D RID: 14109
	public Color32 statusItemEvent;

	// Token: 0x0400371E RID: 14110
	[Header("Germ Overlay")]
	public Color32 germFoodPoisoning;

	// Token: 0x0400371F RID: 14111
	public Color32 germPollenGerms;

	// Token: 0x04003720 RID: 14112
	public Color32 germSlimeLung;

	// Token: 0x04003721 RID: 14113
	public Color32 germZombieSpores;

	// Token: 0x04003722 RID: 14114
	public Color32 germRadiationSickness;

	// Token: 0x04003723 RID: 14115
	[Header("Room Overlay")]
	public Color32 roomNone;

	// Token: 0x04003724 RID: 14116
	public Color32 roomFood;

	// Token: 0x04003725 RID: 14117
	public Color32 roomSleep;

	// Token: 0x04003726 RID: 14118
	public Color32 roomRecreation;

	// Token: 0x04003727 RID: 14119
	public Color32 roomBathroom;

	// Token: 0x04003728 RID: 14120
	public Color32 roomHospital;

	// Token: 0x04003729 RID: 14121
	public Color32 roomIndustrial;

	// Token: 0x0400372A RID: 14122
	public Color32 roomAgricultural;

	// Token: 0x0400372B RID: 14123
	public Color32 roomScience;

	// Token: 0x0400372C RID: 14124
	public Color32 roomPark;

	// Token: 0x0400372D RID: 14125
	[Header("Power Overlay")]
	public Color32 powerConsumer;

	// Token: 0x0400372E RID: 14126
	public Color32 powerGenerator;

	// Token: 0x0400372F RID: 14127
	public Color32 powerBuildingDisabled;

	// Token: 0x04003730 RID: 14128
	public Color32 powerCircuitUnpowered;

	// Token: 0x04003731 RID: 14129
	public Color32 powerCircuitSafe;

	// Token: 0x04003732 RID: 14130
	public Color32 powerCircuitStraining;

	// Token: 0x04003733 RID: 14131
	public Color32 powerCircuitOverloading;

	// Token: 0x04003734 RID: 14132
	[Header("Light Overlay")]
	public Color32 lightOverlay;

	// Token: 0x04003735 RID: 14133
	[Header("Conduit Overlay")]
	public Color32 conduitNormal;

	// Token: 0x04003736 RID: 14134
	public Color32 conduitInsulated;

	// Token: 0x04003737 RID: 14135
	public Color32 conduitRadiant;

	// Token: 0x04003738 RID: 14136
	[Header("Temperature Overlay")]
	public Color32 temperatureThreshold0;

	// Token: 0x04003739 RID: 14137
	public Color32 temperatureThreshold1;

	// Token: 0x0400373A RID: 14138
	public Color32 temperatureThreshold2;

	// Token: 0x0400373B RID: 14139
	public Color32 temperatureThreshold3;

	// Token: 0x0400373C RID: 14140
	public Color32 temperatureThreshold4;

	// Token: 0x0400373D RID: 14141
	public Color32 temperatureThreshold5;

	// Token: 0x0400373E RID: 14142
	public Color32 temperatureThreshold6;

	// Token: 0x0400373F RID: 14143
	public Color32 temperatureThreshold7;

	// Token: 0x04003740 RID: 14144
	public Color32 heatflowThreshold0;

	// Token: 0x04003741 RID: 14145
	public Color32 heatflowThreshold1;

	// Token: 0x04003742 RID: 14146
	public Color32 heatflowThreshold2;

	// Token: 0x04003743 RID: 14147
	private Dictionary<string, Color32> namedLookup;
}
