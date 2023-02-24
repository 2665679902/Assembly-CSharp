using System;
using Klei.AI;
using STRINGS;

// Token: 0x0200050E RID: 1294
public class RoomType : Resource
{
	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x000A56E8 File Offset: 0x000A38E8
	// (set) Token: 0x06001EF6 RID: 7926 RVA: 0x000A56F0 File Offset: 0x000A38F0
	public string tooltip { get; private set; }

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x000A56F9 File Offset: 0x000A38F9
	// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x000A5701 File Offset: 0x000A3901
	public string description { get; set; }

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x000A570A File Offset: 0x000A390A
	// (set) Token: 0x06001EFA RID: 7930 RVA: 0x000A5712 File Offset: 0x000A3912
	public string effect { get; private set; }

	// Token: 0x1700016B RID: 363
	// (get) Token: 0x06001EFB RID: 7931 RVA: 0x000A571B File Offset: 0x000A391B
	// (set) Token: 0x06001EFC RID: 7932 RVA: 0x000A5723 File Offset: 0x000A3923
	public RoomConstraints.Constraint primary_constraint { get; private set; }

	// Token: 0x1700016C RID: 364
	// (get) Token: 0x06001EFD RID: 7933 RVA: 0x000A572C File Offset: 0x000A392C
	// (set) Token: 0x06001EFE RID: 7934 RVA: 0x000A5734 File Offset: 0x000A3934
	public RoomConstraints.Constraint[] additional_constraints { get; private set; }

	// Token: 0x1700016D RID: 365
	// (get) Token: 0x06001EFF RID: 7935 RVA: 0x000A573D File Offset: 0x000A393D
	// (set) Token: 0x06001F00 RID: 7936 RVA: 0x000A5745 File Offset: 0x000A3945
	public int priority { get; private set; }

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x06001F01 RID: 7937 RVA: 0x000A574E File Offset: 0x000A394E
	// (set) Token: 0x06001F02 RID: 7938 RVA: 0x000A5756 File Offset: 0x000A3956
	public bool single_assignee { get; private set; }

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06001F03 RID: 7939 RVA: 0x000A575F File Offset: 0x000A395F
	// (set) Token: 0x06001F04 RID: 7940 RVA: 0x000A5767 File Offset: 0x000A3967
	public RoomDetails.Detail[] display_details { get; private set; }

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06001F05 RID: 7941 RVA: 0x000A5770 File Offset: 0x000A3970
	// (set) Token: 0x06001F06 RID: 7942 RVA: 0x000A5778 File Offset: 0x000A3978
	public bool priority_building_use { get; private set; }

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06001F07 RID: 7943 RVA: 0x000A5781 File Offset: 0x000A3981
	// (set) Token: 0x06001F08 RID: 7944 RVA: 0x000A5789 File Offset: 0x000A3989
	public RoomTypeCategory category { get; private set; }

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000A5792 File Offset: 0x000A3992
	// (set) Token: 0x06001F0A RID: 7946 RVA: 0x000A579A File Offset: 0x000A399A
	public RoomType[] upgrade_paths { get; private set; }

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06001F0B RID: 7947 RVA: 0x000A57A3 File Offset: 0x000A39A3
	// (set) Token: 0x06001F0C RID: 7948 RVA: 0x000A57AB File Offset: 0x000A39AB
	public string[] effects { get; private set; }

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06001F0D RID: 7949 RVA: 0x000A57B4 File Offset: 0x000A39B4
	// (set) Token: 0x06001F0E RID: 7950 RVA: 0x000A57BC File Offset: 0x000A39BC
	public int sortKey { get; private set; }

	// Token: 0x06001F0F RID: 7951 RVA: 0x000A57C8 File Offset: 0x000A39C8
	public RoomType(string id, string name, string description, string tooltip, string effect, RoomTypeCategory category, RoomConstraints.Constraint primary_constraint, RoomConstraints.Constraint[] additional_constraints, RoomDetails.Detail[] display_details, int priority = 0, RoomType[] upgrade_paths = null, bool single_assignee = false, bool priority_building_use = false, string[] effects = null, int sortKey = 0)
		: base(id, name)
	{
		this.tooltip = tooltip;
		this.description = description;
		this.effect = effect;
		this.category = category;
		this.primary_constraint = primary_constraint;
		this.additional_constraints = additional_constraints;
		this.display_details = display_details;
		this.priority = priority;
		this.upgrade_paths = upgrade_paths;
		this.single_assignee = single_assignee;
		this.priority_building_use = priority_building_use;
		this.effects = effects;
		this.sortKey = sortKey;
		if (this.upgrade_paths != null)
		{
			RoomType[] upgrade_paths2 = this.upgrade_paths;
			for (int i = 0; i < upgrade_paths2.Length; i++)
			{
				Debug.Assert(upgrade_paths2[i] != null, name + " has a null upgrade path. Maybe it wasn't initialized yet.");
			}
		}
	}

	// Token: 0x06001F10 RID: 7952 RVA: 0x000A5878 File Offset: 0x000A3A78
	public RoomType.RoomIdentificationResult isSatisfactory(Room candidate_room)
	{
		if (this.primary_constraint != null && !this.primary_constraint.isSatisfied(candidate_room))
		{
			return RoomType.RoomIdentificationResult.primary_unsatisfied;
		}
		if (this.additional_constraints != null)
		{
			RoomConstraints.Constraint[] additional_constraints = this.additional_constraints;
			for (int i = 0; i < additional_constraints.Length; i++)
			{
				if (!additional_constraints[i].isSatisfied(candidate_room))
				{
					return RoomType.RoomIdentificationResult.primary_satisfied;
				}
			}
		}
		return RoomType.RoomIdentificationResult.all_satisfied;
	}

	// Token: 0x06001F11 RID: 7953 RVA: 0x000A58C8 File Offset: 0x000A3AC8
	public string GetCriteriaString()
	{
		string text = string.Concat(new string[]
		{
			"<b>",
			this.Name,
			"</b>\n",
			this.tooltip,
			"\n\n",
			ROOMS.CRITERIA.HEADER
		});
		if (this == Db.Get().RoomTypes.Neutral)
		{
			text = text + "\n    • " + ROOMS.CRITERIA.NEUTRAL_TYPE;
		}
		text += ((this.primary_constraint == null) ? "" : ("\n    • " + this.primary_constraint.name));
		if (this.additional_constraints != null)
		{
			foreach (RoomConstraints.Constraint constraint in this.additional_constraints)
			{
				text = text + "\n    • " + constraint.name;
			}
		}
		return text;
	}

	// Token: 0x06001F12 RID: 7954 RVA: 0x000A59A0 File Offset: 0x000A3BA0
	public string GetRoomEffectsString()
	{
		if (this.effects != null && this.effects.Length != 0)
		{
			string text = ROOMS.EFFECTS.HEADER;
			foreach (string text2 in this.effects)
			{
				Effect effect = Db.Get().effects.Get(text2);
				text += Effect.CreateTooltip(effect, false, "\n    • ", false);
			}
			return text;
		}
		return null;
	}

	// Token: 0x06001F13 RID: 7955 RVA: 0x000A5A0C File Offset: 0x000A3C0C
	public void TriggerRoomEffects(KPrefabID triggerer, Effects target)
	{
		if (this.primary_constraint == null)
		{
			return;
		}
		if (triggerer == null)
		{
			return;
		}
		if (this.effects == null)
		{
			return;
		}
		if (this.primary_constraint.building_criteria(triggerer))
		{
			foreach (string text in this.effects)
			{
				target.Add(text, true);
			}
		}
	}

	// Token: 0x0200114B RID: 4427
	public enum RoomIdentificationResult
	{
		// Token: 0x04005A74 RID: 23156
		all_satisfied,
		// Token: 0x04005A75 RID: 23157
		primary_satisfied,
		// Token: 0x04005A76 RID: 23158
		primary_unsatisfied
	}
}
