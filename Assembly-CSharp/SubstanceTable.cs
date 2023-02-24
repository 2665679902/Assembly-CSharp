using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x0200099E RID: 2462
public class SubstanceTable : ScriptableObject, ISerializationCallbackReceiver
{
	// Token: 0x0600490F RID: 18703 RVA: 0x0019979E File Offset: 0x0019799E
	public List<Substance> GetList()
	{
		return this.list;
	}

	// Token: 0x06004910 RID: 18704 RVA: 0x001997A8 File Offset: 0x001979A8
	public Substance GetSubstance(SimHashes substance)
	{
		int count = this.list.Count;
		for (int i = 0; i < count; i++)
		{
			if (this.list[i].elementID == substance)
			{
				return this.list[i];
			}
		}
		return null;
	}

	// Token: 0x06004911 RID: 18705 RVA: 0x001997EF File Offset: 0x001979EF
	public void OnBeforeSerialize()
	{
		this.BindAnimList();
	}

	// Token: 0x06004912 RID: 18706 RVA: 0x001997F7 File Offset: 0x001979F7
	public void OnAfterDeserialize()
	{
		this.BindAnimList();
	}

	// Token: 0x06004913 RID: 18707 RVA: 0x00199800 File Offset: 0x00197A00
	private void BindAnimList()
	{
		foreach (Substance substance in this.list)
		{
			if (substance.anim != null && (substance.anims == null || substance.anims.Length == 0))
			{
				substance.anims = new KAnimFile[1];
				substance.anims[0] = substance.anim;
			}
		}
	}

	// Token: 0x06004914 RID: 18708 RVA: 0x00199888 File Offset: 0x00197A88
	public void RemoveDuplicates()
	{
		this.list = this.list.Distinct(new SubstanceTable.SubstanceEqualityComparer()).ToList<Substance>();
	}

	// Token: 0x04003007 RID: 12295
	[SerializeField]
	private List<Substance> list;

	// Token: 0x04003008 RID: 12296
	public Material solidMaterial;

	// Token: 0x04003009 RID: 12297
	public Material liquidMaterial;

	// Token: 0x0200179F RID: 6047
	private class SubstanceEqualityComparer : IEqualityComparer<Substance>
	{
		// Token: 0x06008B59 RID: 35673 RVA: 0x002FF47E File Offset: 0x002FD67E
		public bool Equals(Substance x, Substance y)
		{
			return x.elementID.Equals(y.elementID);
		}

		// Token: 0x06008B5A RID: 35674 RVA: 0x002FF49C File Offset: 0x002FD69C
		public int GetHashCode(Substance obj)
		{
			return obj.elementID.GetHashCode();
		}
	}
}
