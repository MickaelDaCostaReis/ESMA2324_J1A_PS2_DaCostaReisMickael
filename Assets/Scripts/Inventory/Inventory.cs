using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ouvrir l'inventaire
public class Inventory : MonoBehaviour
{
    public GameObject inventory;

    /* Input system
    public void OpenInventory()
    {
        if (inventory.activeInHierarchy) inventory.SetActive(false);
        else inventory.SetActive(true);
    }
    */

    
    public void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory.activeInHierarchy) inventory.SetActive(false);
            else inventory.SetActive(true);
        }
    }
    void Update()
    {
        OpenInventory();
        
    }
}