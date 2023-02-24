using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002D0 RID: 720
	public class RailConverter
	{
		// Token: 0x06002ACA RID: 10954 RVA: 0x00056AA0 File Offset: 0x00054CA0
		public static void Cpp2Csharp(IntPtr ptr, AcquireSessionTicketResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AcquireSessionTicketResponse_session_ticket_get(ptr), ret.session_ticket);
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x00056ABA File Offset: 0x00054CBA
		public static void Csharp2Cpp(AcquireSessionTicketResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.session_ticket, RAIL_API_PINVOKE.AcquireSessionTicketResponse_session_ticket_get(ptr));
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x00056AD4 File Offset: 0x00054CD4
		public static void Cpp2Csharp(IntPtr ptr, AsyncAcquireGameServerSessionTicketResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(ptr), ret.session_ticket);
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x00056AEE File Offset: 0x00054CEE
		public static void Csharp2Cpp(AsyncAcquireGameServerSessionTicketResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.session_ticket, RAIL_API_PINVOKE.AsyncAcquireGameServerSessionTicketResponse_session_ticket_get(ptr));
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00056B08 File Offset: 0x00054D08
		public static void Cpp2Csharp(IntPtr ptr, AsyncAddFavoriteGameServerResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncAddFavoriteGameServerResult_server_id_get(ptr), ret.server_id);
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x00056B22 File Offset: 0x00054D22
		public static void Csharp2Cpp(AsyncAddFavoriteGameServerResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_id, RAIL_API_PINVOKE.AsyncAddFavoriteGameServerResult_server_id_get(ptr));
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x00056B3C File Offset: 0x00054D3C
		public static void Cpp2Csharp(IntPtr ptr, AsyncDeleteStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncDeleteStreamFileResult_filename_get(ptr));
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x00056B56 File Offset: 0x00054D56
		public static void Csharp2Cpp(AsyncDeleteStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncDeleteStreamFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x00056B6B File Offset: 0x00054D6B
		public static void Cpp2Csharp(IntPtr ptr, AsyncGetFavoriteGameServersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetFavoriteGameServersResult_server_id_array_get(ptr), ret.server_id_array);
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x00056B85 File Offset: 0x00054D85
		public static void Csharp2Cpp(AsyncGetFavoriteGameServersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_id_array, RAIL_API_PINVOKE.AsyncGetFavoriteGameServersResult_server_id_array_get(ptr));
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00056B9F File Offset: 0x00054D9F
		public static void Cpp2Csharp(IntPtr ptr, AsyncGetMyFavoritesWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00056BC5 File Offset: 0x00054DC5
		public static void Csharp2Cpp(AsyncGetMyFavoritesWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncGetMyFavoritesWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00056BEB File Offset: 0x00054DEB
		public static void Cpp2Csharp(IntPtr ptr, AsyncGetMySubscribedWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x00056C11 File Offset: 0x00054E11
		public static void Csharp2Cpp(AsyncGetMySubscribedWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncGetMySubscribedWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00056C37 File Offset: 0x00054E37
		public static void Cpp2Csharp(IntPtr ptr, AsyncListFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncListFileResult_file_list_get(ptr), ret.file_list);
			ret.try_list_file_num = RAIL_API_PINVOKE.AsyncListFileResult_try_list_file_num_get(ptr);
			ret.all_file_num = RAIL_API_PINVOKE.AsyncListFileResult_all_file_num_get(ptr);
			ret.start_index = RAIL_API_PINVOKE.AsyncListFileResult_start_index_get(ptr);
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00056C75 File Offset: 0x00054E75
		public static void Csharp2Cpp(AsyncListFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.file_list, RAIL_API_PINVOKE.AsyncListFileResult_file_list_get(ptr));
			RAIL_API_PINVOKE.AsyncListFileResult_try_list_file_num_set(ptr, data.try_list_file_num);
			RAIL_API_PINVOKE.AsyncListFileResult_all_file_num_set(ptr, data.all_file_num);
			RAIL_API_PINVOKE.AsyncListFileResult_start_index_set(ptr, data.start_index);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x00056CB3 File Offset: 0x00054EB3
		public static void Cpp2Csharp(IntPtr ptr, AsyncModifyFavoritesWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_failure_ids_get(ptr), ret.failure_ids);
			ret.modify_flag = (EnumRailModifyFavoritesSpaceWorkType)RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_modify_flag_get(ptr);
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x00056CEA File Offset: 0x00054EEA
		public static void Csharp2Cpp(AsyncModifyFavoritesWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.failure_ids, RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_failure_ids_get(ptr));
			RAIL_API_PINVOKE.AsyncModifyFavoritesWorksResult_modify_flag_set(ptr, (int)data.modify_flag);
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x00056D21 File Offset: 0x00054F21
		public static void Cpp2Csharp(IntPtr ptr, AsyncQueryQuotaResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.available_quota = RAIL_API_PINVOKE.AsyncQueryQuotaResult_available_quota_get(ptr);
			ret.total_quota = RAIL_API_PINVOKE.AsyncQueryQuotaResult_total_quota_get(ptr);
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x00056D42 File Offset: 0x00054F42
		public static void Csharp2Cpp(AsyncQueryQuotaResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncQueryQuotaResult_available_quota_set(ptr, data.available_quota);
			RAIL_API_PINVOKE.AsyncQueryQuotaResult_total_quota_set(ptr, data.total_quota);
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x00056D63 File Offset: 0x00054F63
		public static void Cpp2Csharp(IntPtr ptr, AsyncQuerySpaceWorksInfoResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncQuerySpaceWorksInfoResult_query_spaceworks_info_result_get(ptr), ret.query_spaceworks_info_result);
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x00056D7D File Offset: 0x00054F7D
		public static void Csharp2Cpp(AsyncQuerySpaceWorksInfoResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.query_spaceworks_info_result, RAIL_API_PINVOKE.AsyncQuerySpaceWorksInfoResult_query_spaceworks_info_result_get(ptr));
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x00056D97 File Offset: 0x00054F97
		public static void Cpp2Csharp(IntPtr ptr, AsyncQuerySpaceWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06002AE1 RID: 10977 RVA: 0x00056DBD File Offset: 0x00054FBD
		public static void Csharp2Cpp(AsyncQuerySpaceWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncQuerySpaceWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06002AE2 RID: 10978 RVA: 0x00056DE3 File Offset: 0x00054FE3
		public static void Cpp2Csharp(IntPtr ptr, AsyncRateSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncRateSpaceWorkResult_id_get(ptr), ret.id);
		}

		// Token: 0x06002AE3 RID: 10979 RVA: 0x00056DFD File Offset: 0x00054FFD
		public static void Csharp2Cpp(AsyncRateSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncRateSpaceWorkResult_id_get(ptr));
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x00056E18 File Offset: 0x00055018
		public static void Cpp2Csharp(IntPtr ptr, AsyncReadFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.try_read_length = RAIL_API_PINVOKE.AsyncReadFileResult_try_read_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncReadFileResult_offset_get(ptr);
			ret.data = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncReadFileResult_data_get(ptr));
			ret.filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncReadFileResult_filename_get(ptr));
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x00056E66 File Offset: 0x00055066
		public static void Csharp2Cpp(AsyncReadFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncReadFileResult_try_read_length_set(ptr, data.try_read_length);
			RAIL_API_PINVOKE.AsyncReadFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncReadFileResult_data_set(ptr, data.data);
			RAIL_API_PINVOKE.AsyncReadFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00056EA0 File Offset: 0x000550A0
		public static void Cpp2Csharp(IntPtr ptr, AsyncReadStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.try_read_length = RAIL_API_PINVOKE.AsyncReadStreamFileResult_try_read_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncReadStreamFileResult_offset_get(ptr);
			ret.data = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncReadStreamFileResult_data_get(ptr));
			ret.filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncReadStreamFileResult_filename_get(ptr));
		}

		// Token: 0x06002AE7 RID: 10983 RVA: 0x00056EEE File Offset: 0x000550EE
		public static void Csharp2Cpp(AsyncReadStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_try_read_length_set(ptr, data.try_read_length);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_data_set(ptr, data.data);
			RAIL_API_PINVOKE.AsyncReadStreamFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x06002AE8 RID: 10984 RVA: 0x00056F27 File Offset: 0x00055127
		public static void Cpp2Csharp(IntPtr ptr, AsyncRemoveFavoriteGameServerResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncRemoveFavoriteGameServerResult_server_id_get(ptr), ret.server_id);
		}

		// Token: 0x06002AE9 RID: 10985 RVA: 0x00056F41 File Offset: 0x00055141
		public static void Csharp2Cpp(AsyncRemoveFavoriteGameServerResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_id, RAIL_API_PINVOKE.AsyncRemoveFavoriteGameServerResult_server_id_get(ptr));
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x00056F5B File Offset: 0x0005515B
		public static void Cpp2Csharp(IntPtr ptr, AsyncRemoveSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncRemoveSpaceWorkResult_id_get(ptr), ret.id);
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x00056F75 File Offset: 0x00055175
		public static void Csharp2Cpp(AsyncRemoveSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncRemoveSpaceWorkResult_id_get(ptr));
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x00056F8F File Offset: 0x0005518F
		public static void Cpp2Csharp(IntPtr ptr, AsyncRenameStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.old_filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncRenameStreamFileResult_old_filename_get(ptr));
			ret.new_filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncRenameStreamFileResult_new_filename_get(ptr));
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x00056FBA File Offset: 0x000551BA
		public static void Csharp2Cpp(AsyncRenameStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncRenameStreamFileResult_old_filename_set(ptr, data.old_filename);
			RAIL_API_PINVOKE.AsyncRenameStreamFileResult_new_filename_set(ptr, data.new_filename);
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x00056FDB File Offset: 0x000551DB
		public static void Cpp2Csharp(IntPtr ptr, AsyncSearchSpaceWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_available_works = RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_total_available_works_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x00057001 File Offset: 0x00055201
		public static void Csharp2Cpp(AsyncSearchSpaceWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.AsyncSearchSpaceWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x00057027 File Offset: 0x00055227
		public static void Cpp2Csharp(IntPtr ptr, AsyncSubscribeSpaceWorksResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_failure_ids_get(ptr), ret.failure_ids);
			ret.subscribe = RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_subscribe_get(ptr);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x0005705E File Offset: 0x0005525E
		public static void Csharp2Cpp(AsyncSubscribeSpaceWorksResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.failure_ids, RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_failure_ids_get(ptr));
			RAIL_API_PINVOKE.AsyncSubscribeSpaceWorksResult_subscribe_set(ptr, data.subscribe);
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x00057095 File Offset: 0x00055295
		public static void Cpp2Csharp(IntPtr ptr, AsyncUpdateMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.type = (EnumRailSpaceWorkType)RAIL_API_PINVOKE.AsyncUpdateMetadataResult_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncUpdateMetadataResult_id_get(ptr), ret.id);
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000570BB File Offset: 0x000552BB
		public static void Csharp2Cpp(AsyncUpdateMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncUpdateMetadataResult_type_set(ptr, (int)data.type);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncUpdateMetadataResult_id_get(ptr));
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000570E1 File Offset: 0x000552E1
		public static void Cpp2Csharp(IntPtr ptr, AsyncVoteSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.AsyncVoteSpaceWorkResult_id_get(ptr), ret.id);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000570FB File Offset: 0x000552FB
		public static void Csharp2Cpp(AsyncVoteSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.AsyncVoteSpaceWorkResult_id_get(ptr));
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x00057115 File Offset: 0x00055315
		public static void Cpp2Csharp(IntPtr ptr, AsyncWriteFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.written_length = RAIL_API_PINVOKE.AsyncWriteFileResult_written_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncWriteFileResult_offset_get(ptr);
			ret.try_write_length = RAIL_API_PINVOKE.AsyncWriteFileResult_try_write_length_get(ptr);
			ret.filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncWriteFileResult_filename_get(ptr));
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x00057153 File Offset: 0x00055353
		public static void Csharp2Cpp(AsyncWriteFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncWriteFileResult_written_length_set(ptr, data.written_length);
			RAIL_API_PINVOKE.AsyncWriteFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncWriteFileResult_try_write_length_set(ptr, data.try_write_length);
			RAIL_API_PINVOKE.AsyncWriteFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0005718C File Offset: 0x0005538C
		public static void Cpp2Csharp(IntPtr ptr, AsyncWriteStreamFileResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.written_length = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_written_length_get(ptr);
			ret.offset = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_offset_get(ptr);
			ret.try_write_length = RAIL_API_PINVOKE.AsyncWriteStreamFileResult_try_write_length_get(ptr);
			ret.filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.AsyncWriteStreamFileResult_filename_get(ptr));
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000571CA File Offset: 0x000553CA
		public static void Csharp2Cpp(AsyncWriteStreamFileResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_written_length_set(ptr, data.written_length);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_offset_set(ptr, data.offset);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_try_write_length_set(ptr, data.try_write_length);
			RAIL_API_PINVOKE.AsyncWriteStreamFileResult_filename_set(ptr, data.filename);
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x00057204 File Offset: 0x00055404
		public static void Cpp2Csharp(IntPtr ptr, BrowserDamageRectNeedsPaintRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.update_bgra_height = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_height_get(ptr);
			ret.scroll_x_pos = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_x_pos_get(ptr);
			ret.bgra_data = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_data_get(ptr));
			ret.update_bgra_width = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_width_get(ptr);
			ret.page_scale_factor = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_page_scale_factor_get(ptr);
			ret.update_offset_y = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_y_get(ptr);
			ret.update_offset_x = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_x_get(ptr);
			ret.offset_x = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_x_get(ptr);
			ret.offset_y = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_y_get(ptr);
			ret.bgra_height = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_height_get(ptr);
			ret.scroll_y_pos = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_y_pos_get(ptr);
			ret.bgra_width = RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_width_get(ptr);
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000572B0 File Offset: 0x000554B0
		public static void Csharp2Cpp(BrowserDamageRectNeedsPaintRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_height_set(ptr, data.update_bgra_height);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_x_pos_set(ptr, data.scroll_x_pos);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_data_set(ptr, data.bgra_data);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_bgra_width_set(ptr, data.update_bgra_width);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_page_scale_factor_set(ptr, data.page_scale_factor);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_y_set(ptr, data.update_offset_y);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_update_offset_x_set(ptr, data.update_offset_x);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_x_set(ptr, data.offset_x);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_offset_y_set(ptr, data.offset_y);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_height_set(ptr, data.bgra_height);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_scroll_y_pos_set(ptr, data.scroll_y_pos);
			RAIL_API_PINVOKE.BrowserDamageRectNeedsPaintRequest_bgra_width_set(ptr, data.bgra_width);
		}

		// Token: 0x06002AFC RID: 11004 RVA: 0x00057354 File Offset: 0x00055554
		public static void Cpp2Csharp(IntPtr ptr, BrowserNeedsPaintRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.bgra_width = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_width_get(ptr);
			ret.scroll_y_pos = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_y_pos_get(ptr);
			ret.bgra_data = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_data_get(ptr));
			ret.page_scale_factor = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_page_scale_factor_get(ptr);
			ret.offset_x = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_x_get(ptr);
			ret.scroll_x_pos = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_x_pos_get(ptr);
			ret.bgra_height = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_height_get(ptr);
			ret.offset_y = RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_y_get(ptr);
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000573D0 File Offset: 0x000555D0
		public static void Csharp2Cpp(BrowserNeedsPaintRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_width_set(ptr, data.bgra_width);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_y_pos_set(ptr, data.scroll_y_pos);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_data_set(ptr, data.bgra_data);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_page_scale_factor_set(ptr, data.page_scale_factor);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_x_set(ptr, data.offset_x);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_scroll_x_pos_set(ptr, data.scroll_x_pos);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_bgra_height_set(ptr, data.bgra_height);
			RAIL_API_PINVOKE.BrowserNeedsPaintRequest_offset_y_set(ptr, data.offset_y);
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x00057444 File Offset: 0x00055644
		public static void Cpp2Csharp(IntPtr ptr, BrowserRenderNavigateResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.BrowserRenderNavigateResult_url_get(ptr));
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x0005745E File Offset: 0x0005565E
		public static void Csharp2Cpp(BrowserRenderNavigateResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserRenderNavigateResult_url_set(ptr, data.url);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x00057473 File Offset: 0x00055673
		public static void Cpp2Csharp(IntPtr ptr, BrowserRenderStateChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.can_go_back = RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_back_get(ptr);
			ret.can_go_forward = RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_forward_get(ptr);
		}

		// Token: 0x06002B01 RID: 11009 RVA: 0x00057494 File Offset: 0x00055694
		public static void Csharp2Cpp(BrowserRenderStateChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_back_set(ptr, data.can_go_back);
			RAIL_API_PINVOKE.BrowserRenderStateChanged_can_go_forward_set(ptr, data.can_go_forward);
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000574B5 File Offset: 0x000556B5
		public static void Cpp2Csharp(IntPtr ptr, BrowserRenderTitleChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.new_title = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.BrowserRenderTitleChanged_new_title_get(ptr));
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000574CF File Offset: 0x000556CF
		public static void Csharp2Cpp(BrowserRenderTitleChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserRenderTitleChanged_new_title_set(ptr, data.new_title);
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000574E4 File Offset: 0x000556E4
		public static void Cpp2Csharp(IntPtr ptr, BrowserTryNavigateNewPageRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_url_get(ptr));
			ret.target_type = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_target_type_get(ptr));
			ret.is_redirect_request = RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_is_redirect_request_get(ptr);
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x0005751B File Offset: 0x0005571B
		public static void Csharp2Cpp(BrowserTryNavigateNewPageRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_url_set(ptr, data.url);
			RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_target_type_set(ptr, data.target_type);
			RAIL_API_PINVOKE.BrowserTryNavigateNewPageRequest_is_redirect_request_set(ptr, data.is_redirect_request);
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x00057548 File Offset: 0x00055748
		public static void Cpp2Csharp(IntPtr ptr, CheckAllDlcsStateReadyResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x00057551 File Offset: 0x00055751
		public static void Csharp2Cpp(CheckAllDlcsStateReadyResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0005755A File Offset: 0x0005575A
		public static void Cpp2Csharp(IntPtr ptr, ClearRoomMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.ClearRoomMetadataResult_room_id_get(ptr);
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x0005756F File Offset: 0x0005576F
		public static void Csharp2Cpp(ClearRoomMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ClearRoomMetadataResult_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x00057584 File Offset: 0x00055784
		public static void Cpp2Csharp(IntPtr ptr, CloseBrowserResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0005758D File Offset: 0x0005578D
		public static void Csharp2Cpp(CloseBrowserResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x00057596 File Offset: 0x00055796
		public static void Cpp2Csharp(IntPtr ptr, CompleteConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CompleteConsumeAssetsFinished_asset_item_get(ptr), ret.asset_item);
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000575B0 File Offset: 0x000557B0
		public static void Csharp2Cpp(CompleteConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.asset_item, RAIL_API_PINVOKE.CompleteConsumeAssetsFinished_asset_item_get(ptr));
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000575CA File Offset: 0x000557CA
		public static void Cpp2Csharp(IntPtr ptr, CompleteConsumeByExchangeAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000575D3 File Offset: 0x000557D3
		public static void Csharp2Cpp(CompleteConsumeByExchangeAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000575DC File Offset: 0x000557DC
		public static void Cpp2Csharp(IntPtr ptr, CreateBrowserOptions ret)
		{
			ret.allow_alternate_external_browser = RAIL_API_PINVOKE.CreateBrowserOptions_allow_alternate_external_browser_get(ptr);
			ret.has_minimum_button = RAIL_API_PINVOKE.CreateBrowserOptions_has_minimum_button_get(ptr);
			ret.has_border = RAIL_API_PINVOKE.CreateBrowserOptions_has_border_get(ptr);
			ret.is_movable = RAIL_API_PINVOKE.CreateBrowserOptions_is_movable_get(ptr);
			ret.has_maximum_button = RAIL_API_PINVOKE.CreateBrowserOptions_has_maximum_button_get(ptr);
			ret.margin_left = RAIL_API_PINVOKE.CreateBrowserOptions_margin_left_get(ptr);
			ret.margin_top = RAIL_API_PINVOKE.CreateBrowserOptions_margin_top_get(ptr);
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x00057640 File Offset: 0x00055840
		public static void Csharp2Cpp(CreateBrowserOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateBrowserOptions_allow_alternate_external_browser_set(ptr, data.allow_alternate_external_browser);
			RAIL_API_PINVOKE.CreateBrowserOptions_has_minimum_button_set(ptr, data.has_minimum_button);
			RAIL_API_PINVOKE.CreateBrowserOptions_has_border_set(ptr, data.has_border);
			RAIL_API_PINVOKE.CreateBrowserOptions_is_movable_set(ptr, data.is_movable);
			RAIL_API_PINVOKE.CreateBrowserOptions_has_maximum_button_set(ptr, data.has_maximum_button);
			RAIL_API_PINVOKE.CreateBrowserOptions_margin_left_set(ptr, data.margin_left);
			RAIL_API_PINVOKE.CreateBrowserOptions_margin_top_set(ptr, data.margin_top);
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x000576A1 File Offset: 0x000558A1
		public static void Cpp2Csharp(IntPtr ptr, CreateBrowserResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000576AA File Offset: 0x000558AA
		public static void Csharp2Cpp(CreateBrowserResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000576B3 File Offset: 0x000558B3
		public static void Cpp2Csharp(IntPtr ptr, CreateCustomerDrawBrowserOptions ret)
		{
			ret.content_offset_x = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_x_get(ptr);
			ret.content_offset_y = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_y_get(ptr);
			ret.content_window_height = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_height_get(ptr);
			ret.has_scroll = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_has_scroll_get(ptr);
			ret.content_window_width = RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_width_get(ptr);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000576F1 File Offset: 0x000558F1
		public static void Csharp2Cpp(CreateCustomerDrawBrowserOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_x_set(ptr, data.content_offset_x);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_offset_y_set(ptr, data.content_offset_y);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_height_set(ptr, data.content_window_height);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_has_scroll_set(ptr, data.has_scroll);
			RAIL_API_PINVOKE.CreateCustomerDrawBrowserOptions_content_window_width_set(ptr, data.content_window_width);
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x0005772F File Offset: 0x0005592F
		public static void Cpp2Csharp(IntPtr ptr, CreateGameServerOptions ret)
		{
			ret.has_password = RAIL_API_PINVOKE.CreateGameServerOptions_has_password_get(ptr);
			ret.enable_team_voice = RAIL_API_PINVOKE.CreateGameServerOptions_enable_team_voice_get(ptr);
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x00057749 File Offset: 0x00055949
		public static void Csharp2Cpp(CreateGameServerOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateGameServerOptions_has_password_set(ptr, data.has_password);
			RAIL_API_PINVOKE.CreateGameServerOptions_enable_team_voice_set(ptr, data.enable_team_voice);
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x00057763 File Offset: 0x00055963
		public static void Cpp2Csharp(IntPtr ptr, CreateGameServerResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateGameServerResult_game_server_id_get(ptr), ret.game_server_id);
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x0005777D File Offset: 0x0005597D
		public static void Csharp2Cpp(CreateGameServerResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.CreateGameServerResult_game_server_id_get(ptr));
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x00057797 File Offset: 0x00055997
		public static void Cpp2Csharp(IntPtr ptr, CreateRoomResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.CreateRoomResult_room_id_get(ptr);
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x000577AC File Offset: 0x000559AC
		public static void Csharp2Cpp(CreateRoomResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.CreateRoomResult_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000577C1 File Offset: 0x000559C1
		public static void Cpp2Csharp(IntPtr ptr, CreateSessionFailed ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionFailed_local_peer_get(ptr), ret.local_peer);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionFailed_remote_peer_get(ptr), ret.remote_peer);
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x000577EC File Offset: 0x000559EC
		public static void Csharp2Cpp(CreateSessionFailed data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.CreateSessionFailed_local_peer_get(ptr));
			RailConverter.Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.CreateSessionFailed_remote_peer_get(ptr));
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x00057817 File Offset: 0x00055A17
		public static void Cpp2Csharp(IntPtr ptr, CreateSessionRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionRequest_local_peer_get(ptr), ret.local_peer);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateSessionRequest_remote_peer_get(ptr), ret.remote_peer);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x00057842 File Offset: 0x00055A42
		public static void Csharp2Cpp(CreateSessionRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.CreateSessionRequest_local_peer_get(ptr));
			RailConverter.Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.CreateSessionRequest_remote_peer_get(ptr));
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x0005786D File Offset: 0x00055A6D
		public static void Cpp2Csharp(IntPtr ptr, CreateVoiceChannelOption ret)
		{
			ret.join_channel_after_created = RAIL_API_PINVOKE.CreateVoiceChannelOption_join_channel_after_created_get(ptr);
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0005787B File Offset: 0x00055A7B
		public static void Csharp2Cpp(CreateVoiceChannelOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.CreateVoiceChannelOption_join_channel_after_created_set(ptr, data.join_channel_after_created);
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x00057889 File Offset: 0x00055A89
		public static void Cpp2Csharp(IntPtr ptr, CreateVoiceChannelResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.CreateVoiceChannelResult_voice_channel_id_get(ptr), ret.voice_channel_id);
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x000578A3 File Offset: 0x00055AA3
		public static void Csharp2Cpp(CreateVoiceChannelResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.CreateVoiceChannelResult_voice_channel_id_get(ptr));
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x000578BD File Offset: 0x00055ABD
		public static void Cpp2Csharp(IntPtr ptr, DirectConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DirectConsumeAssetsFinished_assets_get(ptr), ret.assets);
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x000578D7 File Offset: 0x00055AD7
		public static void Csharp2Cpp(DirectConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.assets, RAIL_API_PINVOKE.DirectConsumeAssetsFinished_assets_get(ptr));
		}

		// Token: 0x06002B26 RID: 11046 RVA: 0x000578F1 File Offset: 0x00055AF1
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallFinished_dlc_id_get(ptr), ret.dlc_id);
			ret.result = (RailResult)RAIL_API_PINVOKE.DlcInstallFinished_result_get(ptr);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x00057917 File Offset: 0x00055B17
		public static void Csharp2Cpp(DlcInstallFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallFinished_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcInstallFinished_result_set(ptr, (int)data.result);
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x0005793D File Offset: 0x00055B3D
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallProgress ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallProgress_progress_get(ptr), ret.progress);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallProgress_dlc_id_get(ptr), ret.dlc_id);
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x00057968 File Offset: 0x00055B68
		public static void Csharp2Cpp(DlcInstallProgress data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.progress, RAIL_API_PINVOKE.DlcInstallProgress_progress_get(ptr));
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallProgress_dlc_id_get(ptr));
		}

		// Token: 0x06002B2A RID: 11050 RVA: 0x00057993 File Offset: 0x00055B93
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallStart ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallStart_dlc_id_get(ptr), ret.dlc_id);
		}

		// Token: 0x06002B2B RID: 11051 RVA: 0x000579AD File Offset: 0x00055BAD
		public static void Csharp2Cpp(DlcInstallStart data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallStart_dlc_id_get(ptr));
		}

		// Token: 0x06002B2C RID: 11052 RVA: 0x000579C7 File Offset: 0x00055BC7
		public static void Cpp2Csharp(IntPtr ptr, DlcInstallStartResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcInstallStartResult_dlc_id_get(ptr), ret.dlc_id);
			ret.result = (RailResult)RAIL_API_PINVOKE.DlcInstallStartResult_result_get(ptr);
		}

		// Token: 0x06002B2D RID: 11053 RVA: 0x000579ED File Offset: 0x00055BED
		public static void Csharp2Cpp(DlcInstallStartResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcInstallStartResult_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcInstallStartResult_result_set(ptr, (int)data.result);
		}

		// Token: 0x06002B2E RID: 11054 RVA: 0x00057A13 File Offset: 0x00055C13
		public static void Cpp2Csharp(IntPtr ptr, DlcOwnershipChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcOwnershipChanged_dlc_id_get(ptr), ret.dlc_id);
			ret.is_active = RAIL_API_PINVOKE.DlcOwnershipChanged_is_active_get(ptr);
		}

		// Token: 0x06002B2F RID: 11055 RVA: 0x00057A39 File Offset: 0x00055C39
		public static void Csharp2Cpp(DlcOwnershipChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcOwnershipChanged_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcOwnershipChanged_is_active_set(ptr, data.is_active);
		}

		// Token: 0x06002B30 RID: 11056 RVA: 0x00057A5F File Offset: 0x00055C5F
		public static void Cpp2Csharp(IntPtr ptr, DlcRefundChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcRefundChanged_dlc_id_get(ptr), ret.dlc_id);
			ret.refund_state = (EnumRailGameRefundState)RAIL_API_PINVOKE.DlcRefundChanged_refund_state_get(ptr);
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x00057A85 File Offset: 0x00055C85
		public static void Csharp2Cpp(DlcRefundChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcRefundChanged_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcRefundChanged_refund_state_set(ptr, (int)data.refund_state);
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x00057AAB File Offset: 0x00055CAB
		public static void Cpp2Csharp(IntPtr ptr, DlcUninstallFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.DlcUninstallFinished_dlc_id_get(ptr), ret.dlc_id);
			ret.result = (RailResult)RAIL_API_PINVOKE.DlcUninstallFinished_result_get(ptr);
		}

		// Token: 0x06002B33 RID: 11059 RVA: 0x00057AD1 File Offset: 0x00055CD1
		public static void Csharp2Cpp(DlcUninstallFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.DlcUninstallFinished_dlc_id_get(ptr));
			RAIL_API_PINVOKE.DlcUninstallFinished_result_set(ptr, (int)data.result);
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x00057AF8 File Offset: 0x00055CF8
		public static void Cpp2Csharp(IntPtr ptr, EventBase ret)
		{
			ret.result = (RailResult)RAIL_API_PINVOKE.EventBase_result_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.EventBase_game_id_get(ptr), ret.game_id);
			ret.user_data = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.EventBase_user_data_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.EventBase_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x00057B44 File Offset: 0x00055D44
		public static void Csharp2Cpp(EventBase data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.EventBase_result_set(ptr, (int)data.result);
			RailConverter.Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.EventBase_game_id_get(ptr));
			RAIL_API_PINVOKE.EventBase_user_data_set(ptr, data.user_data);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.EventBase_rail_id_get(ptr));
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x00057B80 File Offset: 0x00055D80
		public static void Cpp2Csharp(IntPtr ptr, ExchangeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsFinished_old_assets_get(ptr), ret.old_assets);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsFinished_new_asset_item_list_get(ptr), ret.new_asset_item_list);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00057BAB File Offset: 0x00055DAB
		public static void Csharp2Cpp(ExchangeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.old_assets, RAIL_API_PINVOKE.ExchangeAssetsFinished_old_assets_get(ptr));
			RailConverter.Csharp2Cpp(data.new_asset_item_list, RAIL_API_PINVOKE.ExchangeAssetsFinished_new_asset_item_list_get(ptr));
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x00057BD6 File Offset: 0x00055DD6
		public static void Cpp2Csharp(IntPtr ptr, ExchangeAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.exchange_to_asset_id = RAIL_API_PINVOKE.ExchangeAssetsToFinished_exchange_to_asset_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsToFinished_to_product_info_get(ptr), ret.to_product_info);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ExchangeAssetsToFinished_old_assets_get(ptr), ret.old_assets);
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x00057C0D File Offset: 0x00055E0D
		public static void Csharp2Cpp(ExchangeAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ExchangeAssetsToFinished_exchange_to_asset_id_set(ptr, data.exchange_to_asset_id);
			RailConverter.Csharp2Cpp(data.to_product_info, RAIL_API_PINVOKE.ExchangeAssetsToFinished_to_product_info_get(ptr));
			RailConverter.Csharp2Cpp(data.old_assets, RAIL_API_PINVOKE.ExchangeAssetsToFinished_old_assets_get(ptr));
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00057C44 File Offset: 0x00055E44
		public static void Cpp2Csharp(IntPtr ptr, GameServerInfo ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_server_kvs_get(ptr), ret.server_kvs);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_owner_rail_id_get(ptr), ret.owner_rail_id);
			ret.game_server_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_game_server_name_get(ptr));
			ret.server_fullname = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_server_fullname_get(ptr));
			ret.is_dedicated = RAIL_API_PINVOKE.GameServerInfo_is_dedicated_get(ptr);
			ret.server_info = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_server_info_get(ptr));
			ret.server_tags = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_server_tags_get(ptr));
			ret.spectator_host = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_spectator_host_get(ptr));
			ret.server_description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_server_description_get(ptr));
			ret.server_host = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_server_host_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_game_server_rail_id_get(ptr), ret.game_server_rail_id);
			ret.has_password = RAIL_API_PINVOKE.GameServerInfo_has_password_get(ptr);
			ret.server_version = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_server_version_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerInfo_server_mods_get(ptr), ret.server_mods);
			ret.bot_players = RAIL_API_PINVOKE.GameServerInfo_bot_players_get(ptr);
			ret.game_server_map = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerInfo_game_server_map_get(ptr));
			ret.max_players = RAIL_API_PINVOKE.GameServerInfo_max_players_get(ptr);
			ret.current_players = RAIL_API_PINVOKE.GameServerInfo_current_players_get(ptr);
			ret.is_friend_only = RAIL_API_PINVOKE.GameServerInfo_is_friend_only_get(ptr);
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x00057D78 File Offset: 0x00055F78
		public static void Csharp2Cpp(GameServerInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.server_kvs, RAIL_API_PINVOKE.GameServerInfo_server_kvs_get(ptr));
			RailConverter.Csharp2Cpp(data.owner_rail_id, RAIL_API_PINVOKE.GameServerInfo_owner_rail_id_get(ptr));
			RAIL_API_PINVOKE.GameServerInfo_game_server_name_set(ptr, data.game_server_name);
			RAIL_API_PINVOKE.GameServerInfo_server_fullname_set(ptr, data.server_fullname);
			RAIL_API_PINVOKE.GameServerInfo_is_dedicated_set(ptr, data.is_dedicated);
			RAIL_API_PINVOKE.GameServerInfo_server_info_set(ptr, data.server_info);
			RAIL_API_PINVOKE.GameServerInfo_server_tags_set(ptr, data.server_tags);
			RAIL_API_PINVOKE.GameServerInfo_spectator_host_set(ptr, data.spectator_host);
			RAIL_API_PINVOKE.GameServerInfo_server_description_set(ptr, data.server_description);
			RAIL_API_PINVOKE.GameServerInfo_server_host_set(ptr, data.server_host);
			RailConverter.Csharp2Cpp(data.game_server_rail_id, RAIL_API_PINVOKE.GameServerInfo_game_server_rail_id_get(ptr));
			RAIL_API_PINVOKE.GameServerInfo_has_password_set(ptr, data.has_password);
			RAIL_API_PINVOKE.GameServerInfo_server_version_set(ptr, data.server_version);
			RailConverter.Csharp2Cpp(data.server_mods, RAIL_API_PINVOKE.GameServerInfo_server_mods_get(ptr));
			RAIL_API_PINVOKE.GameServerInfo_bot_players_set(ptr, data.bot_players);
			RAIL_API_PINVOKE.GameServerInfo_game_server_map_set(ptr, data.game_server_map);
			RAIL_API_PINVOKE.GameServerInfo_max_players_set(ptr, data.max_players);
			RAIL_API_PINVOKE.GameServerInfo_current_players_set(ptr, data.current_players);
			RAIL_API_PINVOKE.GameServerInfo_is_friend_only_set(ptr, data.is_friend_only);
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x00057E80 File Offset: 0x00056080
		public static void Cpp2Csharp(IntPtr ptr, GameServerListFilter ret)
		{
			ret.tags_not_contained = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListFilter_tags_not_contained_get(ptr));
			ret.filter_password = (EnumRailOptionalValue)RAIL_API_PINVOKE.GameServerListFilter_filter_password_get(ptr);
			ret.filter_game_server_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_name_get(ptr));
			ret.filter_friends_created = (EnumRailOptionalValue)RAIL_API_PINVOKE.GameServerListFilter_filter_friends_created_get(ptr);
			ret.tags_contained = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListFilter_tags_contained_get(ptr));
			ret.filter_dedicated_server = (EnumRailOptionalValue)RAIL_API_PINVOKE.GameServerListFilter_filter_dedicated_server_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerListFilter_filters_get(ptr), ret.filters);
			ret.filter_game_server_map = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_map_get(ptr));
			ret.filter_game_server_host = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_host_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerListFilter_owner_id_get(ptr), ret.owner_id);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x00057F28 File Offset: 0x00056128
		public static void Csharp2Cpp(GameServerListFilter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerListFilter_tags_not_contained_set(ptr, data.tags_not_contained);
			RAIL_API_PINVOKE.GameServerListFilter_filter_password_set(ptr, (int)data.filter_password);
			RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_name_set(ptr, data.filter_game_server_name);
			RAIL_API_PINVOKE.GameServerListFilter_filter_friends_created_set(ptr, (int)data.filter_friends_created);
			RAIL_API_PINVOKE.GameServerListFilter_tags_contained_set(ptr, data.tags_contained);
			RAIL_API_PINVOKE.GameServerListFilter_filter_dedicated_server_set(ptr, (int)data.filter_dedicated_server);
			RailConverter.Csharp2Cpp(data.filters, RAIL_API_PINVOKE.GameServerListFilter_filters_get(ptr));
			RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_map_set(ptr, data.filter_game_server_map);
			RAIL_API_PINVOKE.GameServerListFilter_filter_game_server_host_set(ptr, data.filter_game_server_host);
			RailConverter.Csharp2Cpp(data.owner_id, RAIL_API_PINVOKE.GameServerListFilter_owner_id_get(ptr));
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00057FB7 File Offset: 0x000561B7
		public static void Cpp2Csharp(IntPtr ptr, GameServerListFilterKey ret)
		{
			ret.filter_value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListFilterKey_filter_value_get(ptr));
			ret.key_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListFilterKey_key_name_get(ptr));
			ret.value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.GameServerListFilterKey_value_type_get(ptr);
			ret.comparison_type = (EnumRailComparisonType)RAIL_API_PINVOKE.GameServerListFilterKey_comparison_type_get(ptr);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00057FF3 File Offset: 0x000561F3
		public static void Csharp2Cpp(GameServerListFilterKey data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerListFilterKey_filter_value_set(ptr, data.filter_value);
			RAIL_API_PINVOKE.GameServerListFilterKey_key_name_set(ptr, data.key_name);
			RAIL_API_PINVOKE.GameServerListFilterKey_value_type_set(ptr, (int)data.value_type);
			RAIL_API_PINVOKE.GameServerListFilterKey_comparison_type_set(ptr, (int)data.comparison_type);
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x00058025 File Offset: 0x00056225
		public static void Cpp2Csharp(IntPtr ptr, GameServerListSorter ret)
		{
			ret.sort_key = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerListSorter_sort_key_get(ptr));
			ret.sort_type = (EnumRailSortType)RAIL_API_PINVOKE.GameServerListSorter_sort_type_get(ptr);
			ret.sorter_key_type = (GameServerListSorterKeyType)RAIL_API_PINVOKE.GameServerListSorter_sorter_key_type_get(ptr);
			ret.sort_value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.GameServerListSorter_sort_value_type_get(ptr);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x0005805C File Offset: 0x0005625C
		public static void Csharp2Cpp(GameServerListSorter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerListSorter_sort_key_set(ptr, data.sort_key);
			RAIL_API_PINVOKE.GameServerListSorter_sort_type_set(ptr, (int)data.sort_type);
			RAIL_API_PINVOKE.GameServerListSorter_sorter_key_type_set(ptr, (int)data.sorter_key_type);
			RAIL_API_PINVOKE.GameServerListSorter_sort_value_type_set(ptr, (int)data.sort_value_type);
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x0005808E File Offset: 0x0005628E
		public static void Cpp2Csharp(IntPtr ptr, GameServerPlayerInfo ret)
		{
			ret.member_nickname = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GameServerPlayerInfo_member_nickname_get(ptr));
			ret.member_score = RAIL_API_PINVOKE.GameServerPlayerInfo_member_score_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerPlayerInfo_member_id_get(ptr), ret.member_id);
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000580BE File Offset: 0x000562BE
		public static void Csharp2Cpp(GameServerPlayerInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.GameServerPlayerInfo_member_nickname_set(ptr, data.member_nickname);
			RAIL_API_PINVOKE.GameServerPlayerInfo_member_score_set(ptr, data.member_score);
			RailConverter.Csharp2Cpp(data.member_id, RAIL_API_PINVOKE.GameServerPlayerInfo_member_id_get(ptr));
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000580E9 File Offset: 0x000562E9
		public static void Cpp2Csharp(IntPtr ptr, GameServerRegisterToServerListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000580F2 File Offset: 0x000562F2
		public static void Csharp2Cpp(GameServerRegisterToServerListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000580FB File Offset: 0x000562FB
		public static void Cpp2Csharp(IntPtr ptr, GameServerStartSessionWithPlayerResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GameServerStartSessionWithPlayerResponse_remote_rail_id_get(ptr), ret.remote_rail_id);
		}

		// Token: 0x06002B47 RID: 11079 RVA: 0x00058115 File Offset: 0x00056315
		public static void Csharp2Cpp(GameServerStartSessionWithPlayerResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.remote_rail_id, RAIL_API_PINVOKE.GameServerStartSessionWithPlayerResponse_remote_rail_id_get(ptr));
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x0005812F File Offset: 0x0005632F
		public static void Cpp2Csharp(IntPtr ptr, GetAllRoomDataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetAllRoomDataResult_room_info_get(ptr), ret.room_info);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x00058149 File Offset: 0x00056349
		public static void Csharp2Cpp(GetAllRoomDataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.GetAllRoomDataResult_room_info_get(ptr));
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x00058163 File Offset: 0x00056363
		public static void Cpp2Csharp(IntPtr ptr, GetAuthenticateURLResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.ticket_expire_time = RAIL_API_PINVOKE.GetAuthenticateURLResult_ticket_expire_time_get(ptr);
			ret.authenticate_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GetAuthenticateURLResult_authenticate_url_get(ptr));
			ret.source_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GetAuthenticateURLResult_source_url_get(ptr));
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x0005819A File Offset: 0x0005639A
		public static void Csharp2Cpp(GetAuthenticateURLResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.GetAuthenticateURLResult_ticket_expire_time_set(ptr, data.ticket_expire_time);
			RAIL_API_PINVOKE.GetAuthenticateURLResult_authenticate_url_set(ptr, data.authenticate_url);
			RAIL_API_PINVOKE.GetAuthenticateURLResult_source_url_set(ptr, data.source_url);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000581C7 File Offset: 0x000563C7
		public static void Cpp2Csharp(IntPtr ptr, GetGameServerListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerListResult_server_info_get(ptr), ret.server_info);
			ret.total_num = RAIL_API_PINVOKE.GetGameServerListResult_total_num_get(ptr);
			ret.start_index = RAIL_API_PINVOKE.GetGameServerListResult_start_index_get(ptr);
			ret.end_index = RAIL_API_PINVOKE.GetGameServerListResult_end_index_get(ptr);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x00058205 File Offset: 0x00056405
		public static void Csharp2Cpp(GetGameServerListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.server_info, RAIL_API_PINVOKE.GetGameServerListResult_server_info_get(ptr));
			RAIL_API_PINVOKE.GetGameServerListResult_total_num_set(ptr, data.total_num);
			RAIL_API_PINVOKE.GetGameServerListResult_start_index_set(ptr, data.start_index);
			RAIL_API_PINVOKE.GetGameServerListResult_end_index_set(ptr, data.end_index);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x00058243 File Offset: 0x00056443
		public static void Cpp2Csharp(IntPtr ptr, GetGameServerMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerMetadataResult_game_server_id_get(ptr), ret.game_server_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerMetadataResult_key_value_get(ptr), ret.key_value);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x0005826E File Offset: 0x0005646E
		public static void Csharp2Cpp(GetGameServerMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.GetGameServerMetadataResult_game_server_id_get(ptr));
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetGameServerMetadataResult_key_value_get(ptr));
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x00058299 File Offset: 0x00056499
		public static void Cpp2Csharp(IntPtr ptr, GetGameServerPlayerListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerPlayerListResult_game_server_id_get(ptr), ret.game_server_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetGameServerPlayerListResult_server_player_info_get(ptr), ret.server_player_info);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000582C4 File Offset: 0x000564C4
		public static void Csharp2Cpp(GetGameServerPlayerListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.GetGameServerPlayerListResult_game_server_id_get(ptr));
			RailConverter.Csharp2Cpp(data.server_player_info, RAIL_API_PINVOKE.GetGameServerPlayerListResult_server_player_info_get(ptr));
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000582EF File Offset: 0x000564EF
		public static void Cpp2Csharp(IntPtr ptr, GetMemberMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetMemberMetadataResult_key_value_get(ptr), ret.key_value);
			ret.room_id = RAIL_API_PINVOKE.GetMemberMetadataResult_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetMemberMetadataResult_member_id_get(ptr), ret.member_id);
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x00058326 File Offset: 0x00056526
		public static void Csharp2Cpp(GetMemberMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetMemberMetadataResult_key_value_get(ptr));
			RAIL_API_PINVOKE.GetMemberMetadataResult_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.member_id, RAIL_API_PINVOKE.GetMemberMetadataResult_member_id_get(ptr));
		}

		// Token: 0x06002B54 RID: 11092 RVA: 0x0005835D File Offset: 0x0005655D
		public static void Cpp2Csharp(IntPtr ptr, GetRoomListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetRoomListResult_room_infos_get(ptr), ret.room_infos);
			ret.total_room_num = RAIL_API_PINVOKE.GetRoomListResult_total_room_num_get(ptr);
			ret.begin_index = RAIL_API_PINVOKE.GetRoomListResult_begin_index_get(ptr);
			ret.end_index = RAIL_API_PINVOKE.GetRoomListResult_end_index_get(ptr);
		}

		// Token: 0x06002B55 RID: 11093 RVA: 0x0005839B File Offset: 0x0005659B
		public static void Csharp2Cpp(GetRoomListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.room_infos, RAIL_API_PINVOKE.GetRoomListResult_room_infos_get(ptr));
			RAIL_API_PINVOKE.GetRoomListResult_total_room_num_set(ptr, data.total_room_num);
			RAIL_API_PINVOKE.GetRoomListResult_begin_index_set(ptr, data.begin_index);
			RAIL_API_PINVOKE.GetRoomListResult_end_index_set(ptr, data.end_index);
		}

		// Token: 0x06002B56 RID: 11094 RVA: 0x000583D9 File Offset: 0x000565D9
		public static void Cpp2Csharp(IntPtr ptr, GetRoomMembersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetRoomMembersResult_member_infos_get(ptr), ret.member_infos);
			ret.room_id = RAIL_API_PINVOKE.GetRoomMembersResult_room_id_get(ptr);
			ret.member_num = RAIL_API_PINVOKE.GetRoomMembersResult_member_num_get(ptr);
		}

		// Token: 0x06002B57 RID: 11095 RVA: 0x0005840B File Offset: 0x0005660B
		public static void Csharp2Cpp(GetRoomMembersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.member_infos, RAIL_API_PINVOKE.GetRoomMembersResult_member_infos_get(ptr));
			RAIL_API_PINVOKE.GetRoomMembersResult_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.GetRoomMembersResult_member_num_set(ptr, data.member_num);
		}

		// Token: 0x06002B58 RID: 11096 RVA: 0x0005843D File Offset: 0x0005663D
		public static void Cpp2Csharp(IntPtr ptr, GetRoomMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetRoomMetadataResult_key_value_get(ptr), ret.key_value);
			ret.room_id = RAIL_API_PINVOKE.GetRoomMetadataResult_room_id_get(ptr);
		}

		// Token: 0x06002B59 RID: 11097 RVA: 0x00058463 File Offset: 0x00056663
		public static void Csharp2Cpp(GetRoomMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.GetRoomMetadataResult_key_value_get(ptr));
			RAIL_API_PINVOKE.GetRoomMetadataResult_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x00058489 File Offset: 0x00056689
		public static void Cpp2Csharp(IntPtr ptr, GetRoomTagResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_tag = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.GetRoomTagResult_room_tag_get(ptr));
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x000584A3 File Offset: 0x000566A3
		public static void Csharp2Cpp(GetRoomTagResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.GetRoomTagResult_room_tag_set(ptr, data.room_tag);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x000584B8 File Offset: 0x000566B8
		public static void Cpp2Csharp(IntPtr ptr, GetUserRoomListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.GetUserRoomListResult_room_info_get(ptr), ret.room_info);
		}

		// Token: 0x06002B5D RID: 11101 RVA: 0x000584D2 File Offset: 0x000566D2
		public static void Csharp2Cpp(GetUserRoomListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.room_info, RAIL_API_PINVOKE.GetUserRoomListResult_room_info_get(ptr));
		}

		// Token: 0x06002B5E RID: 11102 RVA: 0x000584EC File Offset: 0x000566EC
		public static void Cpp2Csharp(IntPtr ptr, GlobalAchievementReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.count = RAIL_API_PINVOKE.GlobalAchievementReceived_count_get(ptr);
		}

		// Token: 0x06002B5F RID: 11103 RVA: 0x00058501 File Offset: 0x00056701
		public static void Csharp2Cpp(GlobalAchievementReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.GlobalAchievementReceived_count_set(ptr, data.count);
		}

		// Token: 0x06002B60 RID: 11104 RVA: 0x00058516 File Offset: 0x00056716
		public static void Cpp2Csharp(IntPtr ptr, GlobalStatsRequestReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B61 RID: 11105 RVA: 0x0005851F File Offset: 0x0005671F
		public static void Csharp2Cpp(GlobalStatsRequestReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002B62 RID: 11106 RVA: 0x00058528 File Offset: 0x00056728
		public static void Cpp2Csharp(IntPtr ptr, JavascriptEventResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.event_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.JavascriptEventResult_event_name_get(ptr));
			ret.event_value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.JavascriptEventResult_event_value_get(ptr));
		}

		// Token: 0x06002B63 RID: 11107 RVA: 0x00058553 File Offset: 0x00056753
		public static void Csharp2Cpp(JavascriptEventResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.JavascriptEventResult_event_name_set(ptr, data.event_name);
			RAIL_API_PINVOKE.JavascriptEventResult_event_value_set(ptr, data.event_value);
		}

		// Token: 0x06002B64 RID: 11108 RVA: 0x00058574 File Offset: 0x00056774
		public static void Cpp2Csharp(IntPtr ptr, JoinRoomResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.JoinRoomResult_room_id_get(ptr);
		}

		// Token: 0x06002B65 RID: 11109 RVA: 0x00058589 File Offset: 0x00056789
		public static void Csharp2Cpp(JoinRoomResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.JoinRoomResult_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B66 RID: 11110 RVA: 0x0005859E File Offset: 0x0005679E
		public static void Cpp2Csharp(IntPtr ptr, JoinVoiceChannelResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.JoinVoiceChannelResult_already_joined_channel_id_get(ptr), ret.already_joined_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.JoinVoiceChannelResult_voice_channel_id_get(ptr), ret.voice_channel_id);
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x000585C9 File Offset: 0x000567C9
		public static void Csharp2Cpp(JoinVoiceChannelResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.already_joined_channel_id, RAIL_API_PINVOKE.JoinVoiceChannelResult_already_joined_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.JoinVoiceChannelResult_voice_channel_id_get(ptr));
		}

		// Token: 0x06002B68 RID: 11112 RVA: 0x000585F4 File Offset: 0x000567F4
		public static void Cpp2Csharp(IntPtr ptr, KickOffMemberResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.KickOffMemberResult_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.KickOffMemberResult_kicked_id_get(ptr), ret.kicked_id);
		}

		// Token: 0x06002B69 RID: 11113 RVA: 0x0005861A File Offset: 0x0005681A
		public static void Csharp2Cpp(KickOffMemberResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.KickOffMemberResult_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.kicked_id, RAIL_API_PINVOKE.KickOffMemberResult_kicked_id_get(ptr));
		}

		// Token: 0x06002B6A RID: 11114 RVA: 0x00058640 File Offset: 0x00056840
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardAttachSpaceWork ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_leaderboard_name_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_spacework_id_get(ptr), ret.spacework_id);
		}

		// Token: 0x06002B6B RID: 11115 RVA: 0x0005866B File Offset: 0x0005686B
		public static void Csharp2Cpp(LeaderboardAttachSpaceWork data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_leaderboard_name_set(ptr, data.leaderboard_name);
			RailConverter.Csharp2Cpp(data.spacework_id, RAIL_API_PINVOKE.LeaderboardAttachSpaceWork_spacework_id_get(ptr));
		}

		// Token: 0x06002B6C RID: 11116 RVA: 0x00058691 File Offset: 0x00056891
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardCreated ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.LeaderboardCreated_leaderboard_name_get(ptr));
		}

		// Token: 0x06002B6D RID: 11117 RVA: 0x000586AB File Offset: 0x000568AB
		public static void Csharp2Cpp(LeaderboardCreated data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardCreated_leaderboard_name_set(ptr, data.leaderboard_name);
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x000586C0 File Offset: 0x000568C0
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardData ret)
		{
			ret.additional_infomation = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.LeaderboardData_additional_infomation_get(ptr));
			ret.score = RAIL_API_PINVOKE.LeaderboardData_score_get(ptr);
			ret.rank = RAIL_API_PINVOKE.LeaderboardData_rank_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardData_spacework_id_get(ptr), ret.spacework_id);
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x000586FC File Offset: 0x000568FC
		public static void Csharp2Cpp(LeaderboardData data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.LeaderboardData_additional_infomation_set(ptr, data.additional_infomation);
			RAIL_API_PINVOKE.LeaderboardData_score_set(ptr, data.score);
			RAIL_API_PINVOKE.LeaderboardData_rank_set(ptr, data.rank);
			RailConverter.Csharp2Cpp(data.spacework_id, RAIL_API_PINVOKE.LeaderboardData_spacework_id_get(ptr));
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x00058733 File Offset: 0x00056933
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardEntry ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardEntry_player_id_get(ptr), ret.player_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaderboardEntry_data_get(ptr), ret.data);
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x00058757 File Offset: 0x00056957
		public static void Csharp2Cpp(LeaderboardEntry data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.player_id, RAIL_API_PINVOKE.LeaderboardEntry_player_id_get(ptr));
			RailConverter.Csharp2Cpp(data.data, RAIL_API_PINVOKE.LeaderboardEntry_data_get(ptr));
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0005877B File Offset: 0x0005697B
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardEntryReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.LeaderboardEntryReceived_leaderboard_name_get(ptr));
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x00058795 File Offset: 0x00056995
		public static void Csharp2Cpp(LeaderboardEntryReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardEntryReceived_leaderboard_name_set(ptr, data.leaderboard_name);
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000587AA File Offset: 0x000569AA
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardParameters ret)
		{
			ret.param = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.LeaderboardParameters_param_get(ptr));
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x000587BD File Offset: 0x000569BD
		public static void Csharp2Cpp(LeaderboardParameters data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.LeaderboardParameters_param_set(ptr, data.param);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x000587CB File Offset: 0x000569CB
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.leaderboard_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.LeaderboardReceived_leaderboard_name_get(ptr));
			ret.does_exist = RAIL_API_PINVOKE.LeaderboardReceived_does_exist_get(ptr);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x000587F1 File Offset: 0x000569F1
		public static void Csharp2Cpp(LeaderboardReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardReceived_leaderboard_name_set(ptr, data.leaderboard_name);
			RAIL_API_PINVOKE.LeaderboardReceived_does_exist_set(ptr, data.does_exist);
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x00058814 File Offset: 0x00056A14
		public static void Cpp2Csharp(IntPtr ptr, LeaderboardUploaded ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.old_rank = RAIL_API_PINVOKE.LeaderboardUploaded_old_rank_get(ptr);
			ret.leaderboard_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.LeaderboardUploaded_leaderboard_name_get(ptr));
			ret.score = RAIL_API_PINVOKE.LeaderboardUploaded_score_get(ptr);
			ret.better_score = RAIL_API_PINVOKE.LeaderboardUploaded_better_score_get(ptr);
			ret.new_rank = RAIL_API_PINVOKE.LeaderboardUploaded_new_rank_get(ptr);
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0005886C File Offset: 0x00056A6C
		public static void Csharp2Cpp(LeaderboardUploaded data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaderboardUploaded_old_rank_set(ptr, data.old_rank);
			RAIL_API_PINVOKE.LeaderboardUploaded_leaderboard_name_set(ptr, data.leaderboard_name);
			RAIL_API_PINVOKE.LeaderboardUploaded_score_set(ptr, data.score);
			RAIL_API_PINVOKE.LeaderboardUploaded_better_score_set(ptr, data.better_score);
			RAIL_API_PINVOKE.LeaderboardUploaded_new_rank_set(ptr, data.new_rank);
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x000588BC File Offset: 0x00056ABC
		public static void Cpp2Csharp(IntPtr ptr, LeaveRoomResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.reason = (EnumLeaveRoomReason)RAIL_API_PINVOKE.LeaveRoomResult_reason_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.LeaveRoomResult_room_id_get(ptr);
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x000588DD File Offset: 0x00056ADD
		public static void Csharp2Cpp(LeaveRoomResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.LeaveRoomResult_reason_set(ptr, (int)data.reason);
			RAIL_API_PINVOKE.LeaveRoomResult_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x000588FE File Offset: 0x00056AFE
		public static void Cpp2Csharp(IntPtr ptr, LeaveVoiceChannelResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.LeaveVoiceChannelResult_voice_channel_id_get(ptr), ret.voice_channel_id);
			ret.reason = (EnumRailVoiceLeaveChannelReason)RAIL_API_PINVOKE.LeaveVoiceChannelResult_reason_get(ptr);
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x00058924 File Offset: 0x00056B24
		public static void Csharp2Cpp(LeaveVoiceChannelResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.LeaveVoiceChannelResult_voice_channel_id_get(ptr));
			RAIL_API_PINVOKE.LeaveVoiceChannelResult_reason_set(ptr, (int)data.reason);
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x0005894A File Offset: 0x00056B4A
		public static void Cpp2Csharp(IntPtr ptr, MergeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.MergeAssetsFinished_source_assets_get(ptr), ret.source_assets);
			ret.new_asset_id = RAIL_API_PINVOKE.MergeAssetsFinished_new_asset_id_get(ptr);
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x00058970 File Offset: 0x00056B70
		public static void Csharp2Cpp(MergeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.source_assets, RAIL_API_PINVOKE.MergeAssetsFinished_source_assets_get(ptr));
			RAIL_API_PINVOKE.MergeAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00058996 File Offset: 0x00056B96
		public static void Cpp2Csharp(IntPtr ptr, MergeAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.merge_to_asset_id = RAIL_API_PINVOKE.MergeAssetsToFinished_merge_to_asset_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.MergeAssetsToFinished_source_assets_get(ptr), ret.source_assets);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000589BC File Offset: 0x00056BBC
		public static void Csharp2Cpp(MergeAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.MergeAssetsToFinished_merge_to_asset_id_set(ptr, data.merge_to_asset_id);
			RailConverter.Csharp2Cpp(data.source_assets, RAIL_API_PINVOKE.MergeAssetsToFinished_source_assets_get(ptr));
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000589E2 File Offset: 0x00056BE2
		public static void Cpp2Csharp(IntPtr ptr, NetworkCreateRawSessionFailed ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NetworkCreateRawSessionFailed_local_peer_get(ptr), ret.local_peer);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NetworkCreateRawSessionFailed_remote_game_peer_get(ptr), ret.remote_game_peer);
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x00058A0D File Offset: 0x00056C0D
		public static void Csharp2Cpp(NetworkCreateRawSessionFailed data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.NetworkCreateRawSessionFailed_local_peer_get(ptr));
			RailConverter.Csharp2Cpp(data.remote_game_peer, RAIL_API_PINVOKE.NetworkCreateRawSessionFailed_remote_game_peer_get(ptr));
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x00058A38 File Offset: 0x00056C38
		public static void Cpp2Csharp(IntPtr ptr, NetworkCreateRawSessionRequest ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NetworkCreateRawSessionRequest_local_peer_get(ptr), ret.local_peer);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NetworkCreateRawSessionRequest_remote_game_peer_get(ptr), ret.remote_game_peer);
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x00058A63 File Offset: 0x00056C63
		public static void Csharp2Cpp(NetworkCreateRawSessionRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.local_peer, RAIL_API_PINVOKE.NetworkCreateRawSessionRequest_local_peer_get(ptr));
			RailConverter.Csharp2Cpp(data.remote_game_peer, RAIL_API_PINVOKE.NetworkCreateRawSessionRequest_remote_game_peer_get(ptr));
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x00058A8E File Offset: 0x00056C8E
		public static void Cpp2Csharp(IntPtr ptr, NotifyMetadataChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyMetadataChange_changer_id_get(ptr), ret.changer_id);
			ret.room_id = RAIL_API_PINVOKE.NotifyMetadataChange_room_id_get(ptr);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x00058AB4 File Offset: 0x00056CB4
		public static void Csharp2Cpp(NotifyMetadataChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.changer_id, RAIL_API_PINVOKE.NotifyMetadataChange_changer_id_get(ptr));
			RAIL_API_PINVOKE.NotifyMetadataChange_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x00058ADA File Offset: 0x00056CDA
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomDestroy ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomDestroy_room_id_get(ptr);
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x00058AEF File Offset: 0x00056CEF
		public static void Csharp2Cpp(NotifyRoomDestroy data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NotifyRoomDestroy_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x00058B04 File Offset: 0x00056D04
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomGameServerChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_rail_id_get(ptr), ret.game_server_rail_id);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_room_id_get(ptr);
			ret.game_server_channel_id = RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_channel_id_get(ptr);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x00058B36 File Offset: 0x00056D36
		public static void Csharp2Cpp(NotifyRoomGameServerChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_rail_id, RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_rail_id_get(ptr));
			RAIL_API_PINVOKE.NotifyRoomGameServerChange_room_id_set(ptr, data.room_id);
			RAIL_API_PINVOKE.NotifyRoomGameServerChange_game_server_channel_id_set(ptr, data.game_server_channel_id);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x00058B68 File Offset: 0x00056D68
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomMemberChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyRoomMemberChange_changer_id_get(ptr), ret.changer_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyRoomMemberChange_id_for_making_change_get(ptr), ret.id_for_making_change);
			ret.state_change = (EnumRoomMemberActionStatus)RAIL_API_PINVOKE.NotifyRoomMemberChange_state_change_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomMemberChange_room_id_get(ptr);
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x00058BB8 File Offset: 0x00056DB8
		public static void Csharp2Cpp(NotifyRoomMemberChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.changer_id, RAIL_API_PINVOKE.NotifyRoomMemberChange_changer_id_get(ptr));
			RailConverter.Csharp2Cpp(data.id_for_making_change, RAIL_API_PINVOKE.NotifyRoomMemberChange_id_for_making_change_get(ptr));
			RAIL_API_PINVOKE.NotifyRoomMemberChange_state_change_set(ptr, (int)data.state_change);
			RAIL_API_PINVOKE.NotifyRoomMemberChange_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x00058C08 File Offset: 0x00056E08
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomMemberKicked ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyRoomMemberKicked_id_for_making_kick_get(ptr), ret.id_for_making_kick);
			ret.due_to_kicker_lost_connect = RAIL_API_PINVOKE.NotifyRoomMemberKicked_due_to_kicker_lost_connect_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomMemberKicked_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyRoomMemberKicked_kicked_id_get(ptr), ret.kicked_id);
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x00058C58 File Offset: 0x00056E58
		public static void Csharp2Cpp(NotifyRoomMemberKicked data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id_for_making_kick, RAIL_API_PINVOKE.NotifyRoomMemberKicked_id_for_making_kick_get(ptr));
			RAIL_API_PINVOKE.NotifyRoomMemberKicked_due_to_kicker_lost_connect_set(ptr, data.due_to_kicker_lost_connect);
			RAIL_API_PINVOKE.NotifyRoomMemberKicked_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.kicked_id, RAIL_API_PINVOKE.NotifyRoomMemberKicked_kicked_id_get(ptr));
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x00058CA8 File Offset: 0x00056EA8
		public static void Cpp2Csharp(IntPtr ptr, NotifyRoomOwnerChange ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyRoomOwnerChange_old_owner_id_get(ptr), ret.old_owner_id);
			ret.reason = (EnumRoomOwnerChangeReason)RAIL_API_PINVOKE.NotifyRoomOwnerChange_reason_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.NotifyRoomOwnerChange_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.NotifyRoomOwnerChange_new_owner_id_get(ptr), ret.new_owner_id);
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x00058CF8 File Offset: 0x00056EF8
		public static void Csharp2Cpp(NotifyRoomOwnerChange data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.old_owner_id, RAIL_API_PINVOKE.NotifyRoomOwnerChange_old_owner_id_get(ptr));
			RAIL_API_PINVOKE.NotifyRoomOwnerChange_reason_set(ptr, (int)data.reason);
			RAIL_API_PINVOKE.NotifyRoomOwnerChange_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.new_owner_id, RAIL_API_PINVOKE.NotifyRoomOwnerChange_new_owner_id_get(ptr));
		}

		// Token: 0x06002B92 RID: 11154 RVA: 0x00058D46 File Offset: 0x00056F46
		public static void Cpp2Csharp(IntPtr ptr, NumberOfPlayerReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.online_number = RAIL_API_PINVOKE.NumberOfPlayerReceived_online_number_get(ptr);
			ret.offline_number = RAIL_API_PINVOKE.NumberOfPlayerReceived_offline_number_get(ptr);
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x00058D67 File Offset: 0x00056F67
		public static void Csharp2Cpp(NumberOfPlayerReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.NumberOfPlayerReceived_online_number_set(ptr, data.online_number);
			RAIL_API_PINVOKE.NumberOfPlayerReceived_offline_number_set(ptr, data.offline_number);
		}

		// Token: 0x06002B94 RID: 11156 RVA: 0x00058D88 File Offset: 0x00056F88
		public static void Cpp2Csharp(IntPtr ptr, OpenRoomResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.OpenRoomResult_room_id_get(ptr);
		}

		// Token: 0x06002B95 RID: 11157 RVA: 0x00058D9D File Offset: 0x00056F9D
		public static void Csharp2Cpp(OpenRoomResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.OpenRoomResult_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x00058DB2 File Offset: 0x00056FB2
		public static void Cpp2Csharp(IntPtr ptr, PlayerAchievementReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x00058DBB File Offset: 0x00056FBB
		public static void Csharp2Cpp(PlayerAchievementReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x00058DC4 File Offset: 0x00056FC4
		public static void Cpp2Csharp(IntPtr ptr, PlayerAchievementStored ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.group_achievement = RAIL_API_PINVOKE.PlayerAchievementStored_group_achievement_get(ptr);
			ret.achievement_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.PlayerAchievementStored_achievement_name_get(ptr));
			ret.current_progress = RAIL_API_PINVOKE.PlayerAchievementStored_current_progress_get(ptr);
			ret.max_progress = RAIL_API_PINVOKE.PlayerAchievementStored_max_progress_get(ptr);
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x00058E02 File Offset: 0x00057002
		public static void Csharp2Cpp(PlayerAchievementStored data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.PlayerAchievementStored_group_achievement_set(ptr, data.group_achievement);
			RAIL_API_PINVOKE.PlayerAchievementStored_achievement_name_set(ptr, data.achievement_name);
			RAIL_API_PINVOKE.PlayerAchievementStored_current_progress_set(ptr, data.current_progress);
			RAIL_API_PINVOKE.PlayerAchievementStored_max_progress_set(ptr, data.max_progress);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x00058E3B File Offset: 0x0005703B
		public static void Cpp2Csharp(IntPtr ptr, PlayerGetGamePurchaseKeyResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.purchase_key = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.PlayerGetGamePurchaseKeyResult_purchase_key_get(ptr));
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x00058E55 File Offset: 0x00057055
		public static void Csharp2Cpp(PlayerGetGamePurchaseKeyResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.PlayerGetGamePurchaseKeyResult_purchase_key_set(ptr, data.purchase_key);
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x00058E6C File Offset: 0x0005706C
		public static void Cpp2Csharp(IntPtr ptr, PlayerPersonalInfo ret)
		{
			ret.error_code = (RailResult)RAIL_API_PINVOKE.PlayerPersonalInfo_error_code_get(ptr);
			ret.avatar_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.PlayerPersonalInfo_avatar_url_get(ptr));
			ret.rail_level = RAIL_API_PINVOKE.PlayerPersonalInfo_rail_level_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.PlayerPersonalInfo_rail_id_get(ptr), ret.rail_id);
			ret.rail_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.PlayerPersonalInfo_rail_name_get(ptr));
			ret.email_address = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.PlayerPersonalInfo_email_address_get(ptr));
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x00058ED8 File Offset: 0x000570D8
		public static void Csharp2Cpp(PlayerPersonalInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.PlayerPersonalInfo_error_code_set(ptr, (int)data.error_code);
			RAIL_API_PINVOKE.PlayerPersonalInfo_avatar_url_set(ptr, data.avatar_url);
			RAIL_API_PINVOKE.PlayerPersonalInfo_rail_level_set(ptr, data.rail_level);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.PlayerPersonalInfo_rail_id_get(ptr));
			RAIL_API_PINVOKE.PlayerPersonalInfo_rail_name_set(ptr, data.rail_name);
			RAIL_API_PINVOKE.PlayerPersonalInfo_email_address_set(ptr, data.email_address);
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x00058F32 File Offset: 0x00057132
		public static void Cpp2Csharp(IntPtr ptr, PlayerStatsReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x00058F3B File Offset: 0x0005713B
		public static void Csharp2Cpp(PlayerStatsReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x00058F44 File Offset: 0x00057144
		public static void Cpp2Csharp(IntPtr ptr, PlayerStatsStored ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002BA1 RID: 11169 RVA: 0x00058F4D File Offset: 0x0005714D
		public static void Csharp2Cpp(PlayerStatsStored data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x00058F56 File Offset: 0x00057156
		public static void Cpp2Csharp(IntPtr ptr, PublishScreenshotResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.PublishScreenshotResult_work_id_get(ptr), ret.work_id);
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x00058F70 File Offset: 0x00057170
		public static void Csharp2Cpp(PublishScreenshotResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.work_id, RAIL_API_PINVOKE.PublishScreenshotResult_work_id_get(ptr));
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x00058F8A File Offset: 0x0005718A
		public static void Cpp2Csharp(IntPtr ptr, QueryIsOwnedDlcsResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.QueryIsOwnedDlcsResult_dlc_owned_list_get(ptr), ret.dlc_owned_list);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x00058FA4 File Offset: 0x000571A4
		public static void Csharp2Cpp(QueryIsOwnedDlcsResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.dlc_owned_list, RAIL_API_PINVOKE.QueryIsOwnedDlcsResult_dlc_owned_list_get(ptr));
		}

		// Token: 0x06002BA6 RID: 11174 RVA: 0x00058FBE File Offset: 0x000571BE
		public static void Cpp2Csharp(IntPtr ptr, QueryMySubscribedSpaceWorksResult ret)
		{
			ret.total_available_works = RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_total_available_works_get(ptr);
			ret.spacework_type = (EnumRailSpaceWorkType)RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(ptr), ret.spacework_descriptors);
		}

		// Token: 0x06002BA7 RID: 11175 RVA: 0x00058FE9 File Offset: 0x000571E9
		public static void Csharp2Cpp(QueryMySubscribedSpaceWorksResult data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_total_available_works_set(ptr, data.total_available_works);
			RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_type_set(ptr, (int)data.spacework_type);
			RailConverter.Csharp2Cpp(data.spacework_descriptors, RAIL_API_PINVOKE.QueryMySubscribedSpaceWorksResult_spacework_descriptors_get(ptr));
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x00059014 File Offset: 0x00057214
		public static void Cpp2Csharp(IntPtr ptr, QueryPlayerBannedStatus ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.status = (EnumRailPlayerBannedStatus)RAIL_API_PINVOKE.QueryPlayerBannedStatus_status_get(ptr);
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x00059029 File Offset: 0x00057229
		public static void Csharp2Cpp(QueryPlayerBannedStatus data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.QueryPlayerBannedStatus_status_set(ptr, (int)data.status);
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x0005903E File Offset: 0x0005723E
		public static void Cpp2Csharp(IntPtr ptr, QuerySubscribeWishPlayStateResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_subscribed = RAIL_API_PINVOKE.QuerySubscribeWishPlayStateResult_is_subscribed_get(ptr);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x00059053 File Offset: 0x00057253
		public static void Csharp2Cpp(QuerySubscribeWishPlayStateResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.QuerySubscribeWishPlayStateResult_is_subscribed_set(ptr, data.is_subscribed);
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x00059068 File Offset: 0x00057268
		public static void Cpp2Csharp(IntPtr ptr, RailAntiAddictionGameOnlineTimeChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.game_online_time_count_minutes = RAIL_API_PINVOKE.RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_get(ptr);
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x0005907D File Offset: 0x0005727D
		public static void Csharp2Cpp(RailAntiAddictionGameOnlineTimeChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailAntiAddictionGameOnlineTimeChanged_game_online_time_count_minutes_set(ptr, data.game_online_time_count_minutes);
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x00059094 File Offset: 0x00057294
		public static void Cpp2Csharp(IntPtr ptr, RailAssetInfo ret)
		{
			ret.asset_id = RAIL_API_PINVOKE.RailAssetInfo_asset_id_get(ptr);
			ret.origin = RAIL_API_PINVOKE.RailAssetInfo_origin_get(ptr);
			ret.product_id = RAIL_API_PINVOKE.RailAssetInfo_product_id_get(ptr);
			ret.container_id = RAIL_API_PINVOKE.RailAssetInfo_container_id_get(ptr);
			ret.flag = RAIL_API_PINVOKE.RailAssetInfo_flag_get(ptr);
			ret.state = RAIL_API_PINVOKE.RailAssetInfo_state_get(ptr);
			ret.progress = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailAssetInfo_progress_get(ptr));
			ret.expire_time = RAIL_API_PINVOKE.RailAssetInfo_expire_time_get(ptr);
			ret.position = RAIL_API_PINVOKE.RailAssetInfo_position_get(ptr);
			ret.product_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailAssetInfo_product_name_get(ptr));
			ret.quantity = RAIL_API_PINVOKE.RailAssetInfo_quantity_get(ptr);
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x00059130 File Offset: 0x00057330
		public static void Csharp2Cpp(RailAssetInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailAssetInfo_asset_id_set(ptr, data.asset_id);
			RAIL_API_PINVOKE.RailAssetInfo_origin_set(ptr, data.origin);
			RAIL_API_PINVOKE.RailAssetInfo_product_id_set(ptr, data.product_id);
			RAIL_API_PINVOKE.RailAssetInfo_container_id_set(ptr, data.container_id);
			RAIL_API_PINVOKE.RailAssetInfo_flag_set(ptr, data.flag);
			RAIL_API_PINVOKE.RailAssetInfo_state_set(ptr, data.state);
			RAIL_API_PINVOKE.RailAssetInfo_progress_set(ptr, data.progress);
			RAIL_API_PINVOKE.RailAssetInfo_expire_time_set(ptr, data.expire_time);
			RAIL_API_PINVOKE.RailAssetInfo_position_set(ptr, data.position);
			RAIL_API_PINVOKE.RailAssetInfo_product_name_set(ptr, data.product_name);
			RAIL_API_PINVOKE.RailAssetInfo_quantity_set(ptr, data.quantity);
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000591C1 File Offset: 0x000573C1
		public static void Cpp2Csharp(IntPtr ptr, RailAssetItem ret)
		{
			ret.asset_id = RAIL_API_PINVOKE.RailAssetItem_asset_id_get(ptr);
			ret.quantity = RAIL_API_PINVOKE.RailAssetItem_quantity_get(ptr);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000591DB File Offset: 0x000573DB
		public static void Csharp2Cpp(RailAssetItem data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailAssetItem_asset_id_set(ptr, data.asset_id);
			RAIL_API_PINVOKE.RailAssetItem_quantity_set(ptr, data.quantity);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000591F5 File Offset: 0x000573F5
		public static void Cpp2Csharp(IntPtr ptr, RailAssetProperty ret)
		{
			ret.asset_id = RAIL_API_PINVOKE.RailAssetProperty_asset_id_get(ptr);
			ret.position = RAIL_API_PINVOKE.RailAssetProperty_position_get(ptr);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x0005920F File Offset: 0x0005740F
		public static void Csharp2Cpp(RailAssetProperty data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailAssetProperty_asset_id_set(ptr, data.asset_id);
			RAIL_API_PINVOKE.RailAssetProperty_position_set(ptr, data.position);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x00059229 File Offset: 0x00057429
		public static void Cpp2Csharp(IntPtr ptr, RailAssetsChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x00059232 File Offset: 0x00057432
		public static void Csharp2Cpp(RailAssetsChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x0005923C File Offset: 0x0005743C
		public static void Cpp2Csharp(IntPtr ptr, RailBranchInfo ret)
		{
			ret.branch_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailBranchInfo_branch_name_get(ptr));
			ret.build_number = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailBranchInfo_build_number_get(ptr));
			ret.branch_type = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailBranchInfo_branch_type_get(ptr));
			ret.branch_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailBranchInfo_branch_id_get(ptr));
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x0005928D File Offset: 0x0005748D
		public static void Csharp2Cpp(RailBranchInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailBranchInfo_branch_name_set(ptr, data.branch_name);
			RAIL_API_PINVOKE.RailBranchInfo_build_number_set(ptr, data.build_number);
			RAIL_API_PINVOKE.RailBranchInfo_branch_type_set(ptr, data.branch_type);
			RAIL_API_PINVOKE.RailBranchInfo_branch_id_set(ptr, data.branch_id);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000592C0 File Offset: 0x000574C0
		public static void Cpp2Csharp(IntPtr ptr, RailCoinInfo ret)
		{
			ret.name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCoinInfo_name_get(ptr));
			ret.icon_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCoinInfo_icon_url_get(ptr));
			ret.description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCoinInfo_description_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailCoinInfo_exchange_rate_get(ptr), ret.exchange_rate);
			ret.coin_class_id = RAIL_API_PINVOKE.RailCoinInfo_coin_class_id_get(ptr);
			ret.metadata = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCoinInfo_metadata_get(ptr));
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x00059330 File Offset: 0x00057530
		public static void Csharp2Cpp(RailCoinInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailCoinInfo_name_set(ptr, data.name);
			RAIL_API_PINVOKE.RailCoinInfo_icon_url_set(ptr, data.icon_url);
			RAIL_API_PINVOKE.RailCoinInfo_description_set(ptr, data.description);
			RailConverter.Csharp2Cpp(data.exchange_rate, RAIL_API_PINVOKE.RailCoinInfo_exchange_rate_get(ptr));
			RAIL_API_PINVOKE.RailCoinInfo_coin_class_id_set(ptr, data.coin_class_id);
			RAIL_API_PINVOKE.RailCoinInfo_metadata_set(ptr, data.metadata);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x0005938A File Offset: 0x0005758A
		public static void Cpp2Csharp(IntPtr ptr, RailCoins ret)
		{
			ret.total_price = RAIL_API_PINVOKE.RailCoins_total_price_get(ptr);
			ret.zone_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCoins_zone_id_get(ptr));
			ret.coin_class_id = RAIL_API_PINVOKE.RailCoins_coin_class_id_get(ptr);
			ret.quantity = RAIL_API_PINVOKE.RailCoins_quantity_get(ptr);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000593C1 File Offset: 0x000575C1
		public static void Csharp2Cpp(RailCoins data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailCoins_total_price_set(ptr, data.total_price);
			RAIL_API_PINVOKE.RailCoins_zone_id_set(ptr, data.zone_id);
			RAIL_API_PINVOKE.RailCoins_coin_class_id_set(ptr, data.coin_class_id);
			RAIL_API_PINVOKE.RailCoins_quantity_set(ptr, data.quantity);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000593F3 File Offset: 0x000575F3
		public static void Cpp2Csharp(IntPtr ptr, RailCrashInfo ret)
		{
			ret.exception_type = (RailUtilsCrashType)RAIL_API_PINVOKE.RailCrashInfo_exception_type_get(ptr);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x00059401 File Offset: 0x00057601
		public static void Csharp2Cpp(RailCrashInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailCrashInfo_exception_type_set(ptr, (int)data.exception_type);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x0005940F File Offset: 0x0005760F
		public static void Cpp2Csharp(IntPtr ptr, RailCurrencyExchangeCoinRate ret)
		{
			ret.currency = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCurrencyExchangeCoinRate_currency_get(ptr));
			ret.to_exchange_coins = RAIL_API_PINVOKE.RailCurrencyExchangeCoinRate_to_exchange_coins_get(ptr);
			ret.pay_price = RAIL_API_PINVOKE.RailCurrencyExchangeCoinRate_pay_price_get(ptr);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x0005943A File Offset: 0x0005763A
		public static void Csharp2Cpp(RailCurrencyExchangeCoinRate data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailCurrencyExchangeCoinRate_currency_set(ptr, data.currency);
			RAIL_API_PINVOKE.RailCurrencyExchangeCoinRate_to_exchange_coins_set(ptr, data.to_exchange_coins);
			RAIL_API_PINVOKE.RailCurrencyExchangeCoinRate_pay_price_set(ptr, data.pay_price);
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00059460 File Offset: 0x00057660
		public static void Cpp2Csharp(IntPtr ptr, RailCustomizeAntiAddictionActions ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.anti_addiction_actions = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailCustomizeAntiAddictionActions_anti_addiction_actions_get(ptr));
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x0005947A File Offset: 0x0005767A
		public static void Csharp2Cpp(RailCustomizeAntiAddictionActions data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailCustomizeAntiAddictionActions_anti_addiction_actions_set(ptr, data.anti_addiction_actions);
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x0005948F File Offset: 0x0005768F
		public static void Cpp2Csharp(IntPtr ptr, RailDirtyWordsCheckResult ret)
		{
			ret.dirty_type = (EnumRailDirtyWordsType)RAIL_API_PINVOKE.RailDirtyWordsCheckResult_dirty_type_get(ptr);
			ret.replace_string = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailDirtyWordsCheckResult_replace_string_get(ptr));
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000594AE File Offset: 0x000576AE
		public static void Csharp2Cpp(RailDirtyWordsCheckResult data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDirtyWordsCheckResult_dirty_type_set(ptr, (int)data.dirty_type);
			RAIL_API_PINVOKE.RailDirtyWordsCheckResult_replace_string_set(ptr, data.replace_string);
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000594C8 File Offset: 0x000576C8
		public static void Cpp2Csharp(IntPtr ptr, RailDiscountInfo ret)
		{
			ret.type = (PurchaseProductDiscountType)RAIL_API_PINVOKE.RailDiscountInfo_type_get(ptr);
			ret.start_time = RAIL_API_PINVOKE.RailDiscountInfo_start_time_get(ptr);
			ret.off = RAIL_API_PINVOKE.RailDiscountInfo_off_get(ptr);
			ret.discount_price = RAIL_API_PINVOKE.RailDiscountInfo_discount_price_get(ptr);
			ret.end_time = RAIL_API_PINVOKE.RailDiscountInfo_end_time_get(ptr);
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x00059506 File Offset: 0x00057706
		public static void Csharp2Cpp(RailDiscountInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDiscountInfo_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RailDiscountInfo_start_time_set(ptr, data.start_time);
			RAIL_API_PINVOKE.RailDiscountInfo_off_set(ptr, data.off);
			RAIL_API_PINVOKE.RailDiscountInfo_discount_price_set(ptr, data.discount_price);
			RAIL_API_PINVOKE.RailDiscountInfo_end_time_set(ptr, data.end_time);
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x00059544 File Offset: 0x00057744
		public static void Cpp2Csharp(IntPtr ptr, RailDlcID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailDlcID_get_id(ptr);
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x00059552 File Offset: 0x00057752
		public static void Csharp2Cpp(RailDlcID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcID_set_id(ptr, data.id_);
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x00059560 File Offset: 0x00057760
		public static void Cpp2Csharp(IntPtr ptr, RailDlcInfo ret)
		{
			ret.original_price = RAIL_API_PINVOKE.RailDlcInfo_original_price_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailDlcInfo_dlc_id_get(ptr), ret.dlc_id);
			ret.description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailDlcInfo_description_get(ptr));
			ret.discount_price = RAIL_API_PINVOKE.RailDlcInfo_discount_price_get(ptr);
			ret.version = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailDlcInfo_version_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailDlcInfo_game_id_get(ptr), ret.game_id);
			ret.name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailDlcInfo_name_get(ptr));
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000595DC File Offset: 0x000577DC
		public static void Csharp2Cpp(RailDlcInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcInfo_original_price_set(ptr, data.original_price);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.RailDlcInfo_dlc_id_get(ptr));
			RAIL_API_PINVOKE.RailDlcInfo_description_set(ptr, data.description);
			RAIL_API_PINVOKE.RailDlcInfo_discount_price_set(ptr, data.discount_price);
			RAIL_API_PINVOKE.RailDlcInfo_version_set(ptr, data.version);
			RailConverter.Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.RailDlcInfo_game_id_get(ptr));
			RAIL_API_PINVOKE.RailDlcInfo_name_set(ptr, data.name);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x00059647 File Offset: 0x00057847
		public static void Cpp2Csharp(IntPtr ptr, RailDlcInstallProgress ret)
		{
			ret.progress = RAIL_API_PINVOKE.RailDlcInstallProgress_progress_get(ptr);
			ret.finished_bytes = RAIL_API_PINVOKE.RailDlcInstallProgress_finished_bytes_get(ptr);
			ret.total_bytes = RAIL_API_PINVOKE.RailDlcInstallProgress_total_bytes_get(ptr);
			ret.speed = RAIL_API_PINVOKE.RailDlcInstallProgress_speed_get(ptr);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x00059679 File Offset: 0x00057879
		public static void Csharp2Cpp(RailDlcInstallProgress data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcInstallProgress_progress_set(ptr, data.progress);
			RAIL_API_PINVOKE.RailDlcInstallProgress_finished_bytes_set(ptr, data.finished_bytes);
			RAIL_API_PINVOKE.RailDlcInstallProgress_total_bytes_set(ptr, data.total_bytes);
			RAIL_API_PINVOKE.RailDlcInstallProgress_speed_set(ptr, data.speed);
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000596AB File Offset: 0x000578AB
		public static void Cpp2Csharp(IntPtr ptr, RailDlcOwned ret)
		{
			ret.is_owned = RAIL_API_PINVOKE.RailDlcOwned_is_owned_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailDlcOwned_dlc_id_get(ptr), ret.dlc_id);
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000596CA File Offset: 0x000578CA
		public static void Csharp2Cpp(RailDlcOwned data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailDlcOwned_is_owned_set(ptr, data.is_owned);
			RailConverter.Csharp2Cpp(data.dlc_id, RAIL_API_PINVOKE.RailDlcOwned_dlc_id_get(ptr));
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000596E9 File Offset: 0x000578E9
		public static void Cpp2Csharp(IntPtr ptr, RailFinalize ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000596F2 File Offset: 0x000578F2
		public static void Csharp2Cpp(RailFinalize data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000596FB File Offset: 0x000578FB
		public static void Cpp2Csharp(IntPtr ptr, RailFriendInfo ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendInfo_friend_rail_id_get(ptr), ret.friend_rail_id);
			ret.friend_type = (EnumRailFriendType)RAIL_API_PINVOKE.RailFriendInfo_friend_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendInfo_online_state_get(ptr), ret.online_state);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0005972B File Offset: 0x0005792B
		public static void Csharp2Cpp(RailFriendInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.friend_rail_id, RAIL_API_PINVOKE.RailFriendInfo_friend_rail_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendInfo_friend_type_set(ptr, (int)data.friend_type);
			RailConverter.Csharp2Cpp(data.online_state, RAIL_API_PINVOKE.RailFriendInfo_online_state_get(ptr));
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0005975B File Offset: 0x0005795B
		public static void Cpp2Csharp(IntPtr ptr, RailFriendMetadata ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendMetadata_friend_rail_id_get(ptr), ret.friend_rail_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendMetadata_metadatas_get(ptr), ret.metadatas);
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x0005977F File Offset: 0x0005797F
		public static void Csharp2Cpp(RailFriendMetadata data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.friend_rail_id, RAIL_API_PINVOKE.RailFriendMetadata_friend_rail_id_get(ptr));
			RailConverter.Csharp2Cpp(data.metadatas, RAIL_API_PINVOKE.RailFriendMetadata_metadatas_get(ptr));
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000597A3 File Offset: 0x000579A3
		public static void Cpp2Csharp(IntPtr ptr, RailFriendOnLineState ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendOnLineState_friend_rail_id_get(ptr), ret.friend_rail_id);
			ret.game_define_game_playing_state = RAIL_API_PINVOKE.RailFriendOnLineState_game_define_game_playing_state_get(ptr);
			ret.friend_online_state = (EnumRailPlayerOnLineState)RAIL_API_PINVOKE.RailFriendOnLineState_friend_online_state_get(ptr);
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000597CE File Offset: 0x000579CE
		public static void Csharp2Cpp(RailFriendOnLineState data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.friend_rail_id, RAIL_API_PINVOKE.RailFriendOnLineState_friend_rail_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendOnLineState_game_define_game_playing_state_set(ptr, data.game_define_game_playing_state);
			RAIL_API_PINVOKE.RailFriendOnLineState_friend_online_state_set(ptr, (int)data.friend_online_state);
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000597FC File Offset: 0x000579FC
		public static void Cpp2Csharp(IntPtr ptr, RailFriendPlayedGameInfo ret)
		{
			ret.in_room = RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_room_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendPlayedGameInfo_room_id_list_get(ptr), ret.room_id_list);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_id_get(ptr), ret.friend_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_server_id_list_get(ptr), ret.game_server_id_list);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_id_get(ptr), ret.game_id);
			ret.in_game_server = RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_game_server_get(ptr);
			ret.friend_played_game_play_state = (RailFriendPlayedGamePlayState)RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_played_game_play_state_get(ptr);
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x00059874 File Offset: 0x00057A74
		public static void Csharp2Cpp(RailFriendPlayedGameInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_room_set(ptr, data.in_room);
			RailConverter.Csharp2Cpp(data.room_id_list, RAIL_API_PINVOKE.RailFriendPlayedGameInfo_room_id_list_get(ptr));
			RailConverter.Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_id_get(ptr));
			RailConverter.Csharp2Cpp(data.game_server_id_list, RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_server_id_list_get(ptr));
			RailConverter.Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.RailFriendPlayedGameInfo_game_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_in_game_server_set(ptr, data.in_game_server);
			RAIL_API_PINVOKE.RailFriendPlayedGameInfo_friend_played_game_play_state_set(ptr, (int)data.friend_played_game_play_state);
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000598E9 File Offset: 0x00057AE9
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsAddFriendRequest ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsAddFriendRequest_target_rail_id_get(ptr), ret.target_rail_id);
		}

		// Token: 0x06002BD9 RID: 11225 RVA: 0x000598FC File Offset: 0x00057AFC
		public static void Csharp2Cpp(RailFriendsAddFriendRequest data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.target_rail_id, RAIL_API_PINVOKE.RailFriendsAddFriendRequest_target_rail_id_get(ptr));
		}

		// Token: 0x06002BDA RID: 11226 RVA: 0x0005990F File Offset: 0x00057B0F
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsAddFriendResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsAddFriendResult_target_rail_id_get(ptr), ret.target_rail_id);
		}

		// Token: 0x06002BDB RID: 11227 RVA: 0x00059929 File Offset: 0x00057B29
		public static void Csharp2Cpp(RailFriendsAddFriendResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.target_rail_id, RAIL_API_PINVOKE.RailFriendsAddFriendResult_target_rail_id_get(ptr));
		}

		// Token: 0x06002BDC RID: 11228 RVA: 0x00059943 File Offset: 0x00057B43
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsClearMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002BDD RID: 11229 RVA: 0x0005994C File Offset: 0x00057B4C
		public static void Csharp2Cpp(RailFriendsClearMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x00059955 File Offset: 0x00057B55
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsGetInviteCommandLine ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_friend_id_get(ptr), ret.friend_id);
			ret.invite_command_line = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_invite_command_line_get(ptr));
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x00059980 File Offset: 0x00057B80
		public static void Csharp2Cpp(RailFriendsGetInviteCommandLine data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_friend_id_get(ptr));
			RAIL_API_PINVOKE.RailFriendsGetInviteCommandLine_invite_command_line_set(ptr, data.invite_command_line);
		}

		// Token: 0x06002BE0 RID: 11232 RVA: 0x000599A6 File Offset: 0x00057BA6
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsGetMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_id_get(ptr), ret.friend_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_kvs_get(ptr), ret.friend_kvs);
		}

		// Token: 0x06002BE1 RID: 11233 RVA: 0x000599D1 File Offset: 0x00057BD1
		public static void Csharp2Cpp(RailFriendsGetMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_id, RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_id_get(ptr));
			RailConverter.Csharp2Cpp(data.friend_kvs, RAIL_API_PINVOKE.RailFriendsGetMetadataResult_friend_kvs_get(ptr));
		}

		// Token: 0x06002BE2 RID: 11234 RVA: 0x000599FC File Offset: 0x00057BFC
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsListChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002BE3 RID: 11235 RVA: 0x00059A05 File Offset: 0x00057C05
		public static void Csharp2Cpp(RailFriendsListChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x00059A0E File Offset: 0x00057C0E
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsMetadataChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsMetadataChanged_friends_changed_metadata_get(ptr), ret.friends_changed_metadata);
		}

		// Token: 0x06002BE5 RID: 11237 RVA: 0x00059A28 File Offset: 0x00057C28
		public static void Csharp2Cpp(RailFriendsMetadataChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friends_changed_metadata, RAIL_API_PINVOKE.RailFriendsMetadataChanged_friends_changed_metadata_get(ptr));
		}

		// Token: 0x06002BE6 RID: 11238 RVA: 0x00059A42 File Offset: 0x00057C42
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsOnlineStateChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsOnlineStateChanged_friend_online_state_get(ptr), ret.friend_online_state);
		}

		// Token: 0x06002BE7 RID: 11239 RVA: 0x00059A5C File Offset: 0x00057C5C
		public static void Csharp2Cpp(RailFriendsOnlineStateChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_online_state, RAIL_API_PINVOKE.RailFriendsOnlineStateChanged_friend_online_state_get(ptr));
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x00059A76 File Offset: 0x00057C76
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryFriendPlayedGamesResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_get(ptr), ret.friend_played_games_info_list);
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x00059A90 File Offset: 0x00057C90
		public static void Csharp2Cpp(RailFriendsQueryFriendPlayedGamesResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.friend_played_games_info_list, RAIL_API_PINVOKE.RailFriendsQueryFriendPlayedGamesResult_friend_played_games_info_list_get(ptr));
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x00059AAA File Offset: 0x00057CAA
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryPlayedWithFriendsGamesResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_get(ptr), ret.played_with_friends_game_list);
		}

		// Token: 0x06002BEB RID: 11243 RVA: 0x00059AC4 File Offset: 0x00057CC4
		public static void Csharp2Cpp(RailFriendsQueryPlayedWithFriendsGamesResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.played_with_friends_game_list, RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsGamesResult_played_with_friends_game_list_get(ptr));
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x00059ADE File Offset: 0x00057CDE
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryPlayedWithFriendsListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_get(ptr), ret.played_with_friends_list);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x00059AF8 File Offset: 0x00057CF8
		public static void Csharp2Cpp(RailFriendsQueryPlayedWithFriendsListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.played_with_friends_list, RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsListResult_played_with_friends_list_get(ptr));
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x00059B12 File Offset: 0x00057D12
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsQueryPlayedWithFriendsTimeResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_get(ptr), ret.played_with_friends_time_list);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x00059B2C File Offset: 0x00057D2C
		public static void Csharp2Cpp(RailFriendsQueryPlayedWithFriendsTimeResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.played_with_friends_time_list, RAIL_API_PINVOKE.RailFriendsQueryPlayedWithFriendsTimeResult_played_with_friends_time_list_get(ptr));
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x00059B46 File Offset: 0x00057D46
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsReportPlayedWithUserListResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x00059B4F File Offset: 0x00057D4F
		public static void Csharp2Cpp(RailFriendsReportPlayedWithUserListResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x00059B58 File Offset: 0x00057D58
		public static void Cpp2Csharp(IntPtr ptr, RailFriendsSetMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x00059B61 File Offset: 0x00057D61
		public static void Csharp2Cpp(RailFriendsSetMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x00059B6C File Offset: 0x00057D6C
		public static void Cpp2Csharp(IntPtr ptr, RailGameActivityInfo ret)
		{
			ret.activity_id = RAIL_API_PINVOKE.RailGameActivityInfo_activity_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGameActivityInfo_metadata_key_values_get(ptr), ret.metadata_key_values);
			ret.end_time = RAIL_API_PINVOKE.RailGameActivityInfo_end_time_get(ptr);
			ret.begin_time = RAIL_API_PINVOKE.RailGameActivityInfo_begin_time_get(ptr);
			ret.activity_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGameActivityInfo_activity_name_get(ptr));
			ret.activity_description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGameActivityInfo_activity_description_get(ptr));
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x00059BD0 File Offset: 0x00057DD0
		public static void Csharp2Cpp(RailGameActivityInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGameActivityInfo_activity_id_set(ptr, data.activity_id);
			RailConverter.Csharp2Cpp(data.metadata_key_values, RAIL_API_PINVOKE.RailGameActivityInfo_metadata_key_values_get(ptr));
			RAIL_API_PINVOKE.RailGameActivityInfo_end_time_set(ptr, data.end_time);
			RAIL_API_PINVOKE.RailGameActivityInfo_begin_time_set(ptr, data.begin_time);
			RAIL_API_PINVOKE.RailGameActivityInfo_activity_name_set(ptr, data.activity_name);
			RAIL_API_PINVOKE.RailGameActivityInfo_activity_description_set(ptr, data.activity_description);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x00059C2A File Offset: 0x00057E2A
		public static void Cpp2Csharp(IntPtr ptr, RailGameActivityPlayerEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.event_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGameActivityPlayerEvent_event_name_get(ptr));
			ret.from_activity_id = RAIL_API_PINVOKE.RailGameActivityPlayerEvent_from_activity_id_get(ptr);
			ret.event_value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGameActivityPlayerEvent_event_value_get(ptr));
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x00059C61 File Offset: 0x00057E61
		public static void Csharp2Cpp(RailGameActivityPlayerEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailGameActivityPlayerEvent_event_name_set(ptr, data.event_name);
			RAIL_API_PINVOKE.RailGameActivityPlayerEvent_from_activity_id_set(ptr, data.from_activity_id);
			RAIL_API_PINVOKE.RailGameActivityPlayerEvent_event_value_set(ptr, data.event_value);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x00059C8E File Offset: 0x00057E8E
		public static void Cpp2Csharp(IntPtr ptr, RailGameDefineGamePlayingState ret)
		{
			ret.state_name_zh_cn = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_zh_cn_get(ptr));
			ret.state_name_en_us = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_en_us_get(ptr));
			ret.game_define_game_playing_state = RAIL_API_PINVOKE.RailGameDefineGamePlayingState_game_define_game_playing_state_get(ptr);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x00059CBE File Offset: 0x00057EBE
		public static void Csharp2Cpp(RailGameDefineGamePlayingState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_zh_cn_set(ptr, data.state_name_zh_cn);
			RAIL_API_PINVOKE.RailGameDefineGamePlayingState_state_name_en_us_set(ptr, data.state_name_en_us);
			RAIL_API_PINVOKE.RailGameDefineGamePlayingState_game_define_game_playing_state_set(ptr, data.game_define_game_playing_state);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x00059CE4 File Offset: 0x00057EE4
		public static void Cpp2Csharp(IntPtr ptr, RailGameID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailGameID_get_id(ptr);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x00059CF2 File Offset: 0x00057EF2
		public static void Csharp2Cpp(RailGameID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGameID_set_id(ptr, data.id_);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x00059D00 File Offset: 0x00057F00
		public static void Cpp2Csharp(IntPtr ptr, RailGamePeer ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGamePeer_peer_get(ptr), ret.peer);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGamePeer_game_id_get(ptr), ret.game_id);
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x00059D24 File Offset: 0x00057F24
		public static void Csharp2Cpp(RailGamePeer data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.peer, RAIL_API_PINVOKE.RailGamePeer_peer_get(ptr));
			RailConverter.Csharp2Cpp(data.game_id, RAIL_API_PINVOKE.RailGamePeer_game_id_get(ptr));
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x00059D48 File Offset: 0x00057F48
		public static void Cpp2Csharp(IntPtr ptr, RailGameSettingMetadataChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGameSettingMetadataChanged_key_values_get(ptr), ret.key_values);
			ret.source = (RailGameSettingMetadataChangedSource)RAIL_API_PINVOKE.RailGameSettingMetadataChanged_source_get(ptr);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x00059D6E File Offset: 0x00057F6E
		public static void Csharp2Cpp(RailGameSettingMetadataChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.key_values, RAIL_API_PINVOKE.RailGameSettingMetadataChanged_key_values_get(ptr));
			RAIL_API_PINVOKE.RailGameSettingMetadataChanged_source_set(ptr, (int)data.source);
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x00059D94 File Offset: 0x00057F94
		public static void Cpp2Csharp(IntPtr ptr, RailGeneratedAssetItem ret)
		{
			ret.container_id = RAIL_API_PINVOKE.RailGeneratedAssetItem_container_id_get(ptr);
			ret.product_id = RAIL_API_PINVOKE.RailGeneratedAssetItem_product_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGeneratedAssetItem_asset_get(ptr), ret.asset);
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x00059DBF File Offset: 0x00057FBF
		public static void Csharp2Cpp(RailGeneratedAssetItem data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGeneratedAssetItem_container_id_set(ptr, data.container_id);
			RAIL_API_PINVOKE.RailGeneratedAssetItem_product_id_set(ptr, data.product_id);
			RailConverter.Csharp2Cpp(data.asset, RAIL_API_PINVOKE.RailGeneratedAssetItem_asset_get(ptr));
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x00059DEA File Offset: 0x00057FEA
		public static void Cpp2Csharp(IntPtr ptr, RailGetAuthenticateURLOptions ret)
		{
			ret.url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGetAuthenticateURLOptions_url_get(ptr));
			ret.oauth2_state = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGetAuthenticateURLOptions_oauth2_state_get(ptr));
			ret.client_id = RAIL_API_PINVOKE.RailGetAuthenticateURLOptions_client_id_get(ptr);
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x00059E1A File Offset: 0x0005801A
		public static void Csharp2Cpp(RailGetAuthenticateURLOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGetAuthenticateURLOptions_url_set(ptr, data.url);
			RAIL_API_PINVOKE.RailGetAuthenticateURLOptions_oauth2_state_set(ptr, data.oauth2_state);
			RAIL_API_PINVOKE.RailGetAuthenticateURLOptions_client_id_set(ptr, data.client_id);
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x00059E40 File Offset: 0x00058040
		public static void Cpp2Csharp(IntPtr ptr, RailGetEncryptedGameTicketResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.encrypted_game_ticket = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGetEncryptedGameTicketResult_encrypted_game_ticket_get(ptr));
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x00059E5A File Offset: 0x0005805A
		public static void Csharp2Cpp(RailGetEncryptedGameTicketResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailGetEncryptedGameTicketResult_encrypted_game_ticket_set(ptr, data.encrypted_game_ticket);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x00059E6F File Offset: 0x0005806F
		public static void Cpp2Csharp(IntPtr ptr, RailGetImageDataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGetImageDataResult_image_data_get(ptr), ret.image_data);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGetImageDataResult_image_data_descriptor_get(ptr), ret.image_data_descriptor);
		}

		// Token: 0x06002C07 RID: 11271 RVA: 0x00059E9A File Offset: 0x0005809A
		public static void Csharp2Cpp(RailGetImageDataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.image_data, RAIL_API_PINVOKE.RailGetImageDataResult_image_data_get(ptr));
			RailConverter.Csharp2Cpp(data.image_data_descriptor, RAIL_API_PINVOKE.RailGetImageDataResult_image_data_descriptor_get(ptr));
		}

		// Token: 0x06002C08 RID: 11272 RVA: 0x00059EC5 File Offset: 0x000580C5
		public static void Cpp2Csharp(IntPtr ptr, RailGetPlayerMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailGetPlayerMetadataResult_key_values_get(ptr), ret.key_values);
		}

		// Token: 0x06002C09 RID: 11273 RVA: 0x00059EDF File Offset: 0x000580DF
		public static void Csharp2Cpp(RailGetPlayerMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.key_values, RAIL_API_PINVOKE.RailGetPlayerMetadataResult_key_values_get(ptr));
		}

		// Token: 0x06002C0A RID: 11274 RVA: 0x00059EF9 File Offset: 0x000580F9
		public static void Cpp2Csharp(IntPtr ptr, RailGroupInfo ret)
		{
			ret.group_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGroupInfo_group_id_get(ptr));
			ret.group_icon_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGroupInfo_group_icon_url_get(ptr));
			ret.group_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailGroupInfo_group_name_get(ptr));
		}

		// Token: 0x06002C0B RID: 11275 RVA: 0x00059F2E File Offset: 0x0005812E
		public static void Csharp2Cpp(RailGroupInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailGroupInfo_group_id_set(ptr, data.group_id);
			RAIL_API_PINVOKE.RailGroupInfo_group_icon_url_set(ptr, data.group_icon_url);
			RAIL_API_PINVOKE.RailGroupInfo_group_name_set(ptr, data.group_name);
		}

		// Token: 0x06002C0C RID: 11276 RVA: 0x00059F54 File Offset: 0x00058154
		public static void Cpp2Csharp(IntPtr ptr, RailHttpSessionResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.http_response_data = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailHttpSessionResponse_http_response_data_get(ptr));
		}

		// Token: 0x06002C0D RID: 11277 RVA: 0x00059F6E File Offset: 0x0005816E
		public static void Csharp2Cpp(RailHttpSessionResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailHttpSessionResponse_http_response_data_set(ptr, data.http_response_data);
		}

		// Token: 0x06002C0E RID: 11278 RVA: 0x00059F83 File Offset: 0x00058183
		public static void Cpp2Csharp(IntPtr ptr, RailID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailID_get_id(ptr);
		}

		// Token: 0x06002C0F RID: 11279 RVA: 0x00059F91 File Offset: 0x00058191
		public static void Csharp2Cpp(RailID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailID_set_id(ptr, data.id_);
		}

		// Token: 0x06002C10 RID: 11280 RVA: 0x00059F9F File Offset: 0x0005819F
		public static void Cpp2Csharp(IntPtr ptr, RailImageDataDescriptor ret)
		{
			ret.pixel_format = (EnumRailImagePixelFormat)RAIL_API_PINVOKE.RailImageDataDescriptor_pixel_format_get(ptr);
			ret.image_height = RAIL_API_PINVOKE.RailImageDataDescriptor_image_height_get(ptr);
			ret.stride_in_bytes = RAIL_API_PINVOKE.RailImageDataDescriptor_stride_in_bytes_get(ptr);
			ret.image_width = RAIL_API_PINVOKE.RailImageDataDescriptor_image_width_get(ptr);
			ret.bits_per_pixel = RAIL_API_PINVOKE.RailImageDataDescriptor_bits_per_pixel_get(ptr);
		}

		// Token: 0x06002C11 RID: 11281 RVA: 0x00059FDD File Offset: 0x000581DD
		public static void Csharp2Cpp(RailImageDataDescriptor data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailImageDataDescriptor_pixel_format_set(ptr, (int)data.pixel_format);
			RAIL_API_PINVOKE.RailImageDataDescriptor_image_height_set(ptr, data.image_height);
			RAIL_API_PINVOKE.RailImageDataDescriptor_stride_in_bytes_set(ptr, data.stride_in_bytes);
			RAIL_API_PINVOKE.RailImageDataDescriptor_image_width_set(ptr, data.image_width);
			RAIL_API_PINVOKE.RailImageDataDescriptor_bits_per_pixel_set(ptr, data.bits_per_pixel);
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x0005A01B File Offset: 0x0005821B
		public static void Cpp2Csharp(IntPtr ptr, RailIMEHelperTextInputCompositionState ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.composition_text = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailIMEHelperTextInputCompositionState_composition_text_get(ptr));
			ret.composition_state = (RailIMETextInputCompositionState)RAIL_API_PINVOKE.RailIMEHelperTextInputCompositionState_composition_state_get(ptr);
		}

		// Token: 0x06002C13 RID: 11283 RVA: 0x0005A041 File Offset: 0x00058241
		public static void Csharp2Cpp(RailIMEHelperTextInputCompositionState data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailIMEHelperTextInputCompositionState_composition_text_set(ptr, data.composition_text);
			RAIL_API_PINVOKE.RailIMEHelperTextInputCompositionState_composition_state_set(ptr, (int)data.composition_state);
		}

		// Token: 0x06002C14 RID: 11284 RVA: 0x0005A062 File Offset: 0x00058262
		public static void Cpp2Csharp(IntPtr ptr, RailIMEHelperTextInputSelectedResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.content = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailIMEHelperTextInputSelectedResult_content_get(ptr));
		}

		// Token: 0x06002C15 RID: 11285 RVA: 0x0005A07C File Offset: 0x0005827C
		public static void Csharp2Cpp(RailIMEHelperTextInputSelectedResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailIMEHelperTextInputSelectedResult_content_set(ptr, data.content);
		}

		// Token: 0x06002C16 RID: 11286 RVA: 0x0005A091 File Offset: 0x00058291
		public static void Cpp2Csharp(IntPtr ptr, RailInGameCoinPurchaseCoinsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002C17 RID: 11287 RVA: 0x0005A09A File Offset: 0x0005829A
		public static void Csharp2Cpp(RailInGameCoinPurchaseCoinsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002C18 RID: 11288 RVA: 0x0005A0A3 File Offset: 0x000582A3
		public static void Cpp2Csharp(IntPtr ptr, RailInGameCoinRequestCoinInfoResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGameCoinRequestCoinInfoResponse_coin_infos_get(ptr), ret.coin_infos);
		}

		// Token: 0x06002C19 RID: 11289 RVA: 0x0005A0BD File Offset: 0x000582BD
		public static void Csharp2Cpp(RailInGameCoinRequestCoinInfoResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.coin_infos, RAIL_API_PINVOKE.RailInGameCoinRequestCoinInfoResponse_coin_infos_get(ptr));
		}

		// Token: 0x06002C1A RID: 11290 RVA: 0x0005A0D7 File Offset: 0x000582D7
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseFinishOrderResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailInGamePurchaseFinishOrderResponse_order_id_get(ptr));
		}

		// Token: 0x06002C1B RID: 11291 RVA: 0x0005A0F1 File Offset: 0x000582F1
		public static void Csharp2Cpp(RailInGamePurchaseFinishOrderResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGamePurchaseFinishOrderResponse_order_id_set(ptr, data.order_id);
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0005A106 File Offset: 0x00058306
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchasePurchaseProductsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_order_id_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_delivered_products_get(ptr), ret.delivered_products);
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x0005A131 File Offset: 0x00058331
		public static void Csharp2Cpp(RailInGamePurchasePurchaseProductsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_order_id_set(ptr, data.order_id);
			RailConverter.Csharp2Cpp(data.delivered_products, RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsResponse_delivered_products_get(ptr));
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x0005A157 File Offset: 0x00058357
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchasePurchaseProductsToAssetsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_get(ptr), ret.delivered_assets);
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x0005A182 File Offset: 0x00058382
		public static void Csharp2Cpp(RailInGamePurchasePurchaseProductsToAssetsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_order_id_set(ptr, data.order_id);
			RailConverter.Csharp2Cpp(data.delivered_assets, RAIL_API_PINVOKE.RailInGamePurchasePurchaseProductsToAssetsResponse_delivered_assets_get(ptr));
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x0005A1A8 File Offset: 0x000583A8
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseRequestAllProductsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchaseRequestAllProductsResponse_all_products_get(ptr), ret.all_products);
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x0005A1C2 File Offset: 0x000583C2
		public static void Csharp2Cpp(RailInGamePurchaseRequestAllProductsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.all_products, RAIL_API_PINVOKE.RailInGamePurchaseRequestAllProductsResponse_all_products_get(ptr));
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0005A1DC File Offset: 0x000583DC
		public static void Cpp2Csharp(IntPtr ptr, RailInGamePurchaseRequestAllPurchasableProductsResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(ptr), ret.purchasable_products);
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0005A1F6 File Offset: 0x000583F6
		public static void Csharp2Cpp(RailInGamePurchaseRequestAllPurchasableProductsResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.purchasable_products, RAIL_API_PINVOKE.RailInGamePurchaseRequestAllPurchasableProductsResponse_purchasable_products_get(ptr));
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0005A210 File Offset: 0x00058410
		public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchasePayWindowClosed ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowClosed_order_id_get(ptr));
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x0005A22A File Offset: 0x0005842A
		public static void Csharp2Cpp(RailInGameStorePurchasePayWindowClosed data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowClosed_order_id_set(ptr, data.order_id);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x0005A23F File Offset: 0x0005843F
		public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchasePayWindowDisplayed ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowDisplayed_order_id_get(ptr));
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x0005A259 File Offset: 0x00058459
		public static void Csharp2Cpp(RailInGameStorePurchasePayWindowDisplayed data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGameStorePurchasePayWindowDisplayed_order_id_set(ptr, data.order_id);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0005A26E File Offset: 0x0005846E
		public static void Cpp2Csharp(IntPtr ptr, RailInGameStorePurchaseResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.order_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailInGameStorePurchaseResult_order_id_get(ptr));
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x0005A288 File Offset: 0x00058488
		public static void Csharp2Cpp(RailInGameStorePurchaseResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailInGameStorePurchaseResult_order_id_set(ptr, data.order_id);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x0005A29D File Offset: 0x0005849D
		public static void Cpp2Csharp(IntPtr ptr, RailInviteOptions ret)
		{
			ret.additional_message = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailInviteOptions_additional_message_get(ptr));
			ret.expire_time = RAIL_API_PINVOKE.RailInviteOptions_expire_time_get(ptr);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailInviteOptions_invite_type_get(ptr);
			ret.need_respond_in_game = RAIL_API_PINVOKE.RailInviteOptions_need_respond_in_game_get(ptr);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x0005A2D4 File Offset: 0x000584D4
		public static void Csharp2Cpp(RailInviteOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailInviteOptions_additional_message_set(ptr, data.additional_message);
			RAIL_API_PINVOKE.RailInviteOptions_expire_time_set(ptr, data.expire_time);
			RAIL_API_PINVOKE.RailInviteOptions_invite_type_set(ptr, (int)data.invite_type);
			RAIL_API_PINVOKE.RailInviteOptions_need_respond_in_game_set(ptr, data.need_respond_in_game);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x0005A306 File Offset: 0x00058506
		public static void Cpp2Csharp(IntPtr ptr, RailKeyValue ret)
		{
			ret.value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailKeyValue_value_get(ptr));
			ret.key = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailKeyValue_key_get(ptr));
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x0005A32A File Offset: 0x0005852A
		public static void Csharp2Cpp(RailKeyValue data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailKeyValue_value_set(ptr, data.value);
			RAIL_API_PINVOKE.RailKeyValue_key_set(ptr, data.key);
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x0005A344 File Offset: 0x00058544
		public static void Cpp2Csharp(IntPtr ptr, RailKeyValueResult ret)
		{
			ret.error_code = (RailResult)RAIL_API_PINVOKE.RailKeyValueResult_error_code_get(ptr);
			ret.value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailKeyValueResult_value_get(ptr));
			ret.key = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailKeyValueResult_key_get(ptr));
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x0005A374 File Offset: 0x00058574
		public static void Csharp2Cpp(RailKeyValueResult data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailKeyValueResult_error_code_set(ptr, (int)data.error_code);
			RAIL_API_PINVOKE.RailKeyValueResult_value_set(ptr, data.value);
			RAIL_API_PINVOKE.RailKeyValueResult_key_set(ptr, data.key);
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0005A39A File Offset: 0x0005859A
		public static void Cpp2Csharp(IntPtr ptr, RailListStreamFileOption ret)
		{
			ret.num_files = RAIL_API_PINVOKE.RailListStreamFileOption_num_files_get(ptr);
			ret.start_index = RAIL_API_PINVOKE.RailListStreamFileOption_start_index_get(ptr);
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x0005A3B4 File Offset: 0x000585B4
		public static void Csharp2Cpp(RailListStreamFileOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailListStreamFileOption_num_files_set(ptr, data.num_files);
			RAIL_API_PINVOKE.RailListStreamFileOption_start_index_set(ptr, data.start_index);
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x0005A3D0 File Offset: 0x000585D0
		public static void Cpp2Csharp(IntPtr ptr, RailNetworkSessionState ret)
		{
			ret.session_error = (RailResult)RAIL_API_PINVOKE.RailNetworkSessionState_session_error_get(ptr);
			ret.remote_port = RAIL_API_PINVOKE.RailNetworkSessionState_remote_port_get(ptr);
			ret.packets_in_send_buffer = RAIL_API_PINVOKE.RailNetworkSessionState_packets_in_send_buffer_get(ptr);
			ret.is_connecting = RAIL_API_PINVOKE.RailNetworkSessionState_is_connecting_get(ptr);
			ret.bytes_in_send_buffer = RAIL_API_PINVOKE.RailNetworkSessionState_bytes_in_send_buffer_get(ptr);
			ret.is_using_relay = RAIL_API_PINVOKE.RailNetworkSessionState_is_using_relay_get(ptr);
			ret.is_connection_active = RAIL_API_PINVOKE.RailNetworkSessionState_is_connection_active_get(ptr);
			ret.remote_ip = RAIL_API_PINVOKE.RailNetworkSessionState_remote_ip_get(ptr);
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0005A440 File Offset: 0x00058640
		public static void Csharp2Cpp(RailNetworkSessionState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailNetworkSessionState_session_error_set(ptr, (int)data.session_error);
			RAIL_API_PINVOKE.RailNetworkSessionState_remote_port_set(ptr, data.remote_port);
			RAIL_API_PINVOKE.RailNetworkSessionState_packets_in_send_buffer_set(ptr, data.packets_in_send_buffer);
			RAIL_API_PINVOKE.RailNetworkSessionState_is_connecting_set(ptr, data.is_connecting);
			RAIL_API_PINVOKE.RailNetworkSessionState_bytes_in_send_buffer_set(ptr, data.bytes_in_send_buffer);
			RAIL_API_PINVOKE.RailNetworkSessionState_is_using_relay_set(ptr, data.is_using_relay);
			RAIL_API_PINVOKE.RailNetworkSessionState_is_connection_active_set(ptr, data.is_connection_active);
			RAIL_API_PINVOKE.RailNetworkSessionState_remote_ip_set(ptr, data.remote_ip);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0005A4AD File Offset: 0x000586AD
		public static void Cpp2Csharp(IntPtr ptr, RailNotifyNewGameActivities ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailNotifyNewGameActivities_game_activities_get(ptr), ret.game_activities);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x0005A4C7 File Offset: 0x000586C7
		public static void Csharp2Cpp(RailNotifyNewGameActivities data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_activities, RAIL_API_PINVOKE.RailNotifyNewGameActivities_game_activities_get(ptr));
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0005A4E1 File Offset: 0x000586E1
		public static void Cpp2Csharp(IntPtr ptr, RailNotifyThirdPartyAccountQrCodeInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.qr_code_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailNotifyThirdPartyAccountQrCodeInfo_qr_code_url_get(ptr));
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0005A4FB File Offset: 0x000586FB
		public static void Csharp2Cpp(RailNotifyThirdPartyAccountQrCodeInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailNotifyThirdPartyAccountQrCodeInfo_qr_code_url_set(ptr, data.qr_code_url);
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0005A510 File Offset: 0x00058710
		public static void Cpp2Csharp(IntPtr ptr, RailOpenGameActivityWindowResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.activity_id = RAIL_API_PINVOKE.RailOpenGameActivityWindowResult_activity_id_get(ptr);
			ret.is_show = RAIL_API_PINVOKE.RailOpenGameActivityWindowResult_is_show_get(ptr);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0005A531 File Offset: 0x00058731
		public static void Csharp2Cpp(RailOpenGameActivityWindowResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailOpenGameActivityWindowResult_activity_id_set(ptr, data.activity_id);
			RAIL_API_PINVOKE.RailOpenGameActivityWindowResult_is_show_set(ptr, data.is_show);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x0005A552 File Offset: 0x00058752
		public static void Cpp2Csharp(IntPtr ptr, RailOpenGroupChatResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0005A55B File Offset: 0x0005875B
		public static void Csharp2Cpp(RailOpenGroupChatResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0005A564 File Offset: 0x00058764
		public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByGameServer ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.commandline_info = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_commandline_info_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(ptr), ret.gameserver_railid);
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0005A58F File Offset: 0x0005878F
		public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByGameServer data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_commandline_info_set(ptr, data.commandline_info);
			RailConverter.Csharp2Cpp(data.gameserver_railid, RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByGameServer_gameserver_railid_get(ptr));
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x0005A5B5 File Offset: 0x000587B5
		public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByRoom ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.commandline_info = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_commandline_info_get(ptr));
			ret.room_id = RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_room_id_get(ptr);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x0005A5DB File Offset: 0x000587DB
		public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByRoom data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_commandline_info_set(ptr, data.commandline_info);
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByRoom_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x0005A5FC File Offset: 0x000587FC
		public static void Cpp2Csharp(IntPtr ptr, RailPlatformNotifyEventJoinGameByUser ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(ptr), ret.rail_id_to_join);
			ret.commandline_info = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_commandline_info_get(ptr));
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x0005A627 File Offset: 0x00058827
		public static void Csharp2Cpp(RailPlatformNotifyEventJoinGameByUser data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.rail_id_to_join, RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_rail_id_to_join_get(ptr));
			RAIL_API_PINVOKE.RailPlatformNotifyEventJoinGameByUser_commandline_info_set(ptr, data.commandline_info);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x0005A64D File Offset: 0x0005884D
		public static void Cpp2Csharp(IntPtr ptr, RailPlayedWithFriendsGameItem ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_game_ids_get(ptr), ret.game_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x0005A671 File Offset: 0x00058871
		public static void Csharp2Cpp(RailPlayedWithFriendsGameItem data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.game_ids, RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_game_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.RailPlayedWithFriendsGameItem_rail_id_get(ptr));
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x0005A695 File Offset: 0x00058895
		public static void Cpp2Csharp(IntPtr ptr, RailPlayedWithFriendsTimeItem ret)
		{
			ret.play_time = RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_play_time_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x0005A6B4 File Offset: 0x000588B4
		public static void Csharp2Cpp(RailPlayedWithFriendsTimeItem data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_play_time_set(ptr, data.play_time);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.RailPlayedWithFriendsTimeItem_rail_id_get(ptr));
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x0005A6D4 File Offset: 0x000588D4
		public static void Cpp2Csharp(IntPtr ptr, RailPlayerAchievementInfo ret)
		{
			ret.display_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlayerAchievementInfo_display_name_get(ptr));
			ret.description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlayerAchievementInfo_description_get(ptr));
			ret.unlock_time_in_seconds = RAIL_API_PINVOKE.RailPlayerAchievementInfo_unlock_time_in_seconds_get(ptr);
			ret.is_achieved = RAIL_API_PINVOKE.RailPlayerAchievementInfo_is_achieved_get(ptr);
			ret.api_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlayerAchievementInfo_api_name_get(ptr));
			ret.unachieved_icon_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlayerAchievementInfo_unachieved_icon_url_get(ptr));
			ret.is_process_achievement = RAIL_API_PINVOKE.RailPlayerAchievementInfo_is_process_achievement_get(ptr);
			ret.current_process_value = RAIL_API_PINVOKE.RailPlayerAchievementInfo_current_process_value_get(ptr);
			ret.unlock_process_value = RAIL_API_PINVOKE.RailPlayerAchievementInfo_unlock_process_value_get(ptr);
			ret.achieved_icon_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPlayerAchievementInfo_achieved_icon_url_get(ptr));
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x0005A774 File Offset: 0x00058974
		public static void Csharp2Cpp(RailPlayerAchievementInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_display_name_set(ptr, data.display_name);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_description_set(ptr, data.description);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_unlock_time_in_seconds_set(ptr, data.unlock_time_in_seconds);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_is_achieved_set(ptr, data.is_achieved);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_api_name_set(ptr, data.api_name);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_unachieved_icon_url_set(ptr, data.unachieved_icon_url);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_is_process_achievement_set(ptr, data.is_process_achievement);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_current_process_value_set(ptr, data.current_process_value);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_unlock_process_value_set(ptr, data.unlock_process_value);
			RAIL_API_PINVOKE.RailPlayerAchievementInfo_achieved_icon_url_set(ptr, data.achieved_icon_url);
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x0005A7F9 File Offset: 0x000589F9
		public static void Cpp2Csharp(IntPtr ptr, RailProductItem ret)
		{
			ret.product_id = RAIL_API_PINVOKE.RailProductItem_product_id_get(ptr);
			ret.quantity = RAIL_API_PINVOKE.RailProductItem_quantity_get(ptr);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x0005A813 File Offset: 0x00058A13
		public static void Csharp2Cpp(RailProductItem data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailProductItem_product_id_set(ptr, data.product_id);
			RAIL_API_PINVOKE.RailProductItem_quantity_set(ptr, data.quantity);
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x0005A830 File Offset: 0x00058A30
		public static void Cpp2Csharp(IntPtr ptr, RailPublishFileToUserSpaceOption ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_key_value_get(ptr), ret.key_value);
			ret.description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_description_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_tags_get(ptr), ret.tags);
			ret.level = (EnumRailSpaceWorkShareLevel)RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_level_get(ptr);
			ret.version = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_version_get(ptr));
			ret.preview_path_filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_preview_path_filename_get(ptr));
			ret.type = (EnumRailSpaceWorkType)RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_type_get(ptr);
			ret.space_work_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_space_work_name_get(ptr));
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x0005A8BC File Offset: 0x00058ABC
		public static void Csharp2Cpp(RailPublishFileToUserSpaceOption data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.key_value, RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_key_value_get(ptr));
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_description_set(ptr, data.description);
			RailConverter.Csharp2Cpp(data.tags, RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_tags_get(ptr));
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_level_set(ptr, (int)data.level);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_version_set(ptr, data.version);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_preview_path_filename_set(ptr, data.preview_path_filename);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RailPublishFileToUserSpaceOption_space_work_name_set(ptr, data.space_work_name);
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x0005A933 File Offset: 0x00058B33
		public static void Cpp2Csharp(IntPtr ptr, RailPurchaseProductExtraInfo ret)
		{
			ret.bundle_rule = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_bundle_rule_get(ptr));
			ret.exchange_rule = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_exchange_rule_get(ptr));
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x0005A957 File Offset: 0x00058B57
		public static void Csharp2Cpp(RailPurchaseProductExtraInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_bundle_rule_set(ptr, data.bundle_rule);
			RAIL_API_PINVOKE.RailPurchaseProductExtraInfo_exchange_rule_set(ptr, data.exchange_rule);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x0005A974 File Offset: 0x00058B74
		public static void Cpp2Csharp(IntPtr ptr, RailPurchaseProductInfo ret)
		{
			ret.category = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPurchaseProductInfo_category_get(ptr));
			ret.original_price = RAIL_API_PINVOKE.RailPurchaseProductInfo_original_price_get(ptr);
			ret.description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPurchaseProductInfo_description_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPurchaseProductInfo_discount_get(ptr), ret.discount);
			ret.is_purchasable = RAIL_API_PINVOKE.RailPurchaseProductInfo_is_purchasable_get(ptr);
			ret.name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPurchaseProductInfo_name_get(ptr));
			ret.currency_type = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPurchaseProductInfo_currency_type_get(ptr));
			ret.product_thumbnail = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailPurchaseProductInfo_product_thumbnail_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailPurchaseProductInfo_extra_info_get(ptr), ret.extra_info);
			ret.product_id = RAIL_API_PINVOKE.RailPurchaseProductInfo_product_id_get(ptr);
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x0005AA1C File Offset: 0x00058C1C
		public static void Csharp2Cpp(RailPurchaseProductInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailPurchaseProductInfo_category_set(ptr, data.category);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_original_price_set(ptr, data.original_price);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_description_set(ptr, data.description);
			RailConverter.Csharp2Cpp(data.discount, RAIL_API_PINVOKE.RailPurchaseProductInfo_discount_get(ptr));
			RAIL_API_PINVOKE.RailPurchaseProductInfo_is_purchasable_set(ptr, data.is_purchasable);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_name_set(ptr, data.name);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_currency_type_set(ptr, data.currency_type);
			RAIL_API_PINVOKE.RailPurchaseProductInfo_product_thumbnail_set(ptr, data.product_thumbnail);
			RailConverter.Csharp2Cpp(data.extra_info, RAIL_API_PINVOKE.RailPurchaseProductInfo_extra_info_get(ptr));
			RAIL_API_PINVOKE.RailPurchaseProductInfo_product_id_set(ptr, data.product_id);
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x0005AAAB File Offset: 0x00058CAB
		public static void Cpp2Csharp(IntPtr ptr, RailQueryGameActivityResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailQueryGameActivityResult_game_activities_get(ptr), ret.game_activities);
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x0005AAC5 File Offset: 0x00058CC5
		public static void Csharp2Cpp(RailQueryGameActivityResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_activities, RAIL_API_PINVOKE.RailQueryGameActivityResult_game_activities_get(ptr));
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x0005AADF File Offset: 0x00058CDF
		public static void Cpp2Csharp(IntPtr ptr, RailQueryGameOnlineTimeResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.game_online_time_seconds = RAIL_API_PINVOKE.RailQueryGameOnlineTimeResult_game_online_time_seconds_get(ptr);
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x0005AAF4 File Offset: 0x00058CF4
		public static void Csharp2Cpp(RailQueryGameOnlineTimeResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailQueryGameOnlineTimeResult_game_online_time_seconds_set(ptr, data.game_online_time_seconds);
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x0005AB09 File Offset: 0x00058D09
		public static void Cpp2Csharp(IntPtr ptr, RailQueryGroupsInfoResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailQueryGroupsInfoResult_group_ids_get(ptr), ret.group_ids);
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x0005AB23 File Offset: 0x00058D23
		public static void Csharp2Cpp(RailQueryGroupsInfoResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.group_ids, RAIL_API_PINVOKE.RailQueryGroupsInfoResult_group_ids_get(ptr));
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x0005AB3D File Offset: 0x00058D3D
		public static void Cpp2Csharp(IntPtr ptr, RailQuerySpaceWorkInfoResult ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailQuerySpaceWorkInfoResult_spacework_descriptor_get(ptr), ret.spacework_descriptor);
			ret.error_code = (RailResult)RAIL_API_PINVOKE.RailQuerySpaceWorkInfoResult_error_code_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailQuerySpaceWorkInfoResult_id_get(ptr), ret.id);
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x0005AB6D File Offset: 0x00058D6D
		public static void Csharp2Cpp(RailQuerySpaceWorkInfoResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.spacework_descriptor, RAIL_API_PINVOKE.RailQuerySpaceWorkInfoResult_spacework_descriptor_get(ptr));
			RAIL_API_PINVOKE.RailQuerySpaceWorkInfoResult_error_code_set(ptr, (int)data.error_code);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.RailQuerySpaceWorkInfoResult_id_get(ptr));
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x0005ABA0 File Offset: 0x00058DA0
		public static void Cpp2Csharp(IntPtr ptr, RailQueryWorkFileOptions ret)
		{
			ret.with_url = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_url_get(ptr);
			ret.with_uploader_ids = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_uploader_ids_get(ptr);
			ret.with_vote_detail = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_vote_detail_get(ptr);
			ret.with_preview_url = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_preview_url_get(ptr);
			ret.with_description = RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_description_get(ptr);
			ret.query_total_only = RAIL_API_PINVOKE.RailQueryWorkFileOptions_query_total_only_get(ptr);
			ret.preview_scaling_rate = RAIL_API_PINVOKE.RailQueryWorkFileOptions_preview_scaling_rate_get(ptr);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x0005AC04 File Offset: 0x00058E04
		public static void Csharp2Cpp(RailQueryWorkFileOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_url_set(ptr, data.with_url);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_uploader_ids_set(ptr, data.with_uploader_ids);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_vote_detail_set(ptr, data.with_vote_detail);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_preview_url_set(ptr, data.with_preview_url);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_with_description_set(ptr, data.with_description);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_query_total_only_set(ptr, data.query_total_only);
			RAIL_API_PINVOKE.RailQueryWorkFileOptions_preview_scaling_rate_set(ptr, data.preview_scaling_rate);
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x0005AC65 File Offset: 0x00058E65
		public static void Cpp2Csharp(IntPtr ptr, RailSessionTicket ret)
		{
			ret.ticket = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSessionTicket_ticket_get(ptr));
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x0005AC78 File Offset: 0x00058E78
		public static void Csharp2Cpp(RailSessionTicket data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSessionTicket_ticket_set(ptr, data.ticket);
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x0005AC86 File Offset: 0x00058E86
		public static void Cpp2Csharp(IntPtr ptr, RailShowChatWindowWithFriendResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_show = RAIL_API_PINVOKE.RailShowChatWindowWithFriendResult_is_show_get(ptr);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x0005AC9B File Offset: 0x00058E9B
		public static void Csharp2Cpp(RailShowChatWindowWithFriendResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailShowChatWindowWithFriendResult_is_show_set(ptr, data.is_show);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x0005ACB0 File Offset: 0x00058EB0
		public static void Cpp2Csharp(IntPtr ptr, RailShowUserHomepageWindowResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_show = RAIL_API_PINVOKE.RailShowUserHomepageWindowResult_is_show_get(ptr);
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x0005ACC5 File Offset: 0x00058EC5
		public static void Csharp2Cpp(RailShowUserHomepageWindowResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailShowUserHomepageWindowResult_is_show_set(ptr, data.is_show);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x0005ACDA File Offset: 0x00058EDA
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectDownloadInfo ret)
		{
			ret.index = RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_index_get(ptr);
			ret.result = (RailResult)RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_result_get(ptr);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x0005ACF4 File Offset: 0x00058EF4
		public static void Csharp2Cpp(RailSmallObjectDownloadInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_index_set(ptr, data.index);
			RAIL_API_PINVOKE.RailSmallObjectDownloadInfo_result_set(ptr, (int)data.result);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x0005AD0E File Offset: 0x00058F0E
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectDownloadResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSmallObjectDownloadResult_download_infos_get(ptr), ret.download_infos);
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x0005AD28 File Offset: 0x00058F28
		public static void Csharp2Cpp(RailSmallObjectDownloadResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.download_infos, RAIL_API_PINVOKE.RailSmallObjectDownloadResult_download_infos_get(ptr));
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x0005AD42 File Offset: 0x00058F42
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectState ret)
		{
			ret.update_state = (EnumRailSmallObjectUpdateState)RAIL_API_PINVOKE.RailSmallObjectState_update_state_get(ptr);
			ret.index = RAIL_API_PINVOKE.RailSmallObjectState_index_get(ptr);
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x0005AD5C File Offset: 0x00058F5C
		public static void Csharp2Cpp(RailSmallObjectState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSmallObjectState_update_state_set(ptr, (int)data.update_state);
			RAIL_API_PINVOKE.RailSmallObjectState_index_set(ptr, data.index);
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x0005AD76 File Offset: 0x00058F76
		public static void Cpp2Csharp(IntPtr ptr, RailSmallObjectStateQueryResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSmallObjectStateQueryResult_objects_state_get(ptr), ret.objects_state);
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x0005AD90 File Offset: 0x00058F90
		public static void Csharp2Cpp(RailSmallObjectStateQueryResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.objects_state, RAIL_API_PINVOKE.RailSmallObjectStateQueryResult_objects_state_get(ptr));
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x0005ADAC File Offset: 0x00058FAC
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkDescriptor ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_vote_details_get(ptr), ret.vote_details);
			ret.description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_description_get(ptr));
			ret.preview_scaling_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_scaling_url_get(ptr));
			ret.recommendation_rate = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_recommendation_rate_get(ptr));
			ret.preview_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_url_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_id_get(ptr), ret.id);
			ret.create_time = RAIL_API_PINVOKE.RailSpaceWorkDescriptor_create_time_get(ptr);
			ret.detail_url = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_detail_url_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_uploader_ids_get(ptr), ret.uploader_ids);
			ret.name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSpaceWorkDescriptor_name_get(ptr));
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x0005AE60 File Offset: 0x00059060
		public static void Csharp2Cpp(RailSpaceWorkDescriptor data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.vote_details, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_vote_details_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_description_set(ptr, data.description);
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_scaling_url_set(ptr, data.preview_scaling_url);
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_recommendation_rate_set(ptr, data.recommendation_rate);
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_preview_url_set(ptr, data.preview_url);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_id_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_create_time_set(ptr, data.create_time);
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_detail_url_set(ptr, data.detail_url);
			RailConverter.Csharp2Cpp(data.uploader_ids, RAIL_API_PINVOKE.RailSpaceWorkDescriptor_uploader_ids_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkDescriptor_name_set(ptr, data.name);
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x0005AEF4 File Offset: 0x000590F4
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkFilter ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_classes_get(ptr), ret.classes);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_type_get(ptr), ret.type);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_collector_list_get(ptr), ret.collector_list);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_subscriber_list_get(ptr), ret.subscriber_list);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkFilter_creator_list_get(ptr), ret.creator_list);
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x0005AF58 File Offset: 0x00059158
		public static void Csharp2Cpp(RailSpaceWorkFilter data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.classes, RAIL_API_PINVOKE.RailSpaceWorkFilter_classes_get(ptr));
			RailConverter.Csharp2Cpp(data.type, RAIL_API_PINVOKE.RailSpaceWorkFilter_type_get(ptr));
			RailConverter.Csharp2Cpp(data.collector_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_collector_list_get(ptr));
			RailConverter.Csharp2Cpp(data.subscriber_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_subscriber_list_get(ptr));
			RailConverter.Csharp2Cpp(data.creator_list, RAIL_API_PINVOKE.RailSpaceWorkFilter_creator_list_get(ptr));
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x0005AFBC File Offset: 0x000591BC
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkSearchFilter ret)
		{
			ret.search_text = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_search_text_get(ptr));
			ret.match_all_required_tags = RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_match_all_required_tags_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_metadata_get(ptr), ret.required_metadata);
			ret.match_all_required_metadata = RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_match_all_required_metadata_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_tags_get(ptr), ret.required_tags);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_metadata_get(ptr), ret.excluded_metadata);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_tags_get(ptr), ret.excluded_tags);
		}

		// Token: 0x06002C6D RID: 11373 RVA: 0x0005B038 File Offset: 0x00059238
		public static void Csharp2Cpp(RailSpaceWorkSearchFilter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_search_text_set(ptr, data.search_text);
			RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_match_all_required_tags_set(ptr, data.match_all_required_tags);
			RailConverter.Csharp2Cpp(data.required_metadata, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_metadata_get(ptr));
			RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_match_all_required_metadata_set(ptr, data.match_all_required_metadata);
			RailConverter.Csharp2Cpp(data.required_tags, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_required_tags_get(ptr));
			RailConverter.Csharp2Cpp(data.excluded_metadata, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_metadata_get(ptr));
			RailConverter.Csharp2Cpp(data.excluded_tags, RAIL_API_PINVOKE.RailSpaceWorkSearchFilter_excluded_tags_get(ptr));
		}

		// Token: 0x06002C6E RID: 11374 RVA: 0x0005B0AD File Offset: 0x000592AD
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkSyncProgress ret)
		{
			ret.progress = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_progress_get(ptr);
			ret.finished_bytes = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_finished_bytes_get(ptr);
			ret.total_bytes = RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_total_bytes_get(ptr);
			ret.current_state = (EnumRailSpaceWorkSyncState)RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_current_state_get(ptr);
		}

		// Token: 0x06002C6F RID: 11375 RVA: 0x0005B0DF File Offset: 0x000592DF
		public static void Csharp2Cpp(RailSpaceWorkSyncProgress data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_progress_set(ptr, data.progress);
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_finished_bytes_set(ptr, data.finished_bytes);
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_total_bytes_set(ptr, data.total_bytes);
			RAIL_API_PINVOKE.RailSpaceWorkSyncProgress_current_state_set(ptr, (int)data.current_state);
		}

		// Token: 0x06002C70 RID: 11376 RVA: 0x0005B114 File Offset: 0x00059314
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkUpdateOptions ret)
		{
			ret.with_my_vote = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_my_vote_get(ptr);
			ret.with_vote_detail = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_vote_detail_get(ptr);
			ret.with_metadata = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_metadata_get(ptr);
			ret.with_detail = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_detail_get(ptr);
			ret.check_has_subscribed = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_subscribed_get(ptr);
			ret.check_has_favorited = RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_favorited_get(ptr);
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x0005B16C File Offset: 0x0005936C
		public static void Csharp2Cpp(RailSpaceWorkUpdateOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_my_vote_set(ptr, data.with_my_vote);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_vote_detail_set(ptr, data.with_vote_detail);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_metadata_set(ptr, data.with_metadata);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_with_detail_set(ptr, data.with_detail);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_subscribed_set(ptr, data.check_has_subscribed);
			RAIL_API_PINVOKE.RailSpaceWorkUpdateOptions_check_has_favorited_set(ptr, data.check_has_favorited);
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x0005B1C1 File Offset: 0x000593C1
		public static void Cpp2Csharp(IntPtr ptr, RailSpaceWorkVoteDetail ret)
		{
			ret.vote_value = (EnumRailSpaceWorkRateValue)RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_vote_value_get(ptr);
			ret.voted_players = RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_voted_players_get(ptr);
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x0005B1DB File Offset: 0x000593DB
		public static void Csharp2Cpp(RailSpaceWorkVoteDetail data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_vote_value_set(ptr, (int)data.vote_value);
			RAIL_API_PINVOKE.RailSpaceWorkVoteDetail_voted_players_set(ptr, data.voted_players);
		}

		// Token: 0x06002C74 RID: 11380 RVA: 0x0005B1F5 File Offset: 0x000593F5
		public static void Cpp2Csharp(IntPtr ptr, RailStoreOptions ret)
		{
			ret.window_margin_top = RAIL_API_PINVOKE.RailStoreOptions_window_margin_top_get(ptr);
			ret.window_margin_left = RAIL_API_PINVOKE.RailStoreOptions_window_margin_left_get(ptr);
			ret.store_type = (EnumRailStoreType)RAIL_API_PINVOKE.RailStoreOptions_store_type_get(ptr);
		}

		// Token: 0x06002C75 RID: 11381 RVA: 0x0005B21B File Offset: 0x0005941B
		public static void Csharp2Cpp(RailStoreOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailStoreOptions_window_margin_top_set(ptr, data.window_margin_top);
			RAIL_API_PINVOKE.RailStoreOptions_window_margin_left_set(ptr, data.window_margin_left);
			RAIL_API_PINVOKE.RailStoreOptions_store_type_set(ptr, (int)data.store_type);
		}

		// Token: 0x06002C76 RID: 11382 RVA: 0x0005B241 File Offset: 0x00059441
		public static void Cpp2Csharp(IntPtr ptr, RailStreamFileInfo ret)
		{
			ret.file_size = RAIL_API_PINVOKE.RailStreamFileInfo_file_size_get(ptr);
			ret.filename = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailStreamFileInfo_filename_get(ptr));
		}

		// Token: 0x06002C77 RID: 11383 RVA: 0x0005B260 File Offset: 0x00059460
		public static void Csharp2Cpp(RailStreamFileInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailStreamFileInfo_file_size_set(ptr, data.file_size);
			RAIL_API_PINVOKE.RailStreamFileInfo_filename_set(ptr, data.filename);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x0005B27A File Offset: 0x0005947A
		public static void Cpp2Csharp(IntPtr ptr, RailStreamFileOption ret)
		{
			ret.open_type = (EnumRailStreamOpenFileType)RAIL_API_PINVOKE.RailStreamFileOption_open_type_get(ptr);
			ret.unavaliabe_when_new_file_writing = RAIL_API_PINVOKE.RailStreamFileOption_unavaliabe_when_new_file_writing_get(ptr);
		}

		// Token: 0x06002C79 RID: 11385 RVA: 0x0005B294 File Offset: 0x00059494
		public static void Csharp2Cpp(RailStreamFileOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailStreamFileOption_open_type_set(ptr, (int)data.open_type);
			RAIL_API_PINVOKE.RailStreamFileOption_unavaliabe_when_new_file_writing_set(ptr, data.unavaliabe_when_new_file_writing);
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x0005B2AE File Offset: 0x000594AE
		public static void Cpp2Csharp(IntPtr ptr, RailSwitchPlayerSelectedZoneResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002C7B RID: 11387 RVA: 0x0005B2B7 File Offset: 0x000594B7
		public static void Csharp2Cpp(RailSwitchPlayerSelectedZoneResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002C7C RID: 11388 RVA: 0x0005B2C0 File Offset: 0x000594C0
		public static void Cpp2Csharp(IntPtr ptr, RailSyncFileOption ret)
		{
			ret.sync_file_not_to_remote = RAIL_API_PINVOKE.RailSyncFileOption_sync_file_not_to_remote_get(ptr);
		}

		// Token: 0x06002C7D RID: 11389 RVA: 0x0005B2CE File Offset: 0x000594CE
		public static void Csharp2Cpp(RailSyncFileOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailSyncFileOption_sync_file_not_to_remote_set(ptr, data.sync_file_not_to_remote);
		}

		// Token: 0x06002C7E RID: 11390 RVA: 0x0005B2DC File Offset: 0x000594DC
		public static void Cpp2Csharp(IntPtr ptr, RailSystemStateChanged ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.state = (RailSystemState)RAIL_API_PINVOKE.RailSystemStateChanged_state_get(ptr);
		}

		// Token: 0x06002C7F RID: 11391 RVA: 0x0005B2F1 File Offset: 0x000594F1
		public static void Csharp2Cpp(RailSystemStateChanged data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailSystemStateChanged_state_set(ptr, (int)data.state);
		}

		// Token: 0x06002C80 RID: 11392 RVA: 0x0005B306 File Offset: 0x00059506
		public static void Cpp2Csharp(IntPtr ptr, RailTextInputImeWindowOption ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailTextInputImeWindowOption_position_get(ptr), ret.position);
			ret.show_rail_ime_window = RAIL_API_PINVOKE.RailTextInputImeWindowOption_show_rail_ime_window_get(ptr);
		}

		// Token: 0x06002C81 RID: 11393 RVA: 0x0005B325 File Offset: 0x00059525
		public static void Csharp2Cpp(RailTextInputImeWindowOption data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.position, RAIL_API_PINVOKE.RailTextInputImeWindowOption_position_get(ptr));
			RAIL_API_PINVOKE.RailTextInputImeWindowOption_show_rail_ime_window_set(ptr, data.show_rail_ime_window);
		}

		// Token: 0x06002C82 RID: 11394 RVA: 0x0005B344 File Offset: 0x00059544
		public static void Cpp2Csharp(IntPtr ptr, RailTextInputResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.content = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailTextInputResult_content_get(ptr));
		}

		// Token: 0x06002C83 RID: 11395 RVA: 0x0005B35E File Offset: 0x0005955E
		public static void Csharp2Cpp(RailTextInputResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailTextInputResult_content_set(ptr, data.content);
		}

		// Token: 0x06002C84 RID: 11396 RVA: 0x0005B374 File Offset: 0x00059574
		public static void Cpp2Csharp(IntPtr ptr, RailTextInputWindowOption ret)
		{
			ret.enable_multi_line_edit = RAIL_API_PINVOKE.RailTextInputWindowOption_enable_multi_line_edit_get(ptr);
			ret.description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailTextInputWindowOption_description_get(ptr));
			ret.position_top = RAIL_API_PINVOKE.RailTextInputWindowOption_position_top_get(ptr);
			ret.position_left = RAIL_API_PINVOKE.RailTextInputWindowOption_position_left_get(ptr);
			ret.caption_text = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailTextInputWindowOption_caption_text_get(ptr));
			ret.show_password_input = RAIL_API_PINVOKE.RailTextInputWindowOption_show_password_input_get(ptr);
			ret.is_min_window = RAIL_API_PINVOKE.RailTextInputWindowOption_is_min_window_get(ptr);
			ret.content_placeholder = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailTextInputWindowOption_content_placeholder_get(ptr));
			ret.auto_cancel = RAIL_API_PINVOKE.RailTextInputWindowOption_auto_cancel_get(ptr);
		}

		// Token: 0x06002C85 RID: 11397 RVA: 0x0005B3FC File Offset: 0x000595FC
		public static void Csharp2Cpp(RailTextInputWindowOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailTextInputWindowOption_enable_multi_line_edit_set(ptr, data.enable_multi_line_edit);
			RAIL_API_PINVOKE.RailTextInputWindowOption_description_set(ptr, data.description);
			RAIL_API_PINVOKE.RailTextInputWindowOption_position_top_set(ptr, data.position_top);
			RAIL_API_PINVOKE.RailTextInputWindowOption_position_left_set(ptr, data.position_left);
			RAIL_API_PINVOKE.RailTextInputWindowOption_caption_text_set(ptr, data.caption_text);
			RAIL_API_PINVOKE.RailTextInputWindowOption_show_password_input_set(ptr, data.show_password_input);
			RAIL_API_PINVOKE.RailTextInputWindowOption_is_min_window_set(ptr, data.is_min_window);
			RAIL_API_PINVOKE.RailTextInputWindowOption_content_placeholder_set(ptr, data.content_placeholder);
			RAIL_API_PINVOKE.RailTextInputWindowOption_auto_cancel_set(ptr, data.auto_cancel);
		}

		// Token: 0x06002C86 RID: 11398 RVA: 0x0005B478 File Offset: 0x00059678
		public static void Cpp2Csharp(IntPtr ptr, RailThirdPartyAccountInfo ret)
		{
			ret.real_name_auth = RAIL_API_PINVOKE.RailThirdPartyAccountInfo_real_name_auth_get(ptr);
			ret.open_id = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailThirdPartyAccountInfo_open_id_get(ptr));
			ret.user_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailThirdPartyAccountInfo_user_name_get(ptr));
			ret.token = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailThirdPartyAccountInfo_token_get(ptr));
			ret.pf = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailThirdPartyAccountInfo_pf_get(ptr));
			ret.token_expire_time = RAIL_API_PINVOKE.RailThirdPartyAccountInfo_token_expire_time_get(ptr);
			ret.error_code = RAIL_API_PINVOKE.RailThirdPartyAccountInfo_error_code_get(ptr);
			ret.error_msg = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailThirdPartyAccountInfo_error_msg_get(ptr));
			ret.channel = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailThirdPartyAccountInfo_channel_get(ptr));
		}

		// Token: 0x06002C87 RID: 11399 RVA: 0x0005B510 File Offset: 0x00059710
		public static void Csharp2Cpp(RailThirdPartyAccountInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_real_name_auth_set(ptr, data.real_name_auth);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_open_id_set(ptr, data.open_id);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_user_name_set(ptr, data.user_name);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_token_set(ptr, data.token);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_pf_set(ptr, data.pf);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_token_expire_time_set(ptr, data.token_expire_time);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_error_code_set(ptr, data.error_code);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_error_msg_set(ptr, data.error_msg);
			RAIL_API_PINVOKE.RailThirdPartyAccountInfo_channel_set(ptr, data.channel);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x0005B589 File Offset: 0x00059789
		public static void Cpp2Csharp(IntPtr ptr, RailThirdPartyAccountLoginOptions ret)
		{
			ret.code = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailThirdPartyAccountLoginOptions_code_get(ptr));
			ret.account_type = (RailPlayerAccountType)RAIL_API_PINVOKE.RailThirdPartyAccountLoginOptions_account_type_get(ptr);
		}

		// Token: 0x06002C89 RID: 11401 RVA: 0x0005B5A8 File Offset: 0x000597A8
		public static void Csharp2Cpp(RailThirdPartyAccountLoginOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailThirdPartyAccountLoginOptions_code_set(ptr, data.code);
			RAIL_API_PINVOKE.RailThirdPartyAccountLoginOptions_account_type_set(ptr, (int)data.account_type);
		}

		// Token: 0x06002C8A RID: 11402 RVA: 0x0005B5C2 File Offset: 0x000597C2
		public static void Cpp2Csharp(IntPtr ptr, RailThirdPartyAccountLoginResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailThirdPartyAccountLoginResult_account_info_get(ptr), ret.account_info);
		}

		// Token: 0x06002C8B RID: 11403 RVA: 0x0005B5DC File Offset: 0x000597DC
		public static void Csharp2Cpp(RailThirdPartyAccountLoginResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.account_info, RAIL_API_PINVOKE.RailThirdPartyAccountLoginResult_account_info_get(ptr));
		}

		// Token: 0x06002C8C RID: 11404 RVA: 0x0005B5F6 File Offset: 0x000597F6
		public static void Cpp2Csharp(IntPtr ptr, RailUserPlayedWith ret)
		{
			ret.user_rich_content = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailUserPlayedWith_user_rich_content_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUserPlayedWith_rail_id_get(ptr), ret.rail_id);
		}

		// Token: 0x06002C8D RID: 11405 RVA: 0x0005B61A File Offset: 0x0005981A
		public static void Csharp2Cpp(RailUserPlayedWith data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailUserPlayedWith_user_rich_content_set(ptr, data.user_rich_content);
			RailConverter.Csharp2Cpp(data.rail_id, RAIL_API_PINVOKE.RailUserPlayedWith_rail_id_get(ptr));
		}

		// Token: 0x06002C8E RID: 11406 RVA: 0x0005B639 File Offset: 0x00059839
		public static void Cpp2Csharp(IntPtr ptr, RailUsersCancelInviteResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersCancelInviteResult_invite_type_get(ptr);
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x0005B64E File Offset: 0x0005984E
		public static void Csharp2Cpp(RailUsersCancelInviteResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersCancelInviteResult_invite_type_set(ptr, (int)data.invite_type);
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x0005B663 File Offset: 0x00059863
		public static void Cpp2Csharp(IntPtr ptr, RailUsersGetInviteDetailResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.command_line = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_command_line_get(ptr));
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_invite_type_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_inviter_id_get(ptr), ret.inviter_id);
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x0005B69A File Offset: 0x0005989A
		public static void Csharp2Cpp(RailUsersGetInviteDetailResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_command_line_set(ptr, data.command_line);
			RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_invite_type_set(ptr, (int)data.invite_type);
			RailConverter.Csharp2Cpp(data.inviter_id, RAIL_API_PINVOKE.RailUsersGetInviteDetailResult_inviter_id_get(ptr));
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x0005B6CC File Offset: 0x000598CC
		public static void Cpp2Csharp(IntPtr ptr, RailUsersGetUserLimitsResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_id_get(ptr), ret.user_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_limits_get(ptr), ret.user_limits);
		}

		// Token: 0x06002C93 RID: 11411 RVA: 0x0005B6F7 File Offset: 0x000598F7
		public static void Csharp2Cpp(RailUsersGetUserLimitsResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.user_id, RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_id_get(ptr));
			RailConverter.Csharp2Cpp(data.user_limits, RAIL_API_PINVOKE.RailUsersGetUserLimitsResult_user_limits_get(ptr));
		}

		// Token: 0x06002C94 RID: 11412 RVA: 0x0005B722 File Offset: 0x00059922
		public static void Cpp2Csharp(IntPtr ptr, RailUsersInfoData ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersInfoData_user_info_list_get(ptr), ret.user_info_list);
		}

		// Token: 0x06002C95 RID: 11413 RVA: 0x0005B73C File Offset: 0x0005993C
		public static void Csharp2Cpp(RailUsersInfoData data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.user_info_list, RAIL_API_PINVOKE.RailUsersInfoData_user_info_list_get(ptr));
		}

		// Token: 0x06002C96 RID: 11414 RVA: 0x0005B756 File Offset: 0x00059956
		public static void Cpp2Csharp(IntPtr ptr, RailUsersInviteJoinGameResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.response_value = (EnumRailUsersInviteResponseType)RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_response_value_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invitee_id_get(ptr), ret.invitee_id);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invite_type_get(ptr);
		}

		// Token: 0x06002C97 RID: 11415 RVA: 0x0005B788 File Offset: 0x00059988
		public static void Csharp2Cpp(RailUsersInviteJoinGameResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_response_value_set(ptr, (int)data.response_value);
			RailConverter.Csharp2Cpp(data.invitee_id, RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invitee_id_get(ptr));
			RAIL_API_PINVOKE.RailUsersInviteJoinGameResult_invite_type_set(ptr, (int)data.invite_type);
		}

		// Token: 0x06002C98 RID: 11416 RVA: 0x0005B7BA File Offset: 0x000599BA
		public static void Cpp2Csharp(IntPtr ptr, RailUsersInviteUsersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.invite_type = (EnumRailUsersInviteType)RAIL_API_PINVOKE.RailUsersInviteUsersResult_invite_type_get(ptr);
		}

		// Token: 0x06002C99 RID: 11417 RVA: 0x0005B7CF File Offset: 0x000599CF
		public static void Csharp2Cpp(RailUsersInviteUsersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RailUsersInviteUsersResult_invite_type_set(ptr, (int)data.invite_type);
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0005B7E4 File Offset: 0x000599E4
		public static void Cpp2Csharp(IntPtr ptr, RailUsersNotifyInviter ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersNotifyInviter_invitee_id_get(ptr), ret.invitee_id);
		}

		// Token: 0x06002C9B RID: 11419 RVA: 0x0005B7FE File Offset: 0x000599FE
		public static void Csharp2Cpp(RailUsersNotifyInviter data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.invitee_id, RAIL_API_PINVOKE.RailUsersNotifyInviter_invitee_id_get(ptr));
		}

		// Token: 0x06002C9C RID: 11420 RVA: 0x0005B818 File Offset: 0x00059A18
		public static void Cpp2Csharp(IntPtr ptr, RailUserSpaceDownloadProgress ret)
		{
			ret.progress = RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_progress_get(ptr);
			ret.total = RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_total_get(ptr);
			ret.speed = RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_speed_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_id_get(ptr), ret.id);
			ret.finidshed = RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_finidshed_get(ptr);
		}

		// Token: 0x06002C9D RID: 11421 RVA: 0x0005B868 File Offset: 0x00059A68
		public static void Csharp2Cpp(RailUserSpaceDownloadProgress data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_progress_set(ptr, data.progress);
			RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_total_set(ptr, data.total);
			RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_speed_set(ptr, data.speed);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_id_get(ptr));
			RAIL_API_PINVOKE.RailUserSpaceDownloadProgress_finidshed_set(ptr, data.finidshed);
		}

		// Token: 0x06002C9E RID: 11422 RVA: 0x0005B8B8 File Offset: 0x00059AB8
		public static void Cpp2Csharp(IntPtr ptr, RailUserSpaceDownloadResult ret)
		{
			ret.err_msg = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailUserSpaceDownloadResult_err_msg_get(ptr));
			ret.finished_bytes = RAIL_API_PINVOKE.RailUserSpaceDownloadResult_finished_bytes_get(ptr);
			ret.finished_files = RAIL_API_PINVOKE.RailUserSpaceDownloadResult_finished_files_get(ptr);
			ret.total_bytes = RAIL_API_PINVOKE.RailUserSpaceDownloadResult_total_bytes_get(ptr);
			ret.total_files = RAIL_API_PINVOKE.RailUserSpaceDownloadResult_total_files_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUserSpaceDownloadResult_id_get(ptr), ret.id);
			ret.err_code = RAIL_API_PINVOKE.RailUserSpaceDownloadResult_err_code_get(ptr);
		}

		// Token: 0x06002C9F RID: 11423 RVA: 0x0005B924 File Offset: 0x00059B24
		public static void Csharp2Cpp(RailUserSpaceDownloadResult data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailUserSpaceDownloadResult_err_msg_set(ptr, data.err_msg);
			RAIL_API_PINVOKE.RailUserSpaceDownloadResult_finished_bytes_set(ptr, data.finished_bytes);
			RAIL_API_PINVOKE.RailUserSpaceDownloadResult_finished_files_set(ptr, data.finished_files);
			RAIL_API_PINVOKE.RailUserSpaceDownloadResult_total_bytes_set(ptr, data.total_bytes);
			RAIL_API_PINVOKE.RailUserSpaceDownloadResult_total_files_set(ptr, data.total_files);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.RailUserSpaceDownloadResult_id_get(ptr));
			RAIL_API_PINVOKE.RailUserSpaceDownloadResult_err_code_set(ptr, data.err_code);
		}

		// Token: 0x06002CA0 RID: 11424 RVA: 0x0005B98A File Offset: 0x00059B8A
		public static void Cpp2Csharp(IntPtr ptr, RailUsersRespondInvitation ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersRespondInvitation_original_invite_option_get(ptr), ret.original_invite_option);
			ret.response = (EnumRailUsersInviteResponseType)RAIL_API_PINVOKE.RailUsersRespondInvitation_response_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailUsersRespondInvitation_inviter_id_get(ptr), ret.inviter_id);
		}

		// Token: 0x06002CA1 RID: 11425 RVA: 0x0005B9C1 File Offset: 0x00059BC1
		public static void Csharp2Cpp(RailUsersRespondInvitation data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.original_invite_option, RAIL_API_PINVOKE.RailUsersRespondInvitation_original_invite_option_get(ptr));
			RAIL_API_PINVOKE.RailUsersRespondInvitation_response_set(ptr, (int)data.response);
			RailConverter.Csharp2Cpp(data.inviter_id, RAIL_API_PINVOKE.RailUsersRespondInvitation_inviter_id_get(ptr));
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x0005B9F8 File Offset: 0x00059BF8
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceCaptureOption ret)
		{
			ret.voice_data_format = (EnumRailVoiceCaptureFormat)RAIL_API_PINVOKE.RailVoiceCaptureOption_voice_data_format_get(ptr);
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x0005BA06 File Offset: 0x00059C06
		public static void Csharp2Cpp(RailVoiceCaptureOption data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceCaptureOption_voice_data_format_set(ptr, (int)data.voice_data_format);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x0005BA14 File Offset: 0x00059C14
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceCaptureSpecification ret)
		{
			ret.channels = (EnumRailVoiceCaptureChannel)RAIL_API_PINVOKE.RailVoiceCaptureSpecification_channels_get(ptr);
			ret.samples_per_second = RAIL_API_PINVOKE.RailVoiceCaptureSpecification_samples_per_second_get(ptr);
			ret.bits_per_sample = RAIL_API_PINVOKE.RailVoiceCaptureSpecification_bits_per_sample_get(ptr);
			ret.capture_format = (EnumRailVoiceCaptureFormat)RAIL_API_PINVOKE.RailVoiceCaptureSpecification_capture_format_get(ptr);
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x0005BA46 File Offset: 0x00059C46
		public static void Csharp2Cpp(RailVoiceCaptureSpecification data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_channels_set(ptr, (int)data.channels);
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_samples_per_second_set(ptr, data.samples_per_second);
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_bits_per_sample_set(ptr, data.bits_per_sample);
			RAIL_API_PINVOKE.RailVoiceCaptureSpecification_capture_format_set(ptr, (int)data.capture_format);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x0005BA78 File Offset: 0x00059C78
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceChannelID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailVoiceChannelID_get_id(ptr);
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x0005BA86 File Offset: 0x00059C86
		public static void Csharp2Cpp(RailVoiceChannelID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceChannelID_set_id(ptr, data.id_);
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x0005BA94 File Offset: 0x00059C94
		public static void Cpp2Csharp(IntPtr ptr, RailVoiceChannelUserSpeakingState ret)
		{
			ret.speaking_limit = (EnumRailVoiceChannelUserSpeakingLimit)RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_speaking_limit_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_user_id_get(ptr), ret.user_id);
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x0005BAB3 File Offset: 0x00059CB3
		public static void Csharp2Cpp(RailVoiceChannelUserSpeakingState data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_speaking_limit_set(ptr, (int)data.speaking_limit);
			RailConverter.Csharp2Cpp(data.user_id, RAIL_API_PINVOKE.RailVoiceChannelUserSpeakingState_user_id_get(ptr));
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x0005BAD2 File Offset: 0x00059CD2
		public static void Cpp2Csharp(IntPtr ptr, RailWindowLayout ret)
		{
			ret.x_margin = RAIL_API_PINVOKE.RailWindowLayout_x_margin_get(ptr);
			ret.y_margin = RAIL_API_PINVOKE.RailWindowLayout_y_margin_get(ptr);
			ret.position_type = (EnumRailNotifyWindowPosition)RAIL_API_PINVOKE.RailWindowLayout_position_type_get(ptr);
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x0005BAF8 File Offset: 0x00059CF8
		public static void Csharp2Cpp(RailWindowLayout data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailWindowLayout_x_margin_set(ptr, data.x_margin);
			RAIL_API_PINVOKE.RailWindowLayout_y_margin_set(ptr, data.y_margin);
			RAIL_API_PINVOKE.RailWindowLayout_position_type_set(ptr, (int)data.position_type);
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x0005BB1E File Offset: 0x00059D1E
		public static void Cpp2Csharp(IntPtr ptr, RailWindowPosition ret)
		{
			ret.position_top = RAIL_API_PINVOKE.RailWindowPosition_position_top_get(ptr);
			ret.position_left = RAIL_API_PINVOKE.RailWindowPosition_position_left_get(ptr);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x0005BB38 File Offset: 0x00059D38
		public static void Csharp2Cpp(RailWindowPosition data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailWindowPosition_position_top_set(ptr, data.position_top);
			RAIL_API_PINVOKE.RailWindowPosition_position_left_set(ptr, data.position_left);
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x0005BB52 File Offset: 0x00059D52
		public static void Cpp2Csharp(IntPtr ptr, RailZoneID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.RailZoneID_get_id(ptr);
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x0005BB60 File Offset: 0x00059D60
		public static void Csharp2Cpp(RailZoneID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RailZoneID_set_id(ptr, data.id_);
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x0005BB6E File Offset: 0x00059D6E
		public static void Cpp2Csharp(IntPtr ptr, ReloadBrowserResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x0005BB77 File Offset: 0x00059D77
		public static void Csharp2Cpp(ReloadBrowserResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x0005BB80 File Offset: 0x00059D80
		public static void Cpp2Csharp(IntPtr ptr, RequestAllAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RequestAllAssetsFinished_assetinfo_list_get(ptr), ret.assetinfo_list);
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x0005BB9A File Offset: 0x00059D9A
		public static void Csharp2Cpp(RequestAllAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.assetinfo_list, RAIL_API_PINVOKE.RequestAllAssetsFinished_assetinfo_list_get(ptr));
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x0005BBB4 File Offset: 0x00059DB4
		public static void Cpp2Csharp(IntPtr ptr, RequestLeaderboardEntryParam ret)
		{
			ret.range_end = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_end_get(ptr);
			ret.range_start = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_start_get(ptr);
			ret.type = (LeaderboardType)RAIL_API_PINVOKE.RequestLeaderboardEntryParam_type_get(ptr);
			ret.user_coordinate = RAIL_API_PINVOKE.RequestLeaderboardEntryParam_user_coordinate_get(ptr);
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x0005BBE6 File Offset: 0x00059DE6
		public static void Csharp2Cpp(RequestLeaderboardEntryParam data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_end_set(ptr, data.range_end);
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_range_start_set(ptr, data.range_start);
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RequestLeaderboardEntryParam_user_coordinate_set(ptr, data.user_coordinate);
		}

		// Token: 0x06002CB6 RID: 11446 RVA: 0x0005BC18 File Offset: 0x00059E18
		public static void Cpp2Csharp(IntPtr ptr, RoomDataReceived ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.data_len = RAIL_API_PINVOKE.RoomDataReceived_data_len_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomDataReceived_remote_peer_get(ptr), ret.remote_peer);
			ret.message_type = RAIL_API_PINVOKE.RoomDataReceived_message_type_get(ptr);
			ret.data_buf = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomDataReceived_data_buf_get(ptr));
		}

		// Token: 0x06002CB7 RID: 11447 RVA: 0x0005BC66 File Offset: 0x00059E66
		public static void Csharp2Cpp(RoomDataReceived data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.RoomDataReceived_data_len_set(ptr, data.data_len);
			RailConverter.Csharp2Cpp(data.remote_peer, RAIL_API_PINVOKE.RoomDataReceived_remote_peer_get(ptr));
			RAIL_API_PINVOKE.RoomDataReceived_message_type_set(ptr, data.message_type);
			RAIL_API_PINVOKE.RoomDataReceived_data_buf_set(ptr, data.data_buf);
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x0005BCA4 File Offset: 0x00059EA4
		public static void Cpp2Csharp(IntPtr ptr, RoomInfo ret)
		{
			ret.has_password = RAIL_API_PINVOKE.RoomInfo_has_password_get(ptr);
			ret.max_members = RAIL_API_PINVOKE.RoomInfo_max_members_get(ptr);
			ret.room_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomInfo_room_name_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfo_game_server_rail_id_get(ptr), ret.game_server_rail_id);
			ret.create_time = RAIL_API_PINVOKE.RoomInfo_create_time_get(ptr);
			ret.current_members = RAIL_API_PINVOKE.RoomInfo_current_members_get(ptr);
			ret.type = (EnumRoomType)RAIL_API_PINVOKE.RoomInfo_type_get(ptr);
			ret.is_joinable = RAIL_API_PINVOKE.RoomInfo_is_joinable_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.RoomInfo_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfo_room_kvs_get(ptr), ret.room_kvs);
			ret.room_tag = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomInfo_room_tag_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfo_owner_id_get(ptr), ret.owner_id);
		}

		// Token: 0x06002CB9 RID: 11449 RVA: 0x0005BD5C File Offset: 0x00059F5C
		public static void Csharp2Cpp(RoomInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfo_has_password_set(ptr, data.has_password);
			RAIL_API_PINVOKE.RoomInfo_max_members_set(ptr, data.max_members);
			RAIL_API_PINVOKE.RoomInfo_room_name_set(ptr, data.room_name);
			RailConverter.Csharp2Cpp(data.game_server_rail_id, RAIL_API_PINVOKE.RoomInfo_game_server_rail_id_get(ptr));
			RAIL_API_PINVOKE.RoomInfo_create_time_set(ptr, data.create_time);
			RAIL_API_PINVOKE.RoomInfo_current_members_set(ptr, data.current_members);
			RAIL_API_PINVOKE.RoomInfo_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RoomInfo_is_joinable_set(ptr, data.is_joinable);
			RAIL_API_PINVOKE.RoomInfo_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.room_kvs, RAIL_API_PINVOKE.RoomInfo_room_kvs_get(ptr));
			RAIL_API_PINVOKE.RoomInfo_room_tag_set(ptr, data.room_tag);
			RailConverter.Csharp2Cpp(data.owner_id, RAIL_API_PINVOKE.RoomInfo_owner_id_get(ptr));
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x0005BE08 File Offset: 0x0005A008
		public static void Cpp2Csharp(IntPtr ptr, RoomInfoListFilter ret)
		{
			ret.filter_friends_in_room = (EnumRailOptionalValue)RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_in_room_get(ptr);
			ret.room_tag = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomInfoListFilter_room_tag_get(ptr));
			ret.available_slot_at_least = RAIL_API_PINVOKE.RoomInfoListFilter_available_slot_at_least_get(ptr);
			ret.filter_password = (EnumRailOptionalValue)RAIL_API_PINVOKE.RoomInfoListFilter_filter_password_get(ptr);
			ret.room_name_contained = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomInfoListFilter_room_name_contained_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomInfoListFilter_key_filters_get(ptr), ret.key_filters);
			ret.filter_friends_owned = (EnumRailOptionalValue)RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_owned_get(ptr);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x0005BE78 File Offset: 0x0005A078
		public static void Csharp2Cpp(RoomInfoListFilter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_in_room_set(ptr, (int)data.filter_friends_in_room);
			RAIL_API_PINVOKE.RoomInfoListFilter_room_tag_set(ptr, data.room_tag);
			RAIL_API_PINVOKE.RoomInfoListFilter_available_slot_at_least_set(ptr, data.available_slot_at_least);
			RAIL_API_PINVOKE.RoomInfoListFilter_filter_password_set(ptr, (int)data.filter_password);
			RAIL_API_PINVOKE.RoomInfoListFilter_room_name_contained_set(ptr, data.room_name_contained);
			RailConverter.Csharp2Cpp(data.key_filters, RAIL_API_PINVOKE.RoomInfoListFilter_key_filters_get(ptr));
			RAIL_API_PINVOKE.RoomInfoListFilter_filter_friends_owned_set(ptr, (int)data.filter_friends_owned);
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x0005BEDE File Offset: 0x0005A0DE
		public static void Cpp2Csharp(IntPtr ptr, RoomInfoListFilterKey ret)
		{
			ret.filter_value = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomInfoListFilterKey_filter_value_get(ptr));
			ret.key_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomInfoListFilterKey_key_name_get(ptr));
			ret.value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.RoomInfoListFilterKey_value_type_get(ptr);
			ret.comparison_type = (EnumRailComparisonType)RAIL_API_PINVOKE.RoomInfoListFilterKey_comparison_type_get(ptr);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x0005BF1A File Offset: 0x0005A11A
		public static void Csharp2Cpp(RoomInfoListFilterKey data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfoListFilterKey_filter_value_set(ptr, data.filter_value);
			RAIL_API_PINVOKE.RoomInfoListFilterKey_key_name_set(ptr, data.key_name);
			RAIL_API_PINVOKE.RoomInfoListFilterKey_value_type_set(ptr, (int)data.value_type);
			RAIL_API_PINVOKE.RoomInfoListFilterKey_comparison_type_set(ptr, (int)data.comparison_type);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0005BF4C File Offset: 0x0005A14C
		public static void Cpp2Csharp(IntPtr ptr, RoomInfoListSorter ret)
		{
			ret.close_to_value = RAIL_API_PINVOKE.RoomInfoListSorter_close_to_value_get(ptr);
			ret.property_key = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomInfoListSorter_property_key_get(ptr));
			ret.property_sort_type = (EnumRailSortType)RAIL_API_PINVOKE.RoomInfoListSorter_property_sort_type_get(ptr);
			ret.property_value_type = (EnumRailPropertyValueType)RAIL_API_PINVOKE.RoomInfoListSorter_property_value_type_get(ptr);
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x0005BF83 File Offset: 0x0005A183
		public static void Csharp2Cpp(RoomInfoListSorter data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomInfoListSorter_close_to_value_set(ptr, data.close_to_value);
			RAIL_API_PINVOKE.RoomInfoListSorter_property_key_set(ptr, data.property_key);
			RAIL_API_PINVOKE.RoomInfoListSorter_property_sort_type_set(ptr, (int)data.property_sort_type);
			RAIL_API_PINVOKE.RoomInfoListSorter_property_value_type_set(ptr, (int)data.property_value_type);
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x0005BFB8 File Offset: 0x0005A1B8
		public static void Cpp2Csharp(IntPtr ptr, RoomMemberInfo ret)
		{
			ret.member_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomMemberInfo_member_name_get(ptr));
			ret.member_index = RAIL_API_PINVOKE.RoomMemberInfo_member_index_get(ptr);
			ret.room_id = RAIL_API_PINVOKE.RoomMemberInfo_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomMemberInfo_member_kvs_get(ptr), ret.member_kvs);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.RoomMemberInfo_member_id_get(ptr), ret.member_id);
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x0005C010 File Offset: 0x0005A210
		public static void Csharp2Cpp(RoomMemberInfo data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomMemberInfo_member_name_set(ptr, data.member_name);
			RAIL_API_PINVOKE.RoomMemberInfo_member_index_set(ptr, data.member_index);
			RAIL_API_PINVOKE.RoomMemberInfo_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.member_kvs, RAIL_API_PINVOKE.RoomMemberInfo_member_kvs_get(ptr));
			RailConverter.Csharp2Cpp(data.member_id, RAIL_API_PINVOKE.RoomMemberInfo_member_id_get(ptr));
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x0005C064 File Offset: 0x0005A264
		public static void Cpp2Csharp(IntPtr ptr, RoomOptions ret)
		{
			ret.max_members = RAIL_API_PINVOKE.RoomOptions_max_members_get(ptr);
			ret.password = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomOptions_password_get(ptr));
			ret.type = (EnumRoomType)RAIL_API_PINVOKE.RoomOptions_type_get(ptr);
			ret.enable_team_voice = RAIL_API_PINVOKE.RoomOptions_enable_team_voice_get(ptr);
			ret.room_tag = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RoomOptions_room_tag_get(ptr));
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0005C0B7 File Offset: 0x0005A2B7
		public static void Csharp2Cpp(RoomOptions data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.RoomOptions_max_members_set(ptr, data.max_members);
			RAIL_API_PINVOKE.RoomOptions_password_set(ptr, data.password);
			RAIL_API_PINVOKE.RoomOptions_type_set(ptr, (int)data.type);
			RAIL_API_PINVOKE.RoomOptions_enable_team_voice_set(ptr, data.enable_team_voice);
			RAIL_API_PINVOKE.RoomOptions_room_tag_set(ptr, data.room_tag);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x0005C0F5 File Offset: 0x0005A2F5
		public static void Cpp2Csharp(IntPtr ptr, ScreenshotRequestInfo ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x0005C0FE File Offset: 0x0005A2FE
		public static void Csharp2Cpp(ScreenshotRequestInfo data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x0005C107 File Offset: 0x0005A307
		public static void Cpp2Csharp(IntPtr ptr, SetGameServerMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.SetGameServerMetadataResult_game_server_id_get(ptr), ret.game_server_id);
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x0005C121 File Offset: 0x0005A321
		public static void Csharp2Cpp(SetGameServerMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.game_server_id, RAIL_API_PINVOKE.SetGameServerMetadataResult_game_server_id_get(ptr));
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x0005C13B File Offset: 0x0005A33B
		public static void Cpp2Csharp(IntPtr ptr, SetMemberMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.SetMemberMetadataResult_room_id_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.SetMemberMetadataResult_member_id_get(ptr), ret.member_id);
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x0005C161 File Offset: 0x0005A361
		public static void Csharp2Cpp(SetMemberMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SetMemberMetadataResult_room_id_set(ptr, data.room_id);
			RailConverter.Csharp2Cpp(data.member_id, RAIL_API_PINVOKE.SetMemberMetadataResult_member_id_get(ptr));
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x0005C187 File Offset: 0x0005A387
		public static void Cpp2Csharp(IntPtr ptr, SetNewRoomOwnerResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0005C190 File Offset: 0x0005A390
		public static void Csharp2Cpp(SetNewRoomOwnerResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x0005C199 File Offset: 0x0005A399
		public static void Cpp2Csharp(IntPtr ptr, SetRoomMaxMemberResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x0005C1A2 File Offset: 0x0005A3A2
		public static void Csharp2Cpp(SetRoomMaxMemberResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0005C1AB File Offset: 0x0005A3AB
		public static void Cpp2Csharp(IntPtr ptr, SetRoomMetadataResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_id = RAIL_API_PINVOKE.SetRoomMetadataResult_room_id_get(ptr);
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x0005C1C0 File Offset: 0x0005A3C0
		public static void Csharp2Cpp(SetRoomMetadataResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SetRoomMetadataResult_room_id_set(ptr, data.room_id);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0005C1D5 File Offset: 0x0005A3D5
		public static void Cpp2Csharp(IntPtr ptr, SetRoomTagResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x0005C1DE File Offset: 0x0005A3DE
		public static void Csharp2Cpp(SetRoomTagResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0005C1E7 File Offset: 0x0005A3E7
		public static void Cpp2Csharp(IntPtr ptr, SetRoomTypeResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.room_type = (EnumRoomType)RAIL_API_PINVOKE.SetRoomTypeResult_room_type_get(ptr);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x0005C1FC File Offset: 0x0005A3FC
		public static void Csharp2Cpp(SetRoomTypeResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SetRoomTypeResult_room_type_set(ptr, (int)data.room_type);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x0005C211 File Offset: 0x0005A411
		public static void Cpp2Csharp(IntPtr ptr, ShareStorageToSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.ShareStorageToSpaceWorkResult_space_work_id_get(ptr), ret.space_work_id);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x0005C22B File Offset: 0x0005A42B
		public static void Csharp2Cpp(ShareStorageToSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.space_work_id, RAIL_API_PINVOKE.ShareStorageToSpaceWorkResult_space_work_id_get(ptr));
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x0005C245 File Offset: 0x0005A445
		public static void Cpp2Csharp(IntPtr ptr, ShowFloatingWindowResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.window_type = (EnumRailWindowType)RAIL_API_PINVOKE.ShowFloatingWindowResult_window_type_get(ptr);
			ret.is_show = RAIL_API_PINVOKE.ShowFloatingWindowResult_is_show_get(ptr);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x0005C266 File Offset: 0x0005A466
		public static void Csharp2Cpp(ShowFloatingWindowResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ShowFloatingWindowResult_window_type_set(ptr, (int)data.window_type);
			RAIL_API_PINVOKE.ShowFloatingWindowResult_is_show_set(ptr, data.is_show);
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x0005C287 File Offset: 0x0005A487
		public static void Cpp2Csharp(IntPtr ptr, ShowNotifyWindow ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.window_type = (EnumRailNotifyWindowType)RAIL_API_PINVOKE.ShowNotifyWindow_window_type_get(ptr);
			ret.json_content = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.ShowNotifyWindow_json_content_get(ptr));
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x0005C2AD File Offset: 0x0005A4AD
		public static void Csharp2Cpp(ShowNotifyWindow data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.ShowNotifyWindow_window_type_set(ptr, (int)data.window_type);
			RAIL_API_PINVOKE.ShowNotifyWindow_json_content_set(ptr, data.json_content);
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x0005C2CE File Offset: 0x0005A4CE
		public static void Cpp2Csharp(IntPtr ptr, SpaceWorkID ret)
		{
			ret.id_ = RAIL_API_PINVOKE.SpaceWorkID_get_id(ptr);
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x0005C2DC File Offset: 0x0005A4DC
		public static void Csharp2Cpp(SpaceWorkID data, IntPtr ptr)
		{
			RAIL_API_PINVOKE.SpaceWorkID_set_id(ptr, data.id_);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x0005C2EA File Offset: 0x0005A4EA
		public static void Cpp2Csharp(IntPtr ptr, SplitAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.source_asset = RAIL_API_PINVOKE.SplitAssetsFinished_source_asset_get(ptr);
			ret.to_quantity = RAIL_API_PINVOKE.SplitAssetsFinished_to_quantity_get(ptr);
			ret.new_asset_id = RAIL_API_PINVOKE.SplitAssetsFinished_new_asset_id_get(ptr);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x0005C317 File Offset: 0x0005A517
		public static void Csharp2Cpp(SplitAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SplitAssetsFinished_source_asset_set(ptr, data.source_asset);
			RAIL_API_PINVOKE.SplitAssetsFinished_to_quantity_set(ptr, data.to_quantity);
			RAIL_API_PINVOKE.SplitAssetsFinished_new_asset_id_set(ptr, data.new_asset_id);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x0005C344 File Offset: 0x0005A544
		public static void Cpp2Csharp(IntPtr ptr, SplitAssetsToFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.source_asset = RAIL_API_PINVOKE.SplitAssetsToFinished_source_asset_get(ptr);
			ret.to_quantity = RAIL_API_PINVOKE.SplitAssetsToFinished_to_quantity_get(ptr);
			ret.split_to_asset_id = RAIL_API_PINVOKE.SplitAssetsToFinished_split_to_asset_id_get(ptr);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x0005C371 File Offset: 0x0005A571
		public static void Csharp2Cpp(SplitAssetsToFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.SplitAssetsToFinished_source_asset_set(ptr, data.source_asset);
			RAIL_API_PINVOKE.SplitAssetsToFinished_to_quantity_set(ptr, data.to_quantity);
			RAIL_API_PINVOKE.SplitAssetsToFinished_split_to_asset_id_set(ptr, data.split_to_asset_id);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x0005C39E File Offset: 0x0005A59E
		public static void Cpp2Csharp(IntPtr ptr, StartConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.asset_id = RAIL_API_PINVOKE.StartConsumeAssetsFinished_asset_id_get(ptr);
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x0005C3B3 File Offset: 0x0005A5B3
		public static void Csharp2Cpp(StartConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.StartConsumeAssetsFinished_asset_id_set(ptr, data.asset_id);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x0005C3C8 File Offset: 0x0005A5C8
		public static void Cpp2Csharp(IntPtr ptr, StartSessionWithPlayerResponse ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.StartSessionWithPlayerResponse_remote_rail_id_get(ptr), ret.remote_rail_id);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x0005C3E2 File Offset: 0x0005A5E2
		public static void Csharp2Cpp(StartSessionWithPlayerResponse data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.remote_rail_id, RAIL_API_PINVOKE.StartSessionWithPlayerResponse_remote_rail_id_get(ptr));
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x0005C3FC File Offset: 0x0005A5FC
		public static void Cpp2Csharp(IntPtr ptr, SyncSpaceWorkResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.SyncSpaceWorkResult_id_get(ptr), ret.id);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x0005C416 File Offset: 0x0005A616
		public static void Csharp2Cpp(SyncSpaceWorkResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.id, RAIL_API_PINVOKE.SyncSpaceWorkResult_id_get(ptr));
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x0005C430 File Offset: 0x0005A630
		public static void Cpp2Csharp(IntPtr ptr, TakeScreenshotResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.thumbnail_file_size = RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_file_size_get(ptr);
			ret.thumbnail_filepath = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_filepath_get(ptr));
			ret.image_file_size = RAIL_API_PINVOKE.TakeScreenshotResult_image_file_size_get(ptr);
			ret.image_file_path = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.TakeScreenshotResult_image_file_path_get(ptr));
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x0005C47E File Offset: 0x0005A67E
		public static void Csharp2Cpp(TakeScreenshotResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_file_size_set(ptr, data.thumbnail_file_size);
			RAIL_API_PINVOKE.TakeScreenshotResult_thumbnail_filepath_set(ptr, data.thumbnail_filepath);
			RAIL_API_PINVOKE.TakeScreenshotResult_image_file_size_set(ptr, data.image_file_size);
			RAIL_API_PINVOKE.TakeScreenshotResult_image_file_path_set(ptr, data.image_file_path);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x0005C4B7 File Offset: 0x0005A6B7
		public static void Cpp2Csharp(IntPtr ptr, UpdateAssetsPropertyFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.UpdateAssetsPropertyFinished_asset_property_list_get(ptr), ret.asset_property_list);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x0005C4D1 File Offset: 0x0005A6D1
		public static void Csharp2Cpp(UpdateAssetsPropertyFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.asset_property_list, RAIL_API_PINVOKE.UpdateAssetsPropertyFinished_asset_property_list_get(ptr));
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0005C4EB File Offset: 0x0005A6EB
		public static void Cpp2Csharp(IntPtr ptr, UpdateConsumeAssetsFinished ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.asset_id = RAIL_API_PINVOKE.UpdateConsumeAssetsFinished_asset_id_get(ptr);
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x0005C500 File Offset: 0x0005A700
		public static void Csharp2Cpp(UpdateConsumeAssetsFinished data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.UpdateConsumeAssetsFinished_asset_id_set(ptr, data.asset_id);
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x0005C515 File Offset: 0x0005A715
		public static void Cpp2Csharp(IntPtr ptr, UploadLeaderboardParam ret)
		{
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.UploadLeaderboardParam_data_get(ptr), ret.data);
			ret.type = (LeaderboardUploadType)RAIL_API_PINVOKE.UploadLeaderboardParam_type_get(ptr);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x0005C534 File Offset: 0x0005A734
		public static void Csharp2Cpp(UploadLeaderboardParam data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data.data, RAIL_API_PINVOKE.UploadLeaderboardParam_data_get(ptr));
			RAIL_API_PINVOKE.UploadLeaderboardParam_type_set(ptr, (int)data.type);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x0005C553 File Offset: 0x0005A753
		public static void Cpp2Csharp(IntPtr ptr, UserSpaceDownloadProgress ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.UserSpaceDownloadProgress_progress_get(ptr), ret.progress);
			ret.total_progress = RAIL_API_PINVOKE.UserSpaceDownloadProgress_total_progress_get(ptr);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x0005C579 File Offset: 0x0005A779
		public static void Csharp2Cpp(UserSpaceDownloadProgress data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.progress, RAIL_API_PINVOKE.UserSpaceDownloadProgress_progress_get(ptr));
			RAIL_API_PINVOKE.UserSpaceDownloadProgress_total_progress_set(ptr, data.total_progress);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x0005C59F File Offset: 0x0005A79F
		public static void Cpp2Csharp(IntPtr ptr, UserSpaceDownloadResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.total_results = RAIL_API_PINVOKE.UserSpaceDownloadResult_total_results_get(ptr);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.UserSpaceDownloadResult_results_get(ptr), ret.results);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x0005C5C5 File Offset: 0x0005A7C5
		public static void Csharp2Cpp(UserSpaceDownloadResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.UserSpaceDownloadResult_total_results_set(ptr, data.total_results);
			RailConverter.Csharp2Cpp(data.results, RAIL_API_PINVOKE.UserSpaceDownloadResult_results_get(ptr));
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x0005C5EB File Offset: 0x0005A7EB
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelAddUsersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelAddUsersResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelAddUsersResult_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelAddUsersResult_failed_ids_get(ptr), ret.failed_ids);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x0005C627 File Offset: 0x0005A827
		public static void Csharp2Cpp(VoiceChannelAddUsersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.VoiceChannelAddUsersResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelAddUsersResult_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.failed_ids, RAIL_API_PINVOKE.VoiceChannelAddUsersResult_failed_ids_get(ptr));
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x0005C663 File Offset: 0x0005A863
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelInviteEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.channel_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.VoiceChannelInviteEvent_channel_name_get(ptr));
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelInviteEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			ret.inviter_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.VoiceChannelInviteEvent_inviter_name_get(ptr));
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x0005C69F File Offset: 0x0005A89F
		public static void Csharp2Cpp(VoiceChannelInviteEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.VoiceChannelInviteEvent_channel_name_set(ptr, data.channel_name);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelInviteEvent_voice_channel_id_get(ptr));
			RAIL_API_PINVOKE.VoiceChannelInviteEvent_inviter_name_set(ptr, data.inviter_name);
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x0005C6D1 File Offset: 0x0005A8D1
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelMemeberChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_member_ids_get(ptr), ret.member_ids);
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x0005C6FC File Offset: 0x0005A8FC
		public static void Csharp2Cpp(VoiceChannelMemeberChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.member_ids, RAIL_API_PINVOKE.VoiceChannelMemeberChangedEvent_member_ids_get(ptr));
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0005C727 File Offset: 0x0005A927
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelPushToTalkKeyChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.push_to_talk_hot_key = RAIL_API_PINVOKE.VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_get(ptr);
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x0005C73C File Offset: 0x0005A93C
		public static void Csharp2Cpp(VoiceChannelPushToTalkKeyChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.VoiceChannelPushToTalkKeyChangedEvent_push_to_talk_hot_key_set(ptr, data.push_to_talk_hot_key);
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x0005C751 File Offset: 0x0005A951
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelRemoveUsersResult ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_success_ids_get(ptr), ret.success_ids);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_failed_ids_get(ptr), ret.failed_ids);
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x0005C78D File Offset: 0x0005A98D
		public static void Csharp2Cpp(VoiceChannelRemoveUsersResult data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.success_ids, RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_success_ids_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.failed_ids, RAIL_API_PINVOKE.VoiceChannelRemoveUsersResult_failed_ids_get(ptr));
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x0005C7C9 File Offset: 0x0005A9C9
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelSpeakingUsersChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_speaking_users_get(ptr), ret.speaking_users);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_get(ptr), ret.not_speaking_users);
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x0005C805 File Offset: 0x0005AA05
		public static void Csharp2Cpp(VoiceChannelSpeakingUsersChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.speaking_users, RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_speaking_users_get(ptr));
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.not_speaking_users, RAIL_API_PINVOKE.VoiceChannelSpeakingUsersChangedEvent_not_speaking_users_get(ptr));
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x0005C841 File Offset: 0x0005AA41
		public static void Cpp2Csharp(IntPtr ptr, VoiceChannelUsersSpeakingStateChangedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_get(ptr), ret.voice_channel_id);
			RailConverter.Cpp2Csharp(RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_get(ptr), ret.users_speaking_state);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x0005C86C File Offset: 0x0005AA6C
		public static void Csharp2Cpp(VoiceChannelUsersSpeakingStateChangedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RailConverter.Csharp2Cpp(data.voice_channel_id, RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_voice_channel_id_get(ptr));
			RailConverter.Csharp2Cpp(data.users_speaking_state, RAIL_API_PINVOKE.VoiceChannelUsersSpeakingStateChangedEvent_users_speaking_state_get(ptr));
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x0005C897 File Offset: 0x0005AA97
		public static void Cpp2Csharp(IntPtr ptr, VoiceDataCapturedEvent ret)
		{
			RailConverter.Cpp2Csharp(ptr, ret);
			ret.is_last_package = RAIL_API_PINVOKE.VoiceDataCapturedEvent_is_last_package_get(ptr);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x0005C8AC File Offset: 0x0005AAAC
		public static void Csharp2Cpp(VoiceDataCapturedEvent data, IntPtr ptr)
		{
			RailConverter.Csharp2Cpp(data, ptr);
			RAIL_API_PINVOKE.VoiceDataCapturedEvent_is_last_package_set(ptr, data.is_last_package);
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x0005C8C4 File Offset: 0x0005AAC4
		public static void Csharp2Cpp(List<GameServerPlayerInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerPlayerInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerPlayerInfo(intPtr);
			}
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x0005C90C File Offset: 0x0005AB0C
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerPlayerInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerPlayerInfo_Item(ptr, (uint)num2);
				GameServerPlayerInfo gameServerPlayerInfo = new GameServerPlayerInfo();
				RailConverter.Cpp2Csharp(intPtr, gameServerPlayerInfo);
				ret.Add(gameServerPlayerInfo);
				num2++;
			}
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x0005C950 File Offset: 0x0005AB50
		public static void Csharp2Cpp(List<RailSpaceWorkDescriptor> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSpaceWorkDescriptor__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSpaceWorkDescriptor(intPtr);
			}
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x0005C998 File Offset: 0x0005AB98
		public static void Cpp2Csharp(IntPtr ptr, List<RailSpaceWorkDescriptor> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSpaceWorkDescriptor_Item(ptr, (uint)num2);
				RailSpaceWorkDescriptor railSpaceWorkDescriptor = new RailSpaceWorkDescriptor();
				RailConverter.Cpp2Csharp(intPtr, railSpaceWorkDescriptor);
				ret.Add(railSpaceWorkDescriptor);
				num2++;
			}
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x0005C9DC File Offset: 0x0005ABDC
		public static void Csharp2Cpp(List<RailDlcOwned> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailDlcOwned_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailDlcOwned__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailDlcOwned_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailDlcOwned(intPtr);
			}
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x0005CA24 File Offset: 0x0005AC24
		public static void Cpp2Csharp(IntPtr ptr, List<RailDlcOwned> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailDlcOwned_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailDlcOwned_Item(ptr, (uint)num2);
				RailDlcOwned railDlcOwned = new RailDlcOwned();
				RailConverter.Cpp2Csharp(intPtr, railDlcOwned);
				ret.Add(railDlcOwned);
				num2++;
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x0005CA68 File Offset: 0x0005AC68
		public static void Csharp2Cpp(List<RailSmallObjectDownloadInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSmallObjectDownloadInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSmallObjectDownloadInfo(intPtr);
			}
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x0005CAB0 File Offset: 0x0005ACB0
		public static void Cpp2Csharp(IntPtr ptr, List<RailSmallObjectDownloadInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSmallObjectDownloadInfo_Item(ptr, (uint)num2);
				RailSmallObjectDownloadInfo railSmallObjectDownloadInfo = new RailSmallObjectDownloadInfo();
				RailConverter.Cpp2Csharp(intPtr, railSmallObjectDownloadInfo);
				ret.Add(railSmallObjectDownloadInfo);
				num2++;
			}
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x0005CAF4 File Offset: 0x0005ACF4
		public static void Csharp2Cpp(List<RailFriendMetadata> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailFriendMetadata_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailFriendMetadata__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailFriendMetadata_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailFriendMetadata(intPtr);
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x0005CB3C File Offset: 0x0005AD3C
		public static void Cpp2Csharp(IntPtr ptr, List<RailFriendMetadata> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailFriendMetadata_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailFriendMetadata_Item(ptr, (uint)num2);
				RailFriendMetadata railFriendMetadata = new RailFriendMetadata();
				RailConverter.Cpp2Csharp(intPtr, railFriendMetadata);
				ret.Add(railFriendMetadata);
				num2++;
			}
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x0005CB80 File Offset: 0x0005AD80
		public static void Csharp2Cpp(List<RailGeneratedAssetItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailGeneratedAssetItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailGeneratedAssetItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailGeneratedAssetItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailGeneratedAssetItem(intPtr);
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0005CBC8 File Offset: 0x0005ADC8
		public static void Cpp2Csharp(IntPtr ptr, List<RailGeneratedAssetItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailGeneratedAssetItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailGeneratedAssetItem_Item(ptr, (uint)num2);
				RailGeneratedAssetItem railGeneratedAssetItem = new RailGeneratedAssetItem();
				RailConverter.Cpp2Csharp(intPtr, railGeneratedAssetItem);
				ret.Add(railGeneratedAssetItem);
				num2++;
			}
		}

		// Token: 0x06002D0E RID: 11534 RVA: 0x0005CC0C File Offset: 0x0005AE0C
		public static void Csharp2Cpp(List<RailPlayedWithFriendsTimeItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailPlayedWithFriendsTimeItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailPlayedWithFriendsTimeItem(intPtr);
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x0005CC54 File Offset: 0x0005AE54
		public static void Cpp2Csharp(IntPtr ptr, List<RailPlayedWithFriendsTimeItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsTimeItem_Item(ptr, (uint)num2);
				RailPlayedWithFriendsTimeItem railPlayedWithFriendsTimeItem = new RailPlayedWithFriendsTimeItem();
				RailConverter.Cpp2Csharp(intPtr, railPlayedWithFriendsTimeItem);
				ret.Add(railPlayedWithFriendsTimeItem);
				num2++;
			}
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0005CC98 File Offset: 0x0005AE98
		public static void Csharp2Cpp(List<RoomInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfo(intPtr);
			}
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x0005CCE0 File Offset: 0x0005AEE0
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfo_Item(ptr, (uint)num2);
				RoomInfo roomInfo = new RoomInfo();
				RailConverter.Cpp2Csharp(intPtr, roomInfo);
				ret.Add(roomInfo);
				num2++;
			}
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x0005CD24 File Offset: 0x0005AF24
		public static void Csharp2Cpp(List<GameServerInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerInfo(intPtr);
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x0005CD6C File Offset: 0x0005AF6C
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerInfo_Item(ptr, (uint)num2);
				GameServerInfo gameServerInfo = new GameServerInfo();
				RailConverter.Cpp2Csharp(intPtr, gameServerInfo);
				ret.Add(gameServerInfo);
				num2++;
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x0005CDB0 File Offset: 0x0005AFB0
		public static void Csharp2Cpp(List<RailGameDefineGamePlayingState> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailGameDefineGamePlayingState__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailGameDefineGamePlayingState(intPtr);
			}
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x0005CDF8 File Offset: 0x0005AFF8
		public static void Cpp2Csharp(IntPtr ptr, List<RailGameDefineGamePlayingState> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailGameDefineGamePlayingState_Item(ptr, (uint)num2);
				RailGameDefineGamePlayingState railGameDefineGamePlayingState = new RailGameDefineGamePlayingState();
				RailConverter.Cpp2Csharp(intPtr, railGameDefineGamePlayingState);
				ret.Add(railGameDefineGamePlayingState);
				num2++;
			}
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x0005CE3C File Offset: 0x0005B03C
		public static void Csharp2Cpp(List<RailUserPlayedWith> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailUserPlayedWith__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailUserPlayedWith(intPtr);
			}
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x0005CE84 File Offset: 0x0005B084
		public static void Cpp2Csharp(IntPtr ptr, List<RailUserPlayedWith> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailUserPlayedWith_Item(ptr, (uint)num2);
				RailUserPlayedWith railUserPlayedWith = new RailUserPlayedWith();
				RailConverter.Cpp2Csharp(intPtr, railUserPlayedWith);
				ret.Add(railUserPlayedWith);
				num2++;
			}
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x0005CEC8 File Offset: 0x0005B0C8
		public static void Csharp2Cpp(List<RailQuerySpaceWorkInfoResult> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailQuerySpaceWorkInfoResult_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailQuerySpaceWorkInfoResult__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailQuerySpaceWorkInfoResult_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailQuerySpaceWorkInfoResult(intPtr);
			}
		}

		// Token: 0x06002D19 RID: 11545 RVA: 0x0005CF10 File Offset: 0x0005B110
		public static void Cpp2Csharp(IntPtr ptr, List<RailQuerySpaceWorkInfoResult> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailQuerySpaceWorkInfoResult_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailQuerySpaceWorkInfoResult_Item(ptr, (uint)num2);
				RailQuerySpaceWorkInfoResult railQuerySpaceWorkInfoResult = new RailQuerySpaceWorkInfoResult();
				RailConverter.Cpp2Csharp(intPtr, railQuerySpaceWorkInfoResult);
				ret.Add(railQuerySpaceWorkInfoResult);
				num2++;
			}
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x0005CF54 File Offset: 0x0005B154
		public static void Csharp2Cpp(List<RailID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
		}

		// Token: 0x06002D1B RID: 11547 RVA: 0x0005CF9C File Offset: 0x0005B19C
		public static void Cpp2Csharp(IntPtr ptr, List<RailID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailID_Item(ptr, (uint)num2);
				RailID railID = new RailID();
				RailConverter.Cpp2Csharp(intPtr, railID);
				ret.Add(railID);
				num2++;
			}
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x0005CFE0 File Offset: 0x0005B1E0
		public static void Csharp2Cpp(List<RailDlcID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailDlcID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailDlcID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailDlcID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailDlcID(intPtr);
			}
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0005D028 File Offset: 0x0005B228
		public static void Cpp2Csharp(IntPtr ptr, List<RailDlcID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailDlcID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailDlcID_Item(ptr, (uint)num2);
				RailDlcID railDlcID = new RailDlcID();
				RailConverter.Cpp2Csharp(intPtr, railDlcID);
				ret.Add(railDlcID);
				num2++;
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x0005D06C File Offset: 0x0005B26C
		public static void Csharp2Cpp(List<GameServerListSorter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerListSorter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerListSorter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerListSorter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerListSorter(intPtr);
			}
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x0005D0B4 File Offset: 0x0005B2B4
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerListSorter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerListSorter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerListSorter_Item(ptr, (uint)num2);
				GameServerListSorter gameServerListSorter = new GameServerListSorter();
				RailConverter.Cpp2Csharp(intPtr, gameServerListSorter);
				ret.Add(gameServerListSorter);
				num2++;
			}
		}

		// Token: 0x06002D20 RID: 11552 RVA: 0x0005D0F8 File Offset: 0x0005B2F8
		public static void Csharp2Cpp(List<RoomInfoListFilterKey> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfoListFilterKey__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfoListFilterKey(intPtr);
			}
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x0005D140 File Offset: 0x0005B340
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListFilterKey> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfoListFilterKey_Item(ptr, (uint)num2);
				RoomInfoListFilterKey roomInfoListFilterKey = new RoomInfoListFilterKey();
				RailConverter.Cpp2Csharp(intPtr, roomInfoListFilterKey);
				ret.Add(roomInfoListFilterKey);
				num2++;
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x0005D184 File Offset: 0x0005B384
		public static void Csharp2Cpp(List<RailPurchaseProductInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailPurchaseProductInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailPurchaseProductInfo(intPtr);
			}
		}

		// Token: 0x06002D23 RID: 11555 RVA: 0x0005D1CC File Offset: 0x0005B3CC
		public static void Cpp2Csharp(IntPtr ptr, List<RailPurchaseProductInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailPurchaseProductInfo_Item(ptr, (uint)num2);
				RailPurchaseProductInfo railPurchaseProductInfo = new RailPurchaseProductInfo();
				RailConverter.Cpp2Csharp(intPtr, railPurchaseProductInfo);
				ret.Add(railPurchaseProductInfo);
				num2++;
			}
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x0005D210 File Offset: 0x0005B410
		public static void Csharp2Cpp(List<EnumRailWorkFileClass> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
				RAIL_API_PINVOKE.SetInt(intPtr, (int)ret[i]);
				RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x0005D258 File Offset: 0x0005B458
		public static void Cpp2Csharp(IntPtr ptr, List<EnumRailWorkFileClass> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayEnumRailWorkFileClass_Item(ptr, (uint)num2);
				ret.Add((EnumRailWorkFileClass)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x0005D294 File Offset: 0x0005B494
		public static void Csharp2Cpp(List<byte> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayuint8_t_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				RAIL_API_PINVOKE.RailArrayuint8_t_push_back(ptr, ret[i]);
			}
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x0005D2C8 File Offset: 0x0005B4C8
		public static void Cpp2Csharp(IntPtr ptr, List<byte> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayuint8_t_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayuint8_t_Item(ptr, (uint)num2);
				ret.Add((byte)RAIL_API_PINVOKE.GetInt8(intPtr));
				num2++;
			}
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x0005D308 File Offset: 0x0005B508
		public static void Csharp2Cpp(List<RailGameID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailGameID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailGameID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailGameID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x0005D350 File Offset: 0x0005B550
		public static void Cpp2Csharp(IntPtr ptr, List<RailGameID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailGameID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailGameID_Item(ptr, (uint)num2);
				RailGameID railGameID = new RailGameID();
				RailConverter.Cpp2Csharp(intPtr, railGameID);
				ret.Add(railGameID);
				num2++;
			}
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x0005D394 File Offset: 0x0005B594
		public static void Csharp2Cpp(List<RailVoiceChannelUserSpeakingState> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailVoiceChannelUserSpeakingState__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailVoiceChannelUserSpeakingState(intPtr);
			}
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x0005D3DC File Offset: 0x0005B5DC
		public static void Cpp2Csharp(IntPtr ptr, List<RailVoiceChannelUserSpeakingState> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailVoiceChannelUserSpeakingState_Item(ptr, (uint)num2);
				RailVoiceChannelUserSpeakingState railVoiceChannelUserSpeakingState = new RailVoiceChannelUserSpeakingState();
				RailConverter.Cpp2Csharp(intPtr, railVoiceChannelUserSpeakingState);
				ret.Add(railVoiceChannelUserSpeakingState);
				num2++;
			}
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x0005D420 File Offset: 0x0005B620
		public static void Csharp2Cpp(List<RailFriendPlayedGameInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailFriendPlayedGameInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailFriendPlayedGameInfo(intPtr);
			}
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x0005D468 File Offset: 0x0005B668
		public static void Cpp2Csharp(IntPtr ptr, List<RailFriendPlayedGameInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailFriendPlayedGameInfo_Item(ptr, (uint)num2);
				RailFriendPlayedGameInfo railFriendPlayedGameInfo = new RailFriendPlayedGameInfo();
				RailConverter.Cpp2Csharp(intPtr, railFriendPlayedGameInfo);
				ret.Add(railFriendPlayedGameInfo);
				num2++;
			}
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x0005D4AC File Offset: 0x0005B6AC
		public static void Csharp2Cpp(List<RailSmallObjectState> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSmallObjectState_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSmallObjectState__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSmallObjectState_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSmallObjectState(intPtr);
			}
		}

		// Token: 0x06002D2F RID: 11567 RVA: 0x0005D4F4 File Offset: 0x0005B6F4
		public static void Cpp2Csharp(IntPtr ptr, List<RailSmallObjectState> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSmallObjectState_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSmallObjectState_Item(ptr, (uint)num2);
				RailSmallObjectState railSmallObjectState = new RailSmallObjectState();
				RailConverter.Cpp2Csharp(intPtr, railSmallObjectState);
				ret.Add(railSmallObjectState);
				num2++;
			}
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x0005D538 File Offset: 0x0005B738
		public static void Csharp2Cpp(List<RailSpaceWorkVoteDetail> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailSpaceWorkVoteDetail__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailSpaceWorkVoteDetail(intPtr);
			}
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x0005D580 File Offset: 0x0005B780
		public static void Cpp2Csharp(IntPtr ptr, List<RailSpaceWorkVoteDetail> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailSpaceWorkVoteDetail_Item(ptr, (uint)num2);
				RailSpaceWorkVoteDetail railSpaceWorkVoteDetail = new RailSpaceWorkVoteDetail();
				RailConverter.Cpp2Csharp(intPtr, railSpaceWorkVoteDetail);
				ret.Add(railSpaceWorkVoteDetail);
				num2++;
			}
		}

		// Token: 0x06002D32 RID: 11570 RVA: 0x0005D5C4 File Offset: 0x0005B7C4
		public static void Csharp2Cpp(List<RailKeyValue> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailKeyValue_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailKeyValue();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailKeyValue_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailKeyValue(intPtr);
			}
		}

		// Token: 0x06002D33 RID: 11571 RVA: 0x0005D60C File Offset: 0x0005B80C
		public static void Cpp2Csharp(IntPtr ptr, List<RailKeyValue> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailKeyValue_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailKeyValue_Item(ptr, (uint)num2);
				RailKeyValue railKeyValue = new RailKeyValue();
				RailConverter.Cpp2Csharp(intPtr, railKeyValue);
				ret.Add(railKeyValue);
				num2++;
			}
		}

		// Token: 0x06002D34 RID: 11572 RVA: 0x0005D650 File Offset: 0x0005B850
		public static void Csharp2Cpp(List<RailZoneID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailZoneID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailZoneID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailZoneID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailZoneID(intPtr);
			}
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x0005D698 File Offset: 0x0005B898
		public static void Cpp2Csharp(IntPtr ptr, List<RailZoneID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailZoneID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailZoneID_Item(ptr, (uint)num2);
				RailZoneID railZoneID = new RailZoneID();
				RailConverter.Cpp2Csharp(intPtr, railZoneID);
				ret.Add(railZoneID);
				num2++;
			}
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x0005D6DC File Offset: 0x0005B8DC
		public static void Csharp2Cpp(List<RailFriendInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailFriendInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailFriendInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailFriendInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailFriendInfo(intPtr);
			}
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x0005D724 File Offset: 0x0005B924
		public static void Cpp2Csharp(IntPtr ptr, List<RailFriendInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailFriendInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailFriendInfo_Item(ptr, (uint)num2);
				RailFriendInfo railFriendInfo = new RailFriendInfo();
				RailConverter.Cpp2Csharp(intPtr, railFriendInfo);
				ret.Add(railFriendInfo);
				num2++;
			}
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x0005D768 File Offset: 0x0005B968
		public static void Csharp2Cpp(List<RailUserSpaceDownloadResult> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadResult_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailUserSpaceDownloadResult__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadResult_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailUserSpaceDownloadResult(intPtr);
			}
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x0005D7B0 File Offset: 0x0005B9B0
		public static void Cpp2Csharp(IntPtr ptr, List<RailUserSpaceDownloadResult> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadResult_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadResult_Item(ptr, (uint)num2);
				RailUserSpaceDownloadResult railUserSpaceDownloadResult = new RailUserSpaceDownloadResult();
				RailConverter.Cpp2Csharp(intPtr, railUserSpaceDownloadResult);
				ret.Add(railUserSpaceDownloadResult);
				num2++;
			}
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0005D7F4 File Offset: 0x0005B9F4
		public static void Csharp2Cpp(List<string> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailString_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
				RAIL_API_PINVOKE.RailString_SetValue(intPtr, ret[i]);
				RAIL_API_PINVOKE.RailArrayRailString_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x0005D83C File Offset: 0x0005BA3C
		public static void Cpp2Csharp(IntPtr ptr, List<string> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailString_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailString_Item(ptr, (uint)num2);
				ret.Add(UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr)));
				num2++;
			}
		}

		// Token: 0x06002D3C RID: 11580 RVA: 0x0005D880 File Offset: 0x0005BA80
		public static void Csharp2Cpp(List<RailKeyValueResult> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailKeyValueResult_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailKeyValueResult__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailKeyValueResult_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailKeyValueResult(intPtr);
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x0005D8C8 File Offset: 0x0005BAC8
		public static void Cpp2Csharp(IntPtr ptr, List<RailKeyValueResult> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailKeyValueResult_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailKeyValueResult_Item(ptr, (uint)num2);
				RailKeyValueResult railKeyValueResult = new RailKeyValueResult();
				RailConverter.Cpp2Csharp(intPtr, railKeyValueResult);
				ret.Add(railKeyValueResult);
				num2++;
			}
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x0005D90C File Offset: 0x0005BB0C
		public static void Csharp2Cpp(List<RoomInfoListSorter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfoListSorter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfoListSorter(intPtr);
			}
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x0005D954 File Offset: 0x0005BB54
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListSorter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfoListSorter_Item(ptr, (uint)num2);
				RoomInfoListSorter roomInfoListSorter = new RoomInfoListSorter();
				RailConverter.Cpp2Csharp(intPtr, roomInfoListSorter);
				ret.Add(roomInfoListSorter);
				num2++;
			}
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x0005D998 File Offset: 0x0005BB98
		public static void Csharp2Cpp(List<SpaceWorkID> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArraySpaceWorkID_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_SpaceWorkID__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArraySpaceWorkID_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_SpaceWorkID(intPtr);
			}
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x0005D9E0 File Offset: 0x0005BBE0
		public static void Cpp2Csharp(IntPtr ptr, List<SpaceWorkID> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArraySpaceWorkID_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArraySpaceWorkID_Item(ptr, (uint)num2);
				SpaceWorkID spaceWorkID = new SpaceWorkID();
				RailConverter.Cpp2Csharp(intPtr, spaceWorkID);
				ret.Add(spaceWorkID);
				num2++;
			}
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x0005DA24 File Offset: 0x0005BC24
		public static void Csharp2Cpp(List<RailPlayedWithFriendsGameItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailPlayedWithFriendsGameItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailPlayedWithFriendsGameItem(intPtr);
			}
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0005DA6C File Offset: 0x0005BC6C
		public static void Cpp2Csharp(IntPtr ptr, List<RailPlayedWithFriendsGameItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailPlayedWithFriendsGameItem_Item(ptr, (uint)num2);
				RailPlayedWithFriendsGameItem railPlayedWithFriendsGameItem = new RailPlayedWithFriendsGameItem();
				RailConverter.Cpp2Csharp(intPtr, railPlayedWithFriendsGameItem);
				ret.Add(railPlayedWithFriendsGameItem);
				num2++;
			}
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x0005DAB0 File Offset: 0x0005BCB0
		public static void Csharp2Cpp(List<RailUserSpaceDownloadProgress> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadProgress_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailUserSpaceDownloadProgress__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadProgress_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailUserSpaceDownloadProgress(intPtr);
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x0005DAF8 File Offset: 0x0005BCF8
		public static void Cpp2Csharp(IntPtr ptr, List<RailUserSpaceDownloadProgress> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadProgress_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailUserSpaceDownloadProgress_Item(ptr, (uint)num2);
				RailUserSpaceDownloadProgress railUserSpaceDownloadProgress = new RailUserSpaceDownloadProgress();
				RailConverter.Cpp2Csharp(intPtr, railUserSpaceDownloadProgress);
				ret.Add(railUserSpaceDownloadProgress);
				num2++;
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x0005DB3C File Offset: 0x0005BD3C
		public static void Csharp2Cpp(List<EnumRailUsersLimits> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
				RAIL_API_PINVOKE.SetInt(intPtr, (int)ret[i]);
				RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x0005DB84 File Offset: 0x0005BD84
		public static void Cpp2Csharp(IntPtr ptr, List<EnumRailUsersLimits> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayEnumRailUsersLimits_Item(ptr, (uint)num2);
				ret.Add((EnumRailUsersLimits)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x0005DBC0 File Offset: 0x0005BDC0
		public static void Csharp2Cpp(List<RailGameActivityInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailGameActivityInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailGameActivityInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailGameActivityInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailGameActivityInfo(intPtr);
			}
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x0005DC08 File Offset: 0x0005BE08
		public static void Cpp2Csharp(IntPtr ptr, List<RailGameActivityInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailGameActivityInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailGameActivityInfo_Item(ptr, (uint)num2);
				RailGameActivityInfo railGameActivityInfo = new RailGameActivityInfo();
				RailConverter.Cpp2Csharp(intPtr, railGameActivityInfo);
				ret.Add(railGameActivityInfo);
				num2++;
			}
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x0005DC4C File Offset: 0x0005BE4C
		public static void Csharp2Cpp(List<EnumRailSpaceWorkType> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
				RAIL_API_PINVOKE.SetInt(intPtr, (int)ret[i]);
				RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0005DC94 File Offset: 0x0005BE94
		public static void Cpp2Csharp(IntPtr ptr, List<EnumRailSpaceWorkType> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayEnumRailSpaceWorkType_Item(ptr, (uint)num2);
				ret.Add((EnumRailSpaceWorkType)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x0005DCD0 File Offset: 0x0005BED0
		public static void Csharp2Cpp(List<GameServerListFilter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerListFilter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerListFilter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerListFilter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerListFilter(intPtr);
			}
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x0005DD18 File Offset: 0x0005BF18
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerListFilter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerListFilter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerListFilter_Item(ptr, (uint)num2);
				GameServerListFilter gameServerListFilter = new GameServerListFilter();
				RailConverter.Cpp2Csharp(intPtr, gameServerListFilter);
				ret.Add(gameServerListFilter);
				num2++;
			}
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x0005DD5C File Offset: 0x0005BF5C
		public static void Csharp2Cpp(List<RoomInfoListFilter> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomInfoListFilter__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomInfoListFilter(intPtr);
			}
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x0005DDA4 File Offset: 0x0005BFA4
		public static void Cpp2Csharp(IntPtr ptr, List<RoomInfoListFilter> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomInfoListFilter_Item(ptr, (uint)num2);
				RoomInfoListFilter roomInfoListFilter = new RoomInfoListFilter();
				RailConverter.Cpp2Csharp(intPtr, roomInfoListFilter);
				ret.Add(roomInfoListFilter);
				num2++;
			}
		}

		// Token: 0x06002D50 RID: 11600 RVA: 0x0005DDE8 File Offset: 0x0005BFE8
		public static void Csharp2Cpp(List<RailStreamFileInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailStreamFileInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailStreamFileInfo(intPtr);
			}
		}

		// Token: 0x06002D51 RID: 11601 RVA: 0x0005DE30 File Offset: 0x0005C030
		public static void Cpp2Csharp(IntPtr ptr, List<RailStreamFileInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailStreamFileInfo_Item(ptr, (uint)num2);
				RailStreamFileInfo railStreamFileInfo = new RailStreamFileInfo();
				RailConverter.Cpp2Csharp(intPtr, railStreamFileInfo);
				ret.Add(railStreamFileInfo);
				num2++;
			}
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x0005DE74 File Offset: 0x0005C074
		public static void Csharp2Cpp(List<GameServerListFilterKey> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_GameServerListFilterKey__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_GameServerListFilterKey(intPtr);
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0005DEBC File Offset: 0x0005C0BC
		public static void Cpp2Csharp(IntPtr ptr, List<GameServerListFilterKey> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayGameServerListFilterKey_Item(ptr, (uint)num2);
				GameServerListFilterKey gameServerListFilterKey = new GameServerListFilterKey();
				RailConverter.Cpp2Csharp(intPtr, gameServerListFilterKey);
				ret.Add(gameServerListFilterKey);
				num2++;
			}
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0005DF00 File Offset: 0x0005C100
		public static void Csharp2Cpp(List<RailAssetProperty> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailAssetProperty_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailAssetProperty__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailAssetProperty_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailAssetProperty(intPtr);
			}
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x0005DF48 File Offset: 0x0005C148
		public static void Cpp2Csharp(IntPtr ptr, List<RailAssetProperty> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailAssetProperty_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailAssetProperty_Item(ptr, (uint)num2);
				RailAssetProperty railAssetProperty = new RailAssetProperty();
				RailConverter.Cpp2Csharp(intPtr, railAssetProperty);
				ret.Add(railAssetProperty);
				num2++;
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x0005DF8C File Offset: 0x0005C18C
		public static void Csharp2Cpp(List<RailCoinInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailCoinInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailCoinInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailCoinInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailCoinInfo(intPtr);
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x0005DFD4 File Offset: 0x0005C1D4
		public static void Cpp2Csharp(IntPtr ptr, List<RailCoinInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailCoinInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailCoinInfo_Item(ptr, (uint)num2);
				RailCoinInfo railCoinInfo = new RailCoinInfo();
				RailConverter.Cpp2Csharp(intPtr, railCoinInfo);
				ret.Add(railCoinInfo);
				num2++;
			}
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x0005E018 File Offset: 0x0005C218
		public static void Csharp2Cpp(List<RoomMemberInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRoomMemberInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RoomMemberInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRoomMemberInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RoomMemberInfo(intPtr);
			}
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x0005E060 File Offset: 0x0005C260
		public static void Cpp2Csharp(IntPtr ptr, List<RoomMemberInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRoomMemberInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRoomMemberInfo_Item(ptr, (uint)num2);
				RoomMemberInfo roomMemberInfo = new RoomMemberInfo();
				RailConverter.Cpp2Csharp(intPtr, roomMemberInfo);
				ret.Add(roomMemberInfo);
				num2++;
			}
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x0005E0A4 File Offset: 0x0005C2A4
		public static void Csharp2Cpp(List<RailAssetItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailAssetItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailAssetItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailAssetItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailAssetItem(intPtr);
			}
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x0005E0EC File Offset: 0x0005C2EC
		public static void Cpp2Csharp(IntPtr ptr, List<RailAssetItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailAssetItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailAssetItem_Item(ptr, (uint)num2);
				RailAssetItem railAssetItem = new RailAssetItem();
				RailConverter.Cpp2Csharp(intPtr, railAssetItem);
				ret.Add(railAssetItem);
				num2++;
			}
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x0005E130 File Offset: 0x0005C330
		public static void Csharp2Cpp(List<RailAssetInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailAssetInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailAssetInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailAssetInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailAssetInfo(intPtr);
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x0005E178 File Offset: 0x0005C378
		public static void Cpp2Csharp(IntPtr ptr, List<RailAssetInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailAssetInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailAssetInfo_Item(ptr, (uint)num2);
				RailAssetInfo railAssetInfo = new RailAssetInfo();
				RailConverter.Cpp2Csharp(intPtr, railAssetInfo);
				ret.Add(railAssetInfo);
				num2++;
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x0005E1BC File Offset: 0x0005C3BC
		public static void Csharp2Cpp(List<uint> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayuint32_t_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				RAIL_API_PINVOKE.RailArrayuint32_t_push_back(ptr, ret[i]);
			}
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x0005E1F0 File Offset: 0x0005C3F0
		public static void Cpp2Csharp(IntPtr ptr, List<uint> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayuint32_t_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayuint32_t_Item(ptr, (uint)num2);
				ret.Add((uint)RAIL_API_PINVOKE.GetInt(intPtr));
				num2++;
			}
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x0005E22C File Offset: 0x0005C42C
		public static void Csharp2Cpp(List<ulong> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayuint64_t_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				RAIL_API_PINVOKE.RailArrayuint64_t_push_back(ptr, ret[i]);
			}
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x0005E260 File Offset: 0x0005C460
		public static void Cpp2Csharp(IntPtr ptr, List<ulong> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayuint64_t_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayuint64_t_Item(ptr, (uint)num2);
				ret.Add((ulong)RAIL_API_PINVOKE.GetInt64(intPtr));
				num2++;
			}
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x0005E29C File Offset: 0x0005C49C
		public static void Csharp2Cpp(List<PlayerPersonalInfo> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_PlayerPersonalInfo__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_PlayerPersonalInfo(intPtr);
			}
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x0005E2E4 File Offset: 0x0005C4E4
		public static void Cpp2Csharp(IntPtr ptr, List<PlayerPersonalInfo> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayPlayerPersonalInfo_Item(ptr, (uint)num2);
				PlayerPersonalInfo playerPersonalInfo = new PlayerPersonalInfo();
				RailConverter.Cpp2Csharp(intPtr, playerPersonalInfo);
				ret.Add(playerPersonalInfo);
				num2++;
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x0005E328 File Offset: 0x0005C528
		public static void Csharp2Cpp(List<RailProductItem> ret, IntPtr ptr)
		{
			int count = ret.Count;
			RAIL_API_PINVOKE.RailArrayRailProductItem_clear(ptr);
			for (int i = 0; i < count; i++)
			{
				IntPtr intPtr = RAIL_API_PINVOKE.new_RailProductItem__SWIG_0();
				RailConverter.Csharp2Cpp(ret[i], intPtr);
				RAIL_API_PINVOKE.RailArrayRailProductItem_push_back(ptr, intPtr);
				RAIL_API_PINVOKE.delete_RailProductItem(intPtr);
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x0005E370 File Offset: 0x0005C570
		public static void Cpp2Csharp(IntPtr ptr, List<RailProductItem> ret)
		{
			ret.Clear();
			uint num = RAIL_API_PINVOKE.RailArrayRailProductItem_size(ptr);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				IntPtr intPtr = RAIL_API_PINVOKE.RailArrayRailProductItem_Item(ptr, (uint)num2);
				RailProductItem railProductItem = new RailProductItem();
				RailConverter.Cpp2Csharp(intPtr, railProductItem);
				ret.Add(railProductItem);
				num2++;
			}
		}
	}
}
