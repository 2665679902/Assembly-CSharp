using System;
using System.Collections.Generic;
using Klei.CustomSettings;
using KSerialization;
using TemplateClasses;

namespace ProcGenGame
{
	// Token: 0x02000C3C RID: 3132
	[SerializationConfig(MemberSerialization.OptOut)]
	public class GameSpawnData
	{
		// Token: 0x06006330 RID: 25392 RVA: 0x0024C904 File Offset: 0x0024AB04
		public void AddRange(IEnumerable<KeyValuePair<int, string>> newItems)
		{
			foreach (KeyValuePair<int, string> keyValuePair in newItems)
			{
				Vector2I vector2I = Grid.CellToXY(keyValuePair.Key);
				Prefab prefab = new Prefab(keyValuePair.Value, Prefab.Type.Other, vector2I.x, vector2I.y, (SimHashes)0, -1f, 1f, null, 0, Orientation.Neutral, null, null, 0);
				this.otherEntities.Add(prefab);
			}
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x0024C98C File Offset: 0x0024AB8C
		public void AddTemplate(TemplateContainer template, Vector2I position, ref Dictionary<int, int> claimedCells)
		{
			int num = Grid.XYToCell(position.x, position.y);
			bool flag = true;
			if (DlcManager.IsExpansion1Active() && CustomGameSettings.Instance != null)
			{
				flag = CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.Teleporters).id == "Enabled";
			}
			if (template.buildings != null)
			{
				foreach (Prefab prefab in template.buildings)
				{
					if (!claimedCells.ContainsKey(Grid.OffsetCell(num, prefab.location_x, prefab.location_y)) && (flag || !this.IsWarpTeleporter(prefab)))
					{
						this.buildings.Add(prefab.Clone(position));
					}
				}
			}
			if (template.pickupables != null)
			{
				foreach (Prefab prefab2 in template.pickupables)
				{
					if (!claimedCells.ContainsKey(Grid.OffsetCell(num, prefab2.location_x, prefab2.location_y)))
					{
						this.pickupables.Add(prefab2.Clone(position));
					}
				}
			}
			if (template.elementalOres != null)
			{
				foreach (Prefab prefab3 in template.elementalOres)
				{
					if (!claimedCells.ContainsKey(Grid.OffsetCell(num, prefab3.location_x, prefab3.location_y)))
					{
						this.elementalOres.Add(prefab3.Clone(position));
					}
				}
			}
			if (template.otherEntities != null)
			{
				foreach (Prefab prefab4 in template.otherEntities)
				{
					if (!claimedCells.ContainsKey(Grid.OffsetCell(num, prefab4.location_x, prefab4.location_y)) && (flag || !this.IsWarpTeleporter(prefab4)))
					{
						this.otherEntities.Add(prefab4.Clone(position));
					}
				}
			}
			if (template.cells != null)
			{
				for (int i = 0; i < template.cells.Count; i++)
				{
					int num2 = Grid.XYToCell(position.x + template.cells[i].location_x, position.y + template.cells[i].location_y);
					if (!claimedCells.ContainsKey(num2))
					{
						claimedCells[num2] = 1;
						this.preventFoWReveal.Add(new KeyValuePair<Vector2I, bool>(new Vector2I(position.x + template.cells[i].location_x, position.y + template.cells[i].location_y), template.cells[i].preventFoWReveal));
					}
					else
					{
						Dictionary<int, int> dictionary = claimedCells;
						int num3 = num2;
						dictionary[num3]++;
					}
				}
			}
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x0024CCBC File Offset: 0x0024AEBC
		private bool IsWarpTeleporter(Prefab prefab)
		{
			return prefab.id == "WarpPortal" || prefab.id == WarpReceiverConfig.ID || prefab.id == "WarpConduitSender" || prefab.id == "WarpConduitReceiver";
		}

		// Token: 0x040044D9 RID: 17625
		public Vector2I baseStartPos;

		// Token: 0x040044DA RID: 17626
		public List<Prefab> buildings = new List<Prefab>();

		// Token: 0x040044DB RID: 17627
		public List<Prefab> pickupables = new List<Prefab>();

		// Token: 0x040044DC RID: 17628
		public List<Prefab> elementalOres = new List<Prefab>();

		// Token: 0x040044DD RID: 17629
		public List<Prefab> otherEntities = new List<Prefab>();

		// Token: 0x040044DE RID: 17630
		public List<KeyValuePair<Vector2I, bool>> preventFoWReveal = new List<KeyValuePair<Vector2I, bool>>();
	}
}
