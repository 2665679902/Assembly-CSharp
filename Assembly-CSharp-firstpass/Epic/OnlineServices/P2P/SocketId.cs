using System;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x0200070B RID: 1803
	public class SocketId
	{
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06004422 RID: 17442 RVA: 0x0008C197 File Offset: 0x0008A397
		public int ApiVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x0008C19A File Offset: 0x0008A39A
		// (set) Token: 0x06004424 RID: 17444 RVA: 0x0008C1A2 File Offset: 0x0008A3A2
		public string SocketName { get; set; }
	}
}
