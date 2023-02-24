using System;

// Token: 0x0200086C RID: 2156
public class NonEssentialEnergyConsumer : EnergyConsumer
{
	// Token: 0x17000453 RID: 1107
	// (get) Token: 0x06003DF0 RID: 15856 RVA: 0x00159AF9 File Offset: 0x00157CF9
	// (set) Token: 0x06003DF1 RID: 15857 RVA: 0x00159B01 File Offset: 0x00157D01
	public override bool IsPowered
	{
		get
		{
			return this.isPowered;
		}
		protected set
		{
			if (value == this.isPowered)
			{
				return;
			}
			this.isPowered = value;
			Action<bool> poweredStateChanged = this.PoweredStateChanged;
			if (poweredStateChanged == null)
			{
				return;
			}
			poweredStateChanged(this.isPowered);
		}
	}

	// Token: 0x0400288D RID: 10381
	public Action<bool> PoweredStateChanged;

	// Token: 0x0400288E RID: 10382
	private bool isPowered;
}
