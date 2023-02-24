using System;

// Token: 0x02000784 RID: 1924
public static class GameSoundEvents
{
	// Token: 0x040022F0 RID: 8944
	public static GameSoundEvents.Event BatteryFull = new GameSoundEvents.Event("game_triggered.battery_full");

	// Token: 0x040022F1 RID: 8945
	public static GameSoundEvents.Event BatteryWarning = new GameSoundEvents.Event("game_triggered.battery_warning");

	// Token: 0x040022F2 RID: 8946
	public static GameSoundEvents.Event BatteryDischarged = new GameSoundEvents.Event("game_triggered.battery_drained");

	// Token: 0x0200149D RID: 5277
	public class Event
	{
		// Token: 0x06008198 RID: 33176 RVA: 0x002E2443 File Offset: 0x002E0643
		public Event(string name)
		{
			this.Name = name;
		}

		// Token: 0x040063DB RID: 25563
		public HashedString Name;
	}
}
