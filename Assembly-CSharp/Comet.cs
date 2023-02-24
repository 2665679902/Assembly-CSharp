using System;
using System.Collections.Generic;
using FMOD.Studio;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020006A1 RID: 1697
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/Comet")]
public class Comet : KMonoBehaviour, ISim33ms
{
	// Token: 0x17000335 RID: 821
	// (get) Token: 0x06002E06 RID: 11782 RVA: 0x000F2003 File Offset: 0x000F0203
	// (set) Token: 0x06002E07 RID: 11783 RVA: 0x000F200B File Offset: 0x000F020B
	public Vector2 Velocity
	{
		get
		{
			return this.velocity;
		}
		set
		{
			this.velocity = value;
		}
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x000F2014 File Offset: 0x000F0214
	private float GetVolume(GameObject gameObject)
	{
		float num = 1f;
		if (gameObject != null && gameObject.GetComponent<KSelectable>() != null && gameObject.GetComponent<KSelectable>().IsSelected)
		{
			num = 1f;
		}
		return num;
	}

	// Token: 0x06002E09 RID: 11785 RVA: 0x000F2054 File Offset: 0x000F0254
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.remainingTileDamage = this.totalTileDamage;
		this.loopingSounds = base.gameObject.GetComponent<LoopingSounds>();
		this.flyingSound = GlobalAssets.GetSound("Meteor_LP", false);
		this.RandomizeVelocity();
	}

	// Token: 0x06002E0A RID: 11786 RVA: 0x000F2090 File Offset: 0x000F0290
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.RandomizeMassAndTemperature();
		this.StartLoopingSound();
	}

	// Token: 0x06002E0B RID: 11787 RVA: 0x000F20A4 File Offset: 0x000F02A4
	public virtual void RandomizeVelocity()
	{
		float num = UnityEngine.Random.Range(this.spawnAngle.x, this.spawnAngle.y);
		float num2 = num * 3.1415927f / 180f;
		float num3 = UnityEngine.Random.Range(this.spawnVelocity.x, this.spawnVelocity.y);
		this.velocity = new Vector2(-Mathf.Cos(num2) * num3, Mathf.Sin(num2) * num3);
		base.GetComponent<KBatchedAnimController>().Rotation = -num - 90f;
	}

	// Token: 0x06002E0C RID: 11788 RVA: 0x000F2128 File Offset: 0x000F0328
	public void RandomizeMassAndTemperature()
	{
		float num = UnityEngine.Random.Range(this.massRange.x, this.massRange.y);
		PrimaryElement component = base.GetComponent<PrimaryElement>();
		component.Mass = num;
		component.Temperature = UnityEngine.Random.Range(this.temperatureRange.x, this.temperatureRange.y);
		if (this.addTiles > 0)
		{
			float num2 = UnityEngine.Random.Range(0.95f, 0.98f);
			this.explosionMass = num * (1f - num2);
			this.addTileMass = num * num2;
			return;
		}
		this.explosionMass = num;
		this.addTileMass = 0f;
	}

	// Token: 0x06002E0D RID: 11789 RVA: 0x000F21C4 File Offset: 0x000F03C4
	[ContextMenu("Explode")]
	private void Explode(Vector3 pos, int cell, int prev_cell, Element element)
	{
		int world = (int)Grid.WorldIdx[cell2];
		this.PlayImpactSound(pos);
		Vector3 vector = pos;
		vector.z = Grid.GetLayerZ(Grid.SceneLayer.FXFront2);
		Game.Instance.SpawnFX(this.explosionEffectHash, vector, 0f);
		Substance substance = element.substance;
		int num = UnityEngine.Random.Range(this.explosionOreCount.x, this.explosionOreCount.y + 1);
		Vector2 vector2 = -this.velocity.normalized;
		Vector2 vector3 = new Vector2(vector2.y, -vector2.x);
		ListPool<ScenePartitionerEntry, Comet>.PooledList pooledList = ListPool<ScenePartitionerEntry, Comet>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)pos.x - 3, (int)pos.y - 3, 6, 6, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			GameObject gameObject = (scenePartitionerEntry.obj as Pickupable).gameObject;
			if (!(gameObject.GetComponent<MinionIdentity>() != null) && gameObject.GetDef<CreatureFallMonitor.Def>() == null)
			{
				Vector2 vector4 = (gameObject.transform.GetPosition() - pos).normalized;
				vector4 += new Vector2(0f, 0.55f);
				vector4 *= 0.5f * UnityEngine.Random.Range(this.explosionSpeedRange.x, this.explosionSpeedRange.y);
				if (GameComps.Fallers.Has(gameObject))
				{
					GameComps.Fallers.Remove(gameObject);
				}
				if (GameComps.Gravities.Has(gameObject))
				{
					GameComps.Gravities.Remove(gameObject);
				}
				GameComps.Fallers.Add(gameObject, vector4);
			}
		}
		pooledList.Recycle();
		int num2 = this.splashRadius + 1;
		for (int i = -num2; i <= num2; i++)
		{
			for (int j = -num2; j <= num2; j++)
			{
				int num3 = Grid.OffsetCell(cell2, j, i);
				if (Grid.IsValidCellInWorld(num3, world) && !this.destroyedCells.Contains(num3))
				{
					float num4 = (1f - (float)Mathf.Abs(j) / (float)num2) * (1f - (float)Mathf.Abs(i) / (float)num2);
					if (num4 > 0f)
					{
						this.DamageTiles(num3, prev_cell, num4 * this.totalTileDamage * 0.5f);
					}
				}
			}
		}
		float num5 = ((num > 0) ? (this.explosionMass / (float)num) : 1f);
		float num6 = UnityEngine.Random.Range(this.explosionTemperatureRange.x, this.explosionTemperatureRange.y);
		for (int k = 0; k < num; k++)
		{
			Vector2 normalized = (vector2 + vector3 * UnityEngine.Random.Range(-1f, 1f)).normalized;
			Vector3 vector5 = normalized * UnityEngine.Random.Range(this.explosionSpeedRange.x, this.explosionSpeedRange.y);
			Vector3 vector6 = normalized.normalized * 0.75f;
			vector6 += new Vector3(0f, 0.55f, 0f);
			vector6 += pos;
			GameObject gameObject2 = substance.SpawnResource(vector6, num5, num6, byte.MaxValue, 0, false, false, false);
			if (GameComps.Fallers.Has(gameObject2))
			{
				GameComps.Fallers.Remove(gameObject2);
			}
			GameComps.Fallers.Add(gameObject2, vector5);
		}
		if (this.addTiles > 0)
		{
			float depthOfElement = (float)this.GetDepthOfElement(cell2, element, world);
			float num7 = 1f;
			float num8 = (depthOfElement - (float)this.addTilesMinHeight) / (float)(this.addTilesMaxHeight - this.addTilesMinHeight);
			if (!float.IsNaN(num8))
			{
				num7 -= num8;
			}
			int num9 = Mathf.Min(this.addTiles, Mathf.Clamp(Mathf.RoundToInt((float)this.addTiles * num7), 1, this.addTiles));
			HashSetPool<int, Comet>.PooledHashSet pooledHashSet = HashSetPool<int, Comet>.Allocate();
			HashSetPool<int, Comet>.PooledHashSet pooledHashSet2 = HashSetPool<int, Comet>.Allocate();
			QueuePool<GameUtil.FloodFillInfo, Comet>.PooledQueue pooledQueue = QueuePool<GameUtil.FloodFillInfo, Comet>.Allocate();
			int num10 = -1;
			int num11 = 1;
			if (this.velocity.x < 0f)
			{
				num10 *= -1;
				num11 *= -1;
			}
			pooledQueue.Enqueue(new GameUtil.FloodFillInfo
			{
				cell = prev_cell,
				depth = 0
			});
			pooledQueue.Enqueue(new GameUtil.FloodFillInfo
			{
				cell = Grid.OffsetCell(prev_cell, new CellOffset(num10, 0)),
				depth = 0
			});
			pooledQueue.Enqueue(new GameUtil.FloodFillInfo
			{
				cell = Grid.OffsetCell(prev_cell, new CellOffset(num11, 0)),
				depth = 0
			});
			Func<int, bool> func = (int cell) => Grid.IsValidCellInWorld(cell, world) && !Grid.Solid[cell];
			GameUtil.FloodFillConditional(pooledQueue, func, pooledHashSet2, pooledHashSet, 10);
			float num12 = ((num9 > 0) ? (this.addTileMass / (float)this.addTiles) : 1f);
			int num13 = this.addDiseaseCount / num9;
			if (element.HasTag(GameTags.Unstable))
			{
				UnstableGroundManager component = World.Instance.GetComponent<UnstableGroundManager>();
				using (HashSet<int>.Enumerator enumerator2 = pooledHashSet.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						int num14 = enumerator2.Current;
						if (num9 <= 0)
						{
							break;
						}
						component.Spawn(num14, element, num12, num6, byte.MaxValue, 0);
						num9--;
					}
					goto IL_5CD;
				}
			}
			foreach (int num15 in pooledHashSet)
			{
				if (num9 <= 0)
				{
					break;
				}
				SimMessages.AddRemoveSubstance(num15, element.id, CellEventLogger.Instance.ElementEmitted, num12, num6, this.diseaseIdx, num13, true, -1);
				num9--;
			}
			IL_5CD:
			pooledHashSet.Recycle();
			pooledHashSet2.Recycle();
			pooledQueue.Recycle();
		}
		this.SpawnCraterPrefabs();
		if (this.OnImpact != null)
		{
			this.OnImpact();
		}
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x000F27F4 File Offset: 0x000F09F4
	protected virtual void SpawnCraterPrefabs()
	{
		if (this.craterPrefabs != null && this.craterPrefabs.Length != 0)
		{
			GameObject gameObject = global::Util.KInstantiate(Assets.GetPrefab(this.craterPrefabs[UnityEngine.Random.Range(0, this.craterPrefabs.Length)]), Grid.CellToPos(Grid.PosToCell(this)));
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -19.5f);
			gameObject.SetActive(true);
		}
	}

	// Token: 0x06002E0F RID: 11791 RVA: 0x000F2880 File Offset: 0x000F0A80
	private int GetDepthOfElement(int cell, Element element, int world)
	{
		int num = 0;
		int num2 = Grid.CellBelow(cell);
		while (Grid.IsValidCellInWorld(num2, world) && Grid.Element[num2] == element)
		{
			num++;
			num2 = Grid.CellBelow(num2);
		}
		return num;
	}

	// Token: 0x06002E10 RID: 11792 RVA: 0x000F28B8 File Offset: 0x000F0AB8
	[ContextMenu("DamageTiles")]
	private float DamageTiles(int cell, int prev_cell, float input_damage)
	{
		GameObject gameObject = Grid.Objects[cell, 9];
		float num = 1f;
		bool flag = false;
		if (gameObject != null)
		{
			if (gameObject.GetComponent<KPrefabID>().HasTag(GameTags.Window))
			{
				num = this.windowDamageMultiplier;
			}
			else if (gameObject.GetComponent<KPrefabID>().HasTag(GameTags.Bunker))
			{
				num = this.bunkerDamageMultiplier;
				if (gameObject.GetComponent<Door>() != null)
				{
					Game.Instance.savedInfo.blockedCometWithBunkerDoor = true;
				}
			}
			SimCellOccupier component = gameObject.GetComponent<SimCellOccupier>();
			if (component != null && !component.doReplaceElement)
			{
				flag = true;
			}
		}
		Element element;
		if (flag)
		{
			element = gameObject.GetComponent<PrimaryElement>().Element;
		}
		else
		{
			element = Grid.Element[cell];
		}
		if (element.strength == 0f)
		{
			return 0f;
		}
		float num2 = input_damage * num / element.strength;
		this.PlayTileDamageSound(element, Grid.CellToPos(cell), gameObject);
		if (num2 == 0f)
		{
			return 0f;
		}
		float num5;
		if (flag)
		{
			BuildingHP component2 = gameObject.GetComponent<BuildingHP>();
			float num3 = (float)component2.HitPoints / (float)component2.MaxHitPoints;
			float num4 = num2 * (float)component2.MaxHitPoints;
			component2.gameObject.Trigger(-794517298, new BuildingHP.DamageSourceInfo
			{
				damage = Mathf.RoundToInt(num4),
				source = BUILDINGS.DAMAGESOURCES.COMET,
				popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.COMET
			});
			num5 = Mathf.Min(num3, num2);
		}
		else
		{
			num5 = WorldDamage.Instance.ApplyDamage(cell, num2, prev_cell, BUILDINGS.DAMAGESOURCES.COMET, UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.COMET);
		}
		this.destroyedCells.Add(cell);
		float num6 = num5 / num2;
		return input_damage * (1f - num6);
	}

	// Token: 0x06002E11 RID: 11793 RVA: 0x000F2A70 File Offset: 0x000F0C70
	private void DamageThings(Vector3 pos, int cell, int damage, GameObject ignoreObject = null)
	{
		if (!Grid.IsValidCell(cell))
		{
			return;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		if (gameObject != null && gameObject != ignoreObject)
		{
			BuildingHP component = gameObject.GetComponent<BuildingHP>();
			Building component2 = gameObject.GetComponent<Building>();
			if (component != null && !this.damagedEntities.Contains(gameObject))
			{
				float num = (gameObject.GetComponent<KPrefabID>().HasTag(GameTags.Bunker) ? ((float)damage * this.bunkerDamageMultiplier) : ((float)damage));
				if (component2 != null && component2.Def != null)
				{
					this.PlayBuildingDamageSound(component2.Def, Grid.CellToPos(cell), gameObject);
				}
				component.gameObject.Trigger(-794517298, new BuildingHP.DamageSourceInfo
				{
					damage = Mathf.RoundToInt(num),
					source = BUILDINGS.DAMAGESOURCES.COMET,
					popString = UI.GAMEOBJECTEFFECTS.DAMAGE_POPS.COMET
				});
				this.damagedEntities.Add(gameObject);
			}
		}
		ListPool<ScenePartitionerEntry, Comet>.PooledList pooledList = ListPool<ScenePartitionerEntry, Comet>.Allocate();
		GameScenePartitioner.Instance.GatherEntries((int)pos.x, (int)pos.y, 1, 1, GameScenePartitioner.Instance.pickupablesLayer, pooledList);
		foreach (ScenePartitionerEntry scenePartitionerEntry in pooledList)
		{
			Pickupable pickupable = scenePartitionerEntry.obj as Pickupable;
			Health component3 = pickupable.GetComponent<Health>();
			if (component3 != null && !this.damagedEntities.Contains(pickupable.gameObject))
			{
				float num2 = (pickupable.GetComponent<KPrefabID>().HasTag(GameTags.Bunker) ? ((float)damage * this.bunkerDamageMultiplier) : ((float)damage));
				component3.Damage(num2);
				this.damagedEntities.Add(pickupable.gameObject);
			}
		}
		pooledList.Recycle();
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x000F2C58 File Offset: 0x000F0E58
	private float GetDistanceFromImpact()
	{
		float num = this.velocity.x / this.velocity.y;
		Vector3 position = base.transform.GetPosition();
		float num2 = 0f;
		while (num2 > -6f)
		{
			num2 -= 1f;
			num2 = Mathf.Ceil(position.y + num2) - 0.2f - position.y;
			float num3 = num2 * num;
			Vector3 vector = new Vector3(num3, num2, 0f);
			int num4 = Grid.PosToCell(position + vector);
			if (Grid.IsValidCell(num4) && Grid.Solid[num4])
			{
				return vector.magnitude;
			}
		}
		return 6f;
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x000F2D01 File Offset: 0x000F0F01
	public float GetSoundDistance()
	{
		return this.GetDistanceFromImpact();
	}

	// Token: 0x06002E14 RID: 11796 RVA: 0x000F2D0C File Offset: 0x000F0F0C
	private void PlayTileDamageSound(Element element, Vector3 pos, GameObject tile_go)
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
		text = "MeteorDamage_" + text;
		text = GlobalAssets.GetSound(text, false);
		if (CameraController.Instance && CameraController.Instance.IsAudibleSound(pos, text))
		{
			float volume = this.GetVolume(tile_go);
			KFMOD.PlayOneShot(text, CameraController.Instance.GetVerticallyScaledPosition(pos, false), volume);
		}
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x000F2DA8 File Offset: 0x000F0FA8
	private void PlayBuildingDamageSound(BuildingDef def, Vector3 pos, GameObject building_go)
	{
		if (def != null)
		{
			string text = GlobalAssets.GetSound(StringFormatter.Combine("MeteorDamage_Building_", def.AudioCategory), false);
			if (text == null)
			{
				text = GlobalAssets.GetSound("MeteorDamage_Building_Metal", false);
			}
			if (text != null && CameraController.Instance && CameraController.Instance.IsAudibleSound(pos, text))
			{
				float volume = this.GetVolume(building_go);
				KFMOD.PlayOneShot(text, CameraController.Instance.GetVerticallyScaledPosition(pos, false), volume);
			}
		}
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x000F2E24 File Offset: 0x000F1024
	public void Sim33ms(float dt)
	{
		if (this.hasExploded)
		{
			return;
		}
		Vector2 vector = new Vector2((float)Grid.WidthInCells, (float)Grid.HeightInCells) * -0.1f;
		Vector2 vector2 = new Vector2((float)Grid.WidthInCells, (float)Grid.HeightInCells) * 1.1f;
		Vector3 position = base.transform.GetPosition();
		Vector3 vector3 = position + new Vector3(this.velocity.x * dt, this.velocity.y * dt, 0f);
		int num = Grid.PosToCell(vector3);
		this.loopingSounds.UpdateVelocity(this.flyingSound, vector3 - position);
		Element element = ElementLoader.FindElementByHash(this.EXHAUST_ELEMENT);
		if (this.EXHAUST_ELEMENT != SimHashes.Void && Grid.IsValidCell(num) && !Grid.Solid[num])
		{
			SimMessages.EmitMass(num, element.idx, dt * this.EXHAUST_RATE, element.defaultValues.temperature, this.diseaseIdx, Mathf.RoundToInt((float)this.addDiseaseCount * dt), -1);
		}
		if (vector3.x < vector.x || vector2.x < vector3.x || vector3.y < vector.y)
		{
			global::Util.KDestroyGameObject(base.gameObject);
		}
		int num2 = Grid.PosToCell(this);
		int num3 = Grid.PosToCell(this.previousPosition);
		if (num2 != num3)
		{
			if (Grid.IsValidCell(num2) && Grid.Solid[num2])
			{
				PrimaryElement component = base.GetComponent<PrimaryElement>();
				this.remainingTileDamage = this.DamageTiles(num2, num3, this.remainingTileDamage);
				if (this.remainingTileDamage <= 0f)
				{
					this.Explode(position, num2, num3, component.Element);
					this.hasExploded = true;
					if (this.destroyOnExplode)
					{
						global::Util.KDestroyGameObject(base.gameObject);
					}
					return;
				}
			}
			else
			{
				GameObject gameObject = ((this.ignoreObstacleForDamage.Get() == null) ? null : this.ignoreObstacleForDamage.Get().gameObject);
				this.DamageThings(position, num2, this.entityDamage, gameObject);
			}
		}
		if (this.canHitDuplicants && this.age > 0.25f && Grid.Objects[Grid.PosToCell(position), 0] != null)
		{
			base.transform.position = Grid.CellToPos(Grid.PosToCell(position));
			this.Explode(position, num2, num3, base.GetComponent<PrimaryElement>().Element);
			if (this.destroyOnExplode)
			{
				global::Util.KDestroyGameObject(base.gameObject);
			}
			return;
		}
		this.previousPosition = position;
		base.transform.SetPosition(vector3);
		this.age += dt;
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x000F30C4 File Offset: 0x000F12C4
	private void PlayImpactSound(Vector3 pos)
	{
		if (this.impactSound == null)
		{
			this.impactSound = "Meteor_Large_Impact";
		}
		this.loopingSounds.StopSound(this.flyingSound);
		string sound = GlobalAssets.GetSound(this.impactSound, false);
		if (CameraController.Instance.IsAudibleSound(pos, sound))
		{
			float volume = this.GetVolume(base.gameObject);
			pos.z = 0f;
			EventInstance eventInstance = KFMOD.BeginOneShot(sound, pos, volume);
			eventInstance.setParameterByName("userVolume_SFX", KPlayerPrefs.GetFloat("Volume_SFX"), false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x000F3156 File Offset: 0x000F1356
	private void StartLoopingSound()
	{
		this.loopingSounds.StartSound(this.flyingSound);
		this.loopingSounds.UpdateFirstParameter(this.flyingSound, this.FLYING_SOUND_ID_PARAMETER, (float)this.flyingSoundID);
	}

	// Token: 0x04001B4A RID: 6986
	public SimHashes EXHAUST_ELEMENT = SimHashes.CarbonDioxide;

	// Token: 0x04001B4B RID: 6987
	public float EXHAUST_RATE = 50f;

	// Token: 0x04001B4C RID: 6988
	public Vector2 spawnVelocity = new Vector2(12f, 15f);

	// Token: 0x04001B4D RID: 6989
	public Vector2 spawnAngle = new Vector2(-100f, -80f);

	// Token: 0x04001B4E RID: 6990
	public Vector2 massRange;

	// Token: 0x04001B4F RID: 6991
	public Vector2 temperatureRange;

	// Token: 0x04001B50 RID: 6992
	public SpawnFXHashes explosionEffectHash;

	// Token: 0x04001B51 RID: 6993
	public int splashRadius = 1;

	// Token: 0x04001B52 RID: 6994
	public int addTiles;

	// Token: 0x04001B53 RID: 6995
	public int addTilesMinHeight;

	// Token: 0x04001B54 RID: 6996
	public int addTilesMaxHeight;

	// Token: 0x04001B55 RID: 6997
	public int entityDamage = 1;

	// Token: 0x04001B56 RID: 6998
	public float totalTileDamage = 0.2f;

	// Token: 0x04001B57 RID: 6999
	private float addTileMass;

	// Token: 0x04001B58 RID: 7000
	public int addDiseaseCount;

	// Token: 0x04001B59 RID: 7001
	public byte diseaseIdx = byte.MaxValue;

	// Token: 0x04001B5A RID: 7002
	public Vector2 elementReplaceTileTemperatureRange = new Vector2(800f, 1000f);

	// Token: 0x04001B5B RID: 7003
	public Vector2I explosionOreCount = new Vector2I(0, 0);

	// Token: 0x04001B5C RID: 7004
	private float explosionMass;

	// Token: 0x04001B5D RID: 7005
	public Vector2 explosionTemperatureRange = new Vector2(500f, 700f);

	// Token: 0x04001B5E RID: 7006
	public Vector2 explosionSpeedRange = new Vector2(8f, 14f);

	// Token: 0x04001B5F RID: 7007
	public float windowDamageMultiplier = 5f;

	// Token: 0x04001B60 RID: 7008
	public float bunkerDamageMultiplier;

	// Token: 0x04001B61 RID: 7009
	public string impactSound;

	// Token: 0x04001B62 RID: 7010
	public string flyingSound;

	// Token: 0x04001B63 RID: 7011
	public int flyingSoundID;

	// Token: 0x04001B64 RID: 7012
	private HashedString FLYING_SOUND_ID_PARAMETER = "meteorType";

	// Token: 0x04001B65 RID: 7013
	[Serialize]
	protected Vector2 velocity;

	// Token: 0x04001B66 RID: 7014
	[Serialize]
	private float remainingTileDamage;

	// Token: 0x04001B67 RID: 7015
	private Vector3 previousPosition;

	// Token: 0x04001B68 RID: 7016
	private bool hasExploded;

	// Token: 0x04001B69 RID: 7017
	public bool canHitDuplicants;

	// Token: 0x04001B6A RID: 7018
	public string[] craterPrefabs;

	// Token: 0x04001B6B RID: 7019
	public bool destroyOnExplode = true;

	// Token: 0x04001B6C RID: 7020
	private float age;

	// Token: 0x04001B6D RID: 7021
	public System.Action OnImpact;

	// Token: 0x04001B6E RID: 7022
	public Ref<KPrefabID> ignoreObstacleForDamage = new Ref<KPrefabID>();

	// Token: 0x04001B6F RID: 7023
	private LoopingSounds loopingSounds;

	// Token: 0x04001B70 RID: 7024
	private List<GameObject> damagedEntities = new List<GameObject>();

	// Token: 0x04001B71 RID: 7025
	private List<int> destroyedCells = new List<int>();

	// Token: 0x04001B72 RID: 7026
	private const float MAX_DISTANCE_TEST = 6f;
}
