using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEngine;

namespace KSerialization
{
	// Token: 0x0200050B RID: 1291
	public static class IOHelper
	{
		// Token: 0x0600373E RID: 14142 RVA: 0x0007C8A8 File Offset: 0x0007AAA8
		public static void WriteKleiString(this BinaryWriter writer, string str)
		{
			if (str == null)
			{
				writer.Write(-1);
				return;
			}
			Encoding utf = Encoding.UTF8;
			int byteCount = utf.GetByteCount(str);
			writer.Write(byteCount);
			if (byteCount < IOHelper.s_stringBuffer.Length)
			{
				utf.GetBytes(str, 0, str.Length, IOHelper.s_stringBuffer, 0);
				writer.Write(IOHelper.s_stringBuffer, 0, byteCount);
				return;
			}
			global::Debug.LogWarning(string.Format("Writing large string {0} of {1} bytes", str, byteCount));
			writer.Write(utf.GetBytes(str));
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x0007C928 File Offset: 0x0007AB28
		public unsafe static void WriteSingleFast(this BinaryWriter writer, float value)
		{
			byte* ptr = (byte*)(&value);
			if (BitConverter.IsLittleEndian)
			{
				IOHelper.s_singleBuffer[0] = *ptr;
				IOHelper.s_singleBuffer[1] = ptr[1];
				IOHelper.s_singleBuffer[2] = ptr[2];
				IOHelper.s_singleBuffer[3] = ptr[3];
			}
			else
			{
				IOHelper.s_singleBuffer[0] = ptr[3];
				IOHelper.s_singleBuffer[1] = ptr[2];
				IOHelper.s_singleBuffer[2] = ptr[1];
				IOHelper.s_singleBuffer[3] = *ptr;
			}
			writer.Write(IOHelper.s_singleBuffer);
		}

		// Token: 0x06003740 RID: 14144 RVA: 0x0007C9A1 File Offset: 0x0007ABA1
		[Conditional("DEBUG_VALIDATE")]
		public static void WriteBoundaryTag(this BinaryWriter writer, object tag)
		{
			writer.Write((uint)tag);
		}

		// Token: 0x06003741 RID: 14145 RVA: 0x0007C9B0 File Offset: 0x0007ABB0
		[Conditional("DEBUG_VALIDATE")]
		public static void CheckBoundaryTag(this IReader reader, object expected)
		{
			uint num = reader.ReadUInt32();
			if ((uint)expected != num)
			{
				global::Debug.LogError(string.Format("Expected Tag {0}(0x{1:X}) but got 0x{2:X} instead", expected.ToString(), (uint)expected, num));
			}
		}

		// Token: 0x06003742 RID: 14146 RVA: 0x0007C9F3 File Offset: 0x0007ABF3
		[Conditional("DEBUG_VALIDATE")]
		public static void Assert(bool condition)
		{
			DebugUtil.Assert(condition);
		}

		// Token: 0x06003743 RID: 14147 RVA: 0x0007C9FC File Offset: 0x0007ABFC
		public static Vector2I ReadVector2I(this IReader reader)
		{
			Vector2I vector2I;
			vector2I.x = reader.ReadInt32();
			vector2I.y = reader.ReadInt32();
			return vector2I;
		}

		// Token: 0x06003744 RID: 14148 RVA: 0x0007CA24 File Offset: 0x0007AC24
		public static Vector2 ReadVector2(this IReader reader)
		{
			Vector2 vector;
			vector.x = reader.ReadSingle();
			vector.y = reader.ReadSingle();
			return vector;
		}

		// Token: 0x06003745 RID: 14149 RVA: 0x0007CA4C File Offset: 0x0007AC4C
		public static Vector3 ReadVector3(this IReader reader)
		{
			Vector3 vector;
			vector.x = reader.ReadSingle();
			vector.y = reader.ReadSingle();
			vector.z = reader.ReadSingle();
			return vector;
		}

		// Token: 0x06003746 RID: 14150 RVA: 0x0007CA84 File Offset: 0x0007AC84
		public static Color ReadColour(this IReader reader)
		{
			byte b = reader.ReadByte();
			byte b2 = reader.ReadByte();
			byte b3 = reader.ReadByte();
			byte b4 = reader.ReadByte();
			Color color;
			color.r = (float)b / 255f;
			color.g = (float)b2 / 255f;
			color.b = (float)b3 / 255f;
			color.a = (float)b4 / 255f;
			return color;
		}

		// Token: 0x040013F5 RID: 5109
		private static byte[] s_stringBuffer = new byte[1024];

		// Token: 0x040013F6 RID: 5110
		private static byte[] s_singleBuffer = new byte[4];
	}
}
