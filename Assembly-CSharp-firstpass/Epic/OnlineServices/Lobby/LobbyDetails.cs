using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000754 RID: 1876
	public sealed class LobbyDetails : Handle
	{
		// Token: 0x060045AD RID: 17837 RVA: 0x0008DBDF File Offset: 0x0008BDDF
		public LobbyDetails(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x060045AE RID: 17838 RVA: 0x0008DBE8 File Offset: 0x0008BDE8
		public ProductUserId GetLobbyOwner(LobbyDetailsGetLobbyOwnerOptions options)
		{
			LobbyDetailsGetLobbyOwnerOptionsInternal lobbyDetailsGetLobbyOwnerOptionsInternal = Helper.CopyProperties<LobbyDetailsGetLobbyOwnerOptionsInternal>(options);
			IntPtr intPtr = LobbyDetails.EOS_LobbyDetails_GetLobbyOwner(base.InnerHandle, ref lobbyDetailsGetLobbyOwnerOptionsInternal);
			Helper.TryMarshalDispose<LobbyDetailsGetLobbyOwnerOptionsInternal>(ref lobbyDetailsGetLobbyOwnerOptionsInternal);
			ProductUserId @default = Helper.GetDefault<ProductUserId>();
			Helper.TryMarshalGet<ProductUserId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060045AF RID: 17839 RVA: 0x0008DC20 File Offset: 0x0008BE20
		public Result CopyInfo(LobbyDetailsCopyInfoOptions options, out LobbyDetailsInfo outLobbyDetailsInfo)
		{
			LobbyDetailsCopyInfoOptionsInternal lobbyDetailsCopyInfoOptionsInternal = Helper.CopyProperties<LobbyDetailsCopyInfoOptionsInternal>(options);
			outLobbyDetailsInfo = Helper.GetDefault<LobbyDetailsInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyDetails.EOS_LobbyDetails_CopyInfo(base.InnerHandle, ref lobbyDetailsCopyInfoOptionsInternal, ref zero);
			Helper.TryMarshalDispose<LobbyDetailsCopyInfoOptionsInternal>(ref lobbyDetailsCopyInfoOptionsInternal);
			if (Helper.TryMarshalGet<LobbyDetailsInfoInternal, LobbyDetailsInfo>(zero, out outLobbyDetailsInfo))
			{
				LobbyDetails.EOS_LobbyDetails_Info_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060045B0 RID: 17840 RVA: 0x0008DC78 File Offset: 0x0008BE78
		public uint GetAttributeCount(LobbyDetailsGetAttributeCountOptions options)
		{
			LobbyDetailsGetAttributeCountOptionsInternal lobbyDetailsGetAttributeCountOptionsInternal = Helper.CopyProperties<LobbyDetailsGetAttributeCountOptionsInternal>(options);
			uint num = LobbyDetails.EOS_LobbyDetails_GetAttributeCount(base.InnerHandle, ref lobbyDetailsGetAttributeCountOptionsInternal);
			Helper.TryMarshalDispose<LobbyDetailsGetAttributeCountOptionsInternal>(ref lobbyDetailsGetAttributeCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x060045B1 RID: 17841 RVA: 0x0008DCB0 File Offset: 0x0008BEB0
		public Result CopyAttributeByIndex(LobbyDetailsCopyAttributeByIndexOptions options, out Attribute outAttribute)
		{
			LobbyDetailsCopyAttributeByIndexOptionsInternal lobbyDetailsCopyAttributeByIndexOptionsInternal = Helper.CopyProperties<LobbyDetailsCopyAttributeByIndexOptionsInternal>(options);
			outAttribute = Helper.GetDefault<Attribute>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyDetails.EOS_LobbyDetails_CopyAttributeByIndex(base.InnerHandle, ref lobbyDetailsCopyAttributeByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<LobbyDetailsCopyAttributeByIndexOptionsInternal>(ref lobbyDetailsCopyAttributeByIndexOptionsInternal);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero, out outAttribute))
			{
				LobbyDetails.EOS_Lobby_Attribute_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x0008DD08 File Offset: 0x0008BF08
		public Result CopyAttributeByKey(LobbyDetailsCopyAttributeByKeyOptions options, out Attribute outAttribute)
		{
			LobbyDetailsCopyAttributeByKeyOptionsInternal lobbyDetailsCopyAttributeByKeyOptionsInternal = Helper.CopyProperties<LobbyDetailsCopyAttributeByKeyOptionsInternal>(options);
			outAttribute = Helper.GetDefault<Attribute>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyDetails.EOS_LobbyDetails_CopyAttributeByKey(base.InnerHandle, ref lobbyDetailsCopyAttributeByKeyOptionsInternal, ref zero);
			Helper.TryMarshalDispose<LobbyDetailsCopyAttributeByKeyOptionsInternal>(ref lobbyDetailsCopyAttributeByKeyOptionsInternal);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero, out outAttribute))
			{
				LobbyDetails.EOS_Lobby_Attribute_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060045B3 RID: 17843 RVA: 0x0008DD60 File Offset: 0x0008BF60
		public uint GetMemberCount(LobbyDetailsGetMemberCountOptions options)
		{
			LobbyDetailsGetMemberCountOptionsInternal lobbyDetailsGetMemberCountOptionsInternal = Helper.CopyProperties<LobbyDetailsGetMemberCountOptionsInternal>(options);
			uint num = LobbyDetails.EOS_LobbyDetails_GetMemberCount(base.InnerHandle, ref lobbyDetailsGetMemberCountOptionsInternal);
			Helper.TryMarshalDispose<LobbyDetailsGetMemberCountOptionsInternal>(ref lobbyDetailsGetMemberCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x060045B4 RID: 17844 RVA: 0x0008DD98 File Offset: 0x0008BF98
		public ProductUserId GetMemberByIndex(LobbyDetailsGetMemberByIndexOptions options)
		{
			LobbyDetailsGetMemberByIndexOptionsInternal lobbyDetailsGetMemberByIndexOptionsInternal = Helper.CopyProperties<LobbyDetailsGetMemberByIndexOptionsInternal>(options);
			IntPtr intPtr = LobbyDetails.EOS_LobbyDetails_GetMemberByIndex(base.InnerHandle, ref lobbyDetailsGetMemberByIndexOptionsInternal);
			Helper.TryMarshalDispose<LobbyDetailsGetMemberByIndexOptionsInternal>(ref lobbyDetailsGetMemberByIndexOptionsInternal);
			ProductUserId @default = Helper.GetDefault<ProductUserId>();
			Helper.TryMarshalGet<ProductUserId>(intPtr, out @default);
			return @default;
		}

		// Token: 0x060045B5 RID: 17845 RVA: 0x0008DDD0 File Offset: 0x0008BFD0
		public uint GetMemberAttributeCount(LobbyDetailsGetMemberAttributeCountOptions options)
		{
			LobbyDetailsGetMemberAttributeCountOptionsInternal lobbyDetailsGetMemberAttributeCountOptionsInternal = Helper.CopyProperties<LobbyDetailsGetMemberAttributeCountOptionsInternal>(options);
			uint num = LobbyDetails.EOS_LobbyDetails_GetMemberAttributeCount(base.InnerHandle, ref lobbyDetailsGetMemberAttributeCountOptionsInternal);
			Helper.TryMarshalDispose<LobbyDetailsGetMemberAttributeCountOptionsInternal>(ref lobbyDetailsGetMemberAttributeCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x060045B6 RID: 17846 RVA: 0x0008DE08 File Offset: 0x0008C008
		public Result CopyMemberAttributeByIndex(LobbyDetailsCopyMemberAttributeByIndexOptions options, out Attribute outAttribute)
		{
			LobbyDetailsCopyMemberAttributeByIndexOptionsInternal lobbyDetailsCopyMemberAttributeByIndexOptionsInternal = Helper.CopyProperties<LobbyDetailsCopyMemberAttributeByIndexOptionsInternal>(options);
			outAttribute = Helper.GetDefault<Attribute>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyDetails.EOS_LobbyDetails_CopyMemberAttributeByIndex(base.InnerHandle, ref lobbyDetailsCopyMemberAttributeByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<LobbyDetailsCopyMemberAttributeByIndexOptionsInternal>(ref lobbyDetailsCopyMemberAttributeByIndexOptionsInternal);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero, out outAttribute))
			{
				LobbyDetails.EOS_Lobby_Attribute_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060045B7 RID: 17847 RVA: 0x0008DE60 File Offset: 0x0008C060
		public Result CopyMemberAttributeByKey(LobbyDetailsCopyMemberAttributeByKeyOptions options, out Attribute outAttribute)
		{
			LobbyDetailsCopyMemberAttributeByKeyOptionsInternal lobbyDetailsCopyMemberAttributeByKeyOptionsInternal = Helper.CopyProperties<LobbyDetailsCopyMemberAttributeByKeyOptionsInternal>(options);
			outAttribute = Helper.GetDefault<Attribute>();
			IntPtr zero = IntPtr.Zero;
			Result result = LobbyDetails.EOS_LobbyDetails_CopyMemberAttributeByKey(base.InnerHandle, ref lobbyDetailsCopyMemberAttributeByKeyOptionsInternal, ref zero);
			Helper.TryMarshalDispose<LobbyDetailsCopyMemberAttributeByKeyOptionsInternal>(ref lobbyDetailsCopyMemberAttributeByKeyOptionsInternal);
			if (Helper.TryMarshalGet<AttributeInternal, Attribute>(zero, out outAttribute))
			{
				LobbyDetails.EOS_Lobby_Attribute_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x060045B8 RID: 17848 RVA: 0x0008DEB6 File Offset: 0x0008C0B6
		public void Release()
		{
			LobbyDetails.EOS_LobbyDetails_Release(base.InnerHandle);
		}

		// Token: 0x060045B9 RID: 17849
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Lobby_Attribute_Release(IntPtr lobbyAttribute);

		// Token: 0x060045BA RID: 17850
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_LobbyDetails_Info_Release(IntPtr lobbyDetailsInfo);

		// Token: 0x060045BB RID: 17851
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_LobbyDetails_Release(IntPtr lobbyHandle);

		// Token: 0x060045BC RID: 17852
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyDetails_CopyMemberAttributeByKey(IntPtr handle, ref LobbyDetailsCopyMemberAttributeByKeyOptionsInternal options, ref IntPtr outAttribute);

		// Token: 0x060045BD RID: 17853
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyDetails_CopyMemberAttributeByIndex(IntPtr handle, ref LobbyDetailsCopyMemberAttributeByIndexOptionsInternal options, ref IntPtr outAttribute);

		// Token: 0x060045BE RID: 17854
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_LobbyDetails_GetMemberAttributeCount(IntPtr handle, ref LobbyDetailsGetMemberAttributeCountOptionsInternal options);

		// Token: 0x060045BF RID: 17855
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_LobbyDetails_GetMemberByIndex(IntPtr handle, ref LobbyDetailsGetMemberByIndexOptionsInternal options);

		// Token: 0x060045C0 RID: 17856
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_LobbyDetails_GetMemberCount(IntPtr handle, ref LobbyDetailsGetMemberCountOptionsInternal options);

		// Token: 0x060045C1 RID: 17857
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyDetails_CopyAttributeByKey(IntPtr handle, ref LobbyDetailsCopyAttributeByKeyOptionsInternal options, ref IntPtr outAttribute);

		// Token: 0x060045C2 RID: 17858
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyDetails_CopyAttributeByIndex(IntPtr handle, ref LobbyDetailsCopyAttributeByIndexOptionsInternal options, ref IntPtr outAttribute);

		// Token: 0x060045C3 RID: 17859
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_LobbyDetails_GetAttributeCount(IntPtr handle, ref LobbyDetailsGetAttributeCountOptionsInternal options);

		// Token: 0x060045C4 RID: 17860
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_LobbyDetails_CopyInfo(IntPtr handle, ref LobbyDetailsCopyInfoOptionsInternal options, ref IntPtr outLobbyDetailsInfo);

		// Token: 0x060045C5 RID: 17861
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern IntPtr EOS_LobbyDetails_GetLobbyOwner(IntPtr handle, ref LobbyDetailsGetLobbyOwnerOptionsInternal options);
	}
}
