﻿using System;
using System.Diagnostics;
using PKHeX.Core;

namespace SysBot.Pokemon
{
    public class TradePartnerBS
    {
        public uint IDHash { get; }

        public string TID { get; }
        public string SID { get; }
        public string TrainerName { get; }

        public TradePartnerBS(byte[] TIDSID, byte[] trainerNameObject)
        {
            Debug.Assert(TIDSID.Length == 4);
            IDHash = BitConverter.ToUInt32(TIDSID, 0);
            TID = $"{IDHash % 1_000_000:000000}";
            SID = $"{IDHash / 1_000_000:0000}";

            TrainerName = ReadStringFromRAMObject(trainerNameObject);
        }

        public const int MaxByteLengthStringObject = 0x14 + 0x1A;

        public static string ReadStringFromRAMObject(byte[] obj)
        {
            // 0x10 typeinfo/monitor, 0x4 len, char[len]
            const int ofs_len = 0x10;
            const int ofs_chars = 0x14;
            Debug.Assert(obj.Length >= ofs_chars);

            // Detect string length, but be cautious about its correctness (protect against bad data)
            int maxCharCount = (obj.Length - ofs_chars) / 2;
            int length = BitConverter.ToInt32(obj, ofs_len);
            if (length < 0 || length > maxCharCount)
                length = maxCharCount;

            return StringConverter.GetString7b(obj, ofs_chars, length * 2);
        }
    }
}
