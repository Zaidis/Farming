using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{

    public List<GameObject> crops = new List<GameObject>();
    public List<Crop> cropsList = new List<Crop>();

    public Crop FindCrop(string name) {
        for(int i = 0; i < cropsList.Count; i++) {
            if(name == cropsList[i].name) {
                return cropsList[i];
            }
        }
        return null;
    }
}
