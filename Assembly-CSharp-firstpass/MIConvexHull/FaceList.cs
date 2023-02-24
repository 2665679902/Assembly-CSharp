using System;

namespace MIConvexHull
{
	// Token: 0x0200049D RID: 1181
	internal sealed class FaceList
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x00069962 File Offset: 0x00067B62
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x0006996A File Offset: 0x00067B6A
		public ConvexFaceInternal First { get; private set; }

		// Token: 0x060032CE RID: 13006 RVA: 0x00069973 File Offset: 0x00067B73
		private void AddFirst(ConvexFaceInternal face)
		{
			face.InList = true;
			this.First.Previous = face;
			face.Next = this.First;
			this.First = face;
		}

		// Token: 0x060032CF RID: 13007 RVA: 0x0006999C File Offset: 0x00067B9C
		public void Add(ConvexFaceInternal face)
		{
			if (face.InList)
			{
				if (this.First.VerticesBeyond.Count < face.VerticesBeyond.Count)
				{
					this.Remove(face);
					this.AddFirst(face);
				}
				return;
			}
			face.InList = true;
			if (this.First != null && this.First.VerticesBeyond.Count < face.VerticesBeyond.Count)
			{
				this.First.Previous = face;
				face.Next = this.First;
				this.First = face;
				return;
			}
			if (this.last != null)
			{
				this.last.Next = face;
			}
			face.Previous = this.last;
			this.last = face;
			if (this.First == null)
			{
				this.First = face;
			}
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x00069A60 File Offset: 0x00067C60
		public void Remove(ConvexFaceInternal face)
		{
			if (!face.InList)
			{
				return;
			}
			face.InList = false;
			if (face.Previous != null)
			{
				face.Previous.Next = face.Next;
			}
			else if (face.Previous == null)
			{
				this.First = face.Next;
			}
			if (face.Next != null)
			{
				face.Next.Previous = face.Previous;
			}
			else if (face.Next == null)
			{
				this.last = face.Previous;
			}
			face.Next = null;
			face.Previous = null;
		}

		// Token: 0x040011A1 RID: 4513
		private ConvexFaceInternal last;
	}
}
