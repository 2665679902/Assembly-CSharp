using System;

namespace rail
{
	// Token: 0x0200029F RID: 671
	public class IRailFactoryImpl : RailObject, IRailFactory
	{
		// Token: 0x06002848 RID: 10312 RVA: 0x0004FEA8 File Offset: 0x0004E0A8
		internal IRailFactoryImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x0004FEB8 File Offset: 0x0004E0B8
		~IRailFactoryImpl()
		{
		}

		// Token: 0x0600284A RID: 10314 RVA: 0x0004FEE0 File Offset: 0x0004E0E0
		public virtual IRailPlayer RailPlayer()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailPlayer(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailPlayerImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x0004FF10 File Offset: 0x0004E110
		public virtual IRailUsersHelper RailUsersHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailUsersHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailUsersHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x0004FF40 File Offset: 0x0004E140
		public virtual IRailFriends RailFriends()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailFriends(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFriendsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x0004FF70 File Offset: 0x0004E170
		public virtual IRailFloatingWindow RailFloatingWindow()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailFloatingWindow(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFloatingWindowImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x0004FFA0 File Offset: 0x0004E1A0
		public virtual IRailBrowserHelper RailBrowserHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailBrowserHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailBrowserHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x0004FFD0 File Offset: 0x0004E1D0
		public virtual IRailInGamePurchase RailInGamePurchase()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailInGamePurchase(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailInGamePurchaseImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00050000 File Offset: 0x0004E200
		public virtual IRailInGameCoin RailInGameCoin()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailInGameCoin(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailInGameCoinImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00050030 File Offset: 0x0004E230
		public virtual IRailRoomHelper RailRoomHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailRoomHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailRoomHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00050060 File Offset: 0x0004E260
		public virtual IRailGameServerHelper RailGameServerHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailGameServerHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGameServerHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00050090 File Offset: 0x0004E290
		public virtual IRailStorageHelper RailStorageHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailStorageHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailStorageHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x000500C0 File Offset: 0x0004E2C0
		public virtual IRailUserSpaceHelper RailUserSpaceHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailUserSpaceHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailUserSpaceHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x000500F0 File Offset: 0x0004E2F0
		public virtual IRailStatisticHelper RailStatisticHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailStatisticHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailStatisticHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x00050120 File Offset: 0x0004E320
		public virtual IRailLeaderboardHelper RailLeaderboardHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailLeaderboardHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailLeaderboardHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00050150 File Offset: 0x0004E350
		public virtual IRailAchievementHelper RailAchievementHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailAchievementHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAchievementHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x00050180 File Offset: 0x0004E380
		public virtual IRailNetwork RailNetworkHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailNetworkHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailNetworkImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x000501B0 File Offset: 0x0004E3B0
		public virtual IRailApps RailApps()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailApps(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAppsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000501E0 File Offset: 0x0004E3E0
		public virtual IRailGame RailGame()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailGame(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGameImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00050210 File Offset: 0x0004E410
		public virtual IRailUtils RailUtils()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailUtils(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailUtilsImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00050240 File Offset: 0x0004E440
		public virtual IRailAssetsHelper RailAssetsHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailAssetsHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAssetsHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x00050270 File Offset: 0x0004E470
		public virtual IRailDlcHelper RailDlcHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailDlcHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailDlcHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x000502A0 File Offset: 0x0004E4A0
		public virtual IRailScreenshotHelper RailScreenshotHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailScreenshotHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailScreenshotHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x000502D0 File Offset: 0x0004E4D0
		public virtual IRailVoiceHelper RailVoiceHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailVoiceHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailVoiceHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x00050300 File Offset: 0x0004E500
		public virtual IRailSystemHelper RailSystemHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailSystemHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailSystemHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x00050330 File Offset: 0x0004E530
		public virtual IRailTextInputHelper RailTextInputHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailTextInputHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailTextInputHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x00050360 File Offset: 0x0004E560
		public virtual IRailIMEHelper RailIMETextInputHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailIMETextInputHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailIMEHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x00050390 File Offset: 0x0004E590
		public virtual IRailHttpSessionHelper RailHttpSessionHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailHttpSessionHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailHttpSessionHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x000503C0 File Offset: 0x0004E5C0
		public virtual IRailSmallObjectServiceHelper RailSmallObjectServiceHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailSmallObjectServiceHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailSmallObjectServiceHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x000503F0 File Offset: 0x0004E5F0
		public virtual IRailZoneServerHelper RailZoneServerHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailZoneServerHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailZoneServerHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x00050420 File Offset: 0x0004E620
		public virtual IRailGroupChatHelper RailGroupChatHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailGroupChatHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailGroupChatHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x00050450 File Offset: 0x0004E650
		public virtual IRailInGameStorePurchaseHelper RailInGameStorePurchaseHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailInGameStorePurchaseHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailInGameStorePurchaseHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x00050480 File Offset: 0x0004E680
		public virtual IRailInGameActivityHelper RailInGameActivityHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailInGameActivityHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailInGameActivityHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x000504B0 File Offset: 0x0004E6B0
		public virtual IRailAntiAddictionHelper RailAntiAddictionHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailAntiAddictionHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailAntiAddictionHelperImpl(intPtr);
			}
			return null;
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x000504E0 File Offset: 0x0004E6E0
		public virtual IRailThirdPartyAccountLoginHelper RailThirdPartyAccountLoginHelper()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailFactory_RailThirdPartyAccountLoginHelper(this.swigCPtr_);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailThirdPartyAccountLoginHelperImpl(intPtr);
			}
			return null;
		}
	}
}
