using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FSC_IniReader.Future
{
    public sealed partial class FSCIniSection : IComparable<FSCIniSection>
    {
        internal FSCIniSection() { }

        /// <summary>
        /// Gets or sets the value of a key
        /// </summary>
        /// <param name="key">The name of the key</param>
        /// <returns>Returns a FSCIniTypes object which includes a lot of types like string, int, double and more</returns>
        public FSCIniTypes this[string key]
        {
            get
            {
                return _keyValues?.Find(prop => prop.Key == key)?.Value ?? string.Empty;
            }
            set
            {
                var index = _keyValues?.FindIndex(prop => prop.Key == key) ?? -1;

                if (_keyValues?[index] is not null)
                {
                    _keyValues[index].Value = value;
                }
            }
        }

        internal string Name { get; set; } = string.Empty;

        /// <summary>
        /// Adds a new key value pair to a section
        /// </summary>
        /// <param name="key">The name of the key</param>
        /// <param name="value">The content of the value</param>
        /// <returns>Returns true, if the data was added successfully</returns>
        public bool Add(string key, FSCIniTypes value)
        {
            key = key.Trim();

            if (HasKey(key))
            {
                return false;
            }

            if (!VerifyKey(key))
            {
                return false;
            }

            var newkeyValue = new FSCIniKeyValue();
            newkeyValue.Key = key;
            newkeyValue.Value = value;

            _keyValues?.Add(newkeyValue);

            return true;
        }

        /// <summary>
        /// Deletes a key value pair
        /// </summary>
        /// <param name="key">The name of a key</param>
        public void Delete(string key)
        {
            if (!HasKey(key) && _keyValues is null)
            {
                return;
            }

            var keyValue = _keyValues.Find(prop => prop?.Key == key);

            if (keyValue is null)
            {
                return;
            }

            _keyValues.Remove(keyValue);
        }

        /// <summary>
        /// Checks if a key exists
        /// </summary>
        /// <param name="key">The name of a key</param>
        /// <returns>Returns true, if the key was found</returns>
        public bool HasKey(string key)
        {
            return _keyValues?.Find(prop => prop.Key == key) is not null;
        }

        /// <summary>
        /// Changes the name of a key to a new name
        /// </summary>
        /// <param name="oldKey">Old name</param>
        /// <param name="newKey">New name</param>
        public void Rename(string oldKey, string newKey)
        {
            if (!HasKey(oldKey))
            {
                return;
            }

            var index = _keyValues?.FindIndex(prop => prop?.Key == oldKey) ?? -1;

            if (index == -1)
            {
                return;
            }

            if (VerifyKey(newKey))
            {
                return;
            }

            if (_keyValues?[index] is not null)
            {
                _keyValues[index].Key = newKey;
            }
        }

        /// <summary>
        /// Gets all keys
        /// </summary>
        /// <returns>Returns all names of the keys</returns>
        public List<string> GetAllKeys()
        {
            if (IsEmpty())
            {
                return new List<string>();
            }

            return new List<string>(from keyValue in _keyValues select keyValue.Key);
        }

        /// <summary>
        /// This removes all key value pairs from the section
        /// </summary>
        public void Clear()
        {
            _keyValues?.Clear();
        }

        /// <summary>
        /// Checks if the section contains any key value pair
        /// </summary>
        /// <returns>Returns true, if empty</returns>
        public bool IsEmpty()
        {
            return !_keyValues.Any();
        }

        /// <summary>
        /// Sorts the key values in ASC or DESC
        /// </summary>
        /// <param name="sortDirection">The direction of sorting the key values</param>
        internal void Sort(ListSortDirection sortDirection)
        {
            _keyValues.Sort();

            if (sortDirection == ListSortDirection.Descending)
            {
                _keyValues.Reverse();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(FSCIniSection? other)
        {
            return Name.CompareTo(other?.Name);
        }
    }
}
