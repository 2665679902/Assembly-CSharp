using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C61 RID: 3169
	public class SimpleDrawer : InlineDrawer
	{
		// Token: 0x060064D1 RID: 25809 RVA: 0x0025AF59 File Offset: 0x00259159
		public override bool CanDrawAtDepth(int depth)
		{
			return true;
		}

		// Token: 0x060064D2 RID: 25810 RVA: 0x0025AF5C File Offset: 0x0025915C
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.type.IsPrimitive || member.CanAssignToType<string>();
		}

		// Token: 0x060064D3 RID: 25811 RVA: 0x0025AF73 File Offset: 0x00259173
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			ImGuiEx.SimpleField(member.name, member.value.ToString());
		}
	}
}
