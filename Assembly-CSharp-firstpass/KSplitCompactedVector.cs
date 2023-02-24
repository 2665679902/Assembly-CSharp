using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x020000AF RID: 175
public class KSplitCompactedVector<Header, Payload> : KCompactedVectorBase, ICollection, IEnumerable
{
	// Token: 0x0600067B RID: 1659 RVA: 0x0001CE9D File Offset: 0x0001B09D
	public KSplitCompactedVector(int initial_count = 0)
		: base(initial_count)
	{
		this.headers = new List<Header>(initial_count);
		this.payloads = new List<Payload>(initial_count);
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0001CEBE File Offset: 0x0001B0BE
	public HandleVector<int>.Handle Allocate(Header header, ref Payload payload)
	{
		this.headers.Add(header);
		this.payloads.Add(payload);
		return base.Allocate(this.headers.Count - 1);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0001CEF0 File Offset: 0x0001B0F0
	public HandleVector<int>.Handle Free(HandleVector<int>.Handle handle)
	{
		int num = this.headers.Count - 1;
		int num2;
		bool flag = base.Free(handle, num, out num2);
		if (flag)
		{
			if (num2 < num)
			{
				this.headers[num2] = this.headers[num];
				this.payloads[num2] = this.payloads[num];
			}
			this.headers.RemoveAt(num);
			this.payloads.RemoveAt(num);
		}
		if (!flag)
		{
			return handle;
		}
		return HandleVector<int>.InvalidHandle;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0001CF70 File Offset: 0x0001B170
	public void GetData(HandleVector<int>.Handle handle, out Header header, out Payload payload)
	{
		int num = base.ComputeIndex(handle);
		header = this.headers[num];
		payload = this.payloads[num];
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0001CFA9 File Offset: 0x0001B1A9
	public Header GetHeader(HandleVector<int>.Handle handle)
	{
		return this.headers[base.ComputeIndex(handle)];
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0001CFBD File Offset: 0x0001B1BD
	public Payload GetPayload(HandleVector<int>.Handle handle)
	{
		return this.payloads[base.ComputeIndex(handle)];
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0001CFD4 File Offset: 0x0001B1D4
	public void SetData(HandleVector<int>.Handle handle, Header new_data0, ref Payload new_data1)
	{
		int num = base.ComputeIndex(handle);
		this.headers[num] = new_data0;
		this.payloads[num] = new_data1;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0001D008 File Offset: 0x0001B208
	public void SetHeader(HandleVector<int>.Handle handle, Header new_data)
	{
		this.headers[base.ComputeIndex(handle)] = new_data;
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0001D01D File Offset: 0x0001B21D
	public void SetPayload(HandleVector<int>.Handle handle, ref Payload new_data)
	{
		this.payloads[base.ComputeIndex(handle)] = new_data;
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0001D037 File Offset: 0x0001B237
	public new virtual void Clear()
	{
		base.Clear();
		this.headers.Clear();
		this.payloads.Clear();
	}

	// Token: 0x170000BF RID: 191
	// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001D055 File Offset: 0x0001B255
	public int Count
	{
		get
		{
			return this.headers.Count;
		}
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0001D062 File Offset: 0x0001B262
	public void GetDataLists(out List<Header> headers, out List<Payload> payloads)
	{
		headers = this.headers;
		payloads = this.payloads;
	}

	// Token: 0x170000C0 RID: 192
	// (get) Token: 0x06000687 RID: 1671 RVA: 0x0001D074 File Offset: 0x0001B274
	public bool IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x170000C1 RID: 193
	// (get) Token: 0x06000688 RID: 1672 RVA: 0x0001D07B File Offset: 0x0001B27B
	public object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0001D082 File Offset: 0x0001B282
	public void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0001D089 File Offset: 0x0001B289
	public IEnumerator GetEnumerator()
	{
		return new KSplitCompactedVector<Header, Payload>.Enumerator(this.headers.GetEnumerator(), this.payloads.GetEnumerator());
	}

	// Token: 0x040005B8 RID: 1464
	protected List<Header> headers;

	// Token: 0x040005B9 RID: 1465
	protected List<Payload> payloads;

	// Token: 0x020009E5 RID: 2533
	private struct Enumerator : IEnumerator
	{
		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x060053BE RID: 21438 RVA: 0x0009C4D4 File Offset: 0x0009A6D4
		public object Current
		{
			get
			{
				return new KSplitCompactedVector<Header, Payload>.Enumerator.Value
				{
					header = this.headerCurrent.Current,
					payload = this.payloadCurrent.Current
				};
			}
		}

		// Token: 0x060053BF RID: 21439 RVA: 0x0009C513 File Offset: 0x0009A713
		public Enumerator(List<Header>.Enumerator headerEnumerator, List<Payload>.Enumerator payloadEnumerator)
		{
			this.headerBegin = headerEnumerator;
			this.payloadBegin = payloadEnumerator;
			this.Reset();
		}

		// Token: 0x060053C0 RID: 21440 RVA: 0x0009C529 File Offset: 0x0009A729
		public bool MoveNext()
		{
			return this.headerCurrent.MoveNext() && this.payloadCurrent.MoveNext();
		}

		// Token: 0x060053C1 RID: 21441 RVA: 0x0009C545 File Offset: 0x0009A745
		public void Reset()
		{
			this.headerCurrent = this.headerBegin;
			this.payloadCurrent = this.payloadBegin;
		}

		// Token: 0x04002229 RID: 8745
		private readonly List<Header>.Enumerator headerBegin;

		// Token: 0x0400222A RID: 8746
		private readonly List<Payload>.Enumerator payloadBegin;

		// Token: 0x0400222B RID: 8747
		private List<Header>.Enumerator headerCurrent;

		// Token: 0x0400222C RID: 8748
		private List<Payload>.Enumerator payloadCurrent;

		// Token: 0x02000B47 RID: 2887
		public struct Value
		{
			// Token: 0x04002696 RID: 9878
			public Header header;

			// Token: 0x04002697 RID: 9879
			public Payload payload;
		}
	}
}
