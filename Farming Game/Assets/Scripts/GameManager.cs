using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameHasStarted;
    public Material niceSkybox;
    public Material meanSkybox;

    [Header("Goat")]
    private float maxGoatTimer = 240f;
    public float goatTimer;
    public GameObject goat;
    public Transform goatSpawnLocation;
    public Light globalLight;
    public WindZone zone;
    public TextMeshProUGUI offeringText; //text on the UI
    public AudioClip goatScream;
    public AudioClip niceSound;
    [Header("Item Slots")]
    public List<GameObject> itemSlots = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    [Header("UI Items")]
    public Image handIcon;
    public Sprite handIconSprite;
    public Sprite[] holdingIcons;
    public GameObject currentItemSlot;

    [Header("Settings Menu")]
    public GameObject settingsMenu;
    public bool inSettings;
    /*
     * 1. Hoe
     * 2. Water can
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

    [Header("Dirt Material")]
    public Material dirtWet;
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    private void Start() {
        goatTimer = maxGoatTimer;
        offeringText.text = "Next Offering: " + goatTimer.ToString("0");
        for (int i = 0; i < itemSlots.Count; i++) {
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
        zone.windTurbulence = 1f;
        this.GetComponent<AudioSource>().clip = niceSound;
        this.GetComponent<AudioSource>().loop = false;
        this.GetComponent<AudioSource>().Play();
    }
    public void GoatIsAngry() {
        RenderSettings.skybox = meanSkybox;
        globalLight.color = Color.red;
        zone.windTurbulence = 15f;
        SoundManager.instance.StopMusic();
        this.GetComponent<AudioSource>().clip = goatScream;
        this.GetComponent<AudioSource>().loop = true;
        this.GetComponent<AudioSource>().Play();
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!inSettings) {
                settingsMenu.SetActive(true);
                inSettings = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            } else {
                settingsMenu.SetActive(false);
                inSettings = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
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
        if (gameHasStarted) { //called when the game has officially started

            goatTimer -= Time.deltaTime;
            offeringText.text = "Next Offering: " + goatTimer.ToString("0");
            if (goatTimer <= 0) {
                SpawnGoatLord();
                goatTimer = maxGoatTimer;
            }

            appleTimer -= Time.deltaTime;
            if (appleTimer <= 0) {
                Tree.Instance.SpawnApple();
                appleTimer = maxAppleTimer;
            }
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
