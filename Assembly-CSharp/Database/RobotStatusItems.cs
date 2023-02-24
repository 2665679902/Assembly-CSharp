using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000CAA RID: 3242
	public class RobotStatusItems : StatusItems
	{
		// Token: 0x060065D2 RID: 26066 RVA: 0x0026F415 File Offset: 0x0026D615
		public RobotStatusItems(ResourceSet parent)
			: base("RobotStatusItems", parent)
		{
			this.CreateStatusItems();
		}

		// Token: 0x060065D3 RID: 26067 RVA: 0x0026F42C File Offset: 0x0026D62C
		private void CreateStatusItems()
		{
			this.CantReachStation = new StatusItem("CantReachStation", "ROBOTS", "status_item_exclamation", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.CantReachStation.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject = (GameObject)data;
				return str.Replace("{0}", gameObject.GetProperName());
			};
			this.LowBattery = new StatusItem("LowBattery", "ROBOTS", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.LowBattery.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject2 = (GameObject)data;
				return str.Replace("{0}", gameObject2.GetProperName());
			};
			this.LowBatteryNoCharge = new StatusItem("LowBatteryNoCharge", "ROBOTS", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.LowBatteryNoCharge.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject3 = (GameObject)data;
				return str.Replace("{0}", gameObject3.GetProperName());
			};
			this.DeadBattery = new StatusItem("DeadBattery", "ROBOTS", "status_item_need_power", StatusItem.IconType.Custom, NotificationType.BadMinor, false, OverlayModes.None.ID, false, 129022, null);
			this.DeadBattery.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject4 = (GameObject)data;
				return str.Replace("{0}", gameObject4.GetProperName());
			};
			this.DustBinFull = new StatusItem("DustBinFull", "ROBOTS", "status_item_pending_clear", StatusItem.IconType.Custom, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.DustBinFull.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject5 = (GameObject)data;
				return str.Replace("{0}", gameObject5.GetProperName());
			};
			this.Working = new StatusItem("Working", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.Working.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject6 = (GameObject)data;
				return str.Replace("{0}", gameObject6.GetProperName());
			};
			this.MovingToChargeStation = new StatusItem("MovingToChargeStation", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.MovingToChargeStation.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject7 = (GameObject)data;
				return str.Replace("{0}", gameObject7.GetProperName());
			};
			this.UnloadingStorage = new StatusItem("UnloadingStorage", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.UnloadingStorage.resolveStringCallback = delegate(string str, object data)
			{
				GameObject gameObject8 = (GameObject)data;
				return str.Replace("{0}", gameObject8.GetProperName());
			};
			this.ReactPositive = new StatusItem("ReactPositive", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.ReactPositive.resolveStringCallback = (string str, object data) => str;
			this.ReactNegative = new StatusItem("ReactNegative", "ROBOTS", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, false, 129022, null);
			this.ReactNegative.resolveStringCallback = (string str, object data) => str;
		}

		// Token: 0x040049DE RID: 18910
		public StatusItem LowBattery;

		// Token: 0x040049DF RID: 18911
		public StatusItem LowBatteryNoCharge;

		// Token: 0x040049E0 RID: 18912
		public StatusItem DeadBattery;

		// Token: 0x040049E1 RID: 18913
		public StatusItem CantReachStation;

		// Token: 0x040049E2 RID: 18914
		public StatusItem DustBinFull;

		// Token: 0x040049E3 RID: 18915
		public StatusItem Working;

		// Token: 0x040049E4 RID: 18916
		public StatusItem UnloadingStorage;

		// Token: 0x040049E5 RID: 18917
		public StatusItem ReactPositive;

		// Token: 0x040049E6 RID: 18918
		public StatusItem ReactNegative;

		// Token: 0x040049E7 RID: 18919
		public StatusItem MovingToChargeStation;
	}
}
