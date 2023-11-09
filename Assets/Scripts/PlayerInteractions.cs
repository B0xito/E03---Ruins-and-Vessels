using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Movement Variables
    [SerializeField] float rayDistance = 3;
    [SerializeField] float speed = 8;
    [SerializeField] float rotSpeed = 200;
    
    #endregion

    #region Stamina
    float staminaMax = 50f;
    float staminaReduction = 5f;
    float staminaIncrement = 5f;
    #endregion

    void Update()
    {
        Movement();
    }



    void Movement()
    {
        //Movimientos
        float h = Input.GetAxisRaw("Horizontal") * rotSpeed * Time.deltaTime;
        float v = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, v);
        transform.Rotate(0, h, 0);

        //El rayito laser piupiu
        RaycastHit hit;
        Debug.DrawRay(
            transform.position,
            transform.forward * rayDistance,
            Color.red);

        if (Input.GetKey(KeyCode.F))
        {
            staminaMax -= staminaReduction * Time.deltaTime;
        }
        else
        {
            staminaMax += staminaIncrement * Time.deltaTime;
        }
    }

}