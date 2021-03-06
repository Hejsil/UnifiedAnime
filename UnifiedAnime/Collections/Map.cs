﻿#region Licence and Terms
// MoreCollections
// https://github.com/more-dotnet/more-collections
//
// Copyright © Darko Jurić, 2015 
// darko.juric2@gmail.com
//
//The MIT License (MIT)
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:
//
//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.
//
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.
#endregion

using System.Collections;
using System.Collections.Generic;

namespace UnifiedAnime.Collections
{
    //Taken from: http://stackoverflow.com/questions/10966331/two-way-bidirectional-dictionary-in-c" and modified.
    /// <summary>
    /// Two-way dictionary (keyA - keyB). The between key association is 1-1.
    /// </summary>
    /// <typeparam name="T1">First key type.</typeparam>
    /// <typeparam name="T2">Second key type.</typeparam>
    public class Map<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
    {
        /// <summary>
        /// Represents all associations associated with a key.
        /// </summary>
        /// <typeparam name="T3">First key type.</typeparam>
        /// <typeparam name="T4">Second key type.</typeparam>
        public class Indexer<T3, T4> : IEnumerable<T3>
        {
            private readonly Dictionary<T3, T4> _dictionary;
            /// <summary>
            /// Creates new instance from a dictionary.
            /// </summary>
            /// <param name="dictionary">Association dictionary.</param>
            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }

            /// <summary>
            /// Gets the associated key.
            /// </summary>
            /// <param name="index">Key.</param>
            /// <returns></returns>
            public T4 this[T3 index]
            {
                get { return _dictionary[index]; }
                set { _dictionary[index] = value; }
            }

            /// <summary>
            /// Gets the associated key.
            /// </summary>
            /// <param name="index">Key.</param>
            /// <param name="value">Associated value (other key).</param>
            /// <returns>Returns true if the specified key exists, otherwise returns false.</returns>
            public bool TryGetValue(T3 index, out T4 value)
            {
                return _dictionary.TryGetValue(index, out value);
            }

            /// <summary>
            /// Determines whether the specified key exists.
            /// </summary>
            /// <param name="index">Key.</param>
            /// <returns>Returns true if the specified key exists otherwise returns false.</returns>
            public bool Contains(T3 index)
            {
                return _dictionary.ContainsKey(index);
            }

            /// <summary>
            /// Return the enumerator that iterates through the collection.
            /// </summary>
            /// <returns>Collection enumerator.</returns>
            public IEnumerator<T3> GetEnumerator()
            {
                return _dictionary.Keys.GetEnumerator();
            }

            /// <summary>
            /// Return the enumerator that iterates through the collection.
            /// </summary>
            /// <returns>Collection enumerator.</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        private readonly Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private readonly Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        /// <summary>
        /// Initializes a new instance of <see cref="Map{T1,T2}"/>.
        /// </summary>
        public Map()
        {
            Forward = new Indexer<T1, T2>(_forward);
            Reverse = new Indexer<T2, T1>(_reverse);
        }

        /// <summary>
        /// Adds the specified association.
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        /// <summary>
        /// Removes the associations specified by the key.
        /// </summary>
        /// <param name="t1">Forward key.</param>
        /// <returns>True if the assocaitons exists, false otherwise.</returns>
        public bool TryRemove(T1 t1)
        {
            if (!_forward.ContainsKey(t1))
                return false;

            var val = _forward[t1];
            _forward.Remove(t1);
            _reverse.Remove(val);
            return true;
        }

        /// <summary>
        /// Removes the associations specified by the key.
        /// </summary>
        /// <param name="t2">Reverse key.</param>
        /// <returns>True if the assocaitons exists, false otherwise.</returns>
        public bool TryRemove(T2 t2)
        {
            if (!_reverse.ContainsKey(t2))
                return false;

            var val = _reverse[t2];
            _reverse.Remove(t2);
            _forward.Remove(val);
            return true;
        }

        /// <summary>
        /// Gets all associations with the first <typeparamref name="T1"/> key.
        /// </summary>
        public Indexer<T1, T2> Forward { get; private set; }
        /// <summary>
        /// Gets all associations with the second <typeparamref name="T2"/> key.
        /// </summary>
        public Indexer<T2, T1> Reverse { get; private set; }

        /// <summary>
        /// Removes all associations from the <see cref="Accord.Extensions.Map{T1, T2}"/>.
        /// </summary>
        public void Clear()
        {
            _forward.Clear();
            _reverse.Clear();
        }

        /// <summary>
        /// Gets the enumerator of the collections.
        /// </summary>
        /// <returns>Enumerator.</returns>
        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return _forward.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator of the collections.
        /// </summary>
        /// <returns>Enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _forward.GetEnumerator();
        }
    }
}
