using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using static UnityEditor.Progress;


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
    [SerializeField] SpawnItems spawnItems;
    #endregion

    #region Pieces
    [Header("Pieces Variables")]
    [SerializeField] List<PieceData> pieces = new List<PieceData>();
    public bool addingPiece = false;
    private Coroutine adding;

    //Incolor pieces
    public int incolorCount;
    [SerializeField] TMP_Text incolorText;

    //Red pieces
    public int redCount;
    [SerializeField] TMP_Text redCountText;

    //Blue pieces
    public int blueCount;
    [SerializeField] TMP_Text blueCountText;

    //Green pieces
    public int greenCount;
    [SerializeField] TMP_Text greenCountText;

    //Purple pieces
    public int purpleCount;
    [SerializeField] TMP_Text purpleCountText;

    //Silver pieces
    public int silverCount;
    [SerializeField] TMP_Text silverCountText;

    //Gold pieces
    public int goldenCount;
    [SerializeField] TMP_Text goldenCountText;

    #endregion

    #region Transport
    [Header("Transport Variables")]
    public LayerMask transportMask;
    public Transform player;
    public Vector3 ogScale;
    private Transform wagon;
    private Animator wagonAnim;
    #endregion

    #region Money and Consumables
    [Header("Money and Consumables Variables")]
    [SerializeField] public float money = 100;
    [SerializeField] List<Consumable> consumables = new List<Consumable>();
    Consumable currentItem;
    [SerializeField] int itemAmount;
    public TMP_Text moneyText;
    [SerializeField] TMP_Text itemAmountText;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            ConsumeItem(currentItem);
        }

        if (consumables.Count <= 0) { itemUI.sprite = notItemSprite; }
        else { itemUI.sprite = currentItem.consumableSprite;}
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
            spawnItems = hit.transform.gameObject.GetComponent<SpawnItems>();

            //Gasta estamina si se pulsa F
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Mining");
                if (CompareTag("Rift"))
                {
                    spawnItems.Mining();
                }
                else
                {
                    Destroy(hit.transform.gameObject);
                }
                #region STAMINA
                stamina -= mineCost;
                if (stamina < 0) stamina = 0;
                StaminaBar.fillAmount = stamina/maxStamina;

                if (recharge != null) StopCoroutine(recharge);
                recharge = StartCoroutine(RechargeStamina());
                #endregion
            }
        }
        #endregion

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

    private IEnumerator AddingPieceTimer()
    {
        yield return new WaitForSeconds(1.7f);

        addingPiece = false;
    }

    #region PIECES LIST
    public void AddPiece(PieceData fragment)
    {
        if (fragment.CompareTag("IncolorPiece") || fragment.CompareTag("RedPiece") ||
            fragment.CompareTag("BluePiece") || fragment.CompareTag("GreenPiece") ||
            fragment.CompareTag("PurplePiece") || fragment.CompareTag("SilverPiece") ||
            fragment.CompareTag("GoldenPiece"))
        {
            pieces.Add(fragment);
            addingPiece = true;
            if (adding != null) StopCoroutine(adding);
            adding = StartCoroutine(AddingPieceTimer());
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
        Debug.Log(item.GetComponent<Consumable>().consumableName + " " + "added to consumables");
        itemUI.sprite = item.GetComponent<Consumable>().consumableSprite;    

    }

    //Donde colocar la funcion para poder activarla?
    public void ConsumeItem(Consumable item)
    {
        if (consumables.Contains(item))
        {
            Debug.Log(item.GetComponent<Consumable>().consumableName + " " + "removed from consumables");
            stamina += item.GetComponent<Consumable>().regenerationAmount;
            consumables.Remove(item);
            itemUI.sprite = notItemSprite;
            itemAmount--;
            itemAmountText.text = itemAmount.ToString();
            if (itemAmount != 0 || consumables.Count > 0) { currentItem = consumables[0]; }
            if (itemAmount <= 0) 
            { 
                itemAmount = 0;
                currentItem = null;                
            }
            moneyText.text = "$" + itemAmount.ToString();
        }
    }
    #endregion

    #region TRIGGER
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PieceData>())  
        {
            AddPiece(other.GetComponent<PieceData>());
            #region PIECE COUNTER
            if (other.CompareTag("IncolorPiece")) { incolorCount++; incolorText.text = incolorCount.ToString(); Destroy(other.gameObject); }
            if (other.CompareTag("RedPiece")) { redCount++; redCountText.text = redCount.ToString(); Destroy(other.gameObject); }
            if (other.CompareTag("BluePiece")) { blueCount++; blueCountText.text = blueCount.ToString(); Destroy(other.gameObject); }
            if (other.CompareTag("GreenPiece")) {  greenCount++; greenCountText.text = greenCount.ToString(); Destroy(other.gameObject); }            
            if (other.CompareTag("PurplePiece")) { purpleCount++; purpleCountText.text = purpleCount.ToString(); Destroy(other.gameObject); }
            if (other.CompareTag("SilverPiece")) { silverCount++; silverCountText.text = silverCount.ToString(); Destroy(other.gameObject); }
            if (other.CompareTag("GoldenPiece")) { goldenCount++; goldenCountText.text = goldenCount.ToString(); Destroy (other.gameObject); }
            #endregion
        }

        if (other.GetComponent<Consumable>())
        {
            float price = other.GetComponent<Consumable>().consumablePrice;
            if (money >= price)
            {
                Consumable thisConsumable = other.GetComponent<Consumable>();
                AddConsumable(thisConsumable);
                currentItem = thisConsumable;
                itemAmount++;
                itemAmountText.text = itemAmount.ToString();
                money -= price;
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
            wagonAnim = wagon.GetComponent<Animator>();
            player.SetParent(wagon.transform);
            //player.localPosition = Vector3.zero;
            wagonAnim.SetFloat("New Float", 1);

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    Debug.Log("Walking");
            //    wagon = null;
            //    player.SetParent(null);
            //    player.transform.localScale = ogScale;
            //}
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