using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class GameAudioSheets : AudioSheets
{
	// Token: 0x06001767 RID: 5991 RVA: 0x00079ED4 File Offset: 0x000780D4
	public static GameAudioSheets Get()
	{
		if (GameAudioSheets._Instance == null)
		{
			GameAudioSheets._Instance = Resources.Load<GameAudioSheets>("GameAudioSheets");
		}
		return GameAudioSheets._Instance;
	}

	// Token: 0x06001768 RID: 5992 RVA: 0x00079EF8 File Offset: 0x000780F8
	public override void Initialize()
	{
		this.validFileNames.Add("game_triggered");
		foreach (KAnimFile kanimFile in Assets.instance.AnimAssets)
		{
			if (!(kanimFile == null))
			{
				this.validFileNames.Add(kanimFile.name);
			}
		}
		base.Initialize();
		foreach (AudioSheet audioSheet in this.sheets)
		{
			foreach (AudioSheet.SoundInfo soundInfo in audioSheet.soundInfos)
			{
				if (soundInfo.Type == "MouthFlapSoundEvent" || soundInfo.Type == "VoiceSoundEvent")
				{
					HashSet<HashedString> hashSet = null;
					if (!this.animsNotAllowedToPlaySpeech.TryGetValue(soundInfo.File, out hashSet))
					{
						hashSet = new HashSet<HashedString>();
						this.animsNotAllowedToPlaySpeech[soundInfo.File] = hashSet;
					}
					hashSet.Add(soundInfo.Anim);
				}
			}
		}
	}

	// Token: 0x06001769 RID: 5993 RVA: 0x0007A060 File Offset: 0x00078260
	protected override AnimEvent CreateSoundOfType(string type, string file_name, string sound_name, int frame, float min_interval, string dlcId)
	{
		SoundEvent soundEvent = null;
		bool flag = true;
		if (sound_name.Contains(":disable_camera_position_scaling"))
		{
			sound_name = sound_name.Replace(":disable_camera_position_scaling", "");
			flag = false;
		}
		if (type == "FloorSoundEvent")
		{
			soundEvent = new FloorSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "SoundEvent" || type == "LoopingSoundEvent")
		{
			bool flag2 = type == "LoopingSoundEvent";
			string[] array = sound_name.Split(new char[] { ':' });
			sound_name = array[0];
			soundEvent = new SoundEvent(file_name, sound_name, frame, true, flag2, min_interval, false);
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i] == "IGNORE_PAUSE")
				{
					soundEvent.ignorePause = true;
				}
				else
				{
					global::Debug.LogWarning(sound_name + " has unknown parameter " + array[i]);
				}
			}
		}
		else if (type == "LadderSoundEvent")
		{
			soundEvent = new LadderSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "LaserSoundEvent")
		{
			soundEvent = new LaserSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "HatchDrillSoundEvent")
		{
			soundEvent = new HatchDrillSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "CreatureChewSoundEvent")
		{
			soundEvent = new CreatureChewSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "BuildingDamageSoundEvent")
		{
			soundEvent = new BuildingDamageSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "WallDamageSoundEvent")
		{
			soundEvent = new WallDamageSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "RemoteSoundEvent")
		{
			soundEvent = new RemoteSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "VoiceSoundEvent" || type == "LoopingVoiceSoundEvent")
		{
			soundEvent = new VoiceSoundEvent(file_name, sound_name, frame, type == "LoopingVoiceSoundEvent");
		}
		else if (type == "MouthFlapSoundEvent")
		{
			soundEvent = new MouthFlapSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "MainMenuSoundEvent")
		{
			soundEvent = new MainMenuSoundEvent(file_name, sound_name, frame);
		}
		else if (type == "ClusterMapSoundEvent")
		{
			soundEvent = new ClusterMapSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "ClusterMapLoopingSoundEvent")
		{
			soundEvent = new ClusterMapSoundEvent(file_name, sound_name, frame, true);
		}
		else if (type == "UIAnimationSoundEvent")
		{
			soundEvent = new UIAnimationSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "UIAnimationVoiceSoundEvent")
		{
			soundEvent = new UIAnimationVoiceSoundEvent(file_name, sound_name, frame, false);
		}
		else if (type == "UIAnimationLoopingSoundEvent")
		{
			soundEvent = new UIAnimationSoundEvent(file_name, sound_name, frame, true);
		}
		else if (type == "CreatureVariationSoundEvent")
		{
			soundEvent = new CreatureVariationSoundEvent(file_name, sound_name, frame, true, type == "LoopingSoundEvent", min_interval, false);
		}
		else if (type == "CountedSoundEvent")
		{
			soundEvent = new CountedSoundEvent(file_name, sound_name, frame, true, false, min_interval, false);
		}
		else if (type == "SculptingSoundEvent")
		{
			soundEvent = new SculptingSoundEvent(file_name, sound_name, frame, true, false, min_interval, false);
		}
		else if (type == "PhonoboxSoundEvent")
		{
			soundEvent = new PhonoboxSoundEvent(file_name, sound_name, frame, min_interval);
		}
		else if (type == "PlantMutationSoundEvent")
		{
			soundEvent = new PlantMutationSoundEvent(file_name, sound_name, frame, min_interval);
		}
		if (soundEvent != null)
		{
			soundEvent.shouldCameraScalePosition = flag;
		}
		return soundEvent;
	}

	// Token: 0x0600176A RID: 5994 RVA: 0x0007A3B0 File Offset: 0x000785B0
	public bool IsAnimAllowedToPlaySpeech(KAnim.Anim anim)
	{
		HashSet<HashedString> hashSet = null;
		return !this.animsNotAllowedToPlaySpeech.TryGetValue(anim.animFile.name, out hashSet) || !hashSet.Contains(anim.hash);
	}

	// Token: 0x04000CFC RID: 3324
	private static GameAudioSheets _Instance;

	// Token: 0x04000CFD RID: 3325
	private HashSet<HashedString> validFileNames = new HashSet<HashedString>();

	// Token: 0x04000CFE RID: 3326
	private Dictionary<HashedString, HashSet<HashedString>> animsNotAllowedToPlaySpeech = new Dictionary<HashedString, HashSet<HashedString>>();

	// Token: 0x0200105A RID: 4186
	private class SingleAudioSheetLoader : AsyncLoader
	{
		// Token: 0x060072BC RID: 29372 RVA: 0x002AEB40 File Offset: 0x002ACD40
		public override void Run()
		{
			this.sheet.soundInfos = new ResourceLoader<AudioSheet.SoundInfo>(this.text, this.name).resources.ToArray();
		}

		// Token: 0x04005754 RID: 22356
		public AudioSheet sheet;

		// Token: 0x04005755 RID: 22357
		public string text;

		// Token: 0x04005756 RID: 22358
		public string name;
	}

	// Token: 0x0200105B RID: 4187
	private class GameAudioSheetLoader : GlobalAsyncLoader<GameAudioSheets.GameAudioSheetLoader>
	{
		// Token: 0x060072BE RID: 29374 RVA: 0x002AEB70 File Offset: 0x002ACD70
		public override void CollectLoaders(List<AsyncLoader> loaders)
		{
			foreach (AudioSheet audioSheet in GameAudioSheets.Get().sheets)
			{
				loaders.Add(new GameAudioSheets.SingleAudioSheetLoader
				{
					sheet = audioSheet,
					text = audioSheet.asset.text,
					name = audioSheet.asset.name
				});
			}
		}

		// Token: 0x060072BF RID: 29375 RVA: 0x002AEBF4 File Offset: 0x002ACDF4
		public override void Run()
		{
		}
	}
}
