using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200062F RID: 1583
	public sealed class SessionModification : Handle
	{
		// Token: 0x06003E52 RID: 15954 RVA: 0x00085F6A File Offset: 0x0008416A
		public SessionModification(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x00085F74 File Offset: 0x00084174
		public Result SetBucketId(SessionModificationSetBucketIdOptions options)
		{
			SessionModificationSetBucketIdOptionsInternal sessionModificationSetBucketIdOptionsInternal = Helper.CopyProperties<SessionModificationSetBucketIdOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_SetBucketId(base.InnerHandle, ref sessionModificationSetBucketIdOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationSetBucketIdOptionsInternal>(ref sessionModificationSetBucketIdOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x00085FAC File Offset: 0x000841AC
		public Result SetHostAddress(SessionModificationSetHostAddressOptions options)
		{
			SessionModificationSetHostAddressOptionsInternal sessionModificationSetHostAddressOptionsInternal = Helper.CopyProperties<SessionModificationSetHostAddressOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_SetHostAddress(base.InnerHandle, ref sessionModificationSetHostAddressOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationSetHostAddressOptionsInternal>(ref sessionModificationSetHostAddressOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x00085FE4 File Offset: 0x000841E4
		public Result SetPermissionLevel(SessionModificationSetPermissionLevelOptions options)
		{
			SessionModificationSetPermissionLevelOptionsInternal sessionModificationSetPermissionLevelOptionsInternal = Helper.CopyProperties<SessionModificationSetPermissionLevelOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_SetPermissionLevel(base.InnerHandle, ref sessionModificationSetPermissionLevelOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationSetPermissionLevelOptionsInternal>(ref sessionModificationSetPermissionLevelOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x0008601C File Offset: 0x0008421C
		public Result SetJoinInProgressAllowed(SessionModificationSetJoinInProgressAllowedOptions options)
		{
			SessionModificationSetJoinInProgressAllowedOptionsInternal sessionModificationSetJoinInProgressAllowedOptionsInternal = Helper.CopyProperties<SessionModificationSetJoinInProgressAllowedOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_SetJoinInProgressAllowed(base.InnerHandle, ref sessionModificationSetJoinInProgressAllowedOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationSetJoinInProgressAllowedOptionsInternal>(ref sessionModificationSetJoinInProgressAllowedOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x00086054 File Offset: 0x00084254
		public Result SetMaxPlayers(SessionModificationSetMaxPlayersOptions options)
		{
			SessionModificationSetMaxPlayersOptionsInternal sessionModificationSetMaxPlayersOptionsInternal = Helper.CopyProperties<SessionModificationSetMaxPlayersOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_SetMaxPlayers(base.InnerHandle, ref sessionModificationSetMaxPlayersOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationSetMaxPlayersOptionsInternal>(ref sessionModificationSetMaxPlayersOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x0008608C File Offset: 0x0008428C
		public Result SetInvitesAllowed(SessionModificationSetInvitesAllowedOptions options)
		{
			SessionModificationSetInvitesAllowedOptionsInternal sessionModificationSetInvitesAllowedOptionsInternal = Helper.CopyProperties<SessionModificationSetInvitesAllowedOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_SetInvitesAllowed(base.InnerHandle, ref sessionModificationSetInvitesAllowedOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationSetInvitesAllowedOptionsInternal>(ref sessionModificationSetInvitesAllowedOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000860C4 File Offset: 0x000842C4
		public Result AddAttribute(SessionModificationAddAttributeOptions options)
		{
			SessionModificationAddAttributeOptionsInternal sessionModificationAddAttributeOptionsInternal = Helper.CopyProperties<SessionModificationAddAttributeOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_AddAttribute(base.InnerHandle, ref sessionModificationAddAttributeOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationAddAttributeOptionsInternal>(ref sessionModificationAddAttributeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000860FC File Offset: 0x000842FC
		public Result RemoveAttribute(SessionModificationRemoveAttributeOptions options)
		{
			SessionModificationRemoveAttributeOptionsInternal sessionModificationRemoveAttributeOptionsInternal = Helper.CopyProperties<SessionModificationRemoveAttributeOptionsInternal>(options);
			Result result = SessionModification.EOS_SessionModification_RemoveAttribute(base.InnerHandle, ref sessionModificationRemoveAttributeOptionsInternal);
			Helper.TryMarshalDispose<SessionModificationRemoveAttributeOptionsInternal>(ref sessionModificationRemoveAttributeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x00086134 File Offset: 0x00084334
		public void Release()
		{
			SessionModification.EOS_SessionModification_Release(base.InnerHandle);
		}

		// Token: 0x06003E5C RID: 15964
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_SessionModification_Release(IntPtr sessionModificationHandle);

		// Token: 0x06003E5D RID: 15965
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_RemoveAttribute(IntPtr handle, ref SessionModificationRemoveAttributeOptionsInternal options);

		// Token: 0x06003E5E RID: 15966
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_AddAttribute(IntPtr handle, ref SessionModificationAddAttributeOptionsInternal options);

		// Token: 0x06003E5F RID: 15967
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_SetInvitesAllowed(IntPtr handle, ref SessionModificationSetInvitesAllowedOptionsInternal options);

		// Token: 0x06003E60 RID: 15968
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_SetMaxPlayers(IntPtr handle, ref SessionModificationSetMaxPlayersOptionsInternal options);

		// Token: 0x06003E61 RID: 15969
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_SetJoinInProgressAllowed(IntPtr handle, ref SessionModificationSetJoinInProgressAllowedOptionsInternal options);

		// Token: 0x06003E62 RID: 15970
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_SetPermissionLevel(IntPtr handle, ref SessionModificationSetPermissionLevelOptionsInternal options);

		// Token: 0x06003E63 RID: 15971
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_SetHostAddress(IntPtr handle, ref SessionModificationSetHostAddressOptionsInternal options);

		// Token: 0x06003E64 RID: 15972
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_SessionModification_SetBucketId(IntPtr handle, ref SessionModificationSetBucketIdOptionsInternal options);
	}
}
