using System;
using System.IO;
using ProcGenGame;
using UnityEngine;

// Token: 0x02000C34 RID: 3124
public class WorldGenScreen : NewGameFlowScreen
{
	// Token: 0x060062C8 RID: 25288 RVA: 0x00247902 File Offset: 0x00245B02
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		WorldGenScreen.Instance = this;
	}

	// Token: 0x060062C9 RID: 25289 RVA: 0x00247910 File Offset: 0x00245B10
	protected override void OnForcedCleanUp()
	{
		WorldGenScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x060062CA RID: 25290 RVA: 0x00247920 File Offset: 0x00245B20
	protected override void OnSpawn()
	{
		base.OnSpawn();
		if (MainMenu.Instance != null)
		{
			MainMenu.Instance.StopAmbience();
		}
		this.TriggerLoadingMusic();
		UnityEngine.Object.FindObjectOfType<FrontEndBackground>().gameObject.SetActive(false);
		SaveLoader.SetActiveSaveFilePath(null);
		try
		{
			int num = 0;
			while (File.Exists(WorldGen.GetSIMSaveFilename(num)))
			{
				File.Delete(WorldGen.GetSIMSaveFilename(num));
				num++;
			}
		}
		catch (Exception ex)
		{
			DebugUtil.LogWarningArgs(new object[] { ex.ToString() });
		}
		this.offlineWorldGen.Generate();
	}

	// Token: 0x060062CB RID: 25291 RVA: 0x002479BC File Offset: 0x00245BBC
	private void TriggerLoadingMusic()
	{
		if (AudioDebug.Get().musicEnabled && !MusicManager.instance.SongIsPlaying("Music_FrontEnd"))
		{
			MainMenu.Instance.StopMainMenuMusic();
			AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndWorldGenerationSnapshot);
			MusicManager.instance.PlaySong("Music_FrontEnd", false);
			MusicManager.instance.SetSongParameter("Music_FrontEnd", "songSection", 1f, true);
		}
	}

	// Token: 0x060062CC RID: 25292 RVA: 0x00247A2F File Offset: 0x00245C2F
	public override void OnKeyDown(KButtonEvent e)
	{
		if (!e.Consumed)
		{
			e.TryConsume(global::Action.Escape);
		}
		if (!e.Consumed)
		{
			e.TryConsume(global::Action.MouseRight);
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0400449D RID: 17565
	[MyCmpReq]
	private OfflineWorldGen offlineWorldGen;

	// Token: 0x0400449E RID: 17566
	public static WorldGenScreen Instance;
}
