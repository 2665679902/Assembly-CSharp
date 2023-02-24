using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020006FE RID: 1790
	public sealed class P2PInterface : Handle
	{
		// Token: 0x060043B1 RID: 17329 RVA: 0x0008B7FE File Offset: 0x000899FE
		public P2PInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x0008B808 File Offset: 0x00089A08
		public Result SendPacket(SendPacketOptions options)
		{
			SendPacketOptionsInternal sendPacketOptionsInternal = Helper.CopyProperties<SendPacketOptionsInternal>(options);
			Result result = P2PInterface.EOS_P2P_SendPacket(base.InnerHandle, ref sendPacketOptionsInternal);
			Helper.TryMarshalDispose<SendPacketOptionsInternal>(ref sendPacketOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x0008B840 File Offset: 0x00089A40
		public Result GetNextReceivedPacketSize(GetNextReceivedPacketSizeOptions options, out uint outPacketSizeBytes)
		{
			GetNextReceivedPacketSizeOptionsInternal getNextReceivedPacketSizeOptionsInternal = Helper.CopyProperties<GetNextReceivedPacketSizeOptionsInternal>(options);
			outPacketSizeBytes = Helper.GetDefault<uint>();
			Result result = P2PInterface.EOS_P2P_GetNextReceivedPacketSize(base.InnerHandle, ref getNextReceivedPacketSizeOptionsInternal, ref outPacketSizeBytes);
			Helper.TryMarshalDispose<GetNextReceivedPacketSizeOptionsInternal>(ref getNextReceivedPacketSizeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x0008B880 File Offset: 0x00089A80
		public Result ReceivePacket(ReceivePacketOptions options, out ProductUserId outPeerId, out SocketId outSocketId, out byte outChannel, ref byte[] outData, out uint outBytesWritten)
		{
			ReceivePacketOptionsInternal receivePacketOptionsInternal = Helper.CopyProperties<ReceivePacketOptionsInternal>(options);
			outPeerId = Helper.GetDefault<ProductUserId>();
			IntPtr zero = IntPtr.Zero;
			outSocketId = Helper.GetDefault<SocketId>();
			SocketIdInternal socketIdInternal = default(SocketIdInternal);
			outChannel = Helper.GetDefault<byte>();
			outBytesWritten = Helper.GetDefault<uint>();
			Result result = P2PInterface.EOS_P2P_ReceivePacket(base.InnerHandle, ref receivePacketOptionsInternal, ref zero, ref socketIdInternal, ref outChannel, outData, ref outBytesWritten);
			Helper.TryMarshalDispose<ReceivePacketOptionsInternal>(ref receivePacketOptionsInternal);
			Helper.TryMarshalGet<ProductUserId>(zero, out outPeerId);
			outSocketId = Helper.CopyProperties<SocketId>(socketIdInternal);
			Helper.TryMarshalDispose<SocketIdInternal>(ref socketIdInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x0008B90C File Offset: 0x00089B0C
		public ulong AddNotifyPeerConnectionRequest(AddNotifyPeerConnectionRequestOptions options, object clientData, OnIncomingConnectionRequestCallback connectionRequestHandler)
		{
			AddNotifyPeerConnectionRequestOptionsInternal addNotifyPeerConnectionRequestOptionsInternal = Helper.CopyProperties<AddNotifyPeerConnectionRequestOptionsInternal>(options);
			OnIncomingConnectionRequestCallbackInternal onIncomingConnectionRequestCallbackInternal = new OnIncomingConnectionRequestCallbackInternal(P2PInterface.OnIncomingConnectionRequest);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, connectionRequestHandler, onIncomingConnectionRequestCallbackInternal, Array.Empty<Delegate>());
			ulong num = P2PInterface.EOS_P2P_AddNotifyPeerConnectionRequest(base.InnerHandle, ref addNotifyPeerConnectionRequestOptionsInternal, zero, onIncomingConnectionRequestCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyPeerConnectionRequestOptionsInternal>(ref addNotifyPeerConnectionRequestOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x0008B974 File Offset: 0x00089B74
		public void RemoveNotifyPeerConnectionRequest(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			P2PInterface.EOS_P2P_RemoveNotifyPeerConnectionRequest(base.InnerHandle, notificationId);
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x0008B98C File Offset: 0x00089B8C
		public ulong AddNotifyPeerConnectionClosed(AddNotifyPeerConnectionClosedOptions options, object clientData, OnRemoteConnectionClosedCallback connectionClosedHandler)
		{
			AddNotifyPeerConnectionClosedOptionsInternal addNotifyPeerConnectionClosedOptionsInternal = Helper.CopyProperties<AddNotifyPeerConnectionClosedOptionsInternal>(options);
			OnRemoteConnectionClosedCallbackInternal onRemoteConnectionClosedCallbackInternal = new OnRemoteConnectionClosedCallbackInternal(P2PInterface.OnRemoteConnectionClosed);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, connectionClosedHandler, onRemoteConnectionClosedCallbackInternal, Array.Empty<Delegate>());
			ulong num = P2PInterface.EOS_P2P_AddNotifyPeerConnectionClosed(base.InnerHandle, ref addNotifyPeerConnectionClosedOptionsInternal, zero, onRemoteConnectionClosedCallbackInternal);
			Helper.TryMarshalDispose<AddNotifyPeerConnectionClosedOptionsInternal>(ref addNotifyPeerConnectionClosedOptionsInternal);
			Helper.TryAssignNotificationIdToCallback(zero, num);
			ulong @default = Helper.GetDefault<ulong>();
			Helper.TryMarshalGet<ulong>(num, out @default);
			return @default;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x0008B9F4 File Offset: 0x00089BF4
		public void RemoveNotifyPeerConnectionClosed(ulong notificationId)
		{
			Helper.TryRemoveCallbackByNotificationId(notificationId);
			P2PInterface.EOS_P2P_RemoveNotifyPeerConnectionClosed(base.InnerHandle, notificationId);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x0008BA0C File Offset: 0x00089C0C
		public Result AcceptConnection(AcceptConnectionOptions options)
		{
			AcceptConnectionOptionsInternal acceptConnectionOptionsInternal = Helper.CopyProperties<AcceptConnectionOptionsInternal>(options);
			Result result = P2PInterface.EOS_P2P_AcceptConnection(base.InnerHandle, ref acceptConnectionOptionsInternal);
			Helper.TryMarshalDispose<AcceptConnectionOptionsInternal>(ref acceptConnectionOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x0008BA44 File Offset: 0x00089C44
		public Result CloseConnection(CloseConnectionOptions options)
		{
			CloseConnectionOptionsInternal closeConnectionOptionsInternal = Helper.CopyProperties<CloseConnectionOptionsInternal>(options);
			Result result = P2PInterface.EOS_P2P_CloseConnection(base.InnerHandle, ref closeConnectionOptionsInternal);
			Helper.TryMarshalDispose<CloseConnectionOptionsInternal>(ref closeConnectionOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x0008BA7C File Offset: 0x00089C7C
		public Result CloseConnections(CloseConnectionsOptions options)
		{
			CloseConnectionsOptionsInternal closeConnectionsOptionsInternal = Helper.CopyProperties<CloseConnectionsOptionsInternal>(options);
			Result result = P2PInterface.EOS_P2P_CloseConnections(base.InnerHandle, ref closeConnectionsOptionsInternal);
			Helper.TryMarshalDispose<CloseConnectionsOptionsInternal>(ref closeConnectionsOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x0008BAB4 File Offset: 0x00089CB4
		public void QueryNATType(QueryNATTypeOptions options, object clientData, OnQueryNATTypeCompleteCallback nATTypeQueriedHandler)
		{
			QueryNATTypeOptionsInternal queryNATTypeOptionsInternal = Helper.CopyProperties<QueryNATTypeOptionsInternal>(options);
			OnQueryNATTypeCompleteCallbackInternal onQueryNATTypeCompleteCallbackInternal = new OnQueryNATTypeCompleteCallbackInternal(P2PInterface.OnQueryNATTypeComplete);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, nATTypeQueriedHandler, onQueryNATTypeCompleteCallbackInternal, Array.Empty<Delegate>());
			P2PInterface.EOS_P2P_QueryNATType(base.InnerHandle, ref queryNATTypeOptionsInternal, zero, onQueryNATTypeCompleteCallbackInternal);
			Helper.TryMarshalDispose<QueryNATTypeOptionsInternal>(ref queryNATTypeOptionsInternal);
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x0008BB04 File Offset: 0x00089D04
		public Result GetNATType(GetNATTypeOptions options, out NATType outNATType)
		{
			GetNATTypeOptionsInternal getNATTypeOptionsInternal = Helper.CopyProperties<GetNATTypeOptionsInternal>(options);
			outNATType = Helper.GetDefault<NATType>();
			Result result = P2PInterface.EOS_P2P_GetNATType(base.InnerHandle, ref getNATTypeOptionsInternal, ref outNATType);
			Helper.TryMarshalDispose<GetNATTypeOptionsInternal>(ref getNATTypeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x0008BB44 File Offset: 0x00089D44
		public Result SetRelayControl(SetRelayControlOptions options)
		{
			SetRelayControlOptionsInternal setRelayControlOptionsInternal = Helper.CopyProperties<SetRelayControlOptionsInternal>(options);
			Result result = P2PInterface.EOS_P2P_SetRelayControl(base.InnerHandle, ref setRelayControlOptionsInternal);
			Helper.TryMarshalDispose<SetRelayControlOptionsInternal>(ref setRelayControlOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x0008BB7C File Offset: 0x00089D7C
		public Result GetRelayControl(GetRelayControlOptions options, out RelayControl outRelayControl)
		{
			GetRelayControlOptionsInternal getRelayControlOptionsInternal = Helper.CopyProperties<GetRelayControlOptionsInternal>(options);
			outRelayControl = Helper.GetDefault<RelayControl>();
			Result result = P2PInterface.EOS_P2P_GetRelayControl(base.InnerHandle, ref getRelayControlOptionsInternal, ref outRelayControl);
			Helper.TryMarshalDispose<GetRelayControlOptionsInternal>(ref getRelayControlOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043C0 RID: 17344 RVA: 0x0008BBBC File Offset: 0x00089DBC
		public Result SetPortRange(SetPortRangeOptions options)
		{
			SetPortRangeOptionsInternal setPortRangeOptionsInternal = Helper.CopyProperties<SetPortRangeOptionsInternal>(options);
			Result result = P2PInterface.EOS_P2P_SetPortRange(base.InnerHandle, ref setPortRangeOptionsInternal);
			Helper.TryMarshalDispose<SetPortRangeOptionsInternal>(ref setPortRangeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043C1 RID: 17345 RVA: 0x0008BBF4 File Offset: 0x00089DF4
		public Result GetPortRange(GetPortRangeOptions options, out ushort outPort, out ushort outNumAdditionalPortsToTry)
		{
			GetPortRangeOptionsInternal getPortRangeOptionsInternal = Helper.CopyProperties<GetPortRangeOptionsInternal>(options);
			outPort = Helper.GetDefault<ushort>();
			outNumAdditionalPortsToTry = Helper.GetDefault<ushort>();
			Result result = P2PInterface.EOS_P2P_GetPortRange(base.InnerHandle, ref getPortRangeOptionsInternal, ref outPort, ref outNumAdditionalPortsToTry);
			Helper.TryMarshalDispose<GetPortRangeOptionsInternal>(ref getPortRangeOptionsInternal);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060043C2 RID: 17346 RVA: 0x0008BC3C File Offset: 0x00089E3C
		[MonoPInvokeCallback]
		internal static void OnQueryNATTypeComplete(IntPtr address)
		{
			OnQueryNATTypeCompleteCallback onQueryNATTypeCompleteCallback = null;
			OnQueryNATTypeCompleteInfo onQueryNATTypeCompleteInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryNATTypeCompleteCallback, OnQueryNATTypeCompleteInfoInternal, OnQueryNATTypeCompleteInfo>(address, out onQueryNATTypeCompleteCallback, out onQueryNATTypeCompleteInfo))
			{
				onQueryNATTypeCompleteCallback(onQueryNATTypeCompleteInfo);
			}
		}

		// Token: 0x060043C3 RID: 17347 RVA: 0x0008BC60 File Offset: 0x00089E60
		[MonoPInvokeCallback]
		internal static void OnRemoteConnectionClosed(IntPtr address)
		{
			OnRemoteConnectionClosedCallback onRemoteConnectionClosedCallback = null;
			OnRemoteConnectionClosedInfo onRemoteConnectionClosedInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnRemoteConnectionClosedCallback, OnRemoteConnectionClosedInfoInternal, OnRemoteConnectionClosedInfo>(address, out onRemoteConnectionClosedCallback, out onRemoteConnectionClosedInfo))
			{
				onRemoteConnectionClosedCallback(onRemoteConnectionClosedInfo);
			}
		}

		// Token: 0x060043C4 RID: 17348 RVA: 0x0008BC84 File Offset: 0x00089E84
		[MonoPInvokeCallback]
		internal static void OnIncomingConnectionRequest(IntPtr address)
		{
			OnIncomingConnectionRequestCallback onIncomingConnectionRequestCallback = null;
			OnIncomingConnectionRequestInfo onIncomingConnectionRequestInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnIncomingConnectionRequestCallback, OnIncomingConnectionRequestInfoInternal, OnIncomingConnectionRequestInfo>(address, out onIncomingConnectionRequestCallback, out onIncomingConnectionRequestInfo))
			{
				onIncomingConnectionRequestCallback(onIncomingConnectionRequestInfo);
			}
		}

		// Token: 0x060043C5 RID: 17349
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_GetPortRange(IntPtr handle, ref GetPortRangeOptionsInternal options, ref ushort outPort, ref ushort outNumAdditionalPortsToTry);

		// Token: 0x060043C6 RID: 17350
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_SetPortRange(IntPtr handle, ref SetPortRangeOptionsInternal options);

		// Token: 0x060043C7 RID: 17351
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_GetRelayControl(IntPtr handle, ref GetRelayControlOptionsInternal options, ref RelayControl outRelayControl);

		// Token: 0x060043C8 RID: 17352
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_SetRelayControl(IntPtr handle, ref SetRelayControlOptionsInternal options);

		// Token: 0x060043C9 RID: 17353
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_GetNATType(IntPtr handle, ref GetNATTypeOptionsInternal options, ref NATType outNATType);

		// Token: 0x060043CA RID: 17354
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_P2P_QueryNATType(IntPtr handle, ref QueryNATTypeOptionsInternal options, IntPtr clientData, OnQueryNATTypeCompleteCallbackInternal nATTypeQueriedHandler);

		// Token: 0x060043CB RID: 17355
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_CloseConnections(IntPtr handle, ref CloseConnectionsOptionsInternal options);

		// Token: 0x060043CC RID: 17356
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_CloseConnection(IntPtr handle, ref CloseConnectionOptionsInternal options);

		// Token: 0x060043CD RID: 17357
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_AcceptConnection(IntPtr handle, ref AcceptConnectionOptionsInternal options);

		// Token: 0x060043CE RID: 17358
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_P2P_RemoveNotifyPeerConnectionClosed(IntPtr handle, ulong notificationId);

		// Token: 0x060043CF RID: 17359
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_P2P_AddNotifyPeerConnectionClosed(IntPtr handle, ref AddNotifyPeerConnectionClosedOptionsInternal options, IntPtr clientData, OnRemoteConnectionClosedCallbackInternal connectionClosedHandler);

		// Token: 0x060043D0 RID: 17360
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_P2P_RemoveNotifyPeerConnectionRequest(IntPtr handle, ulong notificationId);

		// Token: 0x060043D1 RID: 17361
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern ulong EOS_P2P_AddNotifyPeerConnectionRequest(IntPtr handle, ref AddNotifyPeerConnectionRequestOptionsInternal options, IntPtr clientData, OnIncomingConnectionRequestCallbackInternal connectionRequestHandler);

		// Token: 0x060043D2 RID: 17362
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_ReceivePacket(IntPtr handle, ref ReceivePacketOptionsInternal options, ref IntPtr outPeerId, ref SocketIdInternal outSocketId, ref byte outChannel, byte[] outData, ref uint outBytesWritten);

		// Token: 0x060043D3 RID: 17363
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_GetNextReceivedPacketSize(IntPtr handle, ref GetNextReceivedPacketSizeOptionsInternal options, ref uint outPacketSizeBytes);

		// Token: 0x060043D4 RID: 17364
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_P2P_SendPacket(IntPtr handle, ref SendPacketOptionsInternal options);

		// Token: 0x040019F5 RID: 6645
		public const int GetportrangeApiLatest = 1;

		// Token: 0x040019F6 RID: 6646
		public const int SetportrangeApiLatest = 1;

		// Token: 0x040019F7 RID: 6647
		public const int GetrelaycontrolApiLatest = 1;

		// Token: 0x040019F8 RID: 6648
		public const int SetrelaycontrolApiLatest = 1;

		// Token: 0x040019F9 RID: 6649
		public const int GetnattypeApiLatest = 1;

		// Token: 0x040019FA RID: 6650
		public const int QuerynattypeApiLatest = 1;

		// Token: 0x040019FB RID: 6651
		public const int CloseconnectionsApiLatest = 1;

		// Token: 0x040019FC RID: 6652
		public const int CloseconnectionApiLatest = 1;

		// Token: 0x040019FD RID: 6653
		public const int AcceptconnectionApiLatest = 1;

		// Token: 0x040019FE RID: 6654
		public const int AddnotifypeerconnectionclosedApiLatest = 1;

		// Token: 0x040019FF RID: 6655
		public const int AddnotifypeerconnectionrequestApiLatest = 1;

		// Token: 0x04001A00 RID: 6656
		public const int ReceivepacketApiLatest = 2;

		// Token: 0x04001A01 RID: 6657
		public const int GetnextreceivedpacketsizeApiLatest = 2;

		// Token: 0x04001A02 RID: 6658
		public const int SendpacketApiLatest = 2;

		// Token: 0x04001A03 RID: 6659
		public const int SocketidApiLatest = 1;

		// Token: 0x04001A04 RID: 6660
		public const int MaxConnections = 32;

		// Token: 0x04001A05 RID: 6661
		public const int MaxPacketSize = 1170;
	}
}
