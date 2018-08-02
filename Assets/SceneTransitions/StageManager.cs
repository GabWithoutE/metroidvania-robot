using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour {
    public static StageManager stage;
    private string activeSceneName;

    [System.Serializable]
    public class sceneObjectLists<T>
    {
        public List<string> keys = new List<string>();
        public List<T> values = new List<T>();

        public void Clear()
        {
            keys.Clear();
            values.Clear();
        }

        public bool TryGetValue(string key, ref T value)
        {
            int index = keys.FindIndex(x => x == key);
            if (index > -1)
            {
                value = values[index];
                return true;
            }
            return false;
        }

        public void TrySetValue(string key, T value)
        {
            int index = keys.FindIndex(x => x == key);
            if(index > -1)
            {
                values[index] = value;
            }
            else
            {
                keys.Add(key);
                values.Add(value);
            }
        }
    }
    
    private sceneObjectLists<bool> sceneObjectsBool = new sceneObjectLists<bool>();
    private sceneObjectLists<float> sceneObjectsFloat = new sceneObjectLists<float>();
    private sceneObjectLists<string> sceneObjectsString = new sceneObjectLists<string>();
    private sceneObjectLists<Vector2> sceneObjectsPositions = new sceneObjectLists<Vector2>();  //Positions of objects in scene

    public void Reset()
    {
        sceneObjectsBool.Clear();
        sceneObjectsFloat.Clear();
        sceneObjectsString.Clear();
        sceneObjectsPositions.Clear();
    }

    private void Save<T>(sceneObjectLists<T> lists, string key, T value)
    {
        lists.TrySetValue(key, value);
    }
    private bool Load<T>(sceneObjectLists<T> lists, string key, ref T value)
    {
        return lists.TryGetValue(key, ref value);
    }

    public void Save(string key, bool value)
    {
        Save(sceneObjectsBool, key, value);
    }

    public void Save(string key, string value)
    {
        Save(sceneObjectsString, key, value);
    }

    public void Save(string key, float value)
    {
        Save(sceneObjectsFloat, key, value);
    }

    public void Save(string key, Vector2 value)
    {
        Save(sceneObjectsPositions, key, value);
    }

    public bool Load(string key, ref bool value)
    {
        return Load(sceneObjectsBool, key, ref value);
    }

    public bool Load(string key, ref float value)
    {
        return Load(sceneObjectsFloat, key, ref value);
    }

    public bool Load(string key, ref string value)
    {
        return Load(sceneObjectsString, key, ref value);
    }

    public bool Load(string key, ref Vector2 value)
    {
        return Load(sceneObjectsPositions, key, ref value);
    }

    public void SaveBoolToFile()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "bool.dat";
        //If file already exists, delete existing file
        if (File.Exists(pathName))
        {
            File.Delete(pathName);
        }
        //Save data into new file
        FileStream fs = new FileStream(pathName, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsBool);
        fs.Close();
    }

    public void SaveFloatToFile()
    {
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "float.dat";
        //If file already exists, delete existing file
        if (File.Exists(pathName))
        {
            File.Delete(pathName);
        }
        //Save data into new file
        FileStream fs = new FileStream(pathName, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsFloat);
        fs.Close();
    }

    public void SaveStringToFile()
    {
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "string.dat";
        //If file already exists, delete existing file
        if (File.Exists(pathName))
        {
            File.Delete(pathName);
        }
        //Save data into new file
        FileStream fs = new FileStream(pathName, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsString);
        fs.Close();
    }

    public void SavePositionsToFile()
    {
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "positions.dat";
        //If file already exists, delete existing file
        if (File.Exists(pathName))
        {
            File.Delete(pathName);
        }
        //Save data into new file
        FileStream fs = new FileStream(pathName, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsPositions);
        fs.Close();
    }

    //Loads entire scene's data
    public void LoadAll()
    {
        LoadBool();
        LoadFloat();
        LoadString();
        LoadPositions();
    }

    public void LoadBool()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "bool.dat";
        //See if file exists already, if it does load data into list
        if (File.Exists(pathName))
        {
            //Clears current data
            sceneObjectsBool.Clear();
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathName, FileMode.Open);
            sceneObjectsBool = (sceneObjectLists<bool>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    public void LoadFloat()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "float.dat";
        //See if file exists already, if it does load data into list
        if (File.Exists(pathName))
        {
            //Clears current data
            sceneObjectsFloat.Clear();
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathName, FileMode.Open);
            sceneObjectsFloat = (sceneObjectLists<float>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    public void LoadString()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "string.dat";
        //See if file exists already, if it does load data into list
        if (File.Exists(pathName))
        {
            //Clears current data
            sceneObjectsString.Clear();
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathName, FileMode.Open);
            sceneObjectsString = (sceneObjectLists<string>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    public void LoadPositions()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "positions.dat";
        //See if file exists already, if it does load data into list
        if (File.Exists(pathName))
        {
            //Clears current data
            sceneObjectsPositions.Clear();
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathName, FileMode.Open);
            sceneObjectsPositions = (sceneObjectLists<Vector2>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            Debug.Log("File not found");
        }
    }
}
