// ------------------------------------------------------------
// @file       DoubleProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:38
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;

    public class DoubleProcessor : Processor<double>
    {
        public override int GetBytesLength(double value)
        {
            return sizeof(double);
        }
        
        public override int Write(byte[] bytes, double value, int index)
        {
            BitConverter.GetBytes(value).CopyTo(bytes, index);
            return index + sizeof(double);
        }

        public override int Read(byte[] bytes, int index, out double value)
        {
            value = BitConverter.ToDouble(bytes, index);
            return index + sizeof(double);
        }
    }
}