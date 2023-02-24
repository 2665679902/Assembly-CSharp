using System;
using UnityEngine;

// Token: 0x02000730 RID: 1840
public class EffectPrefabs : MonoBehaviour
{
	// Token: 0x1700039B RID: 923
	// (get) Token: 0x06003274 RID: 12916 RVA: 0x00110B65 File Offset: 0x0010ED65
	// (set) Token: 0x06003275 RID: 12917 RVA: 0x00110B6C File Offset: 0x0010ED6C
	public static EffectPrefabs Instance { get; private set; }

	// Token: 0x06003276 RID: 12918 RVA: 0x00110B74 File Offset: 0x0010ED74
	private void Awake()
	{
		EffectPrefabs.Instance = this;
	}

	// Token: 0x04001EB6 RID: 7862
	public GameObject DreamBubble;

	// Token: 0x04001EB7 RID: 7863
	public GameObject ThoughtBubble;

	// Token: 0x04001EB8 RID: 7864
	public GameObject ThoughtBubbleConvo;

	// Token: 0x04001EB9 RID: 7865
	public GameObject MeteorBackground;

	// Token: 0x04001EBA RID: 7866
	public GameObject SparkleStreakFX;

	// Token: 0x04001EBB RID: 7867
	public GameObject HappySingerFX;

	// Token: 0x04001EBC RID: 7868
	public GameObject HugFrenzyFX;

	// Token: 0x04001EBD RID: 7869
	public GameObject GameplayEventDisplay;

	// Token: 0x04001EBE RID: 7870
	public GameObject OpenTemporalTearBeam;
}
