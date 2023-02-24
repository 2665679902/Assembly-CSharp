using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003F2 RID: 1010
	public interface IRailStorageHelper
	{
		// Token: 0x06002FCC RID: 12236
		IRailFile OpenFile(string filename, out RailResult result);

		// Token: 0x06002FCD RID: 12237
		IRailFile OpenFile(string filename);

		// Token: 0x06002FCE RID: 12238
		IRailFile CreateFile(string filename, out RailResult result);

		// Token: 0x06002FCF RID: 12239
		IRailFile CreateFile(string filename);

		// Token: 0x06002FD0 RID: 12240
		bool IsFileExist(string filename);

		// Token: 0x06002FD1 RID: 12241
		bool ListFiles(List<string> filelist);

		// Token: 0x06002FD2 RID: 12242
		RailResult RemoveFile(string filename);

		// Token: 0x06002FD3 RID: 12243
		bool IsFileSyncedToCloud(string filename);

		// Token: 0x06002FD4 RID: 12244
		RailResult GetFileTimestamp(string filename, out ulong time_stamp);

		// Token: 0x06002FD5 RID: 12245
		uint GetFileCount();

		// Token: 0x06002FD6 RID: 12246
		RailResult GetFileNameAndSize(uint file_index, out string filename, out ulong file_size);

		// Token: 0x06002FD7 RID: 12247
		RailResult AsyncQueryQuota();

		// Token: 0x06002FD8 RID: 12248
		RailResult SetSyncFileOption(string filename, RailSyncFileOption option);

		// Token: 0x06002FD9 RID: 12249
		bool IsCloudStorageEnabledForApp();

		// Token: 0x06002FDA RID: 12250
		bool IsCloudStorageEnabledForPlayer();

		// Token: 0x06002FDB RID: 12251
		RailResult AsyncPublishFileToUserSpace(RailPublishFileToUserSpaceOption option, string user_data);

		// Token: 0x06002FDC RID: 12252
		IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option, out RailResult result);

		// Token: 0x06002FDD RID: 12253
		IRailStreamFile OpenStreamFile(string filename, RailStreamFileOption option);

		// Token: 0x06002FDE RID: 12254
		RailResult AsyncListStreamFiles(string contents, RailListStreamFileOption option, string user_data);

		// Token: 0x06002FDF RID: 12255
		RailResult AsyncRenameStreamFile(string old_filename, string new_filename, string user_data);

		// Token: 0x06002FE0 RID: 12256
		RailResult AsyncDeleteStreamFile(string filename, string user_data);

		// Token: 0x06002FE1 RID: 12257
		uint GetRailFileEnabledOS(string filename);

		// Token: 0x06002FE2 RID: 12258
		RailResult SetRailFileEnabledOS(string filename, EnumRailStorageFileEnabledOS sync_os);
	}
}
