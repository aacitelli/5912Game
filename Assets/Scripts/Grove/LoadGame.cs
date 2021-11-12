using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

public class LoadGame : MonoBehaviour
{
    private string[] filenames = new string[10];

    public Save GetSaveForGame(int i)
    {
        string[] XMLfiles = Directory.GetFiles(Application.persistentDataPath + "/XMLSaves/", "*.XML");
        Save save = null;
        // if no profile loaded, use the most recently created.
        if (i == -1 || XMLfiles.Length == 0)
        {
            i = 0;
            DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath + "/XMLSaves/");
            FileSystemInfo[] files = di.GetFileSystemInfos();
            var orderedFiles = files.OrderByDescending(f => f.CreationTime);
            int length = XMLfiles.Length;
            if(length > 0)
            {
                string[] temp = new string[XMLfiles.Length];
                int j = 0;
                foreach (FileSystemInfo info in orderedFiles)
                {
                    temp[j] = info.FullName;
                    j++;
                }
                XMLfiles = temp;
            } else
            {
                //first time loading, make save file & display tutorial(?)
                // enter this condition if no load is selected & no loads exist.
                return save;
            }
            
        }
        save = new Save();
        
        string file = filenames[i];
        string path = XMLfiles[i];
        if (File.Exists(path))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Save));
            FileStream stream = new FileStream(path, FileMode.Open);
            save = serializer.Deserialize(stream) as Save;
            stream.Close();

        }
        else
        {
            Debug.Log("XML SAVE FILE NOT FOUND");
            return save;
        }

        return save;
    }
}
