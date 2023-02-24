using System;

namespace rail
{
	// Token: 0x0200036F RID: 879
	public interface IRailIMEHelper
	{
		// Token: 0x06002EDC RID: 11996
		RailResult EnableIMEHelperTextInputWindow(bool enable, RailTextInputImeWindowOption option);

		// Token: 0x06002EDD RID: 11997
		RailResult UpdateIMEHelperTextInputWindowPosition(RailWindowPosition position);
	}
}
