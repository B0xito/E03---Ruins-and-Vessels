using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesInventoryAndFabrication : MonoBehaviour
{
    Animator invAnim;
    [SerializeField] PlayerInteractions playerInteractions;

    bool isFabricationOpen;

    private void Start()
    {
        invAnim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (playerInteractions.addingPiece == true)
        {
            OpenPreview();
        }
        else
        {
            ClosePreview();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenCloseFabrication();
        }
        
    }

    void OpenPreview()
    {
        invAnim.SetFloat("status", 1);
    }

    void ClosePreview()
    {
        invAnim.SetFloat("status", 0);
    }
    void OpenCloseFabrication()
    {
        isFabricationOpen = !isFabricationOpen;
        if (isFabricationOpen )
        {
            invAnim.SetFloat("statusFab", 1);
        }
        else
        {
            invAnim.SetFloat("statusFab", 0);
        }
    }


}
