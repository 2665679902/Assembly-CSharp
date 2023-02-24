using System;
using System.Collections.Generic;
using Klei;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

// Token: 0x020008A4 RID: 2212
[AddComponentMenu("KMonoBehaviour/scripts/PropertyTextures")]
public class PropertyTextures : KMonoBehaviour, ISim200ms
{
	// Token: 0x06003F93 RID: 16275 RVA: 0x00162BFC File Offset: 0x00160DFC
	public static void DestroyInstance()
	{
		ShaderReloader.Unregister(new System.Action(PropertyTextures.instance.OnShadersReloaded));
		PropertyTextures.externalFlowTex = IntPtr.Zero;
		PropertyTextures.externalLiquidTex = IntPtr.Zero;
		PropertyTextures.externalExposedToSunlight = IntPtr.Zero;
		PropertyTextures.externalSolidDigAmountTex = IntPtr.Zero;
		PropertyTextures.instance = null;
	}

	// Token: 0x06003F94 RID: 16276 RVA: 0x00162C4C File Offset: 0x00160E4C
	protected override void OnPrefabInit()
	{
		PropertyTextures.instance = this;
		base.OnPrefabInit();
		ShaderReloader.Register(new System.Action(this.OnShadersReloaded));
	}

	// Token: 0x1700046F RID: 1135
	// (get) Token: 0x06003F95 RID: 16277 RVA: 0x00162C6B File Offset: 0x00160E6B
	public static bool IsFogOfWarEnabled
	{
		get
		{
			return PropertyTextures.FogOfWarScale < 1f;
		}
	}

	// Token: 0x06003F96 RID: 16278 RVA: 0x00162C79 File Offset: 0x00160E79
	public Texture GetTexture(PropertyTextures.Property property)
	{
		return this.textureBuffers[(int)property].texture;
	}

	// Token: 0x06003F97 RID: 16279 RVA: 0x00162C88 File Offset: 0x00160E88
	private string GetShaderPropertyName(PropertyTextures.Property property)
	{
		return "_" + property.ToString() + "Tex";
	}

	// Token: 0x06003F98 RID: 16280 RVA: 0x00162CA8 File Offset: 0x00160EA8
	protected override void OnSpawn()
	{
		if (GenericGameSettings.instance.disableFogOfWar)
		{
			PropertyTextures.FogOfWarScale = 1f;
		}
		this.WorldSizeID = Shader.PropertyToID("_WorldSizeInfo");
		this.ClusterWorldSizeID = Shader.PropertyToID("_ClusterWorldSizeInfo");
		this.FogOfWarScaleID = Shader.PropertyToID("_FogOfWarScale");
		this.PropTexWsToCsID = Shader.PropertyToID("_PropTexWsToCs");
		this.PropTexCsToWsID = Shader.PropertyToID("_PropTexCsToWs");
		this.TopBorderHeightID = Shader.PropertyToID("_TopBorderHeight");
	}

	// Token: 0x06003F99 RID: 16281 RVA: 0x00162D2C File Offset: 0x00160F2C
	public void OnReset(object data = null)
	{
		this.lerpers = new TextureLerper[14];
		this.texturePagePool = new TexturePagePool();
		this.textureBuffers = new TextureBuffer[14];
		this.externallyUpdatedTextures = new Texture2D[14];
		for (int i = 0; i < 14; i++)
		{
			PropertyTextures.TextureProperties textureProperties = new PropertyTextures.TextureProperties
			{
				textureFormat = TextureFormat.Alpha8,
				filterMode = FilterMode.Bilinear,
				blend = false,
				blendSpeed = 1f
			};
			for (int j = 0; j < this.textureProperties.Length; j++)
			{
				if (i == (int)this.textureProperties[j].simProperty)
				{
					textureProperties = this.textureProperties[j];
				}
			}
			PropertyTextures.Property property = (PropertyTextures.Property)i;
			textureProperties.name = property.ToString();
			if (this.externallyUpdatedTextures[i] != null)
			{
				UnityEngine.Object.Destroy(this.externallyUpdatedTextures[i]);
				this.externallyUpdatedTextures[i] = null;
			}
			Texture texture;
			if (textureProperties.updatedExternally)
			{
				this.externallyUpdatedTextures[i] = new Texture2D(Grid.WidthInCells, Grid.HeightInCells, TextureUtil.TextureFormatToGraphicsFormat(textureProperties.textureFormat), TextureCreationFlags.None);
				texture = this.externallyUpdatedTextures[i];
			}
			else
			{
				TextureBuffer[] array = this.textureBuffers;
				int num = i;
				property = (PropertyTextures.Property)i;
				array[num] = new TextureBuffer(property.ToString(), Grid.WidthInCells, Grid.HeightInCells, textureProperties.textureFormat, textureProperties.filterMode, this.texturePagePool);
				texture = this.textureBuffers[i].texture;
			}
			if (textureProperties.blend)
			{
				TextureLerper[] array2 = this.lerpers;
				int num2 = i;
				Texture texture2 = texture;
				property = (PropertyTextures.Property)i;
				array2[num2] = new TextureLerper(texture2, property.ToString(), texture.filterMode, textureProperties.textureFormat);
				this.lerpers[i].Speed = textureProperties.blendSpeed;
			}
			string shaderPropertyName = this.GetShaderPropertyName((PropertyTextures.Property)i);
			texture.name = shaderPropertyName;
			textureProperties.texturePropertyName = shaderPropertyName;
			Shader.SetGlobalTexture(shaderPropertyName, texture);
			this.allTextureProperties.Add(textureProperties);
		}
	}

	// Token: 0x06003F9A RID: 16282 RVA: 0x00162F10 File Offset: 0x00161110
	private void OnShadersReloaded()
	{
		for (int i = 0; i < 14; i++)
		{
			TextureLerper textureLerper = this.lerpers[i];
			if (textureLerper != null)
			{
				Shader.SetGlobalTexture(this.allTextureProperties[i].texturePropertyName, textureLerper.Update());
			}
		}
	}

	// Token: 0x06003F9B RID: 16283 RVA: 0x00162F54 File Offset: 0x00161154
	public void Sim200ms(float dt)
	{
		if (this.lerpers == null || this.lerpers.Length == 0)
		{
			return;
		}
		for (int i = 0; i < this.lerpers.Length; i++)
		{
			TextureLerper textureLerper = this.lerpers[i];
			if (textureLerper != null)
			{
				textureLerper.LongUpdate(dt);
			}
		}
	}

	// Token: 0x06003F9C RID: 16284 RVA: 0x00162F9C File Offset: 0x0016119C
	private void UpdateTextureThreaded(TextureRegion texture_region, int x0, int y0, int x1, int y1, PropertyTextures.WorkItem.Callback update_texture_cb)
	{
		this.workItems.Reset(null);
		int num = 16;
		for (int i = y0; i <= y1; i += num)
		{
			int num2 = Math.Min(i + num - 1, y1);
			this.workItems.Add(new PropertyTextures.WorkItem(texture_region, x0, i, x1, num2, update_texture_cb));
		}
		GlobalJobManager.Run(this.workItems);
	}

	// Token: 0x06003F9D RID: 16285 RVA: 0x00162FF8 File Offset: 0x001611F8
	private void UpdateProperty(ref PropertyTextures.TextureProperties p, int x0, int y0, int x1, int y1)
	{
		if (Game.Instance == null || Game.Instance.IsLoading())
		{
			return;
		}
		int simProperty = (int)p.simProperty;
		if (!p.updatedExternally)
		{
			TextureRegion textureRegion = this.textureBuffers[simProperty].Lock(x0, y0, x1 - x0 + 1, y1 - y0 + 1);
			switch (p.simProperty)
			{
			case PropertyTextures.Property.StateChange:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateStateChange));
				break;
			case PropertyTextures.Property.GasPressure:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdatePressure));
				break;
			case PropertyTextures.Property.GasColour:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateGasColour));
				break;
			case PropertyTextures.Property.GasDanger:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateDanger));
				break;
			case PropertyTextures.Property.FogOfWar:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateFogOfWar));
				break;
			case PropertyTextures.Property.SolidDigAmount:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateSolidDigAmount));
				break;
			case PropertyTextures.Property.SolidLiquidGasMass:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateSolidLiquidGasMass));
				break;
			case PropertyTextures.Property.WorldLight:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateWorldLight));
				break;
			case PropertyTextures.Property.Temperature:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateTemperature));
				break;
			case PropertyTextures.Property.FallingSolid:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateFallingSolidChange));
				break;
			case PropertyTextures.Property.Radiation:
				this.UpdateTextureThreaded(textureRegion, x0, y0, x1, y1, new PropertyTextures.WorkItem.Callback(PropertyTextures.UpdateRadiation));
				break;
			}
			textureRegion.Unlock();
			return;
		}
		PropertyTextures.Property simProperty2 = p.simProperty;
		if (simProperty2 != PropertyTextures.Property.Flow)
		{
			if (simProperty2 != PropertyTextures.Property.Liquid)
			{
				if (simProperty2 == PropertyTextures.Property.ExposedToSunlight)
				{
					this.externallyUpdatedTextures[simProperty].LoadRawTextureData(PropertyTextures.externalExposedToSunlight, Grid.WidthInCells * Grid.HeightInCells);
				}
			}
			else
			{
				this.externallyUpdatedTextures[simProperty].LoadRawTextureData(PropertyTextures.externalLiquidTex, 4 * Grid.WidthInCells * Grid.HeightInCells);
			}
		}
		else
		{
			this.externallyUpdatedTextures[simProperty].LoadRawTextureData(PropertyTextures.externalFlowTex, 8 * Grid.WidthInCells * Grid.HeightInCells);
		}
		this.externallyUpdatedTextures[simProperty].Apply();
	}

	// Token: 0x06003F9E RID: 16286 RVA: 0x0016325C File Offset: 0x0016145C
	public static Vector4 CalculateClusterWorldSize()
	{
		WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
		Vector2I worldOffset = activeWorld.WorldOffset;
		Vector2I worldSize = activeWorld.WorldSize;
		Vector4 zero = Vector4.zero;
		if (DlcManager.IsPureVanilla() || (CameraController.Instance != null && CameraController.Instance.ignoreClusterFX))
		{
			zero = new Vector4((float)Grid.WidthInCells, (float)Grid.HeightInCells, 0f, 0f);
		}
		else
		{
			zero = new Vector4((float)worldSize.x, (float)worldSize.y, 1f / (float)(worldSize.x + worldOffset.x), 1f / (float)(worldSize.y + worldOffset.y));
		}
		return zero;
	}

	// Token: 0x06003F9F RID: 16287 RVA: 0x00163304 File Offset: 0x00161504
	private void LateUpdate()
	{
		if (!Grid.IsInitialized())
		{
			return;
		}
		Shader.SetGlobalVector(this.WorldSizeID, new Vector4((float)Grid.WidthInCells, (float)Grid.HeightInCells, 1f / (float)Grid.WidthInCells, 1f / (float)Grid.HeightInCells));
		Vector4 vector = PropertyTextures.CalculateClusterWorldSize();
		Shader.SetGlobalVector(this.ClusterWorldSizeID, vector);
		Shader.SetGlobalVector(this.PropTexWsToCsID, new Vector4(0f, 0f, 1f, 1f));
		Shader.SetGlobalVector(this.PropTexCsToWsID, new Vector4(0f, 0f, 1f, 1f));
		Shader.SetGlobalFloat(this.TopBorderHeightID, ClusterManager.Instance.activeWorld.FullyEnclosedBorder ? 0f : ((float)Grid.TopBorderHeight));
		int num;
		int num2;
		int num3;
		int num4;
		this.GetVisibleCellRange(out num, out num2, out num3, out num4);
		Shader.SetGlobalFloat(this.FogOfWarScaleID, PropertyTextures.FogOfWarScale);
		int num5 = this.NextPropertyIdx;
		this.NextPropertyIdx = num5 + 1;
		int num6 = num5 % this.allTextureProperties.Count;
		PropertyTextures.TextureProperties textureProperties = this.allTextureProperties[num6];
		while (textureProperties.updateEveryFrame)
		{
			num5 = this.NextPropertyIdx;
			this.NextPropertyIdx = num5 + 1;
			num6 = num5 % this.allTextureProperties.Count;
			textureProperties = this.allTextureProperties[num6];
		}
		for (int i = 0; i < this.allTextureProperties.Count; i++)
		{
			PropertyTextures.TextureProperties textureProperties2 = this.allTextureProperties[i];
			if (num6 == i || textureProperties2.updateEveryFrame || GameUtil.IsCapturingTimeLapse())
			{
				this.UpdateProperty(ref textureProperties2, num, num2, num3, num4);
			}
		}
		for (int j = 0; j < 14; j++)
		{
			TextureLerper textureLerper = this.lerpers[j];
			if (textureLerper != null)
			{
				if (Time.timeScale == 0f)
				{
					textureLerper.LongUpdate(Time.unscaledDeltaTime);
				}
				Shader.SetGlobalTexture(this.allTextureProperties[j].texturePropertyName, textureLerper.Update());
			}
		}
	}

	// Token: 0x06003FA0 RID: 16288 RVA: 0x00163500 File Offset: 0x00161700
	private void GetVisibleCellRange(out int x0, out int y0, out int x1, out int y1)
	{
		int num = 16;
		Grid.GetVisibleExtents(out x0, out y0, out x1, out y1);
		int widthInCells = Grid.WidthInCells;
		int heightInCells = Grid.HeightInCells;
		int num2 = 0;
		int num3 = 0;
		x0 = Math.Max(num2, x0 - num);
		y0 = Math.Max(num3, y0 - num);
		x0 = Mathf.Min(x0, widthInCells - 1);
		y0 = Mathf.Min(y0, heightInCells - 1);
		x1 = Mathf.CeilToInt((float)(x1 + num));
		y1 = Mathf.CeilToInt((float)(y1 + num));
		x1 = Mathf.Max(x1, num2);
		y1 = Mathf.Max(y1, num3);
		x1 = Mathf.Min(x1, widthInCells - 1);
		y1 = Mathf.Min(y1, heightInCells - 1);
	}

	// Token: 0x06003FA1 RID: 16289 RVA: 0x001635A8 File Offset: 0x001617A8
	private static void UpdateFogOfWar(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		byte[] visible = Grid.Visible;
		int num = Grid.HeightInCells;
		if (ClusterManager.Instance != null)
		{
			WorldContainer activeWorld = ClusterManager.Instance.activeWorld;
			num = activeWorld.WorldSize.y + activeWorld.WorldOffset.y - 1;
		}
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num2 = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num2))
				{
					int num3 = Grid.XYToCell(j, num);
					if (Grid.IsValidCell(num3))
					{
						region.SetBytes(j, i, visible[num3]);
					}
					else
					{
						region.SetBytes(j, i, 0);
					}
				}
				else
				{
					region.SetBytes(j, i, visible[num2]);
				}
			}
		}
	}

	// Token: 0x06003FA2 RID: 16290 RVA: 0x00163664 File Offset: 0x00161864
	private static void UpdatePressure(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		Vector2 pressureRange = PropertyTextures.instance.PressureRange;
		float minPressureVisibility = PropertyTextures.instance.MinPressureVisibility;
		float num = pressureRange.y - pressureRange.x;
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num2 = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num2))
				{
					region.SetBytes(j, i, 0);
				}
				else
				{
					float num3 = 0f;
					Element element = Grid.Element[num2];
					if (element.IsGas)
					{
						float num4 = Grid.Pressure[num2];
						float num5 = ((num4 > 0f) ? minPressureVisibility : 0f);
						num3 = Mathf.Max(Mathf.Clamp01((num4 - pressureRange.x) / num), num5);
					}
					else if (element.IsLiquid)
					{
						int num6 = Grid.CellAbove(num2);
						if (Grid.IsValidCell(num6) && Grid.Element[num6].IsGas)
						{
							float num7 = Grid.Pressure[num6];
							float num8 = ((num7 > 0f) ? minPressureVisibility : 0f);
							num3 = Mathf.Max(Mathf.Clamp01((num7 - pressureRange.x) / num), num8);
						}
					}
					region.SetBytes(j, i, (byte)(num3 * 255f));
				}
			}
		}
	}

	// Token: 0x06003FA3 RID: 16291 RVA: 0x001637A4 File Offset: 0x001619A4
	private static void UpdateDanger(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num))
				{
					region.SetBytes(j, i, 0);
				}
				else
				{
					byte b = ((Grid.Element[num].id == SimHashes.Oxygen) ? 0 : byte.MaxValue);
					region.SetBytes(j, i, b);
				}
			}
		}
	}

	// Token: 0x06003FA4 RID: 16292 RVA: 0x00163810 File Offset: 0x00161A10
	private static void UpdateStateChange(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		float temperatureStateChangeRange = PropertyTextures.instance.TemperatureStateChangeRange;
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num))
				{
					region.SetBytes(j, i, 0);
				}
				else
				{
					float num2 = 0f;
					Element element = Grid.Element[num];
					if (!element.IsVacuum)
					{
						float num3 = Grid.Temperature[num];
						float num4 = element.lowTemp * temperatureStateChangeRange;
						float num5 = Mathf.Abs(num3 - element.lowTemp) / num4;
						float num6 = element.highTemp * temperatureStateChangeRange;
						float num7 = Mathf.Abs(num3 - element.highTemp) / num6;
						num2 = Mathf.Max(num2, 1f - Mathf.Min(num5, num7));
					}
					region.SetBytes(j, i, (byte)(num2 * 255f));
				}
			}
		}
	}

	// Token: 0x06003FA5 RID: 16293 RVA: 0x001638F8 File Offset: 0x00161AF8
	private static void UpdateFallingSolidChange(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num))
				{
					region.SetBytes(j, i, 0);
				}
				else
				{
					float num2 = 0f;
					Element element = Grid.Element[num];
					if (element.id == SimHashes.Mud || element.id == SimHashes.ToxicMud)
					{
						num2 = 0.65f;
					}
					region.SetBytes(j, i, (byte)(num2 * 255f));
				}
			}
		}
	}

	// Token: 0x06003FA6 RID: 16294 RVA: 0x0016397C File Offset: 0x00161B7C
	private static void UpdateGasColour(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num))
				{
					region.SetBytes(j, i, 0, 0, 0, 0);
				}
				else
				{
					Element element = Grid.Element[num];
					if (element.IsGas)
					{
						region.SetBytes(j, i, element.substance.colour.r, element.substance.colour.g, element.substance.colour.b, byte.MaxValue);
					}
					else if (element.IsLiquid)
					{
						if (Grid.IsValidCell(Grid.CellAbove(num)))
						{
							region.SetBytes(j, i, element.substance.colour.r, element.substance.colour.g, element.substance.colour.b, byte.MaxValue);
						}
						else
						{
							region.SetBytes(j, i, 0, 0, 0, 0);
						}
					}
					else
					{
						region.SetBytes(j, i, 0, 0, 0, 0);
					}
				}
			}
		}
	}

	// Token: 0x06003FA7 RID: 16295 RVA: 0x00163A94 File Offset: 0x00161C94
	private static void UpdateLiquid(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		for (int i = x0; i <= x1; i++)
		{
			int num = Grid.XYToCell(i, y1);
			Element element = Grid.Element[num];
			for (int j = y1; j >= y0; j--)
			{
				int num2 = Grid.XYToCell(i, j);
				if (!Grid.IsActiveWorld(num2))
				{
					region.SetBytes(i, j, 0, 0, 0, 0);
				}
				else
				{
					Element element2 = Grid.Element[num2];
					if (element2.IsLiquid)
					{
						Color32 colour = element2.substance.colour;
						float liquidMaxMass = Lighting.Instance.Settings.LiquidMaxMass;
						float liquidAmountOffset = Lighting.Instance.Settings.LiquidAmountOffset;
						float num3;
						if (element.IsLiquid || element.IsSolid)
						{
							num3 = 1f;
						}
						else
						{
							num3 = liquidAmountOffset + (1f - liquidAmountOffset) * Mathf.Min(Grid.Mass[num2] / liquidMaxMass, 1f);
							num3 = Mathf.Pow(Mathf.Min(Grid.Mass[num2] / liquidMaxMass, 1f), 0.45f);
						}
						region.SetBytes(i, j, (byte)(num3 * 255f), colour.r, colour.g, colour.b);
					}
					else
					{
						region.SetBytes(i, j, 0, 0, 0, 0);
					}
					element = element2;
				}
			}
		}
	}

	// Token: 0x06003FA8 RID: 16296 RVA: 0x00163BE0 File Offset: 0x00161DE0
	private static void UpdateSolidDigAmount(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		ushort elementIndex = ElementLoader.GetElementIndex(SimHashes.Void);
		for (int i = y0; i <= y1; i++)
		{
			int num = Grid.XYToCell(x0, i);
			int num2 = Grid.XYToCell(x1, i);
			int j = num;
			int num3 = x0;
			while (j <= num2)
			{
				byte b = 0;
				byte b2 = 0;
				byte b3 = 0;
				if (Grid.ElementIdx[j] != elementIndex)
				{
					b3 = byte.MaxValue;
				}
				if (Grid.Solid[j])
				{
					b = byte.MaxValue;
					b2 = (byte)(255f * Grid.Damage[j]);
				}
				region.SetBytes(num3, i, b, b2, b3);
				j++;
				num3++;
			}
		}
	}

	// Token: 0x06003FA9 RID: 16297 RVA: 0x00163C7C File Offset: 0x00161E7C
	private static void UpdateSolidLiquidGasMass(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num))
				{
					region.SetBytes(j, i, 0, 0, 0, 0);
				}
				else
				{
					Element element = Grid.Element[num];
					byte b = 0;
					byte b2 = 0;
					byte b3 = 0;
					if (element.IsSolid)
					{
						b = byte.MaxValue;
					}
					else if (element.IsLiquid)
					{
						b2 = byte.MaxValue;
					}
					else if (element.IsGas || element.IsVacuum)
					{
						b3 = byte.MaxValue;
					}
					float num2 = Grid.Mass[num];
					float num3 = Mathf.Min(1f, num2 / 2000f);
					if (num2 > 0f)
					{
						num3 = Mathf.Max(0.003921569f, num3);
					}
					region.SetBytes(j, i, b, b2, b3, (byte)(num3 * 255f));
				}
			}
		}
	}

	// Token: 0x06003FAA RID: 16298 RVA: 0x00163D6C File Offset: 0x00161F6C
	private static void GetTemperatureAlpha(float t, Vector2 cold_range, Vector2 hot_range, out byte cold_alpha, out byte hot_alpha)
	{
		cold_alpha = 0;
		hot_alpha = 0;
		if (t <= cold_range.y)
		{
			float num = Mathf.Clamp01((cold_range.y - t) / (cold_range.y - cold_range.x));
			cold_alpha = (byte)(num * 255f);
			return;
		}
		if (t >= hot_range.x)
		{
			float num2 = Mathf.Clamp01((t - hot_range.x) / (hot_range.y - hot_range.x));
			hot_alpha = (byte)(num2 * 255f);
		}
	}

	// Token: 0x06003FAB RID: 16299 RVA: 0x00163DE0 File Offset: 0x00161FE0
	private static void UpdateTemperature(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		Vector2 vector = PropertyTextures.instance.coldRange;
		Vector2 vector2 = PropertyTextures.instance.hotRange;
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num))
				{
					region.SetBytes(j, i, 0, 0, 0);
				}
				else
				{
					float num2 = Grid.Temperature[num];
					byte b;
					byte b2;
					PropertyTextures.GetTemperatureAlpha(num2, vector, vector2, out b, out b2);
					byte b3 = (byte)(255f * Mathf.Pow(Mathf.Clamp(num2 / 1000f, 0f, 1f), 0.45f));
					region.SetBytes(j, i, b, b2, b3);
				}
			}
		}
	}

	// Token: 0x06003FAC RID: 16300 RVA: 0x00163E98 File Offset: 0x00162098
	private static void UpdateWorldLight(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		if (!PropertyTextures.instance.ForceLightEverywhere)
		{
			for (int i = y0; i <= y1; i++)
			{
				int num = Grid.XYToCell(x0, i);
				int num2 = Grid.XYToCell(x1, i);
				int j = num;
				int num3 = x0;
				while (j <= num2)
				{
					Color32 color = ((Grid.LightCount[j] > 0) ? Lighting.Instance.Settings.LightColour : new Color32(0, 0, 0, byte.MaxValue));
					region.SetBytes(num3, i, color.r, color.g, color.b, (color.r + color.g + color.b > 0) ? byte.MaxValue : 0);
					j++;
					num3++;
				}
			}
			return;
		}
		for (int k = y0; k <= y1; k++)
		{
			for (int l = x0; l <= x1; l++)
			{
				region.SetBytes(l, k, byte.MaxValue, byte.MaxValue, byte.MaxValue);
			}
		}
	}

	// Token: 0x06003FAD RID: 16301 RVA: 0x00163F90 File Offset: 0x00162190
	private static void UpdateRadiation(TextureRegion region, int x0, int y0, int x1, int y1)
	{
		Vector2 vector = PropertyTextures.instance.coldRange;
		Vector2 vector2 = PropertyTextures.instance.hotRange;
		for (int i = y0; i <= y1; i++)
		{
			for (int j = x0; j <= x1; j++)
			{
				int num = Grid.XYToCell(j, i);
				if (!Grid.IsActiveWorld(num))
				{
					region.SetBytes(j, i, 0, 0, 0);
				}
				else
				{
					float num2 = Grid.Radiation[num];
					region.SetBytes(j, i, num2);
				}
			}
		}
	}

	// Token: 0x040029B5 RID: 10677
	[NonSerialized]
	public bool ForceLightEverywhere;

	// Token: 0x040029B6 RID: 10678
	[SerializeField]
	private Vector2 PressureRange = new Vector2(15f, 200f);

	// Token: 0x040029B7 RID: 10679
	[SerializeField]
	private float MinPressureVisibility = 0.1f;

	// Token: 0x040029B8 RID: 10680
	[SerializeField]
	[Range(0f, 1f)]
	private float TemperatureStateChangeRange = 0.05f;

	// Token: 0x040029B9 RID: 10681
	public static PropertyTextures instance;

	// Token: 0x040029BA RID: 10682
	public static IntPtr externalFlowTex;

	// Token: 0x040029BB RID: 10683
	public static IntPtr externalLiquidTex;

	// Token: 0x040029BC RID: 10684
	public static IntPtr externalExposedToSunlight;

	// Token: 0x040029BD RID: 10685
	public static IntPtr externalSolidDigAmountTex;

	// Token: 0x040029BE RID: 10686
	[SerializeField]
	private Vector2 coldRange;

	// Token: 0x040029BF RID: 10687
	[SerializeField]
	private Vector2 hotRange;

	// Token: 0x040029C0 RID: 10688
	public static float FogOfWarScale;

	// Token: 0x040029C1 RID: 10689
	private int WorldSizeID;

	// Token: 0x040029C2 RID: 10690
	private int ClusterWorldSizeID;

	// Token: 0x040029C3 RID: 10691
	private int FogOfWarScaleID;

	// Token: 0x040029C4 RID: 10692
	private int PropTexWsToCsID;

	// Token: 0x040029C5 RID: 10693
	private int PropTexCsToWsID;

	// Token: 0x040029C6 RID: 10694
	private int TopBorderHeightID;

	// Token: 0x040029C7 RID: 10695
	private int NextPropertyIdx;

	// Token: 0x040029C8 RID: 10696
	public TextureBuffer[] textureBuffers;

	// Token: 0x040029C9 RID: 10697
	public TextureLerper[] lerpers;

	// Token: 0x040029CA RID: 10698
	private TexturePagePool texturePagePool;

	// Token: 0x040029CB RID: 10699
	[SerializeField]
	private Texture2D[] externallyUpdatedTextures;

	// Token: 0x040029CC RID: 10700
	private PropertyTextures.TextureProperties[] textureProperties = new PropertyTextures.TextureProperties[]
	{
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.Flow,
			textureFormat = TextureFormat.RGFloat,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = true,
			updatedExternally = true,
			blend = true,
			blendSpeed = 0.25f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.Liquid,
			textureFormat = TextureFormat.RGBA32,
			filterMode = FilterMode.Point,
			updateEveryFrame = true,
			updatedExternally = true,
			blend = true,
			blendSpeed = 1f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.ExposedToSunlight,
			textureFormat = TextureFormat.Alpha8,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = true,
			updatedExternally = true,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.SolidDigAmount,
			textureFormat = TextureFormat.RGB24,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = true,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.GasColour,
			textureFormat = TextureFormat.RGBA32,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = true,
			blendSpeed = 0.25f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.GasDanger,
			textureFormat = TextureFormat.Alpha8,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = true,
			blendSpeed = 0.25f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.GasPressure,
			textureFormat = TextureFormat.Alpha8,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = true,
			blendSpeed = 0.25f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.FogOfWar,
			textureFormat = TextureFormat.Alpha8,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = true,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.WorldLight,
			textureFormat = TextureFormat.RGBA32,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.StateChange,
			textureFormat = TextureFormat.Alpha8,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.FallingSolid,
			textureFormat = TextureFormat.Alpha8,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.SolidLiquidGasMass,
			textureFormat = TextureFormat.RGBA32,
			filterMode = FilterMode.Point,
			updateEveryFrame = true,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.Temperature,
			textureFormat = TextureFormat.RGB24,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		},
		new PropertyTextures.TextureProperties
		{
			simProperty = PropertyTextures.Property.Radiation,
			textureFormat = TextureFormat.RFloat,
			filterMode = FilterMode.Bilinear,
			updateEveryFrame = false,
			updatedExternally = false,
			blend = false,
			blendSpeed = 0f
		}
	};

	// Token: 0x040029CD RID: 10701
	private List<PropertyTextures.TextureProperties> allTextureProperties = new List<PropertyTextures.TextureProperties>();

	// Token: 0x040029CE RID: 10702
	private WorkItemCollection<PropertyTextures.WorkItem, object> workItems = new WorkItemCollection<PropertyTextures.WorkItem, object>();

	// Token: 0x02001678 RID: 5752
	public enum Property
	{
		// Token: 0x040069CF RID: 27087
		StateChange,
		// Token: 0x040069D0 RID: 27088
		GasPressure,
		// Token: 0x040069D1 RID: 27089
		GasColour,
		// Token: 0x040069D2 RID: 27090
		GasDanger,
		// Token: 0x040069D3 RID: 27091
		FogOfWar,
		// Token: 0x040069D4 RID: 27092
		Flow,
		// Token: 0x040069D5 RID: 27093
		SolidDigAmount,
		// Token: 0x040069D6 RID: 27094
		SolidLiquidGasMass,
		// Token: 0x040069D7 RID: 27095
		WorldLight,
		// Token: 0x040069D8 RID: 27096
		Liquid,
		// Token: 0x040069D9 RID: 27097
		Temperature,
		// Token: 0x040069DA RID: 27098
		ExposedToSunlight,
		// Token: 0x040069DB RID: 27099
		FallingSolid,
		// Token: 0x040069DC RID: 27100
		Radiation,
		// Token: 0x040069DD RID: 27101
		Num
	}

	// Token: 0x02001679 RID: 5753
	private struct TextureProperties
	{
		// Token: 0x040069DE RID: 27102
		public string name;

		// Token: 0x040069DF RID: 27103
		public PropertyTextures.Property simProperty;

		// Token: 0x040069E0 RID: 27104
		public TextureFormat textureFormat;

		// Token: 0x040069E1 RID: 27105
		public FilterMode filterMode;

		// Token: 0x040069E2 RID: 27106
		public bool updateEveryFrame;

		// Token: 0x040069E3 RID: 27107
		public bool updatedExternally;

		// Token: 0x040069E4 RID: 27108
		public bool blend;

		// Token: 0x040069E5 RID: 27109
		public float blendSpeed;

		// Token: 0x040069E6 RID: 27110
		public string texturePropertyName;
	}

	// Token: 0x0200167A RID: 5754
	private struct WorkItem : IWorkItem<object>
	{
		// Token: 0x060087D5 RID: 34773 RVA: 0x002F3F7C File Offset: 0x002F217C
		public WorkItem(TextureRegion texture_region, int x0, int y0, int x1, int y1, PropertyTextures.WorkItem.Callback update_texture_cb)
		{
			this.textureRegion = texture_region;
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			this.updateTextureCb = update_texture_cb;
		}

		// Token: 0x060087D6 RID: 34774 RVA: 0x002F3FAB File Offset: 0x002F21AB
		public void Run(object shared_data)
		{
			this.updateTextureCb(this.textureRegion, this.x0, this.y0, this.x1, this.y1);
		}

		// Token: 0x040069E7 RID: 27111
		private int x0;

		// Token: 0x040069E8 RID: 27112
		private int y0;

		// Token: 0x040069E9 RID: 27113
		private int x1;

		// Token: 0x040069EA RID: 27114
		private int y1;

		// Token: 0x040069EB RID: 27115
		private TextureRegion textureRegion;

		// Token: 0x040069EC RID: 27116
		private PropertyTextures.WorkItem.Callback updateTextureCb;

		// Token: 0x020020A1 RID: 8353
		// (Invoke) Token: 0x0600A480 RID: 42112
		public delegate void Callback(TextureRegion texture_region, int x0, int y0, int x1, int y1);
	}
}
