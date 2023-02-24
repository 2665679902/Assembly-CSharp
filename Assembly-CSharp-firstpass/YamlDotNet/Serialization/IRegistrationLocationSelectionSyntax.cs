using System;

namespace YamlDotNet.Serialization
{
	// Token: 0x0200018C RID: 396
	public interface IRegistrationLocationSelectionSyntax<TBaseRegistrationType>
	{
		// Token: 0x06000D0D RID: 3341
		void InsteadOf<TRegistrationType>() where TRegistrationType : TBaseRegistrationType;

		// Token: 0x06000D0E RID: 3342
		void Before<TRegistrationType>() where TRegistrationType : TBaseRegistrationType;

		// Token: 0x06000D0F RID: 3343
		void After<TRegistrationType>() where TRegistrationType : TBaseRegistrationType;

		// Token: 0x06000D10 RID: 3344
		void OnTop();

		// Token: 0x06000D11 RID: 3345
		void OnBottom();
	}
}
