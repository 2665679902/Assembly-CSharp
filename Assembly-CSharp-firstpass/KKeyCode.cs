using System;

// Token: 0x02000036 RID: 54
public enum KKeyCode
{
	// Token: 0x04000249 RID: 585
	None,
	// Token: 0x0400024A RID: 586
	Backspace = 8,
	// Token: 0x0400024B RID: 587
	Tab,
	// Token: 0x0400024C RID: 588
	Clear = 12,
	// Token: 0x0400024D RID: 589
	Return,
	// Token: 0x0400024E RID: 590
	Pause = 19,
	// Token: 0x0400024F RID: 591
	Escape = 27,
	// Token: 0x04000250 RID: 592
	Space = 32,
	// Token: 0x04000251 RID: 593
	Exclaim,
	// Token: 0x04000252 RID: 594
	DoubleQuote,
	// Token: 0x04000253 RID: 595
	Hash,
	// Token: 0x04000254 RID: 596
	Dollar,
	// Token: 0x04000255 RID: 597
	Ampersand = 38,
	// Token: 0x04000256 RID: 598
	Quote,
	// Token: 0x04000257 RID: 599
	LeftParen,
	// Token: 0x04000258 RID: 600
	RightParen,
	// Token: 0x04000259 RID: 601
	Asterisk,
	// Token: 0x0400025A RID: 602
	Plus,
	// Token: 0x0400025B RID: 603
	Comma,
	// Token: 0x0400025C RID: 604
	Minus,
	// Token: 0x0400025D RID: 605
	Period,
	// Token: 0x0400025E RID: 606
	Slash,
	// Token: 0x0400025F RID: 607
	Alpha0,
	// Token: 0x04000260 RID: 608
	Alpha1,
	// Token: 0x04000261 RID: 609
	Alpha2,
	// Token: 0x04000262 RID: 610
	Alpha3,
	// Token: 0x04000263 RID: 611
	Alpha4,
	// Token: 0x04000264 RID: 612
	Alpha5,
	// Token: 0x04000265 RID: 613
	Alpha6,
	// Token: 0x04000266 RID: 614
	Alpha7,
	// Token: 0x04000267 RID: 615
	Alpha8,
	// Token: 0x04000268 RID: 616
	Alpha9,
	// Token: 0x04000269 RID: 617
	Colon,
	// Token: 0x0400026A RID: 618
	Semicolon,
	// Token: 0x0400026B RID: 619
	Less,
	// Token: 0x0400026C RID: 620
	Equals,
	// Token: 0x0400026D RID: 621
	Greater,
	// Token: 0x0400026E RID: 622
	Question,
	// Token: 0x0400026F RID: 623
	At,
	// Token: 0x04000270 RID: 624
	LeftBracket = 91,
	// Token: 0x04000271 RID: 625
	Backslash,
	// Token: 0x04000272 RID: 626
	RightBracket,
	// Token: 0x04000273 RID: 627
	Caret,
	// Token: 0x04000274 RID: 628
	Underscore,
	// Token: 0x04000275 RID: 629
	BackQuote,
	// Token: 0x04000276 RID: 630
	A,
	// Token: 0x04000277 RID: 631
	B,
	// Token: 0x04000278 RID: 632
	C,
	// Token: 0x04000279 RID: 633
	D,
	// Token: 0x0400027A RID: 634
	E,
	// Token: 0x0400027B RID: 635
	F,
	// Token: 0x0400027C RID: 636
	G,
	// Token: 0x0400027D RID: 637
	H,
	// Token: 0x0400027E RID: 638
	I,
	// Token: 0x0400027F RID: 639
	J,
	// Token: 0x04000280 RID: 640
	K,
	// Token: 0x04000281 RID: 641
	L,
	// Token: 0x04000282 RID: 642
	M,
	// Token: 0x04000283 RID: 643
	N,
	// Token: 0x04000284 RID: 644
	O,
	// Token: 0x04000285 RID: 645
	P,
	// Token: 0x04000286 RID: 646
	Q,
	// Token: 0x04000287 RID: 647
	R,
	// Token: 0x04000288 RID: 648
	S,
	// Token: 0x04000289 RID: 649
	T,
	// Token: 0x0400028A RID: 650
	U,
	// Token: 0x0400028B RID: 651
	V,
	// Token: 0x0400028C RID: 652
	W,
	// Token: 0x0400028D RID: 653
	X,
	// Token: 0x0400028E RID: 654
	Y,
	// Token: 0x0400028F RID: 655
	Z,
	// Token: 0x04000290 RID: 656
	Delete = 127,
	// Token: 0x04000291 RID: 657
	Keypad0 = 256,
	// Token: 0x04000292 RID: 658
	Keypad1,
	// Token: 0x04000293 RID: 659
	Keypad2,
	// Token: 0x04000294 RID: 660
	Keypad3,
	// Token: 0x04000295 RID: 661
	Keypad4,
	// Token: 0x04000296 RID: 662
	Keypad5,
	// Token: 0x04000297 RID: 663
	Keypad6,
	// Token: 0x04000298 RID: 664
	Keypad7,
	// Token: 0x04000299 RID: 665
	Keypad8,
	// Token: 0x0400029A RID: 666
	Keypad9,
	// Token: 0x0400029B RID: 667
	KeypadPeriod,
	// Token: 0x0400029C RID: 668
	KeypadDivide,
	// Token: 0x0400029D RID: 669
	KeypadMultiply,
	// Token: 0x0400029E RID: 670
	KeypadMinus,
	// Token: 0x0400029F RID: 671
	KeypadPlus,
	// Token: 0x040002A0 RID: 672
	KeypadEnter,
	// Token: 0x040002A1 RID: 673
	KeypadEquals,
	// Token: 0x040002A2 RID: 674
	UpArrow,
	// Token: 0x040002A3 RID: 675
	DownArrow,
	// Token: 0x040002A4 RID: 676
	RightArrow,
	// Token: 0x040002A5 RID: 677
	LeftArrow,
	// Token: 0x040002A6 RID: 678
	Insert,
	// Token: 0x040002A7 RID: 679
	Home,
	// Token: 0x040002A8 RID: 680
	End,
	// Token: 0x040002A9 RID: 681
	PageUp,
	// Token: 0x040002AA RID: 682
	PageDown,
	// Token: 0x040002AB RID: 683
	F1,
	// Token: 0x040002AC RID: 684
	F2,
	// Token: 0x040002AD RID: 685
	F3,
	// Token: 0x040002AE RID: 686
	F4,
	// Token: 0x040002AF RID: 687
	F5,
	// Token: 0x040002B0 RID: 688
	F6,
	// Token: 0x040002B1 RID: 689
	F7,
	// Token: 0x040002B2 RID: 690
	F8,
	// Token: 0x040002B3 RID: 691
	F9,
	// Token: 0x040002B4 RID: 692
	F10,
	// Token: 0x040002B5 RID: 693
	F11,
	// Token: 0x040002B6 RID: 694
	F12,
	// Token: 0x040002B7 RID: 695
	F13,
	// Token: 0x040002B8 RID: 696
	F14,
	// Token: 0x040002B9 RID: 697
	F15,
	// Token: 0x040002BA RID: 698
	Numlock = 300,
	// Token: 0x040002BB RID: 699
	CapsLock,
	// Token: 0x040002BC RID: 700
	ScrollLock,
	// Token: 0x040002BD RID: 701
	RightShift,
	// Token: 0x040002BE RID: 702
	LeftShift,
	// Token: 0x040002BF RID: 703
	RightControl,
	// Token: 0x040002C0 RID: 704
	LeftControl,
	// Token: 0x040002C1 RID: 705
	RightAlt,
	// Token: 0x040002C2 RID: 706
	LeftAlt,
	// Token: 0x040002C3 RID: 707
	RightApple,
	// Token: 0x040002C4 RID: 708
	RightCommand = 309,
	// Token: 0x040002C5 RID: 709
	LeftApple,
	// Token: 0x040002C6 RID: 710
	LeftCommand = 310,
	// Token: 0x040002C7 RID: 711
	LeftWindows,
	// Token: 0x040002C8 RID: 712
	RightWindows,
	// Token: 0x040002C9 RID: 713
	AltGr,
	// Token: 0x040002CA RID: 714
	Help = 315,
	// Token: 0x040002CB RID: 715
	Print,
	// Token: 0x040002CC RID: 716
	SysReq,
	// Token: 0x040002CD RID: 717
	Break,
	// Token: 0x040002CE RID: 718
	Menu,
	// Token: 0x040002CF RID: 719
	Mouse0 = 323,
	// Token: 0x040002D0 RID: 720
	Mouse1,
	// Token: 0x040002D1 RID: 721
	Mouse2,
	// Token: 0x040002D2 RID: 722
	Mouse3,
	// Token: 0x040002D3 RID: 723
	Mouse4,
	// Token: 0x040002D4 RID: 724
	Mouse5,
	// Token: 0x040002D5 RID: 725
	Mouse6,
	// Token: 0x040002D6 RID: 726
	JoystickButton0,
	// Token: 0x040002D7 RID: 727
	JoystickButton1,
	// Token: 0x040002D8 RID: 728
	JoystickButton2,
	// Token: 0x040002D9 RID: 729
	JoystickButton3,
	// Token: 0x040002DA RID: 730
	JoystickButton4,
	// Token: 0x040002DB RID: 731
	JoystickButton5,
	// Token: 0x040002DC RID: 732
	JoystickButton6,
	// Token: 0x040002DD RID: 733
	JoystickButton7,
	// Token: 0x040002DE RID: 734
	JoystickButton8,
	// Token: 0x040002DF RID: 735
	JoystickButton9,
	// Token: 0x040002E0 RID: 736
	JoystickButton10,
	// Token: 0x040002E1 RID: 737
	JoystickButton11,
	// Token: 0x040002E2 RID: 738
	JoystickButton12,
	// Token: 0x040002E3 RID: 739
	JoystickButton13,
	// Token: 0x040002E4 RID: 740
	JoystickButton14,
	// Token: 0x040002E5 RID: 741
	JoystickButton15,
	// Token: 0x040002E6 RID: 742
	JoystickButton16,
	// Token: 0x040002E7 RID: 743
	JoystickButton17,
	// Token: 0x040002E8 RID: 744
	JoystickButton18,
	// Token: 0x040002E9 RID: 745
	JoystickButton19,
	// Token: 0x040002EA RID: 746
	Joystick1Button0,
	// Token: 0x040002EB RID: 747
	Joystick1Button1,
	// Token: 0x040002EC RID: 748
	Joystick1Button2,
	// Token: 0x040002ED RID: 749
	Joystick1Button3,
	// Token: 0x040002EE RID: 750
	Joystick1Button4,
	// Token: 0x040002EF RID: 751
	Joystick1Button5,
	// Token: 0x040002F0 RID: 752
	Joystick1Button6,
	// Token: 0x040002F1 RID: 753
	Joystick1Button7,
	// Token: 0x040002F2 RID: 754
	Joystick1Button8,
	// Token: 0x040002F3 RID: 755
	Joystick1Button9,
	// Token: 0x040002F4 RID: 756
	Joystick1Button10,
	// Token: 0x040002F5 RID: 757
	Joystick1Button11,
	// Token: 0x040002F6 RID: 758
	Joystick1Button12,
	// Token: 0x040002F7 RID: 759
	Joystick1Button13,
	// Token: 0x040002F8 RID: 760
	Joystick1Button14,
	// Token: 0x040002F9 RID: 761
	Joystick1Button15,
	// Token: 0x040002FA RID: 762
	Joystick1Button16,
	// Token: 0x040002FB RID: 763
	Joystick1Button17,
	// Token: 0x040002FC RID: 764
	Joystick1Button18,
	// Token: 0x040002FD RID: 765
	Joystick1Button19,
	// Token: 0x040002FE RID: 766
	Joystick2Button0,
	// Token: 0x040002FF RID: 767
	Joystick2Button1,
	// Token: 0x04000300 RID: 768
	Joystick2Button2,
	// Token: 0x04000301 RID: 769
	Joystick2Button3,
	// Token: 0x04000302 RID: 770
	Joystick2Button4,
	// Token: 0x04000303 RID: 771
	Joystick2Button5,
	// Token: 0x04000304 RID: 772
	Joystick2Button6,
	// Token: 0x04000305 RID: 773
	Joystick2Button7,
	// Token: 0x04000306 RID: 774
	Joystick2Button8,
	// Token: 0x04000307 RID: 775
	Joystick2Button9,
	// Token: 0x04000308 RID: 776
	Joystick2Button10,
	// Token: 0x04000309 RID: 777
	Joystick2Button11,
	// Token: 0x0400030A RID: 778
	Joystick2Button12,
	// Token: 0x0400030B RID: 779
	Joystick2Button13,
	// Token: 0x0400030C RID: 780
	Joystick2Button14,
	// Token: 0x0400030D RID: 781
	Joystick2Button15,
	// Token: 0x0400030E RID: 782
	Joystick2Button16,
	// Token: 0x0400030F RID: 783
	Joystick2Button17,
	// Token: 0x04000310 RID: 784
	Joystick2Button18,
	// Token: 0x04000311 RID: 785
	Joystick2Button19,
	// Token: 0x04000312 RID: 786
	Joystick3Button0,
	// Token: 0x04000313 RID: 787
	Joystick3Button1,
	// Token: 0x04000314 RID: 788
	Joystick3Button2,
	// Token: 0x04000315 RID: 789
	Joystick3Button3,
	// Token: 0x04000316 RID: 790
	Joystick3Button4,
	// Token: 0x04000317 RID: 791
	Joystick3Button5,
	// Token: 0x04000318 RID: 792
	Joystick3Button6,
	// Token: 0x04000319 RID: 793
	Joystick3Button7,
	// Token: 0x0400031A RID: 794
	Joystick3Button8,
	// Token: 0x0400031B RID: 795
	Joystick3Button9,
	// Token: 0x0400031C RID: 796
	Joystick3Button10,
	// Token: 0x0400031D RID: 797
	Joystick3Button11,
	// Token: 0x0400031E RID: 798
	Joystick3Button12,
	// Token: 0x0400031F RID: 799
	Joystick3Button13,
	// Token: 0x04000320 RID: 800
	Joystick3Button14,
	// Token: 0x04000321 RID: 801
	Joystick3Button15,
	// Token: 0x04000322 RID: 802
	Joystick3Button16,
	// Token: 0x04000323 RID: 803
	Joystick3Button17,
	// Token: 0x04000324 RID: 804
	Joystick3Button18,
	// Token: 0x04000325 RID: 805
	Joystick3Button19,
	// Token: 0x04000326 RID: 806
	Joystick4Button0,
	// Token: 0x04000327 RID: 807
	Joystick4Button1,
	// Token: 0x04000328 RID: 808
	Joystick4Button2,
	// Token: 0x04000329 RID: 809
	Joystick4Button3,
	// Token: 0x0400032A RID: 810
	Joystick4Button4,
	// Token: 0x0400032B RID: 811
	Joystick4Button5,
	// Token: 0x0400032C RID: 812
	Joystick4Button6,
	// Token: 0x0400032D RID: 813
	Joystick4Button7,
	// Token: 0x0400032E RID: 814
	Joystick4Button8,
	// Token: 0x0400032F RID: 815
	Joystick4Button9,
	// Token: 0x04000330 RID: 816
	Joystick4Button10,
	// Token: 0x04000331 RID: 817
	Joystick4Button11,
	// Token: 0x04000332 RID: 818
	Joystick4Button12,
	// Token: 0x04000333 RID: 819
	Joystick4Button13,
	// Token: 0x04000334 RID: 820
	Joystick4Button14,
	// Token: 0x04000335 RID: 821
	Joystick4Button15,
	// Token: 0x04000336 RID: 822
	Joystick4Button16,
	// Token: 0x04000337 RID: 823
	Joystick4Button17,
	// Token: 0x04000338 RID: 824
	Joystick4Button18,
	// Token: 0x04000339 RID: 825
	Joystick4Button19,
	// Token: 0x0400033A RID: 826
	KleiKeys = 1000,
	// Token: 0x0400033B RID: 827
	MouseScrollDown,
	// Token: 0x0400033C RID: 828
	MouseScrollUp
}
