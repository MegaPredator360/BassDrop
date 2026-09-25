namespace BaseDrop
{
    internal enum snd_asset_channel : int
	{
		SND_ASSET_CHANNEL_L = 0x1,
		SND_ASSET_CHANNEL_R = 0x2,
		SND_ASSET_CHANNEL_C = 0x4,
		SND_ASSET_CHANNEL_LFE = 0x8,
		SND_ASSET_CHANNEL_LS = 0x10,
		SND_ASSET_CHANNEL_RS = 0x20,
		SND_ASSET_CHANNEL_LB = 0x40,
		SND_ASSET_CHANNEL_RB = 0x80,
	};

	internal enum snd_asset_flags : int
	{
		SND_ASSET_FLAG_DEFAULT = 0x0,
		SND_ASSET_FLAG_LOOPING = 0x1,
		SND_ASSET_FLAG_PAD_LOOP_BUFFER = 0x2,
	};

	internal enum snd_asset_format : int
	{
		SND_ASSET_FORMAT_PCMS16 = 0x0,
		SND_ASSET_FORMAT_PCMS24 = 0x1,
		SND_ASSET_FORMAT_PCMS32 = 0x2,
		SND_ASSET_FORMAT_IEEE = 0x3,
		SND_ASSET_FORMAT_XMA4 = 0x4,
		SND_ASSET_FORMAT_MP3 = 0x5,
		SND_ASSET_FORMAT_MSADPCM = 0x6,
		SND_ASSET_FORMAT_WMA = 0x7,
	};

    internal struct snd_asset
	{
        public uint version { get; set; }
        public uint frame_count { get; set; }
        public uint frame_rate { get; set; }
        public uint channel_count { get; set; }
        public uint header_size { get; set; }
        public uint block_size { get; set; }
        public uint buffer_size { get; set; }
        public snd_asset_format format { get; set; }
        public snd_asset_channel channel_flags { get; set; }
        public snd_asset_flags flags { get; set; }
        public uint seek_table_count { get; set; }
        public uint seek_ptr { get; set; }
        public uint data_size { get; set; }
        public uint data_ptr { get; set; }
	};
}
