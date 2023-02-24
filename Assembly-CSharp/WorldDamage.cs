using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using STRINGS;
using UnityEngine;

// Token: 0x020009D6 RID: 2518
[AddComponentMenu("KMonoBehaviour/scripts/WorldDamage")]
public class WorldDamage : KMonoBehaviour
{
	// Token: 0x170005A0 RID: 1440
	// (get) Token: 0x06004B24 RID: 19236 RVA: 0x001A4B57 File Offset: 0x001A2D57
	// (set) Token: 0x06004B25 RID: 19237 RVA: 0x001A4B5E File Offset: 0x001A2D5E
	public static WorldDamage Instance { get; private set; }

	// Token: 0x06004B26 RID: 19238 RVA: 0x001A4B66 File Offset: 0x001A2D66
	public static void DestroyInstance()
	{
		WorldDamage.Instance = null;
	}

	// Token: 0x06004B27 RID: 19239 RVA: 0x001A4B6E File Offset: 0x001A2D6E
	protected override void OnPrefabInit()
	{
		WorldDamage.Instance = this;
	}

	// Token: 0x06004B28 RID: 19240 RVA: 0x001A4B76 File Offset: 0x001A2D76
	public void RestoreDamageToValue(int cell, float amount)
	{
		if (Grid.Damage[cell] > amount)
		{
			Grid.Damage[cell] = amount;
		}
	}

	// Token: 0x06004B29 RID: 19241 RVA: 0x001A4B8A File Offset: 0x001A2D8A
	public float ApplyDamage(Sim.WorldDamageInfo damage_info)
	{
		return this.ApplyDamage(damage_info.gameCell, this.damageAmount, damage_info.damageSourceOffset, BUILDINGS.DAMAGESOURCES.LIQUID_PRESSURE, UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.LIQUID_PRESSURE);
	}

	// Token: 0x06004B2A RID: 19242 RVA: 0x001A4BB8 File Offset: 0x001A2DB8
	public float ApplyDamage(int cell, float amount, int src_cell, string source_name = null, string pop_text = null)
	{
		float num = 0f;
		if (Grid.Solid[cell])
		{
			float num2 = Grid.Damage[cell];
			num = Mathf.Min(amount, 1f - num2);
			num2 += amount;
			bool flag = num2 > 0.15f;
			if (flag)
			{
				GameObject gameObject = Grid.Objects[cell, 9];
				if (gameObject != null)
				{
					BuildingHP component = gameObject.GetComponent<BuildingHP>();
					if (component != null)
					{
						if (!component.invincible)
						{
							int num3 = Mathf.RoundToInt(Mathf.Max((float)component.HitPoints - (1f - num2) * (float)component.MaxHitPoints, 0f));
							gameObject.Trigger(-794517298, new BuildingHP.DamageSourceInfo
							{
								damage = num3,
								source = source_name,
								popString = pop_text
							});
						}
						else
						{
							num2 = 0f;
						}
					}
				}
			}
			Grid.Damage[cell] = Mathf.Min(1f, num2);
			if (Grid.Damage[cell] >= 1f)
			{
				this.DestroyCell(cell);
			}
			else if (Grid.IsValidCell(src_cell) && flag)
			{
				Element element = Grid.Element[src_cell];
				if (element.IsLiquid && Grid.Mass[src_cell] > 1f)
				{
					int num4 = cell - src_cell;
					if (num4 == 1 || num4 == -1 || num4 == Grid.WidthInCells || num4 == -Grid.WidthInCells)
					{
						int num5 = cell + num4;
						if (Grid.IsValidCell(num5))
						{
							Element element2 = Grid.Element[num5];
							if (!element2.IsSolid && (!element2.IsLiquid || (element2.id == element.id && Grid.Mass[num5] <= 100f)) && (Grid.Properties[num5] & 2) == 0 && !this.spawnTimes.ContainsKey(num5))
							{
								this.spawnTimes[num5] = Time.realtimeSinceStartup;
								ushort idx = element.idx;
								float num6 = Grid.Temperature[src_cell];
								base.StartCoroutine(this.DelayedSpawnFX(src_cell, num5, num4, element, idx, num6));
							}
						}
					}
				}
			}
		}
		return num;
	}

	// Token: 0x06004B2B RID: 19243 RVA: 0x001A4DDE File Offset: 0x001A2FDE
	private void ReleaseGO(GameObject go)
	{
		go.DeleteObject();
	}

	// Token: 0x06004B2C RID: 19244 RVA: 0x001A4DE6 File Offset: 0x001A2FE6
	private IEnumerator DelayedSpawnFX(int src_cell, int dest_cell, int offset, Element elem, ushort idx, float temperature)
	{
		float num = UnityEngine.Random.value * 0.25f;
		yield return new WaitForSeconds(num);
		Vector3 vector = Grid.CellToPosCCC(dest_cell, Grid.SceneLayer.Front);
		GameObject gameObject = GameUtil.KInstantiate(this.leakEffect.gameObject, vector, Grid.SceneLayer.Front, null, 0);
		KBatchedAnimController component = gameObject.GetComponent<KBatchedAnimController>();
		component.TintColour = elem.substance.colour;
		component.onDestroySelf = new Action<GameObject>(this.ReleaseGO);
		SimMessages.AddRemoveSubstance(src_cell, idx, CellEventLogger.Instance.WorldDamageDelayedSpawnFX, -1f, temperature, byte.MaxValue, 0, true, -1);
		if (offset == -1)
		{
			component.Play("side", KAnim.PlayMode.Once, 1f, 0f);
			component.FlipX = true;
			component.enabled = false;
			component.enabled = true;
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() + Vector3.right * 0.5f);
			FallingWater.instance.AddParticle(dest_cell, idx, 1f, temperature, byte.MaxValue, 0, true, false, false, false);
		}
		else if (offset == Grid.WidthInCells)
		{
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() - Vector3.up * 0.5f);
			component.Play("floor", KAnim.PlayMode.Once, 1f, 0f);
			component.enabled = false;
			component.enabled = true;
			SimMessages.AddRemoveSubstance(dest_cell, idx, CellEventLogger.Instance.WorldDamageDelayedSpawnFX, 1f, temperature, byte.MaxValue, 0, true, -1);
		}
		else if (offset == -Grid.WidthInCells)
		{
			component.Play("ceiling", KAnim.PlayMode.Once, 1f, 0f);
			component.enabled = false;
			component.enabled = true;
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() + Vector3.up * 0.5f);
			FallingWater.instance.AddParticle(dest_cell, idx, 1f, temperature, byte.MaxValue, 0, true, false, false, false);
		}
		else
		{
			component.Play("side", KAnim.PlayMode.Once, 1f, 0f);
			component.enabled = false;
			component.enabled = true;
			gameObject.transform.SetPosition(gameObject.transform.GetPosition() - Vector3.right * 0.5f);
			FallingWater.instance.AddParticle(dest_cell, idx, 1f, temperature, byte.MaxValue, 0, true, false, false, false);
		}
		if (CameraController.Instance.IsAudibleSound(gameObject.transform.GetPosition(), this.leakSoundMigrated))
		{
			SoundEvent.PlayOneShot(this.leakSoundMigrated, gameObject.transform.GetPosition(), 1f);
		}
		yield return null;
		yield break;
	}

	// Token: 0x06004B2D RID: 19245 RVA: 0x001A4E24 File Offset: 0x001A3024
	private void Update()
	{
		this.expiredCells.Clear();
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		foreach (KeyValuePair<int, float> keyValuePair in this.spawnTimes)
		{
			if (realtimeSinceStartup - keyValuePair.Value > 1f)
			{
				this.expiredCells.Add(keyValuePair.Key);
			}
		}
		foreach (int num in this.expiredCells)
		{
			this.spawnTimes.Remove(num);
		}
		this.expiredCells.Clear();
	}

	// Token: 0x06004B2E RID: 19246 RVA: 0x001A4EF8 File Offset: 0x001A30F8
	public void DestroyCell(int cell)
	{
		if (Grid.Solid[cell])
		{
			SimMessages.Dig(cell, -1, false);
		}
	}

	// Token: 0x06004B2F RID: 19247 RVA: 0x001A4F0F File Offset: 0x001A310F
	public void OnSolidStateChanged(int cell)
	{
		Grid.Damage[cell] = 0f;
	}

	// Token: 0x06004B30 RID: 19248 RVA: 0x001A4F20 File Offset: 0x001A3120
	public void OnDigComplete(int cell, float mass, float temperature, ushort element_idx, byte disease_idx, int disease_count)
	{
		Vector3 vector = Grid.CellToPos(cell, CellAlignment.RandomInternal, Grid.SceneLayer.Ore);
		Element element = ElementLoader.elements[(int)element_idx];
		Grid.Damage[cell] = 0f;
		WorldDamage.Instance.PlaySoundForSubstance(element, vector);
		float num = mass * 0.5f;
		if (num <= 0f)
		{
			return;
		}
		GameObject gameObject = element.substance.SpawnResource(vector, num, temperature, disease_idx, disease_count, false, false, false);
		Pickupable component = gameObject.GetComponent<Pickupable>();
		if (component != null && component.GetMyWorld() != null && component.GetMyWorld().worldInventory.IsReachable(component))
		{
			PopFXManager.Instance.SpawnFX(PopFXManager.Instance.sprite_Resource, Mathf.RoundToInt(num).ToString() + " " + element.name, gameObject.transform, 1.5f, false);
		}
	}

	// Token: 0x06004B31 RID: 19249 RVA: 0x001A4FFC File Offset: 0x001A31FC
	private void PlaySoundForSubstance(Element element, Vector3 pos)
	{
		string text = element.substance.GetMiningBreakSound();
		if (text == null)
		{
			if (element.HasTag(GameTags.RefinedMetal))
			{
				text = "RefinedMetal";
			}
			else if (element.HasTag(GameTags.Metal))
			{
				text = "RawMetal";
			}
			else
			{
				text = "Rock";
			}
		}
		text = "Break_" + text;
		text = GlobalAssets.GetSound(text, false);
		if (CameraController.Instance && CameraController.Instance.IsAudibleSound(pos, text))
		{
			KFMOD.PlayOneShot(text, CameraController.Instance.GetVerticallyScaledPosition(pos, false), 1f);
		}
	}

	// Token: 0x04003146 RID: 12614
	public KBatchedAnimController leakEffect;

	// Token: 0x04003147 RID: 12615
	[SerializeField]
	private FMODAsset leakSound;

	// Token: 0x04003148 RID: 12616
	[SerializeField]
	private EventReference leakSoundMigrated;

	// Token: 0x04003149 RID: 12617
	private float damageAmount = 0.00083333335f;

	// Token: 0x0400314B RID: 12619
	private const float SPAWN_DELAY = 1f;

	// Token: 0x0400314C RID: 12620
	private Dictionary<int, float> spawnTimes = new Dictionary<int, float>();

	// Token: 0x0400314D RID: 12621
	private List<int> expiredCells = new List<int>();
}
