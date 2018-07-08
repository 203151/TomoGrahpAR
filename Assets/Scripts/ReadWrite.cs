using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Text;

#if !UNITY_EDITOR && UNITY_WSA
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
#endif


public class ReadWrite : MonoBehaviour
{
    public const string AIM_FILE_NAME = "AimFile.aim";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static string ImageFolderName
    {
        get
        {
#if !UNITY_EDITOR
                return ApplicationData.Current.RoamingFolder.Path;
#else
            return Application.persistentDataPath;
#endif
        }
    }


    private static Stream OpenFileForRead(string folderName, string fileName)
    {
        Stream stream = null;

#if !UNITY_EDITOR
            Task task = new Task(
                            async () =>
                            {
                                StorageFolder folder = await StorageFolder.GetFolderFromPathAsync(folderName);
                                StorageFile file = await folder.GetFileAsync(fileName);
                                stream = await file.OpenStreamForReadAsync();
                            });
            task.Start();
            task.Wait();
#else
        stream = new FileStream(Path.Combine(folderName, fileName), FileMode.Open, FileAccess.Read);
#endif
        return stream;
    }

    public string ReadString()
    {
        string s = null;
#if !UNITY_EDITOR && UNITY_METRO
      try {
        using (Stream stream = OpenFileForRead(ApplicationData.Current.RoamingFolder.Path, AIM_FILE_NAME)) 
        {
          byte[] data = new byte[stream.Length];
          stream.Read(data, 0, data.Length);
          s = Encoding.ASCII.GetString(data);
        }
      }
      catch (Exception e) 
        {
        Debug.Log(e);
        }
#endif
        return s;
    }
}
