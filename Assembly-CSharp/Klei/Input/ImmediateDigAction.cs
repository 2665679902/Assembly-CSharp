using System;
using Klei.Actions;

namespace Klei.Input
{
	// Token: 0x02000DAF RID: 3503
	[Action("Immediate")]
	public class ImmediateDigAction : DigAction
	{
		// Token: 0x06006A87 RID: 27271 RVA: 0x0029547A File Offset: 0x0029367A
		public override void Dig(int cell, int distFromOrigin)
		{
			if (Grid.Solid[cell] && !Grid.Foundation[cell])
			{
				SimMessages.Dig(cell, -1, false);
			}
		}

		// Token: 0x06006A88 RID: 27272 RVA: 0x0029549E File Offset: 0x0029369E
		protected override void Uproot(Uprootable uprootable)
		{
			if (uprootable == null)
			{
				return;
			}
			uprootable.Uproot();
		}
	}
}
