using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using UnityEngine;

// Token: 0x02000018 RID: 24
public class KBatchedAnimInstanceData
{
	// Token: 0x06000193 RID: 403 RVA: 0x00008C90 File Offset: 0x00006E90
	public KBatchedAnimInstanceData(KAnimConverter.IAnimConverter target)
	{
		this.target = target;
		this.converter = new KBatchedAnimInstanceData.AnimInstanceDataToByteConverter
		{
			bytes = new byte[112]
		};
		KBatchedAnimInstanceData.AnimInstanceData animInstanceData = this.converter.animInstanceData[0];
		animInstanceData.tintColour = Color.white;
		animInstanceData.highlightColour = Color.black;
		animInstanceData.overlayColour = Color.white;
		this.converter.animInstanceData[0] = animInstanceData;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00008D0F File Offset: 0x00006F0F
	public void SetClipRadius(float x, float y, float dist_sq, bool do_clip)
	{
		this.converter.animInstanceData[0].clipParameters = new Vector4(x, y, dist_sq, (float)(do_clip ? 1 : 0));
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00008D38 File Offset: 0x00006F38
	public void SetBlend(float amt)
	{
		this.converter.animInstanceData[0].blend = amt;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x00008D51 File Offset: 0x00006F51
	public Color GetOverlayColour()
	{
		return this.converter.animInstanceData[0].overlayColour;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x00008D69 File Offset: 0x00006F69
	public bool SetOverlayColour(Color color)
	{
		if (color != this.converter.animInstanceData[0].overlayColour)
		{
			this.converter.animInstanceData[0].overlayColour = color;
			return true;
		}
		return false;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00008DA3 File Offset: 0x00006FA3
	public Color GetTintColour()
	{
		return this.converter.animInstanceData[0].tintColour;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00008DBB File Offset: 0x00006FBB
	public bool SetTintColour(Color color)
	{
		if (color != this.converter.animInstanceData[0].tintColour)
		{
			this.converter.animInstanceData[0].tintColour = color;
			return true;
		}
		return false;
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00008DF5 File Offset: 0x00006FF5
	public Color GetHighlightcolour()
	{
		return this.converter.animInstanceData[0].highlightColour;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00008E0D File Offset: 0x0000700D
	public bool SetHighlightColour(Color color)
	{
		if (color != this.converter.animInstanceData[0].highlightColour)
		{
			this.converter.animInstanceData[0].highlightColour = color;
			return true;
		}
		return false;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00008E48 File Offset: 0x00007048
	public void WriteToTexture(NativeArray<byte> output_bytes, int output_index, int this_index)
	{
		KBatchedAnimInstanceData.AnimInstanceData animInstanceData = this.converter.animInstanceData[0];
		animInstanceData.curAnimFrameIndex = (float)this.target.GetCurrentFrameIndex();
		animInstanceData.thisIndex = (float)this_index;
		animInstanceData.currentAnimNumFrames = (float)(this.target.IsVisible() ? this.target.GetCurrentNumFrames() : 0);
		animInstanceData.currentAnimFirstFrameIdx = (float)this.target.GetFirstFrameIndex();
		if (!this.isTransformOverriden)
		{
			animInstanceData.transformMatrix = this.target.GetTransformMatrix();
		}
		this.converter.animInstanceData[0] = animInstanceData;
		NativeArray<byte>.Copy(this.converter.bytes, 0, output_bytes, output_index, 112);
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00008EFA File Offset: 0x000070FA
	public void SetOverrideTransformMatrix(Matrix2x3 transform_matrix)
	{
		this.isTransformOverriden = true;
		this.converter.animInstanceData[0].transformMatrix = transform_matrix;
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00008F1A File Offset: 0x0000711A
	public void ClearOverrideTransformMatrix()
	{
		this.isTransformOverriden = false;
	}

	// Token: 0x0400009C RID: 156
	public const int SIZE_IN_BYTES = 112;

	// Token: 0x0400009D RID: 157
	public const int SIZE_IN_FLOATS = 28;

	// Token: 0x0400009E RID: 158
	private KAnimConverter.IAnimConverter target;

	// Token: 0x0400009F RID: 159
	private bool isTransformOverriden;

	// Token: 0x040000A0 RID: 160
	private KBatchedAnimInstanceData.AnimInstanceDataToByteConverter converter;

	// Token: 0x0200096E RID: 2414
	[StructLayout(LayoutKind.Explicit)]
	public struct AnimInstanceData
	{
		// Token: 0x04002096 RID: 8342
		[FieldOffset(0)]
		public float curAnimFrameIndex;

		// Token: 0x04002097 RID: 8343
		[FieldOffset(4)]
		public float thisIndex;

		// Token: 0x04002098 RID: 8344
		[FieldOffset(8)]
		public float currentAnimNumFrames;

		// Token: 0x04002099 RID: 8345
		[FieldOffset(12)]
		public float currentAnimFirstFrameIdx;

		// Token: 0x0400209A RID: 8346
		[FieldOffset(16)]
		public Matrix2x3 transformMatrix;

		// Token: 0x0400209B RID: 8347
		[FieldOffset(40)]
		public float blend;

		// Token: 0x0400209C RID: 8348
		[FieldOffset(44)]
		public float unused;

		// Token: 0x0400209D RID: 8349
		[FieldOffset(48)]
		public Color highlightColour;

		// Token: 0x0400209E RID: 8350
		[FieldOffset(64)]
		public Color tintColour;

		// Token: 0x0400209F RID: 8351
		[FieldOffset(80)]
		public Color overlayColour;

		// Token: 0x040020A0 RID: 8352
		[FieldOffset(96)]
		public Vector4 clipParameters;
	}

	// Token: 0x0200096F RID: 2415
	[StructLayout(LayoutKind.Explicit)]
	public struct AnimInstanceDataToByteConverter
	{
		// Token: 0x040020A1 RID: 8353
		[FieldOffset(0)]
		public byte[] bytes;

		// Token: 0x040020A2 RID: 8354
		[FieldOffset(0)]
		public KBatchedAnimInstanceData.AnimInstanceData[] animInstanceData;
	}
}
