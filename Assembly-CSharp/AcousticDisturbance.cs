﻿using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000542 RID: 1346
public class AcousticDisturbance
{
	// Token: 0x0600201E RID: 8222 RVA: 0x000AF880 File Offset: 0x000ADA80
	public static void Emit(object data, int EmissionRadius)
	{
		GameObject gameObject = (GameObject)data;
		Components.Cmps<MinionIdentity> liveMinionIdentities = Components.LiveMinionIdentities;
		Vector2 vector = gameObject.transform.GetPosition();
		int num = Grid.PosToCell(vector);
		int num2 = EmissionRadius * EmissionRadius;
		AcousticDisturbance.cellsInRange = GameUtil.CollectCellsBreadthFirst(num, (int cell) => !Grid.Solid[cell], EmissionRadius);
		AcousticDisturbance.DrawVisualEffect(num, AcousticDisturbance.cellsInRange);
		for (int i = 0; i < liveMinionIdentities.Count; i++)
		{
			MinionIdentity minionIdentity = liveMinionIdentities[i];
			if (minionIdentity.gameObject != gameObject.gameObject)
			{
				Vector2 vector2 = minionIdentity.transform.GetPosition();
				if (Vector2.SqrMagnitude(vector - vector2) <= (float)num2)
				{
					int num3 = Grid.PosToCell(vector2);
					if (AcousticDisturbance.cellsInRange.Contains(num3) && minionIdentity.GetSMI<StaminaMonitor.Instance>().IsSleeping())
					{
						minionIdentity.Trigger(-527751701, data);
						minionIdentity.Trigger(1621815900, data);
					}
				}
			}
		}
		AcousticDisturbance.cellsInRange.Clear();
	}

	// Token: 0x0600201F RID: 8223 RVA: 0x000AF990 File Offset: 0x000ADB90
	private static void DrawVisualEffect(int center_cell, HashSet<int> cells)
	{
		SoundEvent.PlayOneShot(GlobalResources.Instance().AcousticDisturbanceSound, Grid.CellToPos(center_cell), 1f);
		foreach (int num in cells)
		{
			int gridDistance = AcousticDisturbance.GetGridDistance(num, center_cell);
			GameScheduler.Instance.Schedule("radialgrid_pre", AcousticDisturbance.distanceDelay * (float)gridDistance, new Action<object>(AcousticDisturbance.SpawnEffect), num, null);
		}
	}

	// Token: 0x06002020 RID: 8224 RVA: 0x000AFA28 File Offset: 0x000ADC28
	private static void SpawnEffect(object data)
	{
		Grid.SceneLayer sceneLayer = Grid.SceneLayer.InteriorWall;
		int num = (int)data;
		KBatchedAnimController kbatchedAnimController = FXHelpers.CreateEffect("radialgrid_kanim", Grid.CellToPosCCC(num, sceneLayer), null, false, sceneLayer, false);
		kbatchedAnimController.destroyOnAnimComplete = false;
		kbatchedAnimController.Play(AcousticDisturbance.PreAnims, KAnim.PlayMode.Loop);
		GameScheduler.Instance.Schedule("radialgrid_loop", AcousticDisturbance.duration, new Action<object>(AcousticDisturbance.DestroyEffect), kbatchedAnimController, null);
	}

	// Token: 0x06002021 RID: 8225 RVA: 0x000AFA8B File Offset: 0x000ADC8B
	private static void DestroyEffect(object data)
	{
		KBatchedAnimController kbatchedAnimController = (KBatchedAnimController)data;
		kbatchedAnimController.destroyOnAnimComplete = true;
		kbatchedAnimController.Play(AcousticDisturbance.PostAnim, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x06002022 RID: 8226 RVA: 0x000AFAB0 File Offset: 0x000ADCB0
	private static int GetGridDistance(int cell, int center_cell)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = Grid.CellToXY(center_cell);
		Vector2I vector2I3 = vector2I - vector2I2;
		return Math.Abs(vector2I3.x) + Math.Abs(vector2I3.y);
	}

	// Token: 0x04001265 RID: 4709
	private static readonly HashedString[] PreAnims = new HashedString[] { "grid_pre", "grid_loop" };

	// Token: 0x04001266 RID: 4710
	private static readonly HashedString PostAnim = "grid_pst";

	// Token: 0x04001267 RID: 4711
	private static float distanceDelay = 0.25f;

	// Token: 0x04001268 RID: 4712
	private static float duration = 3f;

	// Token: 0x04001269 RID: 4713
	private static HashSet<int> cellsInRange = new HashSet<int>();
}
