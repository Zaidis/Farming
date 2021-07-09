using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public static Tree Instance;

    public List<Transform> spawnLocations = new List<Transform>();
    private List<int> spawnNumbers = new List<int>();
    public GameObject apple;
    public LayerMask appleMask;
    private void Awake() {
        if(Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        ResetNumbers();
    }
    public void SpawnApple() {
        int rand = Random.Range(0, spawnNumbers.Count);
        int number = spawnNumbers[rand];

        if(Physics.CheckSphere(spawnLocations[number].position, 0.4f, appleMask)) {
            //if an apple would spawn on top of another apple, do not spawn the new apple
            return;
        }

        GameObject appleObject = Instantiate(apple, spawnLocations[number].position, Quaternion.identity);
        appleObject.GetComponent<Rigidbody>().useGravity = false;
        appleObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
        spawnNumbers.Remove(number);

        if(spawnNumbers.Count <= 0) {
            ResetNumbers();
        }
    }

    void ResetNumbers() {
        for (int i = 0; i < spawnLocations.Count; i++) {
            spawnNumbers.Add(i);
        }
    }
}
