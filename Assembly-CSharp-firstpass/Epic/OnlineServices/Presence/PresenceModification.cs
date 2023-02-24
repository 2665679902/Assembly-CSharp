using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200067F RID: 1663
	public sealed class PresenceModification : Handle
	{
		// Token: 0x06004065 RID: 16485 RVA: 0x00088660 File Offset: 0x00086860
		public PresenceModification(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x0008866C File Offset: 0x0008686C
		public Result SetStatus(PresenceModificationSetStatusOptions options)
		{
			PresenceModificationSetStatusOptionsInternal presenceModificationSetStatusOptionsInternal = Helper.CopyProperties<PresenceModificationSetStatusOptionsInternal>(options);
			Result result = PresenceModification.EOS_PresenceModification_SetStatus(base.InnerHandle, ref presenceModificationSetStatusOptionsInternal);
			Helper.TryMarshalDispose<PresenceModificationSetStatusOptionsInternal>(ref presenceModificationSetStatusOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x000886A4 File Offset: 0x000868A4
		public Result SetRawRichText(PresenceModificationSetRawRichTextOptions options)
		{
			PresenceModificationSetRawRichTextOptionsInternal presenceModificationSetRawRichTextOptionsInternal = Helper.CopyProperties<PresenceModificationSetRawRichTextOptionsInternal>(options);
			Result result = PresenceModification.EOS_PresenceModification_SetRawRichText(base.InnerHandle, ref presenceModificationSetRawRichTextOptionsInternal);
			Helper.TryMarshalDispose<PresenceModificationSetRawRichTextOptionsInternal>(ref presenceModificationSetRawRichTextOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004068 RID: 16488 RVA: 0x000886DC File Offset: 0x000868DC
		public Result SetData(PresenceModificationSetDataOptions options)
		{
			PresenceModificationSetDataOptionsInternal presenceModificationSetDataOptionsInternal = Helper.CopyProperties<PresenceModificationSetDataOptionsInternal>(options);
			Result result = PresenceModification.EOS_PresenceModification_SetData(base.InnerHandle, ref presenceModificationSetDataOptionsInternal);
			Helper.TryMarshalDispose<PresenceModificationSetDataOptionsInternal>(ref presenceModificationSetDataOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004069 RID: 16489 RVA: 0x00088714 File Offset: 0x00086914
		public Result DeleteData(PresenceModificationDeleteDataOptions options)
		{
			PresenceModificationDeleteDataOptionsInternal presenceModificationDeleteDataOptionsInternal = Helper.CopyProperties<PresenceModificationDeleteDataOptionsInternal>(options);
			Result result = PresenceModification.EOS_PresenceModification_DeleteData(base.InnerHandle, ref presenceModificationDeleteDataOptionsInternal);
			Helper.TryMarshalDispose<PresenceModificationDeleteDataOptionsInternal>(ref presenceModificationDeleteDataOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x0008874C File Offset: 0x0008694C
		public Result SetJoinInfo(PresenceModificationSetJoinInfoOptions options)
		{
			PresenceModificationSetJoinInfoOptionsInternal presenceModificationSetJoinInfoOptionsInternal = Helper.CopyProperties<PresenceModificationSetJoinInfoOptionsInternal>(options);
			Result result = PresenceModification.EOS_PresenceModification_SetJoinInfo(base.InnerHandle, ref presenceModificationSetJoinInfoOptionsInternal);
			Helper.TryMarshalDispose<PresenceModificationSetJoinInfoOptionsInternal>(ref presenceModificationSetJoinInfoOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x0600406B RID: 16491 RVA: 0x00088784 File Offset: 0x00086984
		public void Release()
		{
			PresenceModification.EOS_PresenceModification_Release(base.InnerHandle);
		}

		// Token: 0x0600406C RID: 16492
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_PresenceModification_Release(IntPtr presenceModificationHandle);

		// Token: 0x0600406D RID: 16493
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PresenceModification_SetJoinInfo(IntPtr handle, ref PresenceModificationSetJoinInfoOptionsInternal options);

		// Token: 0x0600406E RID: 16494
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PresenceModification_DeleteData(IntPtr handle, ref PresenceModificationDeleteDataOptionsInternal options);

		// Token: 0x0600406F RID: 16495
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PresenceModification_SetData(IntPtr handle, ref PresenceModificationSetDataOptionsInternal options);

		// Token: 0x06004070 RID: 16496
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PresenceModification_SetRawRichText(IntPtr handle, ref PresenceModificationSetRawRichTextOptionsInternal options);

		// Token: 0x06004071 RID: 16497
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_PresenceModification_SetStatus(IntPtr handle, ref PresenceModificationSetStatusOptionsInternal options);
	}
}
