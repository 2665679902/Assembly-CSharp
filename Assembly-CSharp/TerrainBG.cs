using System;
using UnityEngine;

// Token: 0x020009A7 RID: 2471
[AddComponentMenu("KMonoBehaviour/scripts/TerrainBG")]
public class TerrainBG : KMonoBehaviour
{
	// Token: 0x06004954 RID: 18772 RVA: 0x0019A83C File Offset: 0x00198A3C
	protected override void OnSpawn()
	{
		this.layer = LayerMask.NameToLayer("Default");
		this.noiseVolume = this.CreateTexture3D(32);
		this.starsPlane = this.CreateStarsPlane("StarsPlane");
		this.worldPlane = this.CreateWorldPlane("WorldPlane");
		this.gasPlane = this.CreateGasPlane("GasPlane");
		this.propertyBlocks = new MaterialPropertyBlock[Lighting.Instance.Settings.BackgroundLayers];
		for (int i = 0; i < this.propertyBlocks.Length; i++)
		{
			this.propertyBlocks[i] = new MaterialPropertyBlock();
		}
	}

	// Token: 0x06004955 RID: 18773 RVA: 0x0019A8D4 File Offset: 0x00198AD4
	private Texture3D CreateTexture3D(int size)
	{
		Color32[] array = new Color32[size * size * size];
		Texture3D texture3D = new Texture3D(size, size, size, TextureFormat.RGBA32, true);
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				for (int k = 0; k < size; k++)
				{
					Color32 color = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255));
					array[i + j * size + k * size * size] = color;
				}
			}
		}
		texture3D.SetPixels32(array);
		texture3D.Apply();
		return texture3D;
	}

	// Token: 0x06004956 RID: 18774 RVA: 0x0019A978 File Offset: 0x00198B78
	public Mesh CreateGasPlane(string name)
	{
		Mesh mesh = new Mesh();
		mesh.name = name;
		int num = 4;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		int[] array3 = new int[6];
		array = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3((float)Grid.WidthInCells, 0f, 0f),
			new Vector3(0f, Grid.HeightInMeters, 0f),
			new Vector3(Grid.WidthInMeters, Grid.HeightInMeters, 0f)
		};
		array2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		array3 = new int[] { 0, 2, 1, 1, 2, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		mesh.bounds = new Bounds(new Vector3((float)Grid.WidthInCells * 0.5f, (float)Grid.HeightInCells * 0.5f, 0f), new Vector3((float)Grid.WidthInCells, (float)Grid.HeightInCells, 0f));
		return mesh;
	}

	// Token: 0x06004957 RID: 18775 RVA: 0x0019AAE8 File Offset: 0x00198CE8
	public Mesh CreateWorldPlane(string name)
	{
		Mesh mesh = new Mesh();
		mesh.name = name;
		int num = 4;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		int[] array3 = new int[6];
		array = new Vector3[]
		{
			new Vector3((float)(-(float)Grid.WidthInCells), (float)(-(float)Grid.HeightInCells), 0f),
			new Vector3((float)Grid.WidthInCells * 2f, (float)(-(float)Grid.HeightInCells), 0f),
			new Vector3((float)(-(float)Grid.WidthInCells), Grid.HeightInMeters * 2f, 0f),
			new Vector3(Grid.WidthInMeters * 2f, Grid.HeightInMeters * 2f, 0f)
		};
		array2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		array3 = new int[] { 0, 2, 1, 1, 2, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		mesh.bounds = new Bounds(new Vector3((float)Grid.WidthInCells * 0.5f, (float)Grid.HeightInCells * 0.5f, 0f), new Vector3((float)Grid.WidthInCells, (float)Grid.HeightInCells, 0f));
		return mesh;
	}

	// Token: 0x06004958 RID: 18776 RVA: 0x0019AC78 File Offset: 0x00198E78
	public Mesh CreateStarsPlane(string name)
	{
		Mesh mesh = new Mesh();
		mesh.name = name;
		int num = 4;
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		int[] array3 = new int[6];
		array = new Vector3[]
		{
			new Vector3((float)(-(float)Grid.WidthInCells), (float)(-(float)Grid.HeightInCells), 0f),
			new Vector3((float)Grid.WidthInCells * 2f, (float)(-(float)Grid.HeightInCells), 0f),
			new Vector3((float)(-(float)Grid.WidthInCells), Grid.HeightInMeters * 2f, 0f),
			new Vector3(Grid.WidthInMeters * 2f, Grid.HeightInMeters * 2f, 0f)
		};
		array2 = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 1f)
		};
		array3 = new int[] { 0, 2, 1, 1, 2, 3 };
		mesh.vertices = array;
		mesh.uv = array2;
		mesh.triangles = array3;
		Vector2 vector = new Vector2((float)Grid.WidthInCells, 2f * (float)Grid.HeightInCells);
		mesh.bounds = new Bounds(new Vector3(0.5f * vector.x, 0.5f * vector.y, 0f), new Vector3(vector.x, vector.y, 0f));
		return mesh;
	}

	// Token: 0x06004959 RID: 18777 RVA: 0x0019AE28 File Offset: 0x00199028
	private void LateUpdate()
	{
		if (!this.doDraw)
		{
			return;
		}
		Material material = this.starsMaterial_surface;
		if (ClusterManager.Instance.activeWorld.IsModuleInterior)
		{
			Clustercraft component = ClusterManager.Instance.activeWorld.GetComponent<Clustercraft>();
			if (component.Status != Clustercraft.CraftStatus.InFlight)
			{
				material = this.starsMaterial_surface;
			}
			else if (ClusterGrid.Instance.GetVisibleEntityOfLayerAtAdjacentCell(component.Location, EntityLayer.Asteroid) != null)
			{
				material = this.starsMaterial_orbit;
			}
			else
			{
				material = this.starsMaterial_space;
			}
		}
		material.renderQueue = RenderQueues.Stars;
		material.SetTexture("_NoiseVolume", this.noiseVolume);
		Vector3 vector = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.Background) + 1f);
		Graphics.DrawMesh(this.starsPlane, vector, Quaternion.identity, material, this.layer);
		this.backgroundMaterial.renderQueue = RenderQueues.Backwall;
		for (int i = 0; i < Lighting.Instance.Settings.BackgroundLayers; i++)
		{
			if (i >= Lighting.Instance.Settings.BackgroundLayers - 1)
			{
				float num = (float)i / (float)(Lighting.Instance.Settings.BackgroundLayers - 1);
				float num2 = Mathf.Lerp(1f, Lighting.Instance.Settings.BackgroundDarkening, num);
				float num3 = Mathf.Lerp(1f, Lighting.Instance.Settings.BackgroundUVScale, num);
				float num4 = 1f;
				if (i == Lighting.Instance.Settings.BackgroundLayers - 1)
				{
					num4 = 0f;
				}
				MaterialPropertyBlock materialPropertyBlock = this.propertyBlocks[i];
				materialPropertyBlock.SetVector("_BackWallParameters", new Vector4(num2, Lighting.Instance.Settings.BackgroundClip, num3, num4));
				Vector3 vector2 = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.Background));
				Graphics.DrawMesh(this.worldPlane, vector2, Quaternion.identity, this.backgroundMaterial, this.layer, null, 0, materialPropertyBlock);
			}
		}
		this.gasMaterial.renderQueue = RenderQueues.Gas;
		Vector3 vector3 = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.Gas));
		Graphics.DrawMesh(this.gasPlane, vector3, Quaternion.identity, this.gasMaterial, this.layer);
		Vector3 vector4 = new Vector3(0f, 0f, Grid.GetLayerZ(Grid.SceneLayer.GasFront));
		Graphics.DrawMesh(this.gasPlane, vector4, Quaternion.identity, this.gasMaterial, this.layer);
	}

	// Token: 0x04003024 RID: 12324
	public Material starsMaterial_surface;

	// Token: 0x04003025 RID: 12325
	public Material starsMaterial_orbit;

	// Token: 0x04003026 RID: 12326
	public Material starsMaterial_space;

	// Token: 0x04003027 RID: 12327
	public Material backgroundMaterial;

	// Token: 0x04003028 RID: 12328
	public Material gasMaterial;

	// Token: 0x04003029 RID: 12329
	public bool doDraw = true;

	// Token: 0x0400302A RID: 12330
	[SerializeField]
	private Texture3D noiseVolume;

	// Token: 0x0400302B RID: 12331
	private Mesh starsPlane;

	// Token: 0x0400302C RID: 12332
	private Mesh worldPlane;

	// Token: 0x0400302D RID: 12333
	private Mesh gasPlane;

	// Token: 0x0400302E RID: 12334
	private int layer;

	// Token: 0x0400302F RID: 12335
	private MaterialPropertyBlock[] propertyBlocks;
}
