using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000413 RID: 1043
	public interface IRailSpaceWork : IRailComponent
	{
		// Token: 0x0600300A RID: 12298
		void Close();

		// Token: 0x0600300B RID: 12299
		SpaceWorkID GetSpaceWorkID();

		// Token: 0x0600300C RID: 12300
		bool Editable();

		// Token: 0x0600300D RID: 12301
		RailResult StartSync(string user_data);

		// Token: 0x0600300E RID: 12302
		RailResult GetSyncProgress(RailSpaceWorkSyncProgress progress);

		// Token: 0x0600300F RID: 12303
		RailResult CancelSync();

		// Token: 0x06003010 RID: 12304
		RailResult GetWorkLocalFolder(out string path);

		// Token: 0x06003011 RID: 12305
		RailResult AsyncUpdateMetadata(string user_data);

		// Token: 0x06003012 RID: 12306
		RailResult GetName(out string name);

		// Token: 0x06003013 RID: 12307
		RailResult GetDescription(out string description);

		// Token: 0x06003014 RID: 12308
		RailResult GetUrl(out string url);

		// Token: 0x06003015 RID: 12309
		uint GetCreateTime();

		// Token: 0x06003016 RID: 12310
		uint GetLastUpdateTime();

		// Token: 0x06003017 RID: 12311
		ulong GetWorkFileSize();

		// Token: 0x06003018 RID: 12312
		RailResult GetTags(List<string> tags);

		// Token: 0x06003019 RID: 12313
		RailResult GetPreviewImage(out string path);

		// Token: 0x0600301A RID: 12314
		RailResult GetVersion(out string version);

		// Token: 0x0600301B RID: 12315
		ulong GetDownloadCount();

		// Token: 0x0600301C RID: 12316
		ulong GetSubscribedCount();

		// Token: 0x0600301D RID: 12317
		EnumRailSpaceWorkShareLevel GetShareLevel();

		// Token: 0x0600301E RID: 12318
		ulong GetScore();

		// Token: 0x0600301F RID: 12319
		RailResult GetMetadata(string key, out string value);

		// Token: 0x06003020 RID: 12320
		EnumRailSpaceWorkRateValue GetMyVote();

		// Token: 0x06003021 RID: 12321
		bool IsFavorite();

		// Token: 0x06003022 RID: 12322
		bool IsSubscribed();

		// Token: 0x06003023 RID: 12323
		RailResult SetName(string name);

		// Token: 0x06003024 RID: 12324
		RailResult SetDescription(string description);

		// Token: 0x06003025 RID: 12325
		RailResult SetTags(List<string> tags);

		// Token: 0x06003026 RID: 12326
		RailResult SetPreviewImage(string path_filename);

		// Token: 0x06003027 RID: 12327
		RailResult SetVersion(string version);

		// Token: 0x06003028 RID: 12328
		RailResult SetShareLevel(EnumRailSpaceWorkShareLevel level);

		// Token: 0x06003029 RID: 12329
		RailResult SetShareLevel();

		// Token: 0x0600302A RID: 12330
		RailResult SetMetadata(string key, string value);

		// Token: 0x0600302B RID: 12331
		RailResult SetContentFromFolder(string path);

		// Token: 0x0600302C RID: 12332
		RailResult GetAllMetadata(List<RailKeyValue> metadata);

		// Token: 0x0600302D RID: 12333
		RailResult GetAdditionalPreviewUrls(List<string> preview_urls);

		// Token: 0x0600302E RID: 12334
		RailResult GetAssociatedSpaceWorks(List<SpaceWorkID> ids);

		// Token: 0x0600302F RID: 12335
		RailResult GetLanguages(List<string> languages);

		// Token: 0x06003030 RID: 12336
		RailResult RemoveMetadata(string key);

		// Token: 0x06003031 RID: 12337
		RailResult SetAdditionalPreviews(List<string> local_paths);

		// Token: 0x06003032 RID: 12338
		RailResult SetAssociatedSpaceWorks(List<SpaceWorkID> ids);

		// Token: 0x06003033 RID: 12339
		RailResult SetLanguages(List<string> languages);

		// Token: 0x06003034 RID: 12340
		RailResult GetPreviewUrl(out string url, uint scaling);

		// Token: 0x06003035 RID: 12341
		RailResult GetPreviewUrl(out string url);

		// Token: 0x06003036 RID: 12342
		RailResult GetVoteDetail(List<RailSpaceWorkVoteDetail> vote_details);

		// Token: 0x06003037 RID: 12343
		RailResult GetUploaderIDs(List<RailID> uploader_ids);

		// Token: 0x06003038 RID: 12344
		RailResult SetUpdateOptions(RailSpaceWorkUpdateOptions options);

		// Token: 0x06003039 RID: 12345
		RailResult GetStatistic(EnumRailSpaceWorkStatistic stat_type, out ulong value);

		// Token: 0x0600303A RID: 12346
		RailResult RemovePreviewImage();

		// Token: 0x0600303B RID: 12347
		uint GetState();

		// Token: 0x0600303C RID: 12348
		RailResult AddAssociatedGameIDs(List<RailGameID> game_ids);

		// Token: 0x0600303D RID: 12349
		RailResult RemoveAssociatedGameIDs(List<RailGameID> game_ids);

		// Token: 0x0600303E RID: 12350
		RailResult GetAssociatedGameIDs(List<RailGameID> game_ids);

		// Token: 0x0600303F RID: 12351
		RailResult GetLocalVersion(out string version);
	}
}
