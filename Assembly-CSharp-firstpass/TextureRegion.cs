using System;
using Unity.Collections;

// Token: 0x02000048 RID: 72
public struct TextureRegion
{
	// Token: 0x06000306 RID: 774 RVA: 0x00010948 File Offset: 0x0000EB48
	public TextureRegion(int x, int y, int width, int height, TexturePage page, TextureBuffer buffer)
	{
		this.x = x;
		this.y = y;
		this.page = page;
		this.buffer = buffer;
		this.targetWidth = width;
		this.targetHeight = height;
		this.pageWidth = page.width;
		this.bytesPerPixel = TextureUtil.GetBytesPerPixel(page.format);
		this.bytes = page.texture.GetRawTextureData<byte>();
		this.floats = page.texture.GetRawTextureData<float>();
	}

	// Token: 0x06000307 RID: 775 RVA: 0x000109C8 File Offset: 0x0000EBC8
	private int GetByteIdx(int x, int y)
	{
		int num = x - this.x;
		int num2 = y - this.y;
		return (num + num2 * this.pageWidth) * this.bytesPerPixel;
	}

	// Token: 0x06000308 RID: 776 RVA: 0x000109F8 File Offset: 0x0000EBF8
	public void SetBytes(int x, int y, byte b0)
	{
		int byteIdx = this.GetByteIdx(x, y);
		this.bytes[byteIdx] = b0;
	}

	// Token: 0x06000309 RID: 777 RVA: 0x00010A1C File Offset: 0x0000EC1C
	public void SetBytes(int x, int y, byte b0, byte b1)
	{
		int byteIdx = this.GetByteIdx(x, y);
		this.bytes[byteIdx] = b0;
		this.bytes[byteIdx + 1] = b1;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00010A50 File Offset: 0x0000EC50
	public void SetBytes(int x, int y, byte b0, byte b1, byte b2)
	{
		int byteIdx = this.GetByteIdx(x, y);
		this.bytes[byteIdx] = b0;
		this.bytes[byteIdx + 1] = b1;
		this.bytes[byteIdx + 2] = b2;
	}

	// Token: 0x0600030B RID: 779 RVA: 0x00010A94 File Offset: 0x0000EC94
	public void SetBytes(int x, int y, byte b0, byte b1, byte b2, byte b3)
	{
		int byteIdx = this.GetByteIdx(x, y);
		this.bytes[byteIdx] = b0;
		this.bytes[byteIdx + 1] = b1;
		this.bytes[byteIdx + 2] = b2;
		this.bytes[byteIdx + 3] = b3;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00010AE8 File Offset: 0x0000ECE8
	public void SetBytes(int x, int y, float v0)
	{
		int num = this.GetByteIdx(x, y) / 4;
		this.floats[num] = v0;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x00010B10 File Offset: 0x0000ED10
	public void SetBytes(int x, int y, float v0, float v1)
	{
		int num = this.GetByteIdx(x, y) / 4;
		this.floats[num] = v0;
		this.floats[num + 1] = v1;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x00010B45 File Offset: 0x0000ED45
	public void Unlock()
	{
		this.buffer.Unlock(this);
	}

	// Token: 0x040003C4 RID: 964
	public int x;

	// Token: 0x040003C5 RID: 965
	public int y;

	// Token: 0x040003C6 RID: 966
	public int bytesPerPixel;

	// Token: 0x040003C7 RID: 967
	public NativeArray<byte> bytes;

	// Token: 0x040003C8 RID: 968
	public NativeArray<float> floats;

	// Token: 0x040003C9 RID: 969
	public int targetWidth;

	// Token: 0x040003CA RID: 970
	public int targetHeight;

	// Token: 0x040003CB RID: 971
	public int pageWidth;

	// Token: 0x040003CC RID: 972
	public TexturePage page;

	// Token: 0x040003CD RID: 973
	public TextureBuffer buffer;
}
