using System;
using System.Collections.Generic;

namespace YamlDotNet.RepresentationModel
{
	// Token: 0x020001DE RID: 478
	internal class EmitterState
	{
		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0003B9D6 File Offset: 0x00039BD6
		public HashSet<string> EmittedAnchors
		{
			get
			{
				return this.emittedAnchors;
			}
		}

		// Token: 0x04000848 RID: 2120
		private readonly HashSet<string> emittedAnchors = new HashSet<string>();
	}
}
