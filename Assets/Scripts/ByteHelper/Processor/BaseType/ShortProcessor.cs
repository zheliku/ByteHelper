// ------------------------------------------------------------
// @file       ShortProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:42
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;

    public class ShortProcessor : Processor<short>
    {
        public override int GetBytesLength(short value)
        {
            return sizeof(short);
        }
        
        public override int Write(byte[] bytes, short value, int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            return index + sizeof(short);
        }

        public override int Read(byte[] bytes, int index, out short value)
        {
            value = BitConverter.ToInt16(bytes, index);
            return index + sizeof(short);
        }
    }
}