using System;

namespace rail
{
	// Token: 0x02000449 RID: 1097
	public interface IRailUtils
	{
		// Token: 0x06003083 RID: 12419
		uint GetTimeCountSinceGameLaunch();

		// Token: 0x06003084 RID: 12420
		uint GetTimeCountSinceComputerLaunch();

		// Token: 0x06003085 RID: 12421
		uint GetTimeFromServer();

		// Token: 0x06003086 RID: 12422
		RailResult AsyncGetImageData(string image_path, uint scale_to_width, uint scale_to_height, string user_data);

		// Token: 0x06003087 RID: 12423
		void GetErrorString(RailResult result, out string error_string);

		// Token: 0x06003088 RID: 12424
		RailResult DirtyWordsFilter(string words, bool replace_sensitive, RailDirtyWordsCheckResult check_result);

		// Token: 0x06003089 RID: 12425
		EnumRailPlatformType GetRailPlatformType();

		// Token: 0x0600308A RID: 12426
		RailResult GetLaunchAppParameters(EnumRailLaunchAppType app_type, out string parameter);

		// Token: 0x0600308B RID: 12427
		RailResult GetPlatformLanguageCode(out string language_code);

		// Token: 0x0600308C RID: 12428
		RailResult SetWarningMessageCallback(RailWarningMessageCallbackFunction callback);

		// Token: 0x0600308D RID: 12429
		RailResult GetCountryCodeOfCurrentLoggedInIP(out string country_code);
	}
}
