using System;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C6B RID: 3179
	public sealed class ArrayDrawer : CollectionDrawer
	{
		// Token: 0x060064F5 RID: 25845 RVA: 0x0025B310 File Offset: 0x00259510
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.type.IsArray;
		}

		// Token: 0x060064F6 RID: 25846 RVA: 0x0025B31D File Offset: 0x0025951D
		public override bool IsEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			return ((Array)member.value).Length == 0;
		}

		// Token: 0x060064F7 RID: 25847 RVA: 0x0025B334 File Offset: 0x00259534
		protected override void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member)
		{
			ArrayDrawer.<>c__DisplayClass2_0 CS$<>8__locals1 = new ArrayDrawer.<>c__DisplayClass2_0();
			CS$<>8__locals1.array = (Array)member.value;
			int i;
			int num;
			for (i = 0; i < CS$<>8__locals1.array.Length; i = num)
			{
				int j = i;
				System.Action action;
				if ((action = CS$<>8__locals1.<>9__0) == null)
				{
					action = (CS$<>8__locals1.<>9__0 = delegate
					{
						DrawerUtil.Tooltip(CS$<>8__locals1.array.GetType().GetElementType());
					});
				}
				visit(context, new CollectionDrawer.Element(j, action, () => new
				{
					value = CS$<>8__locals1.array.GetValue(i)
				}));
				num = i + 1;
			}
		}
	}
}
