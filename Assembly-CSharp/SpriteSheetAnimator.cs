using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D0 RID: 1232
public class SpriteSheetAnimator
{
	// Token: 0x06001C8A RID: 7306 RVA: 0x000980AC File Offset: 0x000962AC
	public SpriteSheetAnimator(SpriteSheet sheet)
	{
		this.sheet = sheet;
		this.mesh = new Mesh();
		this.mesh.name = "SpriteSheetAnimator";
		this.mesh.MarkDynamic();
		this.materialProperties = new MaterialPropertyBlock();
		this.materialProperties.SetTexture("_MainTex", sheet.texture);
	}

	// Token: 0x06001C8B RID: 7307 RVA: 0x00098124 File Offset: 0x00096324
	public void Play(Vector3 pos, Quaternion rotation, Vector2 size, Color colour)
	{
		if (rotation == Quaternion.identity)
		{
			this.anims.Add(new SpriteSheetAnimator.AnimInfo
			{
				elapsedTime = 0f,
				pos = pos,
				rotation = rotation,
				size = size,
				colour = colour
			});
			return;
		}
		this.rotatedAnims.Add(new SpriteSheetAnimator.AnimInfo
		{
			elapsedTime = 0f,
			pos = pos,
			rotation = rotation,
			size = size,
			colour = colour
		});
	}

	// Token: 0x06001C8C RID: 7308 RVA: 0x000981CC File Offset: 0x000963CC
	private void GetUVs(int frame, out Vector2 uv_bl, out Vector2 uv_br, out Vector2 uv_tl, out Vector2 uv_tr)
	{
		int num = frame / this.sheet.numXFrames;
		int num2 = frame % this.sheet.numXFrames;
		float num3 = (float)num2 * this.sheet.uvFrameSize.x;
		float num4 = (float)(num2 + 1) * this.sheet.uvFrameSize.x;
		float num5 = 1f - (float)(num + 1) * this.sheet.uvFrameSize.y;
		float num6 = 1f - (float)num * this.sheet.uvFrameSize.y;
		uv_bl = new Vector2(num3, num5);
		uv_br = new Vector2(num4, num5);
		uv_tl = new Vector2(num3, num6);
		uv_tr = new Vector2(num4, num6);
	}

	// Token: 0x06001C8D RID: 7309 RVA: 0x0009828C File Offset: 0x0009648C
	public int GetFrameFromElapsedTime(float elapsed_time)
	{
		return Mathf.Min(this.sheet.numFrames, (int)(elapsed_time / 0.033333335f));
	}

	// Token: 0x06001C8E RID: 7310 RVA: 0x000982A8 File Offset: 0x000964A8
	public int GetFrameFromElapsedTimeLooping(float elapsed_time)
	{
		int num = (int)(elapsed_time / 0.033333335f);
		if (num > this.sheet.numFrames)
		{
			num %= this.sheet.numFrames;
		}
		return num;
	}

	// Token: 0x06001C8F RID: 7311 RVA: 0x000982DB File Offset: 0x000964DB
	public void UpdateAnims(float dt)
	{
		this.UpdateAnims(dt, this.anims);
		this.UpdateAnims(dt, this.rotatedAnims);
	}

	// Token: 0x06001C90 RID: 7312 RVA: 0x000982F8 File Offset: 0x000964F8
	private void UpdateAnims(float dt, IList<SpriteSheetAnimator.AnimInfo> anims)
	{
		int num = anims.Count;
		int i = 0;
		while (i < num)
		{
			SpriteSheetAnimator.AnimInfo animInfo = anims[i];
			animInfo.elapsedTime += dt;
			animInfo.frame = Mathf.Min(this.sheet.numFrames, (int)(animInfo.elapsedTime / 0.033333335f));
			if (animInfo.frame >= this.sheet.numFrames)
			{
				num--;
				anims[i] = anims[num];
				anims.RemoveAt(num);
			}
			else
			{
				anims[i] = animInfo;
				i++;
			}
		}
	}

	// Token: 0x06001C91 RID: 7313 RVA: 0x00098388 File Offset: 0x00096588
	public void Render(List<SpriteSheetAnimator.AnimInfo> anim_infos, bool apply_rotation)
	{
		ListPool<Vector3, SpriteSheetAnimManager>.PooledList pooledList = ListPool<Vector3, SpriteSheetAnimManager>.Allocate();
		ListPool<Vector2, SpriteSheetAnimManager>.PooledList pooledList2 = ListPool<Vector2, SpriteSheetAnimManager>.Allocate();
		ListPool<Color32, SpriteSheetAnimManager>.PooledList pooledList3 = ListPool<Color32, SpriteSheetAnimManager>.Allocate();
		ListPool<int, SpriteSheetAnimManager>.PooledList pooledList4 = ListPool<int, SpriteSheetAnimManager>.Allocate();
		this.mesh.Clear();
		if (apply_rotation)
		{
			int count = anim_infos.Count;
			for (int i = 0; i < count; i++)
			{
				SpriteSheetAnimator.AnimInfo animInfo = anim_infos[i];
				Vector2 vector = animInfo.size * 0.5f;
				Vector3 vector2 = animInfo.rotation * -vector;
				Vector3 vector3 = animInfo.rotation * new Vector2(vector.x, -vector.y);
				Vector3 vector4 = animInfo.rotation * new Vector2(-vector.x, vector.y);
				Vector3 vector5 = animInfo.rotation * vector;
				pooledList.Add(animInfo.pos + vector2);
				pooledList.Add(animInfo.pos + vector3);
				pooledList.Add(animInfo.pos + vector5);
				pooledList.Add(animInfo.pos + vector4);
				Vector2 vector6;
				Vector2 vector7;
				Vector2 vector8;
				Vector2 vector9;
				this.GetUVs(animInfo.frame, out vector6, out vector7, out vector8, out vector9);
				pooledList2.Add(vector6);
				pooledList2.Add(vector7);
				pooledList2.Add(vector9);
				pooledList2.Add(vector8);
				pooledList3.Add(animInfo.colour);
				pooledList3.Add(animInfo.colour);
				pooledList3.Add(animInfo.colour);
				pooledList3.Add(animInfo.colour);
				int num = i * 4;
				pooledList4.Add(num);
				pooledList4.Add(num + 1);
				pooledList4.Add(num + 2);
				pooledList4.Add(num);
				pooledList4.Add(num + 2);
				pooledList4.Add(num + 3);
			}
		}
		else
		{
			int count2 = anim_infos.Count;
			for (int j = 0; j < count2; j++)
			{
				SpriteSheetAnimator.AnimInfo animInfo2 = anim_infos[j];
				Vector2 vector10 = animInfo2.size * 0.5f;
				Vector3 vector11 = -vector10;
				Vector3 vector12 = new Vector2(vector10.x, -vector10.y);
				Vector3 vector13 = new Vector2(-vector10.x, vector10.y);
				Vector3 vector14 = vector10;
				pooledList.Add(animInfo2.pos + vector11);
				pooledList.Add(animInfo2.pos + vector12);
				pooledList.Add(animInfo2.pos + vector14);
				pooledList.Add(animInfo2.pos + vector13);
				Vector2 vector15;
				Vector2 vector16;
				Vector2 vector17;
				Vector2 vector18;
				this.GetUVs(animInfo2.frame, out vector15, out vector16, out vector17, out vector18);
				pooledList2.Add(vector15);
				pooledList2.Add(vector16);
				pooledList2.Add(vector18);
				pooledList2.Add(vector17);
				pooledList3.Add(animInfo2.colour);
				pooledList3.Add(animInfo2.colour);
				pooledList3.Add(animInfo2.colour);
				pooledList3.Add(animInfo2.colour);
				int num2 = j * 4;
				pooledList4.Add(num2);
				pooledList4.Add(num2 + 1);
				pooledList4.Add(num2 + 2);
				pooledList4.Add(num2);
				pooledList4.Add(num2 + 2);
				pooledList4.Add(num2 + 3);
			}
		}
		this.mesh.SetVertices(pooledList);
		this.mesh.SetUVs(0, pooledList2);
		this.mesh.SetColors(pooledList3);
		this.mesh.SetTriangles(pooledList4, 0);
		Graphics.DrawMesh(this.mesh, Vector3.zero, Quaternion.identity, this.sheet.material, this.sheet.renderLayer, null, 0, this.materialProperties);
		pooledList4.Recycle();
		pooledList3.Recycle();
		pooledList2.Recycle();
		pooledList.Recycle();
	}

	// Token: 0x06001C92 RID: 7314 RVA: 0x00098770 File Offset: 0x00096970
	public void Render()
	{
		this.Render(this.anims, false);
		this.Render(this.rotatedAnims, true);
	}

	// Token: 0x04001015 RID: 4117
	private SpriteSheet sheet;

	// Token: 0x04001016 RID: 4118
	private Mesh mesh;

	// Token: 0x04001017 RID: 4119
	private MaterialPropertyBlock materialProperties;

	// Token: 0x04001018 RID: 4120
	private List<SpriteSheetAnimator.AnimInfo> anims = new List<SpriteSheetAnimator.AnimInfo>();

	// Token: 0x04001019 RID: 4121
	private List<SpriteSheetAnimator.AnimInfo> rotatedAnims = new List<SpriteSheetAnimator.AnimInfo>();

	// Token: 0x02001113 RID: 4371
	public struct AnimInfo
	{
		// Token: 0x040059B7 RID: 22967
		public int frame;

		// Token: 0x040059B8 RID: 22968
		public float elapsedTime;

		// Token: 0x040059B9 RID: 22969
		public Vector3 pos;

		// Token: 0x040059BA RID: 22970
		public Quaternion rotation;

		// Token: 0x040059BB RID: 22971
		public Vector2 size;

		// Token: 0x040059BC RID: 22972
		public Color32 colour;
	}
}
