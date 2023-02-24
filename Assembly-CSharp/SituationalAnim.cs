using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000921 RID: 2337
[AddComponentMenu("KMonoBehaviour/scripts/SituationalAnim")]
public class SituationalAnim : KMonoBehaviour
{
	// Token: 0x0600444C RID: 17484 RVA: 0x00181C54 File Offset: 0x0017FE54
	protected override void OnSpawn()
	{
		base.OnSpawn();
		SituationalAnim.Situation situation = this.GetSituation();
		DebugUtil.LogArgs(new object[] { "Situation is", situation });
		this.SetAnimForSituation(situation);
	}

	// Token: 0x0600444D RID: 17485 RVA: 0x00181C94 File Offset: 0x0017FE94
	private void SetAnimForSituation(SituationalAnim.Situation situation)
	{
		foreach (global::Tuple<SituationalAnim.Situation, string> tuple in this.anims)
		{
			if ((tuple.first & situation) == tuple.first)
			{
				DebugUtil.LogArgs(new object[] { "Chose Anim", tuple.first, tuple.second });
				this.SetAnim(tuple.second);
				break;
			}
		}
	}

	// Token: 0x0600444E RID: 17486 RVA: 0x00181D28 File Offset: 0x0017FF28
	private void SetAnim(string animName)
	{
		base.GetComponent<KBatchedAnimController>().Play(animName, KAnim.PlayMode.Once, 1f, 0f);
	}

	// Token: 0x0600444F RID: 17487 RVA: 0x00181D48 File Offset: 0x0017FF48
	private SituationalAnim.Situation GetSituation()
	{
		SituationalAnim.Situation situation = (SituationalAnim.Situation)0;
		Extents extents = base.GetComponent<Building>().GetExtents();
		int x = extents.x;
		int num = extents.x + extents.width - 1;
		int y = extents.y;
		int num2 = extents.y + extents.height - 1;
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(x, num, y - 1, y - 1), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Bottom;
		}
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(x - 1, x - 1, y, num2), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Left;
		}
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(x, num, num2 + 1, num2 + 1), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Top;
		}
		if (this.DoesSatisfy(this.GetSatisfactionForEdge(num + 1, num + 1, y, num2), this.mustSatisfy))
		{
			situation |= SituationalAnim.Situation.Right;
		}
		return situation;
	}

	// Token: 0x06004450 RID: 17488 RVA: 0x00181E1C File Offset: 0x0018001C
	private bool DoesSatisfy(SituationalAnim.MustSatisfy result, SituationalAnim.MustSatisfy requirement)
	{
		if (requirement == SituationalAnim.MustSatisfy.All)
		{
			return result == SituationalAnim.MustSatisfy.All;
		}
		if (requirement == SituationalAnim.MustSatisfy.Any)
		{
			return result > SituationalAnim.MustSatisfy.None;
		}
		return result == SituationalAnim.MustSatisfy.None;
	}

	// Token: 0x06004451 RID: 17489 RVA: 0x00181E34 File Offset: 0x00180034
	private SituationalAnim.MustSatisfy GetSatisfactionForEdge(int minx, int maxx, int miny, int maxy)
	{
		bool flag = false;
		bool flag2 = true;
		for (int i = minx; i <= maxx; i++)
		{
			for (int j = miny; j <= maxy; j++)
			{
				int num = Grid.XYToCell(i, j);
				if (this.test(num))
				{
					flag = true;
				}
				else
				{
					flag2 = false;
				}
			}
		}
		if (flag2)
		{
			return SituationalAnim.MustSatisfy.All;
		}
		if (flag)
		{
			return SituationalAnim.MustSatisfy.Any;
		}
		return SituationalAnim.MustSatisfy.None;
	}

	// Token: 0x04002D8A RID: 11658
	public List<global::Tuple<SituationalAnim.Situation, string>> anims;

	// Token: 0x04002D8B RID: 11659
	public Func<int, bool> test;

	// Token: 0x04002D8C RID: 11660
	public SituationalAnim.MustSatisfy mustSatisfy;

	// Token: 0x020016FF RID: 5887
	[Flags]
	public enum Situation
	{
		// Token: 0x04006BAF RID: 27567
		Left = 1,
		// Token: 0x04006BB0 RID: 27568
		Right = 2,
		// Token: 0x04006BB1 RID: 27569
		Top = 4,
		// Token: 0x04006BB2 RID: 27570
		Bottom = 8
	}

	// Token: 0x02001700 RID: 5888
	public enum MustSatisfy
	{
		// Token: 0x04006BB4 RID: 27572
		None,
		// Token: 0x04006BB5 RID: 27573
		Any,
		// Token: 0x04006BB6 RID: 27574
		All
	}
}
