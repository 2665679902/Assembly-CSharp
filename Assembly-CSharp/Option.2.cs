using System;

// Token: 0x02000360 RID: 864
public static class Option
{
	// Token: 0x0600117D RID: 4477 RVA: 0x0005D837 File Offset: 0x0005BA37
	public static Option<T> Some<T>(T value)
	{
		return new Option<T>(value);
	}

	// Token: 0x0600117E RID: 4478 RVA: 0x0005D840 File Offset: 0x0005BA40
	public static Option<T> Maybe<T>(T value)
	{
		if (value.IsNullOrDestroyed())
		{
			return default(Option<T>);
		}
		return new Option<T>(value);
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x0600117F RID: 4479 RVA: 0x0005D86C File Offset: 0x0005BA6C
	public static Option.Internal.Value_None None
	{
		get
		{
			return default(Option.Internal.Value_None);
		}
	}

	// Token: 0x06001180 RID: 4480 RVA: 0x0005D884 File Offset: 0x0005BA84
	public static bool AllHaveValues(params Option.Internal.Value_HasValue[] options)
	{
		if (options == null || options.Length == 0)
		{
			return false;
		}
		for (int i = 0; i < options.Length; i++)
		{
			if (!options[i].HasValue)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x02000F23 RID: 3875
	public static class Internal
	{
		// Token: 0x02001E96 RID: 7830
		public readonly struct Value_None
		{
		}

		// Token: 0x02001E97 RID: 7831
		public readonly struct Value_HasValue
		{
			// Token: 0x06009C06 RID: 39942 RVA: 0x00339D45 File Offset: 0x00337F45
			public Value_HasValue(bool hasValue)
			{
				this.HasValue = hasValue;
			}

			// Token: 0x0400890E RID: 35086
			public readonly bool HasValue;
		}
	}
}
