using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;

// Token: 0x02000B5D RID: 2909
public class PopFXManager : KScreen
{
	// Token: 0x06005AB1 RID: 23217 RVA: 0x0020ED91 File Offset: 0x0020CF91
	public static void DestroyInstance()
	{
		PopFXManager.Instance = null;
	}

	// Token: 0x06005AB2 RID: 23218 RVA: 0x0020ED99 File Offset: 0x0020CF99
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		PopFXManager.Instance = this;
	}

	// Token: 0x06005AB3 RID: 23219 RVA: 0x0020EDA8 File Offset: 0x0020CFA8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.ready = true;
		if (GenericGameSettings.instance.disablePopFx)
		{
			return;
		}
		for (int i = 0; i < 20; i++)
		{
			PopFX popFX = this.CreatePopFX();
			this.Pool.Add(popFX);
		}
	}

	// Token: 0x06005AB4 RID: 23220 RVA: 0x0020EDEF File Offset: 0x0020CFEF
	public bool Ready()
	{
		return this.ready;
	}

	// Token: 0x06005AB5 RID: 23221 RVA: 0x0020EDF8 File Offset: 0x0020CFF8
	public PopFX SpawnFX(Sprite icon, string text, Transform target_transform, Vector3 offset, float lifetime = 1.5f, bool track_target = false, bool force_spawn = false)
	{
		if (GenericGameSettings.instance.disablePopFx)
		{
			return null;
		}
		if (Game.IsQuitting())
		{
			return null;
		}
		Vector3 vector = offset;
		if (target_transform != null)
		{
			vector += target_transform.GetPosition();
		}
		if (!force_spawn)
		{
			int num = Grid.PosToCell(vector);
			if (!Grid.IsValidCell(num) || !Grid.IsVisible(num))
			{
				return null;
			}
		}
		PopFX popFX;
		if (this.Pool.Count > 0)
		{
			popFX = this.Pool[0];
			this.Pool[0].gameObject.SetActive(true);
			this.Pool[0].Spawn(icon, text, target_transform, offset, lifetime, track_target);
			this.Pool.RemoveAt(0);
		}
		else
		{
			popFX = this.CreatePopFX();
			popFX.gameObject.SetActive(true);
			popFX.Spawn(icon, text, target_transform, offset, lifetime, track_target);
		}
		return popFX;
	}

	// Token: 0x06005AB6 RID: 23222 RVA: 0x0020EED1 File Offset: 0x0020D0D1
	public PopFX SpawnFX(Sprite icon, string text, Transform target_transform, float lifetime = 1.5f, bool track_target = false)
	{
		return this.SpawnFX(icon, text, target_transform, Vector3.zero, lifetime, track_target, false);
	}

	// Token: 0x06005AB7 RID: 23223 RVA: 0x0020EEE6 File Offset: 0x0020D0E6
	private PopFX CreatePopFX()
	{
		GameObject gameObject = Util.KInstantiate(this.Prefab_PopFX, base.gameObject, "Pooled_PopFX");
		gameObject.transform.localScale = Vector3.one;
		return gameObject.GetComponent<PopFX>();
	}

	// Token: 0x06005AB8 RID: 23224 RVA: 0x0020EF13 File Offset: 0x0020D113
	public void RecycleFX(PopFX fx)
	{
		this.Pool.Add(fx);
	}

	// Token: 0x04003D6A RID: 15722
	public static PopFXManager Instance;

	// Token: 0x04003D6B RID: 15723
	public GameObject Prefab_PopFX;

	// Token: 0x04003D6C RID: 15724
	public List<PopFX> Pool = new List<PopFX>();

	// Token: 0x04003D6D RID: 15725
	public Sprite sprite_Plus;

	// Token: 0x04003D6E RID: 15726
	public Sprite sprite_Negative;

	// Token: 0x04003D6F RID: 15727
	public Sprite sprite_Resource;

	// Token: 0x04003D70 RID: 15728
	public Sprite sprite_Building;

	// Token: 0x04003D71 RID: 15729
	public Sprite sprite_Research;

	// Token: 0x04003D72 RID: 15730
	private bool ready;
}
