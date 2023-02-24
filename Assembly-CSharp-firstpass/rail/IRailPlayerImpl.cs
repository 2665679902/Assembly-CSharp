using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002B6 RID: 694
	public class IRailPlayerImpl : RailObject, IRailPlayer
	{
		// Token: 0x06002968 RID: 10600 RVA: 0x000530D8 File Offset: 0x000512D8
		internal IRailPlayerImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x000530E8 File Offset: 0x000512E8
		~IRailPlayerImpl()
		{
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00053110 File Offset: 0x00051310
		public virtual bool AlreadyLoggedIn()
		{
			return RAIL_API_PINVOKE.IRailPlayer_AlreadyLoggedIn(this.swigCPtr_);
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x00053120 File Offset: 0x00051320
		public virtual RailID GetRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailPlayer_GetRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x0600296C RID: 10604 RVA: 0x00053148 File Offset: 0x00051348
		public virtual RailResult GetPlayerDataPath(out string path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_GetPlayerDataPath(this.swigCPtr_, intPtr);
			}
			finally
			{
				path = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600296D RID: 10605 RVA: 0x00053190 File Offset: 0x00051390
		public virtual RailResult AsyncAcquireSessionTicket(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncAcquireSessionTicket(this.swigCPtr_, user_data);
		}

		// Token: 0x0600296E RID: 10606 RVA: 0x000531A0 File Offset: 0x000513A0
		public virtual RailResult AsyncStartSessionWithPlayer(RailSessionTicket player_ticket, RailID player_rail_id, string user_data)
		{
			IntPtr intPtr = ((player_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (player_ticket != null)
			{
				RailConverter.Csharp2Cpp(player_ticket, intPtr);
			}
			IntPtr intPtr2 = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncStartSessionWithPlayer(this.swigCPtr_, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600296F RID: 10607 RVA: 0x00053220 File Offset: 0x00051420
		public virtual void TerminateSessionOfPlayer(RailID player_rail_id)
		{
			IntPtr intPtr = ((player_rail_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (player_rail_id != null)
			{
				RailConverter.Csharp2Cpp(player_rail_id, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailPlayer_TerminateSessionOfPlayer(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x0005327C File Offset: 0x0005147C
		public virtual void AbandonSessionTicket(RailSessionTicket session_ticket)
		{
			IntPtr intPtr = ((session_ticket == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailSessionTicket());
			if (session_ticket != null)
			{
				RailConverter.Csharp2Cpp(session_ticket, intPtr);
			}
			try
			{
				RAIL_API_PINVOKE.IRailPlayer_AbandonSessionTicket(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailSessionTicket(intPtr);
			}
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000532CC File Offset: 0x000514CC
		public virtual RailResult GetPlayerName(out string name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_GetPlayerName(this.swigCPtr_, intPtr);
			}
			finally
			{
				name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x00053314 File Offset: 0x00051514
		public virtual EnumRailPlayerOwnershipType GetPlayerOwnershipType()
		{
			return (EnumRailPlayerOwnershipType)RAIL_API_PINVOKE.IRailPlayer_GetPlayerOwnershipType(this.swigCPtr_);
		}

		// Token: 0x06002973 RID: 10611 RVA: 0x00053321 File Offset: 0x00051521
		public virtual RailResult AsyncGetGamePurchaseKey(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncGetGamePurchaseKey(this.swigCPtr_, user_data);
		}

		// Token: 0x06002974 RID: 10612 RVA: 0x0005332F File Offset: 0x0005152F
		public virtual bool IsGameRevenueLimited()
		{
			return RAIL_API_PINVOKE.IRailPlayer_IsGameRevenueLimited(this.swigCPtr_);
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x0005333C File Offset: 0x0005153C
		public virtual float GetRateOfGameRevenue()
		{
			return RAIL_API_PINVOKE.IRailPlayer_GetRateOfGameRevenue(this.swigCPtr_);
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x00053349 File Offset: 0x00051549
		public virtual RailResult AsyncQueryPlayerBannedStatus(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncQueryPlayerBannedStatus(this.swigCPtr_, user_data);
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x00053358 File Offset: 0x00051558
		public virtual RailResult AsyncGetAuthenticateURL(RailGetAuthenticateURLOptions options, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGetAuthenticateURLOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncGetAuthenticateURL(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailGetAuthenticateURLOptions(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000533A8 File Offset: 0x000515A8
		public virtual RailResult AsyncGetPlayerMetadata(List<string> keys, string user_data)
		{
			IntPtr intPtr = ((keys == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (keys != null)
			{
				RailConverter.Csharp2Cpp(keys, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncGetPlayerMetadata(this.swigCPtr_, intPtr, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000533F8 File Offset: 0x000515F8
		public virtual RailResult AsyncGetEncryptedGameTicket(string set_metadata, string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailPlayer_AsyncGetEncryptedGameTicket(this.swigCPtr_, set_metadata, user_data);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00053407 File Offset: 0x00051607
		public virtual RailPlayerAccountType GetPlayerAccountType()
		{
			return (RailPlayerAccountType)RAIL_API_PINVOKE.IRailPlayer_GetPlayerAccountType(this.swigCPtr_);
		}
	}
}
