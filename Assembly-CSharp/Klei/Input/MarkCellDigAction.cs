using System;
using Klei.Actions;
using UnityEngine;

namespace Klei.Input
{
	// Token: 0x02000DAE RID: 3502
	[Action("Mark Cell")]
	public class MarkCellDigAction : DigAction
	{
		// Token: 0x06006A84 RID: 27268 RVA: 0x0029541C File Offset: 0x0029361C
		public override void Dig(int cell, int distFromOrigin)
		{
			GameObject gameObject = DigTool.PlaceDig(cell, distFromOrigin);
			if (gameObject != null)
			{
				Prioritizable component = gameObject.GetComponent<Prioritizable>();
				if (component != null)
				{
					component.SetMasterPriority(ToolMenu.Instance.PriorityScreen.GetLastSelectedPriority());
				}
			}
		}

		// Token: 0x06006A85 RID: 27269 RVA: 0x0029545F File Offset: 0x0029365F
		protected override void Uproot(Uprootable uprootable)
		{
			if (uprootable == null)
			{
				return;
			}
			uprootable.MarkForUproot(true);
		}
	}
}
