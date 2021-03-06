using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtPile : MonoBehaviour
{

    public Crop myCrop; //the crop that the pile will hold
    public Transform spawnlocation;
    private float deathTimer = 60f;
    private float cropTimer = 60f;
    public bool hasCrop;
    public bool isWet;
    private void Update() {
        if (!hasCrop) {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0) {
                Destroy(this.gameObject);
            }
        } else {
            if (isWet) {
                cropTimer -= Time.deltaTime * 2;
            } else {
                cropTimer -= Time.deltaTime;
            }
            
            if(cropTimer <= 0) {
                for(int i = 0; i < GameManager.instance.gameObject.GetComponent<ItemDatabase>().cropsList.Count; i++) {
                    if(myCrop == GameManager.instance.gameObject.GetComponent<ItemDatabase>().cropsList[i]) {
                        Instantiate(GameManager.instance.gameObject.GetComponent<ItemDatabase>().crops[i], spawnlocation.position, Quaternion.identity);
                        break;
                    }
                }
                Destroy(this.gameObject);
            }
        }
        
    }
    public void Wet() {
        isWet = true;
        GetComponent<MeshRenderer>().material = GameManager.instance.dirtWet;
    }
    private void OnParticleCollision() {
        print("hi");
        Wet();
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Item")) {

            GameObject crop = collision.collider.gameObject;
            if(crop.GetComponent<Seed>() != null) {
                myCrop = crop.GetComponent<Seed>().seedCrop;
                hasCrop = true;
                Destroy(crop.gameObject);
            }
           
        }
    }
}
