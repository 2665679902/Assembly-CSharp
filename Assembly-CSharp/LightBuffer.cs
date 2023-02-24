using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020008C1 RID: 2241
public class LightBuffer : MonoBehaviour
{
	// Token: 0x06004088 RID: 16520 RVA: 0x00168670 File Offset: 0x00166870
	private void Awake()
	{
		LightBuffer.Instance = this;
		this.ColorRangeTag = Shader.PropertyToID("_ColorRange");
		this.LightPosTag = Shader.PropertyToID("_LightPos");
		this.LightDirectionAngleTag = Shader.PropertyToID("_LightDirectionAngle");
		this.TintColorTag = Shader.PropertyToID("_TintColor");
		this.Camera = base.GetComponent<Camera>();
		this.Layer = LayerMask.NameToLayer("Lights");
		this.Mesh = new Mesh();
		this.Mesh.name = "Light Mesh";
		this.Mesh.vertices = new Vector3[]
		{
			new Vector3(-1f, -1f, 0f),
			new Vector3(-1f, 1f, 0f),
			new Vector3(1f, -1f, 0f),
			new Vector3(1f, 1f, 0f)
		};
		this.Mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 0f),
			new Vector2(1f, 1f)
		};
		this.Mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
		this.Mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));
		this.Texture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBHalf);
		this.Texture.name = "LightBuffer";
		this.Camera.targetTexture = this.Texture;
	}

	// Token: 0x06004089 RID: 16521 RVA: 0x00168860 File Offset: 0x00166A60
	private void LateUpdate()
	{
		if (PropertyTextures.instance == null)
		{
			return;
		}
		if (this.Texture.width != Screen.width || this.Texture.height != Screen.height)
		{
			this.Texture.DestroyRenderTexture();
			this.Texture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGBHalf);
			this.Texture.name = "LightBuffer";
			this.Camera.targetTexture = this.Texture;
		}
		Matrix4x4 matrix4x = default(Matrix4x4);
		this.WorldLight = PropertyTextures.instance.GetTexture(PropertyTextures.Property.WorldLight);
		this.Material.SetTexture("_PropertyWorldLight", this.WorldLight);
		this.CircleMaterial.SetTexture("_PropertyWorldLight", this.WorldLight);
		this.ConeMaterial.SetTexture("_PropertyWorldLight", this.WorldLight);
		List<Light2D> list = Components.Light2Ds.Items;
		if (ClusterManager.Instance != null)
		{
			list = Components.Light2Ds.GetWorldItems(ClusterManager.Instance.activeWorldId, false);
		}
		if (list == null)
		{
			return;
		}
		foreach (Light2D light2D in list)
		{
			if (!(light2D == null) && light2D.enabled)
			{
				MaterialPropertyBlock materialPropertyBlock = light2D.materialPropertyBlock;
				materialPropertyBlock.SetVector(this.ColorRangeTag, new Vector4(light2D.Color.r * light2D.IntensityAnimation, light2D.Color.g * light2D.IntensityAnimation, light2D.Color.b * light2D.IntensityAnimation, light2D.Range));
				Vector3 position = light2D.transform.GetPosition();
				position.x += light2D.Offset.x;
				position.y += light2D.Offset.y;
				materialPropertyBlock.SetVector(this.LightPosTag, new Vector4(position.x, position.y, 0f, 0f));
				Vector2 normalized = light2D.Direction.normalized;
				materialPropertyBlock.SetVector(this.LightDirectionAngleTag, new Vector4(normalized.x, normalized.y, 0f, light2D.Angle));
				Graphics.DrawMesh(this.Mesh, Vector3.zero, Quaternion.identity, this.Material, this.Layer, this.Camera, 0, materialPropertyBlock, false, false);
				if (light2D.drawOverlay)
				{
					materialPropertyBlock.SetColor(this.TintColorTag, light2D.overlayColour);
					global::LightShape shape = light2D.shape;
					if (shape != global::LightShape.Circle)
					{
						if (shape == global::LightShape.Cone)
						{
							matrix4x.SetTRS(position - Vector3.up * (light2D.Range * 0.5f), Quaternion.identity, new Vector3(1f, 0.5f, 1f) * light2D.Range);
							Graphics.DrawMesh(this.Mesh, matrix4x, this.ConeMaterial, this.Layer, this.Camera, 0, materialPropertyBlock);
						}
					}
					else
					{
						matrix4x.SetTRS(position, Quaternion.identity, Vector3.one * light2D.Range);
						Graphics.DrawMesh(this.Mesh, matrix4x, this.CircleMaterial, this.Layer, this.Camera, 0, materialPropertyBlock);
					}
				}
			}
		}
	}

	// Token: 0x0600408A RID: 16522 RVA: 0x00168BD0 File Offset: 0x00166DD0
	private void OnDestroy()
	{
		LightBuffer.Instance = null;
	}

	// Token: 0x04002A40 RID: 10816
	private Mesh Mesh;

	// Token: 0x04002A41 RID: 10817
	private Camera Camera;

	// Token: 0x04002A42 RID: 10818
	[NonSerialized]
	public Material Material;

	// Token: 0x04002A43 RID: 10819
	[NonSerialized]
	public Material CircleMaterial;

	// Token: 0x04002A44 RID: 10820
	[NonSerialized]
	public Material ConeMaterial;

	// Token: 0x04002A45 RID: 10821
	private int ColorRangeTag;

	// Token: 0x04002A46 RID: 10822
	private int LightPosTag;

	// Token: 0x04002A47 RID: 10823
	private int LightDirectionAngleTag;

	// Token: 0x04002A48 RID: 10824
	private int TintColorTag;

	// Token: 0x04002A49 RID: 10825
	private int Layer;

	// Token: 0x04002A4A RID: 10826
	public RenderTexture Texture;

	// Token: 0x04002A4B RID: 10827
	public Texture WorldLight;

	// Token: 0x04002A4C RID: 10828
	public static LightBuffer Instance;

	// Token: 0x04002A4D RID: 10829
	private const RenderTextureFormat RTFormat = RenderTextureFormat.ARGBHalf;
}
