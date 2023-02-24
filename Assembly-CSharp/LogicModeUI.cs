using System;
using UnityEngine;

// Token: 0x02000A14 RID: 2580
public class LogicModeUI : ScriptableObject
{
	// Token: 0x0400333C RID: 13116
	[Header("Base Assets")]
	public Sprite inputSprite;

	// Token: 0x0400333D RID: 13117
	public Sprite outputSprite;

	// Token: 0x0400333E RID: 13118
	public Sprite resetSprite;

	// Token: 0x0400333F RID: 13119
	public GameObject prefab;

	// Token: 0x04003340 RID: 13120
	public GameObject ribbonInputPrefab;

	// Token: 0x04003341 RID: 13121
	public GameObject ribbonOutputPrefab;

	// Token: 0x04003342 RID: 13122
	public GameObject controlInputPrefab;

	// Token: 0x04003343 RID: 13123
	[Header("Colouring")]
	public Color32 colourOn = new Color32(0, byte.MaxValue, 0, 0);

	// Token: 0x04003344 RID: 13124
	public Color32 colourOff = new Color32(byte.MaxValue, 0, 0, 0);

	// Token: 0x04003345 RID: 13125
	public Color32 colourDisconnected = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue);

	// Token: 0x04003346 RID: 13126
	public Color32 colourOnProtanopia = new Color32(179, 204, 0, 0);

	// Token: 0x04003347 RID: 13127
	public Color32 colourOffProtanopia = new Color32(166, 51, 102, 0);

	// Token: 0x04003348 RID: 13128
	public Color32 colourOnDeuteranopia = new Color32(128, 0, 128, 0);

	// Token: 0x04003349 RID: 13129
	public Color32 colourOffDeuteranopia = new Color32(byte.MaxValue, 153, 0, 0);

	// Token: 0x0400334A RID: 13130
	public Color32 colourOnTritanopia = new Color32(51, 102, byte.MaxValue, 0);

	// Token: 0x0400334B RID: 13131
	public Color32 colourOffTritanopia = new Color32(byte.MaxValue, 153, 0, 0);
}
