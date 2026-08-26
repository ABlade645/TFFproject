using UnityEngine;
using System;
using System.Security.Cryptography;
using System.IO;
using System.Linq;

public class FileCreator : MonoBehaviour
{
    public string defaultHash;
    public string dirHash;

    public void Check()
    {
        dirHash = GetDirectoryHash(Application.dataPath + "\\CoreExe\\");
        defaultHash = GetDirectoryHash(Application.streamingAssetsPath + "\\CoreExe\\");

        if (!Directory.Exists(Application.dataPath + "\\CoreExe\\") || dirHash != defaultHash)
        {
            CopyDirectory(Application.streamingAssetsPath + "\\CoreExe\\", Application.dataPath + "\\CoreExe\\");
            Debug.Log("CoreExe log: CoreExe rewritten successfully");
        }     
    }

    void CopyDirectory(string Source, string Destination)
    {
        Directory.CreateDirectory(Destination);

        foreach (string file in Directory.GetFiles(Source))
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(Destination, fileName);
            File.Copy(file, destFile, true);
        }

        foreach (string subDir in Directory.GetDirectories(Source))
        {
            string dirName = Path.GetFileName(subDir);
            string destDir = Path.Combine(Destination, dirName);
            CopyDirectory(subDir, destDir);
        }
    }

    public static string GetDirectoryHash(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
            return null;

        using (var md5 = MD5.Create())
        {
            var files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories).OrderBy(f => f).ToArray();

            foreach (var file in files)
            {
                string fileName = Path.GetFileName(file);
                if (fileName.StartsWith("CoreTxt", StringComparison.OrdinalIgnoreCase))
                    continue;

                byte[] bytes = File.ReadAllBytes(file);
                md5.TransformBlock(bytes, 0, bytes.Length, null, 0);
            }

            md5.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            return BitConverter.ToString(md5.Hash).Replace("-", "").ToLowerInvariant();
        }
    }
}
