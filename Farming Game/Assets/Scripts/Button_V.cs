using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_V : MonoBehaviour
{
    public GameObject item;
    public Transform spawner;
    public void SpawnItem() {
        Instantiate(item, spawner.position, Quaternion.identity);
    }
}
