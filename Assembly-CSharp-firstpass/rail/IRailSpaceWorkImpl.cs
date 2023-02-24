using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002BE RID: 702
	public class IRailSpaceWorkImpl : RailObject, IRailSpaceWork, IRailComponent
	{
		// Token: 0x060029DB RID: 10715 RVA: 0x000542AA File Offset: 0x000524AA
		internal IRailSpaceWorkImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x000542BC File Offset: 0x000524BC
		~IRailSpaceWorkImpl()
		{
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x000542E4 File Offset: 0x000524E4
		public virtual void Close()
		{
			RAIL_API_PINVOKE.IRailSpaceWork_Close(this.swigCPtr_);
		}

		// Token: 0x060029DE RID: 10718 RVA: 0x000542F4 File Offset: 0x000524F4
		public virtual SpaceWorkID GetSpaceWorkID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailSpaceWork_GetSpaceWorkID(this.swigCPtr_);
			SpaceWorkID spaceWorkID = new SpaceWorkID();
			RailConverter.Cpp2Csharp(intPtr, spaceWorkID);
			return spaceWorkID;
		}

		// Token: 0x060029DF RID: 10719 RVA: 0x00054319 File Offset: 0x00052519
		public virtual bool Editable()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_Editable(this.swigCPtr_);
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x00054326 File Offset: 0x00052526
		public virtual RailResult StartSync(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_StartSync(this.swigCPtr_, user_data);
		}

		// Token: 0x060029E1 RID: 10721 RVA: 0x00054334 File Offset: 0x00052534
		public virtual RailResult GetSyncProgress(RailSpaceWorkSyncProgress progress)
		{
			IntPtr intPtr = ((progress == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkSyncProgress__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetSyncProgress(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (progress != null)
				{
					RailConverter.Cpp2Csharp(intPtr, progress);
				}
				RAIL_API_PINVOKE.delete_RailSpaceWorkSyncProgress(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029E2 RID: 10722 RVA: 0x00054384 File Offset: 0x00052584
		public virtual RailResult CancelSync()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_CancelSync(this.swigCPtr_);
		}

		// Token: 0x060029E3 RID: 10723 RVA: 0x00054394 File Offset: 0x00052594
		public virtual RailResult GetWorkLocalFolder(out string path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetWorkLocalFolder(this.swigCPtr_, intPtr);
			}
			finally
			{
				path = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029E4 RID: 10724 RVA: 0x000543DC File Offset: 0x000525DC
		public virtual RailResult AsyncUpdateMetadata(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_AsyncUpdateMetadata(this.swigCPtr_, user_data);
		}

		// Token: 0x060029E5 RID: 10725 RVA: 0x000543EC File Offset: 0x000525EC
		public virtual RailResult GetName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029E6 RID: 10726 RVA: 0x00054434 File Offset: 0x00052634
		public virtual RailResult GetDescription(out string description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetDescription(this.swigCPtr_, intPtr);
			}
			finally
			{
				description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029E7 RID: 10727 RVA: 0x0005447C File Offset: 0x0005267C
		public virtual RailResult GetUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetUrl(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029E8 RID: 10728 RVA: 0x000544C4 File Offset: 0x000526C4
		public virtual uint GetCreateTime()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetCreateTime(this.swigCPtr_);
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000544D1 File Offset: 0x000526D1
		public virtual uint GetLastUpdateTime()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetLastUpdateTime(this.swigCPtr_);
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000544DE File Offset: 0x000526DE
		public virtual ulong GetWorkFileSize()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetWorkFileSize(this.swigCPtr_);
		}

		// Token: 0x060029EB RID: 10731 RVA: 0x000544EC File Offset: 0x000526EC
		public virtual RailResult GetTags(List<string> tags)
		{
			IntPtr intPtr = ((tags == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetTags(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (tags != null)
				{
					RailConverter.Cpp2Csharp(intPtr, tags);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029EC RID: 10732 RVA: 0x0005453C File Offset: 0x0005273C
		public virtual RailResult GetPreviewImage(out string path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetPreviewImage(this.swigCPtr_, intPtr);
			}
			finally
			{
				path = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029ED RID: 10733 RVA: 0x00054584 File Offset: 0x00052784
		public virtual RailResult GetVersion(out string version)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetVersion(this.swigCPtr_, intPtr);
			}
			finally
			{
				version = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029EE RID: 10734 RVA: 0x000545CC File Offset: 0x000527CC
		public virtual ulong GetDownloadCount()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetDownloadCount(this.swigCPtr_);
		}

		// Token: 0x060029EF RID: 10735 RVA: 0x000545D9 File Offset: 0x000527D9
		public virtual ulong GetSubscribedCount()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetSubscribedCount(this.swigCPtr_);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x000545E6 File Offset: 0x000527E6
		public virtual EnumRailSpaceWorkShareLevel GetShareLevel()
		{
			return (EnumRailSpaceWorkShareLevel)RAIL_API_PINVOKE.IRailSpaceWork_GetShareLevel(this.swigCPtr_);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x000545F3 File Offset: 0x000527F3
		public virtual ulong GetScore()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetScore(this.swigCPtr_);
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x00054600 File Offset: 0x00052800
		public virtual RailResult GetMetadata(string key, out string value)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetMetadata(this.swigCPtr_, key, intPtr);
			}
			finally
			{
				value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x00054648 File Offset: 0x00052848
		public virtual EnumRailSpaceWorkRateValue GetMyVote()
		{
			return (EnumRailSpaceWorkRateValue)RAIL_API_PINVOKE.IRailSpaceWork_GetMyVote(this.swigCPtr_);
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x00054655 File Offset: 0x00052855
		public virtual bool IsFavorite()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_IsFavorite(this.swigCPtr_);
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x00054662 File Offset: 0x00052862
		public virtual bool IsSubscribed()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_IsSubscribed(this.swigCPtr_);
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0005466F File Offset: 0x0005286F
		public virtual RailResult SetName(string name)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetName(this.swigCPtr_, name);
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0005467D File Offset: 0x0005287D
		public virtual RailResult SetDescription(string description)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetDescription(this.swigCPtr_, description);
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0005468C File Offset: 0x0005288C
		public virtual RailResult SetTags(List<string> tags)
		{
			IntPtr intPtr = ((tags == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (tags != null)
			{
				RailConverter.Csharp2Cpp(tags, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetTags(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x000546DC File Offset: 0x000528DC
		public virtual RailResult SetPreviewImage(string path_filename)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetPreviewImage(this.swigCPtr_, path_filename);
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x000546EA File Offset: 0x000528EA
		public virtual RailResult SetVersion(string version)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetVersion(this.swigCPtr_, version);
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x000546F8 File Offset: 0x000528F8
		public virtual RailResult SetShareLevel(EnumRailSpaceWorkShareLevel level)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetShareLevel__SWIG_0(this.swigCPtr_, (int)level);
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x00054706 File Offset: 0x00052906
		public virtual RailResult SetShareLevel()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetShareLevel__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x00054713 File Offset: 0x00052913
		public virtual RailResult SetMetadata(string key, string value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetMetadata(this.swigCPtr_, key, value);
		}

		// Token: 0x060029FE RID: 10750 RVA: 0x00054722 File Offset: 0x00052922
		public virtual RailResult SetContentFromFolder(string path)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetContentFromFolder(this.swigCPtr_, path);
		}

		// Token: 0x060029FF RID: 10751 RVA: 0x00054730 File Offset: 0x00052930
		public virtual RailResult GetAllMetadata(List<RailKeyValue> metadata)
		{
			IntPtr intPtr = ((metadata == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAllMetadata(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (metadata != null)
				{
					RailConverter.Cpp2Csharp(intPtr, metadata);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x00054780 File Offset: 0x00052980
		public virtual RailResult GetAdditionalPreviewUrls(List<string> preview_urls)
		{
			IntPtr intPtr = ((preview_urls == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAdditionalPreviewUrls(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (preview_urls != null)
				{
					RailConverter.Cpp2Csharp(intPtr, preview_urls);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x000547D0 File Offset: 0x000529D0
		public virtual RailResult GetAssociatedSpaceWorks(List<SpaceWorkID> ids)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAssociatedSpaceWorks(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, ids);
				}
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x00054820 File Offset: 0x00052A20
		public virtual RailResult GetLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (languages != null)
				{
					RailConverter.Cpp2Csharp(intPtr, languages);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x00054870 File Offset: 0x00052A70
		public virtual RailResult RemoveMetadata(string key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_RemoveMetadata(this.swigCPtr_, key);
		}

		// Token: 0x06002A04 RID: 10756 RVA: 0x00054880 File Offset: 0x00052A80
		public virtual RailResult SetAdditionalPreviews(List<string> local_paths)
		{
			IntPtr intPtr = ((local_paths == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (local_paths != null)
			{
				RailConverter.Csharp2Cpp(local_paths, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetAdditionalPreviews(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A05 RID: 10757 RVA: 0x000548D0 File Offset: 0x00052AD0
		public virtual RailResult SetAssociatedSpaceWorks(List<SpaceWorkID> ids)
		{
			IntPtr intPtr = ((ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArraySpaceWorkID__SWIG_0());
			if (ids != null)
			{
				RailConverter.Csharp2Cpp(ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetAssociatedSpaceWorks(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArraySpaceWorkID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A06 RID: 10758 RVA: 0x00054920 File Offset: 0x00052B20
		public virtual RailResult SetLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (languages != null)
			{
				RailConverter.Csharp2Cpp(languages, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x00054970 File Offset: 0x00052B70
		public virtual RailResult GetPreviewUrl(out string url, uint scaling)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetPreviewUrl__SWIG_0(this.swigCPtr_, intPtr, scaling);
			}
			finally
			{
				url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x000549B8 File Offset: 0x00052BB8
		public virtual RailResult GetPreviewUrl(out string url)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetPreviewUrl__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x00054A00 File Offset: 0x00052C00
		public virtual RailResult GetVoteDetail(List<RailSpaceWorkVoteDetail> vote_details)
		{
			IntPtr intPtr = ((vote_details == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailSpaceWorkVoteDetail__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetVoteDetail(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (vote_details != null)
				{
					RailConverter.Cpp2Csharp(intPtr, vote_details);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailSpaceWorkVoteDetail(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A0A RID: 10762 RVA: 0x00054A50 File Offset: 0x00052C50
		public virtual RailResult GetUploaderIDs(List<RailID> uploader_ids)
		{
			IntPtr intPtr = ((uploader_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetUploaderIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (uploader_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, uploader_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A0B RID: 10763 RVA: 0x00054AA0 File Offset: 0x00052CA0
		public virtual RailResult SetUpdateOptions(RailSpaceWorkUpdateOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSpaceWorkUpdateOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_SetUpdateOptions(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSpaceWorkUpdateOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A0C RID: 10764 RVA: 0x00054AF0 File Offset: 0x00052CF0
		public virtual RailResult GetStatistic(EnumRailSpaceWorkStatistic stat_type, out ulong value)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetStatistic(this.swigCPtr_, (int)stat_type, out value);
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x00054AFF File Offset: 0x00052CFF
		public virtual RailResult RemovePreviewImage()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_RemovePreviewImage(this.swigCPtr_);
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x00054B0C File Offset: 0x00052D0C
		public virtual uint GetState()
		{
			return RAIL_API_PINVOKE.IRailSpaceWork_GetState(this.swigCPtr_);
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x00054B1C File Offset: 0x00052D1C
		public virtual RailResult AddAssociatedGameIDs(List<RailGameID> game_ids)
		{
			IntPtr intPtr = ((game_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameID__SWIG_0());
			if (game_ids != null)
			{
				RailConverter.Csharp2Cpp(game_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_AddAssociatedGameIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailGameID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x00054B6C File Offset: 0x00052D6C
		public virtual RailResult RemoveAssociatedGameIDs(List<RailGameID> game_ids)
		{
			IntPtr intPtr = ((game_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameID__SWIG_0());
			if (game_ids != null)
			{
				RailConverter.Csharp2Cpp(game_ids, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_RemoveAssociatedGameIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailGameID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x00054BBC File Offset: 0x00052DBC
		public virtual RailResult GetAssociatedGameIDs(List<RailGameID> game_ids)
		{
			IntPtr intPtr = ((game_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetAssociatedGameIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (game_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, game_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailGameID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00054C0C File Offset: 0x00052E0C
		public virtual RailResult GetLocalVersion(out string version)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailSpaceWork_GetLocalVersion(this.swigCPtr_, intPtr);
			}
			finally
			{
				version = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x00054C54 File Offset: 0x00052E54
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x00054C61 File Offset: 0x00052E61
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
