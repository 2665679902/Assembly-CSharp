using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002BA RID: 698
	public class IRailRoomHelperImpl : RailObject, IRailRoomHelper
	{
		// Token: 0x060029BE RID: 10686 RVA: 0x00053D8E File Offset: 0x00051F8E
		internal IRailRoomHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00053DA0 File Offset: 0x00051FA0
		~IRailRoomHelperImpl()
		{
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x00053DC8 File Offset: 0x00051FC8
		public virtual IRailRoom CreateRoom(RoomOptions options, string room_name, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RoomOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailRoom railRoom;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailRoomHelper_CreateRoom(this.swigCPtr_, intPtr, room_name, out result);
				railRoom = ((intPtr2 == IntPtr.Zero) ? null : new IRailRoomImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RoomOptions(intPtr);
			}
			return railRoom;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00053E30 File Offset: 0x00052030
		public virtual IRailRoom AsyncCreateRoom(RoomOptions options, string room_name, string user_data)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RoomOptions__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailRoom railRoom;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailRoomHelper_AsyncCreateRoom(this.swigCPtr_, intPtr, room_name, user_data);
				railRoom = ((intPtr2 == IntPtr.Zero) ? null : new IRailRoomImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RoomOptions(intPtr);
			}
			return railRoom;
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00053E98 File Offset: 0x00052098
		public virtual IRailRoom OpenRoom(ulong room_id, out RailResult result)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoomHelper_OpenRoom(this.swigCPtr_, room_id, out result);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailRoomImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x00053EC8 File Offset: 0x000520C8
		public virtual IRailRoom AsyncOpenRoom(ulong room_id, string user_data)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailRoomHelper_AsyncOpenRoom(this.swigCPtr_, room_id, user_data);
			if (!(intPtr == IntPtr.Zero))
			{
				return new IRailRoomImpl(intPtr);
			}
			return null;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00053EF8 File Offset: 0x000520F8
		public virtual RailResult AsyncGetRoomList(uint start_index, uint end_index, List<RoomInfoListSorter> sorter, List<RoomInfoListFilter> filter, string user_data)
		{
			IntPtr intPtr = ((sorter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRoomInfoListSorter__SWIG_0());
			if (sorter != null)
			{
				RailConverter.Csharp2Cpp(sorter, intPtr);
			}
			IntPtr intPtr2 = ((filter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRoomInfoListFilter__SWIG_0());
			if (filter != null)
			{
				RailConverter.Csharp2Cpp(filter, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoomHelper_AsyncGetRoomList(this.swigCPtr_, start_index, end_index, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRoomInfoListSorter(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRoomInfoListFilter(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00053F70 File Offset: 0x00052170
		public virtual RailResult AsyncGetRoomListByTags(uint start_index, uint end_index, List<RoomInfoListSorter> sorter, List<string> room_tags, string user_data)
		{
			IntPtr intPtr = ((sorter == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRoomInfoListSorter__SWIG_0());
			if (sorter != null)
			{
				RailConverter.Csharp2Cpp(sorter, intPtr);
			}
			IntPtr intPtr2 = ((room_tags == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			if (room_tags != null)
			{
				RailConverter.Csharp2Cpp(room_tags, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailRoomHelper_AsyncGetRoomListByTags(this.swigCPtr_, start_index, end_index, intPtr, intPtr2, user_data);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailArrayRoomInfoListSorter(intPtr);
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr2);
			}
			return railResult;
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00053FE8 File Offset: 0x000521E8
		public virtual RailResult AsyncGetUserRoomList(string user_data)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailRoomHelper_AsyncGetUserRoomList(this.swigCPtr_, user_data);
		}
	}
}
