using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject pantallaPausa;
    private bool menuOn;
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
            Time.timeScale = 0f;
     //      audioSource.PlayOneShot(Abrir);
        }
        else
        {
            pantallaPausa.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
       //     audioSource.PlayOneShot(Cerrar);
        }
    }
}
