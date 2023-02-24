using System;
using Klei.Actions;

namespace Klei.Input
{
	// Token: 0x02000DB0 RID: 3504
	[Action("Clear Cell")]
	public class ClearCellDigAction : DigAction
	{
		// Token: 0x06006A8A RID: 27274 RVA: 0x002954B8 File Offset: 0x002936B8
		public override void Dig(int cell, int distFromOrigin)
		{
			if (Grid.Solid[cell] && !Grid.Foundation[cell])
			{
				SimMessages.Dig(cell, -1, true);
			}
		}

		// Token: 0x06006A8B RID: 27275 RVA: 0x002954DC File Offset: 0x002936DC
		protected override void Uproot(Uprootable uprootable)
		{
			if (uprootable == null)
			{
				return;
			}
			uprootable.AddTag(GameTags.Uprooted);
		}
	}
}
