using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    #region Movement Variables
    [SerializeField] float speed = 8;
    [SerializeField] float rotSpeed = 200;
    #endregion

    #region Stamina
    float staminaMax = 50f;
    float staminaReduction = 5f;
    float staminaIncrement = 5f;
    #endregion

    private void Update()
    {
        Movement();
    }

    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal") * rotSpeed * Time.deltaTime;
        float v = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, v);
        transform.Rotate(h, 0, 0);

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
