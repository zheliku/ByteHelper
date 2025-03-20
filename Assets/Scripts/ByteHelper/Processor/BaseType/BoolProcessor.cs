// ------------------------------------------------------------
// @file       BoolProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:18
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;

    public class BoolProcessor : Processor<bool>
    {
        public override int GetBytesLength(bool value)
        {
            return sizeof(bool);
        }
        
        public override int Write(byte[] bytes, bool value, int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            return index + sizeof(bool);
        }

        public override int Read(byte[] bytes, int index, out bool value)
        {
            value = BitConverter.ToBoolean(bytes, index);
            return index + sizeof(bool);
        }
    }
}