using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002CA RID: 714
	public class IRailZoneServerImpl : RailObject, IRailZoneServer, IRailComponent
	{
		// Token: 0x06002AA3 RID: 10915 RVA: 0x00056599 File Offset: 0x00054799
		internal IRailZoneServerImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000565A8 File Offset: 0x000547A8
		~IRailZoneServerImpl()
		{
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x000565D0 File Offset: 0x000547D0
		public virtual RailZoneID GetZoneID()
		{
			IntPtr intPtr = RAIL_API_PINVOKE.IRailZoneServer_GetZoneID(this.swigCPtr_);
			RailZoneID railZoneID = new RailZoneID();
			RailConverter.Cpp2Csharp(intPtr, railZoneID);
			return railZoneID;
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000565F8 File Offset: 0x000547F8
		public virtual RailResult GetZoneNameLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneNameLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (languages != null)
				{
					RailConverter.Cpp2Csharp(intPtr, languages);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00056648 File Offset: 0x00054848
		public virtual RailResult GetZoneName(string language_filter, out string zone_name)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneName(this.swigCPtr_, language_filter, intPtr);
			}
			finally
			{
				zone_name = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x00056690 File Offset: 0x00054890
		public virtual RailResult GetZoneDescriptionLanguages(List<string> languages)
		{
			IntPtr intPtr = ((languages == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneDescriptionLanguages(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (languages != null)
				{
					RailConverter.Cpp2Csharp(intPtr, languages);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000566E0 File Offset: 0x000548E0
		public virtual RailResult GetZoneDescription(string language_filter, out string zone_description)
		{
			IntPtr intPtr = RAIL_API_PINVOKE.new_RailString__SWIG_0();
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneDescription(this.swigCPtr_, language_filter, intPtr);
			}
			finally
			{
				zone_description = UTF8Marshaler.MarshalNativeToString(RAIL_API_PINVOKE.RailString_c_str(intPtr));
				RAIL_API_PINVOKE.delete_RailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x00056728 File Offset: 0x00054928
		public virtual RailResult GetGameServerAddresses(List<string> server_addresses)
		{
			IntPtr intPtr = ((server_addresses == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetGameServerAddresses(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (server_addresses != null)
				{
					RailConverter.Cpp2Csharp(intPtr, server_addresses);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x00056778 File Offset: 0x00054978
		public virtual RailResult GetZoneMetadatas(List<RailKeyValue> key_values)
		{
			IntPtr intPtr = ((key_values == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailKeyValue__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetZoneMetadatas(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (key_values != null)
				{
					RailConverter.Cpp2Csharp(intPtr, key_values);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailKeyValue(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000567C8 File Offset: 0x000549C8
		public virtual RailResult GetChildrenZoneIDs(List<RailZoneID> zone_ids)
		{
			IntPtr intPtr = ((zone_ids == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailZoneID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailZoneServer_GetChildrenZoneIDs(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (zone_ids != null)
				{
					RailConverter.Cpp2Csharp(intPtr, zone_ids);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailZoneID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x00056818 File Offset: 0x00054A18
		public virtual bool IsZoneVisiable()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_IsZoneVisiable(this.swigCPtr_);
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00056825 File Offset: 0x00054A25
		public virtual bool IsZoneJoinable()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_IsZoneJoinable(this.swigCPtr_);
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x00056832 File Offset: 0x00054A32
		public virtual uint GetZoneEnableStartTime()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_GetZoneEnableStartTime(this.swigCPtr_);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x0005683F File Offset: 0x00054A3F
		public virtual uint GetZoneEnableEndTime()
		{
			return RAIL_API_PINVOKE.IRailZoneServer_GetZoneEnableEndTime(this.swigCPtr_);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0005684C File Offset: 0x00054A4C
		public virtual ulong GetComponentVersion()
		{
			return RAIL_API_PINVOKE.IRailComponent_GetComponentVersion(this.swigCPtr_);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x00056859 File Offset: 0x00054A59
		public virtual void Release()
		{
			RAIL_API_PINVOKE.IRailComponent_Release(this.swigCPtr_);
		}
	}
}
