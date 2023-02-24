using System;

// Token: 0x020005AB RID: 1451
public interface INavDoor
{
	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x060023CA RID: 9162
	bool isSpawned { get; }

	// Token: 0x060023CB RID: 9163
	bool IsOpen();

	// Token: 0x060023CC RID: 9164
	void Open();

	// Token: 0x060023CD RID: 9165
	void Close();
}
