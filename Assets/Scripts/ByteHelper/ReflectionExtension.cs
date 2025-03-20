// ------------------------------------------------------------
// @file       ReflectionExtension.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 17:03:09
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

namespace ByteFormatter
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ReflectionExtension
    {
        public static bool IsAssignableToGenericInterface(this Type type, Type genericInterfaceType)
        {
            // 获取类型实现的所有接口
            var interfaces = type.GetInterfaces();

            // 检查是否有接口是 genericInterfaceType 泛型版本
            foreach (var iface in interfaces)
            {
                if (iface.IsGenericType && iface.GetGenericTypeDefinition() == genericInterfaceType)
                {
                    return true;
                }
            }

            return false;
        }
        
        public static bool HasAttribute<T>(this Type type, bool inherit = false) where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), inherit).Any();
        }
        
        public static FieldInfo[] GetFieldInfos(this object obj, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                                                             BindingFlags.Instance)
        {
            // 获取 MyClass 的 Type 对象
            var type = obj.GetType();

            // 获取所有 public 字段
            var fields = type.GetFields(bindingFlags);

            return fields;
        }

        public static object[] GetFieldValues(this object obj, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic |
                                                                                           BindingFlags.Instance)
        {
            var fields = obj.GetFieldInfos(bindingFlags);

            return fields.Select(field => field.GetValue(obj)).ToArray();
        }
    }
}