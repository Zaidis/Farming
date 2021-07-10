using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BasketManager : MonoBehaviour
{
    public static BasketManager instance;
    public List<Recipe> recipies = new List<Recipe>();
    public List<Crop> recipe = new List<Crop>(); //what the goat god wants
    public List<Crop> cropsInBasket = new List<Crop>(); //what the goat god is getting

    public TextMeshPro whiteBoard;
    public GameObject basketText;
    private int appleNum;
    private int carrotNum;
    private int eggplantNum;
    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }
    private void Start() {
        NewRecipe();
    }

    private void Update() {
        basketText.transform.LookAt(PlayerManager.instance.transform);
    }
    public void NewRecipe() {
        int rand = Random.Range(0, recipies.Count);
        Recipe newRecipe = recipies[rand];
        appleNum = 0;
        carrotNum = 0;
        eggplantNum = 0;
        ChangeText();
        recipe.Clear();
        for(int i = 0; i < newRecipe.crops.Count; i++) {
            recipe.Add(newRecipe.crops[i]);
        }

        whiteBoard.text = newRecipe.recipeText;
    }
    public bool CheckBasket() {
        /*for(int i = 0; i < cropsInBasket.Count; i++) {
            Crop crop = cropsInBasket[i];
            for(int j = 0; j < recipe.Count; j++) {
                if(crop == recipe[j]) {
                    recipe.RemoveAt(j);
                    break;
                }
            }
        } */

        if(recipe.Count > 0) {
            //you pissed off the goat god
            Debug.Log("You pissed off the Goat God");
            return false;
        } else {
            //the goat god is indeed happy with the results
            Debug.Log("The Goat God is pleased");
        }

        NewRecipe();
        return true;
    }

    public void ChangeText() {
        basketText.transform.GetChild(0).GetComponent<TextMeshPro>().text = appleNum + "-Apples     " + carrotNum + "-Carrots     " + eggplantNum + "-Eggplants     ";
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.gameObject.CompareTag("Item")) {
            GameObject obj = collision.collider.gameObject;
            string cropName = obj.GetComponent<Item>().Name;
            if(obj.GetComponent<Seed>() != null) {
                //this is a seed
                return;
            } else {
                //this is an item for the basket
                Crop crop = GameManager.instance.gameObject.GetComponent<ItemDatabase>().FindCrop(cropName);
                if (crop.name == "Apple") {
                    appleNum++;
                } else if(crop.name == "Carrot") {
                    carrotNum++;
                } else if (crop.name == "Eggplant") {
                    eggplantNum++;
                }

                for(int i = 0; i < recipe.Count; i++) {
                    if(crop == recipe[i]) {
                        recipe.RemoveAt(i);
                        break;
                    }
                }
                ChangeText();
                cropsInBasket.Add(crop);
                Destroy(obj.gameObject);
            }
        }
    }

}
