using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BasketManager : MonoBehaviour
{
    public List<Recipe> recipies = new List<Recipe>();
    public List<Crop> recipe = new List<Crop>(); //what the goat god wants
    public List<Crop> cropsInBasket = new List<Crop>(); //what the goat god is getting

    public TextMeshPro whiteBoard;
    private void Start() {
        NewRecipe();
    }
    public void NewRecipe() {
        int rand = Random.Range(0, recipies.Count);
        Recipe newRecipe = recipies[rand];
        recipe.Clear();
        for(int i = 0; i < newRecipe.crops.Count; i++) {
            recipe.Add(newRecipe.crops[i]);
        }

        whiteBoard.text = newRecipe.recipeText;
    }
    public void CheckBasket() {
        for(int i = 0; i < cropsInBasket.Count; i++) {
            Crop crop = cropsInBasket[i];
            for(int j = 0; j < recipe.Count; j++) {
                if(crop == recipe[j]) {
                    recipe.RemoveAt(j);
                    break;
                }
            }
        }

        if(recipe.Count > 0) {
            //you pissed off the goat god
            Debug.Log("You pissed off the Goat God");
        } else {
            Debug.Log("The Goat God is pleased");
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Item")) {
            GameObject obj = collision.collider.gameObject;
            if(obj.GetComponent<Seed>() != null) {
                //it is an object for the basket
                cropsInBasket.Add(obj.GetComponent<Seed>().seedCrop);
                Destroy(obj.gameObject);
            }
        }
    }

}
