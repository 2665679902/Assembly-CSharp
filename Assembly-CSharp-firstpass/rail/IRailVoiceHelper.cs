using System;

namespace rail
{
	// Token: 0x02000457 RID: 1111
	public interface IRailVoiceHelper
	{
		// Token: 0x060030A8 RID: 12456
		IRailVoiceChannel AsyncCreateVoiceChannel(CreateVoiceChannelOption options, string channel_name, string user_data, out RailResult result);

		// Token: 0x060030A9 RID: 12457
		IRailVoiceChannel OpenVoiceChannel(RailVoiceChannelID voice_channel_id, string channel_name, out RailResult result);

		// Token: 0x060030AA RID: 12458
		EnumRailVoiceChannelSpeakerState GetSpeakerState();

		// Token: 0x060030AB RID: 12459
		RailResult MuteSpeaker();

		// Token: 0x060030AC RID: 12460
		RailResult ResumeSpeaker();

		// Token: 0x060030AD RID: 12461
		RailResult SetupVoiceCapture(RailVoiceCaptureOption options, RailCaptureVoiceCallback callback);

		// Token: 0x060030AE RID: 12462
		RailResult SetupVoiceCapture(RailVoiceCaptureOption options);

		// Token: 0x060030AF RID: 12463
		RailResult StartVoiceCapturing(uint duration_milliseconds, bool repeat);

		// Token: 0x060030B0 RID: 12464
		RailResult StartVoiceCapturing(uint duration_milliseconds);

		// Token: 0x060030B1 RID: 12465
		RailResult StartVoiceCapturing();

		// Token: 0x060030B2 RID: 12466
		RailResult StopVoiceCapturing();

		// Token: 0x060030B3 RID: 12467
		RailResult GetCapturedVoiceData(byte[] buffer, uint buffer_length, out uint encoded_bytes_written);

		// Token: 0x060030B4 RID: 12468
		RailResult DecodeVoice(byte[] encoded_buffer, uint encoded_length, byte[] pcm_buffer, uint pcm_buffer_length, out uint pcm_buffer_written);

		// Token: 0x060030B5 RID: 12469
		RailResult GetVoiceCaptureSpecification(RailVoiceCaptureSpecification spec);

		// Token: 0x060030B6 RID: 12470
		RailResult EnableInGameVoiceSpeaking(bool can_speaking);

		// Token: 0x060030B7 RID: 12471
		RailResult SetPlayerNicknameInVoiceChannel(string nickname);

		// Token: 0x060030B8 RID: 12472
		RailResult SetPushToTalkKeyInVoiceChannel(uint push_to_talk_hot_key);

		// Token: 0x060030B9 RID: 12473
		uint GetPushToTalkKeyInVoiceChannel();

		// Token: 0x060030BA RID: 12474
		RailResult ShowOverlayUI(bool show);

		// Token: 0x060030BB RID: 12475
		RailResult SetMicroVolume(uint volume);

		// Token: 0x060030BC RID: 12476
		RailResult SetSpeakerVolume(uint volume);
	}
}
