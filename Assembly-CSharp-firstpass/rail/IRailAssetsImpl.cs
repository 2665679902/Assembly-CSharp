using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000299 RID: 665
	public class IRailAssetsImpl : RailObject, IRailAssets, IRailComponent
	{
		// Token: 0x060027F8 RID: 10232 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		internal IRailAssetsImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x0004F1DC File Offset: 0x0004D3DC
		~IRailAssetsImpl()
		{
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x0004F204 File Offset: 0x0004D404
		public virtual RailResult AsyncRequestAllAssets(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncRequestAllAssets(this.swigCPtr_, user_data);
		}

		// Token: 0x060027FB RID: 10235 RVA: 0x0004F214 File Offset: 0x0004D414
		public virtual RailResult QueryAssetInfo(ulong asset_id, RailAssetInfo asset_info)
		{
			IntPtr intPtr = ((asset_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailAssetInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_QueryAssetInfo(this.swigCPtr_, asset_id, intPtr);
			}
			finally
			{
				if (asset_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, asset_info);
				}
				RAIL_API_PINVOKE.delete_RailAssetInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060027FC RID: 10236 RVA: 0x0004F264 File Offset: 0x0004D464
		public virtual RailResult AsyncUpdateAssetsProperty(List<RailAssetProperty> asset_property_list, string user_data)
		{
			IntPtr intPtr = ((asset_property_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetProperty__SWIG_0());
			if (asset_property_list != null)
			{
				RailConverter.Csharp2Cpp(asset_property_list, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncUpdateAssetsProperty(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetProperty(intPtr);
			}
			return railResult;
		}

		// Token: 0x060027FD RID: 10237 RVA: 0x0004F2B4 File Offset: 0x0004D4B4
		public virtual RailResult AsyncDirectConsumeAssets(List<RailAssetItem> assets, string user_data)
		{
			IntPtr intPtr = ((assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (assets != null)
			{
				RailConverter.Csharp2Cpp(assets, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncDirectConsumeAssets(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x060027FE RID: 10238 RVA: 0x0004F304 File Offset: 0x0004D504
		public virtual RailResult AsyncStartConsumeAsset(ulong asset_id, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncStartConsumeAsset(this.swigCPtr_, asset_id, user_data);
		}

		// Token: 0x060027FF RID: 10239 RVA: 0x0004F313 File Offset: 0x0004D513
		public virtual RailResult AsyncUpdateConsumeProgress(ulong asset_id, string progress, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncUpdateConsumeProgress(this.swigCPtr_, asset_id, progress, user_data);
		}

		// Token: 0x06002800 RID: 10240 RVA: 0x0004F323 File Offset: 0x0004D523
		public virtual RailResult AsyncCompleteConsumeAsset(ulong asset_id, uint quantity, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncCompleteConsumeAsset(this.swigCPtr_, asset_id, quantity, user_data);
		}

		// Token: 0x06002801 RID: 10241 RVA: 0x0004F334 File Offset: 0x0004D534
		public virtual RailResult AsyncExchangeAssets(List<RailAssetItem> old_assets, RailProductItem to_product_info, string user_data)
		{
			IntPtr intPtr = ((old_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (old_assets != null)
			{
				RailConverter.Csharp2Cpp(old_assets, intPtr);
			}
			IntPtr intPtr2 = ((to_product_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailProductItem__SWIG_0());
			if (to_product_info != null)
			{
				RailConverter.Csharp2Cpp(to_product_info, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncExchangeAssets(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
				RAIL_API_PINVOKE.delete_RailProductItem(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002802 RID: 10242 RVA: 0x0004F3A8 File Offset: 0x0004D5A8
		public virtual RailResult AsyncExchangeAssetsTo(List<RailAssetItem> old_assets, RailProductItem to_product_info, ulong add_to_exist_assets, string user_data)
		{
			IntPtr intPtr = ((old_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (old_assets != null)
			{
				RailConverter.Csharp2Cpp(old_assets, intPtr);
			}
			IntPtr intPtr2 = ((to_product_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailProductItem__SWIG_0());
			if (to_product_info != null)
			{
				RailConverter.Csharp2Cpp(to_product_info, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncExchangeAssetsTo(this.swigCPtr_, intPtr, intPtr2, add_to_exist_assets, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
				RAIL_API_PINVOKE.delete_RailProductItem(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002803 RID: 10243 RVA: 0x0004F41C File Offset: 0x0004D61C
		public virtual RailResult AsyncSplitAsset(ulong source_asset, uint to_quantity, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncSplitAsset(this.swigCPtr_, source_asset, to_quantity, user_data);
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x0004F42C File Offset: 0x0004D62C
		public virtual RailResult AsyncSplitAssetTo(ulong source_asset, uint to_quantity, ulong add_to_asset, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncSplitAssetTo(this.swigCPtr_, source_asset, to_quantity, add_to_asset, user_data);
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x0004F440 File Offset: 0x0004D640
		public virtual RailResult AsyncMergeAsset(List<RailAssetItem> source_assets, string user_data)
		{
			IntPtr intPtr = ((source_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (source_assets != null)
			{
				RailConverter.Csharp2Cpp(source_assets, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncMergeAsset(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x0004F490 File Offset: 0x0004D690
		public virtual RailResult AsyncMergeAssetTo(List<RailAssetItem> source_assets, ulong add_to_asset, string user_data)
		{
			IntPtr intPtr = ((source_assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetItem__SWIG_0());
			if (source_assets != null)
			{
				RailConverter.Csharp2Cpp(source_assets, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_AsyncMergeAssetTo(this.swigCPtr_, intPtr, add_to_asset, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailAssetItem(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002807 RID: 10247 RVA: 0x0004F4E4 File Offset: 0x0004D6E4
		public virtual RailResult SerializeAssetsToBuffer(out string buffer)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_SerializeAssetsToBuffer__SWIG_0(this.swigCPtr_, intPtr);
			}
			finally
			{
				buffer = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002808 RID: 10248 RVA: 0x0004F52C File Offset: 0x0004D72C
		public virtual RailResult SerializeAssetsToBuffer(List<ulong> assets, out string buffer)
		{
			IntPtr intPtr = ((assets == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayuint64_t__SWIG_0());
			if (assets != null)
			{
				RailConverter.Csharp2Cpp(assets, intPtr);
			}
			IntPtr intPtr2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_SerializeAssetsToBuffer__SWIG_1(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayuint64_t(intPtr);
				buffer = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr2));
				RAIL_API_PINVOKE.delete_RailString(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002809 RID: 10249 RVA: 0x0004F594 File Offset: 0x0004D794
		public virtual RailResult DeserializeAssetsFromBuffer(RailID assets_owner, string buffer, List<RailAssetInfo> assets_info)
		{
			IntPtr intPtr = ((assets_owner == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (assets_owner != null)
			{
				RailConverter.Csharp2Cpp(assets_owner, intPtr);
			}
			IntPtr intPtr2 = ((assets_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailAssetInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailAssets_DeserializeAssetsFromBuffer(this.swigCPtr_, intPtr, buffer, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (assets_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, assets_info);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailAssetInfo(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600280A RID: 10250 RVA: 0x0004F610 File Offset: 0x0004D810
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x0600280B RID: 10251 RVA: 0x0004F61D File Offset: 0x0004D81D
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
