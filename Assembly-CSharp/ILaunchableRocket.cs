using System;
using UnityEngine;

// Token: 0x0200094F RID: 2383
public interface ILaunchableRocket
{
	// Token: 0x1700052B RID: 1323
	// (get) Token: 0x06004662 RID: 18018
	LaunchableRocketRegisterType registerType { get; }

	// Token: 0x1700052C RID: 1324
	// (get) Token: 0x06004663 RID: 18019
	GameObject LaunchableGameObject { get; }

	// Token: 0x1700052D RID: 1325
	// (get) Token: 0x06004664 RID: 18020
	float rocketSpeed { get; }

	// Token: 0x1700052E RID: 1326
	// (get) Token: 0x06004665 RID: 18021
	bool isLanding { get; }
}
