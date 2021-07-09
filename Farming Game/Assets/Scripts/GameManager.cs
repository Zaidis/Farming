using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    private void Start() {
        for(int i = 0; i < itemSlots.Count; i++) {
            itemSlots[i].transform.GetChild(0).GetComponent<Image>().color = offSlot;
            items[i].gameObject.SetActive(false);
        }
        InitializeSlot(); //initially the first slot, the left most slot
    }

    private void Update() {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) {
            //up
            currentItemSlotNum++;
            if (currentItemSlotNum > 2) {
                currentItemSlotNum = 0;
            }
            ChangeSlot();
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
            //down
            currentItemSlotNum--;
            if(currentItemSlotNum < 0) {
                currentItemSlotNum = 2;
            }
            ChangeSlot();
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
}
