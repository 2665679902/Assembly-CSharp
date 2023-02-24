using System;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C69 RID: 3177
	public sealed class Vector4Drawer : InlineDrawer
	{
		// Token: 0x060064EA RID: 25834 RVA: 0x0025B1C7 File Offset: 0x002593C7
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is Vector4;
		}

		// Token: 0x060064EB RID: 25835 RVA: 0x0025B1D8 File Offset: 0x002593D8
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Vector4 vector = (Vector4)member.value;
			ImGuiEx.SimpleField(member.name, string.Format("( {0}, {1}, {2}, {3} )", new object[] { vector.x, vector.y, vector.z, vector.w }));
		}
	}
}
