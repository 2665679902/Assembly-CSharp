using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x02000452 RID: 1106
	public class RailGetImageDataResult : EventBase
	{
		// Token: 0x0400109F RID: 4255
		public List<byte> image_data = new List<byte>();

		// Token: 0x040010A0 RID: 4256
		public RailImageDataDescriptor image_data_descriptor = new RailImageDataDescriptor();
	}
}
