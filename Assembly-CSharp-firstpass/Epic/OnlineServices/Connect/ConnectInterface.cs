using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200088C RID: 2188
	public sealed class ConnectInterface : Handle
	{
		// Token: 0x06004D8C RID: 19852 RVA: 0x000959B6 File Offset: 0x00093BB6
		public ConnectInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06004D8D RID: 19853 RVA: 0x000959C0 File Offset: 0x00093BC0
		public void Login(LoginOptions options, object clientData, OnLoginCallback completionDelegate)
		{
			LoginOptionsInternal loginOptionsInternal = Helper.CopyProperties<LoginOptionsInternal>(options);
			OnLoginCallbackInternal onLoginCallbackInternal = new OnLoginCallbackInternal(ConnectInterface.OnLogin);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onLoginCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_Login(base.InnerHandle, ref loginOptionsInternal, zero, onLoginCallbackInternal);
			Helper.TryMarshalDispose<LoginOptionsInternal>(ref loginOptionsInternal);
		}

		// Token: 0x06004D8E RID: 19854 RVA: 0x00095A10 File Offset: 0x00093C10
		public void CreateUser(CreateUserOptions options, object clientData, OnCreateUserCallback completionDelegate)
		{
			CreateUserOptionsInternal createUserOptionsInternal = Helper.CopyProperties<CreateUserOptionsInternal>(options);
			OnCreateUserCallbackInternal onCreateUserCallbackInternal = new OnCreateUserCallbackInternal(ConnectInterface.OnCreateUser);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onCreateUserCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_CreateUser(base.InnerHandle, ref createUserOptionsInternal, zero, onCreateUserCallbackInternal);
			Helper.TryMarshalDispose<CreateUserOptionsInternal>(ref createUserOptionsInternal);
		}

		// Token: 0x06004D8F RID: 19855 RVA: 0x00095A60 File Offset: 0x00093C60
		public void LinkAccount(LinkAccountOptions options, object clientData, OnLinkAccountCallback completionDelegate)
		{
			LinkAccountOptionsInternal linkAccountOptionsInternal = Helper.CopyProperties<LinkAccountOptionsInternal>(options);
			OnLinkAccountCallbackInternal onLinkAccountCallbackInternal = new OnLinkAccountCallbackInternal(ConnectInterface.OnLinkAccount);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onLinkAccountCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_LinkAccount(base.InnerHandle, ref linkAccountOptionsInternal, zero, onLinkAccountCallbackInternal);
			Helper.TryMarshalDispose<LinkAccountOptionsInternal>(ref linkAccountOptionsInternal);
		}

		// Token: 0x06004D90 RID: 19856 RVA: 0x00095AB0 File Offset: 0x00093CB0
		public void UnlinkAccount(UnlinkAccountOptions options, object clientData, OnUnlinkAccountCallback completionDelegate)
		{
			UnlinkAccountOptionsInternal unlinkAccountOptionsInternal = Helper.CopyProperties<UnlinkAccountOptionsInternal>(options);
			OnUnlinkAccountCallbackInternal onUnlinkAccountCallbackInternal = new OnUnlinkAccountCallbackInternal(ConnectInterface.OnUnlinkAccount);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onUnlinkAccountCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_UnlinkAccount(base.InnerHandle, ref unlinkAccountOptionsInternal, zero, onUnlinkAccountCallbackInternal);
			Helper.TryMarshalDispose<UnlinkAccountOptionsInternal>(ref unlinkAccountOptionsInternal);
		}

		// Token: 0x06004D91 RID: 19857 RVA: 0x00095B00 File Offset: 0x00093D00
		public void CreateDeviceId(CreateDeviceIdOptions options, object clientData, OnCreateDeviceIdCallback completionDelegate)
		{
			CreateDeviceIdOptionsInternal createDeviceIdOptionsInternal = Helper.CopyProperties<CreateDeviceIdOptionsInternal>(options);
			OnCreateDeviceIdCallbackInternal onCreateDeviceIdCallbackInternal = new OnCreateDeviceIdCallbackInternal(ConnectInterface.OnCreateDeviceId);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onCreateDeviceIdCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_CreateDeviceId(base.InnerHandle, ref createDeviceIdOptionsInternal, zero, onCreateDeviceIdCallbackInternal);
			Helper.TryMarshalDispose<CreateDeviceIdOptionsInternal>(ref createDeviceIdOptionsInternal);
		}

		// Token: 0x06004D92 RID: 19858 RVA: 0x00095B50 File Offset: 0x00093D50
		public void DeleteDeviceId(DeleteDeviceIdOptions options, object clientData, OnDeleteDeviceIdCallback completionDelegate)
		{
			DeleteDeviceIdOptionsInternal deleteDeviceIdOptionsInternal = Helper.CopyProperties<DeleteDeviceIdOptionsInternal>(options);
			OnDeleteDeviceIdCallbackInternal onDeleteDeviceIdCallbackInternal = new OnDeleteDeviceIdCallbackInternal(ConnectInterface.OnDeleteDeviceId);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onDeleteDeviceIdCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_DeleteDeviceId(base.InnerHandle, ref deleteDeviceIdOptionsInternal, zero, onDeleteDeviceIdCallbackInternal);
			Helper.TryMarshalDispose<DeleteDeviceIdOptionsInternal>(ref deleteDeviceIdOptionsInternal);
		}

		// Token: 0x06004D93 RID: 19859 RVA: 0x00095BA0 File Offset: 0x00093DA0
		public void TransferDeviceIdAccount(TransferDeviceIdAccountOptions options, object clientData, OnTransferDeviceIdAccountCallback completionDelegate)
		{
			TransferDeviceIdAccountOptionsInternal transferDeviceIdAccountOptionsInternal = Helper.CopyProperties<TransferDeviceIdAccountOptionsInternal>(options);
			OnTransferDeviceIdAccountCallbackInternal onTransferDeviceIdAccountCallbackInternal = new OnTransferDeviceIdAccountCallbackInternal(ConnectInterface.OnTransferDeviceIdAccount);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onTransferDeviceIdAccountCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_TransferDeviceIdAccount(base.InnerHandle, ref transferDeviceIdAccountOptionsInternal, zero, onTransferDeviceIdAccountCallbackInternal);
			Helper.TryMarshalDispose<TransferDeviceIdAccountOptionsInternal>(ref transferDeviceIdAccountOptionsInternal);
		}

		// Token: 0x06004D94 RID: 19860 RVA: 0x00095BF0 File Offset: 0x00093DF0
		public void QueryExternalAccountMappings(QueryExternalAccountMappingsOptions options, object clientData, OnQueryExternalAccountMappingsCallback completionDelegate)
		{
			QueryExternalAccountMappingsOptionsInternal queryExternalAccountMappingsOptionsInternal = Helper.CopyProperties<QueryExternalAccountMappingsOptionsInternal>(options);
			OnQueryExternalAccountMappingsCallbackInternal onQueryExternalAccountMappingsCallbackInternal = new OnQueryExternalAccountMappingsCallbackInternal(ConnectInterface.OnQueryExternalAccountMappings);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryExternalAccountMappingsCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_QueryExternalAccountMappings(base.InnerHandle, ref queryExternalAccountMappingsOptionsInternal, zero, onQueryExternalAccountMappingsCallbackInternal);
			Helper.TryMarshalDispose<QueryExternalAccountMappingsOptionsInternal>(ref queryExternalAccountMappingsOptionsInternal);
		}

		// Token: 0x06004D95 RID: 19861 RVA: 0x00095C40 File Offset: 0x00093E40
		public void QueryProductUserIdMappings(QueryProductUserIdMappingsOptions options, object clientData, OnQueryProductUserIdMappingsCallback completionDelegate)
		{
			QueryProductUserIdMappingsOptionsInternal queryProductUserIdMappingsOptionsInternal = Helper.CopyProperties<QueryProductUserIdMappingsOptionsInternal>(options);
			OnQueryProductUserIdMappingsCallbackInternal onQueryProductUserIdMappingsCallbackInternal = new OnQueryProductUserIdMappingsCallbackInternal(ConnectInterface.OnQueryProductUserIdMappings);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryProductUserIdMappingsCallbackInternal, Array.Empty<Delegate>());
			ConnectInterface.EOS_Connect_QueryProductUserIdMappings(base.InnerHandle, ref queryProductUserIdMappingsOptionsInternal, zero, onQueryProductUserIdMappingsCallbackInternal);
			Helper.TryMarshalDispose<QueryProductUserIdMappingsOptionsInternal>(ref queryProductUserIdMappingsOptionsInternal);
		}

		// Token: 0x06004D96 RID: 19862 RVA: 0x00095C90 File Offset: 0x00093E90
		public ProductUserId GetExternalAccountMapping(GetExternalAccountMappingsOptions options)
		{
			GetExternalAccountMappingsOptionsInternal getExternalAccountMappingsOptionsInternal = Helper.CopyProperties<GetExternalAccountMappingsOptionsInternal>(options);
			IntPtr intPtr = ConnectInterface.EOS_Connect_GetExternalAccountMapping(base.InnerHandle, ref getExternalAccountMappingsOptionsInternal);
			Helper.TryMarshalDispose<GetExternalAccountMappingsOptionsInternal>(ref getExternalAccountMappingsOptionsInternal);
			ProductUserId @default = Helper.GetDefault<ProductUserId>();
			Helper.TryMarshalGet<ProductUserId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x06004D97 RID: 19863 RVA: 0x00095CC8 File Offset: 0x00093EC8
		public Result GetProductUserIdMapping(GetProductUserIdMappingOptions options, StringBuilder outBuffer, ref int inOutBufferLength)
		{
			GetProductUserIdMappingOptionsInternal getProductUserIdMappingOptionsInternal = Helper.CopyProperties<GetProductUserIdMappingOptionsInternal>(options);
			Result result = ConnectInterface.EOS_Connect_GetProductUserIdMapping(base.InnerHandle, ref getProductUserIdMappingOptionsInternal, outBuffer, ref inOutBufferLength);
			Helper.TryMarshalDispose<GetProductUserIdMappingOptionsInternal>(ref getProductUserIdMappingOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004D98 RID: 19864 RVA: 0x00095D04 File Offset: 0x00093F04
		public uint GetProductUserExternalAccountCount(GetProductUserExternalAccountCountOptions options)
		{
			GetProductUserExternalAccountCountOptionsInternal getProductUserExternalAccountCountOptionsInternal = Helper.CopyProperties<GetProductUserExternalAccountCountOptionsInternal>(options);
			uint num = ConnectInterface.EOS_Connect_GetProductUserExternalAccountCount(base.InnerHandle, ref getProductUserExternalAccountCountOptionsInternal);
			Helper.TryMarshalDispose<GetProductUserExternalAccountCountOptionsInternal>(ref getProductUserExternalAccountCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004D99 RID: 19865 RVA: 0x00095D3C File Offset: 0x00093F3C
		public Result CopyProductUserExternalAccountByIndex(CopyProductUserExternalAccountByIndexOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			CopyProductUserExternalAccountByIndexOptionsInternal copyProductUserExternalAccountByIndexOptionsInternal = Helper.CopyProperties<CopyProductUserExternalAccountByIndexOptionsInternal>(options);
			outExternalAccountInfo = Helper.GetDefault<ExternalAccountInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = ConnectInterface.EOS_Connect_CopyProductUserExternalAccountByIndex(base.InnerHandle, ref copyProductUserExternalAccountByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyProductUserExternalAccountByIndexOptionsInternal>(ref copyProductUserExternalAccountByIndexOptionsInternal);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo))
			{
				ConnectInterface.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004D9A RID: 19866 RVA: 0x00095D94 File Offset: 0x00093F94
		public Result CopyProductUserExternalAccountByAccountType(CopyProductUserExternalAccountByAccountTypeOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			CopyProductUserExternalAccountByAccountTypeOptionsInternal copyProductUserExternalAccountByAccountTypeOptionsInternal = Helper.CopyProperties<CopyProductUserExternalAccountByAccountTypeOptionsInternal>(options);
			outExternalAccountInfo = Helper.GetDefault<ExternalAccountInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = ConnectInterface.EOS_Connect_CopyProductUserExternalAccountByAccountType(base.InnerHandle, ref copyProductUserExternalAccountByAccountTypeOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyProductUserExternalAccountByAccountTypeOptionsInternal>(ref copyProductUserExternalAccountByAccountTypeOptionsInternal);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo))
			{
				ConnectInterface.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004D9B RID: 19867 RVA: 0x00095DEC File Offset: 0x00093FEC
		public Result CopyProductUserExternalAccountByAccountId(CopyProductUserExternalAccountByAccountIdOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			CopyProductUserExternalAccountByAccountIdOptionsInternal copyProductUserExternalAccountByAccountIdOptionsInternal = Helper.CopyProperties<CopyProductUserExternalAccountByAccountIdOptionsInternal>(options);
			outExternalAccountInfo = Helper.GetDefault<ExternalAccountInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = ConnectInterface.EOS_Connect_CopyProductUserExternalAccountByAccountId(base.InnerHandle, ref copyProductUserExternalAccountByAccountIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyProductUserExternalAccountByAccountIdOptionsInternal>(ref copyProductUserExternalAccountByAccountIdOptionsInternal);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo))
			{
				ConnectInterface.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004D9C RID: 19868 RVA: 0x00095E44 File Offset: 0x00094044
		public Result CopyProductUserInfo(CopyProductUserInfoOptions options, out ExternalAccountInfo outExternalAccountInfo)
		{
			CopyProductUserInfoOptionsInternal copyProductUserInfoOptionsInternal = Helper.CopyProperties<CopyProductUserInfoOptionsInternal>(options);
			outExternalAccountInfo = Helper.GetDefault<ExternalAccountInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = ConnectInterface.EOS_Connect_CopyProductUserInfo(base.InnerHandle, ref copyProductUserInfoOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyProductUserInfoOptionsInternal>(ref copyProductUserInfoOptionsInternal);
			if (Helper.TryMarshalGet<ExternalAccountInfoInternal, ExternalAccountInfo>(zero, out outExternalAccountInfo))
			{
				ConnectInterface.EOS_Connect_ExternalAccountInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004D9D RID: 19869 RVA: 0x00095E9C File Offset: 0x0009409C
		public int GetLoggedInUsersCount()
		{
			int num = ConnectInterface.EOS_Connect_GetLoggedInUsersCount(base.InnerHandle);
			int @default = Helper.GetDefault<int>();
			Helper.TryMarshalGet<int>(num, out @default);
			return @default;
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x00095EC4 File Offset: 0x000940C4
		public ProductUserId GetLoggedInUserByIndex(int index)
		{
			IntPtr intPtr = ConnectInterface.EOS_Connect_GetLoggedInUserByIndex(base.InnerHandle, index);
			ProductUserId @default = Helper.GetDefault<ProductUserId>();
			Helper.TryMarshalGet<ProductUserId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00095EEC File Offset: 0x000940EC
		public LoginStatus GetLoginStatus(ProductUserId localUserId)
		{
			LoginStatus loginStatus = ConnectInterface.EOS_Connect_GetLoginStatus(base.InnerHandle, localUserId.InnerHandle);
			LoginStatus @default = Helper.GetDefault<LoginStatus>();
			Helper.TryMarshalGet<LoginStatus>(loginStatus, out @default);
			return @default;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00095F1C File Offset: 0x0009411C
		public ulong AddNotifyAuthExpiration(AddNotifyAuthExpirationOptions options, object clientData, OnAuthExpirationCallback notification)
		{
			AddNotifyAuthExpirationOptionsInternal addNotifyAuthExpirationOptionsInternal = Helper.CopyProperties<AddNotifyAuthExpirationOptionsInternal>(options);
			OnAuthExpirationCallbackInternal onAuthExpirationCallbackInternal = new OnAuthExpirationCallbackInternal(ConnectInterface.OnAuthExpiration);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notification, onAuthExpirationCallbackInternal, Array.Empty<Delegate>());
			ulong num = ConnectInterface.EOS_Connect_AddNotifyAuthExpiration(base.InnerHandle, ref addNotifyAuthExpirationOptionsInternal, zero, onAuthExpirationCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyAuthExpirationOptionsInternal>(ref addNotifyAuthExpirationOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06004DA1 RID: 19873 RVA: 0x00095F84 File Offset: 0x00094184
		public void RemoveNotifyAuthExpiration(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			ConnectInterface.EOS_Connect_RemoveNotifyAuthExpiration(base.InnerHandle, inId);
		}

		// Token: 0x06004DA2 RID: 19874 RVA: 0x00095F9C File Offset: 0x0009419C
		public ulong AddNotifyLoginStatusChanged(AddNotifyLoginStatusChangedOptions options, object clientData, OnLoginStatusChangedCallback notification)
		{
			AddNotifyLoginStatusChangedOptionsInternal addNotifyLoginStatusChangedOptionsInternal = Helper.CopyProperties<AddNotifyLoginStatusChangedOptionsInternal>(options);
			OnLoginStatusChangedCallbackInternal onLoginStatusChangedCallbackInternal = new OnLoginStatusChangedCallbackInternal(ConnectInterface.OnLoginStatusChanged);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, notification, onLoginStatusChangedCallbackInternal, Array.Empty<Delegate>());
			ulong num = ConnectInterface.EOS_Connect_AddNotifyLoginStatusChanged(base.InnerHandle, ref addNotifyLoginStatusChangedOptionsInternal, zero, onLoginStatusChangedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyLoginStatusChangedOptionsInternal>(ref addNotifyLoginStatusChangedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x06004DA3 RID: 19875 RVA: 0x00096004 File Offset: 0x00094204
		public void RemoveNotifyLoginStatusChanged(ulong inId)
		{
			Helper.TryRemoveCallbackByNotificationId(inId);
			ConnectInterface.EOS_Connect_RemoveNotifyLoginStatusChanged(base.InnerHandle, inId);
		}

		// Token: 0x06004DA4 RID: 19876 RVA: 0x0009601C File Offset: 0x0009421C
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

		// Token: 0x06004DA5 RID: 19877 RVA: 0x00096040 File Offset: 0x00094240
		[MonoPInvokeCallback]
		internal static void OnAuthExpiration(IntPtr address)
		{
			OnAuthExpirationCallback onAuthExpirationCallback = null;
			AuthExpirationCallbackInfo authExpirationCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnAuthExpirationCallback, AuthExpirationCallbackInfoInternal, AuthExpirationCallbackInfo>(address, out onAuthExpirationCallback, out authExpirationCallbackInfo))
			{
				onAuthExpirationCallback(authExpirationCallbackInfo);
			}
		}

		// Token: 0x06004DA6 RID: 19878 RVA: 0x00096064 File Offset: 0x00094264
		[MonoPInvokeCallback]
		internal static void OnQueryProductUserIdMappings(IntPtr address)
		{
			OnQueryProductUserIdMappingsCallback onQueryProductUserIdMappingsCallback = null;
			QueryProductUserIdMappingsCallbackInfo queryProductUserIdMappingsCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryProductUserIdMappingsCallback, QueryProductUserIdMappingsCallbackInfoInternal, QueryProductUserIdMappingsCallbackInfo>(address, out onQueryProductUserIdMappingsCallback, out queryProductUserIdMappingsCallbackInfo))
			{
				onQueryProductUserIdMappingsCallback(queryProductUserIdMappingsCallbackInfo);
			}
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x00096088 File Offset: 0x00094288
		[MonoPInvokeCallback]
		internal static void OnQueryExternalAccountMappings(IntPtr address)
		{
			OnQueryExternalAccountMappingsCallback onQueryExternalAccountMappingsCallback = null;
			QueryExternalAccountMappingsCallbackInfo queryExternalAccountMappingsCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryExternalAccountMappingsCallback, QueryExternalAccountMappingsCallbackInfoInternal, QueryExternalAccountMappingsCallbackInfo>(address, out onQueryExternalAccountMappingsCallback, out queryExternalAccountMappingsCallbackInfo))
			{
				onQueryExternalAccountMappingsCallback(queryExternalAccountMappingsCallbackInfo);
			}
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x000960AC File Offset: 0x000942AC
		[MonoPInvokeCallback]
		internal static void OnTransferDeviceIdAccount(IntPtr address)
		{
			OnTransferDeviceIdAccountCallback onTransferDeviceIdAccountCallback = null;
			TransferDeviceIdAccountCallbackInfo transferDeviceIdAccountCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnTransferDeviceIdAccountCallback, TransferDeviceIdAccountCallbackInfoInternal, TransferDeviceIdAccountCallbackInfo>(address, out onTransferDeviceIdAccountCallback, out transferDeviceIdAccountCallbackInfo))
			{
				onTransferDeviceIdAccountCallback(transferDeviceIdAccountCallbackInfo);
			}
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x000960D0 File Offset: 0x000942D0
		[MonoPInvokeCallback]
		internal static void OnDeleteDeviceId(IntPtr address)
		{
			OnDeleteDeviceIdCallback onDeleteDeviceIdCallback = null;
			DeleteDeviceIdCallbackInfo deleteDeviceIdCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnDeleteDeviceIdCallback, DeleteDeviceIdCallbackInfoInternal, DeleteDeviceIdCallbackInfo>(address, out onDeleteDeviceIdCallback, out deleteDeviceIdCallbackInfo))
			{
				onDeleteDeviceIdCallback(deleteDeviceIdCallbackInfo);
			}
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x000960F4 File Offset: 0x000942F4
		[MonoPInvokeCallback]
		internal static void OnCreateDeviceId(IntPtr address)
		{
			OnCreateDeviceIdCallback onCreateDeviceIdCallback = null;
			CreateDeviceIdCallbackInfo createDeviceIdCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnCreateDeviceIdCallback, CreateDeviceIdCallbackInfoInternal, CreateDeviceIdCallbackInfo>(address, out onCreateDeviceIdCallback, out createDeviceIdCallbackInfo))
			{
				onCreateDeviceIdCallback(createDeviceIdCallbackInfo);
			}
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00096118 File Offset: 0x00094318
		[MonoPInvokeCallback]
		internal static void OnUnlinkAccount(IntPtr address)
		{
			OnUnlinkAccountCallback onUnlinkAccountCallback = null;
			UnlinkAccountCallbackInfo unlinkAccountCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnUnlinkAccountCallback, UnlinkAccountCallbackInfoInternal, UnlinkAccountCallbackInfo>(address, out onUnlinkAccountCallback, out unlinkAccountCallbackInfo))
			{
				onUnlinkAccountCallback(unlinkAccountCallbackInfo);
			}
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x0009613C File Offset: 0x0009433C
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

		// Token: 0x06004DAD RID: 19885 RVA: 0x00096160 File Offset: 0x00094360
		[MonoPInvokeCallback]
		internal static void OnCreateUser(IntPtr address)
		{
			OnCreateUserCallback onCreateUserCallback = null;
			CreateUserCallbackInfo createUserCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnCreateUserCallback, CreateUserCallbackInfoInternal, CreateUserCallbackInfo>(address, out onCreateUserCallback, out createUserCallbackInfo))
			{
				onCreateUserCallback(createUserCallbackInfo);
			}
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x00096184 File Offset: 0x00094384
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

		// Token: 0x06004DAF RID: 19887
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_ExternalAccountInfo_Release(IntPtr externalAccountInfo);

		// Token: 0x06004DB0 RID: 19888
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_RemoveNotifyLoginStatusChanged(IntPtr handle, ulong inId);

		// Token: 0x06004DB1 RID: 19889
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Connect_AddNotifyLoginStatusChanged(IntPtr handle, ref AddNotifyLoginStatusChangedOptionsInternal options, IntPtr clientData, OnLoginStatusChangedCallbackInternal notification);

		// Token: 0x06004DB2 RID: 19890
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_RemoveNotifyAuthExpiration(IntPtr handle, ulong inId);

		// Token: 0x06004DB3 RID: 19891
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_Connect_AddNotifyAuthExpiration(IntPtr handle, ref AddNotifyAuthExpirationOptionsInternal options, IntPtr clientData, OnAuthExpirationCallbackInternal notification);

		// Token: 0x06004DB4 RID: 19892
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern LoginStatus EOS_Connect_GetLoginStatus(IntPtr handle, IntPtr localUserId);

		// Token: 0x06004DB5 RID: 19893
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Connect_GetLoggedInUserByIndex(IntPtr handle, int index);

		// Token: 0x06004DB6 RID: 19894
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern int EOS_Connect_GetLoggedInUsersCount(IntPtr handle);

		// Token: 0x06004DB7 RID: 19895
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Connect_CopyProductUserInfo(IntPtr handle, ref CopyProductUserInfoOptionsInternal options, ref IntPtr outExternalAccountInfo);

		// Token: 0x06004DB8 RID: 19896
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Connect_CopyProductUserExternalAccountByAccountId(IntPtr handle, ref CopyProductUserExternalAccountByAccountIdOptionsInternal options, ref IntPtr outExternalAccountInfo);

		// Token: 0x06004DB9 RID: 19897
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Connect_CopyProductUserExternalAccountByAccountType(IntPtr handle, ref CopyProductUserExternalAccountByAccountTypeOptionsInternal options, ref IntPtr outExternalAccountInfo);

		// Token: 0x06004DBA RID: 19898
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Connect_CopyProductUserExternalAccountByIndex(IntPtr handle, ref CopyProductUserExternalAccountByIndexOptionsInternal options, ref IntPtr outExternalAccountInfo);

		// Token: 0x06004DBB RID: 19899
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Connect_GetProductUserExternalAccountCount(IntPtr handle, ref GetProductUserExternalAccountCountOptionsInternal options);

		// Token: 0x06004DBC RID: 19900
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Connect_GetProductUserIdMapping(IntPtr handle, ref GetProductUserIdMappingOptionsInternal options, StringBuilder outBuffer, ref int inOutBufferLength);

		// Token: 0x06004DBD RID: 19901
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_Connect_GetExternalAccountMapping(IntPtr handle, ref GetExternalAccountMappingsOptionsInternal options);

		// Token: 0x06004DBE RID: 19902
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_QueryProductUserIdMappings(IntPtr handle, ref QueryProductUserIdMappingsOptionsInternal options, IntPtr clientData, OnQueryProductUserIdMappingsCallbackInternal completionDelegate);

		// Token: 0x06004DBF RID: 19903
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_QueryExternalAccountMappings(IntPtr handle, ref QueryExternalAccountMappingsOptionsInternal options, IntPtr clientData, OnQueryExternalAccountMappingsCallbackInternal completionDelegate);

		// Token: 0x06004DC0 RID: 19904
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_TransferDeviceIdAccount(IntPtr handle, ref TransferDeviceIdAccountOptionsInternal options, IntPtr clientData, OnTransferDeviceIdAccountCallbackInternal completionDelegate);

		// Token: 0x06004DC1 RID: 19905
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_DeleteDeviceId(IntPtr handle, ref DeleteDeviceIdOptionsInternal options, IntPtr clientData, OnDeleteDeviceIdCallbackInternal completionDelegate);

		// Token: 0x06004DC2 RID: 19906
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_CreateDeviceId(IntPtr handle, ref CreateDeviceIdOptionsInternal options, IntPtr clientData, OnCreateDeviceIdCallbackInternal completionDelegate);

		// Token: 0x06004DC3 RID: 19907
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_UnlinkAccount(IntPtr handle, ref UnlinkAccountOptionsInternal options, IntPtr clientData, OnUnlinkAccountCallbackInternal completionDelegate);

		// Token: 0x06004DC4 RID: 19908
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_LinkAccount(IntPtr handle, ref LinkAccountOptionsInternal options, IntPtr clientData, OnLinkAccountCallbackInternal completionDelegate);

		// Token: 0x06004DC5 RID: 19909
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_CreateUser(IntPtr handle, ref CreateUserOptionsInternal options, IntPtr clientData, OnCreateUserCallbackInternal completionDelegate);

		// Token: 0x06004DC6 RID: 19910
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Connect_Login(IntPtr handle, ref LoginOptionsInternal options, IntPtr clientData, OnLoginCallbackInternal completionDelegate);

		// Token: 0x04001E10 RID: 7696
		public const int AddnotifyloginstatuschangedApiLatest = 1;

		// Token: 0x04001E11 RID: 7697
		public const int OnauthexpirationcallbackApiLatest = 1;

		// Token: 0x04001E12 RID: 7698
		public const int AddnotifyauthexpirationApiLatest = 1;

		// Token: 0x04001E13 RID: 7699
		public const int ExternalaccountinfoApiLatest = 1;

		// Token: 0x04001E14 RID: 7700
		public const int TimeUndefined = -1;

		// Token: 0x04001E15 RID: 7701
		public const int CopyproductuserinfoApiLatest = 1;

		// Token: 0x04001E16 RID: 7702
		public const int CopyproductuserexternalaccountbyaccountidApiLatest = 1;

		// Token: 0x04001E17 RID: 7703
		public const int CopyproductuserexternalaccountbyaccounttypeApiLatest = 1;

		// Token: 0x04001E18 RID: 7704
		public const int CopyproductuserexternalaccountbyindexApiLatest = 1;

		// Token: 0x04001E19 RID: 7705
		public const int GetproductuserexternalaccountcountApiLatest = 1;

		// Token: 0x04001E1A RID: 7706
		public const int GetproductuseridmappingApiLatest = 1;

		// Token: 0x04001E1B RID: 7707
		public const int QueryproductuseridmappingsApiLatest = 2;

		// Token: 0x04001E1C RID: 7708
		public const int GetexternalaccountmappingsApiLatest = 1;

		// Token: 0x04001E1D RID: 7709
		public const int GetexternalaccountmappingApiLatest = 1;

		// Token: 0x04001E1E RID: 7710
		public const int QueryexternalaccountmappingsMaxAccountIds = 128;

		// Token: 0x04001E1F RID: 7711
		public const int QueryexternalaccountmappingsApiLatest = 1;

		// Token: 0x04001E20 RID: 7712
		public const int TransferdeviceidaccountApiLatest = 1;

		// Token: 0x04001E21 RID: 7713
		public const int DeletedeviceidApiLatest = 1;

		// Token: 0x04001E22 RID: 7714
		public const int CreatedeviceidDevicemodelMaxLength = 64;

		// Token: 0x04001E23 RID: 7715
		public const int CreatedeviceidApiLatest = 1;

		// Token: 0x04001E24 RID: 7716
		public const int UnlinkaccountApiLatest = 1;

		// Token: 0x04001E25 RID: 7717
		public const int LinkaccountApiLatest = 1;

		// Token: 0x04001E26 RID: 7718
		public const int CreateuserApiLatest = 1;

		// Token: 0x04001E27 RID: 7719
		public const int LoginApiLatest = 2;

		// Token: 0x04001E28 RID: 7720
		public const int UserlogininfoApiLatest = 1;

		// Token: 0x04001E29 RID: 7721
		public const int UserlogininfoDisplaynameMaxLength = 32;

		// Token: 0x04001E2A RID: 7722
		public const int CredentialsApiLatest = 1;

		// Token: 0x04001E2B RID: 7723
		public const int ExternalAccountIdMaxLength = 256;
	}
}
