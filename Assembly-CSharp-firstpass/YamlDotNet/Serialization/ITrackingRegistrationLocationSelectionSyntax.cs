using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200018D RID: 397
	public interface ITrackingRegistrationLocationSelectionSyntax<TBaseRegistrationType>
	{
		// Token: 0x06000D12 RID: 3346
		void InsteadOf<TRegistrationType>() where TRegistrationType : TBaseRegistrationType;
	}
}
