using System;

// Token: 0x020007FE RID: 2046
public interface ILogicEventReceiver : ILogicNetworkConnection
{
	// Token: 0x06003B21 RID: 15137
	void ReceiveLogicEvent(int value);

	// Token: 0x06003B22 RID: 15138
	int GetLogicCell();
}
