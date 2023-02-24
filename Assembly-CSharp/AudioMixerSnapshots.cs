using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

// Token: 0x020009E6 RID: 2534
public class AudioMixerSnapshots : ScriptableObject
{
	// Token: 0x06004BB1 RID: 19377 RVA: 0x001A9388 File Offset: 0x001A7588
	[ContextMenu("Reload")]
	public void ReloadSnapshots()
	{
		this.snapshotMap.Clear();
		EventReference[] array = this.snapshots;
		for (int i = 0; i < array.Length; i++)
		{
			string eventReferencePath = KFMOD.GetEventReferencePath(array[i]);
			if (!eventReferencePath.IsNullOrWhiteSpace())
			{
				this.snapshotMap.Add(eventReferencePath);
			}
		}
	}

	// Token: 0x06004BB2 RID: 19378 RVA: 0x001A93D6 File Offset: 0x001A75D6
	public static AudioMixerSnapshots Get()
	{
		if (AudioMixerSnapshots.instance == null)
		{
			AudioMixerSnapshots.instance = Resources.Load<AudioMixerSnapshots>("AudioMixerSnapshots");
		}
		return AudioMixerSnapshots.instance;
	}

	// Token: 0x04003195 RID: 12693
	public EventReference TechFilterOnMigrated;

	// Token: 0x04003196 RID: 12694
	public EventReference TechFilterLogicOn;

	// Token: 0x04003197 RID: 12695
	public EventReference NightStartedMigrated;

	// Token: 0x04003198 RID: 12696
	public EventReference MenuOpenMigrated;

	// Token: 0x04003199 RID: 12697
	public EventReference MenuOpenHalfEffect;

	// Token: 0x0400319A RID: 12698
	public EventReference SpeedPausedMigrated;

	// Token: 0x0400319B RID: 12699
	public EventReference DuplicantCountAttenuatorMigrated;

	// Token: 0x0400319C RID: 12700
	public EventReference NewBaseSetupSnapshot;

	// Token: 0x0400319D RID: 12701
	public EventReference FrontEndSnapshot;

	// Token: 0x0400319E RID: 12702
	public EventReference FrontEndWelcomeScreenSnapshot;

	// Token: 0x0400319F RID: 12703
	public EventReference FrontEndWorldGenerationSnapshot;

	// Token: 0x040031A0 RID: 12704
	public EventReference IntroNIS;

	// Token: 0x040031A1 RID: 12705
	public EventReference PulseSnapshot;

	// Token: 0x040031A2 RID: 12706
	public EventReference ESCPauseSnapshot;

	// Token: 0x040031A3 RID: 12707
	public EventReference MENUNewDuplicantSnapshot;

	// Token: 0x040031A4 RID: 12708
	public EventReference UserVolumeSettingsSnapshot;

	// Token: 0x040031A5 RID: 12709
	public EventReference DuplicantCountMovingSnapshot;

	// Token: 0x040031A6 RID: 12710
	public EventReference DuplicantCountSleepingSnapshot;

	// Token: 0x040031A7 RID: 12711
	public EventReference PortalLPDimmedSnapshot;

	// Token: 0x040031A8 RID: 12712
	public EventReference DynamicMusicPlayingSnapshot;

	// Token: 0x040031A9 RID: 12713
	public EventReference FabricatorSideScreenOpenSnapshot;

	// Token: 0x040031AA RID: 12714
	public EventReference SpaceVisibleSnapshot;

	// Token: 0x040031AB RID: 12715
	public EventReference MENUStarmapSnapshot;

	// Token: 0x040031AC RID: 12716
	public EventReference MENUStarmapNotPausedSnapshot;

	// Token: 0x040031AD RID: 12717
	public EventReference GameNotFocusedSnapshot;

	// Token: 0x040031AE RID: 12718
	public EventReference FacilityVisibleSnapshot;

	// Token: 0x040031AF RID: 12719
	public EventReference TutorialVideoPlayingSnapshot;

	// Token: 0x040031B0 RID: 12720
	public EventReference VictoryMessageSnapshot;

	// Token: 0x040031B1 RID: 12721
	public EventReference VictoryNISGenericSnapshot;

	// Token: 0x040031B2 RID: 12722
	public EventReference VictoryNISRocketSnapshot;

	// Token: 0x040031B3 RID: 12723
	public EventReference VictoryCinematicSnapshot;

	// Token: 0x040031B4 RID: 12724
	public EventReference VictoryFadeToBlackSnapshot;

	// Token: 0x040031B5 RID: 12725
	public EventReference MuteDynamicMusicSnapshot;

	// Token: 0x040031B6 RID: 12726
	public EventReference ActiveBaseChangeSnapshot;

	// Token: 0x040031B7 RID: 12727
	public EventReference EventPopupSnapshot;

	// Token: 0x040031B8 RID: 12728
	public EventReference SmallRocketInteriorReverbSnapshot;

	// Token: 0x040031B9 RID: 12729
	public EventReference MediumRocketInteriorReverbSnapshot;

	// Token: 0x040031BA RID: 12730
	public EventReference MainMenuVideoPlayingSnapshot;

	// Token: 0x040031BB RID: 12731
	public EventReference TechFilterRadiationOn;

	// Token: 0x040031BC RID: 12732
	public EventReference FrontEndSupplyClosetSnapshot;

	// Token: 0x040031BD RID: 12733
	[SerializeField]
	private EventReference[] snapshots;

	// Token: 0x040031BE RID: 12734
	[NonSerialized]
	public List<string> snapshotMap = new List<string>();

	// Token: 0x040031BF RID: 12735
	private static AudioMixerSnapshots instance;
}
