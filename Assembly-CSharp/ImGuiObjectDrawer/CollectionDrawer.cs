using System;
using ImGuiNET;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C6A RID: 3178
	public abstract class CollectionDrawer : MemberDrawer
	{
		// Token: 0x060064ED RID: 25837
		public abstract bool IsEmpty(in MemberDrawContext context, in MemberDetails member);

		// Token: 0x060064EE RID: 25838 RVA: 0x0025B24C File Offset: 0x0025944C
		public override MemberDrawType GetDrawType(in MemberDrawContext context, in MemberDetails member)
		{
			if (this.IsEmpty(context, member))
			{
				return MemberDrawType.Inline;
			}
			return MemberDrawType.Custom;
		}

		// Token: 0x060064EF RID: 25839 RVA: 0x0025B25B File Offset: 0x0025945B
		protected sealed override void DrawInline(in MemberDrawContext context, in MemberDetails member)
		{
			Debug.Assert(this.IsEmpty(context, member));
			this.DrawEmpty(context, member);
		}

		// Token: 0x060064F0 RID: 25840 RVA: 0x0025B272 File Offset: 0x00259472
		protected sealed override void DrawCustom(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			Debug.Assert(!this.IsEmpty(context, member));
			this.DrawWithContents(context, member, depth);
		}

		// Token: 0x060064F1 RID: 25841 RVA: 0x0025B28D File Offset: 0x0025948D
		private void DrawEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			ImGui.Text(member.name + "(empty)");
		}

		// Token: 0x060064F2 RID: 25842 RVA: 0x0025B2A4 File Offset: 0x002594A4
		private void DrawWithContents(in MemberDrawContext context, in MemberDetails member, int depth)
		{
			CollectionDrawer.<>c__DisplayClass5_0 CS$<>8__locals1 = new CollectionDrawer.<>c__DisplayClass5_0();
			CS$<>8__locals1.depth = depth;
			ImGuiTreeNodeFlags imGuiTreeNodeFlags = ImGuiTreeNodeFlags.None;
			if (context.default_open && CS$<>8__locals1.depth <= 0)
			{
				imGuiTreeNodeFlags |= ImGuiTreeNodeFlags.DefaultOpen;
			}
			bool flag = ImGui.TreeNodeEx(member.name, imGuiTreeNodeFlags);
			DrawerUtil.Tooltip(member.type);
			if (flag)
			{
				this.VisitElements(new CollectionDrawer.ElementVisitor(CS$<>8__locals1.<DrawWithContents>g__Visitor|0), context, member);
				ImGui.TreePop();
			}
		}

		// Token: 0x060064F3 RID: 25843
		protected abstract void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member);

		// Token: 0x02001B07 RID: 6919
		// (Invoke) Token: 0x0600949E RID: 38046
		protected delegate void ElementVisitor(in MemberDrawContext context, CollectionDrawer.Element element);

		// Token: 0x02001B08 RID: 6920
		protected struct Element
		{
			// Token: 0x060094A1 RID: 38049 RVA: 0x0031CF16 File Offset: 0x0031B116
			public Element(string node_name, System.Action draw_tooltip, Func<object> get_object_to_inspect)
			{
				this.node_name = node_name;
				this.draw_tooltip = draw_tooltip;
				this.get_object_to_inspect = get_object_to_inspect;
			}

			// Token: 0x060094A2 RID: 38050 RVA: 0x0031CF2D File Offset: 0x0031B12D
			public Element(int index, System.Action draw_tooltip, Func<object> get_object_to_inspect)
			{
				this = new CollectionDrawer.Element(string.Format("[{0}]", index), draw_tooltip, get_object_to_inspect);
			}

			// Token: 0x04007974 RID: 31092
			public readonly string node_name;

			// Token: 0x04007975 RID: 31093
			public readonly System.Action draw_tooltip;

			// Token: 0x04007976 RID: 31094
			public readonly Func<object> get_object_to_inspect;
		}
	}
}
