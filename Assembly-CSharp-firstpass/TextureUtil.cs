using System;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

// Token: 0x02000049 RID: 73
public static class TextureUtil
{
	// Token: 0x0600030F RID: 783 RVA: 0x00010B58 File Offset: 0x0000ED58
	public static GraphicsFormat TextureFormatToGraphicsFormat(TextureFormat format)
	{
		switch (format)
		{
		case TextureFormat.Alpha8:
			return GraphicsFormat.R8_UNorm;
		case TextureFormat.ARGB4444:
			break;
		case TextureFormat.RGB24:
			return GraphicsFormat.R8G8B8_SRGB;
		case TextureFormat.RGBA32:
			return GraphicsFormat.R8G8B8A8_SRGB;
		default:
			if (format == TextureFormat.RGFloat)
			{
				return GraphicsFormat.R32G32_SFloat;
			}
			if (format == TextureFormat.RGBAFloat)
			{
				return GraphicsFormat.R32G32B32A32_SFloat;
			}
			break;
		}
		global::Debug.LogError("Unspecfied graphics format for texture format: " + format.ToString());
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00010BB8 File Offset: 0x0000EDB8
	public static int GetBytesPerPixel(TextureFormat format)
	{
		switch (format)
		{
		case TextureFormat.Alpha8:
			return 1;
		case TextureFormat.ARGB4444:
			break;
		case TextureFormat.RGB24:
			return 3;
		case TextureFormat.RGBA32:
			return 4;
		case TextureFormat.ARGB32:
			return 4;
		default:
			switch (format)
			{
			case TextureFormat.RFloat:
				return 4;
			case TextureFormat.RGFloat:
				return 8;
			case TextureFormat.RGBAFloat:
				return 16;
			}
			break;
		}
		throw new ArgumentOutOfRangeException();
	}

	// Token: 0x06000311 RID: 785 RVA: 0x00010C0C File Offset: 0x0000EE0C
	public static RenderTextureFormat GetRenderTextureFormat(TextureFormat format)
	{
		switch (format)
		{
		case TextureFormat.Alpha8:
			return RenderTextureFormat.ARGB32;
		case TextureFormat.ARGB4444:
			break;
		case TextureFormat.RGB24:
			return RenderTextureFormat.ARGB32;
		case TextureFormat.RGBA32:
			return RenderTextureFormat.ARGB32;
		case TextureFormat.ARGB32:
			return RenderTextureFormat.ARGB32;
		default:
			switch (format)
			{
			case TextureFormat.RFloat:
				return RenderTextureFormat.RFloat;
			case TextureFormat.RGFloat:
				return RenderTextureFormat.RGFloat;
			case TextureFormat.RGBAFloat:
				return RenderTextureFormat.ARGBHalf;
			}
			break;
		}
		throw new ArgumentOutOfRangeException();
	}
}
