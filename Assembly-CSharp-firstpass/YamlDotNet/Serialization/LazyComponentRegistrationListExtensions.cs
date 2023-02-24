using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000199 RID: 409
	internal static class LazyComponentRegistrationListExtensions
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x00037A81 File Offset: 0x00035C81
		public static TComponent BuildComponentChain<TComponent>(this LazyComponentRegistrationList<TComponent, TComponent> registrations, TComponent innerComponent)
		{
			return registrations.InReverseOrder.Aggregate(innerComponent, (TComponent inner, Func<TComponent, TComponent> factory) => factory(inner));
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00037AB0 File Offset: 0x00035CB0
		public static TComponent BuildComponentChain<TArgument, TComponent>(this LazyComponentRegistrationList<TArgument, TComponent> registrations, TComponent innerComponent, Func<TComponent, TArgument> argumentBuilder)
		{
			return registrations.InReverseOrder.Aggregate(innerComponent, (TComponent inner, Func<TArgument, TComponent> factory) => factory(argumentBuilder(inner)));
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x00037AE2 File Offset: 0x00035CE2
		public static List<TComponent> BuildComponentList<TComponent>(this LazyComponentRegistrationList<Nothing, TComponent> registrations)
		{
			return registrations.Select((Func<Nothing, TComponent> factory) => factory(null)).ToList<TComponent>();
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x00037B10 File Offset: 0x00035D10
		public static List<TComponent> BuildComponentList<TArgument, TComponent>(this LazyComponentRegistrationList<TArgument, TComponent> registrations, TArgument argument)
		{
			return registrations.Select((Func<TArgument, TComponent> factory) => factory(argument)).ToList<TComponent>();
		}
	}
}
