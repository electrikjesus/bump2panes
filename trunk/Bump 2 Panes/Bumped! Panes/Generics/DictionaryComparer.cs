using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bump_2_Panes.Generics
{
    public class DictionaryComparer<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public static bool CompareJsonDictionary(Dictionary<string, object> initialDictionary, Dictionary<string, object> comparedDictionary)
        {
            if (initialDictionary.Count == comparedDictionary.Count)
            {
                Boolean isEqual = true;
                foreach (string key in initialDictionary.Keys)
                {
                    if (isEqual)
                    {
                        if (!comparedDictionary.ContainsKey(key))
                        {
                            isEqual = false;
                        }
                        else
                        {
                            switch (initialDictionary[key].GetType().ToString())
                            {
                                case "System.Collections.Generic.Dictionary`2[System.String,System.Object]":
                                    isEqual = CompareJsonDictionary(initialDictionary[key] as Dictionary<string, object>, comparedDictionary[key] as Dictionary<string, object>);
                                    break;
                                case "System.String":
                                    if (initialDictionary[key].ToString() != comparedDictionary[key].ToString())
                                        isEqual = false;
                                    break;
                                case "System.Int32":
                                    if ((Int32)initialDictionary[key] != (Int32)comparedDictionary[key])
                                        isEqual = false;
                                    break;
                            }
                        }
                    }
                }

                return isEqual;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks two Simple Dictionary objects and returns new Dictionary of the same
        /// type with the keys and values that are missing from one to the other
        /// </summary>
        /// <param name="initialDictionary"></param>
        /// <param name="comparedDictionary"></param>
        /// <returns>Dictionary with Missing Values</returns>
        public static Dictionary<TKey, TValue> CompareSimpleDictionary(Dictionary<TKey, TValue> initialDictionary, Dictionary<TKey, TValue> comparedDictionary)
        {
            Dictionary<TKey, TValue> missing = new Dictionary<TKey, TValue>();
            if (initialDictionary.Count != comparedDictionary.Count)
            {
                foreach (TKey key in initialDictionary.Keys)
                {
                    if (!comparedDictionary.ContainsKey(key))
                    {
                        missing.Add(key, initialDictionary[key]);
                    }
                }

                return missing;
            }
            else
            {
                return missing;
            }
        }
    }
}
