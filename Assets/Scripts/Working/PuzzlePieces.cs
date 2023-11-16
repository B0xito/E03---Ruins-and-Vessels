using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum PuzzlePieceColor
{
    none,
    incolored,
    red,
    blue,
    green,
    purple,
    silver,
    golden
}

public class PuzzlePieces : MonoBehaviour, IPointerClickHandler
{
    bool isPickedUp;
    public Image pickedPiece;
    Vector3 initialPosition;
    public PlayerInteractions playerInteractions;
    public PuzzlePieceColor thisPiece;

    public Transform[] slots = new Transform[18];
    Image slotImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPickedUp)
        {
            isPickedUp = false;
        }

        switch (thisPiece)
        {
            case PuzzlePieceColor.incolored:
                if (playerInteractions.incolorCount > 0) { isPickedUp = true; }             
                break;
            case PuzzlePieceColor.red:
                if (playerInteractions.redCount > 0) { isPickedUp = true; }
                break;
            case PuzzlePieceColor.blue: 
                if (playerInteractions.blueCount > 0) { isPickedUp = true; }
                break;
            case PuzzlePieceColor.green:
                if (playerInteractions.greenCount > 0) { isPickedUp = true; }
                break;
            case PuzzlePieceColor.purple:
                if (playerInteractions.purpleCount > 0) { isPickedUp = true; }
                break;
            case PuzzlePieceColor.silver:
                if (playerInteractions.silverCount > 0) { isPickedUp = true; }
                break;
            case PuzzlePieceColor.golden:
                if (playerInteractions.goldenCount > 0) { isPickedUp = true; }
                break;
            default:
                break;
        }

        #region Pieces
        if (gameObject.CompareTag("IncolorPiece"))
        {
            Debug.Log("Click detected on " + "IncolorPiece");
        }
        if (gameObject.CompareTag("RedPiece"))
        {
            Debug.Log("Click detected on " + "RedPiece");
        }
        if (gameObject.CompareTag("BluePiece"))
        {
            Debug.Log("Click detected on " + "BluePiece");
        }
        if (gameObject.CompareTag("GreenPiece"))
        {
            Debug.Log("Click detected on " + "GreenPiece");
        }
        if (gameObject.CompareTag("PurplePiece"))
        {
            Debug.Log("Click detected on " + "PurplePiece");
        }
        if (gameObject.CompareTag("SilverPiece"))
        {
            Debug.Log("Click detected on " + "SilverPiece");
        }
        if (gameObject.CompareTag("GoldenPiece"))
        {
            Debug.Log("Click detected on " + "GoldenPiece");
        }
        #endregion

        if (isPickedUp)
        {
            pickedPiece = GetComponent<Image>();
            initialPosition = transform.position;
        }
        else
        {
            foreach (Transform slot in slots) 
            {
                float distanceToSlot = Vector2.Distance(transform.position, slot.position);
                if (distanceToSlot < 15)
                {
                    if (slot.GetComponent<PuzzlePieces>().thisPiece != PuzzlePieceColor.none)
                    {
                        switch (slot.GetComponent<PuzzlePieces>().thisPiece)
                        {
                            case PuzzlePieceColor.incolored:
                                playerInteractions.incolorCount++;
                                break;
                            case PuzzlePieceColor.red:
                                playerInteractions.redCount++;
                                break;
                            case PuzzlePieceColor.blue:
                                playerInteractions.blueCount++;
                                break;
                            case PuzzlePieceColor.green:
                                playerInteractions.greenCount++;
                                break;
                            case PuzzlePieceColor.purple:
                                playerInteractions.purpleCount++;
                                break;
                            case PuzzlePieceColor.silver:
                                playerInteractions.silverCount++;
                                break;
                            case PuzzlePieceColor.golden:
                                playerInteractions.goldenCount++;
                                break;
                            default:
                                break;
                        }
                    }

                    slot.GetComponent<Image>().sprite = pickedPiece.sprite;
                    slot.GetComponent<Image>().color = pickedPiece.color;
                    slot.GetComponent<PuzzlePieces>().thisPiece = pickedPiece.GetComponent<PuzzlePieces>().thisPiece; ;
                }
            }

            transform.position = initialPosition;
            pickedPiece = null;
        }     
    }

    

    private void FixedUpdate()
    {
        if (isPickedUp)
        {            
            FollowMouse();           
        }
    }

    void FollowMouse()
    {
        transform.position = Input.mousePosition;
    } 

    //void AssingPiece() 
    //{
    //    #region Pieces
    //    if (gameObject.CompareTag("IncolorPiece"))
    //    {
    //        playerInteractions.incolorCount--;

    //    }
    //    if (gameObject.CompareTag("RedPiece"))
    //    {
    //        playerInteractions.redCount--;
    //    }
    //    if (gameObject.CompareTag("BluePiece"))
    //    {
    //        playerInteractions.blueCount--;
    //    }
    //    if (gameObject.CompareTag("GreenPiece"))
    //    {
    //        playerInteractions.greenCount--;
    //    }
    //    if (gameObject.CompareTag("PurplePiece"))
    //    {
    //        playerInteractions.purpleCount--;
    //    }
    //    if (gameObject.CompareTag("SilverPiece"))
    //    {
    //        playerInteractions.silverCount--;
    //    }
    //    if (gameObject.CompareTag("GoldenPiece"))
    //    {
    //        playerInteractions.goldenCount--;
    //    }
    //    #endregion
    //}
}
