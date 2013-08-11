using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClient.Containers
{
    public class MultiValueConcurrentDictionary<Key, Value> : ConcurrentDictionary<Key, ConcurrentBag<Value>>
    {
        public void Add(Key key, Value value)
        {
            ConcurrentBag<Value> values;
            if (!TryGetValue(key, out values))
            {
                lock (this) // double check locking like a boss
                {
                    if (!TryGetValue(key, out values))
                    {
                        values = new ConcurrentBag<Value>();
                        this[key] = values;
                    }
                }
            }

            values.Add(value);
        }
    }
}
