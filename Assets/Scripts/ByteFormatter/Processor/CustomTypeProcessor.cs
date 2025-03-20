// ------------------------------------------------------------
// @file       ConvetibleProcessor.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:16
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;
    using System.Linq;

    public class CustomTypeProcessor<TValue> : Processor<TValue>
    {
        public override int GetBytesLength(TValue value)
        {
            return sizeof(int) + value.GetFieldValues().Sum(v => ByteFormatter.GetBytesLength(v));
        }

        public override int Write(byte[] bytes, TValue value, int index)
        {
            // 先写长度，以便读取
            BitConverter.GetBytes(GetBytesLength(value)).CopyTo(bytes, index);
            index += sizeof(int);

            var fieldValues = value.GetFieldValues();
            foreach (var fieldValue in fieldValues)
            {
                index = ByteFormatter.Write(bytes, fieldValue, index);
            }

            return index;
        }

        public override int Read(byte[] bytes, int index, out TValue value)
        {
            var obj = (object) Activator.CreateInstance<TValue>(); // 装箱，以防 TValue 为值类型
            index += sizeof(int);

            var fieldInfos = obj.GetFieldInfos();
            foreach (var fieldInfo in fieldInfos)
            {
                index = ByteFormatter.Read(bytes, index, fieldInfo.FieldType, out var v);
                fieldInfo.SetValue(obj, v);
            }
            
            value = (TValue) obj; // 拆箱，还原 value

            return index;
        }
    }
}