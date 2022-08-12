using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FSC_IniReader
{
    public partial class FSCIniStream : IDisposable
    {
        private List<string> _iniFileNoSections;
        private Dictionary<string, List<string>> _iniFileSections;
        private string _filename = null;
        private FileStream _file = null;

        /// <summary>
        /// Can be used to read the ini data from a file and keep the file locked or unlocked after reading
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="lockFile"></param>
        public FSCIniStream(string filename, bool lockFile = false)
        {
            _iniFileNoSections = new List<string>();
            _iniFileSections = new Dictionary<string, List<string>>();

            _filename = Path.GetFullPath(filename);

            List<string> iniTempFile = new List<string>();

            if (File.Exists(_filename))
            {
                iniTempFile = new List<string>(File.ReadLines(_filename));
            }

            string lastSection = String.Empty;

            foreach (string str in iniTempFile)
            {
                if (str.StartsWith("[") && str.EndsWith("]"))
                {
                    if (String.IsNullOrWhiteSpace(_GetSectionValue(str)))
                    {
                        throw new Exception("Can't read empty sections");
                    }
                    else
                    {
                        _iniFileSections.Add(_GetSectionValue(str), new List<string>());
                        lastSection = _GetSectionValue(str);

                        continue;
                    }
                }

                if (!String.IsNullOrWhiteSpace(str))
                {
                    if (lastSection == String.Empty)
                    {
                        _iniFileNoSections.Add(str);
                    }
                    else
                    {
                        _iniFileSections[lastSection].Add(str);
                    }
                }
            }

            if (lockFile)
            {
                _file = new FileStream(_filename, FileMode.OpenOrCreate);
            }
        }

        /// <summary>
        /// Can be used to read the ini data from a stream reader and keep the file locked or unlocked after reading
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="filename"></param>
        /// <param name="lockFile"></param>
        public FSCIniStream(StreamReader sr, string filename, bool lockFile = false)
        {
            _iniFileNoSections = new List<string>();
            _iniFileSections = new Dictionary<string, List<string>>();

            _filename = Path.GetFullPath(filename);

            List<string> iniTempFile = new List<string>(sr.ReadToEnd().Split(Environment.NewLine));

            sr.Close();
            sr.Dispose();

            string lastSection = String.Empty;

            foreach (string str in iniTempFile)
            {
                if (str.StartsWith("[") && str.EndsWith("]"))
                {
                    if (String.IsNullOrWhiteSpace(_GetSectionValue(str)))
                    {
                        throw new Exception("Can't read empty sections");
                    }
                    else
                    {
                        _iniFileSections.Add(_GetSectionValue(str), new List<string>());
                        lastSection = _GetSectionValue(str);

                        continue;
                    }
                }

                if (!String.IsNullOrWhiteSpace(str))
                {
                    if (lastSection == String.Empty)
                    {
                        _iniFileNoSections.Add(str);
                    }
                    else
                    {
                        _iniFileSections[lastSection].Add(str);
                    }
                }
            }

            if (lockFile)
            {
                _file = new FileStream(_filename, FileMode.OpenOrCreate);
            }
        }

        /// <summary>
        /// Can be used to read the ini data from a file stream and keep the file locked or unlocked after reading
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="filename"></param>
        /// <param name="lockFile"></param>
        public FSCIniStream(FileStream fs, string filename, bool lockFile = false) : this(new StreamReader(fs), filename, lockFile) { }

        /// <summary>
        /// Can be used to read the ini data from a string reader. This creates no file
        /// </summary>
        /// <param name="sr"></param>
        /// <param name="closeAfterReading"></param>
        public FSCIniStream(StringReader sr, bool closeAfterReading = true)
        {
            _iniFileNoSections = new List<string>();
            _iniFileSections = new Dictionary<string, List<string>>();

            List<string> iniTempFile = new List<string>(sr.ReadToEnd().Split(Environment.NewLine));

            if (closeAfterReading)
            {
                sr.Close();
                sr.Dispose();
            }

            string lastSection = String.Empty;

            foreach (string str in iniTempFile)
            {
                if (str.StartsWith("[") && str.EndsWith("]"))
                {
                    if (String.IsNullOrWhiteSpace(_GetSectionValue(str)))
                    {
                        throw new Exception("Can't read empty sections");
                    }
                    else
                    {
                        _iniFileSections.Add(_GetSectionValue(str), new List<string>());
                        lastSection = _GetSectionValue(str);

                        continue;
                    }
                }

                if (!String.IsNullOrWhiteSpace(str))
                {
                    if (lastSection == String.Empty)
                    {
                        _iniFileNoSections.Add(str);
                    }
                    else
                    {
                        _iniFileSections[lastSection].Add(str);
                    }
                }
            }
        }

        /// <summary>
        /// If true, there will be an empty line before each section for a more clean look
        /// </summary>
        public bool SetEmptyLineBeforeSection { get; set; } = true;

        /// <summary>
        /// It's still possible to work with the ini data, but makes it impossible to save it back to the file
        /// </summary>
        public bool Cancel { get; set; } = false;

        public MemoryStream GetStream()
        {
            StringBuilder sb = new StringBuilder();

            foreach (string str in _iniFileNoSections)
            {
                sb.AppendLine(str);
            }

            foreach (string key in _iniFileSections.Keys)
            {
                if (SetEmptyLineBeforeSection)
                {
                    sb.AppendLine(String.Empty);
                }

                sb.AppendLine($"[{ key}]");

                foreach (string str in _iniFileSections[key])
                {
                    sb.AppendLine(str);
                }
            }

            byte[] bytes = Encoding.UTF8.GetBytes(sb.ToString());

            MemoryStream stream = new MemoryStream(bytes);

            return stream;
        }

        ~FSCIniStream()
        {
            Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Close()
        {
            if (_file != null)
            {
                _file.Close();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_file != null)
            {
                _file.Close();
                _file.Dispose();
            }

            if (Cancel)
            {
                return;
            }

            if (_filename != null)
            {
                StringBuilder sb = new StringBuilder();

                foreach (string str in _iniFileNoSections)
                {
                    sb.AppendLine(str);
                }

                foreach (string key in _iniFileSections.Keys)
                {
                    if (SetEmptyLineBeforeSection)
                    {
                        sb.AppendLine(String.Empty);
                    }

                    sb.AppendLine($"[{ key}]");

                    foreach (string str in _iniFileSections[key])
                    {
                        sb.AppendLine(str);
                    }
                }

                File.WriteAllText(_filename, sb.ToString());
            }
        }

        // Just for internal debug reasons
        private void log(string text)
        {
            File.WriteAllText("log.log", text);
        }
    }
}
