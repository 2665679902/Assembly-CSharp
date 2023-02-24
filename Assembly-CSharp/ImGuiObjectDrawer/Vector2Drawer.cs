using System;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C67 RID: 3175
	public sealed class Vector2Drawer : InlineDrawer
	{
		// Token: 0x060064E4 RID: 25828 RVA: 0x0025B102 File Offset: 0x00259302
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is Vector2;
		}

		// Token: 0x060064E5 RID: 25829 RVA: 0x0025B114 File Offset: 0x00259314
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Vector2 vector = (Vector2)member.value;
			ImGuiEx.SimpleField(member.name, string.Format("( {0}, {1} )", vector.x, vector.y));
		}
	}
}
