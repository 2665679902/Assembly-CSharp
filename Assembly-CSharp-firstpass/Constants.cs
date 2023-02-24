using System;
using UnityEngine;

// Token: 0x02000090 RID: 144
public static class Constants
{
	// Token: 0x04000544 RID: 1348
	public const float GlobalTimeScale = 1f;

	// Token: 0x04000545 RID: 1349
	public const float MINUTES = 60f;

	// Token: 0x04000546 RID: 1350
	public const float SECONDS_PER_CYCLE = 600f;

	// Token: 0x04000547 RID: 1351
	public const int ScheduleBlocksPerCycle = 24;

	// Token: 0x04000548 RID: 1352
	public const int NIGHT_BLOCKS = 3;

	// Token: 0x04000549 RID: 1353
	public const int DAY_BLOCKS = 21;

	// Token: 0x0400054A RID: 1354
	public const float SecondsPerScheduleBlock = 25f;

	// Token: 0x0400054B RID: 1355
	public const float NightimeDurationInSeconds = 75f;

	// Token: 0x0400054C RID: 1356
	public const float DaytimeDurationInSeconds = 525f;

	// Token: 0x0400054D RID: 1357
	public const float NightimeDurationInPercentage = 0.125f;

	// Token: 0x0400054E RID: 1358
	public const float DaytimeDurationInPercentage = 0.875f;

	// Token: 0x0400054F RID: 1359
	public const float StartTimeInSeconds = 50f;

	// Token: 0x04000550 RID: 1360
	public const float CircuitOverloadTime = 6f;

	// Token: 0x04000551 RID: 1361
	public const int AutoSaveDayInterval = 1;

	// Token: 0x04000552 RID: 1362
	public const float ICE_DIG_TIME = 4f;

	// Token: 0x04000553 RID: 1363
	public const string BULLETSTRING = "• ";

	// Token: 0x04000554 RID: 1364
	public const string TABSTRING = "    ";

	// Token: 0x04000555 RID: 1365
	public const string TABBULLETSTRING = "    • ";

	// Token: 0x04000556 RID: 1366
	public static readonly Color POSITIVE_COLOR = new Color(0.32156864f, 0.7529412f, 0.4745098f);

	// Token: 0x04000557 RID: 1367
	public static readonly string POSITIVE_COLOR_STR = "#" + Constants.POSITIVE_COLOR.ToHexString();

	// Token: 0x04000558 RID: 1368
	public static readonly Color NEGATIVE_COLOR = new Color(0.95686275f, 0.2901961f, 0.2784314f);

	// Token: 0x04000559 RID: 1369
	public static readonly string NEGATIVE_COLOR_STR = "#" + Constants.NEGATIVE_COLOR.ToHexString();

	// Token: 0x0400055A RID: 1370
	public static readonly Color NEUTRAL_COLOR = Color.grey;

	// Token: 0x0400055B RID: 1371
	public static readonly string NEUTRAL_COLOR_STR = "#" + Constants.NEUTRAL_COLOR.ToHexString();

	// Token: 0x0400055C RID: 1372
	public static readonly Color WARNING_COLOR = new Color(1f, 0.93333334f, 0.43137255f);

	// Token: 0x0400055D RID: 1373
	public static readonly string WARNING_COLOR_STR = "#" + Constants.WARNING_COLOR.ToHexString();

	// Token: 0x0400055E RID: 1374
	public static readonly string WHITE_COLOR_STR = "#" + Color.white.ToHexString();

	// Token: 0x0400055F RID: 1375
	public const float W2KW = 0.001f;

	// Token: 0x04000560 RID: 1376
	public const float KW2W = 1000f;

	// Token: 0x04000561 RID: 1377
	public const float J2DTU = 1f;

	// Token: 0x04000562 RID: 1378
	public const float W2DTU_S = 1f;

	// Token: 0x04000563 RID: 1379
	public const float KW2DTU_S = 1000f;

	// Token: 0x04000564 RID: 1380
	public const float G2KG = 0.001f;

	// Token: 0x04000565 RID: 1381
	public const float KG2G = 1000f;

	// Token: 0x04000566 RID: 1382
	public const float CELSIUS2KELVIN = 273.15f;

	// Token: 0x04000567 RID: 1383
	public const float CAL2KCAL = 0.001f;

	// Token: 0x04000568 RID: 1384
	public const float KCAL2CAL = 1000f;

	// Token: 0x04000569 RID: 1385
	public const float HEATOFVAPORIZATION_WATER = 580f;

	// Token: 0x0400056A RID: 1386
	public const float DefaultEntityThickness = 0.01f;

	// Token: 0x0400056B RID: 1387
	public const float DefaultSurfaceArea = 10f;

	// Token: 0x0400056C RID: 1388
	public const float DefaultGroundTransferScale = 0.0625f;

	// Token: 0x0400056D RID: 1389
	public const float SPACE_DISTANCE_TO_KILOMETERS = 10000f;

	// Token: 0x0400056E RID: 1390
	public const int LOGIC_SOUND_INTERVAL_COUNTER = 2;

	// Token: 0x0400056F RID: 1391
	public const float LOGIC_SOUND_VOLUME_COOLDOWN = 3f;

	// Token: 0x04000570 RID: 1392
	public const float DEFAULT_SOUND_EVENT_VOLUME = 1f;

	// Token: 0x04000571 RID: 1393
	public const float AUDIO_HIGHLIGHT_POSITION_INTENSITY = 0f;
}
