﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour, IInventory
{
    private string inventoryFilePathName = "SaveData/Inventory.dat";
    private List<Item> inventory;
    private SceneController sceneController;

    void Awake()
    {
        inventory = new List<Item>();
    }

    void Start()
    {
        GameObject sceneControllerGameObject = GameObject.FindGameObjectWithTag("SceneController");
        sceneController = sceneControllerGameObject.GetComponent<SceneController>();
        //Subscribe to before and after scene unload/load
        sceneController.AfterSceneLoad += LoadInventory;    //After scene loads, load inventory
        sceneController.BeforeSceneUnload += SaveInventory; //Before leaving scene, save inventory

        //If there is an inventory save file, load from that
        if(File.Exists(inventoryFilePathName))
        {
            LoadInventory();
        }
    }

    //If there are already instances of the item that is to be added, add amount to quantity instead
    public void AddItem(Item item)
    {
        int itemIndex = FindItemIndexByName(item.getName());
        //If item exists, add to quantity
        if (itemIndex != -1)
        {
            inventory[itemIndex].AddQuantity(item.getQuantity());
        }
        else
        {
            inventory.Add(item);
        }
    }

    //Remove from quantity of item until quantity equals 0, then remove the item type from inventory
    public void RemoveItem(Item item)
    {
        int itemIndex = FindItemIndexByName(item.getName());
        //If there is one quantity left, remove item completely from inventory
        if (inventory[itemIndex].getQuantity() == 1)
        {
            inventory.Remove(item);
        }
        //Otherwise just remove one
        else
        {
            inventory[itemIndex].RemoveQuantity(1);
        }
    }

    //Returns index of GameObject item with input name in inventory. If not found, return -1
    public int FindItemIndexByName(string itemName)
    {
        for (int i = 0; i < inventory.Count; ++i)
        {
            if (inventory[i].name == itemName)
            {
                return i;
            }
        }
        return -1;
    }

    //Returns quantity of GameObject item with input name in inventory. If not found, return 0
    public int GetItemQuantity(string itemName)
    {
        int itemIndex = FindItemIndexByName(itemName);
        if (itemIndex != -1)
        {
            return inventory[itemIndex].getQuantity();
        }
        return 0;
    }

    //Saves inventory as .dat file
    public void SaveInventory()
    {
        FileStream fs;
        fs = new FileStream(inventoryFilePathName, FileMode.OpenOrCreate);
        //Save data into new file        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, inventory);
        fs.Close();
        //Debug.Log("Saved inventory");
    }

    //Load save file into inventory
    public void LoadInventory()
    {
        //See if file exists already, if it does load data into list
        if (File.Exists(inventoryFilePathName))
        {
            //Clears current data
            inventory.Clear();
            //Loads data into list
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(inventoryFilePathName, FileMode.Open);
            inventory = (List<Item>)bf.Deserialize(file);
            file.Close();
            //Debug.Log("Loaded inventory");
        }
    }
}