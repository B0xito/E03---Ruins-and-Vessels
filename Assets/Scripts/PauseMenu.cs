using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public GameObject pantallaPausa;
    public GameObject pantallaLost;
    private bool menuOn;
    private bool lostOn;
    //  AudioSource audioSource;

    //  [SerializeField] private AudioClip Abrir;
    //   [SerializeField] private AudioClip Cerrar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuOn = !menuOn;
        }
        if (menuOn == true)
        {
            pantallaPausa.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
     //      audioSource.PlayOneShot(Abrir);
        }
        else
        {
            pantallaPausa.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
       //     audioSource.PlayOneShot(Cerrar);
        }



        if (Input.GetKeyDown(KeyCode.Tab))
        {
                pantallaLost.SetActive(true);
                //Cursor.lockState = CursorLockMode.None;
                //Cursor.visible = true;
                //      audioSource.PlayOneShot(Abrir);
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            pantallaLost.SetActive(false);
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            //     audioSource.PlayOneShot(Cerrar);
        }


    }
    
}
