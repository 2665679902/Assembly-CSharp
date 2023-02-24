using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x0200040E RID: 1038
public class ClusterMapSoundEvent : SoundEvent
{
	// Token: 0x06001597 RID: 5527 RVA: 0x0006FE70 File Offset: 0x0006E070
	public ClusterMapSoundEvent(string file_name, string sound_name, int frame, bool looping)
		: base(file_name, sound_name, frame, true, looping, (float)SoundEvent.IGNORE_INTERVAL, false)
	{
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x0006FE85 File Offset: 0x0006E085
	public override void OnPlay(AnimEventManager.EventPlayerData behaviour)
	{
		if (ClusterMapScreen.Instance.IsActive())
		{
			this.PlaySound(behaviour);
		}
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x0006FE9C File Offset: 0x0006E09C
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component == null)
			{
				global::Debug.Log(behaviour.name + " (Cluster Map Object) is missing LoopingSounds component.");
				return;
			}
			if (!component.StartSound(base.sound, true, false, false))
			{
				DebugUtil.LogWarningArgs(new object[] { string.Format("SoundEvent has invalid sound [{0}] on behaviour [{1}]", base.sound, behaviour.name) });
				return;
			}
		}
		else
		{
			EventInstance eventInstance = KFMOD.BeginOneShot(base.sound, Vector3.zero, 1f);
			eventInstance.setParameterByName(ClusterMapSoundEvent.X_POSITION_PARAMETER, behaviour.controller.transform.GetPosition().x / (float)Screen.width, false);
			eventInstance.setParameterByName(ClusterMapSoundEvent.Y_POSITION_PARAMETER, behaviour.controller.transform.GetPosition().y / (float)Screen.height, false);
			eventInstance.setParameterByName(ClusterMapSoundEvent.ZOOM_PARAMETER, ClusterMapScreen.Instance.CurrentZoomPercentage(), false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x0006FF9C File Offset: 0x0006E19C
	public override void Stop(AnimEventManager.EventPlayerData behaviour)
	{
		if (base.looping)
		{
			LoopingSounds component = behaviour.GetComponent<LoopingSounds>();
			if (component != null)
			{
				component.StopSound(base.sound);
			}
		}
	}

	// Token: 0x04000C09 RID: 3081
	private static string X_POSITION_PARAMETER = "Starmap_Position_X";

	// Token: 0x04000C0A RID: 3082
	private static string Y_POSITION_PARAMETER = "Starmap_Position_Y";

	// Token: 0x04000C0B RID: 3083
	private static string ZOOM_PARAMETER = "Starmap_Zoom_Percentage";
}
