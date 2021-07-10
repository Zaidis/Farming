using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    [SerializeField]
    List<AudioClip> music = new List<AudioClip>();
    AudioSource source;

    public static SoundManager instance;


    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }
    void Start() {
        source = GetComponent<AudioSource>();

        StartCoroutine(Play());
    }
    public void StopMusic() {
        StopAllCoroutines();
        source.Stop();
    }
    IEnumerator Play() {
        while (true) {
            source.clip = music[Random.Range(0, music.Count)];
            source.Play();
            yield return new WaitForSeconds(source.clip.length);
        }
    }
}