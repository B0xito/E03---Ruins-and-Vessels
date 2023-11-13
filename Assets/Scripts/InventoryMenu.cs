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
//Este scrpit por alguna razon no permite que en el menu funcionen sus interacciónes, por lo que supongo que con el inventario
//y to lo qu eestá haciendo el Marcelo, habra que inutilizar este srcip :3
