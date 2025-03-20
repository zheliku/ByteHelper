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
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine.UI;
    using ByteFormatter;

    public class Test_CollectionType : MonoBehaviour
    {
        public Button Btn;

        public Text Text;

        [Header("Set Your CollectionType, Only List Can be edit in Editor")]
        public List<int> List = new List<int>();

        public List<List<string>> NestedList = new List<List<string>>() // Supports nested types
        {
            new List<string>() { "hi", "hello" },
            new List<string>() { "world", "zheliku" }
        };

        public LinkedList<bool> LinkedList = new LinkedList<bool>();

        public HashSet<short> HashSet = new HashSet<short>()
        {
            1, 2, 3, 4, 5
        };

        public Dictionary<string, List<float>> Dictionary = new Dictionary<string, List<float>>()
        {
            { "hi", new List<float>() { 1.2f, -9f, 6.3f } },
            { "hello", new List<float>() { 1.8f, 2.5f } }
        };

        private void Start()
        {
            LinkedList.AddLast(true);
            LinkedList.AddLast(false);
            LinkedList.AddLast(true);

            SerializeData();

            Text.text = "Serialize Data Success!";

            Btn.onClick.AddListener(OnClick);
        }

        private void SerializeData()
        {
            ByteHelper.SaveBytes($"CollectionType/{nameof(List)}", List);
            ByteHelper.SaveBytes($"CollectionType/{nameof(NestedList)}", NestedList);
            ByteHelper.SaveBytes($"CollectionType/{nameof(LinkedList)}", LinkedList);
            ByteHelper.SaveBytes($"CollectionType/{nameof(HashSet)}", HashSet);
            ByteHelper.SaveBytes($"CollectionType/{nameof(Dictionary)}", Dictionary);
        }

        public void OnClick()
        {
            Text.text = "List: " + CollectionInfo(ByteHelper.LoadBytes<List<int>>($"CollectionType/{nameof(List)}")) + "\n" +
                        "NestedList: " + CollectionInfo(ByteHelper.LoadBytes<List<List<string>>>("CollectionType/" + nameof(NestedList))) + "\n" +
                        "LinkedList: " + CollectionInfo(ByteHelper.LoadBytes<LinkedList<bool>>($"CollectionType/{nameof(LinkedList)}")) + "\n" +
                        "HashSet: " + CollectionInfo(ByteHelper.LoadBytes<HashSet<short>>($"CollectionType/{nameof(HashSet)}")) + "\n" +
                        "Dictionary: " + CollectionInfo(ByteHelper.LoadBytes<Dictionary<string, List<float>>>($"CollectionType/{nameof(Dictionary)}"));
        }

        public string CollectionInfo(object collection)
        {
            if (collection is IDictionary dict)
            {
                return $"[{string.Join(", ", dict.Keys.Cast<object>().Select(k => $"{CollectionInfo(k)}: {CollectionInfo(dict[k])}"))}]";
            }
            if (collection is IEnumerable ie and not string)
            {
                var sb = new StringBuilder("[");
                foreach (var item in ie)
                {
                    sb.Append($"{CollectionInfo(item)}, ");
                }
                if (sb.Length > 1)
                {
                    sb.Remove(sb.Length - 2, 2); // Remove the last ", "
                }
                sb.Append("]");
                return sb.ToString();
            }
            return collection.ToString();
        }
    }
}