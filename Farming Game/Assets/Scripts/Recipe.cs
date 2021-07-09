using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "New Recipe")]
public class Recipe : ScriptableObject
{
    public List<Crop> crops = new List<Crop>();
    [TextArea()]
    public string recipeText;
}
