using System;

namespace rail
{
	// Token: 0x0200040B RID: 1035
	public interface IRailTextInputHelper
	{
		// Token: 0x06002FFE RID: 12286
		RailResult ShowTextInputWindow(RailTextInputWindowOption options);

		// Token: 0x06002FFF RID: 12287
		void GetTextInputContent(out string content);

		// Token: 0x06003000 RID: 12288
		RailResult HideTextInputWindow();
	}
}
