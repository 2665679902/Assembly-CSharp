using System;
using UnityEngine;

// Token: 0x020008A0 RID: 2208
public class FogOfWarPostFX : MonoBehaviour
{
	// Token: 0x06003F60 RID: 16224 RVA: 0x00161D94 File Offset: 0x0015FF94
	private void Awake()
	{
		if (this.shader != null)
		{
			this.material = new Material(this.shader);
		}
	}

	// Token: 0x06003F61 RID: 16225 RVA: 0x00161DB5 File Offset: 0x0015FFB5
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SetupUVs();
		Graphics.Blit(source, destination, this.material, 0);
	}

	// Token: 0x06003F62 RID: 16226 RVA: 0x00161DCC File Offset: 0x0015FFCC
	private void SetupUVs()
	{
		if (this.myCamera == null)
		{
			this.myCamera = base.GetComponent<Camera>();
			if (this.myCamera == null)
			{
				return;
			}
		}
		Ray ray = this.myCamera.ViewportPointToRay(Vector3.zero);
		float num = Mathf.Abs(ray.origin.z / ray.direction.z);
		Vector3 vector = ray.GetPoint(num);
		Vector4 vector2;
		vector2.x = vector.x / Grid.WidthInMeters;
		vector2.y = vector.y / Grid.HeightInMeters;
		ray = this.myCamera.ViewportPointToRay(Vector3.one);
		num = Mathf.Abs(ray.origin.z / ray.direction.z);
		vector = ray.GetPoint(num);
		vector2.z = vector.x / Grid.WidthInMeters - vector2.x;
		vector2.w = vector.y / Grid.HeightInMeters - vector2.y;
		this.material.SetVector("_UVOffsetScale", vector2);
	}

	// Token: 0x0400299C RID: 10652
	[SerializeField]
	private Shader shader;

	// Token: 0x0400299D RID: 10653
	private Material material;

	// Token: 0x0400299E RID: 10654
	private Camera myCamera;
}
