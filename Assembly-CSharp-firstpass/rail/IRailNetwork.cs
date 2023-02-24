using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020003A2 RID: 930
	public interface IRailNetwork
	{
		// Token: 0x06002F1F RID: 12063
		RailResult AcceptSessionRequest(RailID local_peer, RailID remote_peer);

		// Token: 0x06002F20 RID: 12064
		RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06002F21 RID: 12065
		RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x06002F22 RID: 12066
		RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06002F23 RID: 12067
		RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x06002F24 RID: 12068
		bool IsDataReady(RailID local_peer, out uint data_len, out uint message_type);

		// Token: 0x06002F25 RID: 12069
		bool IsDataReady(RailID local_peer, out uint data_len);

		// Token: 0x06002F26 RID: 12070
		RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06002F27 RID: 12071
		RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len);

		// Token: 0x06002F28 RID: 12072
		RailResult BlockMessageType(RailID local_peer, uint message_type);

		// Token: 0x06002F29 RID: 12073
		RailResult UnblockMessageType(RailID local_peer, uint message_type);

		// Token: 0x06002F2A RID: 12074
		RailResult CloseSession(RailID local_peer, RailID remote_peer);

		// Token: 0x06002F2B RID: 12075
		RailResult ResolveHostname(string domain, List<string> ip_list);

		// Token: 0x06002F2C RID: 12076
		RailResult GetSessionState(RailID remote_peer, RailNetworkSessionState session_state);

		// Token: 0x06002F2D RID: 12077
		RailResult ForbidSessionRelay(bool forbid_relay);

		// Token: 0x06002F2E RID: 12078
		RailResult SendRawData(RailID local_peer, RailGamePeer remote_game_peer, byte[] data_buf, uint data_len, bool reliable, uint message_type);

		// Token: 0x06002F2F RID: 12079
		RailResult AcceptRawSessionRequest(RailID local_peer, RailGamePeer remote_game_peer);

		// Token: 0x06002F30 RID: 12080
		RailResult ReadRawData(RailID local_peer, RailGamePeer remote_game_peer, byte[] data_buf, uint data_len, uint message_type);

		// Token: 0x06002F31 RID: 12081
		RailResult ReadRawData(RailID local_peer, RailGamePeer remote_game_peer, byte[] data_buf, uint data_len);
	}
}
