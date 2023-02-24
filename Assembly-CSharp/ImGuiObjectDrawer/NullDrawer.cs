using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C60 RID: 3168
	public class NullDrawer : InlineDrawer
	{
		// Token: 0x060064CD RID: 25805 RVA: 0x0025AF31 File Offset: 0x00259131
		public override bool CanDrawAtDepth(int depth)
		{
			return true;
		}

		// Token: 0x060064CE RID: 25806 RVA: 0x0025AF34 File Offset: 0x00259134
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value == null;
		}

		// Token: 0x060064CF RID: 25807 RVA: 0x0025AF3F File Offset: 0x0025913F
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, "null");
		}
	}
}
