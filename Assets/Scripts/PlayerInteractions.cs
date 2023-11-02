using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] float speed = 8;
    [SerializeField] float rotSpeed = 200;
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal") * rotSpeed * Time.deltaTime;
        float v = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, v);
        transform.Rotate(0, h, 0);
    }
}
