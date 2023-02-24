using System;
using ImGuiNET;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C6F RID: 3183
	public class PlainCSharpObjectDrawer : MemberDrawer
	{
		// Token: 0x06006504 RID: 25860 RVA: 0x0025B5BF File Offset: 0x002597BF
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return true;
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x0025B5C2 File Offset: 0x002597C2
		public override MemberDrawType GetDrawType(in MemberDrawContext context, in MemberDetails member)
		{
			return MemberDrawType.Custom;
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x0025B5C5 File Offset: 0x002597C5
		protected override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x0025B5CC File Offset: 0x002597CC
		protected override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			ImGuiTreeNodeFlags imGuiTreeNodeFlags = ImGuiTreeNodeFlags.None;
			if (context.default_open && depth <= 0)
			{
				imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.DefaultOpen;
			}
			bool flag = ImGui.TreeNodeEx(member.name, imGuiTreeNodeFlags);
			DrawerUtil.Tooltip(member.type);
			if (flag)
			{
				this.DrawContents(context, member, depth);
				ImGui.TreePop();
			}
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x0025B613 File Offset: 0x00259813
		protected virtual void DrawContents(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			DrawerUtil.DrawObjectContents(member.value, context, depth + 1);
		}
	}
}
