using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingManager : MonoBehaviour
{
    public float maxDistance;
    public LayerMask ground;
    public GameObject test;
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

                    Instantiate(test, location, Quaternion.identity);
                }
            } else if (GameManager.instance.currentItemSlotNum == 1) { //other tool

            } else { //your hand

            }
        }
    }

}
