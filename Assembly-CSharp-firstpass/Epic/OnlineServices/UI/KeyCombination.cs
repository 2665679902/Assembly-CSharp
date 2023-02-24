using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000565 RID: 1381
	[Flags]
	public enum KeyCombination
	{
		// Token: 0x04001596 RID: 5526
		ModifierShift = 16,
		// Token: 0x04001597 RID: 5527
		KeyTypeMask = 65535,
		// Token: 0x04001598 RID: 5528
		ModifierMask = -65536,
		// Token: 0x04001599 RID: 5529
		Shift = 65536,
		// Token: 0x0400159A RID: 5530
		Control = 131072,
		// Token: 0x0400159B RID: 5531
		Alt = 262144,
		// Token: 0x0400159C RID: 5532
		Meta = 524288,
		// Token: 0x0400159D RID: 5533
		ValidModifierMask = 983040,
		// Token: 0x0400159E RID: 5534
		None = 0,
		// Token: 0x0400159F RID: 5535
		Space = 1,
		// Token: 0x040015A0 RID: 5536
		Backspace = 2,
		// Token: 0x040015A1 RID: 5537
		Tab = 3,
		// Token: 0x040015A2 RID: 5538
		Escape = 4,
		// Token: 0x040015A3 RID: 5539
		PageUp = 5,
		// Token: 0x040015A4 RID: 5540
		PageDown = 6,
		// Token: 0x040015A5 RID: 5541
		End = 7,
		// Token: 0x040015A6 RID: 5542
		Home = 8,
		// Token: 0x040015A7 RID: 5543
		Insert = 9,
		// Token: 0x040015A8 RID: 5544
		Delete = 10,
		// Token: 0x040015A9 RID: 5545
		Left = 11,
		// Token: 0x040015AA RID: 5546
		Up = 12,
		// Token: 0x040015AB RID: 5547
		Right = 13,
		// Token: 0x040015AC RID: 5548
		Down = 14,
		// Token: 0x040015AD RID: 5549
		Key0 = 15,
		// Token: 0x040015AE RID: 5550
		Key1 = 16,
		// Token: 0x040015AF RID: 5551
		Key2 = 17,
		// Token: 0x040015B0 RID: 5552
		Key3 = 18,
		// Token: 0x040015B1 RID: 5553
		Key4 = 19,
		// Token: 0x040015B2 RID: 5554
		Key5 = 20,
		// Token: 0x040015B3 RID: 5555
		Key6 = 21,
		// Token: 0x040015B4 RID: 5556
		Key7 = 22,
		// Token: 0x040015B5 RID: 5557
		Key8 = 23,
		// Token: 0x040015B6 RID: 5558
		Key9 = 24,
		// Token: 0x040015B7 RID: 5559
		KeyA = 25,
		// Token: 0x040015B8 RID: 5560
		KeyB = 26,
		// Token: 0x040015B9 RID: 5561
		KeyC = 27,
		// Token: 0x040015BA RID: 5562
		KeyD = 28,
		// Token: 0x040015BB RID: 5563
		KeyE = 29,
		// Token: 0x040015BC RID: 5564
		KeyF = 30,
		// Token: 0x040015BD RID: 5565
		KeyG = 31,
		// Token: 0x040015BE RID: 5566
		KeyH = 32,
		// Token: 0x040015BF RID: 5567
		KeyI = 33,
		// Token: 0x040015C0 RID: 5568
		KeyJ = 34,
		// Token: 0x040015C1 RID: 5569
		KeyK = 35,
		// Token: 0x040015C2 RID: 5570
		KeyL = 36,
		// Token: 0x040015C3 RID: 5571
		KeyM = 37,
		// Token: 0x040015C4 RID: 5572
		KeyN = 38,
		// Token: 0x040015C5 RID: 5573
		KeyO = 39,
		// Token: 0x040015C6 RID: 5574
		KeyP = 40,
		// Token: 0x040015C7 RID: 5575
		KeyQ = 41,
		// Token: 0x040015C8 RID: 5576
		KeyR = 42,
		// Token: 0x040015C9 RID: 5577
		KeyS = 43,
		// Token: 0x040015CA RID: 5578
		KeyT = 44,
		// Token: 0x040015CB RID: 5579
		KeyU = 45,
		// Token: 0x040015CC RID: 5580
		KeyV = 46,
		// Token: 0x040015CD RID: 5581
		KeyW = 47,
		// Token: 0x040015CE RID: 5582
		KeyX = 48,
		// Token: 0x040015CF RID: 5583
		KeyY = 49,
		// Token: 0x040015D0 RID: 5584
		KeyZ = 50,
		// Token: 0x040015D1 RID: 5585
		Numpad0 = 51,
		// Token: 0x040015D2 RID: 5586
		Numpad1 = 52,
		// Token: 0x040015D3 RID: 5587
		Numpad2 = 53,
		// Token: 0x040015D4 RID: 5588
		Numpad3 = 54,
		// Token: 0x040015D5 RID: 5589
		Numpad4 = 55,
		// Token: 0x040015D6 RID: 5590
		Numpad5 = 56,
		// Token: 0x040015D7 RID: 5591
		Numpad6 = 57,
		// Token: 0x040015D8 RID: 5592
		Numpad7 = 58,
		// Token: 0x040015D9 RID: 5593
		Numpad8 = 59,
		// Token: 0x040015DA RID: 5594
		Numpad9 = 60,
		// Token: 0x040015DB RID: 5595
		NumpadAsterisk = 61,
		// Token: 0x040015DC RID: 5596
		NumpadPlus = 62,
		// Token: 0x040015DD RID: 5597
		NumpadMinus = 63,
		// Token: 0x040015DE RID: 5598
		NumpadPeriod = 64,
		// Token: 0x040015DF RID: 5599
		NumpadDivide = 65,
		// Token: 0x040015E0 RID: 5600
		F1 = 66,
		// Token: 0x040015E1 RID: 5601
		F2 = 67,
		// Token: 0x040015E2 RID: 5602
		F3 = 68,
		// Token: 0x040015E3 RID: 5603
		F4 = 69,
		// Token: 0x040015E4 RID: 5604
		F5 = 70,
		// Token: 0x040015E5 RID: 5605
		F6 = 71,
		// Token: 0x040015E6 RID: 5606
		F7 = 72,
		// Token: 0x040015E7 RID: 5607
		F8 = 73,
		// Token: 0x040015E8 RID: 5608
		F9 = 74,
		// Token: 0x040015E9 RID: 5609
		F10 = 75,
		// Token: 0x040015EA RID: 5610
		F11 = 76,
		// Token: 0x040015EB RID: 5611
		F12 = 77,
		// Token: 0x040015EC RID: 5612
		F13 = 78,
		// Token: 0x040015ED RID: 5613
		F14 = 79,
		// Token: 0x040015EE RID: 5614
		F15 = 80,
		// Token: 0x040015EF RID: 5615
		F16 = 81,
		// Token: 0x040015F0 RID: 5616
		F17 = 82,
		// Token: 0x040015F1 RID: 5617
		F18 = 83,
		// Token: 0x040015F2 RID: 5618
		F19 = 84,
		// Token: 0x040015F3 RID: 5619
		F20 = 85,
		// Token: 0x040015F4 RID: 5620
		F21 = 86,
		// Token: 0x040015F5 RID: 5621
		F22 = 87,
		// Token: 0x040015F6 RID: 5622
		F23 = 88,
		// Token: 0x040015F7 RID: 5623
		F24 = 89,
		// Token: 0x040015F8 RID: 5624
		OemPlus = 90,
		// Token: 0x040015F9 RID: 5625
		OemComma = 91,
		// Token: 0x040015FA RID: 5626
		OemMinus = 92,
		// Token: 0x040015FB RID: 5627
		OemPeriod = 93,
		// Token: 0x040015FC RID: 5628
		Oem1 = 94,
		// Token: 0x040015FD RID: 5629
		Oem2 = 95,
		// Token: 0x040015FE RID: 5630
		Oem3 = 96,
		// Token: 0x040015FF RID: 5631
		Oem4 = 97,
		// Token: 0x04001600 RID: 5632
		Oem5 = 98,
		// Token: 0x04001601 RID: 5633
		Oem6 = 99,
		// Token: 0x04001602 RID: 5634
		Oem7 = 100,
		// Token: 0x04001603 RID: 5635
		Oem8 = 101,
		// Token: 0x04001604 RID: 5636
		MaxKeyType = 102
	}
}
