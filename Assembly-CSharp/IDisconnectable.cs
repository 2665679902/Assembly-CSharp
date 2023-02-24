using System;

// Token: 0x02000671 RID: 1649
public interface IDisconnectable
{
	// Token: 0x06002C77 RID: 11383
	bool Connect();

	// Token: 0x06002C78 RID: 11384
	void Disconnect();

	// Token: 0x06002C79 RID: 11385
	bool IsDisconnected();
}
