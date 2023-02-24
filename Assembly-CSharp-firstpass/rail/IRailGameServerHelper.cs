using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000352 RID: 850
	public interface IRailGameServerHelper
	{
		// Token: 0x06002EA5 RID: 11941
		RailResult AsyncGetGameServerPlayerList(RailID gameserver_rail_id, string user_data);

		// Token: 0x06002EA6 RID: 11942
		RailResult AsyncGetGameServerList(uint start_index, uint end_index, List<GameServerListFilter> alternative_filters, List<GameServerListSorter> sorter, string user_data);

		// Token: 0x06002EA7 RID: 11943
		IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name, string user_data);

		// Token: 0x06002EA8 RID: 11944
		IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name);

		// Token: 0x06002EA9 RID: 11945
		IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options);

		// Token: 0x06002EAA RID: 11946
		IRailGameServer AsyncCreateGameServer();

		// Token: 0x06002EAB RID: 11947
		RailResult AsyncGetFavoriteGameServers(string user_data);

		// Token: 0x06002EAC RID: 11948
		RailResult AsyncGetFavoriteGameServers();

		// Token: 0x06002EAD RID: 11949
		RailResult AsyncAddFavoriteGameServer(RailID game_server_id, string user_data);

		// Token: 0x06002EAE RID: 11950
		RailResult AsyncAddFavoriteGameServer(RailID game_server_id);

		// Token: 0x06002EAF RID: 11951
		RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id, string user_Data);

		// Token: 0x06002EB0 RID: 11952
		RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id);
	}
}
