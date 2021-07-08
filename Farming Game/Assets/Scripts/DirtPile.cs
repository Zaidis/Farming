using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPile : MonoBehaviour
{

    public Crop myCrop; //the crop that the pile will hold

    private float deathTimer = 60f;
    public bool hasCrop;
    private void Update() {
        if (!hasCrop) {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0) {
                Destroy(this.gameObject);
            }
        }
        
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Seed")) {
            GameObject crop = collision.collider.gameObject;

            myCrop = crop.GetComponent<Seed>().seedCrop;
            hasCrop = true;
            Destroy(crop.gameObject);
        }
    }
}
