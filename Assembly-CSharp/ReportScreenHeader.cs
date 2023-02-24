using System;
using UnityEngine;

// Token: 0x02000B69 RID: 2921
[AddComponentMenu("KMonoBehaviour/scripts/ReportScreenHeader")]
public class ReportScreenHeader : KMonoBehaviour
{
	// Token: 0x06005B39 RID: 23353 RVA: 0x00211FB6 File Offset: 0x002101B6
	public void SetMainEntry(ReportManager.ReportGroup reportGroup)
	{
		if (this.mainRow == null)
		{
			this.mainRow = Util.KInstantiateUI(this.rowTemplate.gameObject, base.gameObject, true).GetComponent<ReportScreenHeaderRow>();
		}
		this.mainRow.SetLine(reportGroup);
	}

	// Token: 0x04003DE3 RID: 15843
	[SerializeField]
	private ReportScreenHeaderRow rowTemplate;

	// Token: 0x04003DE4 RID: 15844
	private ReportScreenHeaderRow mainRow;
}
