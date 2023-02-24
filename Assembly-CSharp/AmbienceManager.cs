using System;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000430 RID: 1072
[AddComponentMenu("KMonoBehaviour/scripts/AmbienceManager")]
public class AmbienceManager : KMonoBehaviour
{
	// Token: 0x06001719 RID: 5913 RVA: 0x00077E9C File Offset: 0x0007609C
	protected override void OnSpawn()
	{
		if (!RuntimeManager.IsInitialized)
		{
			base.enabled = false;
			return;
		}
		for (int i = 0; i < this.quadrants.Length; i++)
		{
			this.quadrants[i] = new AmbienceManager.Quadrant(this.quadrantDefs[i]);
		}
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x00077EE0 File Offset: 0x000760E0
	protected override void OnForcedCleanUp()
	{
		AmbienceManager.Quadrant[] array = this.quadrants;
		for (int i = 0; i < array.Length; i++)
		{
			foreach (AmbienceManager.Layer layer in array[i].GetAllLayers())
			{
				layer.Stop();
			}
		}
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x00077F48 File Offset: 0x00076148
	private void LateUpdate()
	{
		GridArea visibleArea = GridVisibleArea.GetVisibleArea();
		Vector2I min = visibleArea.Min;
		Vector2I max = visibleArea.Max;
		Vector2I vector2I = min + (max - min) / 2;
		Vector3 vector = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.transform.GetPosition().z));
		Vector3 vector2 = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, Camera.main.transform.GetPosition().z));
		Vector3 vector3 = vector2 + (vector - vector2) / 2f;
		Vector3 vector4 = vector - vector2;
		if (vector4.x > vector4.y)
		{
			vector4.y = vector4.x;
		}
		else
		{
			vector4.x = vector4.y;
		}
		vector = vector3 + vector4 / 2f;
		vector2 = vector3 - vector4 / 2f;
		Vector3 vector5 = vector4 / 2f / 2f;
		this.quadrants[0].Update(new Vector2I(min.x, min.y), new Vector2I(vector2I.x, vector2I.y), new Vector3(vector2.x + vector5.x, vector2.y + vector5.y, this.emitterZPosition));
		this.quadrants[1].Update(new Vector2I(vector2I.x, min.y), new Vector2I(max.x, vector2I.y), new Vector3(vector3.x + vector5.x, vector2.y + vector5.y, this.emitterZPosition));
		this.quadrants[2].Update(new Vector2I(min.x, vector2I.y), new Vector2I(vector2I.x, max.y), new Vector3(vector2.x + vector5.x, vector3.y + vector5.y, this.emitterZPosition));
		this.quadrants[3].Update(new Vector2I(vector2I.x, vector2I.y), new Vector2I(max.x, max.y), new Vector3(vector3.x + vector5.x, vector3.y + vector5.y, this.emitterZPosition));
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		for (int i = 0; i < this.quadrants.Length; i++)
		{
			num += (float)this.quadrants[i].spaceLayer.tileCount;
			num2 += (float)this.quadrants[i].facilityLayer.tileCount;
			num3 += (float)this.quadrants[i].totalTileCount;
		}
		AudioMixer.instance.UpdateSpaceVisibleSnapshot(num / num3);
		AudioMixer.instance.UpdateFacilityVisibleSnapshot(num2 / num3);
	}

	// Token: 0x04000CC8 RID: 3272
	private float emitterZPosition;

	// Token: 0x04000CC9 RID: 3273
	public AmbienceManager.QuadrantDef[] quadrantDefs;

	// Token: 0x04000CCA RID: 3274
	public AmbienceManager.Quadrant[] quadrants = new AmbienceManager.Quadrant[4];

	// Token: 0x0200104F RID: 4175
	public class Tuning : TuningData<AmbienceManager.Tuning>
	{
		// Token: 0x040056F7 RID: 22263
		public int backwallTileValue = 1;

		// Token: 0x040056F8 RID: 22264
		public int foundationTileValue = 2;

		// Token: 0x040056F9 RID: 22265
		public int buildingTileValue = 3;
	}

	// Token: 0x02001050 RID: 4176
	public class Layer : IComparable<AmbienceManager.Layer>
	{
		// Token: 0x060072A8 RID: 29352 RVA: 0x002AE0E6 File Offset: 0x002AC2E6
		public Layer(EventReference sound, EventReference one_shot_sound = default(EventReference))
		{
			this.sound = sound;
			this.oneShotSound = one_shot_sound;
		}

		// Token: 0x060072A9 RID: 29353 RVA: 0x002AE0FC File Offset: 0x002AC2FC
		public void Reset()
		{
			this.tileCount = 0;
			this.averageTemperature = 0f;
			this.averageRadiation = 0f;
		}

		// Token: 0x060072AA RID: 29354 RVA: 0x002AE11B File Offset: 0x002AC31B
		public void UpdatePercentage(int cell_count)
		{
			this.tilePercentage = (float)this.tileCount / (float)cell_count;
		}

		// Token: 0x060072AB RID: 29355 RVA: 0x002AE12D File Offset: 0x002AC32D
		public void UpdateAverageTemperature()
		{
			this.averageTemperature /= (float)this.tileCount;
			this.soundEvent.setParameterByName("averageTemperature", this.averageTemperature, false);
		}

		// Token: 0x060072AC RID: 29356 RVA: 0x002AE15B File Offset: 0x002AC35B
		public void UpdateAverageRadiation()
		{
			this.averageRadiation = ((this.tileCount > 0) ? (this.averageRadiation / (float)this.tileCount) : 0f);
			this.soundEvent.setParameterByName("averageRadiation", this.averageRadiation, false);
		}

		// Token: 0x060072AD RID: 29357 RVA: 0x002AE19C File Offset: 0x002AC39C
		public void UpdateParameters(Vector3 emitter_position)
		{
			if (!this.soundEvent.isValid())
			{
				return;
			}
			Vector3 vector = new Vector3(emitter_position.x, emitter_position.y, 0f);
			this.soundEvent.set3DAttributes(vector.To3DAttributes());
			this.soundEvent.setParameterByName("tilePercentage", this.tilePercentage, false);
		}

		// Token: 0x060072AE RID: 29358 RVA: 0x002AE1F9 File Offset: 0x002AC3F9
		public int CompareTo(AmbienceManager.Layer layer)
		{
			return layer.tileCount - this.tileCount;
		}

		// Token: 0x060072AF RID: 29359 RVA: 0x002AE208 File Offset: 0x002AC408
		public void Stop()
		{
			if (this.soundEvent.isValid())
			{
				this.soundEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
				this.soundEvent.release();
			}
			this.isRunning = false;
		}

		// Token: 0x060072B0 RID: 29360 RVA: 0x002AE238 File Offset: 0x002AC438
		public void Start(Vector3 emitter_position)
		{
			if (!this.isRunning)
			{
				if (!this.oneShotSound.IsNull)
				{
					EventInstance eventInstance = KFMOD.CreateInstance(this.oneShotSound);
					if (!eventInstance.isValid())
					{
						string text = "Could not find event: ";
						EventReference eventReference = this.oneShotSound;
						global::Debug.LogWarning(text + eventReference.ToString());
						return;
					}
					ATTRIBUTES_3D attributes_3D = new Vector3(emitter_position.x, emitter_position.y, 0f).To3DAttributes();
					eventInstance.set3DAttributes(attributes_3D);
					eventInstance.setVolume(this.tilePercentage * 2f);
					eventInstance.start();
					eventInstance.release();
					return;
				}
				else
				{
					this.soundEvent = KFMOD.CreateInstance(this.sound);
					if (this.soundEvent.isValid())
					{
						this.soundEvent.start();
					}
					this.isRunning = true;
				}
			}
		}

		// Token: 0x040056FA RID: 22266
		private const string TILE_PERCENTAGE_ID = "tilePercentage";

		// Token: 0x040056FB RID: 22267
		private const string AVERAGE_TEMPERATURE_ID = "averageTemperature";

		// Token: 0x040056FC RID: 22268
		private const string AVERAGE_RADIATION_ID = "averageRadiation";

		// Token: 0x040056FD RID: 22269
		public EventReference sound;

		// Token: 0x040056FE RID: 22270
		public EventReference oneShotSound;

		// Token: 0x040056FF RID: 22271
		public int tileCount;

		// Token: 0x04005700 RID: 22272
		public float tilePercentage;

		// Token: 0x04005701 RID: 22273
		public float volume;

		// Token: 0x04005702 RID: 22274
		public bool isRunning;

		// Token: 0x04005703 RID: 22275
		private EventInstance soundEvent;

		// Token: 0x04005704 RID: 22276
		public float averageTemperature;

		// Token: 0x04005705 RID: 22277
		public float averageRadiation;
	}

	// Token: 0x02001051 RID: 4177
	[Serializable]
	public class QuadrantDef
	{
		// Token: 0x04005706 RID: 22278
		public string name;

		// Token: 0x04005707 RID: 22279
		public EventReference[] liquidSounds;

		// Token: 0x04005708 RID: 22280
		public EventReference[] gasSounds;

		// Token: 0x04005709 RID: 22281
		public EventReference[] solidSounds;

		// Token: 0x0400570A RID: 22282
		public EventReference fogSound;

		// Token: 0x0400570B RID: 22283
		public EventReference spaceSound;

		// Token: 0x0400570C RID: 22284
		public EventReference facilitySound;

		// Token: 0x0400570D RID: 22285
		public EventReference radiationSound;
	}

	// Token: 0x02001052 RID: 4178
	public class Quadrant
	{
		// Token: 0x060072B2 RID: 29362 RVA: 0x002AE31C File Offset: 0x002AC51C
		public Quadrant(AmbienceManager.QuadrantDef def)
		{
			this.name = def.name;
			this.fogLayer = new AmbienceManager.Layer(def.fogSound, default(EventReference));
			this.allLayers.Add(this.fogLayer);
			this.loopingLayers.Add(this.fogLayer);
			this.spaceLayer = new AmbienceManager.Layer(def.spaceSound, default(EventReference));
			this.allLayers.Add(this.spaceLayer);
			this.loopingLayers.Add(this.spaceLayer);
			this.facilityLayer = new AmbienceManager.Layer(def.facilitySound, default(EventReference));
			this.allLayers.Add(this.facilityLayer);
			this.loopingLayers.Add(this.facilityLayer);
			this.m_isRadiationEnabled = Sim.IsRadiationEnabled();
			if (this.m_isRadiationEnabled)
			{
				this.radiationLayer = new AmbienceManager.Layer(def.radiationSound, default(EventReference));
				this.allLayers.Add(this.radiationLayer);
			}
			for (int i = 0; i < 4; i++)
			{
				this.gasLayers[i] = new AmbienceManager.Layer(def.gasSounds[i], default(EventReference));
				this.liquidLayers[i] = new AmbienceManager.Layer(def.liquidSounds[i], default(EventReference));
				this.allLayers.Add(this.gasLayers[i]);
				this.allLayers.Add(this.liquidLayers[i]);
				this.loopingLayers.Add(this.gasLayers[i]);
				this.loopingLayers.Add(this.liquidLayers[i]);
			}
			for (int j = 0; j < this.solidLayers.Length; j++)
			{
				if (j >= def.solidSounds.Length)
				{
					string text = "Missing solid layer: ";
					SolidAmbienceType solidAmbienceType = (SolidAmbienceType)j;
					global::Debug.LogError(text + solidAmbienceType.ToString());
				}
				this.solidLayers[j] = new AmbienceManager.Layer(default(EventReference), def.solidSounds[j]);
				this.allLayers.Add(this.solidLayers[j]);
				this.oneShotLayers.Add(this.solidLayers[j]);
			}
			this.solidTimers = new AmbienceManager.Quadrant.SolidTimer[AmbienceManager.Quadrant.activeSolidLayerCount];
			for (int k = 0; k < AmbienceManager.Quadrant.activeSolidLayerCount; k++)
			{
				this.solidTimers[k] = new AmbienceManager.Quadrant.SolidTimer();
			}
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x002AE5D8 File Offset: 0x002AC7D8
		public void Update(Vector2I min, Vector2I max, Vector3 emitter_position)
		{
			this.emitterPosition = emitter_position;
			this.totalTileCount = 0;
			for (int i = 0; i < this.allLayers.Count; i++)
			{
				this.allLayers[i].Reset();
			}
			for (int j = min.y; j < max.y; j++)
			{
				if (j % 2 != 1)
				{
					for (int k = min.x; k < max.x; k++)
					{
						if (k % 2 != 0)
						{
							int num = Grid.XYToCell(k, j);
							if (Grid.IsValidCell(num))
							{
								this.totalTileCount++;
								if (Grid.IsVisible(num))
								{
									if (Grid.GravitasFacility[num])
									{
										this.facilityLayer.tileCount += 8;
									}
									else
									{
										Element element = Grid.Element[num];
										if (element != null)
										{
											if (element.IsLiquid && Grid.IsSubstantialLiquid(num, 0.35f))
											{
												AmbienceType ambience = element.substance.GetAmbience();
												if (ambience != AmbienceType.None)
												{
													this.liquidLayers[(int)ambience].tileCount++;
													this.liquidLayers[(int)ambience].averageTemperature += Grid.Temperature[num];
												}
											}
											else if (element.IsGas)
											{
												AmbienceType ambience2 = element.substance.GetAmbience();
												if (ambience2 != AmbienceType.None)
												{
													this.gasLayers[(int)ambience2].tileCount++;
													this.gasLayers[(int)ambience2].averageTemperature += Grid.Temperature[num];
												}
											}
											else if (element.IsSolid)
											{
												SolidAmbienceType solidAmbienceType = element.substance.GetSolidAmbience();
												if (Grid.Foundation[num])
												{
													solidAmbienceType = SolidAmbienceType.Tile;
													this.solidLayers[(int)solidAmbienceType].tileCount += TuningData<AmbienceManager.Tuning>.Get().foundationTileValue;
													this.spaceLayer.tileCount -= TuningData<AmbienceManager.Tuning>.Get().foundationTileValue;
												}
												else if (Grid.Objects[num, 2] != null)
												{
													solidAmbienceType = SolidAmbienceType.Tile;
													this.solidLayers[(int)solidAmbienceType].tileCount += TuningData<AmbienceManager.Tuning>.Get().backwallTileValue;
													this.spaceLayer.tileCount -= TuningData<AmbienceManager.Tuning>.Get().backwallTileValue;
												}
												else if (solidAmbienceType != SolidAmbienceType.None)
												{
													this.solidLayers[(int)solidAmbienceType].tileCount++;
												}
												else if (element.id == SimHashes.Regolith || element.id == SimHashes.MaficRock)
												{
													this.spaceLayer.tileCount++;
												}
											}
											else if (element.id == SimHashes.Vacuum && CellSelectionObject.IsExposedToSpace(num))
											{
												if (Grid.Objects[num, 1] != null)
												{
													this.spaceLayer.tileCount -= TuningData<AmbienceManager.Tuning>.Get().buildingTileValue;
												}
												this.spaceLayer.tileCount++;
											}
										}
									}
									if (Grid.Radiation[num] > 0f)
									{
										this.radiationLayer.averageRadiation += Grid.Radiation[num];
										this.radiationLayer.tileCount++;
									}
								}
								else
								{
									this.fogLayer.tileCount++;
								}
							}
						}
					}
				}
			}
			Vector2I vector2I = max - min;
			int num2 = vector2I.x * vector2I.y;
			for (int l = 0; l < this.allLayers.Count; l++)
			{
				this.allLayers[l].UpdatePercentage(num2);
			}
			this.loopingLayers.Sort();
			this.topLayers.Clear();
			for (int m = 0; m < this.loopingLayers.Count; m++)
			{
				AmbienceManager.Layer layer = this.loopingLayers[m];
				if (m < 3 && layer.tilePercentage > 0f)
				{
					layer.Start(emitter_position);
					layer.UpdateAverageTemperature();
					layer.UpdateParameters(emitter_position);
					this.topLayers.Add(layer);
				}
				else
				{
					layer.Stop();
				}
			}
			if (this.m_isRadiationEnabled)
			{
				this.radiationLayer.Start(emitter_position);
				this.radiationLayer.UpdateAverageRadiation();
				this.radiationLayer.UpdateParameters(emitter_position);
			}
			this.oneShotLayers.Sort();
			for (int n = 0; n < AmbienceManager.Quadrant.activeSolidLayerCount; n++)
			{
				if (this.solidTimers[n].ShouldPlay() && this.oneShotLayers[n].tilePercentage > 0f)
				{
					this.oneShotLayers[n].Start(emitter_position);
				}
			}
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x002AEA9F File Offset: 0x002ACC9F
		public List<AmbienceManager.Layer> GetAllLayers()
		{
			return this.allLayers;
		}

		// Token: 0x0400570E RID: 22286
		public string name;

		// Token: 0x0400570F RID: 22287
		public Vector3 emitterPosition;

		// Token: 0x04005710 RID: 22288
		public AmbienceManager.Layer[] gasLayers = new AmbienceManager.Layer[4];

		// Token: 0x04005711 RID: 22289
		public AmbienceManager.Layer[] liquidLayers = new AmbienceManager.Layer[4];

		// Token: 0x04005712 RID: 22290
		public AmbienceManager.Layer fogLayer;

		// Token: 0x04005713 RID: 22291
		public AmbienceManager.Layer spaceLayer;

		// Token: 0x04005714 RID: 22292
		public AmbienceManager.Layer facilityLayer;

		// Token: 0x04005715 RID: 22293
		public AmbienceManager.Layer radiationLayer;

		// Token: 0x04005716 RID: 22294
		public AmbienceManager.Layer[] solidLayers = new AmbienceManager.Layer[16];

		// Token: 0x04005717 RID: 22295
		private List<AmbienceManager.Layer> allLayers = new List<AmbienceManager.Layer>();

		// Token: 0x04005718 RID: 22296
		private List<AmbienceManager.Layer> loopingLayers = new List<AmbienceManager.Layer>();

		// Token: 0x04005719 RID: 22297
		private List<AmbienceManager.Layer> oneShotLayers = new List<AmbienceManager.Layer>();

		// Token: 0x0400571A RID: 22298
		private List<AmbienceManager.Layer> topLayers = new List<AmbienceManager.Layer>();

		// Token: 0x0400571B RID: 22299
		public static int activeSolidLayerCount = 2;

		// Token: 0x0400571C RID: 22300
		public int totalTileCount;

		// Token: 0x0400571D RID: 22301
		private bool m_isRadiationEnabled;

		// Token: 0x0400571E RID: 22302
		private AmbienceManager.Quadrant.SolidTimer[] solidTimers;

		// Token: 0x02001F66 RID: 8038
		public class SolidTimer
		{
			// Token: 0x06009EC8 RID: 40648 RVA: 0x0033F375 File Offset: 0x0033D575
			public SolidTimer()
			{
				this.solidTargetTime = Time.unscaledTime + UnityEngine.Random.value * AmbienceManager.Quadrant.SolidTimer.solidMinTime;
			}

			// Token: 0x06009EC9 RID: 40649 RVA: 0x0033F394 File Offset: 0x0033D594
			public bool ShouldPlay()
			{
				if (Time.unscaledTime > this.solidTargetTime)
				{
					this.solidTargetTime = Time.unscaledTime + AmbienceManager.Quadrant.SolidTimer.solidMinTime + UnityEngine.Random.value * (AmbienceManager.Quadrant.SolidTimer.solidMaxTime - AmbienceManager.Quadrant.SolidTimer.solidMinTime);
					return true;
				}
				return false;
			}

			// Token: 0x04008BA6 RID: 35750
			public static float solidMinTime = 9f;

			// Token: 0x04008BA7 RID: 35751
			public static float solidMaxTime = 15f;

			// Token: 0x04008BA8 RID: 35752
			public float solidTargetTime;
		}
	}
}
