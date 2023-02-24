using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x020000A3 RID: 163
public class HandleVector<T>
{
	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001C406 File Offset: 0x0001A606
	public List<T> Items
	{
		get
		{
			return this.items;
		}
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001C40E File Offset: 0x0001A60E
	public Stack<HandleVector<T>.Handle> Handles
	{
		get
		{
			return this.freeHandles;
		}
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x0001C416 File Offset: 0x0001A616
	public virtual void Clear()
	{
		this.items.Clear();
		this.freeHandles.Clear();
		this.versions.Clear();
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0001C439 File Offset: 0x0001A639
	public HandleVector(int initial_size)
	{
		this.freeHandles = new Stack<HandleVector<T>.Handle>(initial_size);
		this.items = new List<T>(initial_size);
		this.versions = new List<byte>(initial_size);
		this.Initialize(initial_size);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0001C46C File Offset: 0x0001A66C
	private void Initialize(int size)
	{
		for (int i = size - 1; i >= 0; i--)
		{
			this.freeHandles.Push(new HandleVector<T>.Handle
			{
				index = i
			});
			this.items.Add(default(T));
			this.versions.Add(0);
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0001C4C4 File Offset: 0x0001A6C4
	public virtual HandleVector<T>.Handle Add(T item)
	{
		HandleVector<T>.Handle handle;
		if (this.freeHandles.Count > 0)
		{
			handle = this.freeHandles.Pop();
			byte b;
			int num;
			this.UnpackHandle(handle, out b, out num);
			this.items[num] = item;
		}
		else
		{
			this.versions.Add(0);
			handle = this.PackHandle(this.items.Count);
			this.items.Add(item);
		}
		return handle;
	}

	// Token: 0x0600062F RID: 1583 RVA: 0x0001C530 File Offset: 0x0001A730
	public virtual T Release(HandleVector<T>.Handle handle)
	{
		if (!handle.IsValid())
		{
			return default(T);
		}
		byte b;
		int num;
		this.UnpackHandle(handle, out b, out num);
		b += 1;
		this.versions[num] = b;
		global::Debug.Assert(num >= 0);
		global::Debug.Assert(num < 16777216);
		handle = this.PackHandle(num);
		this.freeHandles.Push(handle);
		T t = this.items[num];
		this.items[num] = default(T);
		return t;
	}

	// Token: 0x06000630 RID: 1584 RVA: 0x0001C5BC File Offset: 0x0001A7BC
	public T GetItem(HandleVector<T>.Handle handle)
	{
		byte b;
		int num;
		this.UnpackHandle(handle, out b, out num);
		return this.items[num];
	}

	// Token: 0x06000631 RID: 1585 RVA: 0x0001C5E0 File Offset: 0x0001A7E0
	private HandleVector<T>.Handle PackHandle(int index)
	{
		global::Debug.Assert(index < 16777216);
		byte b = this.versions[index];
		this.versions[index] = b;
		HandleVector<T>.Handle invalidHandle = HandleVector<T>.InvalidHandle;
		invalidHandle.index = ((int)b << 24) | index;
		return invalidHandle;
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0001C628 File Offset: 0x0001A828
	public void UnpackHandle(HandleVector<T>.Handle handle, out byte version, out int index)
	{
		version = (byte)(handle.index >> 24);
		index = handle.index & 16777215;
		if (this.versions[index] != version)
		{
			throw new ArgumentException("Accessing mismatched handle version. Expected version=" + this.versions[index].ToString() + " but got version=" + version.ToString());
		}
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0001C692 File Offset: 0x0001A892
	public void UnpackHandleUnchecked(HandleVector<T>.Handle handle, out byte version, out int index)
	{
		version = (byte)(handle.index >> 24);
		index = handle.index & 16777215;
	}

	// Token: 0x06000634 RID: 1588 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
	public bool IsValid(HandleVector<T>.Handle handle)
	{
		return (handle.index & 16777215) != 16777215;
	}

	// Token: 0x06000635 RID: 1589 RVA: 0x0001C6CC File Offset: 0x0001A8CC
	public bool IsVersionValid(HandleVector<T>.Handle handle)
	{
		byte b = (byte)(handle.index >> 24);
		int num = handle.index & 16777215;
		return b == this.versions[num];
	}

	// Token: 0x040005A1 RID: 1441
	public static readonly HandleVector<T>.Handle InvalidHandle = HandleVector<T>.Handle.InvalidHandle;

	// Token: 0x040005A2 RID: 1442
	protected Stack<HandleVector<T>.Handle> freeHandles;

	// Token: 0x040005A3 RID: 1443
	protected List<T> items;

	// Token: 0x040005A4 RID: 1444
	protected List<byte> versions;

	// Token: 0x020009E3 RID: 2531
	[DebuggerDisplay("{index}")]
	public struct Handle : IComparable<HandleVector<T>.Handle>, IEquatable<HandleVector<T>.Handle>
	{
		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x060053AE RID: 21422 RVA: 0x0009C2CB File Offset: 0x0009A4CB
		// (set) Token: 0x060053AF RID: 21423 RVA: 0x0009C2D5 File Offset: 0x0009A4D5
		public int index
		{
			get
			{
				return this._index - 1;
			}
			set
			{
				this._index = value + 1;
			}
		}

		// Token: 0x060053B0 RID: 21424 RVA: 0x0009C2E0 File Offset: 0x0009A4E0
		public bool IsValid()
		{
			return this._index != 0;
		}

		// Token: 0x060053B1 RID: 21425 RVA: 0x0009C2EB File Offset: 0x0009A4EB
		public void Clear()
		{
			this._index = 0;
		}

		// Token: 0x060053B2 RID: 21426 RVA: 0x0009C2F4 File Offset: 0x0009A4F4
		public int CompareTo(HandleVector<T>.Handle obj)
		{
			return this._index - obj._index;
		}

		// Token: 0x060053B3 RID: 21427 RVA: 0x0009C304 File Offset: 0x0009A504
		public override bool Equals(object obj)
		{
			HandleVector<T>.Handle handle = (HandleVector<T>.Handle)obj;
			return this._index == handle._index;
		}

		// Token: 0x060053B4 RID: 21428 RVA: 0x0009C326 File Offset: 0x0009A526
		public bool Equals(HandleVector<T>.Handle other)
		{
			return this._index == other._index;
		}

		// Token: 0x060053B5 RID: 21429 RVA: 0x0009C336 File Offset: 0x0009A536
		public override int GetHashCode()
		{
			return this._index;
		}

		// Token: 0x060053B6 RID: 21430 RVA: 0x0009C33E File Offset: 0x0009A53E
		public static bool operator ==(HandleVector<T>.Handle x, HandleVector<T>.Handle y)
		{
			return x._index == y._index;
		}

		// Token: 0x060053B7 RID: 21431 RVA: 0x0009C34E File Offset: 0x0009A54E
		public static bool operator !=(HandleVector<T>.Handle x, HandleVector<T>.Handle y)
		{
			return x._index != y._index;
		}

		// Token: 0x04002222 RID: 8738
		private const int InvalidIndex = 0;

		// Token: 0x04002223 RID: 8739
		private int _index;

		// Token: 0x04002224 RID: 8740
		public static readonly HandleVector<T>.Handle InvalidHandle = new HandleVector<T>.Handle
		{
			_index = 0
		};
	}
}
