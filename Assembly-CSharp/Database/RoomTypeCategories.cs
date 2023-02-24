using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000CAB RID: 3243
	public class RoomTypeCategories : ResourceSet<RoomTypeCategory>
	{
		// Token: 0x060065D4 RID: 26068 RVA: 0x0026F778 File Offset: 0x0026D978
		private RoomTypeCategory Add(string id, string name, string colorName, string icon)
		{
			RoomTypeCategory roomTypeCategory = new RoomTypeCategory(id, name, colorName, icon);
			base.Add(roomTypeCategory);
			return roomTypeCategory;
		}

		// Token: 0x060065D5 RID: 26069 RVA: 0x0026F79C File Offset: 0x0026D99C
		public RoomTypeCategories(ResourceSet parent)
			: base("RoomTypeCategories", parent)
		{
			base.Initialize();
			this.None = this.Add("None", ROOMS.CATEGORY.NONE.NAME, "roomNone", "unknown");
			this.Food = this.Add("Food", ROOMS.CATEGORY.FOOD.NAME, "roomFood", "ui_room_food");
			this.Sleep = this.Add("Sleep", ROOMS.CATEGORY.SLEEP.NAME, "roomSleep", "ui_room_sleep");
			this.Recreation = this.Add("Recreation", ROOMS.CATEGORY.RECREATION.NAME, "roomRecreation", "ui_room_recreational");
			this.Bathroom = this.Add("Bathroom", ROOMS.CATEGORY.BATHROOM.NAME, "roomBathroom", "ui_room_bathroom");
			this.Hospital = this.Add("Hospital", ROOMS.CATEGORY.HOSPITAL.NAME, "roomHospital", "ui_room_hospital");
			this.Industrial = this.Add("Industrial", ROOMS.CATEGORY.INDUSTRIAL.NAME, "roomIndustrial", "ui_room_industrial");
			this.Agricultural = this.Add("Agricultural", ROOMS.CATEGORY.AGRICULTURAL.NAME, "roomAgricultural", "ui_room_agricultural");
			this.Park = this.Add("Park", ROOMS.CATEGORY.PARK.NAME, "roomPark", "ui_room_park");
			this.Science = this.Add("Science", ROOMS.CATEGORY.SCIENCE.NAME, "roomScience", "ui_room_science");
		}

		// Token: 0x040049E8 RID: 18920
		public RoomTypeCategory None;

		// Token: 0x040049E9 RID: 18921
		public RoomTypeCategory Food;

		// Token: 0x040049EA RID: 18922
		public RoomTypeCategory Sleep;

		// Token: 0x040049EB RID: 18923
		public RoomTypeCategory Recreation;

		// Token: 0x040049EC RID: 18924
		public RoomTypeCategory Bathroom;

		// Token: 0x040049ED RID: 18925
		public RoomTypeCategory Hospital;

		// Token: 0x040049EE RID: 18926
		public RoomTypeCategory Industrial;

		// Token: 0x040049EF RID: 18927
		public RoomTypeCategory Agricultural;

		// Token: 0x040049F0 RID: 18928
		public RoomTypeCategory Park;

		// Token: 0x040049F1 RID: 18929
		public RoomTypeCategory Science;
	}
}
