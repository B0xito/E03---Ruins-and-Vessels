using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Transform initialPosition;
    [SerializeField] Transform finalPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject.transform;
            if (!player)
            {
                player = finalPosition;
            }
        }
    }
}
