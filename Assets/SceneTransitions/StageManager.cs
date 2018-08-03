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
    
    //If save file does not exist, save a new copy
    void Start()
    {
        //Close all open filestreams pertaining to this scene
        //CloseStreams();
        SaveAllToFile();
    }

    private void CloseStreams()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "enemy.dat";
        FileStream fs = File.Open(pathName, FileMode.Open);
        fs.Close();
        pathName = "SaveData/" + activeSceneName + "item.dat";
        fs = File.Open(pathName, FileMode.Open);
        fs.Close();
    }

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

    //Saves entire scene data
    public void SaveAllToFile()
    {
        SaveEnemyToFile();
        SaveItemToFile();
    }

    public void SaveEnemyToFile()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "enemy.dat";
        
        FileStream fs;
        //If file already exists, just open file
        if (File.Exists(pathName))
        {
            fs = new FileStream(pathName, FileMode.Open);
        }
        //Otherwise create file
        else
        {
            fs = new FileStream(pathName, FileMode.Create);
        }
        /*
        using (var file = File.Open(pathName, FileMode.OpenOrCreate))
        {
            //Save data into new file        
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, sceneObjectsEnemy);
        }
        */
        //Save data into new file        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsEnemy);
        fs.Close();
    }

    public void SaveItemToFile()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "item.dat";

        FileStream fs;
        //If file already exists, just open file
        if (File.Exists(pathName))
        {
            fs = new FileStream(pathName, FileMode.Open);
        }
        //Otherwise create file
        else
        {
            fs = new FileStream(pathName, FileMode.Create);
        }
        /*
        using (var file = File.Open(pathName, FileMode.OpenOrCreate))
        {
            //Save data into new file        
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, sceneObjectsEnemy);
        }
        */
        //Save data into new file        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsItem);
        fs.Close();
    }

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
}
