using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

//List of all equipped item sets for all transformations
public class EquippedSetManager : MonoBehaviour, IEquippedSetManager
{
    private string equippedInventoryFilePathName = "SaveData/EquippedInventory.dat";
    //private List<EquippedSet> equippedInventory;    //Equipped item sets for all transformations
    private EquippedSet[] equippedInventory;
    private SceneController sceneController;
    public int currentSet;      //0 - Base. 1 - Transformation
    public Item lightItem;
    public Item heavyItem;
    public Item utilityItem;

    void Awake()
    {
        //equippedInventory = new List<EquippedSet>();
        equippedInventory = new EquippedSet[2];     //Array of 2 equipped sets. One for base, one for transformation
    }

    // Use this for initialization
    void Start () {
        currentSet = 0; //Default to 0
        GameObject sceneControllerGameObject = GameObject.FindGameObjectWithTag("SceneController");
        sceneController = sceneControllerGameObject.GetComponent<SceneController>();
        //Subscribe to before and after scene unload/load
        sceneController.AfterSceneLoad += LoadEquippedInventory;    //After scene loads, load inventory
        sceneController.BeforeSceneUnload += SaveEquippedInventory; //Before leaving scene, save inventory

        //Start with one equipment set called Base
        AddEmptySet("Base");
    }

    void Update()
    {
        //Display items in inspector for debugging purposes
        lightItem = equippedInventory[currentSet].getLightItem();
        heavyItem = equippedInventory[currentSet].getHeavyItem();
        utilityItem = equippedInventory[currentSet].getUtilityItem();
    }
    
    //Adds blank equipment set with input name to array
    public void AddEmptySet(string setName)
    {
        EquippedSet newSet = new EquippedSet(setName);
        equippedInventory[0] = newSet;
    }
    
    //Equip light item into current equipment set
    public void EquipLightInCurrent(Item lightItem)
    {
        equippedInventory[currentSet].EquipLight(lightItem);
    }

    //Equip heavy item into current equipment set
    public void EquipHeavyInCurrent(Item heavyItem)
    {
        equippedInventory[currentSet].EquipHeavy(heavyItem);
    }

    //Equip utility item into current equipment set
    public void EquipUtilityInCurrent(Item utilityItem)
    {
        equippedInventory[currentSet].EquipUtility(utilityItem);
    }

    public void SaveEquippedInventory()
    {
        FileStream fs;
        fs = new FileStream(equippedInventoryFilePathName, FileMode.OpenOrCreate);
        //Save data into new file        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, equippedInventory);
        fs.Close();
        //Debug.Log("Saved inventory");
    }

    public void LoadEquippedInventory()
    {
        //See if file exists already, if it does load data into list
        if (File.Exists(equippedInventoryFilePathName))
        {
            //Clears current data
            //equippedInventory.Clear();
            //equippedInventory[0] = null;
            //equippedInventory[1] = null;
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(equippedInventoryFilePathName, FileMode.Open);
            //equippedInventory = (List<EquippedSet>)bf.Deserialize(file);
            equippedInventory = (EquippedSet[])bf.Deserialize(file);
            file.Close();
            //Debug.Log("Loaded inventory");
        }
    }

    public void SwapSet()
    {
        //If currently using base equipment, switch to transformation equipment
        if(currentSet == 0)
        {
            currentSet = 1;
        }
        //If currently using transformation equipment, switch to base equipment
        else
        {
            currentSet = 0;
        }
    }
}
