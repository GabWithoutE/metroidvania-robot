using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveInventory : MonoBehaviour {
    string inventoryFilePathName = "SaveData/Inventory.dat";

    private void LoadEnemies()
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
            sceneObjectsEnemy = (List<EnemyState>)bf.Deserialize(file);
            file.Close();
            Debug.Log("Loaded enemies");
        }
        //If file does not exist, create file
        else
        {
            //SaveEnemyToFile();
        }
    }

    private void SaveEnemyToFile()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "enemy.dat";
        FileStream fs;
        fs = new FileStream(pathName, FileMode.OpenOrCreate);
        //Save data into new file        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsEnemy);
        fs.Close();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
