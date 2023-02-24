using System;
using System.Runtime.InteropServices;
using System.Text;

// Token: 0x02000136 RID: 310
public class UTF8Marshaler : ICustomMarshaler
{
	// Token: 0x06000A7F RID: 2687 RVA: 0x0002863C File Offset: 0x0002683C
	public IntPtr MarshalManagedToNative(object obj)
	{
		if (obj == null)
		{
			return IntPtr.Zero;
		}
		if (!(obj is string))
		{
			throw new MarshalDirectiveException("Invalid obj in UTF8Marshaler.");
		}
		byte[] bytes = Encoding.UTF8.GetBytes((string)obj);
		IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length + 1);
		Marshal.Copy(bytes, 0, intPtr, bytes.Length);
		Marshal.WriteByte((IntPtr)((long)intPtr + (long)bytes.Length), 0);
		return intPtr;
	}

	// Token: 0x06000A80 RID: 2688 RVA: 0x000286A3 File Offset: 0x000268A3
	public object MarshalNativeToManaged(IntPtr data)
	{
		return UTF8Marshaler.MarshalNativeToString(data);
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x000286AB File Offset: 0x000268AB
	public void CleanUpNativeData(IntPtr data)
	{
		Marshal.FreeHGlobal(data);
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x000286B3 File Offset: 0x000268B3
	public void CleanUpManagedData(object obj)
	{
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x000286B5 File Offset: 0x000268B5
	public int GetNativeDataSize()
	{
		return -1;
	}

	// Token: 0x06000A84 RID: 2692 RVA: 0x000286B8 File Offset: 0x000268B8
	public static ICustomMarshaler GetInstance(string cookie)
	{
		if (UTF8Marshaler.instance_ == null)
		{
			return UTF8Marshaler.instance_ = new UTF8Marshaler();
		}
		return UTF8Marshaler.instance_;
	}

	// Token: 0x06000A85 RID: 2693 RVA: 0x000286D4 File Offset: 0x000268D4
	public static string MarshalNativeToString(IntPtr data)
	{
		int num = 0;
		while (Marshal.ReadByte(data, num) != 0)
		{
			num++;
		}
		if (num == 0)
		{
			return string.Empty;
		}
		byte[] array = new byte[num];
		Marshal.Copy(data, array, 0, num);
		return Encoding.UTF8.GetString(array);
	}

	// Token: 0x040006E4 RID: 1764
	private static UTF8Marshaler instance_;
}
