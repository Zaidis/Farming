using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class OpenDoor : MonoBehaviour
{
    public static OpenDoor instance;
    public PlayableDirector dir;

    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
        dir = this.GetComponent<PlayableDirector>();
    }
    public void OpenTheDoor() {
        dir.Play();
        GameManager.instance.gameHasStarted = true;
    }
    
}
