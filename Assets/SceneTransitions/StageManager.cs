using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StageManager : MonoBehaviour, IStageManager
{
    private string activeSceneName;
    //List of enemy states for each enemy in scene
    private List<EnemyState> sceneObjectsEnemy = new List<EnemyState>();
    private List<GameObject> sceneObjectsEnemyGameObjects = new List<GameObject>();
    //List of item states for each item in scene
    private List<ItemState> sceneObjectsItem = new List<ItemState>();
    private List<GameObject> sceneObjectsItemGameObjects = new List<GameObject>();
    //Dictionary of scenes. <Scene Name, Distance from scene>
    private Dictionary<string, int> sceneList = new Dictionary<string, int>();

    public void RegisterEnemy(ref GameObject go)
    {
        sceneObjectsEnemyGameObjects.Add(go);
    }

    public void RegisterItem(GameObject go)
    {
        sceneObjectsItemGameObjects.Add(go);
    }

    //Clears all lists
    public void Reset()
    {
        sceneObjectsEnemy.Clear();
        sceneObjectsItem.Clear();
        sceneObjectsEnemyGameObjects.Clear();
        sceneObjectsItemGameObjects.Clear();
    }

    public void LoadAll()
    {
        LoadEnemies();
        LoadItem();
    }

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

    public void SaveAll()
    {
        SaveEnemyToFile();
        SaveItemToFile();
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

    private void SaveItemToFile()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathName = "SaveData/" + activeSceneName + "item.dat";
        FileStream fs;
        fs = new FileStream(pathName, FileMode.OpenOrCreate);
        //Save data into new file        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, sceneObjectsItem);
        fs.Close();
    }

    //Sets matching element in enemy state list to input parameter enemy state
    public void UpdateEnemyState(EnemyState enemyStateIn)
    {
        foreach(EnemyState es in sceneObjectsEnemy)
        {
            if(es.getName() == enemyStateIn.getName())
            {
                es.setDead(enemyStateIn.isDead());
                es.setCurrentPosition(enemyStateIn.getCurrentPosition().getVect2());
            }
        }
    }

    public bool activeSceneSaveFileExists()
    {
        //Get name of active scene
        activeSceneName = SceneManager.GetActiveScene().name;
        string pathNameEnemy = "SaveData/" + activeSceneName + "enemy.dat";
        string pathNameItem = "SaveData/" + activeSceneName + "item.dat";
        return File.Exists(pathNameEnemy) && File.Exists(pathNameItem);
    }

    public EnemyState GetEnemyState(string name)
    {
        foreach(EnemyState es in sceneObjectsEnemy)
        {
            if(es.getName() == name)
            {
                //Debug.Log("Match found");
                return es;
            }
        }
        //Debug.Log("EnemyState not found");
        EnemyState error = new EnemyState();
        return error;
    }

    public void BeforeLeavingScene()
    {
        sceneObjectsEnemy.Clear();
        //Loop through all the gameobjects in the list of enemy gameobjects, if gameobject is still alive, move
        //enemy back to original position. If enemy is dead, do not move.
        foreach(GameObject go in sceneObjectsEnemyGameObjects)
        {
            ICharacterStateObserver stateObserver = go.GetComponent<ICharacterStateObserver>();
            if (!(bool)stateObserver.GetCharacterStateValue(ConstantStrings.DEATH_STATE))
            {
                go.transform.position = (Vector2)stateObserver.GetCharacterStateValue("OriginalPosition");
            }
            //Convert gameobject into an enemy state and add to list of enemy states
            ConvertGameObjectToEnemy(go);
        }
        //Loop through all the gameobjects in the list of item gameobjects, if gameobject is picked up, set pickedUp

        //Save all changes
        SaveAll();
    }

    //After transitioning into a new scene, settle anything that needs to be done
    public void AfterArrivingScene()
    {
        //Update scene list
        IncrementSceneList();
        //Erase files from scenes that have a value of >= 2.
        foreach(KeyValuePair<string, int> scene in sceneList)
        {
            if(scene.Value >= 2)
            {
                //Get name of active scene
                activeSceneName = SceneManager.GetActiveScene().name;
                string pathNameEnemy = "SaveData/" + activeSceneName + "enemy.dat";
                //Check if there is a save file for enemies, if so, delete the file
                if (File.Exists(pathNameEnemy))
                {
                    File.Delete(pathNameEnemy);
                }
            }
        }
    }

    private void LoadItem()
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
            sceneObjectsItem = (List<ItemState>)bf.Deserialize(file);
            file.Close();
        }
        else
        {
            //SaveItemToFile();
        }
    }

    //Convert input game object into an enemy state and add enemy state into list
    private void ConvertGameObjectToEnemy(GameObject go)
    {
        ICharacterStateObserver stateObserver = go.GetComponent<ICharacterStateObserver>();
        EnemyState tempState = new EnemyState(go.name, (bool)stateObserver.GetCharacterStateValue(ConstantStrings.DEATH_STATE), go.transform.position);
        sceneObjectsEnemy.Add(tempState);        
    }

    //Adds scene name and default value of 0 to scene list
    public void AddScene(Scene scene)
    {
        sceneList.Add(scene.name, 0);
    }

    //Increments value of every scene in dictionary except active scene
    private void IncrementSceneList()
    {
        foreach(KeyValuePair<string, int> scene in sceneList)
        {
            //Reset active scene value to 0
            if(scene.Key == SceneManager.GetActiveScene().name)
            {
                sceneList[scene.Key] = 0;
            }
            //Everything else increment
            else
            {
                ++sceneList[scene.Key];
            }            
        }
    }
}