using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020009CF RID: 2511
[AddComponentMenu("KMonoBehaviour/scripts/WaterCubes")]
public class WaterCubes : KMonoBehaviour
{
	// Token: 0x1700057F RID: 1407
	// (get) Token: 0x06004AA4 RID: 19108 RVA: 0x001A1F59 File Offset: 0x001A0159
	// (set) Token: 0x06004AA5 RID: 19109 RVA: 0x001A1F60 File Offset: 0x001A0160
	public static WaterCubes Instance { get; private set; }

	// Token: 0x06004AA6 RID: 19110 RVA: 0x001A1F68 File Offset: 0x001A0168
	public static void DestroyInstance()
	{
		WaterCubes.Instance = null;
	}

	// Token: 0x06004AA7 RID: 19111 RVA: 0x001A1F70 File Offset: 0x001A0170
	protected override void OnPrefabInit()
	{
		WaterCubes.Instance = this;
	}

	// Token: 0x06004AA8 RID: 19112 RVA: 0x001A1F78 File Offset: 0x001A0178
	public void Init()
	{
		this.cubes = Util.NewGameObject(base.gameObject, "WaterCubes");
		GameObject gameObject = new GameObject();
		gameObject.name = "WaterCubesMesh";
		gameObject.transform.parent = this.cubes.transform;
		this.material.renderQueue = RenderQueues.Liquid;
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
		meshRenderer.sharedMaterial = this.material;
		meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		meshRenderer.receiveShadows = false;
		meshRenderer.lightProbeUsage = LightProbeUsage.Off;
		meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		meshRenderer.sharedMaterial.SetTexture("_MainTex2", this.waveTexture);
		meshFilter.sharedMesh = this.CreateNewMesh();
		meshRenderer.gameObject.layer = 0;
		meshRenderer.gameObject.transform.parent = base.transform;
		meshRenderer.gameObject.transform.SetPosition(new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.Liquid)));
	}

	// Token: 0x06004AA9 RID: 19113 RVA: 0x001A2070 File Offset: 0x001A0270
	private Mesh CreateNewMesh()
	{
		Mesh mesh = new Mesh();
		mesh.name = "WaterCubes";
		int num = 4;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		Vector3[] array3 = new Vector3[num];
		Vector4[] array4 = new Vector4[num];
		int[] array5 = new int[6];
		float layerZ = Grid.GetLayerZ(Grid.SceneLayer.Liquid);
		array = new Vector3[]
		{
			new Vector3(0f, 0f, layerZ),
			new Vector3((float)Grid.WidthInCells, 0f, layerZ),
			new Vector3(0f, Grid.HeightInMeters, layerZ),
			new Vector3(Grid.WidthInMeters, Grid.HeightInMeters, layerZ)
		};
		array2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		array3 = new Vector3[]
		{
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, -1f),
			new Vector3(0f, 0f, -1f)
		};
		array4 = new Vector4[]
		{
			new Vector4(0f, 1f, 0f, -1f),
			new Vector4(0f, 1f, 0f, -1f),
			new Vector4(0f, 1f, 0f, -1f),
			new Vector4(0f, 1f, 0f, -1f)
		};
		array5 = new int[] { 0, 2, 1, 1, 2, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.uv2 = array2;
		mesh.normals = array3;
		mesh.tangents = array4;
		mesh.triangles = array5;
		mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, 0f));
		return mesh;
	}

	// Token: 0x0400310D RID: 12557
	public Material material;

	// Token: 0x0400310E RID: 12558
	public Texture2D waveTexture;

	// Token: 0x0400310F RID: 12559
	private GameObject cubes;
}
