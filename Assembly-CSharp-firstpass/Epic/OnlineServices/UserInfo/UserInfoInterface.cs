using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000558 RID: 1368
	public sealed class UserInfoInterface : Handle
	{
		// Token: 0x0600397D RID: 14717 RVA: 0x0008176B File Offset: 0x0007F96B
		public UserInfoInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x00081774 File Offset: 0x0007F974
		public void QueryUserInfo(QueryUserInfoOptions options, object clientData, OnQueryUserInfoCallback completionDelegate)
		{
			QueryUserInfoOptionsInternal queryUserInfoOptionsInternal = Helper.CopyProperties<QueryUserInfoOptionsInternal>(options);
			OnQueryUserInfoCallbackInternal onQueryUserInfoCallbackInternal = new OnQueryUserInfoCallbackInternal(UserInfoInterface.OnQueryUserInfo);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryUserInfoCallbackInternal, Array.Empty<Delegate>());
			UserInfoInterface.EOS_UserInfo_QueryUserInfo(base.InnerHandle, ref queryUserInfoOptionsInternal, zero, onQueryUserInfoCallbackInternal);
			Helper.TryMarshalDispose<QueryUserInfoOptionsInternal>(ref queryUserInfoOptionsInternal);
		}

		// Token: 0x0600397F RID: 14719 RVA: 0x000817C4 File Offset: 0x0007F9C4
		public void QueryUserInfoByDisplayName(QueryUserInfoByDisplayNameOptions options, object clientData, OnQueryUserInfoByDisplayNameCallback completionDelegate)
		{
			QueryUserInfoByDisplayNameOptionsInternal queryUserInfoByDisplayNameOptionsInternal = Helper.CopyProperties<QueryUserInfoByDisplayNameOptionsInternal>(options);
			OnQueryUserInfoByDisplayNameCallbackInternal onQueryUserInfoByDisplayNameCallbackInternal = new OnQueryUserInfoByDisplayNameCallbackInternal(UserInfoInterface.OnQueryUserInfoByDisplayName);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryUserInfoByDisplayNameCallbackInternal, Array.Empty<Delegate>());
			UserInfoInterface.EOS_UserInfo_QueryUserInfoByDisplayName(base.InnerHandle, ref queryUserInfoByDisplayNameOptionsInternal, zero, onQueryUserInfoByDisplayNameCallbackInternal);
			Helper.TryMarshalDispose<QueryUserInfoByDisplayNameOptionsInternal>(ref queryUserInfoByDisplayNameOptionsInternal);
		}

		// Token: 0x06003980 RID: 14720 RVA: 0x00081814 File Offset: 0x0007FA14
		public void QueryUserInfoByExternalAccount(QueryUserInfoByExternalAccountOptions options, object clientData, OnQueryUserInfoByExternalAccountCallback completionDelegate)
		{
			QueryUserInfoByExternalAccountOptionsInternal queryUserInfoByExternalAccountOptionsInternal = Helper.CopyProperties<QueryUserInfoByExternalAccountOptionsInternal>(options);
			OnQueryUserInfoByExternalAccountCallbackInternal onQueryUserInfoByExternalAccountCallbackInternal = new OnQueryUserInfoByExternalAccountCallbackInternal(UserInfoInterface.OnQueryUserInfoByExternalAccount);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryUserInfoByExternalAccountCallbackInternal, Array.Empty<Delegate>());
			UserInfoInterface.EOS_UserInfo_QueryUserInfoByExternalAccount(base.InnerHandle, ref queryUserInfoByExternalAccountOptionsInternal, zero, onQueryUserInfoByExternalAccountCallbackInternal);
			Helper.TryMarshalDispose<QueryUserInfoByExternalAccountOptionsInternal>(ref queryUserInfoByExternalAccountOptionsInternal);
		}

		// Token: 0x06003981 RID: 14721 RVA: 0x00081864 File Offset: 0x0007FA64
		public Result CopyUserInfo(CopyUserInfoOptions options, out UserInfoData outUserInfo)
		{
			CopyUserInfoOptionsInternal copyUserInfoOptionsInternal = Helper.CopyProperties<CopyUserInfoOptionsInternal>(options);
			outUserInfo = Helper.GetDefault<UserInfoData>();
			IntPtr zero = IntPtr.Zero;
			Result result = UserInfoInterface.EOS_UserInfo_CopyUserInfo(base.InnerHandle, ref copyUserInfoOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyUserInfoOptionsInternal>(ref copyUserInfoOptionsInternal);
			if (Helper.TryMarshalGet<UserInfoDataInternal, UserInfoData>(zero, out outUserInfo))
			{
				UserInfoInterface.EOS_UserInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003982 RID: 14722 RVA: 0x000818BC File Offset: 0x0007FABC
		public uint GetExternalUserInfoCount(GetExternalUserInfoCountOptions options)
		{
			GetExternalUserInfoCountOptionsInternal getExternalUserInfoCountOptionsInternal = Helper.CopyProperties<GetExternalUserInfoCountOptionsInternal>(options);
			uint num = UserInfoInterface.EOS_UserInfo_GetExternalUserInfoCount(base.InnerHandle, ref getExternalUserInfoCountOptionsInternal);
			Helper.TryMarshalDispose<GetExternalUserInfoCountOptionsInternal>(ref getExternalUserInfoCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06003983 RID: 14723 RVA: 0x000818F4 File Offset: 0x0007FAF4
		public Result CopyExternalUserInfoByIndex(CopyExternalUserInfoByIndexOptions options, out ExternalUserInfo outExternalUserInfo)
		{
			CopyExternalUserInfoByIndexOptionsInternal copyExternalUserInfoByIndexOptionsInternal = Helper.CopyProperties<CopyExternalUserInfoByIndexOptionsInternal>(options);
			outExternalUserInfo = Helper.GetDefault<ExternalUserInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = UserInfoInterface.EOS_UserInfo_CopyExternalUserInfoByIndex(base.InnerHandle, ref copyExternalUserInfoByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyExternalUserInfoByIndexOptionsInternal>(ref copyExternalUserInfoByIndexOptionsInternal);
			if (Helper.TryMarshalGet<ExternalUserInfoInternal, ExternalUserInfo>(zero, out outExternalUserInfo))
			{
				UserInfoInterface.EOS_UserInfo_ExternalUserInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003984 RID: 14724 RVA: 0x0008194C File Offset: 0x0007FB4C
		public Result CopyExternalUserInfoByAccountType(CopyExternalUserInfoByAccountTypeOptions options, out ExternalUserInfo outExternalUserInfo)
		{
			CopyExternalUserInfoByAccountTypeOptionsInternal copyExternalUserInfoByAccountTypeOptionsInternal = Helper.CopyProperties<CopyExternalUserInfoByAccountTypeOptionsInternal>(options);
			outExternalUserInfo = Helper.GetDefault<ExternalUserInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = UserInfoInterface.EOS_UserInfo_CopyExternalUserInfoByAccountType(base.InnerHandle, ref copyExternalUserInfoByAccountTypeOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyExternalUserInfoByAccountTypeOptionsInternal>(ref copyExternalUserInfoByAccountTypeOptionsInternal);
			if (Helper.TryMarshalGet<ExternalUserInfoInternal, ExternalUserInfo>(zero, out outExternalUserInfo))
			{
				UserInfoInterface.EOS_UserInfo_ExternalUserInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003985 RID: 14725 RVA: 0x000819A4 File Offset: 0x0007FBA4
		public Result CopyExternalUserInfoByAccountId(CopyExternalUserInfoByAccountIdOptions options, out ExternalUserInfo outExternalUserInfo)
		{
			CopyExternalUserInfoByAccountIdOptionsInternal copyExternalUserInfoByAccountIdOptionsInternal = Helper.CopyProperties<CopyExternalUserInfoByAccountIdOptionsInternal>(options);
			outExternalUserInfo = Helper.GetDefault<ExternalUserInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = UserInfoInterface.EOS_UserInfo_CopyExternalUserInfoByAccountId(base.InnerHandle, ref copyExternalUserInfoByAccountIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyExternalUserInfoByAccountIdOptionsInternal>(ref copyExternalUserInfoByAccountIdOptionsInternal);
			if (Helper.TryMarshalGet<ExternalUserInfoInternal, ExternalUserInfo>(zero, out outExternalUserInfo))
			{
				UserInfoInterface.EOS_UserInfo_ExternalUserInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06003986 RID: 14726 RVA: 0x000819FC File Offset: 0x0007FBFC
		[MonoPInvokeCallback]
		internal static void OnQueryUserInfoByExternalAccount(IntPtr address)
		{
			OnQueryUserInfoByExternalAccountCallback onQueryUserInfoByExternalAccountCallback = null;
			QueryUserInfoByExternalAccountCallbackInfo queryUserInfoByExternalAccountCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryUserInfoByExternalAccountCallback, QueryUserInfoByExternalAccountCallbackInfoInternal, QueryUserInfoByExternalAccountCallbackInfo>(address, out onQueryUserInfoByExternalAccountCallback, out queryUserInfoByExternalAccountCallbackInfo))
			{
				onQueryUserInfoByExternalAccountCallback(queryUserInfoByExternalAccountCallbackInfo);
			}
		}

		// Token: 0x06003987 RID: 14727 RVA: 0x00081A20 File Offset: 0x0007FC20
		[MonoPInvokeCallback]
		internal static void OnQueryUserInfoByDisplayName(IntPtr address)
		{
			OnQueryUserInfoByDisplayNameCallback onQueryUserInfoByDisplayNameCallback = null;
			QueryUserInfoByDisplayNameCallbackInfo queryUserInfoByDisplayNameCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryUserInfoByDisplayNameCallback, QueryUserInfoByDisplayNameCallbackInfoInternal, QueryUserInfoByDisplayNameCallbackInfo>(address, out onQueryUserInfoByDisplayNameCallback, out queryUserInfoByDisplayNameCallbackInfo))
			{
				onQueryUserInfoByDisplayNameCallback(queryUserInfoByDisplayNameCallbackInfo);
			}
		}

		// Token: 0x06003988 RID: 14728 RVA: 0x00081A44 File Offset: 0x0007FC44
		[MonoPInvokeCallback]
		internal static void OnQueryUserInfo(IntPtr address)
		{
			OnQueryUserInfoCallback onQueryUserInfoCallback = null;
			QueryUserInfoCallbackInfo queryUserInfoCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryUserInfoCallback, QueryUserInfoCallbackInfoInternal, QueryUserInfoCallbackInfo>(address, out onQueryUserInfoCallback, out queryUserInfoCallbackInfo))
			{
				onQueryUserInfoCallback(queryUserInfoCallbackInfo);
			}
		}

		// Token: 0x06003989 RID: 14729
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UserInfo_ExternalUserInfo_Release(IntPtr externalUserInfo);

		// Token: 0x0600398A RID: 14730
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UserInfo_Release(IntPtr userInfo);

		// Token: 0x0600398B RID: 14731
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_UserInfo_CopyExternalUserInfoByAccountId(IntPtr handle, ref CopyExternalUserInfoByAccountIdOptionsInternal options, ref IntPtr outExternalUserInfo);

		// Token: 0x0600398C RID: 14732
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_UserInfo_CopyExternalUserInfoByAccountType(IntPtr handle, ref CopyExternalUserInfoByAccountTypeOptionsInternal options, ref IntPtr outExternalUserInfo);

		// Token: 0x0600398D RID: 14733
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_UserInfo_CopyExternalUserInfoByIndex(IntPtr handle, ref CopyExternalUserInfoByIndexOptionsInternal options, ref IntPtr outExternalUserInfo);

		// Token: 0x0600398E RID: 14734
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_UserInfo_GetExternalUserInfoCount(IntPtr handle, ref GetExternalUserInfoCountOptionsInternal options);

		// Token: 0x0600398F RID: 14735
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_UserInfo_CopyUserInfo(IntPtr handle, ref CopyUserInfoOptionsInternal options, ref IntPtr outUserInfo);

		// Token: 0x06003990 RID: 14736
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UserInfo_QueryUserInfoByExternalAccount(IntPtr handle, ref QueryUserInfoByExternalAccountOptionsInternal options, IntPtr clientData, OnQueryUserInfoByExternalAccountCallbackInternal completionDelegate);

		// Token: 0x06003991 RID: 14737
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UserInfo_QueryUserInfoByDisplayName(IntPtr handle, ref QueryUserInfoByDisplayNameOptionsInternal options, IntPtr clientData, OnQueryUserInfoByDisplayNameCallbackInternal completionDelegate);

		// Token: 0x06003992 RID: 14738
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_UserInfo_QueryUserInfo(IntPtr handle, ref QueryUserInfoOptionsInternal options, IntPtr clientData, OnQueryUserInfoCallbackInternal completionDelegate);

		// Token: 0x04001577 RID: 5495
		public const int CopyexternaluserinfobyaccountidApiLatest = 1;

		// Token: 0x04001578 RID: 5496
		public const int CopyexternaluserinfobyaccounttypeApiLatest = 1;

		// Token: 0x04001579 RID: 5497
		public const int CopyexternaluserinfobyindexApiLatest = 1;

		// Token: 0x0400157A RID: 5498
		public const int GetexternaluserinfocountApiLatest = 1;

		// Token: 0x0400157B RID: 5499
		public const int ExternaluserinfoApiLatest = 1;

		// Token: 0x0400157C RID: 5500
		public const int CopyuserinfoApiLatest = 2;

		// Token: 0x0400157D RID: 5501
		public const int MaxDisplaynameUtf8Length = 64;

		// Token: 0x0400157E RID: 5502
		public const int MaxDisplaynameCharacters = 16;

		// Token: 0x0400157F RID: 5503
		public const int QueryuserinfobyexternalaccountApiLatest = 1;

		// Token: 0x04001580 RID: 5504
		public const int QueryuserinfobydisplaynameApiLatest = 1;

		// Token: 0x04001581 RID: 5505
		public const int QueryuserinfoApiLatest = 1;
	}
}
