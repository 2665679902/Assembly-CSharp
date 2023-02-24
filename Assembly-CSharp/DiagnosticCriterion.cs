using System;

// Token: 0x0200070C RID: 1804
public class DiagnosticCriterion
{
	// Token: 0x17000395 RID: 917
	// (get) Token: 0x06003185 RID: 12677 RVA: 0x001082CB File Offset: 0x001064CB
	// (set) Token: 0x06003186 RID: 12678 RVA: 0x001082D3 File Offset: 0x001064D3
	public string id { get; private set; }

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x06003187 RID: 12679 RVA: 0x001082DC File Offset: 0x001064DC
	// (set) Token: 0x06003188 RID: 12680 RVA: 0x001082E4 File Offset: 0x001064E4
	public string name { get; private set; }

	// Token: 0x06003189 RID: 12681 RVA: 0x001082ED File Offset: 0x001064ED
	public DiagnosticCriterion(string name, Func<ColonyDiagnostic.DiagnosticResult> action)
	{
		this.name = name;
		this.evaluateAction = action;
	}

	// Token: 0x0600318A RID: 12682 RVA: 0x00108303 File Offset: 0x00106503
	public void SetID(string id)
	{
		this.id = id;
	}

	// Token: 0x0600318B RID: 12683 RVA: 0x0010830C File Offset: 0x0010650C
	public ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		return this.evaluateAction();
	}

	// Token: 0x04001E3A RID: 7738
	private Func<ColonyDiagnostic.DiagnosticResult> evaluateAction;
}
