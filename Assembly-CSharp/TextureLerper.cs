using System;
using UnityEngine;
using UnityEngine.Rendering;

// Token: 0x020008D1 RID: 2257
public class TextureLerper
{
	// Token: 0x060040DA RID: 16602 RVA: 0x0016B184 File Offset: 0x00169384
	public TextureLerper(Texture target_texture, string name, FilterMode filter_mode = FilterMode.Bilinear, TextureFormat texture_format = TextureFormat.ARGB32)
	{
		this.name = name;
		this.Init(target_texture.width, target_texture.height, name, filter_mode, texture_format);
		this.Material.SetTexture("_TargetTex", target_texture);
	}

	// Token: 0x060040DB RID: 16603 RVA: 0x0016B1DC File Offset: 0x001693DC
	private void Init(int width, int height, string name, FilterMode filter_mode, TextureFormat texture_format)
	{
		for (int i = 0; i < 2; i++)
		{
			this.BlendTextures[i] = new RenderTexture(width, height, 0, TextureUtil.GetRenderTextureFormat(texture_format));
			this.BlendTextures[i].filterMode = filter_mode;
			this.BlendTextures[i].name = name;
		}
		this.Material = new Material(Shader.Find("Klei/LerpEffect"));
		this.Material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.None;
		this.mesh = new Mesh();
		this.mesh.name = "LerpEffect";
		this.mesh.vertices = new Vector3[]
		{
			new Vector3(0f, 0f, 0f),
			new Vector3(1f, 1f, 0f),
			new Vector3(0f, 1f, 0f),
			new Vector3(1f, 0f, 0f)
		};
		this.mesh.triangles = new int[] { 0, 1, 2, 0, 3, 1 };
		this.mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(1f, 1f),
			new Vector2(0f, 1f),
			new Vector2(1f, 0f)
		};
		int num = LayerMask.NameToLayer("RTT");
		int mask = LayerMask.GetMask(new string[] { "RTT" });
		this.cameraGO = new GameObject();
		this.cameraGO.name = "TextureLerper_" + name;
		this.textureCam = this.cameraGO.AddComponent<Camera>();
		this.textureCam.transform.SetPosition(new Vector3((float)TextureLerper.offsetCounter + 0.5f, 0.5f, 0f));
		this.textureCam.clearFlags = CameraClearFlags.Nothing;
		this.textureCam.depth = -100f;
		this.textureCam.allowHDR = false;
		this.textureCam.orthographic = true;
		this.textureCam.orthographicSize = 0.5f;
		this.textureCam.cullingMask = mask;
		this.textureCam.targetTexture = this.dest;
		this.textureCam.nearClipPlane = -5f;
		this.textureCam.farClipPlane = 5f;
		this.textureCam.useOcclusionCulling = false;
		this.textureCam.aspect = 1f;
		this.textureCam.rect = new Rect(0f, 0f, 1f, 1f);
		this.meshGO = new GameObject();
		this.meshGO.name = "mesh";
		this.meshGO.transform.parent = this.cameraGO.transform;
		this.meshGO.transform.SetLocalPosition(new Vector3(-0.5f, -0.5f, 0f));
		this.meshGO.isStatic = true;
		MeshRenderer meshRenderer = this.meshGO.AddComponent<MeshRenderer>();
		meshRenderer.receiveShadows = false;
		meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		meshRenderer.lightProbeUsage = LightProbeUsage.Off;
		meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		this.meshGO.AddComponent<MeshFilter>().mesh = this.mesh;
		meshRenderer.sharedMaterial = this.Material;
		this.cameraGO.SetLayerRecursively(num);
		TextureLerper.offsetCounter++;
	}

	// Token: 0x060040DC RID: 16604 RVA: 0x0016B56C File Offset: 0x0016976C
	public void LongUpdate(float dt)
	{
		this.BlendDt = dt;
		this.BlendTime = 0f;
	}

	// Token: 0x060040DD RID: 16605 RVA: 0x0016B580 File Offset: 0x00169780
	public Texture Update()
	{
		float num = Time.deltaTime * this.Speed;
		if (Time.deltaTime == 0f)
		{
			num = Time.unscaledDeltaTime * this.Speed;
		}
		float num2 = Mathf.Min(num / Mathf.Max(this.BlendDt - this.BlendTime, 0f), 1f);
		this.BlendTime += num;
		if (GameUtil.IsCapturingTimeLapse())
		{
			num2 = 1f;
		}
		this.source = this.BlendTextures[this.BlendIdx];
		this.BlendIdx = (this.BlendIdx + 1) % 2;
		this.dest = this.BlendTextures[this.BlendIdx];
		Vector4 visibleCellRange = this.GetVisibleCellRange();
		visibleCellRange = new Vector4(0f, 0f, (float)Grid.WidthInCells, (float)Grid.HeightInCells);
		this.Material.SetFloat("_Lerp", num2);
		this.Material.SetTexture("_SourceTex", this.source);
		this.Material.SetVector("_MeshParams", visibleCellRange);
		this.textureCam.targetTexture = this.dest;
		return this.dest;
	}

	// Token: 0x060040DE RID: 16606 RVA: 0x0016B69C File Offset: 0x0016989C
	private Vector4 GetVisibleCellRange()
	{
		Camera main = Camera.main;
		float cellSizeInMeters = Grid.CellSizeInMeters;
		Ray ray = main.ViewportPointToRay(Vector3.zero);
		float num = Mathf.Abs(ray.origin.z / ray.direction.z);
		Vector3 vector = ray.GetPoint(num);
		int num2 = Grid.PosToCell(vector);
		float num3 = -Grid.HalfCellSizeInMeters;
		vector = Grid.CellToPos(num2, num3, num3, num3);
		int num4 = Math.Max(0, (int)(vector.x / cellSizeInMeters));
		int num5 = Math.Max(0, (int)(vector.y / cellSizeInMeters));
		ray = main.ViewportPointToRay(Vector3.one);
		num = Mathf.Abs(ray.origin.z / ray.direction.z);
		vector = ray.GetPoint(num);
		int num6 = Mathf.CeilToInt(vector.x / cellSizeInMeters);
		int num7 = Mathf.CeilToInt(vector.y / cellSizeInMeters);
		num6 = Mathf.Min(num6, Grid.WidthInCells - 1);
		num7 = Mathf.Min(num7, Grid.HeightInCells - 1);
		return new Vector4((float)num4, (float)num5, (float)num6, (float)num7);
	}

	// Token: 0x04002B3C RID: 11068
	private static int offsetCounter;

	// Token: 0x04002B3D RID: 11069
	public string name;

	// Token: 0x04002B3E RID: 11070
	private RenderTexture[] BlendTextures = new RenderTexture[2];

	// Token: 0x04002B3F RID: 11071
	private float BlendDt;

	// Token: 0x04002B40 RID: 11072
	private float BlendTime;

	// Token: 0x04002B41 RID: 11073
	private int BlendIdx;

	// Token: 0x04002B42 RID: 11074
	private Material Material;

	// Token: 0x04002B43 RID: 11075
	public float Speed = 1f;

	// Token: 0x04002B44 RID: 11076
	private Mesh mesh;

	// Token: 0x04002B45 RID: 11077
	private RenderTexture source;

	// Token: 0x04002B46 RID: 11078
	private RenderTexture dest;

	// Token: 0x04002B47 RID: 11079
	private GameObject meshGO;

	// Token: 0x04002B48 RID: 11080
	private GameObject cameraGO;

	// Token: 0x04002B49 RID: 11081
	private Camera textureCam;

	// Token: 0x04002B4A RID: 11082
	private float blend;
}
