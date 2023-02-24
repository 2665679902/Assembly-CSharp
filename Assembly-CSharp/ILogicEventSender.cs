using System;

// Token: 0x020007FD RID: 2045
public interface ILogicEventSender : ILogicNetworkConnection
{
	// Token: 0x06003B1E RID: 15134
	void LogicTick();

	// Token: 0x06003B1F RID: 15135
	int GetLogicCell();

	// Token: 0x06003B20 RID: 15136
	int GetLogicValue();
}
