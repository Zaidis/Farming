using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class OpenDoor : MonoBehaviour
{

    public PlayableDirector dir;

    private void Awake() {
        dir = this.GetComponent<PlayableDirector>();
    }
    public void OpenTheDoor() {
        dir.Play();
    }
    
}
