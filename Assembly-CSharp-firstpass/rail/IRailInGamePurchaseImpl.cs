using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002B0 RID: 688
	public class IRailInGamePurchaseImpl : RailObject, IRailInGamePurchase
	{
		// Token: 0x0600292C RID: 10540 RVA: 0x00052250 File Offset: 0x00050450
		internal IRailInGamePurchaseImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x00052260 File Offset: 0x00050460
		~IRailInGamePurchaseImpl()
		{
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x00052288 File Offset: 0x00050488
		public virtual RailResult AsyncRequestAllPurchasableProducts(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncRequestAllPurchasableProducts(this.swigCPtr_, user_data);
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00052296 File Offset: 0x00050496
		public virtual RailResult AsyncRequestAllProducts(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncRequestAllProducts(this.swigCPtr_, user_data);
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x000522A4 File Offset: 0x000504A4
		public virtual RailResult GetProductInfo(uint product_id, RailPurchaseProductInfo product)
		{
			IntPtr intPtr = ((product == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailPurchaseProductInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_GetProductInfo(this.swigCPtr_, product_id, intPtr);
			}
			finally
			{
				if (product != null)
				{
					RailConverter.Cpp2Csharp(intPtr, product);
				}
				RAIL_API_PINVOKE.delete_RailPurchaseProductInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x000522F4 File Offset: 0x000504F4
		public virtual RailResult AsyncPurchaseProducts(List<RailProductItem> cart_items, string user_data)
		{
			IntPtr intPtr = ((cart_items == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailProductItem__SWIG_0());
			if (cart_items != null)
			{
				RailConverter.Csharp2Cpp(cart_items, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncPurchaseProducts(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailProductItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x00052344 File Offset: 0x00050544
		public virtual RailResult AsyncFinishOrder(string order_id, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncFinishOrder(this.swigCPtr_, order_id, user_data);
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x00052354 File Offset: 0x00050554
		public virtual RailResult AsyncPurchaseProductsToAssets(List<RailProductItem> cart_items, string user_data)
		{
			IntPtr intPtr = ((cart_items == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailProductItem__SWIG_0());
			if (cart_items != null)
			{
				RailConverter.Csharp2Cpp(cart_items, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailInGamePurchase_AsyncPurchaseProductsToAssets(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailProductItem(intPtr);
			}
			return railResult;
		}
	}
}
