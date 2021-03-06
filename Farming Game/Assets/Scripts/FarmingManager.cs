using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingManager : MonoBehaviour
{
    public float maxDistance;
    public LayerMask ground;
    public LayerMask itemMask;
    public LayerMask buttonMask;
    public LayerMask bowlingMask;
    public LayerMask tutMask;
    public LayerMask openMask;
    public GameObject dirtPile;
    public Transform handLocation;
    public bool isHolding;
    public float force;

    public List<GameObject> objects = new List<GameObject>();
    public int objectAmount;
    private void Awake() {
        Physics.IgnoreLayerCollision(9, 10);
    }
    private void Update() {
        this.transform.position = PlayerManager.instance.gameObject.transform.position;
        if (Input.GetMouseButtonDown(0)) { //left click
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (GameManager.instance.currentItemSlotNum == 0) { //hoe
                if (Physics.Raycast(ray, out hit, maxDistance, ground)) {
                    Vector3 location = hit.point;

                    GameObject dirt = Instantiate(dirtPile, location, Quaternion.identity);
                    dirt.transform.rotation = Quaternion.Euler(-90, 0, 0);
                }
            } else if (GameManager.instance.currentItemSlotNum == 1) { //watercan
                //this will affect the watering can. use this to make the dirt wet
                GameManager.instance.items[1].GetComponent<WaterCan>().TurnOn();

            } else { //your hand
                if (isHolding) { //drop item
                    GameObject obj = objects[objects.Count - 1];
                    obj.GetComponent<Rigidbody>().useGravity = true;
                    obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    obj.GetComponent<Rigidbody>().drag = 0;
                    obj.GetComponent<Collider>().enabled = true;
                    obj.transform.parent = null;
                    objects.Remove(obj);
                    GameManager.instance.ChangeItemCount(-1);
                    if (objects.Count <= 0) {
                        isHolding = false;
                        GameManager.instance.handIcon.sprite = GameManager.instance.handIconSprite;
                    }
                }
            }

            //checks to see if you left clicked on a button
            if(Physics.Raycast(ray, out hit, maxDistance, buttonMask)) {
                hit.collider.gameObject.GetComponent<Button_V>().SpawnItem();
            } else if (Physics.Raycast(ray, out hit, maxDistance, bowlingMask)) {
                hit.collider.gameObject.GetComponent<Bowling>().ResetPins();
            } else if (Physics.Raycast(ray, out hit, maxDistance, tutMask)) {
                hit.collider.gameObject.transform.parent.GetComponent<Blackboard>().ChangeSlide();
            } else if (Physics.Raycast(ray, out hit, maxDistance, openMask)) {
                OpenDoor.instance.OpenTheDoor();
                Destroy(hit.collider.gameObject);
            }

        } else if (Input.GetKeyDown(KeyCode.E)) {
            if (!isHolding) { //if you are not holding onto an object
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, maxDistance, itemMask)) {
                    if (hit.collider.gameObject.CompareTag("Item")) {
                        
                        GameObject obj = hit.collider.gameObject;
                        objects.Add(obj);
                        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        obj.GetComponent<Rigidbody>().useGravity = false;
                        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                        obj.GetComponent<Collider>().enabled = false;
                        obj.transform.parent = handLocation;
                        obj.GetComponent<Rigidbody>().drag = 100;
                        obj.transform.localPosition = Vector3.zero;

                        GameManager.instance.ChangeItemCount(1);
                        isHolding = true;

                        if(obj.GetComponent<Item>().Name == "Apple") {
                            GameManager.instance.handIcon.sprite = GameManager.instance.holdingIcons[0];
                        } else if (obj.GetComponent<Item>().Name == "Eggplant") {
                            GameManager.instance.handIcon.sprite = GameManager.instance.holdingIcons[1];
                        } else if (obj.GetComponent<Item>().Name == "Carrot") {
                            GameManager.instance.handIcon.sprite = GameManager.instance.holdingIcons[2];
                        }

                    }
                }
            } else { //you ARE holding onto something
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, maxDistance, itemMask)) {
                    if (hit.collider.gameObject.CompareTag("Item")) {
                        
                        GameObject obj = hit.collider.gameObject;
                        if (obj.GetComponent<Item>().Name == objects[0].GetComponent<Item>().Name) {
                            //if (obj.GetComponent<Seed>().seedCrop == objects[0].GetComponent<Seed>().seedCrop) { //makes sure you pick up the same type of object that you are aleady holding
                                objects.Add(obj);
                                obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                                obj.GetComponent<Rigidbody>().useGravity = false;
                                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                                obj.GetComponent<Collider>().enabled = false;
                                obj.transform.parent = handLocation;
                                obj.GetComponent<Rigidbody>().drag = 100;
                                obj.transform.localPosition = Vector3.zero;
                                GameManager.instance.ChangeItemCount(1);
                            //}
                        }
                    }
                }
            }
        } else if (Input.GetMouseButtonDown(1)) {
            if (isHolding) { //drop item
                GameObject obj = objects[objects.Count - 1];
                obj.GetComponent<Rigidbody>().useGravity = true;
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                obj.GetComponent<Rigidbody>().drag = 0;
                obj.GetComponent<Collider>().enabled = true;
                obj.transform.parent = null;
                obj.GetComponent<Rigidbody>().AddForce(handLocation.transform.forward * force, ForceMode.Impulse);
                objects.Remove(obj);
                GameManager.instance.ChangeItemCount(-1);
                if (objects.Count <= 0) {
                    isHolding = false;
                    GameManager.instance.handIcon.sprite = GameManager.instance.handIconSprite;
                }
            }
        }
    }

}
