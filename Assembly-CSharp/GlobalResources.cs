using System;
using FMODUnity;
using UnityEngine;

// Token: 0x020009E9 RID: 2537
public class GlobalResources : ScriptableObject
{
	// Token: 0x06004BD7 RID: 19415 RVA: 0x001A97C8 File Offset: 0x001A79C8
	public static GlobalResources Instance()
	{
		if (GlobalResources._Instance == null)
		{
			GlobalResources._Instance = Resources.Load<GlobalResources>("GlobalResources");
		}
		return GlobalResources._Instance;
	}

	// Token: 0x040031E6 RID: 12774
	public Material AnimMaterial;

	// Token: 0x040031E7 RID: 12775
	public Material AnimUIMaterial;

	// Token: 0x040031E8 RID: 12776
	public Material AnimPlaceMaterial;

	// Token: 0x040031E9 RID: 12777
	public Material AnimMaterialUIDesaturated;

	// Token: 0x040031EA RID: 12778
	public Material AnimSimpleMaterial;

	// Token: 0x040031EB RID: 12779
	public Material AnimOverlayMaterial;

	// Token: 0x040031EC RID: 12780
	public Texture2D WhiteTexture;

	// Token: 0x040031ED RID: 12781
	public EventReference ConduitOverlaySoundLiquid;

	// Token: 0x040031EE RID: 12782
	public EventReference ConduitOverlaySoundGas;

	// Token: 0x040031EF RID: 12783
	public EventReference ConduitOverlaySoundSolid;

	// Token: 0x040031F0 RID: 12784
	public EventReference AcousticDisturbanceSound;

	// Token: 0x040031F1 RID: 12785
	public EventReference AcousticDisturbanceBubbleSound;

	// Token: 0x040031F2 RID: 12786
	public EventReference WallDamageLayerSound;

	// Token: 0x040031F3 RID: 12787
	public Sprite sadDupeAudio;

	// Token: 0x040031F4 RID: 12788
	public Sprite sadDupe;

	// Token: 0x040031F5 RID: 12789
	public Sprite baseGameLogoSmall;

	// Token: 0x040031F6 RID: 12790
	public Sprite expansion1LogoSmall;

	// Token: 0x040031F7 RID: 12791
	private static GlobalResources _Instance;
}
