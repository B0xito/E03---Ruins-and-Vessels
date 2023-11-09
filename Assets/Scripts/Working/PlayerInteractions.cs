using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;


public class PlayerInteractions : MonoBehaviour
{
    #region Movement Variables
    [Header("Movement Variables")]
    [SerializeField] float rayDistance = 3;
    [SerializeField] float speed = 8;
    [SerializeField] float rotSpeed = 200;
    #endregion

    #region Stamina
    [Header("Stamina Variables")]
    public Image StaminaBar;

    [SerializeField] float stamina, maxStamina;
    [SerializeField] float mineCost;

    public float chargeRate;
    private Coroutine recharge;
    #endregion

    #region Mine
    public LayerMask minableMask;
    #endregion

    #region Pieces
    [Header("Pieces Variables")]
    [SerializeField] List<PieceData> pieces = new List<PieceData>();
    [SerializeField] int redCount;
    [SerializeField] TMP_Text redCountText;
    #endregion

    #region Transport
    [Header("Transport Variables")]
    public LayerMask transportMask;
    public Transform player;
    public Vector3 ogScale;
    private Transform wagon;
    #endregion

    #region Money and Consumables
    [Header("Money and Consumables Variables")]
    [SerializeField] public float money = 100;
    [SerializeField] List<Consumable> consumables = new List<Consumable>();
    [SerializeField] GameObject notMoney;
    [SerializeField] Image itemUI;
    [SerializeField] Sprite notItemSprite;
    #endregion

    private void Start()
    {
        maxStamina = 100f;
        stamina = maxStamina;
        pieces.Clear();
        notMoney.SetActive(false);
        redCount = 0;
        ogScale = new Vector3(1, 1, 1);
    }

    void Update()
    {
        Interactions();

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    if (!consumables.Contains(item))
        //    {
        //        stamina += item.GetComponent<Consumable>().regenerationAmount;
        //        consumables.Remove(item);
        //        itemUI.sprite = notItemSprite;
        //    }
        //}

        if (consumables.Count <= 0) { itemUI.sprite = notItemSprite; }
    }

    void Interactions()
    {
        #region MOVEMENT AND ROTATION
        float h = Input.GetAxisRaw("Horizontal") * rotSpeed * Time.deltaTime;
        float v = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.Translate(0, 0, v);
        transform.Rotate(0, h, 0);
        #endregion

        #region RAYCAST
        RaycastHit hit;
        Debug.DrawRay(transform.position,transform.forward * rayDistance,Color.red);

        #region MINE
        //Revisa con un rayo desde la posicion 0,0 del player hacia adelante si hay un gameobject
        //con el layer minable, si es asi entra el if
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, minableMask))
        {
            //Gasta estamina si se pulsa F
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Mining");
                Destroy(hit.transform.gameObject);

                #region STAMINA
                stamina -= mineCost;
                if (stamina < 0) stamina = 0;
                StaminaBar.fillAmount = stamina/maxStamina;

                if(recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
                #endregion
            }
        }
        #endregion

        //#region TRANSPORT
        //if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, transportMask))
        //{
        //    if (Input.GetKeyDown(KeyCode.F))
        //    {
        //        Debug.Log("Riding");
        //        wagon = hit.transform;
        //        player.SetParent(wagon);
        //        player.localPosition = Vector3.zero;

        //        if (Input.GetKeyDown(KeyCode.Escape))
        //        {
        //            Debug.Log("Walking");
        //            wagon = null;
        //            player.SetParent(null);
        //            player.transform.localScale = Vector3.one;
        //        }
        //    }
        //}
        //#endregion

        #endregion
    }

    //Recargar stamina
    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);

        while(stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            StaminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }

    #region PIECES LIST
    public void AddPiece(PieceData fragment)
    {
        if (fragment.CompareTag("RedPiece"))
        {
            pieces.Add(fragment);
        }
    }

    public void RemovePiece(PieceData fragment)
    {
        if (pieces.Contains(fragment))
        {
            pieces.Remove(fragment);
        }
    }
    #endregion

    #region CONSUMABLES
    public void AddConsumable(Consumable item)
    {
        consumables.Add(item);
        itemUI.sprite = item.GetComponent<Consumable>().consumableSprite;    
    }

    //Donde colocar la funcion para poder activarla?
    public void ConsumeItem(Consumable item)
    {
        if (!consumables.Contains(item))
        {
           stamina += item.GetComponent<Consumable>().regenerationAmount;
           consumables.Remove(item);
           itemUI.sprite = notItemSprite;
        }
    }

    private IEnumerator DestroyConsumable()
    { 
        yield return new WaitForSeconds(1f); 
    }
    #endregion

    #region TRIGGER
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RedPiece") && other.gameObject.GetComponent<PieceData>()) //si se quiere anadir piezas de otro colores usar " || other.CompareTag("PieceColor")" en Piece Color introduce el tag que creaste con Piece+Color 
        {
            AddPiece(other.GetComponent<PieceData>());
            redCount++;
            redCountText.text = redCount.ToString();
            other.gameObject.SetActive(false);

        }

        if (other.GetComponent<Consumable>())
        {
            float price = other.GetComponent<Consumable>().consumablePrice;
            if (money >= price)
            {
                AddConsumable(other.GetComponent<Consumable>());               
            }
            else
            {
                notMoney.SetActive(true);
            }
        }

        if (other.CompareTag("Transport"))
        {
            Debug.Log("Riding");
            wagon = other.transform;
            player.SetParent(wagon);
            player.localPosition = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Walking");
                wagon = null;
                player.SetParent(null);
                player.transform.localScale = ogScale;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Consumable>())
        {
            notMoney.SetActive(false);
        }
    }
    #endregion
}