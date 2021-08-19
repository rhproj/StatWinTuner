using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace StatWinTuner
{
    public class Tuner
    {
        readonly string _exServer;
        readonly string _newServer;
        readonly string _winPath;
        readonly string _winPathTTR;
        readonly string _statDirDctp;
        readonly string _statDirDctp2;
        readonly string _sourcePath;
        string resultMsg = string.Empty;
        public bool success;

        public Tuner()
        {
            _exServer = ConfigurationManager.AppSettings["exServer"];
            _newServer = ConfigurationManager.AppSettings["newServer"];
            _winPath = ConfigurationManager.AppSettings["winPath"];
            _winPathTTR = ConfigurationManager.AppSettings["winPathTTR"];
            _statDirDctp = ConfigurationManager.AppSettings["statDirDctp"];    //statDirDctp
            _statDirDctp2 = ConfigurationManager.AppSettings["statDirDctp2"];
            _sourcePath = ConfigurationManager.AppSettings["sourcePath"];
        }

        public string ApplySettings()
        {
            if (File.Exists(_winPath) || File.Exists(_winPathTTR))
            {
                if (File.Exists(_winPath))
                {
                    CorrectWin(_winPath);
                    resultMsg += $@"Файл {_winPath} перезаписан";
                }
                if (File.Exists(_winPathTTR))
                {
                    CorrectWin(_winPathTTR);
                    resultMsg += $@"{Environment.NewLine}Файл {_winPathTTR} перезаписан";
                }
                ReplaceLnk();
                success = true;
            }
            else
            {
                resultMsg = $"Не удалось найти файлов для перезаписи! {Environment.NewLine}Убедитесь что установлена соответствующая версия программы АрмСтат";
                success = false;
            }
            return resultMsg;
        }

        private void CorrectWin(string statWinFile)
        {
            string statWinConfig = File.ReadAllText($@"{statWinFile}", Encoding.Default);   //@"C:\ARM_STAT\STAT_WIN.INI"

            statWinConfig = Regex.Replace($@"{statWinConfig}", _exServer, _newServer, RegexOptions.IgnoreCase);
            File.WriteAllText($@"{statWinFile}", statWinConfig, Encoding.Default);
        }

        private string ReplaceLnk()
        {
            if (Directory.Exists(_sourcePath))
            {
                string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $@"\{_statDirDctp}";
                string targetPath2 = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + $@"\{_statDirDctp2}";

                if (Directory.Exists(targetPath))
                {
                    DirCopy.CopyAll(new DirectoryInfo(_sourcePath), new DirectoryInfo(targetPath));
                    resultMsg += $"{Environment.NewLine}ссылки на ресурсы обновлены";
                }
                else if (Directory.Exists(targetPath2))
                {
                    DirCopy.CopyAll(new DirectoryInfo(_sourcePath), new DirectoryInfo(targetPath2));
                    resultMsg += $"{Environment.NewLine}ссылки на ресурсы обновлены";
                }
                else
                {
                    DirCopy.CopyAll(new DirectoryInfo(_sourcePath), new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop)));
                    resultMsg += $"{Environment.NewLine}ссылка на директорию Отчеты в Аппарат помещена на рабочий стол!";
                }
            }
            else
            {
                resultMsg += $"{Environment.NewLine}Отсутствует доступ к ресурсу {_sourcePath}";
            }
            return resultMsg;
        }
    }

}
