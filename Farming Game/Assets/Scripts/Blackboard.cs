using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Blackboard : MonoBehaviour
{
    [TextArea()]
    public List<string> lines = new List<string>();

    public TextMeshPro myText;
    public int currentNum;

    private void Start() {
        currentNum = 0;
        myText.text = lines[currentNum];
    }

    public void ChangeSlide() {
        currentNum++;
        if(currentNum > lines.Count) {
            currentNum = 0;
        }
        myText.text = lines[currentNum];
    }
}
