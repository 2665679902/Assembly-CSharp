using System;
using System.Collections.Generic;
using System.Diagnostics;
using KSerialization;

// Token: 0x0200035F RID: 863
[DebuggerDisplay("has_value={hasValue} {value}")]
[Serializable]
public readonly struct Option<T> : IEquatable<Option<T>>, IEquatable<T>
{
	// Token: 0x06001161 RID: 4449 RVA: 0x0005D560 File Offset: 0x0005B760
	public Option(T value)
	{
		this.value = value;
		this.hasValue = true;
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x06001162 RID: 4450 RVA: 0x0005D570 File Offset: 0x0005B770
	public bool HasValue
	{
		get
		{
			return this.hasValue;
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x06001163 RID: 4451 RVA: 0x0005D578 File Offset: 0x0005B778
	public T Value
	{
		get
		{
			return this.Unwrap();
		}
	}

	// Token: 0x06001164 RID: 4452 RVA: 0x0005D580 File Offset: 0x0005B780
	public T Unwrap()
	{
		if (!this.hasValue)
		{
			throw new Exception("Tried to get a value for a Option<" + typeof(T).FullName + ">, but hasValue is false");
		}
		return this.value;
	}

	// Token: 0x06001165 RID: 4453 RVA: 0x0005D5B4 File Offset: 0x0005B7B4
	public T UnwrapOr(T fallback_value, string warn_on_fallback = null)
	{
		if (!this.hasValue)
		{
			if (warn_on_fallback != null)
			{
				DebugUtil.DevAssert(false, "Failed to unwrap a Option<" + typeof(T).FullName + ">: " + warn_on_fallback, null);
			}
			return fallback_value;
		}
		return this.value;
	}

	// Token: 0x06001166 RID: 4454 RVA: 0x0005D5EF File Offset: 0x0005B7EF
	public T UnwrapOrElse(Func<T> get_fallback_value_fn, string warn_on_fallback = null)
	{
		if (!this.hasValue)
		{
			if (warn_on_fallback != null)
			{
				DebugUtil.DevAssert(false, "Failed to unwrap a Option<" + typeof(T).FullName + ">: " + warn_on_fallback, null);
			}
			return get_fallback_value_fn();
		}
		return this.value;
	}

	// Token: 0x06001167 RID: 4455 RVA: 0x0005D630 File Offset: 0x0005B830
	public T UnwrapOrDefault()
	{
		if (!this.hasValue)
		{
			return default(T);
		}
		return this.value;
	}

	// Token: 0x06001168 RID: 4456 RVA: 0x0005D655 File Offset: 0x0005B855
	public T Expect(string msg_on_fail)
	{
		if (!this.hasValue)
		{
			throw new Exception(msg_on_fail);
		}
		return this.value;
	}

	// Token: 0x06001169 RID: 4457 RVA: 0x0005D66C File Offset: 0x0005B86C
	public bool IsSome()
	{
		return this.hasValue;
	}

	// Token: 0x0600116A RID: 4458 RVA: 0x0005D674 File Offset: 0x0005B874
	public bool IsNone()
	{
		return !this.hasValue;
	}

	// Token: 0x0600116B RID: 4459 RVA: 0x0005D67F File Offset: 0x0005B87F
	public Option<U> AndThen<U>(Func<T, U> fn)
	{
		if (this.IsNone())
		{
			return Option.None;
		}
		return Option.Maybe<U>(fn(this.value));
	}

	// Token: 0x0600116C RID: 4460 RVA: 0x0005D6A5 File Offset: 0x0005B8A5
	public Option<U> AndThen<U>(Func<T, Option<U>> fn)
	{
		if (this.IsNone())
		{
			return Option.None;
		}
		return fn(this.value);
	}

	// Token: 0x0600116D RID: 4461 RVA: 0x0005D6C6 File Offset: 0x0005B8C6
	public static implicit operator Option<T>(T value)
	{
		return Option.Maybe<T>(value);
	}

	// Token: 0x0600116E RID: 4462 RVA: 0x0005D6CE File Offset: 0x0005B8CE
	public static explicit operator T(Option<T> option)
	{
		return option.Unwrap();
	}

	// Token: 0x0600116F RID: 4463 RVA: 0x0005D6D8 File Offset: 0x0005B8D8
	public static implicit operator Option<T>(Option.Internal.Value_None value)
	{
		return default(Option<T>);
	}

	// Token: 0x06001170 RID: 4464 RVA: 0x0005D6EE File Offset: 0x0005B8EE
	public static implicit operator Option.Internal.Value_HasValue(Option<T> value)
	{
		return new Option.Internal.Value_HasValue(value.hasValue);
	}

	// Token: 0x06001171 RID: 4465 RVA: 0x0005D6FB File Offset: 0x0005B8FB
	public void Deconstruct(out bool hasValue, out T value)
	{
		hasValue = this.hasValue;
		value = this.value;
	}

	// Token: 0x06001172 RID: 4466 RVA: 0x0005D711 File Offset: 0x0005B911
	public bool Equals(Option<T> other)
	{
		return EqualityComparer<bool>.Default.Equals(this.hasValue, other.hasValue) && EqualityComparer<T>.Default.Equals(this.value, other.value);
	}

	// Token: 0x06001173 RID: 4467 RVA: 0x0005D744 File Offset: 0x0005B944
	public override bool Equals(object obj)
	{
		if (obj is Option<T>)
		{
			Option<T> option = (Option<T>)obj;
			return this.Equals(option);
		}
		return false;
	}

	// Token: 0x06001174 RID: 4468 RVA: 0x0005D769 File Offset: 0x0005B969
	public static bool operator ==(Option<T> lhs, Option<T> rhs)
	{
		return lhs.Equals(rhs);
	}

	// Token: 0x06001175 RID: 4469 RVA: 0x0005D773 File Offset: 0x0005B973
	public static bool operator !=(Option<T> lhs, Option<T> rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x06001176 RID: 4470 RVA: 0x0005D780 File Offset: 0x0005B980
	public override int GetHashCode()
	{
		return (-363764631 * -1521134295 + this.hasValue.GetHashCode()) * -1521134295 + EqualityComparer<T>.Default.GetHashCode(this.value);
	}

	// Token: 0x06001177 RID: 4471 RVA: 0x0005D7BE File Offset: 0x0005B9BE
	public override string ToString()
	{
		if (!this.hasValue)
		{
			return "None";
		}
		return string.Format("{0}", this.value);
	}

	// Token: 0x06001178 RID: 4472 RVA: 0x0005D7E3 File Offset: 0x0005B9E3
	public static bool operator ==(Option<T> lhs, T rhs)
	{
		return lhs.Equals(rhs);
	}

	// Token: 0x06001179 RID: 4473 RVA: 0x0005D7ED File Offset: 0x0005B9ED
	public static bool operator !=(Option<T> lhs, T rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x0600117A RID: 4474 RVA: 0x0005D7F9 File Offset: 0x0005B9F9
	public static bool operator ==(T lhs, Option<T> rhs)
	{
		return lhs.Equals(rhs);
	}

	// Token: 0x0600117B RID: 4475 RVA: 0x0005D80E File Offset: 0x0005BA0E
	public static bool operator !=(T lhs, Option<T> rhs)
	{
		return !(lhs == rhs);
	}

	// Token: 0x0600117C RID: 4476 RVA: 0x0005D81A File Offset: 0x0005BA1A
	public bool Equals(T other)
	{
		return this.HasValue && EqualityComparer<T>.Default.Equals(this.value, other);
	}

	// Token: 0x0400097F RID: 2431
	[Serialize]
	private readonly bool hasValue;

	// Token: 0x04000980 RID: 2432
	[Serialize]
	private readonly T value;
}
