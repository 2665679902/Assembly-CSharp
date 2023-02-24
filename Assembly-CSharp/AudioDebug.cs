using System;
using UnityEngine;

// Token: 0x02000431 RID: 1073
[AddComponentMenu("KMonoBehaviour/scripts/AudioDebug")]
public class AudioDebug : KMonoBehaviour
{
	// Token: 0x0600171D RID: 5917 RVA: 0x00078278 File Offset: 0x00076478
	public static AudioDebug Get()
	{
		return AudioDebug.instance;
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x0007827F File Offset: 0x0007647F
	protected override void OnPrefabInit()
	{
		AudioDebug.instance = this;
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x00078287 File Offset: 0x00076487
	public void ToggleMusic()
	{
		if (Game.Instance != null)
		{
			Game.Instance.SetMusicEnabled(this.musicEnabled);
		}
		this.musicEnabled = !this.musicEnabled;
	}

	// Token: 0x04000CCB RID: 3275
	private static AudioDebug instance;

	// Token: 0x04000CCC RID: 3276
	public bool musicEnabled;

	// Token: 0x04000CCD RID: 3277
	public bool debugSoundEvents;

	// Token: 0x04000CCE RID: 3278
	public bool debugFloorSounds;

	// Token: 0x04000CCF RID: 3279
	public bool debugGameEventSounds;

	// Token: 0x04000CD0 RID: 3280
	public bool debugNotificationSounds;

	// Token: 0x04000CD1 RID: 3281
	public bool debugVoiceSounds;
}
