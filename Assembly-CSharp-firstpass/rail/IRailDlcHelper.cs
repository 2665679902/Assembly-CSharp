using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000317 RID: 791
	public interface IRailDlcHelper
	{
		// Token: 0x06002DF8 RID: 11768
		RailResult AsyncQueryIsOwnedDlcsOnServer(List<RailDlcID> dlc_ids, string user_data);

		// Token: 0x06002DF9 RID: 11769
		RailResult AsyncCheckAllDlcsStateReady(string user_data);

		// Token: 0x06002DFA RID: 11770
		bool IsDlcInstalled(RailDlcID dlc_id, out string installed_path);

		// Token: 0x06002DFB RID: 11771
		bool IsDlcInstalled(RailDlcID dlc_id);

		// Token: 0x06002DFC RID: 11772
		bool IsOwnedDlc(RailDlcID dlc_id);

		// Token: 0x06002DFD RID: 11773
		uint GetDlcCount();

		// Token: 0x06002DFE RID: 11774
		bool GetDlcInfo(uint index, RailDlcInfo dlc_info);

		// Token: 0x06002DFF RID: 11775
		bool AsyncInstallDlc(RailDlcID dlc_id, string user_data);

		// Token: 0x06002E00 RID: 11776
		bool AsyncRemoveDlc(RailDlcID dlc_id, string user_data);
	}
}
