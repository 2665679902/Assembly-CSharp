using System;
using System.Collections.Generic;
using Klei;
using Klei.AI;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

// Token: 0x0200091E RID: 2334
[AddComponentMenu("KMonoBehaviour/scripts/SimDebugView")]
public class SimDebugView : KMonoBehaviour
{
	// Token: 0x060043ED RID: 17389 RVA: 0x0017F11F File Offset: 0x0017D31F
	public static void DestroyInstance()
	{
		SimDebugView.Instance = null;
	}

	// Token: 0x060043EE RID: 17390 RVA: 0x0017F127 File Offset: 0x0017D327
	protected override void OnPrefabInit()
	{
		SimDebugView.Instance = this;
		this.material = UnityEngine.Object.Instantiate<Material>(this.material);
		this.diseaseMaterial = UnityEngine.Object.Instantiate<Material>(this.diseaseMaterial);
	}

	// Token: 0x060043EF RID: 17391 RVA: 0x0017F154 File Offset: 0x0017D354
	protected override void OnSpawn()
	{
		SimDebugViewCompositor.Instance.material.SetColor("_Color0", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[0].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color1", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[1].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color2", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[2].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color3", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[3].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color4", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[4].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color5", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[5].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color6", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[6].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color7", GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[7].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color0", GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[0].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color1", GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[1].colorName));
		SimDebugViewCompositor.Instance.material.SetColor("_Color2", GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[2].colorName));
		this.SetMode(global::OverlayModes.None.ID);
	}

	// Token: 0x060043F0 RID: 17392 RVA: 0x0017F3E0 File Offset: 0x0017D5E0
	public void OnReset()
	{
		this.plane = SimDebugView.CreatePlane("SimDebugView", base.transform);
		this.tex = SimDebugView.CreateTexture(out this.texBytes, Grid.WidthInCells, Grid.HeightInCells);
		this.plane.GetComponent<Renderer>().sharedMaterial = this.material;
		this.plane.GetComponent<Renderer>().sharedMaterial.mainTexture = this.tex;
		this.plane.transform.SetLocalPosition(new Vector3(0f, 0f, -6f));
		this.SetMode(global::OverlayModes.None.ID);
	}

	// Token: 0x060043F1 RID: 17393 RVA: 0x0017F47F File Offset: 0x0017D67F
	public static Texture2D CreateTexture(int width, int height)
	{
		return new Texture2D(width, height)
		{
			name = "SimDebugView",
			wrapMode = TextureWrapMode.Clamp,
			filterMode = FilterMode.Point
		};
	}

	// Token: 0x060043F2 RID: 17394 RVA: 0x0017F4A1 File Offset: 0x0017D6A1
	public static Texture2D CreateTexture(out byte[] textureBytes, int width, int height)
	{
		textureBytes = new byte[width * height * 4];
		return new Texture2D(width, height, TextureUtil.TextureFormatToGraphicsFormat(TextureFormat.RGBA32), TextureCreationFlags.None)
		{
			name = "SimDebugView",
			wrapMode = TextureWrapMode.Clamp,
			filterMode = FilterMode.Point
		};
	}

	// Token: 0x060043F3 RID: 17395 RVA: 0x0017F4D8 File Offset: 0x0017D6D8
	public static Texture2D CreateTexture(int width, int height, Color col)
	{
		Color[] array = new Color[width * height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = col;
		}
		Texture2D texture2D = new Texture2D(width, height);
		texture2D.SetPixels(array);
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x060043F4 RID: 17396 RVA: 0x0017F518 File Offset: 0x0017D718
	public static GameObject CreatePlane(string layer, Transform parent)
	{
		GameObject gameObject = new GameObject();
		gameObject.name = "overlayViewDisplayPlane";
		gameObject.SetLayerRecursively(LayerMask.NameToLayer(layer));
		gameObject.transform.SetParent(parent);
		gameObject.transform.SetPosition(Vector3.zero);
		gameObject.AddComponent<MeshRenderer>().reflectionProbeUsage = ReflectionProbeUsage.Off;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		meshFilter.mesh = mesh;
		int num = 4;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		int[] array3 = new int[6];
		float num2 = 2f * (float)Grid.HeightInCells;
		array = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3((float)Grid.WidthInCells, 0f, 0f),
			new Vector3(0f, num2, 0f),
			new Vector3(Grid.WidthInMeters, num2, 0f)
		};
		array2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 2f),
			new Vector2(1f, 2f)
		};
		array3 = new int[] { 0, 2, 1, 1, 2, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		Vector2 vector = new Vector2((float)Grid.WidthInCells, num2);
		mesh.bounds = new Bounds(new Vector3(0.5f * vector.x, 0.5f * vector.y, 0f), new Vector3(vector.x, vector.y, 0f));
		return gameObject;
	}

	// Token: 0x060043F5 RID: 17397 RVA: 0x0017F6F0 File Offset: 0x0017D8F0
	private void Update()
	{
		if (this.plane == null)
		{
			return;
		}
		bool flag = this.mode != global::OverlayModes.None.ID;
		this.plane.SetActive(flag);
		SimDebugViewCompositor.Instance.Toggle(flag && !GameUtil.IsCapturingTimeLapse());
		SimDebugViewCompositor.Instance.material.SetVector("_Thresholds0", new Vector4(0.1f, 0.2f, 0.3f, 0.4f));
		SimDebugViewCompositor.Instance.material.SetVector("_Thresholds1", new Vector4(0.5f, 0.6f, 0.7f, 0.8f));
		float num = 0f;
		if (this.mode == global::OverlayModes.ThermalConductivity.ID || this.mode == global::OverlayModes.Temperature.ID)
		{
			num = 1f;
		}
		SimDebugViewCompositor.Instance.material.SetVector("_ThresholdParameters", new Vector4(num, this.thresholdRange, this.thresholdOpacity, 0f));
		if (flag)
		{
			this.UpdateData(this.tex, this.texBytes, this.mode, 192);
		}
	}

	// Token: 0x060043F6 RID: 17398 RVA: 0x0017F816 File Offset: 0x0017DA16
	private static void SetDefaultBilinear(SimDebugView instance, Texture texture)
	{
		Renderer component = instance.plane.GetComponent<Renderer>();
		component.sharedMaterial = instance.material;
		component.sharedMaterial.mainTexture = instance.tex;
		texture.filterMode = FilterMode.Bilinear;
	}

	// Token: 0x060043F7 RID: 17399 RVA: 0x0017F846 File Offset: 0x0017DA46
	private static void SetDefaultPoint(SimDebugView instance, Texture texture)
	{
		Renderer component = instance.plane.GetComponent<Renderer>();
		component.sharedMaterial = instance.material;
		component.sharedMaterial.mainTexture = instance.tex;
		texture.filterMode = FilterMode.Point;
	}

	// Token: 0x060043F8 RID: 17400 RVA: 0x0017F876 File Offset: 0x0017DA76
	private static void SetDisease(SimDebugView instance, Texture texture)
	{
		Renderer component = instance.plane.GetComponent<Renderer>();
		component.sharedMaterial = instance.diseaseMaterial;
		component.sharedMaterial.mainTexture = instance.tex;
		texture.filterMode = FilterMode.Bilinear;
	}

	// Token: 0x060043F9 RID: 17401 RVA: 0x0017F8A8 File Offset: 0x0017DAA8
	public void UpdateData(Texture2D texture, byte[] textureBytes, HashedString viewMode, byte alpha)
	{
		Action<SimDebugView, Texture> action;
		if (!this.dataUpdateFuncs.TryGetValue(viewMode, out action))
		{
			action = new Action<SimDebugView, Texture>(SimDebugView.SetDefaultPoint);
		}
		action(this, texture);
		int num;
		int num2;
		int num3;
		int num4;
		Grid.GetVisibleExtents(out num, out num2, out num3, out num4);
		this.selectedPathProber = null;
		KSelectable selected = SelectTool.Instance.selected;
		if (selected != null)
		{
			this.selectedPathProber = selected.GetComponent<PathProber>();
		}
		this.updateSimViewWorkItems.Reset(new SimDebugView.UpdateSimViewSharedData(this, this.texBytes, viewMode, this));
		int num5 = 16;
		for (int i = num2; i <= num4; i += num5)
		{
			int num6 = Math.Min(i + num5 - 1, num4);
			this.updateSimViewWorkItems.Add(new SimDebugView.UpdateSimViewWorkItem(num, i, num3, num6));
		}
		this.currentFrame = Time.frameCount;
		this.selectedCell = Grid.PosToCell(Camera.main.ScreenToWorldPoint(KInputManager.GetMousePos()));
		GlobalJobManager.Run(this.updateSimViewWorkItems);
		texture.LoadRawTextureData(textureBytes);
		texture.Apply();
	}

	// Token: 0x060043FA RID: 17402 RVA: 0x0017F9A3 File Offset: 0x0017DBA3
	public void SetGameGridMode(SimDebugView.GameGridMode mode)
	{
		this.gameGridMode = mode;
	}

	// Token: 0x060043FB RID: 17403 RVA: 0x0017F9AC File Offset: 0x0017DBAC
	public SimDebugView.GameGridMode GetGameGridMode()
	{
		return this.gameGridMode;
	}

	// Token: 0x060043FC RID: 17404 RVA: 0x0017F9B4 File Offset: 0x0017DBB4
	public void SetMode(HashedString mode)
	{
		this.mode = mode;
		Game.Instance.gameObject.Trigger(1798162660, mode);
	}

	// Token: 0x060043FD RID: 17405 RVA: 0x0017F9D7 File Offset: 0x0017DBD7
	public HashedString GetMode()
	{
		return this.mode;
	}

	// Token: 0x060043FE RID: 17406 RVA: 0x0017F9E0 File Offset: 0x0017DBE0
	public static Color TemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = Mathf.Clamp((temperature - minTempExpected) / (maxTempExpected - minTempExpected), 0f, 1f);
		return Color.HSVToRGB((10f + (1f - num) * 171f) / 360f, 1f, 1f);
	}

	// Token: 0x060043FF RID: 17407 RVA: 0x0017FA2C File Offset: 0x0017DC2C
	public static Color LiquidTemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = (temperature - minTempExpected) / (maxTempExpected - minTempExpected);
		float num2 = Mathf.Clamp(num, 0.5f, 1f);
		float num3 = Mathf.Clamp(num, 0f, 1f);
		return Color.HSVToRGB((10f + (1f - num2) * 171f) / 360f, num3, 1f);
	}

	// Token: 0x06004400 RID: 17408 RVA: 0x0017FA88 File Offset: 0x0017DC88
	public static Color SolidTemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = Mathf.Clamp((temperature - minTempExpected) / (maxTempExpected - minTempExpected), 0.5f, 1f);
		float num2 = 1f;
		return Color.HSVToRGB((10f + (1f - num) * 171f) / 360f, num2, 1f);
	}

	// Token: 0x06004401 RID: 17409 RVA: 0x0017FAD8 File Offset: 0x0017DCD8
	public static Color GasTemperatureToColor(float temperature, float minTempExpected, float maxTempExpected)
	{
		float num = Mathf.Clamp((temperature - minTempExpected) / (maxTempExpected - minTempExpected), 0f, 0.5f);
		float num2 = 1f;
		return Color.HSVToRGB((10f + (1f - num) * 171f) / 360f, num2, 1f);
	}

	// Token: 0x06004402 RID: 17410 RVA: 0x0017FB28 File Offset: 0x0017DD28
	public Color NormalizedTemperature(float temperature)
	{
		int num = 0;
		int num2 = 0;
		for (int i = 0; i < this.temperatureThresholds.Length; i++)
		{
			if (temperature <= this.temperatureThresholds[i].value)
			{
				num2 = i;
				break;
			}
			num = i;
			num2 = i;
		}
		float num3 = 0f;
		if (num != num2)
		{
			num3 = (temperature - this.temperatureThresholds[num].value) / (this.temperatureThresholds[num2].value - this.temperatureThresholds[num].value);
		}
		num3 = Mathf.Max(num3, 0f);
		num3 = Mathf.Min(num3, 1f);
		return Color.Lerp(GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[num].colorName), GlobalAssets.Instance.colorSet.GetColorByName(this.temperatureThresholds[num2].colorName), num3);
	}

	// Token: 0x06004403 RID: 17411 RVA: 0x0017FC14 File Offset: 0x0017DE14
	public Color NormalizedHeatFlow(int cell)
	{
		int num = 0;
		int num2 = 0;
		float thermalComfort = GameUtil.GetThermalComfort(cell, -0.08368001f);
		for (int i = 0; i < this.heatFlowThresholds.Length; i++)
		{
			if (thermalComfort <= this.heatFlowThresholds[i].value)
			{
				num2 = i;
				break;
			}
			num = i;
			num2 = i;
		}
		float num3 = 0f;
		if (num != num2)
		{
			num3 = (thermalComfort - this.heatFlowThresholds[num].value) / (this.heatFlowThresholds[num2].value - this.heatFlowThresholds[num].value);
		}
		num3 = Mathf.Max(num3, 0f);
		num3 = Mathf.Min(num3, 1f);
		Color color = Color.Lerp(GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[num].colorName), GlobalAssets.Instance.colorSet.GetColorByName(this.heatFlowThresholds[num2].colorName), num3);
		if (Grid.Solid[cell])
		{
			color = Color.black;
		}
		return color;
	}

	// Token: 0x06004404 RID: 17412 RVA: 0x0017FD2A File Offset: 0x0017DF2A
	private static bool IsInsulated(int cell)
	{
		return (Grid.Element[cell].state & Element.State.TemperatureInsulated) > Element.State.Vacuum;
	}

	// Token: 0x06004405 RID: 17413 RVA: 0x0017FD40 File Offset: 0x0017DF40
	private static Color GetDiseaseColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (Grid.DiseaseIdx[cell] != 255)
		{
			Disease disease = Db.Get().Diseases[(int)Grid.DiseaseIdx[cell]];
			color = GlobalAssets.Instance.colorSet.GetColorByName(disease.overlayColourName);
			color.a = SimUtil.DiseaseCountToAlpha(Grid.DiseaseCount[cell]);
		}
		else
		{
			color.a = 0f;
		}
		return color;
	}

	// Token: 0x06004406 RID: 17414 RVA: 0x0017FDC1 File Offset: 0x0017DFC1
	private static Color GetHeatFlowColour(SimDebugView instance, int cell)
	{
		return instance.NormalizedHeatFlow(cell);
	}

	// Token: 0x06004407 RID: 17415 RVA: 0x0017FDCA File Offset: 0x0017DFCA
	private static Color GetBlack(SimDebugView instance, int cell)
	{
		return Color.black;
	}

	// Token: 0x06004408 RID: 17416 RVA: 0x0017FDD4 File Offset: 0x0017DFD4
	public static Color GetLightColour(SimDebugView instance, int cell)
	{
		Color color = GlobalAssets.Instance.colorSet.lightOverlay;
		color.a = Mathf.Clamp(Mathf.Sqrt((float)(Grid.LightIntensity[cell] + LightGridManager.previewLux[cell])) / Mathf.Sqrt(80000f), 0f, 1f);
		if (Grid.LightIntensity[cell] > 72000)
		{
			float num = ((float)Grid.LightIntensity[cell] + (float)LightGridManager.previewLux[cell] - 72000f) / 8000f;
			num /= 10f;
			color.r += Mathf.Min(0.1f, PerlinSimplexNoise.noise(Grid.CellToPos2D(cell).x / 8f, Grid.CellToPos2D(cell).y / 8f + (float)instance.currentFrame / 32f) * num);
		}
		return color;
	}

	// Token: 0x06004409 RID: 17417 RVA: 0x0017FEBC File Offset: 0x0017E0BC
	public static Color GetRadiationColour(SimDebugView instance, int cell)
	{
		float num = Mathf.Clamp(Mathf.Sqrt(Grid.Radiation[cell]) / 30f, 0f, 1f);
		return new Color(0.2f, 0.9f, 0.3f, num);
	}

	// Token: 0x0600440A RID: 17418 RVA: 0x0017FF04 File Offset: 0x0017E104
	public static Color GetRoomsColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (Grid.IsValidCell(instance.selectedCell))
		{
			CavityInfo cavityForCell = Game.Instance.roomProber.GetCavityForCell(cell);
			if (cavityForCell != null && cavityForCell.room != null)
			{
				Room room = cavityForCell.room;
				color = GlobalAssets.Instance.colorSet.GetColorByName(room.roomType.category.colorName);
				color.a = 0.45f;
				if (Game.Instance.roomProber.GetCavityForCell(instance.selectedCell) == cavityForCell)
				{
					color.a += 0.3f;
				}
			}
		}
		return color;
	}

	// Token: 0x0600440B RID: 17419 RVA: 0x0017FFA4 File Offset: 0x0017E1A4
	public static Color GetJoulesColour(SimDebugView instance, int cell)
	{
		float num = Grid.Element[cell].specificHeatCapacity * Grid.Temperature[cell] * (Grid.Mass[cell] * 1000f);
		float num2 = 0.5f * num / (ElementLoader.FindElementByHash(SimHashes.SandStone).specificHeatCapacity * 294f * 1000000f);
		return Color.Lerp(Color.black, Color.red, num2);
	}

	// Token: 0x0600440C RID: 17420 RVA: 0x00180010 File Offset: 0x0017E210
	public static Color GetNormalizedTemperatureColourMode(SimDebugView instance, int cell)
	{
		switch (Game.Instance.temperatureOverlayMode)
		{
		case Game.TemperatureOverlayModes.AbsoluteTemperature:
			return SimDebugView.GetNormalizedTemperatureColour(instance, cell);
		case Game.TemperatureOverlayModes.AdaptiveTemperature:
			return SimDebugView.GetNormalizedTemperatureColour(instance, cell);
		case Game.TemperatureOverlayModes.HeatFlow:
			return SimDebugView.GetHeatFlowColour(instance, cell);
		case Game.TemperatureOverlayModes.StateChange:
			return SimDebugView.GetStateChangeProximityColour(instance, cell);
		default:
			return SimDebugView.GetNormalizedTemperatureColour(instance, cell);
		}
	}

	// Token: 0x0600440D RID: 17421 RVA: 0x00180068 File Offset: 0x0017E268
	public static Color GetStateChangeProximityColour(SimDebugView instance, int cell)
	{
		float num = Grid.Temperature[cell];
		Element element = Grid.Element[cell];
		float num2 = element.lowTemp;
		float num3 = element.highTemp;
		if (element.IsGas)
		{
			num3 = Mathf.Min(num2 + 150f, num3);
			return SimDebugView.GasTemperatureToColor(num, num2, num3);
		}
		if (element.IsSolid)
		{
			num2 = Mathf.Max(num3 - 150f, num2);
			return SimDebugView.SolidTemperatureToColor(num, num2, num3);
		}
		return SimDebugView.TemperatureToColor(num, num2, num3);
	}

	// Token: 0x0600440E RID: 17422 RVA: 0x001800E0 File Offset: 0x0017E2E0
	public static Color GetNormalizedTemperatureColour(SimDebugView instance, int cell)
	{
		float num = Grid.Temperature[cell];
		return instance.NormalizedTemperature(num);
	}

	// Token: 0x0600440F RID: 17423 RVA: 0x00180100 File Offset: 0x0017E300
	private static Color GetGameGridColour(SimDebugView instance, int cell)
	{
		Color color = new Color32(0, 0, 0, byte.MaxValue);
		switch (instance.gameGridMode)
		{
		case SimDebugView.GameGridMode.GameSolidMap:
			color = (Grid.Solid[cell] ? Color.white : Color.black);
			break;
		case SimDebugView.GameGridMode.Lighting:
			color = ((Grid.LightCount[cell] > 0 || LightGridManager.previewLux[cell] > 0) ? Color.white : Color.black);
			break;
		case SimDebugView.GameGridMode.DigAmount:
			if (Grid.Element[cell].IsSolid)
			{
				float num = Grid.Damage[cell] / 255f;
				color = Color.HSVToRGB(1f - num, 1f, 1f);
			}
			break;
		case SimDebugView.GameGridMode.DupePassable:
			color = (Grid.DupePassable[cell] ? Color.white : Color.black);
			break;
		}
		return color;
	}

	// Token: 0x06004410 RID: 17424 RVA: 0x001801E1 File Offset: 0x0017E3E1
	public Color32 GetColourForID(int id)
	{
		return this.networkColours[id % this.networkColours.Length];
	}

	// Token: 0x06004411 RID: 17425 RVA: 0x001801F8 File Offset: 0x0017E3F8
	private static Color GetThermalConductivityColour(SimDebugView instance, int cell)
	{
		bool flag = SimDebugView.IsInsulated(cell);
		Color black = Color.black;
		float num = instance.maxThermalConductivity - instance.minThermalConductivity;
		if (!flag && num != 0f)
		{
			float num2 = (Grid.Element[cell].thermalConductivity - instance.minThermalConductivity) / num;
			num2 = Mathf.Max(num2, 0f);
			num2 = Mathf.Min(num2, 1f);
			black = new Color(num2, num2, num2);
		}
		return black;
	}

	// Token: 0x06004412 RID: 17426 RVA: 0x00180264 File Offset: 0x0017E464
	private static Color GetPressureMapColour(SimDebugView instance, int cell)
	{
		Color32 color = Color.black;
		if (Grid.Pressure[cell] > 0f)
		{
			float num = Mathf.Clamp((Grid.Pressure[cell] - instance.minPressureExpected) / (instance.maxPressureExpected - instance.minPressureExpected), 0f, 1f) * 0.9f;
			color = new Color(num, num, num, 1f);
		}
		return color;
	}

	// Token: 0x06004413 RID: 17427 RVA: 0x001802DC File Offset: 0x0017E4DC
	private static Color GetOxygenMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (!Grid.IsLiquid(cell) && !Grid.Solid[cell])
		{
			if (Grid.Mass[cell] > SimDebugView.minimumBreathable && (Grid.Element[cell].id == SimHashes.Oxygen || Grid.Element[cell].id == SimHashes.ContaminatedOxygen))
			{
				float num = Mathf.Clamp((Grid.Mass[cell] - SimDebugView.minimumBreathable) / SimDebugView.optimallyBreathable, 0f, 1f);
				color = instance.breathableGradient.Evaluate(num);
			}
			else
			{
				color = instance.unbreathableColour;
			}
		}
		return color;
	}

	// Token: 0x06004414 RID: 17428 RVA: 0x00180384 File Offset: 0x0017E584
	private static Color GetTileColour(SimDebugView instance, int cell)
	{
		float num = 0.33f;
		Color color = new Color(num, num, num);
		Element element = Grid.Element[cell];
		bool flag = false;
		foreach (Tag tag in Game.Instance.tileOverlayFilters)
		{
			if (element.HasTag(tag))
			{
				flag = true;
			}
		}
		if (flag)
		{
			color = element.substance.uiColour;
		}
		return color;
	}

	// Token: 0x06004415 RID: 17429 RVA: 0x00180414 File Offset: 0x0017E614
	private static Color GetTileTypeColour(SimDebugView instance, int cell)
	{
		return Grid.Element[cell].substance.uiColour;
	}

	// Token: 0x06004416 RID: 17430 RVA: 0x0018042C File Offset: 0x0017E62C
	private static Color GetStateMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		switch (Grid.Element[cell].state & Element.State.Solid)
		{
		case Element.State.Gas:
			color = Color.yellow;
			break;
		case Element.State.Liquid:
			color = Color.green;
			break;
		case Element.State.Solid:
			color = Color.blue;
			break;
		}
		return color;
	}

	// Token: 0x06004417 RID: 17431 RVA: 0x00180480 File Offset: 0x0017E680
	private static Color GetSolidLiquidMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		switch (Grid.Element[cell].state & Element.State.Solid)
		{
		case Element.State.Liquid:
			color = Color.green;
			break;
		case Element.State.Solid:
			color = Color.blue;
			break;
		}
		return color;
	}

	// Token: 0x06004418 RID: 17432 RVA: 0x001804CC File Offset: 0x0017E6CC
	private static Color GetStateChangeColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		Element element = Grid.Element[cell];
		if (!element.IsVacuum)
		{
			float num = Grid.Temperature[cell];
			float num2 = element.lowTemp * 0.05f;
			float num3 = Mathf.Abs(num - element.lowTemp) / num2;
			float num4 = element.highTemp * 0.05f;
			float num5 = Mathf.Abs(num - element.highTemp) / num4;
			float num6 = Mathf.Max(0f, 1f - Mathf.Min(num3, num5));
			color = Color.Lerp(Color.black, Color.red, num6);
		}
		return color;
	}

	// Token: 0x06004419 RID: 17433 RVA: 0x00180564 File Offset: 0x0017E764
	private static Color GetDecorColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (!Grid.Solid[cell])
		{
			float num = GameUtil.GetDecorAtCell(cell) / 100f;
			if (num > 0f)
			{
				color = Color.Lerp(GlobalAssets.Instance.colorSet.decorBaseline, GlobalAssets.Instance.colorSet.decorPositive, Mathf.Abs(num));
			}
			else
			{
				color = Color.Lerp(GlobalAssets.Instance.colorSet.decorBaseline, GlobalAssets.Instance.colorSet.decorNegative, Mathf.Abs(num));
			}
		}
		return color;
	}

	// Token: 0x0600441A RID: 17434 RVA: 0x00180604 File Offset: 0x0017E804
	private static Color GetDangerColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		SimDebugView.DangerAmount dangerAmount = SimDebugView.DangerAmount.None;
		if (!Grid.Element[cell].IsSolid)
		{
			float num = 0f;
			if (Grid.Temperature[cell] < SimDebugView.minMinionTemperature)
			{
				num = Mathf.Abs(Grid.Temperature[cell] - SimDebugView.minMinionTemperature);
			}
			if (Grid.Temperature[cell] > SimDebugView.maxMinionTemperature)
			{
				num = Mathf.Abs(Grid.Temperature[cell] - SimDebugView.maxMinionTemperature);
			}
			if (num > 0f)
			{
				if (num < 10f)
				{
					dangerAmount = SimDebugView.DangerAmount.VeryLow;
				}
				else if (num < 30f)
				{
					dangerAmount = SimDebugView.DangerAmount.Low;
				}
				else if (num < 100f)
				{
					dangerAmount = SimDebugView.DangerAmount.Moderate;
				}
				else if (num < 200f)
				{
					dangerAmount = SimDebugView.DangerAmount.High;
				}
				else if (num < 400f)
				{
					dangerAmount = SimDebugView.DangerAmount.VeryHigh;
				}
				else if (num > 800f)
				{
					dangerAmount = SimDebugView.DangerAmount.Extreme;
				}
			}
		}
		if (dangerAmount < SimDebugView.DangerAmount.VeryHigh && (Grid.Element[cell].IsVacuum || (Grid.Element[cell].IsGas && (Grid.Element[cell].id != SimHashes.Oxygen || Grid.Pressure[cell] < SimDebugView.minMinionPressure))))
		{
			dangerAmount++;
		}
		if (dangerAmount != SimDebugView.DangerAmount.None)
		{
			float num2 = (float)dangerAmount / 6f;
			color = Color.HSVToRGB((80f - num2 * 80f) / 360f, 1f, 1f);
		}
		return color;
	}

	// Token: 0x0600441B RID: 17435 RVA: 0x0018074C File Offset: 0x0017E94C
	private static Color GetSimCheckErrorMapColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		Element element = Grid.Element[cell];
		float num = Grid.Mass[cell];
		float num2 = Grid.Temperature[cell];
		if (float.IsNaN(num) || float.IsNaN(num2) || num > 10000f || num2 > 10000f)
		{
			return Color.red;
		}
		if (element.IsVacuum)
		{
			if (num2 != 0f)
			{
				color = Color.yellow;
			}
			else if (num != 0f)
			{
				color = Color.blue;
			}
			else
			{
				color = Color.gray;
			}
		}
		else if (num2 < 10f)
		{
			color = Color.red;
		}
		else if (Grid.Mass[cell] < 1f && Grid.Pressure[cell] < 1f)
		{
			color = Color.green;
		}
		else if (num2 > element.highTemp + 3f && element.highTempTransition != null)
		{
			color = Color.magenta;
		}
		else if (num2 < element.lowTemp + 3f && element.lowTempTransition != null)
		{
			color = Color.cyan;
		}
		return color;
	}

	// Token: 0x0600441C RID: 17436 RVA: 0x00180854 File Offset: 0x0017EA54
	private static Color GetFakeFloorColour(SimDebugView instance, int cell)
	{
		if (!Grid.FakeFloor[cell])
		{
			return Color.black;
		}
		return Color.cyan;
	}

	// Token: 0x0600441D RID: 17437 RVA: 0x0018086E File Offset: 0x0017EA6E
	private static Color GetFoundationColour(SimDebugView instance, int cell)
	{
		if (!Grid.Foundation[cell])
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x0600441E RID: 17438 RVA: 0x00180888 File Offset: 0x0017EA88
	private static Color GetDupePassableColour(SimDebugView instance, int cell)
	{
		if (!Grid.DupePassable[cell])
		{
			return Color.black;
		}
		return Color.green;
	}

	// Token: 0x0600441F RID: 17439 RVA: 0x001808A2 File Offset: 0x0017EAA2
	private static Color GetCritterImpassableColour(SimDebugView instance, int cell)
	{
		if (!Grid.CritterImpassable[cell])
		{
			return Color.black;
		}
		return Color.yellow;
	}

	// Token: 0x06004420 RID: 17440 RVA: 0x001808BC File Offset: 0x0017EABC
	private static Color GetDupeImpassableColour(SimDebugView instance, int cell)
	{
		if (!Grid.DupeImpassable[cell])
		{
			return Color.black;
		}
		return Color.red;
	}

	// Token: 0x06004421 RID: 17441 RVA: 0x001808D6 File Offset: 0x0017EAD6
	private static Color GetMinionOccupiedColour(SimDebugView instance, int cell)
	{
		if (!(Grid.Objects[cell, 0] != null))
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06004422 RID: 17442 RVA: 0x001808F7 File Offset: 0x0017EAF7
	private static Color GetMinionGroupProberColour(SimDebugView instance, int cell)
	{
		if (!MinionGroupProber.Get().IsReachable(cell))
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06004423 RID: 17443 RVA: 0x00180911 File Offset: 0x0017EB11
	private static Color GetPathProberColour(SimDebugView instance, int cell)
	{
		if (!(instance.selectedPathProber != null) || instance.selectedPathProber.GetCost(cell) == -1)
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06004424 RID: 17444 RVA: 0x0018093B File Offset: 0x0017EB3B
	private static Color GetReservedColour(SimDebugView instance, int cell)
	{
		if (!Grid.Reserved[cell])
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06004425 RID: 17445 RVA: 0x00180955 File Offset: 0x0017EB55
	private static Color GetAllowPathFindingColour(SimDebugView instance, int cell)
	{
		if (!Grid.AllowPathfinding[cell])
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x06004426 RID: 17446 RVA: 0x00180970 File Offset: 0x0017EB70
	private static Color GetMassColour(SimDebugView instance, int cell)
	{
		Color color = Color.black;
		if (!SimDebugView.IsInsulated(cell))
		{
			float num = Grid.Mass[cell];
			if (num > 0f)
			{
				float num2 = (num - SimDebugView.Instance.minMassExpected) / (SimDebugView.Instance.maxMassExpected - SimDebugView.Instance.minMassExpected);
				color = Color.HSVToRGB(1f - num2, 1f, 1f);
			}
		}
		return color;
	}

	// Token: 0x06004427 RID: 17447 RVA: 0x001809DA File Offset: 0x0017EBDA
	public static Color GetScenePartitionerColour(SimDebugView instance, int cell)
	{
		if (!GameScenePartitioner.Instance.DoDebugLayersContainItemsOnCell(cell))
		{
			return Color.black;
		}
		return Color.white;
	}

	// Token: 0x04002D55 RID: 11605
	[SerializeField]
	public Material material;

	// Token: 0x04002D56 RID: 11606
	public Material diseaseMaterial;

	// Token: 0x04002D57 RID: 11607
	public bool hideFOW;

	// Token: 0x04002D58 RID: 11608
	public const int colourSize = 4;

	// Token: 0x04002D59 RID: 11609
	private byte[] texBytes;

	// Token: 0x04002D5A RID: 11610
	private int currentFrame;

	// Token: 0x04002D5B RID: 11611
	[SerializeField]
	private Texture2D tex;

	// Token: 0x04002D5C RID: 11612
	[SerializeField]
	private GameObject plane;

	// Token: 0x04002D5D RID: 11613
	private HashedString mode = global::OverlayModes.Power.ID;

	// Token: 0x04002D5E RID: 11614
	private SimDebugView.GameGridMode gameGridMode = SimDebugView.GameGridMode.DigAmount;

	// Token: 0x04002D5F RID: 11615
	private PathProber selectedPathProber;

	// Token: 0x04002D60 RID: 11616
	public float minTempExpected = 173.15f;

	// Token: 0x04002D61 RID: 11617
	public float maxTempExpected = 423.15f;

	// Token: 0x04002D62 RID: 11618
	public float minMassExpected = 1.0001f;

	// Token: 0x04002D63 RID: 11619
	public float maxMassExpected = 10000f;

	// Token: 0x04002D64 RID: 11620
	public float minPressureExpected = 1.300003f;

	// Token: 0x04002D65 RID: 11621
	public float maxPressureExpected = 201.3f;

	// Token: 0x04002D66 RID: 11622
	public float minThermalConductivity;

	// Token: 0x04002D67 RID: 11623
	public float maxThermalConductivity = 30f;

	// Token: 0x04002D68 RID: 11624
	public float thresholdRange = 0.001f;

	// Token: 0x04002D69 RID: 11625
	public float thresholdOpacity = 0.8f;

	// Token: 0x04002D6A RID: 11626
	public static float minimumBreathable = 0.05f;

	// Token: 0x04002D6B RID: 11627
	public static float optimallyBreathable = 1f;

	// Token: 0x04002D6C RID: 11628
	public SimDebugView.ColorThreshold[] temperatureThresholds;

	// Token: 0x04002D6D RID: 11629
	public SimDebugView.ColorThreshold[] heatFlowThresholds;

	// Token: 0x04002D6E RID: 11630
	public Color32[] networkColours;

	// Token: 0x04002D6F RID: 11631
	public Gradient breathableGradient = new Gradient();

	// Token: 0x04002D70 RID: 11632
	public Color32 unbreathableColour = new Color(0.5f, 0f, 0f);

	// Token: 0x04002D71 RID: 11633
	public Color32[] toxicColour = new Color32[]
	{
		new Color(0.5f, 0f, 0.5f),
		new Color(1f, 0f, 1f)
	};

	// Token: 0x04002D72 RID: 11634
	public static SimDebugView Instance;

	// Token: 0x04002D73 RID: 11635
	private WorkItemCollection<SimDebugView.UpdateSimViewWorkItem, SimDebugView.UpdateSimViewSharedData> updateSimViewWorkItems = new WorkItemCollection<SimDebugView.UpdateSimViewWorkItem, SimDebugView.UpdateSimViewSharedData>();

	// Token: 0x04002D74 RID: 11636
	private int selectedCell;

	// Token: 0x04002D75 RID: 11637
	private Dictionary<HashedString, Action<SimDebugView, Texture>> dataUpdateFuncs = new Dictionary<HashedString, Action<SimDebugView, Texture>>
	{
		{
			global::OverlayModes.Temperature.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultBilinear)
		},
		{
			global::OverlayModes.Oxygen.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultBilinear)
		},
		{
			global::OverlayModes.Decor.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultBilinear)
		},
		{
			global::OverlayModes.TileMode.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDefaultPoint)
		},
		{
			global::OverlayModes.Disease.ID,
			new Action<SimDebugView, Texture>(SimDebugView.SetDisease)
		}
	};

	// Token: 0x04002D76 RID: 11638
	private Dictionary<HashedString, Func<SimDebugView, int, Color>> getColourFuncs = new Dictionary<HashedString, Func<SimDebugView, int, Color>>
	{
		{
			global::OverlayModes.ThermalConductivity.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetThermalConductivityColour)
		},
		{
			global::OverlayModes.Temperature.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetNormalizedTemperatureColourMode)
		},
		{
			global::OverlayModes.Disease.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDiseaseColour)
		},
		{
			global::OverlayModes.Decor.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDecorColour)
		},
		{
			global::OverlayModes.Oxygen.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetOxygenMapColour)
		},
		{
			global::OverlayModes.Light.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetLightColour)
		},
		{
			global::OverlayModes.Radiation.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetRadiationColour)
		},
		{
			global::OverlayModes.Rooms.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetRoomsColour)
		},
		{
			global::OverlayModes.TileMode.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetTileColour)
		},
		{
			global::OverlayModes.Suit.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			global::OverlayModes.Priorities.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			global::OverlayModes.Crop.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			global::OverlayModes.Harvest.ID,
			new Func<SimDebugView, int, Color>(SimDebugView.GetBlack)
		},
		{
			SimDebugView.OverlayModes.GameGrid,
			new Func<SimDebugView, int, Color>(SimDebugView.GetGameGridColour)
		},
		{
			SimDebugView.OverlayModes.StateChange,
			new Func<SimDebugView, int, Color>(SimDebugView.GetStateChangeColour)
		},
		{
			SimDebugView.OverlayModes.SimCheckErrorMap,
			new Func<SimDebugView, int, Color>(SimDebugView.GetSimCheckErrorMapColour)
		},
		{
			SimDebugView.OverlayModes.Foundation,
			new Func<SimDebugView, int, Color>(SimDebugView.GetFoundationColour)
		},
		{
			SimDebugView.OverlayModes.FakeFloor,
			new Func<SimDebugView, int, Color>(SimDebugView.GetFakeFloorColour)
		},
		{
			SimDebugView.OverlayModes.DupePassable,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDupePassableColour)
		},
		{
			SimDebugView.OverlayModes.DupeImpassable,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDupeImpassableColour)
		},
		{
			SimDebugView.OverlayModes.CritterImpassable,
			new Func<SimDebugView, int, Color>(SimDebugView.GetCritterImpassableColour)
		},
		{
			SimDebugView.OverlayModes.MinionGroupProber,
			new Func<SimDebugView, int, Color>(SimDebugView.GetMinionGroupProberColour)
		},
		{
			SimDebugView.OverlayModes.PathProber,
			new Func<SimDebugView, int, Color>(SimDebugView.GetPathProberColour)
		},
		{
			SimDebugView.OverlayModes.Reserved,
			new Func<SimDebugView, int, Color>(SimDebugView.GetReservedColour)
		},
		{
			SimDebugView.OverlayModes.AllowPathFinding,
			new Func<SimDebugView, int, Color>(SimDebugView.GetAllowPathFindingColour)
		},
		{
			SimDebugView.OverlayModes.Danger,
			new Func<SimDebugView, int, Color>(SimDebugView.GetDangerColour)
		},
		{
			SimDebugView.OverlayModes.MinionOccupied,
			new Func<SimDebugView, int, Color>(SimDebugView.GetMinionOccupiedColour)
		},
		{
			SimDebugView.OverlayModes.Pressure,
			new Func<SimDebugView, int, Color>(SimDebugView.GetPressureMapColour)
		},
		{
			SimDebugView.OverlayModes.TileType,
			new Func<SimDebugView, int, Color>(SimDebugView.GetTileTypeColour)
		},
		{
			SimDebugView.OverlayModes.State,
			new Func<SimDebugView, int, Color>(SimDebugView.GetStateMapColour)
		},
		{
			SimDebugView.OverlayModes.SolidLiquid,
			new Func<SimDebugView, int, Color>(SimDebugView.GetSolidLiquidMapColour)
		},
		{
			SimDebugView.OverlayModes.Mass,
			new Func<SimDebugView, int, Color>(SimDebugView.GetMassColour)
		},
		{
			SimDebugView.OverlayModes.Joules,
			new Func<SimDebugView, int, Color>(SimDebugView.GetJoulesColour)
		},
		{
			SimDebugView.OverlayModes.ScenePartitioner,
			new Func<SimDebugView, int, Color>(SimDebugView.GetScenePartitionerColour)
		}
	};

	// Token: 0x04002D77 RID: 11639
	public static readonly Color[] dbColours = new Color[]
	{
		new Color(0f, 0f, 0f, 0f),
		new Color(1f, 1f, 1f, 0.3f),
		new Color(0.7058824f, 0.8235294f, 1f, 0.2f),
		new Color(0f, 0.3137255f, 1f, 0.3f),
		new Color(0.7058824f, 1f, 0.7058824f, 0.5f),
		new Color(0.078431375f, 1f, 0f, 0.7f),
		new Color(1f, 0.9019608f, 0.7058824f, 0.9f),
		new Color(1f, 0.8235294f, 0f, 0.9f),
		new Color(1f, 0.7176471f, 0.3019608f, 0.9f),
		new Color(1f, 0.41568628f, 0f, 0.9f),
		new Color(1f, 0.7058824f, 0.7058824f, 1f),
		new Color(1f, 0f, 0f, 1f),
		new Color(1f, 0f, 0f, 1f)
	};

	// Token: 0x04002D78 RID: 11640
	private static float minMinionTemperature = 260f;

	// Token: 0x04002D79 RID: 11641
	private static float maxMinionTemperature = 310f;

	// Token: 0x04002D7A RID: 11642
	private static float minMinionPressure = 80f;

	// Token: 0x020016F9 RID: 5881
	public static class OverlayModes
	{
		// Token: 0x04006B7C RID: 27516
		public static readonly HashedString Mass = "Mass";

		// Token: 0x04006B7D RID: 27517
		public static readonly HashedString Pressure = "Pressure";

		// Token: 0x04006B7E RID: 27518
		public static readonly HashedString GameGrid = "GameGrid";

		// Token: 0x04006B7F RID: 27519
		public static readonly HashedString ScenePartitioner = "ScenePartitioner";

		// Token: 0x04006B80 RID: 27520
		public static readonly HashedString ConduitUpdates = "ConduitUpdates";

		// Token: 0x04006B81 RID: 27521
		public static readonly HashedString Flow = "Flow";

		// Token: 0x04006B82 RID: 27522
		public static readonly HashedString StateChange = "StateChange";

		// Token: 0x04006B83 RID: 27523
		public static readonly HashedString SimCheckErrorMap = "SimCheckErrorMap";

		// Token: 0x04006B84 RID: 27524
		public static readonly HashedString DupePassable = "DupePassable";

		// Token: 0x04006B85 RID: 27525
		public static readonly HashedString Foundation = "Foundation";

		// Token: 0x04006B86 RID: 27526
		public static readonly HashedString FakeFloor = "FakeFloor";

		// Token: 0x04006B87 RID: 27527
		public static readonly HashedString CritterImpassable = "CritterImpassable";

		// Token: 0x04006B88 RID: 27528
		public static readonly HashedString DupeImpassable = "DupeImpassable";

		// Token: 0x04006B89 RID: 27529
		public static readonly HashedString MinionGroupProber = "MinionGroupProber";

		// Token: 0x04006B8A RID: 27530
		public static readonly HashedString PathProber = "PathProber";

		// Token: 0x04006B8B RID: 27531
		public static readonly HashedString Reserved = "Reserved";

		// Token: 0x04006B8C RID: 27532
		public static readonly HashedString AllowPathFinding = "AllowPathFinding";

		// Token: 0x04006B8D RID: 27533
		public static readonly HashedString Danger = "Danger";

		// Token: 0x04006B8E RID: 27534
		public static readonly HashedString MinionOccupied = "MinionOccupied";

		// Token: 0x04006B8F RID: 27535
		public static readonly HashedString TileType = "TileType";

		// Token: 0x04006B90 RID: 27536
		public static readonly HashedString State = "State";

		// Token: 0x04006B91 RID: 27537
		public static readonly HashedString SolidLiquid = "SolidLiquid";

		// Token: 0x04006B92 RID: 27538
		public static readonly HashedString Joules = "Joules";
	}

	// Token: 0x020016FA RID: 5882
	public enum GameGridMode
	{
		// Token: 0x04006B94 RID: 27540
		GameSolidMap,
		// Token: 0x04006B95 RID: 27541
		Lighting,
		// Token: 0x04006B96 RID: 27542
		RoomMap,
		// Token: 0x04006B97 RID: 27543
		Style,
		// Token: 0x04006B98 RID: 27544
		PlantDensity,
		// Token: 0x04006B99 RID: 27545
		DigAmount,
		// Token: 0x04006B9A RID: 27546
		DupePassable
	}

	// Token: 0x020016FB RID: 5883
	[Serializable]
	public struct ColorThreshold
	{
		// Token: 0x04006B9B RID: 27547
		public string colorName;

		// Token: 0x04006B9C RID: 27548
		public float value;
	}

	// Token: 0x020016FC RID: 5884
	private struct UpdateSimViewSharedData
	{
		// Token: 0x0600892D RID: 35117 RVA: 0x002F7DC2 File Offset: 0x002F5FC2
		public UpdateSimViewSharedData(SimDebugView instance, byte[] texture_bytes, HashedString sim_view_mode, SimDebugView sim_debug_view)
		{
			this.instance = instance;
			this.textureBytes = texture_bytes;
			this.simViewMode = sim_view_mode;
			this.simDebugView = sim_debug_view;
		}

		// Token: 0x04006B9D RID: 27549
		public SimDebugView instance;

		// Token: 0x04006B9E RID: 27550
		public HashedString simViewMode;

		// Token: 0x04006B9F RID: 27551
		public SimDebugView simDebugView;

		// Token: 0x04006BA0 RID: 27552
		public byte[] textureBytes;
	}

	// Token: 0x020016FD RID: 5885
	private struct UpdateSimViewWorkItem : IWorkItem<SimDebugView.UpdateSimViewSharedData>
	{
		// Token: 0x0600892E RID: 35118 RVA: 0x002F7DE4 File Offset: 0x002F5FE4
		public UpdateSimViewWorkItem(int x0, int y0, int x1, int y1)
		{
			this.x0 = Mathf.Clamp(x0, 0, Grid.WidthInCells - 1);
			this.x1 = Mathf.Clamp(x1, 0, Grid.WidthInCells - 1);
			this.y0 = Mathf.Clamp(y0, 0, Grid.HeightInCells - 1);
			this.y1 = Mathf.Clamp(y1, 0, Grid.HeightInCells - 1);
		}

		// Token: 0x0600892F RID: 35119 RVA: 0x002F7E44 File Offset: 0x002F6044
		public void Run(SimDebugView.UpdateSimViewSharedData shared_data)
		{
			Func<SimDebugView, int, Color> func;
			if (!shared_data.instance.getColourFuncs.TryGetValue(shared_data.simViewMode, out func))
			{
				func = new Func<SimDebugView, int, Color>(SimDebugView.GetBlack);
			}
			for (int i = this.y0; i <= this.y1; i++)
			{
				int num = Grid.XYToCell(this.x0, i);
				int num2 = Grid.XYToCell(this.x1, i);
				for (int j = num; j <= num2; j++)
				{
					int num3 = j * 4;
					if (Grid.IsActiveWorld(j))
					{
						Color color = func(shared_data.instance, j);
						shared_data.textureBytes[num3] = (byte)(Mathf.Min(color.r, 1f) * 255f);
						shared_data.textureBytes[num3 + 1] = (byte)(Mathf.Min(color.g, 1f) * 255f);
						shared_data.textureBytes[num3 + 2] = (byte)(Mathf.Min(color.b, 1f) * 255f);
						shared_data.textureBytes[num3 + 3] = (byte)(Mathf.Min(color.a, 1f) * 255f);
					}
					else
					{
						shared_data.textureBytes[num3] = 0;
						shared_data.textureBytes[num3 + 1] = 0;
						shared_data.textureBytes[num3 + 2] = 0;
						shared_data.textureBytes[num3 + 3] = 0;
					}
				}
			}
		}

		// Token: 0x04006BA1 RID: 27553
		private int x0;

		// Token: 0x04006BA2 RID: 27554
		private int y0;

		// Token: 0x04006BA3 RID: 27555
		private int x1;

		// Token: 0x04006BA4 RID: 27556
		private int y1;
	}

	// Token: 0x020016FE RID: 5886
	public enum DangerAmount
	{
		// Token: 0x04006BA6 RID: 27558
		None,
		// Token: 0x04006BA7 RID: 27559
		VeryLow,
		// Token: 0x04006BA8 RID: 27560
		Low,
		// Token: 0x04006BA9 RID: 27561
		Moderate,
		// Token: 0x04006BAA RID: 27562
		High,
		// Token: 0x04006BAB RID: 27563
		VeryHigh,
		// Token: 0x04006BAC RID: 27564
		Extreme,
		// Token: 0x04006BAD RID: 27565
		MAX_DANGERAMOUNT = 6
	}
}
