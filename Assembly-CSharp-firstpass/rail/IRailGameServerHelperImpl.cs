using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002A5 RID: 677
	public class IRailGameServerHelperImpl : RailObject, IRailGameServerHelper
	{
		// Token: 0x060028DB RID: 10459 RVA: 0x000516EC File Offset: 0x0004F8EC
		internal IRailGameServerHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000516FC File Offset: 0x0004F8FC
		~IRailGameServerHelperImpl()
		{
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00051724 File Offset: 0x0004F924
		public virtual RailResult AsyncGetGameServerPlayerList(RailID gameserver_rail_id, string user_data)
		{
			IntPtr intPtr = ((gameserver_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (gameserver_rail_id != null)
			{
				RailConverter.Csharp2Cpp(gameserver_rail_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetGameServerPlayerList(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x00051780 File Offset: 0x0004F980
		public virtual RailResult AsyncGetGameServerList(uint start_index, uint end_index, List<GameServerListFilter> alternative_filters, List<GameServerListSorter> sorter, string user_data)
		{
			IntPtr intPtr = ((alternative_filters == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerListFilter__SWIG_0());
			if (alternative_filters != null)
			{
				RailConverter.Csharp2Cpp(alternative_filters, intPtr);
			}
			IntPtr intPtr2 = ((sorter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayGameServerListSorter__SWIG_0());
			if (sorter != null)
			{
				RailConverter.Csharp2Cpp(sorter, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetGameServerList(this.swigCPtr_, start_index, end_index, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayGameServerListFilter(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayGameServerListSorter(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x000517F8 File Offset: 0x0004F9F8
		public virtual IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailGameServer railGameServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_0(this.swigCPtr_, intPtr, game_server_name, user_data);
				railGameServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailGameServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateGameServerOptions(intPtr);
			}
			return railGameServer;
		}

		// Token: 0x060028E0 RID: 10464 RVA: 0x00051860 File Offset: 0x0004FA60
		public virtual IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options, string game_server_name)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailGameServer railGameServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_1(this.swigCPtr_, intPtr, game_server_name);
				railGameServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailGameServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateGameServerOptions(intPtr);
			}
			return railGameServer;
		}

		// Token: 0x060028E1 RID: 10465 RVA: 0x000518C8 File Offset: 0x0004FAC8
		public virtual IRailGameServer AsyncCreateGameServer(CreateGameServerOptions options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateGameServerOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailGameServer railGameServer;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_2(this.swigCPtr_, intPtr);
				railGameServer = ((intPtr2 == IntPtr.Zero) ? null : new IRailGameServerImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateGameServerOptions(intPtr);
			}
			return railGameServer;
		}

		// Token: 0x060028E2 RID: 10466 RVA: 0x00051930 File Offset: 0x0004FB30
		public virtual IRailGameServer AsyncCreateGameServer()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGameServerHelper_AsyncCreateGameServer__SWIG_3(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGameServerImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060028E3 RID: 10467 RVA: 0x0005195E File Offset: 0x0004FB5E
		public virtual RailResult AsyncGetFavoriteGameServers(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_0(this.swigCPtr_, user_data);
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x0005196C File Offset: 0x0004FB6C
		public virtual RailResult AsyncGetFavoriteGameServers()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncGetFavoriteGameServers__SWIG_1(this.swigCPtr_);
		}

		// Token: 0x060028E5 RID: 10469 RVA: 0x0005197C File Offset: 0x0004FB7C
		public virtual RailResult AsyncAddFavoriteGameServer(RailID game_server_id, string user_data)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_0(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028E6 RID: 10470 RVA: 0x000519D8 File Offset: 0x0004FBD8
		public virtual RailResult AsyncAddFavoriteGameServer(RailID game_server_id)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncAddFavoriteGameServer__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028E7 RID: 10471 RVA: 0x00051A34 File Offset: 0x0004FC34
		public virtual RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id, string user_Data)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_0(this.swigCPtr_, intPtr, user_Data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028E8 RID: 10472 RVA: 0x00051A90 File Offset: 0x0004FC90
		public virtual RailResult AsyncRemoveFavoriteGameServer(RailID game_server_id)
		{
			IntPtr intPtr = ((game_server_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (game_server_id != null)
			{
				RailConverter.Csharp2Cpp(game_server_id, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGameServerHelper_AsyncRemoveFavoriteGameServer__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}
	}
}
