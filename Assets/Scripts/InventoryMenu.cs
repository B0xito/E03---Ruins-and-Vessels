using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenu : MonoBehaviour
{
    public GameObject InventoryMenus;
    private bool InventoryOn;
  

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InventoryOn = !InventoryOn;
        }
        if (InventoryOn == true)
        {
            InventoryMenus.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            InventoryMenus.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}