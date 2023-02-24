using System;

namespace rail
{
	// Token: 0x020003E0 RID: 992
	public interface IRailScreenshotHelper
	{
		// Token: 0x06002F9F RID: 12191
		IRailScreenshot CreateScreenshotWithRawData(byte[] rgb_data, uint len, uint width, uint height);

		// Token: 0x06002FA0 RID: 12192
		IRailScreenshot CreateScreenshotWithLocalImage(string image_file, string thumbnail_file);

		// Token: 0x06002FA1 RID: 12193
		void AsyncTakeScreenshot(string user_data);

		// Token: 0x06002FA2 RID: 12194
		void HookScreenshotHotKey(bool hook);

		// Token: 0x06002FA3 RID: 12195
		bool IsScreenshotHotKeyHooked();
	}
}
