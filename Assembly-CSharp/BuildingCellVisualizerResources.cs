using System;
using UnityEngine;

// Token: 0x020009E7 RID: 2535
public class BuildingCellVisualizerResources : ScriptableObject
{
	// Token: 0x170005A7 RID: 1447
	// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x001A940C File Offset: 0x001A760C
	// (set) Token: 0x06004BB5 RID: 19381 RVA: 0x001A9414 File Offset: 0x001A7614
	public Material backgroundMaterial { get; set; }

	// Token: 0x170005A8 RID: 1448
	// (get) Token: 0x06004BB6 RID: 19382 RVA: 0x001A941D File Offset: 0x001A761D
	// (set) Token: 0x06004BB7 RID: 19383 RVA: 0x001A9425 File Offset: 0x001A7625
	public Material iconBackgroundMaterial { get; set; }

	// Token: 0x170005A9 RID: 1449
	// (get) Token: 0x06004BB8 RID: 19384 RVA: 0x001A942E File Offset: 0x001A762E
	// (set) Token: 0x06004BB9 RID: 19385 RVA: 0x001A9436 File Offset: 0x001A7636
	public Material powerInputMaterial { get; set; }

	// Token: 0x170005AA RID: 1450
	// (get) Token: 0x06004BBA RID: 19386 RVA: 0x001A943F File Offset: 0x001A763F
	// (set) Token: 0x06004BBB RID: 19387 RVA: 0x001A9447 File Offset: 0x001A7647
	public Material powerOutputMaterial { get; set; }

	// Token: 0x170005AB RID: 1451
	// (get) Token: 0x06004BBC RID: 19388 RVA: 0x001A9450 File Offset: 0x001A7650
	// (set) Token: 0x06004BBD RID: 19389 RVA: 0x001A9458 File Offset: 0x001A7658
	public Material liquidInputMaterial { get; set; }

	// Token: 0x170005AC RID: 1452
	// (get) Token: 0x06004BBE RID: 19390 RVA: 0x001A9461 File Offset: 0x001A7661
	// (set) Token: 0x06004BBF RID: 19391 RVA: 0x001A9469 File Offset: 0x001A7669
	public Material liquidOutputMaterial { get; set; }

	// Token: 0x170005AD RID: 1453
	// (get) Token: 0x06004BC0 RID: 19392 RVA: 0x001A9472 File Offset: 0x001A7672
	// (set) Token: 0x06004BC1 RID: 19393 RVA: 0x001A947A File Offset: 0x001A767A
	public Material gasInputMaterial { get; set; }

	// Token: 0x170005AE RID: 1454
	// (get) Token: 0x06004BC2 RID: 19394 RVA: 0x001A9483 File Offset: 0x001A7683
	// (set) Token: 0x06004BC3 RID: 19395 RVA: 0x001A948B File Offset: 0x001A768B
	public Material gasOutputMaterial { get; set; }

	// Token: 0x170005AF RID: 1455
	// (get) Token: 0x06004BC4 RID: 19396 RVA: 0x001A9494 File Offset: 0x001A7694
	// (set) Token: 0x06004BC5 RID: 19397 RVA: 0x001A949C File Offset: 0x001A769C
	public Material highEnergyParticleInputMaterial { get; set; }

	// Token: 0x170005B0 RID: 1456
	// (get) Token: 0x06004BC6 RID: 19398 RVA: 0x001A94A5 File Offset: 0x001A76A5
	// (set) Token: 0x06004BC7 RID: 19399 RVA: 0x001A94AD File Offset: 0x001A76AD
	public Material highEnergyParticleOutputMaterial { get; set; }

	// Token: 0x170005B1 RID: 1457
	// (get) Token: 0x06004BC8 RID: 19400 RVA: 0x001A94B6 File Offset: 0x001A76B6
	// (set) Token: 0x06004BC9 RID: 19401 RVA: 0x001A94BE File Offset: 0x001A76BE
	public Mesh backgroundMesh { get; set; }

	// Token: 0x170005B2 RID: 1458
	// (get) Token: 0x06004BCA RID: 19402 RVA: 0x001A94C7 File Offset: 0x001A76C7
	// (set) Token: 0x06004BCB RID: 19403 RVA: 0x001A94CF File Offset: 0x001A76CF
	public Mesh iconMesh { get; set; }

	// Token: 0x170005B3 RID: 1459
	// (get) Token: 0x06004BCC RID: 19404 RVA: 0x001A94D8 File Offset: 0x001A76D8
	// (set) Token: 0x06004BCD RID: 19405 RVA: 0x001A94E0 File Offset: 0x001A76E0
	public int backgroundLayer { get; set; }

	// Token: 0x170005B4 RID: 1460
	// (get) Token: 0x06004BCE RID: 19406 RVA: 0x001A94E9 File Offset: 0x001A76E9
	// (set) Token: 0x06004BCF RID: 19407 RVA: 0x001A94F1 File Offset: 0x001A76F1
	public int iconLayer { get; set; }

	// Token: 0x06004BD0 RID: 19408 RVA: 0x001A94FA File Offset: 0x001A76FA
	public static void DestroyInstance()
	{
		BuildingCellVisualizerResources._Instance = null;
	}

	// Token: 0x06004BD1 RID: 19409 RVA: 0x001A9502 File Offset: 0x001A7702
	public static BuildingCellVisualizerResources Instance()
	{
		if (BuildingCellVisualizerResources._Instance == null)
		{
			BuildingCellVisualizerResources._Instance = Resources.Load<BuildingCellVisualizerResources>("BuildingCellVisualizerResources");
			BuildingCellVisualizerResources._Instance.Initialize();
		}
		return BuildingCellVisualizerResources._Instance;
	}

	// Token: 0x06004BD2 RID: 19410 RVA: 0x001A9530 File Offset: 0x001A7730
	private void Initialize()
	{
		Shader shader = Shader.Find("Klei/BuildingCell");
		this.backgroundMaterial = new Material(shader);
		this.backgroundMaterial.mainTexture = GlobalResources.Instance().WhiteTexture;
		this.iconBackgroundMaterial = new Material(shader);
		this.iconBackgroundMaterial.mainTexture = GlobalResources.Instance().WhiteTexture;
		this.powerInputMaterial = new Material(shader);
		this.powerOutputMaterial = new Material(shader);
		this.liquidInputMaterial = new Material(shader);
		this.liquidOutputMaterial = new Material(shader);
		this.gasInputMaterial = new Material(shader);
		this.gasOutputMaterial = new Material(shader);
		this.highEnergyParticleInputMaterial = new Material(shader);
		this.highEnergyParticleOutputMaterial = new Material(shader);
		this.backgroundMesh = this.CreateMesh("BuildingCellVisualizer", Vector2.zero, 0.5f);
		float num = 0.5f;
		this.iconMesh = this.CreateMesh("BuildingCellVisualizerIcon", Vector2.zero, num * 0.5f);
		this.backgroundLayer = LayerMask.NameToLayer("Default");
		this.iconLayer = LayerMask.NameToLayer("Place");
	}

	// Token: 0x06004BD3 RID: 19411 RVA: 0x001A9648 File Offset: 0x001A7848
	private Mesh CreateMesh(string name, Vector2 base_offset, float half_size)
	{
		Mesh mesh = new Mesh();
		mesh.name = name;
		mesh.vertices = new Vector3[]
		{
			new Vector3(-half_size + base_offset.x, -half_size + base_offset.y, 0f),
			new Vector3(half_size + base_offset.x, -half_size + base_offset.y, 0f),
			new Vector3(-half_size + base_offset.x, half_size + base_offset.y, 0f),
			new Vector3(half_size + base_offset.x, half_size + base_offset.y, 0f)
		};
		mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
		mesh.RecalculateBounds();
		return mesh;
	}

	// Token: 0x040031C0 RID: 12736
	[Header("Electricity")]
	public Color electricityInputColor;

	// Token: 0x040031C1 RID: 12737
	public Color electricityOutputColor;

	// Token: 0x040031C2 RID: 12738
	public Sprite electricityInputIcon;

	// Token: 0x040031C3 RID: 12739
	public Sprite electricityOutputIcon;

	// Token: 0x040031C4 RID: 12740
	public Sprite electricityConnectedIcon;

	// Token: 0x040031C5 RID: 12741
	public Sprite electricityBridgeIcon;

	// Token: 0x040031C6 RID: 12742
	public Sprite electricityBridgeConnectedIcon;

	// Token: 0x040031C7 RID: 12743
	public Sprite electricityArrowIcon;

	// Token: 0x040031C8 RID: 12744
	public Sprite switchIcon;

	// Token: 0x040031C9 RID: 12745
	public Color32 switchColor;

	// Token: 0x040031CA RID: 12746
	public Color32 switchOffColor = Color.red;

	// Token: 0x040031CB RID: 12747
	[Header("Gas")]
	public Sprite gasInputIcon;

	// Token: 0x040031CC RID: 12748
	public Sprite gasOutputIcon;

	// Token: 0x040031CD RID: 12749
	public BuildingCellVisualizerResources.IOColours gasIOColours;

	// Token: 0x040031CE RID: 12750
	[Header("Liquid")]
	public Sprite liquidInputIcon;

	// Token: 0x040031CF RID: 12751
	public Sprite liquidOutputIcon;

	// Token: 0x040031D0 RID: 12752
	public BuildingCellVisualizerResources.IOColours liquidIOColours;

	// Token: 0x040031D1 RID: 12753
	[Header("High Energy Particle")]
	public Sprite highEnergyParticleInputIcon;

	// Token: 0x040031D2 RID: 12754
	public Sprite[] highEnergyParticleOutputIcons;

	// Token: 0x040031D3 RID: 12755
	public Color highEnergyParticleInputColour;

	// Token: 0x040031D4 RID: 12756
	public Color highEnergyParticleOutputColour;

	// Token: 0x040031E3 RID: 12771
	private static BuildingCellVisualizerResources _Instance;

	// Token: 0x020017F1 RID: 6129
	[Serializable]
	public struct ConnectedDisconnectedColours
	{
		// Token: 0x04006E77 RID: 28279
		public Color32 connected;

		// Token: 0x04006E78 RID: 28280
		public Color32 disconnected;
	}

	// Token: 0x020017F2 RID: 6130
	[Serializable]
	public struct IOColours
	{
		// Token: 0x04006E79 RID: 28281
		public BuildingCellVisualizerResources.ConnectedDisconnectedColours input;

		// Token: 0x04006E7A RID: 28282
		public BuildingCellVisualizerResources.ConnectedDisconnectedColours output;
	}
}
