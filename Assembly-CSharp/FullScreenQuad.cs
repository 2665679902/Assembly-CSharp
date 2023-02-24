using System;
using UnityEngine;

// Token: 0x020008BE RID: 2238
public class FullScreenQuad
{
	// Token: 0x0600405A RID: 16474 RVA: 0x00167E74 File Offset: 0x00166074
	public FullScreenQuad(string name, Camera camera, bool invert = false)
	{
		this.Camera = camera;
		this.Layer = LayerMask.NameToLayer("ForceDraw");
		this.Mesh = new Mesh();
		this.Mesh.name = name;
		this.Mesh.vertices = new Vector3[]
		{
			new Vector3(-1f, -1f, 0f),
			new Vector3(-1f, 1f, 0f),
			new Vector3(1f, -1f, 0f),
			new Vector3(1f, 1f, 0f)
		};
		float num = 1f;
		float num2 = 0f;
		if (invert)
		{
			num = 0f;
			num2 = 1f;
		}
		this.Mesh.uv = new Vector2[]
		{
			new Vector2(0f, num2),
			new Vector2(0f, num),
			new Vector2(1f, num2),
			new Vector2(1f, num)
		};
		this.Mesh.triangles = new int[] { 0, 1, 2, 2, 1, 3 };
		this.Mesh.bounds = new Bounds(Vector3.zero, new Vector3(float.MaxValue, float.MaxValue, float.MaxValue));
		this.Material = new Material(Shader.Find("Klei/PostFX/FullScreen"));
		this.Camera.cullingMask = this.Camera.cullingMask | LayerMask.GetMask(new string[] { "ForceDraw" });
	}

	// Token: 0x0600405B RID: 16475 RVA: 0x0016802C File Offset: 0x0016622C
	public void Draw(Texture texture)
	{
		this.Material.mainTexture = texture;
		Graphics.DrawMesh(this.Mesh, Vector3.zero, Quaternion.identity, this.Material, this.Layer, this.Camera, 0, null, false, false);
	}

	// Token: 0x04002A2A RID: 10794
	private Mesh Mesh;

	// Token: 0x04002A2B RID: 10795
	private Camera Camera;

	// Token: 0x04002A2C RID: 10796
	private Material Material;

	// Token: 0x04002A2D RID: 10797
	private int Layer;
}
