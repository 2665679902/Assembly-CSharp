using System;
using UnityEngine;

namespace Klei.AI
{
	// Token: 0x02000D78 RID: 3448
	public class Sicknesses : Modifications<Sickness, SicknessInstance>
	{
		// Token: 0x0600695A RID: 26970 RVA: 0x0028F84B File Offset: 0x0028DA4B
		public Sicknesses(GameObject go)
			: base(go, Db.Get().Sicknesses)
		{
		}

		// Token: 0x0600695B RID: 26971 RVA: 0x0028F860 File Offset: 0x0028DA60
		public void Infect(SicknessExposureInfo exposure_info)
		{
			Sickness sickness = Db.Get().Sicknesses.Get(exposure_info.sicknessID);
			if (!base.Has(sickness))
			{
				this.CreateInstance(sickness).ExposureInfo = exposure_info;
			}
		}

		// Token: 0x0600695C RID: 26972 RVA: 0x0028F89C File Offset: 0x0028DA9C
		public override SicknessInstance CreateInstance(Sickness sickness)
		{
			SicknessInstance sicknessInstance = new SicknessInstance(base.gameObject, sickness);
			this.Add(sicknessInstance);
			base.Trigger(GameHashes.SicknessAdded, sicknessInstance);
			ReportManager.Instance.ReportValue(ReportManager.ReportType.DiseaseAdded, 1f, base.gameObject.GetProperName(), null);
			return sicknessInstance;
		}

		// Token: 0x0600695D RID: 26973 RVA: 0x0028F8E7 File Offset: 0x0028DAE7
		public bool IsInfected()
		{
			return base.Count > 0;
		}

		// Token: 0x0600695E RID: 26974 RVA: 0x0028F8F2 File Offset: 0x0028DAF2
		public bool Cure(Sickness sickness)
		{
			return this.Cure(sickness.Id);
		}

		// Token: 0x0600695F RID: 26975 RVA: 0x0028F900 File Offset: 0x0028DB00
		public bool Cure(string sickness_id)
		{
			SicknessInstance sicknessInstance = null;
			foreach (SicknessInstance sicknessInstance2 in this)
			{
				if (sicknessInstance2.modifier.Id == sickness_id)
				{
					sicknessInstance = sicknessInstance2;
					break;
				}
			}
			if (sicknessInstance != null)
			{
				this.Remove(sicknessInstance);
				base.Trigger(GameHashes.SicknessCured, sicknessInstance);
				ReportManager.Instance.ReportValue(ReportManager.ReportType.DiseaseAdded, -1f, base.gameObject.GetProperName(), null);
				return true;
			}
			return false;
		}
	}
}
