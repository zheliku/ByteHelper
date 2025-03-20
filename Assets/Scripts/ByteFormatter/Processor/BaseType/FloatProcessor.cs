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

    public class FloatProcessor : Processor<float>
    {
        public override int GetBytesLength(float value)
        {
            return sizeof(float);
        }
        
        public override int Write(byte[] bytes, float value, int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            return index + sizeof(float);
        }

        public override int Read(byte[] bytes, int index, out float value)
        {
            value = BitConverter.ToSingle(bytes, index);
            return index + sizeof(float);
        }
    }
}