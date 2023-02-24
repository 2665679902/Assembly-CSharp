using System;
using System.Collections.Generic;
using STRINGS;

namespace Database
{
	// Token: 0x02000CEF RID: 3311
	public class TeleportDuplicant : ColonyAchievementRequirement
	{
		// Token: 0x060066F2 RID: 26354 RVA: 0x002783C0 File Offset: 0x002765C0
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.TELEPORT_DUPLICANT;
		}

		// Token: 0x060066F3 RID: 26355 RVA: 0x002783CC File Offset: 0x002765CC
		public override bool Success()
		{
			using (List<WarpReceiver>.Enumerator enumerator = Components.WarpReceivers.Items.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Used)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
