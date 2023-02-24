using System;

namespace rail
{
	// Token: 0x02000409 RID: 1033
	public enum RailSystemState
	{
		// Token: 0x04000FBD RID: 4029
		kSystemStateUnknown,
		// Token: 0x04000FBE RID: 4030
		kSystemStatePlatformOnline,
		// Token: 0x04000FBF RID: 4031
		kSystemStatePlatformOffline,
		// Token: 0x04000FC0 RID: 4032
		kSystemStatePlatformExit,
		// Token: 0x04000FC1 RID: 4033
		kSystemStatePlayerOwnershipExpired = 20,
		// Token: 0x04000FC2 RID: 4034
		kSystemStatePlayerOwnershipActivated,
		// Token: 0x04000FC3 RID: 4035
		kSystemStatePlayerOwnershipBanned,
		// Token: 0x04000FC4 RID: 4036
		kSystemStateGameExitByAntiAddiction = 40
	}
}
