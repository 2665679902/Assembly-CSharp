using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class MixManager : MonoBehaviour
{
	// Token: 0x0600176C RID: 5996 RVA: 0x0007A40D File Offset: 0x0007860D
	private void Update()
	{
		if (AudioMixer.instance != null && AudioMixer.instance.persistentSnapshotsActive)
		{
			AudioMixer.instance.UpdatePersistentSnapshotParameters();
		}
	}

	// Token: 0x0600176D RID: 5997 RVA: 0x0007A42C File Offset: 0x0007862C
	private void OnApplicationFocus(bool hasFocus)
	{
		if (AudioMixer.instance == null || AudioMixerSnapshots.Get() == null)
		{
			return;
		}
		if (!hasFocus && KPlayerPrefs.GetInt(AudioOptionsScreen.MuteOnFocusLost) == 1)
		{
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().GameNotFocusedSnapshot);
			return;
		}
		AudioMixer.instance.Stop(AudioMixerSnapshots.Get().GameNotFocusedSnapshot, STOP_MODE.ALLOWFADEOUT);
	}
}
