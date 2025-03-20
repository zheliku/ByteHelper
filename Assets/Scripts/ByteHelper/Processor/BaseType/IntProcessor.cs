// ------------------------------------------------------------
// @file       BaseTypeProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:18
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;

    public class IntProcessor : Processor<int>
    {
        public override int GetBytesLength(int value)
        {
            return sizeof(int);
        }
        
        public override int Write(byte[] bytes, int value, int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            return index + sizeof(int);
        }

        public override int Read(byte[] bytes, int index, out int value)
        {
            value = BitConverter.ToInt32(bytes, index);
            return index + sizeof(int);
        }
    }
}