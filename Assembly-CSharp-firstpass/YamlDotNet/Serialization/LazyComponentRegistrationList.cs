using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace YamlDotNet.Serialization
{
	// Token: 0x02000198 RID: 408
	internal sealed class LazyComponentRegistrationList<TArgument, TComponent> : IEnumerable<Func<TArgument, TComponent>>, IEnumerable
	{
		// Token: 0x06000D29 RID: 3369 RVA: 0x00037890 File Offset: 0x00035A90
		public LazyComponentRegistrationList<TArgument, TComponent> Clone()
		{
			LazyComponentRegistrationList<TArgument, TComponent> lazyComponentRegistrationList = new LazyComponentRegistrationList<TArgument, TComponent>();
			foreach (LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration lazyComponentRegistration in this.entries)
			{
				lazyComponentRegistrationList.entries.Add(lazyComponentRegistration);
			}
			return lazyComponentRegistrationList;
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000378F0 File Offset: 0x00035AF0
		public void Add(Type componentType, Func<TArgument, TComponent> factory)
		{
			this.entries.Add(new LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration(componentType, factory));
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00037904 File Offset: 0x00035B04
		public void Remove(Type componentType)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (this.entries[i].ComponentType == componentType)
				{
					this.entries.RemoveAt(i);
					return;
				}
			}
			throw new KeyNotFoundException(string.Format("A component registration of type '{0}' was not found.", componentType.FullName));
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00037962 File Offset: 0x00035B62
		public int Count
		{
			get
			{
				return this.entries.Count;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0003796F File Offset: 0x00035B6F
		public IEnumerable<Func<TArgument, TComponent>> InReverseOrder
		{
			get
			{
				int num;
				for (int i = this.entries.Count - 1; i >= 0; i = num)
				{
					yield return this.entries[i].Factory;
					num = i - 1;
				}
				yield break;
			}
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0003797F File Offset: 0x00035B7F
		public IRegistrationLocationSelectionSyntax<TComponent> CreateRegistrationLocationSelector(Type componentType, Func<TArgument, TComponent> factory)
		{
			return new LazyComponentRegistrationList<TArgument, TComponent>.RegistrationLocationSelector(this, new LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration(componentType, factory));
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0003798E File Offset: 0x00035B8E
		public ITrackingRegistrationLocationSelectionSyntax<TComponent> CreateTrackingRegistrationLocationSelector(Type componentType, Func<TComponent, TArgument, TComponent> factory)
		{
			return new LazyComponentRegistrationList<TArgument, TComponent>.TrackingRegistrationLocationSelector(this, new LazyComponentRegistrationList<TArgument, TComponent>.TrackingLazyComponentRegistration(componentType, factory));
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0003799D File Offset: 0x00035B9D
		public IEnumerator<Func<TArgument, TComponent>> GetEnumerator()
		{
			return this.entries.Select((LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration e) => e.Factory).GetEnumerator();
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x000379CE File Offset: 0x00035BCE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x000379D8 File Offset: 0x00035BD8
		private int IndexOfRegistration(Type registrationType)
		{
			for (int i = 0; i < this.entries.Count; i++)
			{
				if (registrationType == this.entries[i].ComponentType)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00037A17 File Offset: 0x00035C17
		private void EnsureNoDuplicateRegistrationType(Type componentType)
		{
			if (this.IndexOfRegistration(componentType) != -1)
			{
				throw new InvalidOperationException(string.Format("A component of type '{0}' has already been registered.", componentType.FullName));
			}
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00037A39 File Offset: 0x00035C39
		private int EnsureRegistrationExists<TRegistrationType>()
		{
			int num = this.IndexOfRegistration(typeof(TRegistrationType));
			if (num == -1)
			{
				throw new InvalidOperationException(string.Format("A component of type '{0}' has not been registered.", typeof(TRegistrationType).FullName));
			}
			return num;
		}

		// Token: 0x040007F3 RID: 2035
		private readonly List<LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration> entries = new List<LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration>();

		// Token: 0x02000A26 RID: 2598
		public sealed class LazyComponentRegistration
		{
			// Token: 0x0600548D RID: 21645 RVA: 0x0009D563 File Offset: 0x0009B763
			public LazyComponentRegistration(Type componentType, Func<TArgument, TComponent> factory)
			{
				this.ComponentType = componentType;
				this.Factory = factory;
			}

			// Token: 0x040022BD RID: 8893
			public readonly Type ComponentType;

			// Token: 0x040022BE RID: 8894
			public readonly Func<TArgument, TComponent> Factory;
		}

		// Token: 0x02000A27 RID: 2599
		public sealed class TrackingLazyComponentRegistration
		{
			// Token: 0x0600548E RID: 21646 RVA: 0x0009D579 File Offset: 0x0009B779
			public TrackingLazyComponentRegistration(Type componentType, Func<TComponent, TArgument, TComponent> factory)
			{
				this.ComponentType = componentType;
				this.Factory = factory;
			}

			// Token: 0x040022BF RID: 8895
			public readonly Type ComponentType;

			// Token: 0x040022C0 RID: 8896
			public readonly Func<TComponent, TArgument, TComponent> Factory;
		}

		// Token: 0x02000A28 RID: 2600
		private class RegistrationLocationSelector : IRegistrationLocationSelectionSyntax<TComponent>
		{
			// Token: 0x0600548F RID: 21647 RVA: 0x0009D58F File Offset: 0x0009B78F
			public RegistrationLocationSelector(LazyComponentRegistrationList<TArgument, TComponent> registrations, LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration newRegistration)
			{
				this.registrations = registrations;
				this.newRegistration = newRegistration;
			}

			// Token: 0x06005490 RID: 21648 RVA: 0x0009D5A8 File Offset: 0x0009B7A8
			void IRegistrationLocationSelectionSyntax<TComponent>.InsteadOf<TRegistrationType>()
			{
				if (this.newRegistration.ComponentType != typeof(TRegistrationType))
				{
					this.registrations.EnsureNoDuplicateRegistrationType(this.newRegistration.ComponentType);
				}
				int num = this.registrations.EnsureRegistrationExists<TRegistrationType>();
				this.registrations.entries[num] = this.newRegistration;
			}

			// Token: 0x06005491 RID: 21649 RVA: 0x0009D60C File Offset: 0x0009B80C
			void IRegistrationLocationSelectionSyntax<TComponent>.After<TRegistrationType>()
			{
				this.registrations.EnsureNoDuplicateRegistrationType(this.newRegistration.ComponentType);
				int num = this.registrations.EnsureRegistrationExists<TRegistrationType>();
				this.registrations.entries.Insert(num + 1, this.newRegistration);
			}

			// Token: 0x06005492 RID: 21650 RVA: 0x0009D654 File Offset: 0x0009B854
			void IRegistrationLocationSelectionSyntax<TComponent>.Before<TRegistrationType>()
			{
				this.registrations.EnsureNoDuplicateRegistrationType(this.newRegistration.ComponentType);
				int num = this.registrations.EnsureRegistrationExists<TRegistrationType>();
				this.registrations.entries.Insert(num, this.newRegistration);
			}

			// Token: 0x06005493 RID: 21651 RVA: 0x0009D69A File Offset: 0x0009B89A
			void IRegistrationLocationSelectionSyntax<TComponent>.OnBottom()
			{
				this.registrations.EnsureNoDuplicateRegistrationType(this.newRegistration.ComponentType);
				this.registrations.entries.Add(this.newRegistration);
			}

			// Token: 0x06005494 RID: 21652 RVA: 0x0009D6C8 File Offset: 0x0009B8C8
			void IRegistrationLocationSelectionSyntax<TComponent>.OnTop()
			{
				this.registrations.EnsureNoDuplicateRegistrationType(this.newRegistration.ComponentType);
				this.registrations.entries.Insert(0, this.newRegistration);
			}

			// Token: 0x040022C1 RID: 8897
			private readonly LazyComponentRegistrationList<TArgument, TComponent> registrations;

			// Token: 0x040022C2 RID: 8898
			private readonly LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration newRegistration;
		}

		// Token: 0x02000A29 RID: 2601
		private class TrackingRegistrationLocationSelector : ITrackingRegistrationLocationSelectionSyntax<TComponent>
		{
			// Token: 0x06005495 RID: 21653 RVA: 0x0009D6F7 File Offset: 0x0009B8F7
			public TrackingRegistrationLocationSelector(LazyComponentRegistrationList<TArgument, TComponent> registrations, LazyComponentRegistrationList<TArgument, TComponent>.TrackingLazyComponentRegistration newRegistration)
			{
				this.registrations = registrations;
				this.newRegistration = newRegistration;
			}

			// Token: 0x06005496 RID: 21654 RVA: 0x0009D710 File Offset: 0x0009B910
			void ITrackingRegistrationLocationSelectionSyntax<TComponent>.InsteadOf<TRegistrationType>()
			{
				if (this.newRegistration.ComponentType != typeof(TRegistrationType))
				{
					this.registrations.EnsureNoDuplicateRegistrationType(this.newRegistration.ComponentType);
				}
				int num = this.registrations.EnsureRegistrationExists<TRegistrationType>();
				Func<TArgument, TComponent> innerComponentFactory = this.registrations.entries[num].Factory;
				this.registrations.entries[num] = new LazyComponentRegistrationList<TArgument, TComponent>.LazyComponentRegistration(this.newRegistration.ComponentType, (TArgument arg) => this.newRegistration.Factory(innerComponentFactory(arg), arg));
			}

			// Token: 0x040022C3 RID: 8899
			private readonly LazyComponentRegistrationList<TArgument, TComponent> registrations;

			// Token: 0x040022C4 RID: 8900
			private readonly LazyComponentRegistrationList<TArgument, TComponent>.TrackingLazyComponentRegistration newRegistration;
		}
	}
}
