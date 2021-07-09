using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Material niceSkybox;
    public Material meanSkybox;

    [Header("Goat")]
    private float maxGoatTimer = 240f;
    public float goatTimer;
    public GameObject goat;
    public Transform goatSpawnLocation;
    public Light globalLight;
    [Header("Item Slots")]
    public List<GameObject> itemSlots = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public GameObject currentItemSlot;
    /*
     * 1. Hoe
     * 2. Other Tool
     * 3. Open Hand
     */
    public int currentItemSlotNum = 0;

    private Color32 onSlot = new Color32(150, 255, 250, 255);
    private Color32 offSlot = new Color32(150, 255, 250, 160);

    //Item counter
    public GameObject counter;
    public TextMeshProUGUI itemCounterText;
    public int countNum;

    public float appleTimer;
    private float maxAppleTimer = 25f;
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    private void Start() {
        goatTimer = maxGoatTimer;
        for(int i = 0; i < itemSlots.Count; i++) {
            itemSlots[i].transform.GetChild(0).GetComponent<Image>().color = offSlot;
            items[i].gameObject.SetActive(false);
        }
        InitializeSlot(); //initially the first slot, the left most slot
    }
    public void SpawnGoatLord() {
        Instantiate(goat, goatSpawnLocation.position, Quaternion.identity);
    }
    public void GoatNotAngry() {
        RenderSettings.skybox = niceSkybox;
        globalLight.color = new Color32(255, 244, 214, 255);
    }
    public void GoatIsAngry() {
        RenderSettings.skybox = meanSkybox;
        globalLight.color = Color.red;
    }
    private void Update() {

        goatTimer -= Time.deltaTime;
        if(goatTimer <= 0) {
            SpawnGoatLord();
            goatTimer = maxGoatTimer;
        }

        if(Input.GetAxis("Mouse ScrollWheel") < 0f) {
            //up
            currentItemSlotNum++;
            if (currentItemSlotNum > 2) {
                currentItemSlotNum = 0;
            }
            ChangeSlot();
        } else if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
            //down
            currentItemSlotNum--;
            if(currentItemSlotNum < 0) {
                currentItemSlotNum = 2;
            }
            ChangeSlot();
        }

        appleTimer -= Time.deltaTime;
        if(appleTimer <= 0) {
            Tree.Instance.SpawnApple();
            appleTimer = maxAppleTimer;
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            BasketManager.instance.CheckBasket();
        }

    }

    void InitializeSlot() {
        currentItemSlot = itemSlots[currentItemSlotNum];
        currentItemSlot.transform.GetChild(0).GetComponent<Image>().color = onSlot;
        items[currentItemSlotNum].gameObject.SetActive(true);
    }
    void ChangeSlot() {
        currentItemSlot.transform.GetChild(0).GetComponent<Image>().color = offSlot;
        currentItemSlot = itemSlots[currentItemSlotNum];
        currentItemSlot.transform.GetChild(0).GetComponent<Image>().color = onSlot;

        foreach(GameObject item in items) {
            item.SetActive(false);
        }
        items[currentItemSlotNum].gameObject.SetActive(true);

    }

    public void ChangeItemCount(int num) {
        countNum += num;
        itemCounterText.text = countNum.ToString();
        if(countNum <= 0) {
            counter.SetActive(false);
        } else {
            counter.SetActive(true);
        }
    }
}
