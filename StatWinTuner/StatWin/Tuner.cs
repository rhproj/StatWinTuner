using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StatWinTuner
{
    public class Tuner
    {
        readonly string _exServer;
        readonly string _newServer;
        readonly string _sitePath;
        readonly string _winPath;
        readonly string _winPathTTR;
        public bool success;

        public Tuner()
        {
            _exServer = ConfigurationManager.AppSettings["exServer"];
            _newServer = ConfigurationManager.AppSettings["newServer"];
            _sitePath = ConfigurationManager.AppSettings["sitePath"];
            _winPath = ConfigurationManager.AppSettings["winPath"];
            _winPathTTR = ConfigurationManager.AppSettings["winPathTTR"];
        }

        //public List<string> DisplayAppCfg()
        //{
        //    List<string> list = new List<string>
        //    {
        //        _exServer, _newServer, _sitePath, _winPath, _winPathTTR
        //    };

        //    return list;
        //}

        public string ApplySettings()
        {
            string resultMsg = string.Empty;
            if (File.Exists(_winPath))
            {
                CorrectWin(_winPath);
                resultMsg += $@"Файл {_winPath} перезаписан{Environment.NewLine}";
                success = true;
            }
            if (File.Exists(_winPathTTR))
            {
                CorrectWin(_winPathTTR);
                resultMsg += $@"Файл {_winPathTTR} перезаписан{Environment.NewLine}";
                success = true;
            }
            else
            {
                resultMsg = $"Не удалось найти файлов для перезаписи {Environment.NewLine}Убедитесь что установлена соответствующая версия программы АрмСтат";
                success = false;
            }

            return resultMsg;
        }

        private void CorrectWin(string statWinFile)
        {
            string statWinConfig = File.ReadAllText($@"{statWinFile}", Encoding.Default);   //@"C:\ARM_STAT\STAT_WIN.INI"

            //int space = comboBox1.Text.IndexOf(' ');
            //string district = $"{comboBox1.Text.Substring(0, space)}-{comboBox1.Text.Substring(space + 1)}";

            statWinConfig = Regex.Replace($@"{statWinConfig}", _exServer, _newServer);
            File.WriteAllText($@"{statWinFile}", statWinConfig, Encoding.Default);
        }



        //public override string ToString()
        //{
        //    return $"{_exServer}\n{_newServer}\n{_sitePath}";
        //}
    }
}
