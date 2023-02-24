using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200000F RID: 15
public readonly struct MinionVoice
{
	// Token: 0x06000040 RID: 64 RVA: 0x00003BA0 File Offset: 0x00001DA0
	public MinionVoice(int voiceIndex)
	{
		this.voiceIndex = voiceIndex;
		this.voiceId = (voiceIndex + 1).ToString("D2");
		this.isValid = true;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00003BD1 File Offset: 0x00001DD1
	public static MinionVoice ByPersonality(Personality personality)
	{
		return MinionVoice.ByPersonality(personality.Id);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x00003BE0 File Offset: 0x00001DE0
	public static MinionVoice ByPersonality(string personalityId)
	{
		if (personalityId == "JORGE")
		{
			return new MinionVoice(-2);
		}
		if (personalityId == "MEEP")
		{
			return new MinionVoice(2);
		}
		MinionVoice minionVoice;
		if (!MinionVoice.personalityVoiceMap.TryGetValue(personalityId, out minionVoice))
		{
			minionVoice = MinionVoice.Random();
			MinionVoice.personalityVoiceMap.Add(personalityId, minionVoice);
		}
		return minionVoice;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00003C38 File Offset: 0x00001E38
	public static MinionVoice Random()
	{
		return new MinionVoice(UnityEngine.Random.Range(0, 4));
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00003C48 File Offset: 0x00001E48
	public static Option<MinionVoice> ByObject(UnityEngine.Object unityObject)
	{
		GameObject gameObject = unityObject as GameObject;
		GameObject gameObject2;
		if (gameObject != null)
		{
			gameObject2 = gameObject;
		}
		else
		{
			Component component = unityObject as Component;
			if (component != null)
			{
				gameObject2 = component.gameObject;
			}
			else
			{
				gameObject2 = null;
			}
		}
		if (gameObject2.IsNullOrDestroyed())
		{
			return Option.None;
		}
		MinionVoiceProviderMB componentInParent = gameObject2.GetComponentInParent<MinionVoiceProviderMB>();
		if (componentInParent.IsNullOrDestroyed())
		{
			return Option.None;
		}
		return componentInParent.voice;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00003CAC File Offset: 0x00001EAC
	public string GetSoundAssetName(string localName)
	{
		global::Debug.Assert(this.isValid);
		string text = localName;
		if (localName.Contains(":"))
		{
			text = localName.Split(new char[] { ':' })[0];
		}
		return StringFormatter.Combine("DupVoc_", this.voiceId, "_", text);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00003CFD File Offset: 0x00001EFD
	public string GetSoundPath(string localName)
	{
		return GlobalAssets.GetSound(this.GetSoundAssetName(localName), true);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00003D0C File Offset: 0x00001F0C
	public void PlaySoundUI(string localName)
	{
		global::Debug.Assert(this.isValid);
		string soundPath = this.GetSoundPath(localName);
		try
		{
			if (SoundListenerController.Instance == null)
			{
				KFMOD.PlayUISound(soundPath);
			}
			else
			{
				KFMOD.PlayOneShot(soundPath, SoundListenerController.Instance.transform.GetPosition(), 1f);
			}
		}
		catch
		{
			DebugUtil.LogWarningArgs(new object[] { "AUDIOERROR: Missing [" + soundPath + "]" });
		}
	}

	// Token: 0x04000033 RID: 51
	public readonly int voiceIndex;

	// Token: 0x04000034 RID: 52
	public readonly string voiceId;

	// Token: 0x04000035 RID: 53
	public readonly bool isValid;

	// Token: 0x04000036 RID: 54
	private static Dictionary<string, MinionVoice> personalityVoiceMap = new Dictionary<string, MinionVoice>();
}
