using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesInventoryAndFabrication : MonoBehaviour
{
    Animator invAnim;
    [SerializeField] PlayerInteractions playerInteractions;

    private void Start()
    {
        invAnim = GetComponent<Animator>();
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
    }

    void OpenPreview()
    {
        invAnim.SetFloat("status", 1);
    }

    void ClosePreview()
    {
        invAnim.SetFloat("status", 0);
    }

}
