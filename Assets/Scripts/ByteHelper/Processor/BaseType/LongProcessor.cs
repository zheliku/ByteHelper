// ------------------------------------------------------------
// @file       LongProcessoor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:39
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;

    public class LongProcessor : Processor<long>
    {
        public override int GetBytesLength(long value)
        {
            return sizeof(long);
        }
        
        public override int Write(byte[] bytes, long value, int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            return index + sizeof(long);
        }

        public override int Read(byte[] bytes, int index, out long value)
        {
            value = BitConverter.ToInt64(bytes, index);
            return index + sizeof(long);
        }
    }
}