using System;
using System.IO;

namespace BaseDrop
{
    internal static class AudioInfo
    {
        // Reads the duration from the file header, null if it can't be worked out (FastFiles, unknown files)
        public static TimeSpan? GetDuration(string FilePath)
        {
            try
            {
                if (!File.Exists(FilePath) || FilePath.ToLower().EndsWith(".ff"))
                    return null;

                using (BinaryReader readFile = new BinaryReader(File.OpenRead(FilePath)))
                {
                    if (readFile.BaseStream.Length < 16)
                        return null;
                    // Check for a BO1 file, same check as the converter
                    int Version = readFile.ReadInt32();
                    readFile.BaseStream.Position += 8;
                    int Channels = readFile.ReadInt32();
                    readFile.BaseStream.Position = 0;
                    if (Version == 1 && (Channels > 0 && Channels < 10))
                    {
                        var Header = readFile.ReadStruct<snd_asset>();
                        // Files made by BassDrop leave the frame count empty
                        if (Header.frame_count == 0 || Header.frame_rate == 0)
                            return null;
                        return TimeSpan.FromSeconds((double)Header.frame_count / Header.frame_rate);
                    }
                    // Otherwise a RIFF file, walk the chunks for the byte rate and the data size
                    if (readFile.ReadUInt32() != 0x46464952) // RIFF
                        return null;
                    readFile.BaseStream.Position = 12;
                    uint ByteRate = 0;
                    while (readFile.BaseStream.Position + 8 <= readFile.BaseStream.Length)
                    {
                        uint ChunkID = readFile.ReadUInt32();
                        uint ChunkSize = readFile.ReadUInt32();
                        long ChunkStart = readFile.BaseStream.Position;
                        switch (ChunkID)
                        {
                            case 0x20746D66: // fmt
                                readFile.BaseStream.Position += 8;
                                ByteRate = readFile.ReadUInt32();
                                break;
                            case 0x61746164: // data
                                if (ByteRate == 0)
                                    return null;
                                return TimeSpan.FromSeconds((double)ChunkSize / ByteRate);
                        }
                        // Chunks are word aligned
                        readFile.BaseStream.Position = ChunkStart + ChunkSize + (ChunkSize & 1);
                    }
                }
            }
            catch
            {
                // Unreadable, no duration
            }
            return null;
        }

        public static string FormatDuration(TimeSpan? Duration)
        {
            if (Duration == null)
                return "—";
            var Value = Duration.Value;
            return Value.TotalHours >= 1 ? Value.ToString(@"h\:mm\:ss") : Value.ToString(@"m\:ss");
        }

        public static string FormatSize(long Bytes)
        {
            if (Bytes < 1024)
                return Bytes + " B";
            if (Bytes < 1024 * 1024)
                return (Bytes / 1024.0).ToString("0.0") + " KB";
            return (Bytes / (1024.0 * 1024.0)).ToString("0.0") + " MB";
        }
    }
}
