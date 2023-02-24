using System;
using System.Diagnostics;
using KSerialization;

// Token: 0x02000742 RID: 1858
[SerializationConfig(MemberSerialization.OptIn)]
[DebuggerDisplay("{name} {WattsUsed}W")]
public class EnergyConsumerSelfSustaining : EnergyConsumer
{
	// Token: 0x1400001A RID: 26
	// (add) Token: 0x0600332C RID: 13100 RVA: 0x00113D30 File Offset: 0x00111F30
	// (remove) Token: 0x0600332D RID: 13101 RVA: 0x00113D68 File Offset: 0x00111F68
	public event System.Action OnConnectionChanged;

	// Token: 0x170003BD RID: 957
	// (get) Token: 0x0600332E RID: 13102 RVA: 0x00113D9D File Offset: 0x00111F9D
	public override bool IsPowered
	{
		get
		{
			return this.isSustained || this.connectionStatus == CircuitManager.ConnectionStatus.Powered;
		}
	}

	// Token: 0x170003BE RID: 958
	// (get) Token: 0x0600332F RID: 13103 RVA: 0x00113DB2 File Offset: 0x00111FB2
	public bool IsExternallyPowered
	{
		get
		{
			return this.connectionStatus == CircuitManager.ConnectionStatus.Powered;
		}
	}

	// Token: 0x06003330 RID: 13104 RVA: 0x00113DBD File Offset: 0x00111FBD
	public void SetSustained(bool isSustained)
	{
		this.isSustained = isSustained;
	}

	// Token: 0x06003331 RID: 13105 RVA: 0x00113DC8 File Offset: 0x00111FC8
	public override void SetConnectionStatus(CircuitManager.ConnectionStatus connection_status)
	{
		CircuitManager.ConnectionStatus connectionStatus = this.connectionStatus;
		switch (connection_status)
		{
		case CircuitManager.ConnectionStatus.NotConnected:
			this.connectionStatus = CircuitManager.ConnectionStatus.NotConnected;
			break;
		case CircuitManager.ConnectionStatus.Unpowered:
			if (this.connectionStatus == CircuitManager.ConnectionStatus.Powered && base.GetComponent<Battery>() == null)
			{
				this.connectionStatus = CircuitManager.ConnectionStatus.Unpowered;
			}
			break;
		case CircuitManager.ConnectionStatus.Powered:
			if (this.connectionStatus != CircuitManager.ConnectionStatus.Powered)
			{
				this.connectionStatus = CircuitManager.ConnectionStatus.Powered;
			}
			break;
		}
		this.UpdatePoweredStatus();
		if (connectionStatus != this.connectionStatus && this.OnConnectionChanged != null)
		{
			this.OnConnectionChanged();
		}
	}

	// Token: 0x06003332 RID: 13106 RVA: 0x00113E4B File Offset: 0x0011204B
	public void UpdatePoweredStatus()
	{
		this.operational.SetFlag(EnergyConsumer.PoweredFlag, this.IsPowered);
	}

	// Token: 0x04001F73 RID: 8051
	private bool isSustained;

	// Token: 0x04001F74 RID: 8052
	private CircuitManager.ConnectionStatus connectionStatus;
}
