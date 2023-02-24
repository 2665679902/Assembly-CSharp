using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002A3 RID: 675
	public class IRailGameImpl : RailObject, IRailGame
	{
		// Token: 0x06002891 RID: 10385 RVA: 0x00050AF2 File Offset: 0x0004ECF2
		internal IRailGameImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x00050B04 File Offset: 0x0004ED04
		~IRailGameImpl()
		{
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x00050B2C File Offset: 0x0004ED2C
		public virtual RailGameID GetGameID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGame_GetGameID(this.swigCPtr_);
			RailGameID railGameID = new RailGameID();
			RailConverter.Cpp2Csharp(intPtr, railGameID);
			return railGameID;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x00050B51 File Offset: 0x0004ED51
		public virtual RailResult ReportGameContentDamaged(EnumRailGameContentDamageFlag flag)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_ReportGameContentDamaged(this.swigCPtr_, (int)flag);
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x00050B60 File Offset: 0x0004ED60
		public virtual RailResult GetGameInstallPath(out string app_path)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameInstallPath(this.swigCPtr_, intPtr);
			}
			finally
			{
				app_path = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x00050BA8 File Offset: 0x0004EDA8
		public virtual RailResult AsyncQuerySubscribeWishPlayState(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_AsyncQuerySubscribeWishPlayState(this.swigCPtr_, user_data);
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x00050BB8 File Offset: 0x0004EDB8
		public virtual RailResult GetPlayerSelectedLanguageCode(out string language_code)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetPlayerSelectedLanguageCode(this.swigCPtr_, intPtr);
			}
			finally
			{
				language_code = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x00050C00 File Offset: 0x0004EE00
		public virtual RailResult GetGameSupportedLanguageCodes(List<string> language_codes)
		{
			IntPtr intPtr = ((language_codes == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameSupportedLanguageCodes(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (language_codes != null)
				{
					RailConverter.Cpp2Csharp(intPtr, language_codes);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x00050C50 File Offset: 0x0004EE50
		public virtual RailResult SetGameState(EnumRailGamePlayingState game_state)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_SetGameState(this.swigCPtr_, (int)game_state);
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x00050C60 File Offset: 0x0004EE60
		public virtual RailResult GetGameState(out EnumRailGamePlayingState game_state)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.NewInt();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameState(this.swigCPtr_, intPtr);
			}
			finally
			{
				game_state = (EnumRailGamePlayingState)RAIL_API_PINVOKE.GetInt(intPtr);
				RAIL_API_PINVOKE.DeleteInt(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x00050CA4 File Offset: 0x0004EEA4
		public virtual RailResult RegisterGameDefineGamePlayingState(List<RailGameDefineGamePlayingState> game_playing_states)
		{
			IntPtr intPtr = ((game_playing_states == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailGameDefineGamePlayingState__SWIG_0());
			if (game_playing_states != null)
			{
				RailConverter.Csharp2Cpp(game_playing_states, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_RegisterGameDefineGamePlayingState(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRailGameDefineGamePlayingState(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x00050CF4 File Offset: 0x0004EEF4
		public virtual RailResult SetGameDefineGamePlayingState(uint game_playing_state)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_SetGameDefineGamePlayingState(this.swigCPtr_, game_playing_state);
		}

		// Token: 0x0600289D RID: 10397 RVA: 0x00050D02 File Offset: 0x0004EF02
		public virtual RailResult GetGameDefineGamePlayingState(out uint game_playing_state)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_GetGameDefineGamePlayingState(this.swigCPtr_, out game_playing_state);
		}

		// Token: 0x0600289E RID: 10398 RVA: 0x00050D10 File Offset: 0x0004EF10
		public virtual RailResult GetBranchBuildNumber(out string build_number)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetBranchBuildNumber(this.swigCPtr_, intPtr);
			}
			finally
			{
				build_number = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x00050D58 File Offset: 0x0004EF58
		public virtual RailResult GetCurrentBranchInfo(RailBranchInfo branch_info)
		{
			IntPtr intPtr = ((branch_info == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailBranchInfo__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailGame_GetCurrentBranchInfo(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (branch_info != null)
				{
					RailConverter.Cpp2Csharp(intPtr, branch_info);
				}
				RAIL_API_PINVOKE.delete_RailBranchInfo(intPtr);
			}
			return railResult;
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x00050DA8 File Offset: 0x0004EFA8
		public virtual RailResult StartGameTimeCounting(string counting_key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_StartGameTimeCounting(this.swigCPtr_, counting_key);
		}

		// Token: 0x060028A1 RID: 10401 RVA: 0x00050DB6 File Offset: 0x0004EFB6
		public virtual RailResult EndGameTimeCounting(string counting_key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailGame_EndGameTimeCounting(this.swigCPtr_, counting_key);
		}

		// Token: 0x060028A2 RID: 10402 RVA: 0x00050DC4 File Offset: 0x0004EFC4
		public virtual RailID GetGamePurchasePlayerRailID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailGame_GetGamePurchasePlayerRailID(this.swigCPtr_);
			RailID railID = new RailID();
			RailConverter.Cpp2Csharp(intPtr, railID);
			return railID;
		}

		// Token: 0x060028A3 RID: 10403 RVA: 0x00050DE9 File Offset: 0x0004EFE9
		public virtual uint GetGameEarliestPurchaseTime()
		{
			return RAIL_API_PINVOKE.IRailGame_GetGameEarliestPurchaseTime(this.swigCPtr_);
		}

		// Token: 0x060028A4 RID: 10404 RVA: 0x00050DF6 File Offset: 0x0004EFF6
		public virtual uint GetTimeCountSinceGameActivated()
		{
			return RAIL_API_PINVOKE.IRailGame_GetTimeCountSinceGameActivated(this.swigCPtr_);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x00050E03 File Offset: 0x0004F003
		public virtual uint GetTimeCountSinceLastMouseMoved()
		{
			return RAIL_API_PINVOKE.IRailGame_GetTimeCountSinceLastMouseMoved(this.swigCPtr_);
		}
	}
}
