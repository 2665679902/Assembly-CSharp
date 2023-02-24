using System;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C68 RID: 3176
	public sealed class Vector3Drawer : InlineDrawer
	{
		// Token: 0x060064E7 RID: 25831 RVA: 0x0025B160 File Offset: 0x00259360
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is Vector3;
		}

		// Token: 0x060064E8 RID: 25832 RVA: 0x0025B170 File Offset: 0x00259370
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Vector3 vector = (Vector3)member.value;
			ImGuiEx.SimpleField(member.name, string.Format("( {0}, {1}, {2} )", vector.x, vector.y, vector.z));
		}
	}
}
