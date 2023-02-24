﻿using System;
using System.Diagnostics;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000412 RID: 1042
[DebuggerDisplay("{Name}")]
public class FloorSoundEvent : SoundEvent
{
	// Token: 0x060015A6 RID: 5542 RVA: 0x0007045C File Offset: 0x0006E65C
	public FloorSoundEvent(string file_name, string sound_name, int frame)
		: base(file_name, sound_name, frame, false, false, (float)SoundEvent.IGNORE_INTERVAL, true)
	{
		base.noiseValues = SoundEventVolumeCache.instance.GetVolume("FloorSoundEvent", sound_name);
	}

	// Token: 0x060015A7 RID: 5543 RVA: 0x00070488 File Offset: 0x0006E688
	public override void PlaySound(AnimEventManager.EventPlayerData behaviour)
	{
		Vector3 vector = behaviour.GetComponent<Transform>().GetPosition();
		KBatchedAnimController component = behaviour.GetComponent<KBatchedAnimController>();
		if (component != null)
		{
			vector = component.GetPivotSymbolPosition();
		}
		int num = Grid.PosToCell(vector);
		string text = GlobalAssets.GetSound(StringFormatter.Combine(FloorSoundEvent.GetAudioCategory(Grid.CellBelow(num)), "_", base.name), true);
		if (text == null)
		{
			text = GlobalAssets.GetSound(StringFormatter.Combine("Rock_", base.name), true);
			if (text == null)
			{
				text = GlobalAssets.GetSound(base.name, true);
			}
		}
		GameObject gameObject = behaviour.controller.gameObject;
		base.objectIsSelectedAndVisible = SoundEvent.ObjectIsSelectedAndVisible(gameObject);
		if (SoundEvent.IsLowPrioritySound(text) && !base.objectIsSelectedAndVisible)
		{
			return;
		}
		vector = SoundEvent.GetCameraScaledPosition(vector, false);
		vector.z = 0f;
		if (base.objectIsSelectedAndVisible)
		{
			vector = SoundEvent.AudioHighlightListenerPosition(vector);
		}
		if (Grid.Element == null)
		{
			return;
		}
		bool isLiquid = Grid.Element[num].IsLiquid;
		float num2 = 0f;
		if (isLiquid)
		{
			num2 = SoundUtil.GetLiquidDepth(num);
			string sound = GlobalAssets.GetSound("Liquid_footstep", true);
			if (sound != null && (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, sound, base.looping, this.isDynamic)))
			{
				FMOD.Studio.EventInstance eventInstance = SoundEvent.BeginOneShot(sound, vector, SoundEvent.GetVolume(base.objectIsSelectedAndVisible), false);
				if (num2 > 0f)
				{
					eventInstance.setParameterByName("liquidDepth", num2, false);
				}
				SoundEvent.EndOneShot(eventInstance);
			}
		}
		if (text != null && (base.objectIsSelectedAndVisible || SoundEvent.ShouldPlaySound(behaviour.controller, text, base.looping, this.isDynamic)))
		{
			FMOD.Studio.EventInstance eventInstance2 = SoundEvent.BeginOneShot(text, vector, 1f, false);
			if (eventInstance2.isValid())
			{
				if (num2 > 0f)
				{
					eventInstance2.setParameterByName("liquidDepth", num2, false);
				}
				if (behaviour.controller.HasAnimationFile("anim_loco_walk_kanim"))
				{
					eventInstance2.setVolume(FloorSoundEvent.IDLE_WALKING_VOLUME_REDUCTION);
				}
				SoundEvent.EndOneShot(eventInstance2);
			}
		}
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x00070670 File Offset: 0x0006E870
	private static string GetAudioCategory(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return "Rock";
		}
		Element element = Grid.Element[cell];
		if (Grid.Foundation[cell])
		{
			BuildingDef buildingDef = null;
			GameObject gameObject = Grid.Objects[cell, 1];
			if (gameObject != null)
			{
				Building component = gameObject.GetComponent<BuildingComplete>();
				if (component != null)
				{
					buildingDef = component.Def;
				}
			}
			string text = "";
			if (buildingDef != null)
			{
				string prefabID = buildingDef.PrefabID;
				if (prefabID == "PlasticTile")
				{
					text = "TilePlastic";
				}
				else if (prefabID == "GlassTile")
				{
					text = "TileGlass";
				}
				else if (prefabID == "BunkerTile")
				{
					text = "TileBunker";
				}
				else if (prefabID == "MetalTile")
				{
					text = "TileMetal";
				}
				else if (prefabID == "CarpetTile")
				{
					text = "Carpet";
				}
				else
				{
					text = "Tile";
				}
			}
			return text;
		}
		string floorEventAudioCategory = element.substance.GetFloorEventAudioCategory();
		if (floorEventAudioCategory != null)
		{
			return floorEventAudioCategory;
		}
		if (element.HasTag(GameTags.RefinedMetal))
		{
			return "RefinedMetal";
		}
		if (element.HasTag(GameTags.Metal))
		{
			return "RawMetal";
		}
		return "Rock";
	}

	// Token: 0x04000C11 RID: 3089
	public static float IDLE_WALKING_VOLUME_REDUCTION = 0.55f;
}
