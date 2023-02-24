using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x020000FD RID: 253
public class SoundListenerController : MonoBehaviour
{
	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600089B RID: 2203 RVA: 0x00022C68 File Offset: 0x00020E68
	// (set) Token: 0x0600089C RID: 2204 RVA: 0x00022C6F File Offset: 0x00020E6F
	public static SoundListenerController Instance { get; private set; }

	// Token: 0x0600089D RID: 2205 RVA: 0x00022C77 File Offset: 0x00020E77
	private void Awake()
	{
		SoundListenerController.Instance = this;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x00022C7F File Offset: 0x00020E7F
	private void OnDestroy()
	{
		SoundListenerController.Instance = null;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00022C88 File Offset: 0x00020E88
	private void Start()
	{
		if (RuntimeManager.IsInitialized)
		{
			RuntimeManager.StudioSystem.getVCA("vca:/Looping", out this.loopingVCA);
			return;
		}
		base.enabled = false;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00022CBD File Offset: 0x00020EBD
	public void SetLoopingVolume(float volume)
	{
		this.loopingVCA.setVolume(volume);
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00022CCC File Offset: 0x00020ECC
	private void Update()
	{
		Audio audio = Audio.Get();
		Vector3 position = Camera.main.transform.GetPosition();
		float num = (Camera.main.orthographicSize - audio.listenerMinOrthographicSize) / (audio.listenerReferenceOrthographicSize - audio.listenerMinOrthographicSize);
		num = Mathf.Max(num, 0f);
		float num2 = -audio.listenerMinZ - num * (audio.listenerReferenceZ - audio.listenerMinZ);
		position.z = num2;
		base.transform.SetPosition(position);
	}

	// Token: 0x0400065E RID: 1630
	private VCA loopingVCA;
}
