using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowling : MonoBehaviour
{
    public List<GameObject> pins = new List<GameObject>();
    public List<Vector3> pinLocs = new List<Vector3>();
    public Vector3 ballLocation;
    public GameObject ball;
    private void Start() {
        for(int i = 0; i < pins.Count; i++) {
            pinLocs.Add(pins[i].gameObject.transform.position);
        }
        ballLocation = ball.transform.position;
    }
    public void ResetPins() {
        foreach(GameObject pin in pins) {
            pin.transform.rotation = Quaternion.Euler(-89.98f, 0f, 0f);
        }
        for(int i = 0; i < pins.Count; i++) {
            pins[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
            pins[i].transform.position = pinLocs[i];
        }
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        ball.transform.position = ballLocation;
    }
}
