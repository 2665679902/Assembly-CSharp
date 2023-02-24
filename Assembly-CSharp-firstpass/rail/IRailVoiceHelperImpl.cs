using System;

namespace rail
{
	// Token: 0x020002C9 RID: 713
	public class IRailVoiceHelperImpl : RailObject, IRailVoiceHelper
	{
		// Token: 0x06002A8C RID: 10892 RVA: 0x000562AB File Offset: 0x000544AB
		internal IRailVoiceHelperImpl(IntPtr cPtr)
		{
			this.swigCPtr_ = cPtr;
		}

		// Token: 0x06002A8D RID: 10893 RVA: 0x000562BC File Offset: 0x000544BC
		~IRailVoiceHelperImpl()
		{
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x000562E4 File Offset: 0x000544E4
		public virtual IRailVoiceChannel AsyncCreateVoiceChannel(CreateVoiceChannelOption options, string channel_name, string user_data, out RailResult result)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_CreateVoiceChannelOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			IRailVoiceChannel railVoiceChannel;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailVoiceHelper_AsyncCreateVoiceChannel(this.swigCPtr_, intPtr, channel_name, user_data, out result);
				railVoiceChannel = ((intPtr2 == IntPtr.Zero) ? null : new IRailVoiceChannelImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_CreateVoiceChannelOption(intPtr);
			}
			return railVoiceChannel;
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x00056350 File Offset: 0x00054550
		public virtual IRailVoiceChannel OpenVoiceChannel(RailVoiceChannelID voice_channel_id, string channel_name, out RailResult result)
		{
			IntPtr intPtr = ((voice_channel_id == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceChannelID__SWIG_0());
			if (voice_channel_id != null)
			{
				RailConverter.Csharp2Cpp(voice_channel_id, intPtr);
			}
			IRailVoiceChannel railVoiceChannel;
			try
			{
				IntPtr intPtr2 = RAIL_API_PINVOKE.IRailVoiceHelper_OpenVoiceChannel(this.swigCPtr_, intPtr, channel_name, out result);
				railVoiceChannel = ((intPtr2 == IntPtr.Zero) ? null : new IRailVoiceChannelImpl(intPtr2));
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailVoiceChannelID(intPtr);
			}
			return railVoiceChannel;
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x000563C4 File Offset: 0x000545C4
		public virtual EnumRailVoiceChannelSpeakerState GetSpeakerState()
		{
			return (EnumRailVoiceChannelSpeakerState)RAIL_API_PINVOKE.IRailVoiceHelper_GetSpeakerState(this.swigCPtr_);
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x000563D1 File Offset: 0x000545D1
		public virtual RailResult MuteSpeaker()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_MuteSpeaker(this.swigCPtr_);
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x000563DE File Offset: 0x000545DE
		public virtual RailResult ResumeSpeaker()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_ResumeSpeaker(this.swigCPtr_);
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x000563EC File Offset: 0x000545EC
		public virtual RailResult SetupVoiceCapture(RailVoiceCaptureOption options, RailCaptureVoiceCallback callback)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetupVoiceCapture__SWIG_0(this.swigCPtr_, intPtr, callback);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailVoiceCaptureOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A94 RID: 10900 RVA: 0x0005643C File Offset: 0x0005463C
		public virtual RailResult SetupVoiceCapture(RailVoiceCaptureOption options)
		{
			IntPtr intPtr = ((options == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureOption__SWIG_0());
			if (options != null)
			{
				RailConverter.Csharp2Cpp(options, intPtr);
			}
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetupVoiceCapture__SWIG_1(this.swigCPtr_, intPtr);
			}
			finally
			{
				RAIL_API_PINVOKE.delete_RailVoiceCaptureOption(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0005648C File Offset: 0x0005468C
		public virtual RailResult StartVoiceCapturing(uint duration_milliseconds, bool repeat)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_0(this.swigCPtr_, duration_milliseconds, repeat);
		}

		// Token: 0x06002A96 RID: 10902 RVA: 0x0005649B File Offset: 0x0005469B
		public virtual RailResult StartVoiceCapturing(uint duration_milliseconds)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_1(this.swigCPtr_, duration_milliseconds);
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x000564A9 File Offset: 0x000546A9
		public virtual RailResult StartVoiceCapturing()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StartVoiceCapturing__SWIG_2(this.swigCPtr_);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x000564B6 File Offset: 0x000546B6
		public virtual RailResult StopVoiceCapturing()
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_StopVoiceCapturing(this.swigCPtr_);
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x000564C3 File Offset: 0x000546C3
		public virtual RailResult GetCapturedVoiceData(byte[] buffer, uint buffer_length, out uint encoded_bytes_written)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_GetCapturedVoiceData(this.swigCPtr_, buffer, buffer_length, out encoded_bytes_written);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000564D3 File Offset: 0x000546D3
		public virtual RailResult DecodeVoice(byte[] encoded_buffer, uint encoded_length, byte[] pcm_buffer, uint pcm_buffer_length, out uint pcm_buffer_written)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_DecodeVoice(this.swigCPtr_, encoded_buffer, encoded_length, pcm_buffer, pcm_buffer_length, out pcm_buffer_written);
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000564E8 File Offset: 0x000546E8
		public virtual RailResult GetVoiceCaptureSpecification(RailVoiceCaptureSpecification spec)
		{
			IntPtr intPtr = ((spec == null) ? IntPtr.Zero : RAIL_API_PINVOKE.new_RailVoiceCaptureSpecification__SWIG_0());
			RailResult railResult;
			try
			{
				railResult = (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_GetVoiceCaptureSpecification(this.swigCPtr_, intPtr);
			}
			finally
			{
				if (spec != null)
				{
					RailConverter.Cpp2Csharp(intPtr, spec);
				}
				RAIL_API_PINVOKE.delete_RailVoiceCaptureSpecification(intPtr);
			}
			return railResult;
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00056538 File Offset: 0x00054738
		public virtual RailResult EnableInGameVoiceSpeaking(bool can_speaking)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_EnableInGameVoiceSpeaking(this.swigCPtr_, can_speaking);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x00056546 File Offset: 0x00054746
		public virtual RailResult SetPlayerNicknameInVoiceChannel(string nickname)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetPlayerNicknameInVoiceChannel(this.swigCPtr_, nickname);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x00056554 File Offset: 0x00054754
		public virtual RailResult SetPushToTalkKeyInVoiceChannel(uint push_to_talk_hot_key)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetPushToTalkKeyInVoiceChannel(this.swigCPtr_, push_to_talk_hot_key);
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x00056562 File Offset: 0x00054762
		public virtual uint GetPushToTalkKeyInVoiceChannel()
		{
			return RAIL_API_PINVOKE.IRailVoiceHelper_GetPushToTalkKeyInVoiceChannel(this.swigCPtr_);
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x0005656F File Offset: 0x0005476F
		public virtual RailResult ShowOverlayUI(bool show)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_ShowOverlayUI(this.swigCPtr_, show);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x0005657D File Offset: 0x0005477D
		public virtual RailResult SetMicroVolume(uint volume)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetMicroVolume(this.swigCPtr_, volume);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x0005658B File Offset: 0x0005478B
		public virtual RailResult SetSpeakerVolume(uint volume)
		{
			return (RailResult)RAIL_API_PINVOKE.IRailVoiceHelper_SetSpeakerVolume(this.swigCPtr_, volume);
		}
	}
}
