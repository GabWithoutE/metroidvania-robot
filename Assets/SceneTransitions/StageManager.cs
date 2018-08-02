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

        //Returns true if list contains a key, false if doesn't contain
        public bool Contains(string key)
        {
            int index = keys.FindIndex(x => x == key);
            if(index == -1)
            {
                return false;
            }
            return true;
        }
    }

    //List of enemy states for each enemy in scene
    private sceneObjectLists<EnemyState> sceneObjectsEnemy = new sceneObjectLists<EnemyState>();
    //List of item states for each item in scene
    private sceneObjectLists<ItemState> sceneObjectsItem = new sceneObjectLists<ItemState>();
    
    //private sceneObjectLists<bool> sceneObjectsBool = new sceneObjectLists<bool>();
    //private sceneObjectLists<float> sceneObjectsFloat = new sceneObjectLists<float>();
    //private sceneObjectLists<string> sceneObjectsString = new sceneObjectLists<string>();
    //private sceneObjectLists<Vector2> sceneObjectsPositions = new sceneObjectLists<Vector2>();

    public void Reset()
    {
        //sceneObjectsBool.Clear();
        //sceneObjectsFloat.Clear();
        //sceneObjectsString.Clear();
        //sceneObjectsPositions.Clear();
        sceneObjectsEnemy.Clear();
        sceneObjectsItem.Clear();
    }

    //Registers new enemy
    public void RegisterState(EnemyState enemyState)
    {
        sceneObjectsEnemy.TrySetValue(enemyState.getName(), enemyState);
    }

    //Registers new item
    public void RegisterState(ItemState itemState)
    {
        sceneObjectsItem.TrySetValue(itemState.getName(), itemState);
    }

    //Returns true if list of enemies contain input key
    public bool ContainsEnemy(string key)
    {
        if(sceneObjectsEnemy.Contains(key))
        {
            return true;
        }
        return false;
    }

    //Returns true if list of items contain input key
    public bool ContainsItem(string key)
    {
        if (sceneObjectsItem.Contains(key))
        {
            return true;
        }
        return false;
    }

    //Sets an element with input key in input list to input value
    private void Save<T>(sceneObjectLists<T> lists, string key, T value)
    {
        lists.TrySetValue(key, value);
    }

    //Loads the value of an element in input list with input key into input value. 
    //Returns true if key found, false if not found
    private bool Load<T>(sceneObjectLists<T> lists, string key, ref T value)
    {
        return lists.TryGetValue(key, ref value);
    }

    //Set enemy state in list
    public void SetState(string key, EnemyState state)
    {
        Save(sceneObjectsEnemy, key, state);
    }

    //Set item state in list
    public void SetState(string key, ItemState state)
    {
        Save(sceneObjectsItem, key, state);
    }

    //Get enemy state in list
    public EnemyState GetEnemyState(string key)
    {
        EnemyState tempEnemyState = new EnemyState();
        if(Load(sceneObjectsEnemy, key, ref tempEnemyState))
        {
            return tempEnemyState;
        }
        return tempEnemyState;
    }

    //Get item state in list
    public ItemState GetItemState(string key)
    {
        ItemState tempItemState = new ItemState();
        if (Load(sceneObjectsItem, key, ref tempItemState))
        {
            return tempItemState;
        }
        return tempItemState;
    }

    /*
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

    //Loads bool list element into value
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
    */
    //Saves entire scene data
    public void SaveAllToFile()
    {
        /*
        SaveBoolToFile();
        SaveFloatToFile();
        SaveStringToFile();
        SavePositionsToFile();
        */
        SaveEnemyToFile();
        SaveItemToFile();
    }

    public void SaveEnemyToFile()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "enemy.dat";
        //If file already exists, delete existing file
        if (File.Exists(pathName))
        {
            File.Delete(pathName);
        }
        //Save data into new file
        FileStream fs = new FileStream(pathName, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsEnemy);
        fs.Close();
    }

    public void SaveItemToFile()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "item.dat";
        //If file already exists, delete existing file
        if (File.Exists(pathName))
        {
            File.Delete(pathName);
        }
        //Save data into new file
        FileStream fs = new FileStream(pathName, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsItem);
        fs.Close();
    }
    /*
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
    */
    //Loads entire scene's data
    public void LoadAll()
    {
        /*
        LoadBool();
        LoadFloat();
        LoadString();
        LoadPositions();
        */
        LoadEnemy();
        LoadItem();
    }

    public void LoadEnemy()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "enemy.dat";
        //See if file exists already, if it does load data into list
        if (File.Exists(pathName))
        {
            //Clears current data
            sceneObjectsEnemy.Clear();
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathName, FileMode.Open);
            sceneObjectsEnemy = (sceneObjectLists<EnemyState>)bf.Deserialize(file);
            file.Close();
        }
        //If file does not exist, create file
        else
        {
            SaveEnemyToFile();
        }
    }

    public void LoadItem()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "item.dat";
        //See if file exists already, if it does load data into list
        if (File.Exists(pathName))
        {
            //Clears current data
            sceneObjectsItem.Clear();
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(pathName, FileMode.Open);
            sceneObjectsItem = (sceneObjectLists<ItemState>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            SaveItemToFile();
        }
    }
    /*
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
    */
}
