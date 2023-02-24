using System;
using Klei.Input;

namespace Klei.Actions
{
	// Token: 0x02000DB4 RID: 3508
	public class DigToolActionFactory : ActionFactory<DigToolActionFactory, DigAction, DigToolActionFactory.Actions>
	{
		// Token: 0x06006A98 RID: 27288 RVA: 0x0029562B File Offset: 0x0029382B
		protected override DigAction CreateAction(DigToolActionFactory.Actions action)
		{
			if (action == DigToolActionFactory.Actions.Immediate)
			{
				return new ImmediateDigAction();
			}
			if (action == DigToolActionFactory.Actions.ClearCell)
			{
				return new ClearCellDigAction();
			}
			if (action == DigToolActionFactory.Actions.MarkCell)
			{
				return new MarkCellDigAction();
			}
			throw new InvalidOperationException("Can not create DigAction 'Count'. Please provide a valid action.");
		}

		// Token: 0x02001E7F RID: 7807
		public enum Actions
		{
			// Token: 0x040088F7 RID: 35063
			MarkCell = 145163119,
			// Token: 0x040088F8 RID: 35064
			Immediate = -1044758767,
			// Token: 0x040088F9 RID: 35065
			ClearCell = -1011242513,
			// Token: 0x040088FA RID: 35066
			Count = -1427607121
		}
	}
}
