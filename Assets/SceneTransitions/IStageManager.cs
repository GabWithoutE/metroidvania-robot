using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public interface IStageManager{
    void RegisterEnemy(ref GameObject go);      //Adds reference to enemy gameobject to enemy list
    void RegisterItem(GameObject go);       //Adds reference to item gameobject to item list
    void UpdateEnemyState(EnemyState enemyStateIn);            //Change existing element in enemy state list
    bool activeSceneSaveFileExists();       //Returns true if save file for active scene exists
    void Reset();       //Clears lists when going to new scene
    void LoadAll();     //Loads data from save file
    //void LoadEnemies(); //Loads enemies from save file
    void SaveAll();     //Save scene data to save file
    EnemyState GetEnemyState(string name);
    void BeforeLeavingScene();      //Updates save file before leaving scene
    void AfterArrivingScene();      //Increments scene list value and deletes the enemy save file of any scene that is far away
    void AddScene(Scene scene);        //Adds scene to dictionary
}
