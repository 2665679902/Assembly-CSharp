using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020008E2 RID: 2274
	public sealed class AuthInterface : Handle
	{
		// Token: 0x06004F92 RID: 20370 RVA: 0x0009787B File Offset: 0x00095A7B
		public AuthInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06004F93 RID: 20371 RVA: 0x00097884 File Offset: 0x00095A84
		public void Login(LoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			LoginOptionsInternal loginOptionsInternal = Helper.CopyProperties<LoginOptionsInternal>(options);
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(AuthInterface.OnLogin);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			AuthInterface.EOS_Auth_Login(base.InnerHandle, ref loginOptionsInternal, zero, onLoginCallbackInternal);
			Helper.TryMarshalDispose<LoginOptionsInternal>(ref loginOptionsInternal);
		}

		// Token: 0x06004F94 RID: 20372 RVA: 0x000978D4 File Offset: 0x00095AD4
		public void Logout(LogoutOptions options, object clientData, OnLogoutCallback completionDelegate)
		{
			LogoutOptionsInternal logoutOptionsInternal = Helper.CopyProperties<LogoutOptionsInternal>(options);
			OnLogoutCallbackInternal onLogoutCallbackInternal = new OnLogoutCallbackInternal(AuthInterface.OnLogout);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onLogoutCallbackInternal, Array.Empty<Delegate>());
			AuthInterface.EOS_Auth_Logout(base.InnerHandle, ref logoutOptionsInternal, zero, onLogoutCallbackInternal);
			Helper.TryMarshalDispose<LogoutOptionsInternal>(ref logoutOptionsInternal);
		}

		// Token: 0x06004F95 RID: 20373 RVA: 0x00097924 File Offset: 0x00095B24
		public void LinkAccount(LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
		{
			LinkAccountOptionsInternal linkAccountOptionsInternal = Helper.CopyProperties<LinkAccountOptionsInternal>(options);
			OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = new OnLinkAccountCallbackInternal(AuthInterface.OnLinkAccount);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onLinkAccountCallbackInternal, Array.Empty<Delegate>());
			AuthInterface.EOS_Auth_LinkAccount(base.InnerHandle, ref linkAccountOptionsInternal, zero, onLinkAccountCallbackInternal);
			Helper.TryMarshalDispose<LinkAccountOptionsInternal>(ref linkAccountOptionsInternal);
		}

		// Token: 0x06004F96 RID: 20374 RVA: 0x00097974 File Offset: 0x00095B74
		public void DeletePersistentAuth(DeletePersistentAuthOptions options, object clientData, OnDeletePersistentAuthCallback completionDelegate)
		{
			DeletePersistentAuthOptionsInternal deletePersistentAuthOptionsInternal = Helper.CopyProperties<DeletePersistentAuthOptionsInternal>(options);
			OnDeletePersistentAuthCallbackInternal onDeletePersistentAuthCallbackInternal = new OnDeletePersistentAuthCallbackInternal(AuthInterface.OnDeletePersistentAuth);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onDeletePersistentAuthCallbackInternal, Array.Empty<Delegate>());
			AuthInterface.EOS_Auth_DeletePersistentAuth(base.InnerHandle, ref deletePersistentAuthOptionsInternal, zero, onDeletePersistentAuthCallbackInternal);
			Helper.TryMarshalDispose<DeletePersistentAuthOptionsInternal>(ref deletePersistentAuthOptionsInternal);
		}

		// Token: 0x06004F97 RID: 20375 RVA: 0x000979C4 File Offset: 0x00095BC4
		public void VerifyUserAuth(VerifyUserAuthOptions options, object clientData, OnVerifyUserAuthCallback completionDelegate)
		{
			VerifyUserAuthOptionsInternal verifyUserAuthOptionsInternal = Helper.CopyProperties<VerifyUserAuthOptionsInternal>(options);
			OnVerifyUserAuthCallbackInternal onVerifyUserAuthCallbackInternal = new OnVerifyUserAuthCallbackInternal(AuthInterface.OnVerifyUserAuth);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onVerifyUserAuthCallbackInternal, Array.Empty<Delegate>());
			AuthInterface.EOS_Auth_VerifyUserAuth(base.InnerHandle, ref verifyUserAuthOptionsInternal, zero, onVerifyUserAuthCallbackInternal);
			Helper.TryMarshalDispose<VerifyUserAuthOptionsInternal>(ref verifyUserAuthOptionsInternal);
		}

		// Token: 0x06004F98 RID: 20376 RVA: 0x00097A14 File Offset: 0x00095C14
		public int GetLoggedInAccountsCount()
		{
			int num = AuthInterface.EOS_Auth_GetLoggedInAccountsCount(base.InnerHandle);
			int @default = Helper.GetDefault<int>();
			Helper.TryMarshalGet<int>(num, out @default);
			return @default;
		}

		// Token: 0x06004F99 RID: 20377 RVA: 0x00097A3C File Offset: 0x00095C3C
		public EpicAccountId GetLoggedInAccountByIndex(int index)
		{
			IntPtr intPtr = AuthInterface.EOS_Auth_GetLoggedInAccountByIndex(base.InnerHandle, index);
			EpicAccountId @default = Helper.GetDefault<EpicAccountId>();
			Helper.TryMarshalGet<EpicAccountId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x06004F9A RID: 20378 RVA: 0x00097A64 File Offset: 0x00095C64
		public LoginStatus GetLoginStatus(EpicAccountId localUserId)
		{
			LoginStatus loginStatus = AuthInterface.EOS_Auth_GetLoginStatus(base.InnerHandle, localUserId.InnerHandle);
			LoginStatus @default = Helper.GetDefault<LoginStatus>();
			Helper.TryMarshalGet<LoginStatus>(loginStatus, out @default);
			return @default;
		}

		// Token: 0x06004F9B RID: 20379 RVA: 0x00097A94 File Offset: 0x00095C94
		public Result CopyUserAuthToken(CopyUserAuthTokenOptions options, EpicAccountId localUserId, out Token outUserAuthToken)
		{
			CopyUserAuthTokenOptionsInternal copyUserAuthTokenOptionsInternal = Helper.CopyProperties<CopyUserAuthTokenOptionsInternal>(options);
			outUserAuthToken = Helper.GetDefault<Token>();
			IntPtr zero = IntPtr.Zero;
			Result result = AuthInterface.EOS_Auth_CopyUserAuthToken(base.InnerHandle, ref copyUserAuthTokenOptionsInternal, localUserId.InnerHandle, ref zero);
			Helper.TryMarshalDispose<CopyUserAuthTokenOptionsInternal>(ref copyUserAuthTokenOptionsInternal);
			if (Helper.TryMarshalGet<TokenInternal, Token>(zero, out outUserAuthToken))
			{
				AuthInterface.EOS_Auth_Token_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004F9C RID: 20380 RVA: 0x00097AF0 File Offset: 0x00095CF0
		public ulong AddNotifyLoginStatusChanged(AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
		{
			AddNotifyLoginStatusChangedOptionsInternal addNotifyLoginStatusChangedOptionsInternal = Helper.CopyProperties<AddNotifyLoginStatusChangedOptionsInternal>(options);
			OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = new OnLoginStatusChangedCallbackInternal(AuthInterface.OnLoginStatusChanged);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notification, onLoginStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = AuthInterface.EOS_Auth_AddNotifyLoginStatusChanged(base.InnerHandle, ref addNotifyLoginStatusChangedOptionsInternal, zero, onLoginStatusChangedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyLoginStatusChangedOptionsInternal>(ref addNotifyLoginStatusChangedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06004F9D RID: 20381 RVA: 0x00097B58 File Offset: 0x00095D58
		public void RemoveNotifyLoginStatusChanged(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			AuthInterface.EOS_Auth_RemoveNotifyLoginStatusChanged(base.InnerHandle, inId);
		}

		// Token: 0x06004F9E RID: 20382 RVA: 0x00097B70 File Offset: 0x00095D70
		[MonoPInvokeCallback]
		internal static void OnLoginStatusChanged(IntPtr address)
		{
			OnLoginStatusChangedCallback onLoginStatusChangedCallback = null;
			LoginStatusChangedCallbackInfo loginStatusChangedCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLoginStatusChangedCallback, LoginStatusChangedCallbackInfoInternal, LoginStatusChangedCallbackInfo>(address, out onLoginStatusChangedCallback, out loginStatusChangedCallbackInfo))
			{
				onLoginStatusChangedCallback(loginStatusChangedCallbackInfo);
			}
		}

		// Token: 0x06004F9F RID: 20383 RVA: 0x00097B94 File Offset: 0x00095D94
		[MonoPInvokeCallback]
		internal static void OnVerifyUserAuth(IntPtr address)
		{
			OnVerifyUserAuthCallback onVerifyUserAuthCallback = null;
			VerifyUserAuthCallbackInfo verifyUserAuthCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnVerifyUserAuthCallback, VerifyUserAuthCallbackInfoInternal, VerifyUserAuthCallbackInfo>(address, out onVerifyUserAuthCallback, out verifyUserAuthCallbackInfo))
			{
				onVerifyUserAuthCallback(verifyUserAuthCallbackInfo);
			}
		}

		// Token: 0x06004FA0 RID: 20384 RVA: 0x00097BB8 File Offset: 0x00095DB8
		[MonoPInvokeCallback]
		internal static void OnDeletePersistentAuth(IntPtr address)
		{
			OnDeletePersistentAuthCallback onDeletePersistentAuthCallback = null;
			DeletePersistentAuthCallbackInfo deletePersistentAuthCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDeletePersistentAuthCallback, DeletePersistentAuthCallbackInfoInternal, DeletePersistentAuthCallbackInfo>(address, out onDeletePersistentAuthCallback, out deletePersistentAuthCallbackInfo))
			{
				onDeletePersistentAuthCallback(deletePersistentAuthCallbackInfo);
			}
		}

		// Token: 0x06004FA1 RID: 20385 RVA: 0x00097BDC File Offset: 0x00095DDC
		[MonoPInvokeCallback]
		internal static void OnLinkAccount(IntPtr address)
		{
			OnLinkAccountCallback onLinkAccountCallback = null;
			LinkAccountCallbackInfo linkAccountCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLinkAccountCallback, LinkAccountCallbackInfoInternal, LinkAccountCallbackInfo>(address, out onLinkAccountCallback, out linkAccountCallbackInfo))
			{
				onLinkAccountCallback(linkAccountCallbackInfo);
			}
		}

		// Token: 0x06004FA2 RID: 20386 RVA: 0x00097C00 File Offset: 0x00095E00
		[MonoPInvokeCallback]
		internal static void OnLogout(IntPtr address)
		{
			OnLogoutCallback onLogoutCallback = null;
			LogoutCallbackInfo logoutCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLogoutCallback, LogoutCallbackInfoInternal, LogoutCallbackInfo>(address, out onLogoutCallback, out logoutCallbackInfo))
			{
				onLogoutCallback(logoutCallbackInfo);
			}
		}

		// Token: 0x06004FA3 RID: 20387 RVA: 0x00097C24 File Offset: 0x00095E24
		[MonoPInvokeCallback]
		internal static void OnLogin(IntPtr address)
		{
			OnLoginCallback onLoginCallback = null;
			LoginCallbackInfo loginCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnLoginCallback, LoginCallbackInfoInternal, LoginCallbackInfo>(address, out onLoginCallback, out loginCallbackInfo))
			{
				onLoginCallback(loginCallbackInfo);
			}
		}

		// Token: 0x06004FA4 RID: 20388
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Auth_Token_Release(IntPtr authToken);

		// Token: 0x06004FA5 RID: 20389
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Auth_RemoveNotifyLoginStatusChanged(IntPtr handle, ulong inId);

		// Token: 0x06004FA6 RID: 20390
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Auth_AddNotifyLoginStatusChanged(IntPtr handle, ref AddNotifyLoginStatusChangedOptionsInternal options, IntPtr clientData, OnLoginStatusChangedCallbackInternal notification);

		// Token: 0x06004FA7 RID: 20391
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Auth_CopyUserAuthToken(IntPtr handle, ref CopyUserAuthTokenOptionsInternal options, IntPtr localUserId, ref IntPtr outUserAuthToken);

		// Token: 0x06004FA8 RID: 20392
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern LoginStatus EOS_Auth_GetLoginStatus(IntPtr handle, IntPtr localUserId);

		// Token: 0x06004FA9 RID: 20393
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Auth_GetLoggedInAccountByIndex(IntPtr handle, int index);

		// Token: 0x06004FAA RID: 20394
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_Auth_GetLoggedInAccountsCount(IntPtr handle);

		// Token: 0x06004FAB RID: 20395
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Auth_VerifyUserAuth(IntPtr handle, ref VerifyUserAuthOptionsInternal options, IntPtr clientData, OnVerifyUserAuthCallbackInternal completionDelegate);

		// Token: 0x06004FAC RID: 20396
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Auth_DeletePersistentAuth(IntPtr handle, ref DeletePersistentAuthOptionsInternal options, IntPtr clientData, OnDeletePersistentAuthCallbackInternal completionDelegate);

		// Token: 0x06004FAD RID: 20397
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Auth_LinkAccount(IntPtr handle, ref LinkAccountOptionsInternal options, IntPtr clientData, OnLinkAccountCallbackInternal completionDelegate);

		// Token: 0x06004FAE RID: 20398
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Auth_Logout(IntPtr handle, ref LogoutOptionsInternal options, IntPtr clientData, OnLogoutCallbackInternal completionDelegate);

		// Token: 0x06004FAF RID: 20399
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Auth_Login(IntPtr handle, ref LoginOptionsInternal options, IntPtr clientData, OnLoginCallbackInternal completionDelegate);

		// Token: 0x04001EDA RID: 7898
		public const int DeletepersistentauthApiLatest = 2;

		// Token: 0x04001EDB RID: 7899
		public const int AddnotifyloginstatuschangedApiLatest = 1;

		// Token: 0x04001EDC RID: 7900
		public const int CopyuserauthtokenApiLatest = 1;

		// Token: 0x04001EDD RID: 7901
		public const int VerifyuserauthApiLatest = 1;

		// Token: 0x04001EDE RID: 7902
		public const int LinkaccountApiLatest = 1;

		// Token: 0x04001EDF RID: 7903
		public const int LogoutApiLatest = 1;

		// Token: 0x04001EE0 RID: 7904
		public const int LoginApiLatest = 2;

		// Token: 0x04001EE1 RID: 7905
		public const int AccountfeaturerestrictedinfoApiLatest = 1;

		// Token: 0x04001EE2 RID: 7906
		public const int PingrantinfoApiLatest = 2;

		// Token: 0x04001EE3 RID: 7907
		public const int CredentialsApiLatest = 3;

		// Token: 0x04001EE4 RID: 7908
		public const int TokenApiLatest = 2;
	}
}
