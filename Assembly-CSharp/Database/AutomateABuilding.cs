﻿using System;
using STRINGS;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CE2 RID: 3298
	public class AutomateABuilding : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066C0 RID: 26304 RVA: 0x0027788C File Offset: 0x00275A8C
		public override bool Success()
		{
			foreach (UtilityNetwork utilityNetwork in Game.Instance.logicCircuitSystem.GetNetworks())
			{
				LogicCircuitNetwork logicCircuitNetwork = (LogicCircuitNetwork)utilityNetwork;
				if (logicCircuitNetwork.Receivers.Count > 0 && logicCircuitNetwork.Senders.Count > 0)
				{
					bool flag = false;
					foreach (ILogicEventReceiver logicEventReceiver in logicCircuitNetwork.Receivers)
					{
						GameObject gameObject = Grid.Objects[logicEventReceiver.GetLogicCell(), 1];
						if (gameObject != null && !gameObject.GetComponent<KPrefabID>().HasTag(GameTags.TemplateBuilding))
						{
							flag = true;
							break;
						}
					}
					bool flag2 = false;
					foreach (ILogicEventSender logicEventSender in logicCircuitNetwork.Senders)
					{
						GameObject gameObject2 = Grid.Objects[logicEventSender.GetLogicCell(), 1];
						if (gameObject2 != null && !gameObject2.GetComponent<KPrefabID>().HasTag(GameTags.TemplateBuilding))
						{
							flag2 = true;
							break;
						}
					}
					if (flag && flag2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060066C1 RID: 26305 RVA: 0x00277A20 File Offset: 0x00275C20
		public void Deserialize(IReader reader)
		{
		}

		// Token: 0x060066C2 RID: 26306 RVA: 0x00277A22 File Offset: 0x00275C22
		public override string GetProgress(bool complete)
		{
			return COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.AUTOMATE_A_BUILDING;
		}
	}
}
