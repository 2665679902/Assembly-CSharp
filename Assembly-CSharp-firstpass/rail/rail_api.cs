using System;

namespace rail
{
	// Token: 0x020002DC RID: 732
	public class rail_api
	{
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x0005E3EA File Offset: 0x0005C5EA
		public static uint kRailMaxQuerySpaceWorksLimit
		{
			get
			{
				return RAIL_API_PINVOKE.kRailMaxQuerySpaceWorksLimit_get();
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x0005E3F1 File Offset: 0x0005C5F1
		public static uint kRailMaxQueryPlayedWithFriendsTimeLimit
		{
			get
			{
				return RAIL_API_PINVOKE.kRailMaxQueryPlayedWithFriendsTimeLimit_get();
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x0005E3F8 File Offset: 0x0005C5F8
		public static uint kRailRoomDefaultMaxMemberNumber
		{
			get
			{
				return RAIL_API_PINVOKE.kRailRoomDefaultMaxMemberNumber_get();
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x0005E3FF File Offset: 0x0005C5FF
		public static uint kRailRoomDataKeyValuePairsLimit
		{
			get
			{
				return RAIL_API_PINVOKE.kRailRoomDataKeyValuePairsLimit_get();
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0005E406 File Offset: 0x0005C606
		public static uint kRailMaxGameDefinePlayingStateValue
		{
			get
			{
				return RAIL_API_PINVOKE.kRailMaxGameDefinePlayingStateValue_get();
			}
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x0005E410 File Offset: 0x0005C610
		public static bool RailNeedRestartAppForCheckingEnvironment(RailGameID game_id, int argc, string[] argv)
		{
			IntPtr intPtr = ((game_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGameID__SWIG_0());
			if (game_id != null)
			{
				RailConverter.Csharp2Cpp(game_id, intPtr);
			}
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.RailNeedRestartAppForCheckingEnvironment(intPtr, argc, argv);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailGameID(intPtr);
			}
			return flag;
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x0005E468 File Offset: 0x0005C668
		public static bool RailInitialize()
		{
			return RAIL_API_PINVOKE.RailInitialize();
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x0005E46F File Offset: 0x0005C66F
		public static void RailFinalize()
		{
			RAIL_API_PINVOKE.RailFinalize();
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x0005E476 File Offset: 0x0005C676
		public static void RailFireEvents()
		{
			RAIL_API_PINVOKE.RailFireEvents();
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x0005E480 File Offset: 0x0005C680
		public static IRailFactory RailFactory()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.RailFactory();
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailFactoryImpl(intPtr);
			}
			return null;
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x0005E4A8 File Offset: 0x0005C6A8
		public static void RailGetSdkVersion(out string version, out string description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			IntPtr intPtr2 = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			try
			{
				RAIL_API_PINVOKE.RailGetSdkVersion(intPtr, intPtr2);
			}
			finally
			{
				version = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
				description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr2));
				RAIL_API_PINVOKE.delete_RailString(intPtr2);
			}
		}

		// Token: 0x04000A84 RID: 2692
		public static readonly int USE_MANUAL_ALLOC = RAIL_API_PINVOKE.USE_MANUAL_ALLOC_get();

		// Token: 0x04000A85 RID: 2693
		public static readonly int RAIL_SDK_PACKING = RAIL_API_PINVOKE.RAIL_SDK_PACKING_get();
	}
}
