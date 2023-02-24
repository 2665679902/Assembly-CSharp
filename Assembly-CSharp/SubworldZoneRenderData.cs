using System;
using Delaunay.Geo;
using Klei;
using ProcGen;
using UnityEngine;

// Token: 0x020008D0 RID: 2256
[AddComponentMenu("KMonoBehaviour/scripts/SubworldZoneRenderData")]
public class SubworldZoneRenderData : KMonoBehaviour
{
	// Token: 0x060040D0 RID: 16592 RVA: 0x0016A96C File Offset: 0x00168B6C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		ShaderReloader.Register(new System.Action(this.OnShadersReloaded));
		this.GenerateTexture();
		this.OnActiveWorldChanged();
		Game.Instance.Subscribe(1983128072, delegate(object worlds)
		{
			this.OnActiveWorldChanged();
		});
	}

	// Token: 0x060040D1 RID: 16593 RVA: 0x0016A9B8 File Offset: 0x00168BB8
	public void OnActiveWorldChanged()
	{
		byte[] rawTextureData = this.colourTex.GetRawTextureData();
		byte[] rawTextureData2 = this.indexTex.GetRawTextureData();
		WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
		Vector2 zero = Vector2.zero;
		for (int i = 0; i < clusterDetailSave.overworldCells.Count; i++)
		{
			WorldDetailSave.OverworldCell overworldCell = clusterDetailSave.overworldCells[i];
			Polygon poly = overworldCell.poly;
			zero.y = (float)((int)Mathf.Floor(poly.bounds.yMin));
			while (zero.y < Mathf.Ceil(poly.bounds.yMax))
			{
				zero.x = (float)((int)Mathf.Floor(poly.bounds.xMin));
				while (zero.x < Mathf.Ceil(poly.bounds.xMax))
				{
					if (poly.Contains(zero))
					{
						int num = Grid.XYToCell((int)zero.x, (int)zero.y);
						if (Grid.IsValidCell(num))
						{
							if (Grid.IsActiveWorld(num))
							{
								rawTextureData2[num] = ((overworldCell.zoneType == SubWorld.ZoneType.Space) ? byte.MaxValue : ((byte)this.zoneTextureArrayIndices[(int)overworldCell.zoneType]));
								Color32 color = this.zoneColours[(int)overworldCell.zoneType];
								rawTextureData[num * 3] = color.r;
								rawTextureData[num * 3 + 1] = color.g;
								rawTextureData[num * 3 + 2] = color.b;
							}
							else
							{
								rawTextureData2[num] = byte.MaxValue;
								Color32 color2 = this.zoneColours[7];
								rawTextureData[num * 3] = color2.r;
								rawTextureData[num * 3 + 1] = color2.g;
								rawTextureData[num * 3 + 2] = color2.b;
							}
						}
					}
					zero.x += 1f;
				}
				zero.y += 1f;
			}
		}
		this.colourTex.LoadRawTextureData(rawTextureData);
		this.indexTex.LoadRawTextureData(rawTextureData2);
		this.colourTex.Apply();
		this.indexTex.Apply();
		this.OnShadersReloaded();
	}

	// Token: 0x060040D2 RID: 16594 RVA: 0x0016ABE0 File Offset: 0x00168DE0
	public void GenerateTexture()
	{
		byte[] array = new byte[Grid.WidthInCells * Grid.HeightInCells];
		byte[] array2 = new byte[Grid.WidthInCells * Grid.HeightInCells * 3];
		this.colourTex = new Texture2D(Grid.WidthInCells, Grid.HeightInCells, TextureFormat.RGB24, false);
		this.colourTex.name = "SubworldRegionColourData";
		this.colourTex.filterMode = FilterMode.Bilinear;
		this.colourTex.wrapMode = TextureWrapMode.Clamp;
		this.colourTex.anisoLevel = 0;
		this.indexTex = new Texture2D(Grid.WidthInCells, Grid.HeightInCells, TextureFormat.Alpha8, false);
		this.indexTex.name = "SubworldRegionIndexData";
		this.indexTex.filterMode = FilterMode.Point;
		this.indexTex.wrapMode = TextureWrapMode.Clamp;
		this.indexTex.anisoLevel = 0;
		for (int i = 0; i < Grid.CellCount; i++)
		{
			array[i] = byte.MaxValue;
			Color32 color = this.zoneColours[7];
			array2[i * 3] = color.r;
			array2[i * 3 + 1] = color.g;
			array2[i * 3 + 2] = color.b;
		}
		this.colourTex.LoadRawTextureData(array2);
		this.indexTex.LoadRawTextureData(array);
		this.colourTex.Apply();
		this.indexTex.Apply();
		this.worldZoneTypes = new SubWorld.ZoneType[Grid.CellCount];
		WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
		Vector2 zero = Vector2.zero;
		for (int j = 0; j < clusterDetailSave.overworldCells.Count; j++)
		{
			WorldDetailSave.OverworldCell overworldCell = clusterDetailSave.overworldCells[j];
			Polygon poly = overworldCell.poly;
			zero.y = (float)((int)Mathf.Floor(poly.bounds.yMin));
			while (zero.y < Mathf.Ceil(poly.bounds.yMax))
			{
				zero.x = (float)((int)Mathf.Floor(poly.bounds.xMin));
				while (zero.x < Mathf.Ceil(poly.bounds.xMax))
				{
					if (poly.Contains(zero))
					{
						int num = Grid.XYToCell((int)zero.x, (int)zero.y);
						if (Grid.IsValidCell(num))
						{
							array[num] = ((overworldCell.zoneType == SubWorld.ZoneType.Space) ? byte.MaxValue : ((byte)overworldCell.zoneType));
							this.worldZoneTypes[num] = overworldCell.zoneType;
						}
					}
					zero.x += 1f;
				}
				zero.y += 1f;
			}
		}
		this.InitSimZones(array);
	}

	// Token: 0x060040D3 RID: 16595 RVA: 0x0016AE83 File Offset: 0x00169083
	private void OnShadersReloaded()
	{
		Shader.SetGlobalTexture("_WorldZoneTex", this.colourTex);
		Shader.SetGlobalTexture("_WorldZoneIndexTex", this.indexTex);
	}

	// Token: 0x060040D4 RID: 16596 RVA: 0x0016AEA5 File Offset: 0x001690A5
	public SubWorld.ZoneType GetSubWorldZoneType(int cell)
	{
		if (cell >= 0 && cell < this.worldZoneTypes.Length)
		{
			return this.worldZoneTypes[cell];
		}
		return SubWorld.ZoneType.Sandstone;
	}

	// Token: 0x060040D5 RID: 16597 RVA: 0x0016AEC0 File Offset: 0x001690C0
	private SubWorld.ZoneType GetSubWorldZoneType(Vector2I pos)
	{
		WorldDetailSave clusterDetailSave = SaveLoader.Instance.clusterDetailSave;
		if (clusterDetailSave != null)
		{
			for (int i = 0; i < clusterDetailSave.overworldCells.Count; i++)
			{
				if (clusterDetailSave.overworldCells[i].poly.Contains(pos))
				{
					return clusterDetailSave.overworldCells[i].zoneType;
				}
			}
		}
		return SubWorld.ZoneType.Sandstone;
	}

	// Token: 0x060040D6 RID: 16598 RVA: 0x0016AF24 File Offset: 0x00169124
	private Color32 GetZoneColor(SubWorld.ZoneType zone_type)
	{
		Color32 color = new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, 3);
		bool flag = zone_type < (SubWorld.ZoneType)this.zoneColours.Length;
		string text = "Need to add more colours to handle this zone";
		int num = (int)zone_type;
		global::Debug.Assert(flag, text + num.ToString() + "<" + this.zoneColours.Length.ToString());
		return color;
	}

	// Token: 0x060040D7 RID: 16599 RVA: 0x0016AF80 File Offset: 0x00169180
	private unsafe void InitSimZones(byte[] bytes)
	{
		fixed (byte[] array = bytes)
		{
			byte* ptr;
			if (bytes == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			Sim.SIM_HandleMessage(-457308393, bytes.Length, ptr);
		}
	}

	// Token: 0x04002B36 RID: 11062
	[SerializeField]
	private Texture2D colourTex;

	// Token: 0x04002B37 RID: 11063
	[SerializeField]
	private Texture2D indexTex;

	// Token: 0x04002B38 RID: 11064
	[HideInInspector]
	public SubWorld.ZoneType[] worldZoneTypes;

	// Token: 0x04002B39 RID: 11065
	[SerializeField]
	[HideInInspector]
	public Color32[] zoneColours = new Color32[]
	{
		new Color32(145, 198, 213, 0),
		new Color32(135, 82, 160, 1),
		new Color32(123, 151, 75, 2),
		new Color32(236, 189, 89, 3),
		new Color32(201, 152, 181, 4),
		new Color32(222, 90, 59, 5),
		new Color32(201, 152, 181, 6),
		new Color32(byte.MaxValue, 0, 0, 7),
		new Color32(201, 201, 151, 8),
		new Color32(236, 90, 110, 9),
		new Color32(110, 236, 110, 10),
		new Color32(145, 198, 213, 11),
		new Color32(145, 198, 213, 12),
		new Color32(145, 198, 213, 13),
		new Color32(173, 222, 212, 14)
	};

	// Token: 0x04002B3A RID: 11066
	private const int NUM_COLOUR_BYTES = 3;

	// Token: 0x04002B3B RID: 11067
	public int[] zoneTextureArrayIndices = new int[]
	{
		0, 1, 2, 3, 4, 5, 5, 3, 6, 7,
		8, 9, 10, 11, 12, 7, 3, 13
	};
}
