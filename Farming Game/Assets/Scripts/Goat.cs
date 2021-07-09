using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Goat : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool angry = false;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        
    }
    private void Update() {
        if (angry) {
            agent.SetDestination(PlayerManager.instance.gameObject.transform.position);

        } else {
            //going to check the basket
            agent.SetDestination(BasketManager.instance.gameObject.transform.position);
        }
            
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Basket")) {
            if (BasketManager.instance.CheckBasket()) {
                //you did well
                Destroy(this.gameObject);
                return;
            } else {
                angry = true;
                GameManager.instance.GoatIsAngry();
            }
        }
    }

}
