using System;
using System.IO;
using UnityEngine;

public class NFileExplorer : MonoBehaviour
{
    private DirectoryInfo currentDirectory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

    private void Awake()
    {
        UpdateDirectory(currentDirectory);
    }

    private void UpdateDirectory(DirectoryInfo newDirectoryInfo)
    {
        currentDirectory = newDirectoryInfo;

        for (int i = 0; i < currentDirectory.GetDirectories().Length; i++)
        {

        }

        for (int i = 0; i < currentDirectory.GetFiles().Length; i++)
        {

        }
    }    
}
