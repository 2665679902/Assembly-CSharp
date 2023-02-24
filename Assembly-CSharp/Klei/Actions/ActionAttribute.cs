using System;

namespace Klei.Actions
{
	// Token: 0x02000DB2 RID: 3506
	[AttributeUsage(AttributeTargets.Class)]
	public class ActionAttribute : Attribute
	{
		// Token: 0x06006A92 RID: 27282 RVA: 0x00295582 File Offset: 0x00293782
		public ActionAttribute(string actionName)
		{
			this.ActionName = actionName;
		}

		// Token: 0x04005003 RID: 20483
		public readonly string ActionName;
	}
}
