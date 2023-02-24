using System;
using System.Collections;

namespace ImGuiObjectDrawer
{
	// Token: 0x02000C6D RID: 3181
	public sealed class IEnumerableDrawer : CollectionDrawer
	{
		// Token: 0x060064FD RID: 25853 RVA: 0x0025B4A0 File Offset: 0x002596A0
		public override bool CanDraw(in MemberDrawContext context, in MemberDetails member)
		{
			return member.CanAssignToType<IEnumerable>();
		}

		// Token: 0x060064FE RID: 25854 RVA: 0x0025B4A8 File Offset: 0x002596A8
		public override bool IsEmpty(in MemberDrawContext context, in MemberDetails member)
		{
			return !((IEnumerable)member.value).GetEnumerator().MoveNext();
		}

		// Token: 0x060064FF RID: 25855 RVA: 0x0025B4C4 File Offset: 0x002596C4
		protected override void VisitElements(CollectionDrawer.ElementVisitor visit, in MemberDrawContext context, in MemberDetails member)
		{
			IEnumerable enumerable = (IEnumerable)member.value;
			int num = 0;
			using (IEnumerator enumerator = enumerable.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object el = enumerator.Current;
					visit(context, new CollectionDrawer.Element(num, delegate
					{
						DrawerUtil.Tooltip(el.GetType());
					}, () => new
					{
						value = el
					}));
					num++;
				}
			}
		}
	}
}
