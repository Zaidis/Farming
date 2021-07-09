using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Hoop : MonoBehaviour
{
    public int hoopCounter;
    public TextMeshPro cc;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Item")) {
            hoopCounter++;
        }
        cc.text = hoopCounter.ToString();
    }

}
