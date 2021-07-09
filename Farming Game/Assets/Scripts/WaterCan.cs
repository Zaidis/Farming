using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
    
    public void TurnOn() {
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
    }

}
