using UnityEngine;
using System.Collections.Generic;
using System;


public class Scene {

    private string name;
    private int objects;
    private SceneInfo info;
    public List<string> list;

    public Scene(string jsonString)
    {
        info = JsonUtility.FromJson<SceneInfo>(jsonString);
        name = info.sceneName;
        objects = info.numberOfObjectsInScene;
    }

    public Scene()
    {
        name = "MEINE Szene";
        objects = 2;
        info = new SceneInfo();
        info.sceneName = name;
        info.numberOfObjectsInScene = objects;
        info.hh = new string[2];
        info.hh[0] = "111";
        info.hh[1] = "222";

        list = info.GetList();
        list.Add("LISTE");
        list.Add("LISTE");
        list.Add("LISTE");
        list.Add("LISTE");
    }

    public string toString()
    {
        return "name: " + name + " objekte: " + objects;
    }

    public List<String> GetList()
    {
        return list;
    }

    public string SaveToString()
    {
        return JsonUtility.ToJson(info);
    }

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    private class SceneInfo
    {
        public string sceneName;
        public int numberOfObjectsInScene;
        public string[] hh;
        public List<string> list = new List<string>();

        public List<String> GetList()
        {
            return list;
        }
    }
}
