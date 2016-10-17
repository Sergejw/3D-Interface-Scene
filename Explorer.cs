using UnityEngine;
using System;
using System.Net;
using System.Text;
using System.IO;


public class Explorer {

    string dataPath = Application.persistentDataPath;
    int filesCount = 0;
    string[] sArr = new string[10];

    public Explorer() {

    }

    public void writeFile()
    {
        string lines = "First line.\r\nSecond line.\r\nThird line.\r\n " + Application.persistentDataPath;

        // Write the string to a file.
        StreamWriter file = new StreamWriter(dataPath + @"\DSFeest.txt");
        file.WriteLine(lines);

        file.Close();

    }

    public void SaveScene()
    {
        Scene scene = new Scene();
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(dataPath + @"\JSONeest_34_31.01.2016_01,37.txt", scene.SaveToString());
    }

    public void GetScene(string sceneName)
    {
        //string fileContent = File.ReadAllText(dataPath + @"\" + sceneName + ".txt");
        string fileContent = File.ReadAllText(dataPath + @"\JSONeest.txt");
        Scene scene = new Scene(fileContent);
        Debug.Log("SCENE TO STRING: " + scene.toString());

    }

    /// <summary>
    /// Liefert eine Zeichenkette, welche alle Szenennamen beinhaltet.
    /// </summary>
    public string GetAllSceneNames()
    {
        string[] files = Directory.GetFiles(dataPath, "*.txt");
        filesCount = files.Length;
        
        for (int i = 0; i < files.Length; ++i)
            files[i] = files[i].Replace(dataPath + @"\", "").Replace(".txt", "");

        return string.Join("#", files);
    }
}
