using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CDF RID: 3295
	public class VentXKG : ColonyAchievementRequirement, AchievementRequirementSerialization_Deprecated
	{
		// Token: 0x060066B2 RID: 26290 RVA: 0x0027736B File Offset: 0x0027556B
		public VentXKG(SimHashes element, float kilogramsToVent)
		{
			this.element = element;
			this.kilogramsToVent = kilogramsToVent;
		}

		// Token: 0x060066B3 RID: 26291 RVA: 0x00277384 File Offset: 0x00275584
		public override bool Success()
		{
			float num = 0f;
			foreach (UtilityNetwork utilityNetwork in Conduit.GetNetworkManager(ConduitType.Gas).GetNetworks())
			{
				FlowUtilityNetwork flowUtilityNetwork = utilityNetwork as FlowUtilityNetwork;
				if (flowUtilityNetwork != null)
				{
					foreach (FlowUtilityNetwork.IItem item in flowUtilityNetwork.sinks)
					{
						Vent component = item.GameObject.GetComponent<Vent>();
						if (component != null)
						{
							num += component.GetVentedMass(this.element);
						}
					}
				}
			}
			return num >= this.kilogramsToVent;
		}

		// Token: 0x060066B4 RID: 26292 RVA: 0x0027744C File Offset: 0x0027564C
		public void Deserialize(IReader reader)
		{
			this.element = (SimHashes)reader.ReadInt32();
			this.kilogramsToVent = reader.ReadSingle();
		}

		// Token: 0x060066B5 RID: 26293 RVA: 0x00277468 File Offset: 0x00275668
		public override string GetProgress(bool complete)
		{
			float num = 0f;
			foreach (UtilityNetwork utilityNetwork in Conduit.GetNetworkManager(ConduitType.Gas).GetNetworks())
			{
				FlowUtilityNetwork flowUtilityNetwork = utilityNetwork as FlowUtilityNetwork;
				if (flowUtilityNetwork != null)
				{
					foreach (FlowUtilityNetwork.IItem item in flowUtilityNetwork.sinks)
					{
						Vent component = item.GameObject.GetComponent<Vent>();
						if (component != null)
						{
							num += component.GetVentedMass(this.element);
						}
					}
				}
			}
			return string.Format(COLONY_ACHIEVEMENTS.MISC_REQUIREMENTS.STATUS.VENTED_MASS, GameUtil.GetFormattedMass(complete ? this.kilogramsToVent : num, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"), GameUtil.GetFormattedMass(this.kilogramsToVent, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.Kilogram, true, "{0:0.#}"));
		}

		// Token: 0x04004AE1 RID: 19169
		private SimHashes element;

		// Token: 0x04004AE2 RID: 19170
		private float kilogramsToVent;
	}
}
