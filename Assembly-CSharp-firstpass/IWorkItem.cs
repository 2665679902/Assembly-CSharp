using System;

// Token: 0x020000A8 RID: 168
public interface IWorkItem<SharedDataType>
{
	// Token: 0x06000653 RID: 1619
	void Run(SharedDataType shared_data);
}
