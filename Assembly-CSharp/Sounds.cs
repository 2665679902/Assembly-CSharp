using System;
using FMODUnity;
using UnityEngine;

// Token: 0x02000932 RID: 2354
[AddComponentMenu("KMonoBehaviour/scripts/Sounds")]
public class Sounds : KMonoBehaviour
{
	// Token: 0x170004E9 RID: 1257
	// (get) Token: 0x060044F5 RID: 17653 RVA: 0x001851F0 File Offset: 0x001833F0
	// (set) Token: 0x060044F6 RID: 17654 RVA: 0x001851F7 File Offset: 0x001833F7
	public static Sounds Instance { get; private set; }

	// Token: 0x060044F7 RID: 17655 RVA: 0x001851FF File Offset: 0x001833FF
	public static void DestroyInstance()
	{
		Sounds.Instance = null;
	}

	// Token: 0x060044F8 RID: 17656 RVA: 0x00185207 File Offset: 0x00183407
	protected override void OnPrefabInit()
	{
		Sounds.Instance = this;
	}

	// Token: 0x04002DEF RID: 11759
	public FMODAsset BlowUp_Generic;

	// Token: 0x04002DF0 RID: 11760
	public FMODAsset Build_Generic;

	// Token: 0x04002DF1 RID: 11761
	public FMODAsset InUse_Fabricator;

	// Token: 0x04002DF2 RID: 11762
	public FMODAsset InUse_OxygenGenerator;

	// Token: 0x04002DF3 RID: 11763
	public FMODAsset Place_OreOnSite;

	// Token: 0x04002DF4 RID: 11764
	public FMODAsset Footstep_rock;

	// Token: 0x04002DF5 RID: 11765
	public FMODAsset Ice_crack;

	// Token: 0x04002DF6 RID: 11766
	public FMODAsset BuildingPowerOn;

	// Token: 0x04002DF7 RID: 11767
	public FMODAsset ElectricGridOverload;

	// Token: 0x04002DF8 RID: 11768
	public FMODAsset IngameMusic;

	// Token: 0x04002DF9 RID: 11769
	public FMODAsset[] OreSplashSounds;

	// Token: 0x04002DFB RID: 11771
	public EventReference BlowUp_GenericMigrated;

	// Token: 0x04002DFC RID: 11772
	public EventReference Build_GenericMigrated;

	// Token: 0x04002DFD RID: 11773
	public EventReference InUse_FabricatorMigrated;

	// Token: 0x04002DFE RID: 11774
	public EventReference InUse_OxygenGeneratorMigrated;

	// Token: 0x04002DFF RID: 11775
	public EventReference Place_OreOnSiteMigrated;

	// Token: 0x04002E00 RID: 11776
	public EventReference Footstep_rockMigrated;

	// Token: 0x04002E01 RID: 11777
	public EventReference Ice_crackMigrated;

	// Token: 0x04002E02 RID: 11778
	public EventReference BuildingPowerOnMigrated;

	// Token: 0x04002E03 RID: 11779
	public EventReference ElectricGridOverloadMigrated;

	// Token: 0x04002E04 RID: 11780
	public EventReference IngameMusicMigrated;

	// Token: 0x04002E05 RID: 11781
	public EventReference[] OreSplashSoundsMigrated;
}
