using System;
using System.Collections.Generic;

// Token: 0x02000815 RID: 2069
public interface IGroupProber
{
	// Token: 0x06003C06 RID: 15366
	void Occupy(object prober, short serial_no, IEnumerable<int> cells);

	// Token: 0x06003C07 RID: 15367
	void SetValidSerialNos(object prober, short previous_serial_no, short serial_no);

	// Token: 0x06003C08 RID: 15368
	bool ReleaseProber(object prober);
}
