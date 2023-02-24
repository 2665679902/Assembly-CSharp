using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200061C RID: 1564
	public sealed class SessionDetails : Handle
	{
		// Token: 0x06003DCF RID: 15823 RVA: 0x000856C7 File Offset: 0x000838C7
		public SessionDetails(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003DD0 RID: 15824 RVA: 0x000856D0 File Offset: 0x000838D0
		public Result CopyInfo(SessionDetailsCopyInfoOptions options, out SessionDetailsInfo outSessionInfo)
		{
			SessionDetailsCopyInfoOptionsInternal sessionDetailsCopyInfoOptionsInternal = Helper.CopyProperties<SessionDetailsCopyInfoOptionsInternal>(options);
			outSessionInfo = Helper.GetDefault<SessionDetailsInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionDetails.EOS_SessionDetails_CopyInfo(base.InnerHandle, ref sessionDetailsCopyInfoOptionsInternal, ref zero);
			Helper.TryMarshalDispose<SessionDetailsCopyInfoOptionsInternal>(ref sessionDetailsCopyInfoOptionsInternal);
			if (Helper.TryMarshalGet<SessionDetailsInfoInternal, SessionDetailsInfo>(zero, out outSessionInfo))
			{
				SessionDetails.EOS_SessionDetails_Info_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003DD1 RID: 15825 RVA: 0x00085728 File Offset: 0x00083928
		public uint GetSessionAttributeCount(SessionDetailsGetSessionAttributeCountOptions options)
		{
			SessionDetailsGetSessionAttributeCountOptionsInternal sessionDetailsGetSessionAttributeCountOptionsInternal = Helper.CopyProperties<SessionDetailsGetSessionAttributeCountOptionsInternal>(options);
			uint num = SessionDetails.EOS_SessionDetails_GetSessionAttributeCount(base.InnerHandle, ref sessionDetailsGetSessionAttributeCountOptionsInternal);
			Helper.TryMarshalDispose<SessionDetailsGetSessionAttributeCountOptionsInternal>(ref sessionDetailsGetSessionAttributeCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06003DD2 RID: 15826 RVA: 0x00085760 File Offset: 0x00083960
		public Result CopySessionAttributeByIndex(SessionDetailsCopySessionAttributeByIndexOptions options, out SessionDetailsAttribute outSessionAttribute)
		{
			SessionDetailsCopySessionAttributeByIndexOptionsInternal sessionDetailsCopySessionAttributeByIndexOptionsInternal = Helper.CopyProperties<SessionDetailsCopySessionAttributeByIndexOptionsInternal>(options);
			outSessionAttribute = Helper.GetDefault<SessionDetailsAttribute>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionDetails.EOS_SessionDetails_CopySessionAttributeByIndex(base.InnerHandle, ref sessionDetailsCopySessionAttributeByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<SessionDetailsCopySessionAttributeByIndexOptionsInternal>(ref sessionDetailsCopySessionAttributeByIndexOptionsInternal);
			if (Helper.TryMarshalGet<SessionDetailsAttributeInternal, SessionDetailsAttribute>(zero, out outSessionAttribute))
			{
				SessionDetails.EOS_SessionDetails_Attribute_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003DD3 RID: 15827 RVA: 0x000857B8 File Offset: 0x000839B8
		public Result CopySessionAttributeByKey(SessionDetailsCopySessionAttributeByKeyOptions options, out SessionDetailsAttribute outSessionAttribute)
		{
			SessionDetailsCopySessionAttributeByKeyOptionsInternal sessionDetailsCopySessionAttributeByKeyOptionsInternal = Helper.CopyProperties<SessionDetailsCopySessionAttributeByKeyOptionsInternal>(options);
			outSessionAttribute = Helper.GetDefault<SessionDetailsAttribute>();
			IntPtr zero = IntPtr.Zero;
			Result result = SessionDetails.EOS_SessionDetails_CopySessionAttributeByKey(base.InnerHandle, ref sessionDetailsCopySessionAttributeByKeyOptionsInternal, ref zero);
			Helper.TryMarshalDispose<SessionDetailsCopySessionAttributeByKeyOptionsInternal>(ref sessionDetailsCopySessionAttributeByKeyOptionsInternal);
			if (Helper.TryMarshalGet<SessionDetailsAttributeInternal, SessionDetailsAttribute>(zero, out outSessionAttribute))
			{
				SessionDetails.EOS_SessionDetails_Attribute_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003DD4 RID: 15828 RVA: 0x0008580E File Offset: 0x00083A0E
		public void Release()
		{
			SessionDetails.EOS_SessionDetails_Release(base.InnerHandle);
		}

		// Token: 0x06003DD5 RID: 15829
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_SessionDetails_Info_Release(IntPtr sessionInfo);

		// Token: 0x06003DD6 RID: 15830
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_SessionDetails_Attribute_Release(IntPtr sessionAttribute);

		// Token: 0x06003DD7 RID: 15831
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_SessionDetails_Release(IntPtr sessionHandle);

		// Token: 0x06003DD8 RID: 15832
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionDetails_CopySessionAttributeByKey(IntPtr handle, ref SessionDetailsCopySessionAttributeByKeyOptionsInternal options, ref IntPtr outSessionAttribute);

		// Token: 0x06003DD9 RID: 15833
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionDetails_CopySessionAttributeByIndex(IntPtr handle, ref SessionDetailsCopySessionAttributeByIndexOptionsInternal options, ref IntPtr outSessionAttribute);

		// Token: 0x06003DDA RID: 15834
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_SessionDetails_GetSessionAttributeCount(IntPtr handle, ref SessionDetailsGetSessionAttributeCountOptionsInternal options);

		// Token: 0x06003DDB RID: 15835
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionDetails_CopyInfo(IntPtr handle, ref SessionDetailsCopyInfoOptionsInternal options, ref IntPtr outSessionInfo);
	}
}
