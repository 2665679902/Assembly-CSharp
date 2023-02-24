using System;
using STRINGS;

// Token: 0x020008F5 RID: 2293
public class RoomDetails
{
	// Token: 0x0600423B RID: 16955 RVA: 0x001753F8 File Offset: 0x001735F8
	public static string RoomDetailString(Room room)
	{
		string text = "";
		text = text + "<b>" + ROOMS.DETAILS.HEADER + "</b>";
		foreach (RoomDetails.Detail detail in room.roomType.display_details)
		{
			text = text + "\n    • " + detail.resolve_string_function(room);
		}
		return text;
	}

	// Token: 0x04002C36 RID: 11318
	public static readonly RoomDetails.Detail AVERAGE_TEMPERATURE = new RoomDetails.Detail(delegate(Room room)
	{
		float num = 0f;
		if (num == 0f)
		{
			return string.Format(ROOMS.DETAILS.AVERAGE_TEMPERATURE.NAME, UI.OVERLAYS.TEMPERATURE.EXTREMECOLD);
		}
		return string.Format(ROOMS.DETAILS.AVERAGE_TEMPERATURE.NAME, GameUtil.GetFormattedTemperature(num, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false));
	});

	// Token: 0x04002C37 RID: 11319
	public static readonly RoomDetails.Detail AVERAGE_ATMO_MASS = new RoomDetails.Detail(delegate(Room room)
	{
		float num2 = 0f;
		float num3 = 0f;
		if (num3 > 0f)
		{
			num2 /= num3;
		}
		else
		{
			num2 = 0f;
		}
		return string.Format(ROOMS.DETAILS.AVERAGE_ATMO_MASS.NAME, GameUtil.GetFormattedMass(num2, GameUtil.TimeSlice.None, GameUtil.MetricMassFormat.UseThreshold, true, "{0:0.#}"));
	});

	// Token: 0x04002C38 RID: 11320
	public static readonly RoomDetails.Detail ASSIGNED_TO = new RoomDetails.Detail(delegate(Room room)
	{
		string text = "";
		foreach (KPrefabID kprefabID in room.GetPrimaryEntities())
		{
			if (!(kprefabID == null))
			{
				Assignable component = kprefabID.GetComponent<Assignable>();
				if (!(component == null))
				{
					IAssignableIdentity assignee = component.assignee;
					if (assignee == null)
					{
						text += ((text == "") ? ("<color=#BCBCBC>    • " + kprefabID.GetProperName() + ": " + ROOMS.DETAILS.ASSIGNED_TO.UNASSIGNED) : ("\n<color=#BCBCBC>    • " + kprefabID.GetProperName() + ": " + ROOMS.DETAILS.ASSIGNED_TO.UNASSIGNED));
						text += "</color>";
					}
					else
					{
						text += ((text == "") ? ("    • " + kprefabID.GetProperName() + ": " + assignee.GetProperName()) : ("\n    • " + kprefabID.GetProperName() + ": " + assignee.GetProperName()));
					}
				}
			}
		}
		if (text == "")
		{
			text = ROOMS.DETAILS.ASSIGNED_TO.UNASSIGNED;
		}
		return string.Format(ROOMS.DETAILS.ASSIGNED_TO.NAME, text);
	});

	// Token: 0x04002C39 RID: 11321
	public static readonly RoomDetails.Detail SIZE = new RoomDetails.Detail((Room room) => string.Format(ROOMS.DETAILS.SIZE.NAME, room.cavity.numCells));

	// Token: 0x04002C3A RID: 11322
	public static readonly RoomDetails.Detail BUILDING_COUNT = new RoomDetails.Detail((Room room) => string.Format(ROOMS.DETAILS.BUILDING_COUNT.NAME, room.buildings.Count));

	// Token: 0x04002C3B RID: 11323
	public static readonly RoomDetails.Detail CREATURE_COUNT = new RoomDetails.Detail((Room room) => string.Format(ROOMS.DETAILS.CREATURE_COUNT.NAME, room.cavity.creatures.Count + room.cavity.eggs.Count));

	// Token: 0x04002C3C RID: 11324
	public static readonly RoomDetails.Detail PLANT_COUNT = new RoomDetails.Detail((Room room) => string.Format(ROOMS.DETAILS.PLANT_COUNT.NAME, room.cavity.plants.Count));

	// Token: 0x04002C3D RID: 11325
	public static readonly RoomDetails.Detail EFFECT = new RoomDetails.Detail((Room room) => room.roomType.effect);

	// Token: 0x04002C3E RID: 11326
	public static readonly RoomDetails.Detail EFFECTS = new RoomDetails.Detail((Room room) => room.roomType.GetRoomEffectsString());

	// Token: 0x020016C1 RID: 5825
	public class Detail
	{
		// Token: 0x06008894 RID: 34964 RVA: 0x002F5FA0 File Offset: 0x002F41A0
		public Detail(Func<Room, string> resolve_string_function)
		{
			this.resolve_string_function = resolve_string_function;
		}

		// Token: 0x04006AE2 RID: 27362
		public Func<Room, string> resolve_string_function;
	}
}
