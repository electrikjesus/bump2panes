using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bump_2_Panes.Generics
{
    public class EventfulDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public delegate void ChangedEventHandler(DictionaryEventArgs changedEventArgs);

        public event ChangedEventHandler ChangedEvent;

        public delegate void AddedEventHandler(DictionaryEventArgs addedEventArgs);

        public event AddedEventHandler AddedEvent;

        public delegate void RemovedEventHandler(DictionaryEventArgs removedEventArgs);

        public event RemovedEventHandler RemovedEvent;

        public delegate void LoadedEventHandler(DictionaryEventArgs loadedEventArgs);

        public event LoadedEventHandler LoadedEvent;

        public class DictionaryEventArgs : EventArgs
        {
            private TKey _key;
            private TValue _value;
            private Dictionary<TKey, TValue> _dict;

            public DictionaryEventArgs(TKey key)
            {
                _key = key;
            }

            public DictionaryEventArgs(TKey key, TValue value)
            {
                _key = key;
                _value = value;
            }

            public DictionaryEventArgs(Dictionary<TKey, TValue> dict)
            {
                _dict = dict;
            }

            public TKey Key
            {
                get
                {
                    return _key;
                }
            }

            public TValue Value
            {
                get
                {
                    return _value;
                }
            }

            public Dictionary<TKey, TValue> Dict
            {
                get
                {
                    return _dict;
                }
            }
        }

        public EventfulDictionary()
        {
            
        }

        public void Change(TKey key, TValue value)
        {
            if (ChangedEvent != null)
                ChangedEvent(new DictionaryEventArgs(key, value));
            base[key] = value;
        }

        public void Add(TKey key, TValue value)
        {
            if (AddedEvent != null)
                AddedEvent(new DictionaryEventArgs(key, value));
            base.Add(key, value);
        }

        public void Remove(TKey key)
        {
            if (RemovedEvent != null)
                RemovedEvent(new DictionaryEventArgs(key));
            base.Remove(key);
        }

        public void Load(Dictionary<TKey, TValue> dict)
        {
            if (LoadedEvent != null)
                LoadedEvent(new DictionaryEventArgs(dict));
            base.Clear();
            foreach (TKey value in dict.Keys)
            {
                base.Add(value, dict[value]);
            }
        }
    }
}
