using System.IO;

namespace StatWinTuner
{
    static class DirCopy
    {
        public static void CopyAll(DirectoryInfo fromD, DirectoryInfo toD)
        {
            Directory.CreateDirectory(toD.FullName);

            //copy files
            foreach (FileInfo fI in fromD.GetFiles())
            {
                int qt = fromD.GetFiles("*", SearchOption.AllDirectories).Length;
                fI.CopyTo(Path.Combine(toD.FullName, fI.Name), true);
            }
            //copy sub-dirs recursively
            foreach (DirectoryInfo sourceDirs in fromD.GetDirectories())
            {
                DirectoryInfo targDirs = toD.CreateSubdirectory(sourceDirs.Name);
                CopyAll(sourceDirs, targDirs);
            }
        }
    }
}
