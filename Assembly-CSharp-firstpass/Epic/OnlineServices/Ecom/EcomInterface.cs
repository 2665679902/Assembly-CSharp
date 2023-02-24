using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x02000848 RID: 2120
	public sealed class EcomInterface : Handle
	{
		// Token: 0x06004BCD RID: 19405 RVA: 0x00093C33 File Offset: 0x00091E33
		public EcomInterface(IntPtr innerHandle)
			: base(innerHandle)
		{
		}

		// Token: 0x06004BCE RID: 19406 RVA: 0x00093C3C File Offset: 0x00091E3C
		public void QueryOwnership(QueryOwnershipOptions options, object clientData, OnQueryOwnershipCallback completionDelegate)
		{
			QueryOwnershipOptionsInternal queryOwnershipOptionsInternal = Helper.CopyProperties<QueryOwnershipOptionsInternal>(options);
			OnQueryOwnershipCallbackInternal onQueryOwnershipCallbackInternal = new OnQueryOwnershipCallbackInternal(EcomInterface.OnQueryOwnership);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryOwnershipCallbackInternal, Array.Empty<Delegate>());
			EcomInterface.EOS_Ecom_QueryOwnership(base.InnerHandle, ref queryOwnershipOptionsInternal, zero, onQueryOwnershipCallbackInternal);
			Helper.TryMarshalDispose<QueryOwnershipOptionsInternal>(ref queryOwnershipOptionsInternal);
		}

		// Token: 0x06004BCF RID: 19407 RVA: 0x00093C8C File Offset: 0x00091E8C
		public void QueryOwnershipToken(QueryOwnershipTokenOptions options, object clientData, OnQueryOwnershipTokenCallback completionDelegate)
		{
			QueryOwnershipTokenOptionsInternal queryOwnershipTokenOptionsInternal = Helper.CopyProperties<QueryOwnershipTokenOptionsInternal>(options);
			OnQueryOwnershipTokenCallbackInternal onQueryOwnershipTokenCallbackInternal = new OnQueryOwnershipTokenCallbackInternal(EcomInterface.OnQueryOwnershipToken);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryOwnershipTokenCallbackInternal, Array.Empty<Delegate>());
			EcomInterface.EOS_Ecom_QueryOwnershipToken(base.InnerHandle, ref queryOwnershipTokenOptionsInternal, zero, onQueryOwnershipTokenCallbackInternal);
			Helper.TryMarshalDispose<QueryOwnershipTokenOptionsInternal>(ref queryOwnershipTokenOptionsInternal);
		}

		// Token: 0x06004BD0 RID: 19408 RVA: 0x00093CDC File Offset: 0x00091EDC
		public void QueryEntitlements(QueryEntitlementsOptions options, object clientData, OnQueryEntitlementsCallback completionDelegate)
		{
			QueryEntitlementsOptionsInternal queryEntitlementsOptionsInternal = Helper.CopyProperties<QueryEntitlementsOptionsInternal>(options);
			OnQueryEntitlementsCallbackInternal onQueryEntitlementsCallbackInternal = new OnQueryEntitlementsCallbackInternal(EcomInterface.OnQueryEntitlements);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryEntitlementsCallbackInternal, Array.Empty<Delegate>());
			EcomInterface.EOS_Ecom_QueryEntitlements(base.InnerHandle, ref queryEntitlementsOptionsInternal, zero, onQueryEntitlementsCallbackInternal);
			Helper.TryMarshalDispose<QueryEntitlementsOptionsInternal>(ref queryEntitlementsOptionsInternal);
		}

		// Token: 0x06004BD1 RID: 19409 RVA: 0x00093D2C File Offset: 0x00091F2C
		public void QueryOffers(QueryOffersOptions options, object clientData, OnQueryOffersCallback completionDelegate)
		{
			QueryOffersOptionsInternal queryOffersOptionsInternal = Helper.CopyProperties<QueryOffersOptionsInternal>(options);
			OnQueryOffersCallbackInternal onQueryOffersCallbackInternal = new OnQueryOffersCallbackInternal(EcomInterface.OnQueryOffers);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onQueryOffersCallbackInternal, Array.Empty<Delegate>());
			EcomInterface.EOS_Ecom_QueryOffers(base.InnerHandle, ref queryOffersOptionsInternal, zero, onQueryOffersCallbackInternal);
			Helper.TryMarshalDispose<QueryOffersOptionsInternal>(ref queryOffersOptionsInternal);
		}

		// Token: 0x06004BD2 RID: 19410 RVA: 0x00093D7C File Offset: 0x00091F7C
		public void Checkout(CheckoutOptions options, object clientData, OnCheckoutCallback completionDelegate)
		{
			CheckoutOptionsInternal checkoutOptionsInternal = Helper.CopyProperties<CheckoutOptionsInternal>(options);
			OnCheckoutCallbackInternal onCheckoutCallbackInternal = new OnCheckoutCallbackInternal(EcomInterface.OnCheckout);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onCheckoutCallbackInternal, Array.Empty<Delegate>());
			EcomInterface.EOS_Ecom_Checkout(base.InnerHandle, ref checkoutOptionsInternal, zero, onCheckoutCallbackInternal);
			Helper.TryMarshalDispose<CheckoutOptionsInternal>(ref checkoutOptionsInternal);
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x00093DCC File Offset: 0x00091FCC
		public void RedeemEntitlements(RedeemEntitlementsOptions options, object clientData, OnRedeemEntitlementsCallback completionDelegate)
		{
			RedeemEntitlementsOptionsInternal redeemEntitlementsOptionsInternal = Helper.CopyProperties<RedeemEntitlementsOptionsInternal>(options);
			OnRedeemEntitlementsCallbackInternal onRedeemEntitlementsCallbackInternal = new OnRedeemEntitlementsCallbackInternal(EcomInterface.OnRedeemEntitlements);
			IntPtr zero = IntPtr.Zero;
			Helper.AddCallback(ref zero, clientData, completionDelegate, onRedeemEntitlementsCallbackInternal, Array.Empty<Delegate>());
			EcomInterface.EOS_Ecom_RedeemEntitlements(base.InnerHandle, ref redeemEntitlementsOptionsInternal, zero, onRedeemEntitlementsCallbackInternal);
			Helper.TryMarshalDispose<RedeemEntitlementsOptionsInternal>(ref redeemEntitlementsOptionsInternal);
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x00093E1C File Offset: 0x0009201C
		public uint GetEntitlementsCount(GetEntitlementsCountOptions options)
		{
			GetEntitlementsCountOptionsInternal getEntitlementsCountOptionsInternal = Helper.CopyProperties<GetEntitlementsCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetEntitlementsCount(base.InnerHandle, ref getEntitlementsCountOptionsInternal);
			Helper.TryMarshalDispose<GetEntitlementsCountOptionsInternal>(ref getEntitlementsCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x00093E54 File Offset: 0x00092054
		public uint GetEntitlementsByNameCount(GetEntitlementsByNameCountOptions options)
		{
			GetEntitlementsByNameCountOptionsInternal getEntitlementsByNameCountOptionsInternal = Helper.CopyProperties<GetEntitlementsByNameCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetEntitlementsByNameCount(base.InnerHandle, ref getEntitlementsByNameCountOptionsInternal);
			Helper.TryMarshalDispose<GetEntitlementsByNameCountOptionsInternal>(ref getEntitlementsByNameCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00093E8C File Offset: 0x0009208C
		public Result CopyEntitlementByIndex(CopyEntitlementByIndexOptions options, out Entitlement outEntitlement)
		{
			CopyEntitlementByIndexOptionsInternal copyEntitlementByIndexOptionsInternal = Helper.CopyProperties<CopyEntitlementByIndexOptionsInternal>(options);
			outEntitlement = Helper.GetDefault<Entitlement>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyEntitlementByIndex(base.InnerHandle, ref copyEntitlementByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyEntitlementByIndexOptionsInternal>(ref copyEntitlementByIndexOptionsInternal);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero, out outEntitlement))
			{
				EcomInterface.EOS_Ecom_Entitlement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x00093EE4 File Offset: 0x000920E4
		public Result CopyEntitlementByNameAndIndex(CopyEntitlementByNameAndIndexOptions options, out Entitlement outEntitlement)
		{
			CopyEntitlementByNameAndIndexOptionsInternal copyEntitlementByNameAndIndexOptionsInternal = Helper.CopyProperties<CopyEntitlementByNameAndIndexOptionsInternal>(options);
			outEntitlement = Helper.GetDefault<Entitlement>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyEntitlementByNameAndIndex(base.InnerHandle, ref copyEntitlementByNameAndIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyEntitlementByNameAndIndexOptionsInternal>(ref copyEntitlementByNameAndIndexOptionsInternal);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero, out outEntitlement))
			{
				EcomInterface.EOS_Ecom_Entitlement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x00093F3C File Offset: 0x0009213C
		public Result CopyEntitlementById(CopyEntitlementByIdOptions options, out Entitlement outEntitlement)
		{
			CopyEntitlementByIdOptionsInternal copyEntitlementByIdOptionsInternal = Helper.CopyProperties<CopyEntitlementByIdOptionsInternal>(options);
			outEntitlement = Helper.GetDefault<Entitlement>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyEntitlementById(base.InnerHandle, ref copyEntitlementByIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyEntitlementByIdOptionsInternal>(ref copyEntitlementByIdOptionsInternal);
			if (Helper.TryMarshalGet<EntitlementInternal, Entitlement>(zero, out outEntitlement))
			{
				EcomInterface.EOS_Ecom_Entitlement_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x00093F94 File Offset: 0x00092194
		public uint GetOfferCount(GetOfferCountOptions options)
		{
			GetOfferCountOptionsInternal getOfferCountOptionsInternal = Helper.CopyProperties<GetOfferCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetOfferCount(base.InnerHandle, ref getOfferCountOptionsInternal);
			Helper.TryMarshalDispose<GetOfferCountOptionsInternal>(ref getOfferCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x00093FCC File Offset: 0x000921CC
		public Result CopyOfferByIndex(CopyOfferByIndexOptions options, out CatalogOffer outOffer)
		{
			CopyOfferByIndexOptionsInternal copyOfferByIndexOptionsInternal = Helper.CopyProperties<CopyOfferByIndexOptionsInternal>(options);
			outOffer = Helper.GetDefault<CatalogOffer>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyOfferByIndex(base.InnerHandle, ref copyOfferByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyOfferByIndexOptionsInternal>(ref copyOfferByIndexOptionsInternal);
			if (Helper.TryMarshalGet<CatalogOfferInternal, CatalogOffer>(zero, out outOffer))
			{
				EcomInterface.EOS_Ecom_CatalogOffer_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BDB RID: 19419 RVA: 0x00094024 File Offset: 0x00092224
		public Result CopyOfferById(CopyOfferByIdOptions options, out CatalogOffer outOffer)
		{
			CopyOfferByIdOptionsInternal copyOfferByIdOptionsInternal = Helper.CopyProperties<CopyOfferByIdOptionsInternal>(options);
			outOffer = Helper.GetDefault<CatalogOffer>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyOfferById(base.InnerHandle, ref copyOfferByIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyOfferByIdOptionsInternal>(ref copyOfferByIdOptionsInternal);
			if (Helper.TryMarshalGet<CatalogOfferInternal, CatalogOffer>(zero, out outOffer))
			{
				EcomInterface.EOS_Ecom_CatalogOffer_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BDC RID: 19420 RVA: 0x0009407C File Offset: 0x0009227C
		public uint GetOfferItemCount(GetOfferItemCountOptions options)
		{
			GetOfferItemCountOptionsInternal getOfferItemCountOptionsInternal = Helper.CopyProperties<GetOfferItemCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetOfferItemCount(base.InnerHandle, ref getOfferItemCountOptionsInternal);
			Helper.TryMarshalDispose<GetOfferItemCountOptionsInternal>(ref getOfferItemCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BDD RID: 19421 RVA: 0x000940B4 File Offset: 0x000922B4
		public Result CopyOfferItemByIndex(CopyOfferItemByIndexOptions options, out CatalogItem outItem)
		{
			CopyOfferItemByIndexOptionsInternal copyOfferItemByIndexOptionsInternal = Helper.CopyProperties<CopyOfferItemByIndexOptionsInternal>(options);
			outItem = Helper.GetDefault<CatalogItem>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyOfferItemByIndex(base.InnerHandle, ref copyOfferItemByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyOfferItemByIndexOptionsInternal>(ref copyOfferItemByIndexOptionsInternal);
			if (Helper.TryMarshalGet<CatalogItemInternal, CatalogItem>(zero, out outItem))
			{
				EcomInterface.EOS_Ecom_CatalogItem_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BDE RID: 19422 RVA: 0x0009410C File Offset: 0x0009230C
		public Result CopyItemById(CopyItemByIdOptions options, out CatalogItem outItem)
		{
			CopyItemByIdOptionsInternal copyItemByIdOptionsInternal = Helper.CopyProperties<CopyItemByIdOptionsInternal>(options);
			outItem = Helper.GetDefault<CatalogItem>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyItemById(base.InnerHandle, ref copyItemByIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyItemByIdOptionsInternal>(ref copyItemByIdOptionsInternal);
			if (Helper.TryMarshalGet<CatalogItemInternal, CatalogItem>(zero, out outItem))
			{
				EcomInterface.EOS_Ecom_CatalogItem_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BDF RID: 19423 RVA: 0x00094164 File Offset: 0x00092364
		public uint GetOfferImageInfoCount(GetOfferImageInfoCountOptions options)
		{
			GetOfferImageInfoCountOptionsInternal getOfferImageInfoCountOptionsInternal = Helper.CopyProperties<GetOfferImageInfoCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetOfferImageInfoCount(base.InnerHandle, ref getOfferImageInfoCountOptionsInternal);
			Helper.TryMarshalDispose<GetOfferImageInfoCountOptionsInternal>(ref getOfferImageInfoCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BE0 RID: 19424 RVA: 0x0009419C File Offset: 0x0009239C
		public Result CopyOfferImageInfoByIndex(CopyOfferImageInfoByIndexOptions options, out KeyImageInfo outImageInfo)
		{
			CopyOfferImageInfoByIndexOptionsInternal copyOfferImageInfoByIndexOptionsInternal = Helper.CopyProperties<CopyOfferImageInfoByIndexOptionsInternal>(options);
			outImageInfo = Helper.GetDefault<KeyImageInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyOfferImageInfoByIndex(base.InnerHandle, ref copyOfferImageInfoByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyOfferImageInfoByIndexOptionsInternal>(ref copyOfferImageInfoByIndexOptionsInternal);
			if (Helper.TryMarshalGet<KeyImageInfoInternal, KeyImageInfo>(zero, out outImageInfo))
			{
				EcomInterface.EOS_Ecom_KeyImageInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BE1 RID: 19425 RVA: 0x000941F4 File Offset: 0x000923F4
		public uint GetItemImageInfoCount(GetItemImageInfoCountOptions options)
		{
			GetItemImageInfoCountOptionsInternal getItemImageInfoCountOptionsInternal = Helper.CopyProperties<GetItemImageInfoCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetItemImageInfoCount(base.InnerHandle, ref getItemImageInfoCountOptionsInternal);
			Helper.TryMarshalDispose<GetItemImageInfoCountOptionsInternal>(ref getItemImageInfoCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BE2 RID: 19426 RVA: 0x0009422C File Offset: 0x0009242C
		public Result CopyItemImageInfoByIndex(CopyItemImageInfoByIndexOptions options, out KeyImageInfo outImageInfo)
		{
			CopyItemImageInfoByIndexOptionsInternal copyItemImageInfoByIndexOptionsInternal = Helper.CopyProperties<CopyItemImageInfoByIndexOptionsInternal>(options);
			outImageInfo = Helper.GetDefault<KeyImageInfo>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyItemImageInfoByIndex(base.InnerHandle, ref copyItemImageInfoByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyItemImageInfoByIndexOptionsInternal>(ref copyItemImageInfoByIndexOptionsInternal);
			if (Helper.TryMarshalGet<KeyImageInfoInternal, KeyImageInfo>(zero, out outImageInfo))
			{
				EcomInterface.EOS_Ecom_KeyImageInfo_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BE3 RID: 19427 RVA: 0x00094284 File Offset: 0x00092484
		public uint GetItemReleaseCount(GetItemReleaseCountOptions options)
		{
			GetItemReleaseCountOptionsInternal getItemReleaseCountOptionsInternal = Helper.CopyProperties<GetItemReleaseCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetItemReleaseCount(base.InnerHandle, ref getItemReleaseCountOptionsInternal);
			Helper.TryMarshalDispose<GetItemReleaseCountOptionsInternal>(ref getItemReleaseCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BE4 RID: 19428 RVA: 0x000942BC File Offset: 0x000924BC
		public Result CopyItemReleaseByIndex(CopyItemReleaseByIndexOptions options, out CatalogRelease outRelease)
		{
			CopyItemReleaseByIndexOptionsInternal copyItemReleaseByIndexOptionsInternal = Helper.CopyProperties<CopyItemReleaseByIndexOptionsInternal>(options);
			outRelease = Helper.GetDefault<CatalogRelease>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyItemReleaseByIndex(base.InnerHandle, ref copyItemReleaseByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyItemReleaseByIndexOptionsInternal>(ref copyItemReleaseByIndexOptionsInternal);
			if (Helper.TryMarshalGet<CatalogReleaseInternal, CatalogRelease>(zero, out outRelease))
			{
				EcomInterface.EOS_Ecom_CatalogRelease_Release(zero);
			}
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BE5 RID: 19429 RVA: 0x00094314 File Offset: 0x00092514
		public uint GetTransactionCount(GetTransactionCountOptions options)
		{
			GetTransactionCountOptionsInternal getTransactionCountOptionsInternal = Helper.CopyProperties<GetTransactionCountOptionsInternal>(options);
			uint num = EcomInterface.EOS_Ecom_GetTransactionCount(base.InnerHandle, ref getTransactionCountOptionsInternal);
			Helper.TryMarshalDispose<GetTransactionCountOptionsInternal>(ref getTransactionCountOptionsInternal);
			uint @default = Helper.GetDefault<uint>();
			Helper.TryMarshalGet<uint>(num, out @default);
			return @default;
		}

		// Token: 0x06004BE6 RID: 19430 RVA: 0x0009434C File Offset: 0x0009254C
		public Result CopyTransactionByIndex(CopyTransactionByIndexOptions options, out Transaction outTransaction)
		{
			CopyTransactionByIndexOptionsInternal copyTransactionByIndexOptionsInternal = Helper.CopyProperties<CopyTransactionByIndexOptionsInternal>(options);
			outTransaction = Helper.GetDefault<Transaction>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyTransactionByIndex(base.InnerHandle, ref copyTransactionByIndexOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyTransactionByIndexOptionsInternal>(ref copyTransactionByIndexOptionsInternal);
			Helper.TryMarshalGet<Transaction>(zero, out outTransaction);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BE7 RID: 19431 RVA: 0x0009439C File Offset: 0x0009259C
		public Result CopyTransactionById(CopyTransactionByIdOptions options, out Transaction outTransaction)
		{
			CopyTransactionByIdOptionsInternal copyTransactionByIdOptionsInternal = Helper.CopyProperties<CopyTransactionByIdOptionsInternal>(options);
			outTransaction = Helper.GetDefault<Transaction>();
			IntPtr zero = IntPtr.Zero;
			Result result = EcomInterface.EOS_Ecom_CopyTransactionById(base.InnerHandle, ref copyTransactionByIdOptionsInternal, ref zero);
			Helper.TryMarshalDispose<CopyTransactionByIdOptionsInternal>(ref copyTransactionByIdOptionsInternal);
			Helper.TryMarshalGet<Transaction>(zero, out outTransaction);
			Result @default = Helper.GetDefault<Result>();
			Helper.TryMarshalGet<Result>(result, out @default);
			return @default;
		}

		// Token: 0x06004BE8 RID: 19432 RVA: 0x000943EC File Offset: 0x000925EC
		[MonoPInvokeCallback]
		internal static void OnRedeemEntitlements(IntPtr address)
		{
			OnRedeemEntitlementsCallback onRedeemEntitlementsCallback = null;
			RedeemEntitlementsCallbackInfo redeemEntitlementsCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnRedeemEntitlementsCallback, RedeemEntitlementsCallbackInfoInternal, RedeemEntitlementsCallbackInfo>(address, out onRedeemEntitlementsCallback, out redeemEntitlementsCallbackInfo))
			{
				onRedeemEntitlementsCallback(redeemEntitlementsCallbackInfo);
			}
		}

		// Token: 0x06004BE9 RID: 19433 RVA: 0x00094410 File Offset: 0x00092610
		[MonoPInvokeCallback]
		internal static void OnCheckout(IntPtr address)
		{
			OnCheckoutCallback onCheckoutCallback = null;
			CheckoutCallbackInfo checkoutCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnCheckoutCallback, CheckoutCallbackInfoInternal, CheckoutCallbackInfo>(address, out onCheckoutCallback, out checkoutCallbackInfo))
			{
				onCheckoutCallback(checkoutCallbackInfo);
			}
		}

		// Token: 0x06004BEA RID: 19434 RVA: 0x00094434 File Offset: 0x00092634
		[MonoPInvokeCallback]
		internal static void OnQueryOffers(IntPtr address)
		{
			OnQueryOffersCallback onQueryOffersCallback = null;
			QueryOffersCallbackInfo queryOffersCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryOffersCallback, QueryOffersCallbackInfoInternal, QueryOffersCallbackInfo>(address, out onQueryOffersCallback, out queryOffersCallbackInfo))
			{
				onQueryOffersCallback(queryOffersCallbackInfo);
			}
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x00094458 File Offset: 0x00092658
		[MonoPInvokeCallback]
		internal static void OnQueryEntitlements(IntPtr address)
		{
			OnQueryEntitlementsCallback onQueryEntitlementsCallback = null;
			QueryEntitlementsCallbackInfo queryEntitlementsCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryEntitlementsCallback, QueryEntitlementsCallbackInfoInternal, QueryEntitlementsCallbackInfo>(address, out onQueryEntitlementsCallback, out queryEntitlementsCallbackInfo))
			{
				onQueryEntitlementsCallback(queryEntitlementsCallbackInfo);
			}
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0009447C File Offset: 0x0009267C
		[MonoPInvokeCallback]
		internal static void OnQueryOwnershipToken(IntPtr address)
		{
			OnQueryOwnershipTokenCallback onQueryOwnershipTokenCallback = null;
			QueryOwnershipTokenCallbackInfo queryOwnershipTokenCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryOwnershipTokenCallback, QueryOwnershipTokenCallbackInfoInternal, QueryOwnershipTokenCallbackInfo>(address, out onQueryOwnershipTokenCallback, out queryOwnershipTokenCallbackInfo))
			{
				onQueryOwnershipTokenCallback(queryOwnershipTokenCallbackInfo);
			}
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x000944A0 File Offset: 0x000926A0
		[MonoPInvokeCallback]
		internal static void OnQueryOwnership(IntPtr address)
		{
			OnQueryOwnershipCallback onQueryOwnershipCallback = null;
			QueryOwnershipCallbackInfo queryOwnershipCallbackInfo = null;
			if (Helper.TryGetAndRemoveCallback<OnQueryOwnershipCallback, QueryOwnershipCallbackInfoInternal, QueryOwnershipCallbackInfo>(address, out onQueryOwnershipCallback, out queryOwnershipCallbackInfo))
			{
				onQueryOwnershipCallback(queryOwnershipCallbackInfo);
			}
		}

		// Token: 0x06004BEE RID: 19438
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_CatalogRelease_Release(IntPtr catalogRelease);

		// Token: 0x06004BEF RID: 19439
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_KeyImageInfo_Release(IntPtr keyImageInfo);

		// Token: 0x06004BF0 RID: 19440
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_CatalogOffer_Release(IntPtr catalogOffer);

		// Token: 0x06004BF1 RID: 19441
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_CatalogItem_Release(IntPtr catalogItem);

		// Token: 0x06004BF2 RID: 19442
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_Entitlement_Release(IntPtr entitlement);

		// Token: 0x06004BF3 RID: 19443
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyTransactionById(IntPtr handle, ref CopyTransactionByIdOptionsInternal options, ref IntPtr outTransaction);

		// Token: 0x06004BF4 RID: 19444
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyTransactionByIndex(IntPtr handle, ref CopyTransactionByIndexOptionsInternal options, ref IntPtr outTransaction);

		// Token: 0x06004BF5 RID: 19445
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetTransactionCount(IntPtr handle, ref GetTransactionCountOptionsInternal options);

		// Token: 0x06004BF6 RID: 19446
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyItemReleaseByIndex(IntPtr handle, ref CopyItemReleaseByIndexOptionsInternal options, ref IntPtr outRelease);

		// Token: 0x06004BF7 RID: 19447
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetItemReleaseCount(IntPtr handle, ref GetItemReleaseCountOptionsInternal options);

		// Token: 0x06004BF8 RID: 19448
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyItemImageInfoByIndex(IntPtr handle, ref CopyItemImageInfoByIndexOptionsInternal options, ref IntPtr outImageInfo);

		// Token: 0x06004BF9 RID: 19449
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetItemImageInfoCount(IntPtr handle, ref GetItemImageInfoCountOptionsInternal options);

		// Token: 0x06004BFA RID: 19450
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyOfferImageInfoByIndex(IntPtr handle, ref CopyOfferImageInfoByIndexOptionsInternal options, ref IntPtr outImageInfo);

		// Token: 0x06004BFB RID: 19451
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetOfferImageInfoCount(IntPtr handle, ref GetOfferImageInfoCountOptionsInternal options);

		// Token: 0x06004BFC RID: 19452
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyItemById(IntPtr handle, ref CopyItemByIdOptionsInternal options, ref IntPtr outItem);

		// Token: 0x06004BFD RID: 19453
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyOfferItemByIndex(IntPtr handle, ref CopyOfferItemByIndexOptionsInternal options, ref IntPtr outItem);

		// Token: 0x06004BFE RID: 19454
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetOfferItemCount(IntPtr handle, ref GetOfferItemCountOptionsInternal options);

		// Token: 0x06004BFF RID: 19455
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyOfferById(IntPtr handle, ref CopyOfferByIdOptionsInternal options, ref IntPtr outOffer);

		// Token: 0x06004C00 RID: 19456
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyOfferByIndex(IntPtr handle, ref CopyOfferByIndexOptionsInternal options, ref IntPtr outOffer);

		// Token: 0x06004C01 RID: 19457
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetOfferCount(IntPtr handle, ref GetOfferCountOptionsInternal options);

		// Token: 0x06004C02 RID: 19458
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyEntitlementById(IntPtr handle, ref CopyEntitlementByIdOptionsInternal options, ref IntPtr outEntitlement);

		// Token: 0x06004C03 RID: 19459
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyEntitlementByNameAndIndex(IntPtr handle, ref CopyEntitlementByNameAndIndexOptionsInternal options, ref IntPtr outEntitlement);

		// Token: 0x06004C04 RID: 19460
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern Result EOS_Ecom_CopyEntitlementByIndex(IntPtr handle, ref CopyEntitlementByIndexOptionsInternal options, ref IntPtr outEntitlement);

		// Token: 0x06004C05 RID: 19461
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetEntitlementsByNameCount(IntPtr handle, ref GetEntitlementsByNameCountOptionsInternal options);

		// Token: 0x06004C06 RID: 19462
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern uint EOS_Ecom_GetEntitlementsCount(IntPtr handle, ref GetEntitlementsCountOptionsInternal options);

		// Token: 0x06004C07 RID: 19463
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_RedeemEntitlements(IntPtr handle, ref RedeemEntitlementsOptionsInternal options, IntPtr clientData, OnRedeemEntitlementsCallbackInternal completionDelegate);

		// Token: 0x06004C08 RID: 19464
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_Checkout(IntPtr handle, ref CheckoutOptionsInternal options, IntPtr clientData, OnCheckoutCallbackInternal completionDelegate);

		// Token: 0x06004C09 RID: 19465
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_QueryOffers(IntPtr handle, ref QueryOffersOptionsInternal options, IntPtr clientData, OnQueryOffersCallbackInternal completionDelegate);

		// Token: 0x06004C0A RID: 19466
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_QueryEntitlements(IntPtr handle, ref QueryEntitlementsOptionsInternal options, IntPtr clientData, OnQueryEntitlementsCallbackInternal completionDelegate);

		// Token: 0x06004C0B RID: 19467
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_QueryOwnershipToken(IntPtr handle, ref QueryOwnershipTokenOptionsInternal options, IntPtr clientData, OnQueryOwnershipTokenCallbackInternal completionDelegate);

		// Token: 0x06004C0C RID: 19468
		[DllImport("EOSSDK-Win64-Shipping")]
		private static extern void EOS_Ecom_QueryOwnership(IntPtr handle, ref QueryOwnershipOptionsInternal options, IntPtr clientData, OnQueryOwnershipCallbackInternal completionDelegate);

		// Token: 0x04001D50 RID: 7504
		public const int TransactionCopyentitlementbyindexApiLatest = 1;

		// Token: 0x04001D51 RID: 7505
		public const int TransactionGetentitlementscountApiLatest = 1;

		// Token: 0x04001D52 RID: 7506
		public const int CopytransactionbyidApiLatest = 1;

		// Token: 0x04001D53 RID: 7507
		public const int CopytransactionbyindexApiLatest = 1;

		// Token: 0x04001D54 RID: 7508
		public const int GettransactioncountApiLatest = 1;

		// Token: 0x04001D55 RID: 7509
		public const int CopyitemreleasebyindexApiLatest = 1;

		// Token: 0x04001D56 RID: 7510
		public const int GetitemreleasecountApiLatest = 1;

		// Token: 0x04001D57 RID: 7511
		public const int CopyitemimageinfobyindexApiLatest = 1;

		// Token: 0x04001D58 RID: 7512
		public const int GetitemimageinfocountApiLatest = 1;

		// Token: 0x04001D59 RID: 7513
		public const int CopyofferimageinfobyindexApiLatest = 1;

		// Token: 0x04001D5A RID: 7514
		public const int GetofferimageinfocountApiLatest = 1;

		// Token: 0x04001D5B RID: 7515
		public const int CopyitembyidApiLatest = 1;

		// Token: 0x04001D5C RID: 7516
		public const int CopyofferitembyindexApiLatest = 1;

		// Token: 0x04001D5D RID: 7517
		public const int GetofferitemcountApiLatest = 1;

		// Token: 0x04001D5E RID: 7518
		public const int CopyofferbyidApiLatest = 1;

		// Token: 0x04001D5F RID: 7519
		public const int CopyofferbyindexApiLatest = 1;

		// Token: 0x04001D60 RID: 7520
		public const int GetoffercountApiLatest = 1;

		// Token: 0x04001D61 RID: 7521
		public const int CopyentitlementbyidApiLatest = 2;

		// Token: 0x04001D62 RID: 7522
		public const int CopyentitlementbynameandindexApiLatest = 1;

		// Token: 0x04001D63 RID: 7523
		public const int CopyentitlementbyindexApiLatest = 1;

		// Token: 0x04001D64 RID: 7524
		public const int GetentitlementsbynamecountApiLatest = 1;

		// Token: 0x04001D65 RID: 7525
		public const int GetentitlementscountApiLatest = 1;

		// Token: 0x04001D66 RID: 7526
		public const int RedeementitlementsMaxIds = 32;

		// Token: 0x04001D67 RID: 7527
		public const int RedeementitlementsApiLatest = 1;

		// Token: 0x04001D68 RID: 7528
		public const int TransactionidMaximumLength = 64;

		// Token: 0x04001D69 RID: 7529
		public const int CheckoutMaxEntries = 10;

		// Token: 0x04001D6A RID: 7530
		public const int CheckoutApiLatest = 1;

		// Token: 0x04001D6B RID: 7531
		public const int QueryoffersApiLatest = 1;

		// Token: 0x04001D6C RID: 7532
		public const int QueryentitlementsMaxEntitlementIds = 32;

		// Token: 0x04001D6D RID: 7533
		public const int QueryentitlementsApiLatest = 2;

		// Token: 0x04001D6E RID: 7534
		public const int QueryownershiptokenMaxCatalogitemIds = 32;

		// Token: 0x04001D6F RID: 7535
		public const int QueryownershiptokenApiLatest = 2;

		// Token: 0x04001D70 RID: 7536
		public const int QueryownershipMaxCatalogIds = 32;

		// Token: 0x04001D71 RID: 7537
		public const int QueryownershipApiLatest = 2;

		// Token: 0x04001D72 RID: 7538
		public const int CheckoutentryApiLatest = 1;

		// Token: 0x04001D73 RID: 7539
		public const int CatalogreleaseApiLatest = 1;

		// Token: 0x04001D74 RID: 7540
		public const int KeyimageinfoApiLatest = 1;

		// Token: 0x04001D75 RID: 7541
		public const int CatalogofferExpirationtimestampUndefined = -1;

		// Token: 0x04001D76 RID: 7542
		public const int CatalogofferApiLatest = 2;

		// Token: 0x04001D77 RID: 7543
		public const int CatalogitemEntitlementendtimestampUndefined = -1;

		// Token: 0x04001D78 RID: 7544
		public const int CatalogitemApiLatest = 1;

		// Token: 0x04001D79 RID: 7545
		public const int ItemownershipApiLatest = 1;

		// Token: 0x04001D7A RID: 7546
		public const int EntitlementEndtimestampUndefined = -1;

		// Token: 0x04001D7B RID: 7547
		public const int EntitlementApiLatest = 2;
	}
}
