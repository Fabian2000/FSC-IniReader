using System;
using System.Collections.Generic;
using System.Linq;

namespace FSC_IniReader.Future
{
    public sealed class FSCIniTypes
    {
        internal FSCIniTypes(List<string> values)
        {
            GetString = values.First();
            GetStringList = values;

            GetBool = ConvertBool(GetString);
            
            foreach (var parseElement in GetStringList)
            {
                GetBoolList.Add(ConvertBool(parseElement));
            }

            if (byte.TryParse(GetString, out var _))
            {
                GetByte = byte.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (byte.TryParse(parseElement, out var _))
                {
                    GetByteList.Add(byte.Parse(parseElement));
                }
            }

            if (short.TryParse(GetString, out var _))
            {
                GetShort = short.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (short.TryParse(parseElement, out var _))
                {
                    GetShortList.Add(short.Parse(parseElement));
                }
            }

            if (int.TryParse(GetString, out var _))
            {
                GetInt = int.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (int.TryParse(parseElement, out var _))
                {
                    GetIntList.Add(int.Parse(parseElement));
                }
            }

            if (long.TryParse(GetString, out var _))
            {
                GetLong = long.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (long.TryParse(parseElement, out var _))
                {
                    GetLongList.Add(long.Parse(parseElement));
                }
            }

            if (ushort.TryParse(GetString, out var _))
            {
                GetUShort = ushort.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (ushort.TryParse(parseElement, out var _))
                {
                    GetUShortList.Add(ushort.Parse(parseElement));
                }
            }

            if (uint.TryParse(GetString, out var _))
            {
                GetUInt = uint.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (uint.TryParse(parseElement, out var _))
                {
                    GetUIntList.Add(uint.Parse(parseElement));
                }
            }

            if (ulong.TryParse(GetString, out var _))
            {
                GetULong = ulong.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (ulong.TryParse(parseElement, out var _))
                {
                    GetULongList.Add(ulong.Parse(parseElement));
                }
            }

            if (double.TryParse(GetString, out var _))
            {
                GetDouble = double.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (double.TryParse(parseElement, out var _))
                {
                    GetDoubleList.Add(double.Parse(parseElement));
                }
            }

            if (float.TryParse(GetString, out var _))
            {
                GetFloat = float.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (float.TryParse(parseElement, out var _))
                {
                    GetFloatList.Add(float.Parse(parseElement));
                }
            }

            if (decimal.TryParse(GetString, out var _))
            {
                GetDecimal = decimal.Parse(GetString);
            }
            foreach (var parseElement in GetStringList)
            {
                if (decimal.TryParse(parseElement, out var _))
                {
                    GetDecimalList.Add(decimal.Parse(parseElement));
                }
            }
        }

        //

        public string GetString { get; internal set; } = string.Empty;
        public List<string> GetStringList { get; internal set; } = new();

        public bool GetBool { get; internal set; } = false;
        public List<bool> GetBoolList { get; internal set; } = new();

        public byte GetByte { get; internal set; } = 0;
        public List<byte> GetByteList { get; internal set; } = new();

        public short GetShort { get; internal set; } = 0;
        public List<short> GetShortList { get; internal set; } = new();

        public int GetInt { get; internal set; } = 0;
        public List<int> GetIntList { get; internal set; } = new();

        public long GetLong { get; internal set; } = 0;
        public List<long> GetLongList { get; internal set; } = new();

        public ushort GetUShort { get; internal set; } = 0;
        public List<ushort> GetUShortList { get; internal set; } = new();

        public uint GetUInt { get; internal set; } = 0;
        public List<uint> GetUIntList { get; internal set; } = new();

        public ulong GetULong { get; internal set; } = 0;
        public List<ulong> GetULongList { get; internal set; } = new();

        public double GetDouble { get; internal set; } = 0;
        public List<double> GetDoubleList { get; internal set; } = new();

        public float GetFloat { get; internal set; } = 0;
        public List<float> GetFloatList { get; internal set; } = new();

        public decimal GetDecimal { get; internal set; } = 0;
        public List<decimal> GetDecimalList { get; internal set; } = new();

        //

        private static List<string> ToStringList<T>(List<T> convert)
        {
            var list = new List<string>();

            foreach (var element in convert)
            {
                list.Add(element?.ToString() ?? string.Empty);
            }

            return list;
        }

        //

        public static implicit operator FSCIniTypes(string value)
        {
            return new(new List<string>() { value });
        }

        public static implicit operator FSCIniTypes(List<string> value)
        {
            return new(value);
        }

        public static implicit operator FSCIniTypes(bool value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<bool> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(byte value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<byte> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(short value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<short> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(int value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<int> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(long value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<long> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(ushort value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<ushort> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(uint value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<uint> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(ulong value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<ulong> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(double value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<double> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(float value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<float> value)
        {
            return new(ToStringList(value));
        }

        public static implicit operator FSCIniTypes(decimal value)
        {
            return new(new List<string>() { value.ToString() });
        }

        public static implicit operator FSCIniTypes(List<decimal> value)
        {
            return new(ToStringList(value));
        }

        private bool ConvertBool(string parseElement)
        {
            if (parseElement.Equals("true", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            
            if (parseElement.Equals("1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return GetString;
        }
    }
}
