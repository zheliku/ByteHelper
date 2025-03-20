// ------------------------------------------------------------
// @file       BaseType.cs
// @brief
// @author     zheliku
// @Modified   2025-03-20 17:03:53
// @Copyright  Copyright (c) 2025, zheliku
// ------------------------------------------------------------

using UnityEngine;

namespace Test
{
    using System;
    using System.Collections.Generic;
    using UnityEngine.UI;
    using ByteFormatter;

    [Serializable] [ByteSerializable]
    public class CustomData
    {
        public int        Id;
        public string     Name;
        public List<int>  List = new List<int>();
        public NestedData NestedData; // Supports nested types

        public override string ToString()
        {
            return $"{{ Id: {Id}, " +
                   $"Name: {Name}, " +
                   $"List: [{string.Join(", ", List)}], " +
                   $"NestedData: {NestedData} }}";
        }
    }

    [Serializable] [ByteSerializable]
    public struct NestedData
    {
        public bool Bool;

        public override string ToString()
        {
            return $"{{ Bool: {Bool} }}";
        }
    }

    public class Test_CustomType : MonoBehaviour
    {
        public Button Btn;

        public Text Text;

        [Header("Set Your CustomData")]
        public CustomData CustomData;

        public NestedData NestedData;

        private void Start()
        {
            SerializeData();

            Text.text = "Serialize Data Success!";

            Btn.onClick.AddListener(OnClick);
        }

        private void SerializeData()
        {
            ByteHelper.SaveBytes($"CustomType/{nameof(CustomData)}", CustomData);
            ByteHelper.SaveBytes($"CustomType/{nameof(NestedData)}", NestedData);
        }

        public void OnClick()
        {
            Text.text = "CustomData: " + ByteHelper.LoadBytes<CustomData>($"CustomType/{nameof(CustomData)}") + "\n\n" +
                        "NestedData: " + ByteHelper.LoadBytes<NestedData>($"CustomType/{nameof(NestedData)}");
        }
    }
}