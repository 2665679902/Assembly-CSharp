using System;

// Token: 0x02000596 RID: 1430
public class ConduitBridgeBase : KMonoBehaviour
{
	// Token: 0x0600231A RID: 8986 RVA: 0x000BE406 File Offset: 0x000BC606
	protected void SendEmptyOnMassTransfer()
	{
		if (this.OnMassTransfer != null)
		{
			this.OnMassTransfer(SimHashes.Void, 0f, 0f, 0, 0, null);
		}
	}

	// Token: 0x04001433 RID: 5171
	public ConduitBridgeBase.DesiredMassTransfer desiredMassTransfer;

	// Token: 0x04001434 RID: 5172
	public ConduitBridgeBase.ConduitBridgeEvent OnMassTransfer;

	// Token: 0x020011C9 RID: 4553
	// (Invoke) Token: 0x0600780C RID: 30732
	public delegate float DesiredMassTransfer(float dt, SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable);

	// Token: 0x020011CA RID: 4554
	// (Invoke) Token: 0x06007810 RID: 30736
	public delegate void ConduitBridgeEvent(SimHashes element, float mass, float temperature, byte disease_idx, int disease_count, Pickupable pickupable);
}
