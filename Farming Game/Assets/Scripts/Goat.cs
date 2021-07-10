using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Goat : MonoBehaviour
{
    public NavMeshAgent agent;
    public bool angry = false;
    public float defaultSpeed;
    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        
    }
    private void Start() {
        defaultSpeed = agent.speed;
    }
    private void Update() {
        if (angry) {
            agent.SetDestination(PlayerManager.instance.gameObject.transform.position);
            agent.speed = defaultSpeed + 10;
        } else {
            //going to check the basket
            agent.SetDestination(BasketManager.instance.gameObject.transform.position);
            agent.speed = defaultSpeed;
        }
            
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Basket")) {
            if (BasketManager.instance.CheckBasket()) {
                //you did well
                GameManager.instance.GoatNotAngry();
                Destroy(this.gameObject);
                return;
            } else {
                angry = true;
                GameManager.instance.GoatIsAngry();
            }
        } else if (other.gameObject.CompareTag("Player")) {
            print("You have died!");
            SceneManager.LoadScene(0);
        }
    }

}
