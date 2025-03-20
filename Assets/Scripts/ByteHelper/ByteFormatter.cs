// ------------------------------------------------------------
// @file       ByteFormatter.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 01:03:15
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ByteSerializableAttribute : Attribute
    { }

    public class ByteFormatter
    {
        // 定义字典，用于存储预置类型的处理器
        public static readonly Dictionary<Type, object> PRIMITIVE_PROCESSORS = new Dictionary<Type, object>()
        {
            { typeof(int), new IntProcessor() },
            { typeof(short), new ShortProcessor() },
            { typeof(long), new LongProcessor() },
            { typeof(float), new FloatProcessor() },
            { typeof(double), new DoubleProcessor() },
            { typeof(bool), new BoolProcessor() },
            { typeof(char), new CharProcessor() },
            { typeof(byte), new ByteProcessor() },
            { typeof(string), new StringProcessor() },
        };

        // 根据类型获取对应的处理器
        public static Processor<T> GetProcessor<T>()
        {
            return (Processor<T>) GetProcessor(typeof(T));
        }

        public static Processor GetProcessor(Type type)
        {
            // 在预置类型的字典中，直接读取
            if (PRIMITIVE_PROCESSORS.TryGetValue(type, out object value))
            {
                return (Processor) value;
            }

            // 如果类型是 KeyValuePair，则使用 KeyValuePairProcessor
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>))
            {
                var processorType = typeof(KeyValuePairProcessor<,>).MakeGenericType(type.GetGenericArguments());
                var processor     = (Processor) Activator.CreateInstance(processorType);
                PRIMITIVE_PROCESSORS.Add(type, processor); // 添加到字典中
                return processor;
            }

            // 如果类型是字典，则使用 IDictionaryProcessor
            if (type.IsAssignableToGenericInterface(typeof(IDictionary<,>)))
            {
                var processorType = typeof(IDictionaryProcessor<,,>).MakeGenericType(type, type.GetGenericArguments()[0], type.GetGenericArguments()[1]);
                var processor     = (Processor) Activator.CreateInstance(processorType);
                PRIMITIVE_PROCESSORS.Add(type, processor); // 添加到字典中
                return processor;
            }

            // 如果是集合类型，则使用 ICollectionProcessor
            if (type.IsAssignableToGenericInterface(typeof(ICollection<>)))
            {
                var processorType = typeof(ICollectionProcessor<,>).MakeGenericType(type, type.GetGenericArguments()[0]);
                var processor     = (Processor) Activator.CreateInstance(processorType);
                PRIMITIVE_PROCESSORS.Add(type, processor); // 添加到字典中
                return processor;
            }

            // 如果拥有 ByteSerializableAttribute 特性，则使用 CustomTypeProcessor
            if (type.HasAttribute<ByteSerializableAttribute>())
            {
                var processorType = typeof(CustomTypeProcessor<>).MakeGenericType(type);
                var processor     = (Processor) Activator.CreateInstance(processorType);
                PRIMITIVE_PROCESSORS.Add(type, processor); // 添加到字典中
                return processor;
            }

            throw new Exception($"Type {type} is not supported");
        }

        public static int Write<T>(byte[] bytes, T value, int index)
        {
            return GetProcessor<T>().Write(bytes, value, index);
        }

        public static int Write(byte[] bytes, object value, int index)
        {
            return GetProcessor(value.GetType()).Write(bytes, value, index);
        }

        public static int Read<T>(byte[] bytes, int index, out T value)
        {
            return GetProcessor<T>().Read(bytes, index, out value);
        }

        public static int Read(byte[] bytes, int index, Type type, out object value)
        {
            return GetProcessor(type).Read(bytes, index, out value);
        }

        public static int GetBytesLength(params object[] value)
        {
            return value.Sum(o => GetProcessor(o.GetType()).GetBytesLength(o));
        }
    }
}