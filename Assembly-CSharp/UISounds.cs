using System;
using UnityEngine;

// Token: 0x020009B1 RID: 2481
[AddComponentMenu("KMonoBehaviour/scripts/UISounds")]
public class UISounds : KMonoBehaviour
{
	// Token: 0x17000576 RID: 1398
	// (get) Token: 0x060049A5 RID: 18853 RVA: 0x0019C59F File Offset: 0x0019A79F
	// (set) Token: 0x060049A6 RID: 18854 RVA: 0x0019C5A6 File Offset: 0x0019A7A6
	public static UISounds Instance { get; private set; }

	// Token: 0x060049A7 RID: 18855 RVA: 0x0019C5AE File Offset: 0x0019A7AE
	public static void DestroyInstance()
	{
		UISounds.Instance = null;
	}

	// Token: 0x060049A8 RID: 18856 RVA: 0x0019C5B6 File Offset: 0x0019A7B6
	protected override void OnPrefabInit()
	{
		UISounds.Instance = this;
	}

	// Token: 0x060049A9 RID: 18857 RVA: 0x0019C5BE File Offset: 0x0019A7BE
	public static void PlaySound(UISounds.Sound sound)
	{
		UISounds.Instance.PlaySoundInternal(sound);
	}

	// Token: 0x060049AA RID: 18858 RVA: 0x0019C5CC File Offset: 0x0019A7CC
	private void PlaySoundInternal(UISounds.Sound sound)
	{
		for (int i = 0; i < this.soundData.Length; i++)
		{
			if (this.soundData[i].sound == sound)
			{
				if (this.logSounds)
				{
					DebugUtil.LogArgs(new object[]
					{
						"Play sound",
						this.soundData[i].name
					});
				}
				KMonoBehaviour.PlaySound(GlobalAssets.GetSound(this.soundData[i].name, false));
			}
		}
	}

	// Token: 0x04003063 RID: 12387
	[SerializeField]
	private bool logSounds;

	// Token: 0x04003064 RID: 12388
	[SerializeField]
	private UISounds.SoundData[] soundData;

	// Token: 0x020017B3 RID: 6067
	public enum Sound
	{
		// Token: 0x04006DC5 RID: 28101
		NegativeNotification,
		// Token: 0x04006DC6 RID: 28102
		PositiveNotification,
		// Token: 0x04006DC7 RID: 28103
		Select,
		// Token: 0x04006DC8 RID: 28104
		Negative,
		// Token: 0x04006DC9 RID: 28105
		Back,
		// Token: 0x04006DCA RID: 28106
		ClickObject,
		// Token: 0x04006DCB RID: 28107
		HUD_Mouseover,
		// Token: 0x04006DCC RID: 28108
		Object_Mouseover,
		// Token: 0x04006DCD RID: 28109
		ClickHUD
	}

	// Token: 0x020017B4 RID: 6068
	[Serializable]
	private struct SoundData
	{
		// Token: 0x04006DCE RID: 28110
		public string name;

		// Token: 0x04006DCF RID: 28111
		public UISounds.Sound sound;
	}
}
