using System;
using System.Collections.Generic;

namespace rail
{
	// Token: 0x020002B5 RID: 693
	public class IRailNetworkImpl : RailObject, IRailNetwork
	{
		// Token: 0x06002953 RID: 10579 RVA: 0x000527F5 File Offset: 0x000509F5
		internal IRailNetworkImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x00052804 File Offset: 0x00050A04
		~IRailNetworkImpl()
		{
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x0005282C File Offset: 0x00050A2C
		public virtual RailResult AcceptSessionRequest(RailID local_peer, RailID remote_peer)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_AcceptSessionRequest(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002956 RID: 10582 RVA: 0x000528B4 File Offset: 0x00050AB4
		public virtual RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendData__SWIG_0(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002957 RID: 10583 RVA: 0x00052944 File Offset: 0x00050B44
		public virtual RailResult SendData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendData__SWIG_1(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002958 RID: 10584 RVA: 0x000529D0 File Offset: 0x00050BD0
		public virtual RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendReliableData__SWIG_0(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x00052A60 File Offset: 0x00050C60
		public virtual RailResult SendReliableData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendReliableData__SWIG_1(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600295A RID: 10586 RVA: 0x00052AEC File Offset: 0x00050CEC
		public virtual bool IsDataReady(RailID local_peer, out uint data_len, out uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailNetwork_IsDataReady__SWIG_0(this.swigCPtr_, intPtr, out data_len, out message_type);
			}
			finally
			{
				if (local_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr, local_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x0600295B RID: 10587 RVA: 0x00052B48 File Offset: 0x00050D48
		public virtual bool IsDataReady(RailID local_peer, out uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			bool flag;
			try
			{
				flag = RAIL_API_PINVOKE.IRailNetwork_IsDataReady__SWIG_1(this.swigCPtr_, intPtr, out data_len);
			}
			finally
			{
				if (local_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr, local_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return flag;
		}

		// Token: 0x0600295C RID: 10588 RVA: 0x00052BA4 File Offset: 0x00050DA4
		public virtual RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ReadData__SWIG_0(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (remote_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, remote_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600295D RID: 10589 RVA: 0x00052C30 File Offset: 0x00050E30
		public virtual RailResult ReadData(RailID local_peer, RailID remote_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ReadData__SWIG_1(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (remote_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, remote_peer);
				}
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x0600295E RID: 10590 RVA: 0x00052CBC File Offset: 0x00050EBC
		public virtual RailResult BlockMessageType(RailID local_peer, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_BlockMessageType(this.swigCPtr_, intPtr, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x00052D18 File Offset: 0x00050F18
		public virtual RailResult UnblockMessageType(RailID local_peer, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_UnblockMessageType(this.swigCPtr_, intPtr, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x00052D74 File Offset: 0x00050F74
		public virtual RailResult CloseSession(RailID local_peer, RailID remote_peer)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_CloseSession(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailID(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002961 RID: 10593 RVA: 0x00052DFC File Offset: 0x00050FFC
		public virtual RailResult ResolveHostname(string domain, List<string> ip_list)
		{
			IntPtr intPtr = ((ip_list == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailArrayRailString__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ResolveHostname(this.swigCPtr_, domain, intPtr);
			}
			finally
			{
				if (ip_list != null)
				{
					RailConverter.Cpp2Csharp(intPtr, ip_list);
				}
				RAIL_API_PINVOKE.delete_RailArrayRailString(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x00052E4C File Offset: 0x0005104C
		public virtual RailResult GetSessionState(RailID remote_peer, RailNetworkSessionState session_state)
		{
			IntPtr intPtr = ((remote_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (remote_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_peer, intPtr);
			}
			IntPtr intPtr2 = ((session_state == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailNetworkSessionState__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_GetSessionState(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (session_state != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, session_state);
				}
				RAIL_API_PINVOKE.delete_RailNetworkSessionState(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00052EC8 File Offset: 0x000510C8
		public virtual RailResult ForbidSessionRelay(bool forbid_relay)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailNetwork_ForbidSessionRelay(this.swigCPtr_, forbid_relay);
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00052ED8 File Offset: 0x000510D8
		public virtual RailResult SendRawData(RailID local_peer, RailGamePeer remote_game_peer, byte[] data_buf, uint data_len, bool reliable, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_game_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGamePeer__SWIG_0());
			if (remote_game_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_game_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_SendRawData(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, reliable, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailGamePeer(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x00052F5C File Offset: 0x0005115C
		public virtual RailResult AcceptRawSessionRequest(RailID local_peer, RailGamePeer remote_game_peer)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_game_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGamePeer__SWIG_0());
			if (remote_game_peer != null)
			{
				RailConverter.Csharp2Cpp(remote_game_peer, intPtr2);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_AcceptRawSessionRequest(this.swigCPtr_, intPtr, intPtr2);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				RAIL_API_PINVOKE.delete_RailGamePeer(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00052FD8 File Offset: 0x000511D8
		public virtual RailResult ReadRawData(RailID local_peer, RailGamePeer remote_game_peer, byte[] data_buf, uint data_len, uint message_type)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_game_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGamePeer__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ReadRawData__SWIG_0(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len, message_type);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (remote_game_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, remote_game_peer);
				}
				RAIL_API_PINVOKE.delete_RailGamePeer(intPtr2);
			}
			return railResult;
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x00053058 File Offset: 0x00051258
		public virtual RailResult ReadRawData(RailID local_peer, RailGamePeer remote_game_peer, byte[] data_buf, uint data_len)
		{
			IntPtr intPtr = ((local_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailID__SWIG_0());
			if (local_peer != null)
			{
				RailConverter.Csharp2Cpp(local_peer, intPtr);
			}
			IntPtr intPtr2 = ((remote_game_peer == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailGamePeer__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailNetwork_ReadRawData__SWIG_1(this.swigCPtr_, intPtr, intPtr2, data_buf, data_len);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailID(intPtr);
				if (remote_game_peer != null)
				{
					RailConverter.Cpp2Csharp(intPtr2, remote_game_peer);
				}
				RAIL_API_PINVOKE.delete_RailGamePeer(intPtr2);
			}
			return railResult;
		}
	}
}
