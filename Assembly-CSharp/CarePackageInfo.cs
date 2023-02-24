using System;
using UnityEngine;

// Token: 0x020007B8 RID: 1976
public class CarePackageInfo : ITelepadDeliverable
{
	// Token: 0x06003837 RID: 14391 RVA: 0x00137750 File Offset: 0x00135950
	public CarePackageInfo(string ID, float amount, Func<bool> requirement)
	{
		this.id = ID;
		this.quantity = amount;
		this.requirement = requirement;
	}

	// Token: 0x06003838 RID: 14392 RVA: 0x0013776D File Offset: 0x0013596D
	public CarePackageInfo(string ID, float amount, Func<bool> requirement, string facadeID)
	{
		this.id = ID;
		this.quantity = amount;
		this.requirement = requirement;
		this.facadeID = facadeID;
	}

	// Token: 0x06003839 RID: 14393 RVA: 0x00137794 File Offset: 0x00135994
	public GameObject Deliver(Vector3 location)
	{
		location += Vector3.right / 2f;
		GameObject gameObject = Util.KInstantiate(Assets.GetPrefab(CarePackageConfig.ID), location);
		gameObject.SetActive(true);
		gameObject.GetComponent<CarePackage>().SetInfo(this);
		return gameObject;
	}

	// Token: 0x04002575 RID: 9589
	public readonly string id;

	// Token: 0x04002576 RID: 9590
	public readonly float quantity;

	// Token: 0x04002577 RID: 9591
	public readonly Func<bool> requirement;

	// Token: 0x04002578 RID: 9592
	public readonly string facadeID;
}
