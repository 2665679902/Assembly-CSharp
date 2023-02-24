using System;
using ImGuiNET;
using UnityEngine;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C6E RID: 3182
	public class UnityObjectDrawer : PlainCSharpObjectDrawer
	{
		// Token: 0x06006501 RID: 25857 RVA: 0x0025B554 File Offset: 0x00259754
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.value is UnityEngine.Object;
		}

		// Token: 0x06006502 RID: 25858 RVA: 0x0025B564 File Offset: 0x00259764
		protected override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			UnityEngine.Object @object = (UnityEngine.Object)member.value;
			ImGuiTreeNodeFlags imGuiTreeNodeFlags = ImGuiTreeNodeFlags.None;
			if (context.default_open && depth <= 0)
			{
				imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.DefaultOpen;
			}
			bool flag = ImGui.TreeNodeEx(member.name, imGuiTreeNodeFlags);
			DrawerUtil.Tooltip(member.type);
			if (flag)
			{
				base.DrawContents(context, member, depth);
				ImGui.TreePop();
			}
		}
	}
}
