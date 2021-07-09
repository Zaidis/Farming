using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public ParticleSystem part;

    private void Awake() {
        part = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other) {
        if (other.gameObject.CompareTag("Dirt")) {
            other.gameObject.GetComponent<DirtPile>().Wet();
        }
    }
}
