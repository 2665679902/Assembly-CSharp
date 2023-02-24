using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class ClustercraftInteriorDoorConfig : IEntityConfig
{
	// Token: 0x0600011D RID: 285 RVA: 0x000088E7 File Offset: 0x00006AE7
	public string[] GetDlcIds()
	{
		return DlcManager.AVAILABLE_EXPANSION1_ONLY;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000088F0 File Offset: 0x00006AF0
	public GameObject CreatePrefab()
	{
		string id = ClustercraftInteriorDoorConfig.ID;
		string text = STRINGS.BUILDINGS.PREFABS.CLUSTERCRAFTINTERIORDOOR.NAME;
		string text2 = STRINGS.BUILDINGS.PREFABS.CLUSTERCRAFTINTERIORDOOR.DESC;
		float num = 400f;
		EffectorValues tier = TUNING.BUILDINGS.DECOR.BONUS.TIER0;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("rocket_hatch_door_kanim"), "closed", Grid.SceneLayer.TileMain, 1, 2, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.Gravitas }, 293f);
		gameObject.AddTag(GameTags.NotRoomAssignable);
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Unobtanium, true);
		component.Temperature = 294.15f;
		gameObject.AddOrGet<Operational>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<KBatchedAnimController>().fgLayer = Grid.SceneLayer.InteriorWall;
		gameObject.AddOrGet<ClustercraftInteriorDoor>();
		gameObject.AddOrGet<AssignmentGroupController>().generateGroupOnStart = false;
		gameObject.AddOrGet<NavTeleporter>().offset = new CellOffset(1, 0);
		gameObject.AddOrGet<AccessControl>();
		return gameObject;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x000089D5 File Offset: 0x00006BD5
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000089EC File Offset: 0x00006BEC
	public void OnSpawn(GameObject inst)
	{
		PrimaryElement component = inst.GetComponent<PrimaryElement>();
		OccupyArea component2 = inst.GetComponent<OccupyArea>();
		int num = Grid.PosToCell(inst);
		CellOffset[] occupiedCellsOffsets = component2.OccupiedCellsOffsets;
		int[] array = new int[occupiedCellsOffsets.Length];
		for (int i = 0; i < occupiedCellsOffsets.Length; i++)
		{
			CellOffset cellOffset = occupiedCellsOffsets[i];
			int num2 = Grid.OffsetCell(num, cellOffset);
			array[i] = num2;
		}
		foreach (int num3 in array)
		{
			Grid.HasDoor[num3] = true;
			SimMessages.SetCellProperties(num3, 8);
			Grid.RenderedByWorld[num3] = false;
			World.Instance.groundRenderer.MarkDirty(num3);
			SimMessages.ReplaceAndDisplaceElement(num3, component.ElementID, CellEventLogger.Instance.DoorClose, component.Mass / 2f, component.Temperature, byte.MaxValue, 0, -1);
			SimMessages.SetCellProperties(num3, 4);
		}
	}

	// Token: 0x0400009A RID: 154
	public static string ID = "ClustercraftInteriorDoor";
}
