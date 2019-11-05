using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public static CookingManager CM;
    [Header("Cooking Zone Game")]
    [SerializeField] GameObject cookingGame;
    [SerializeField] GameObject ingredientMenu;
    ZoneGame zoneGame;
    float theResult;
    [SerializeField] List<Recipe> recipes;
    int product;

    private void Awake()
    {
        zoneGame = cookingGame.GetComponent<ZoneGame>();
        cookingGame.SetActive(false);
        ingredientMenu.SetActive(false);

        if (CM == null)
        {
            CM = this;
        } else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Start a Cooking Game
    /// </summary>
    public void StartCooking(int ingredient1, int ingredient2)
    {

        //print(cookingID);
        product = GetRecipe(ingredient1, ingredient2);
        //Check recipes here.
        StartZoneGame();
    }


    /// <summary>
    /// Returns the ID of the recipe if there is a match. If not, then it returns Disgusting Soup ID
    /// </summary>
    /// <param name="ingredient1"></param>
    /// <param name="ingredient2"></param>
    /// <returns></returns>
    public int GetRecipe(int ingredient1, int ingredient2)
    {
        string cookingID;
        if (ingredient1 > ingredient2)
        {
            cookingID = ingredient2.ToString() + ingredient1.ToString();
        }
        else
        {
            cookingID = ingredient1.ToString() + ingredient2.ToString();
        }
        foreach (Recipe recipe in recipes)
        {
            //Look for matching recipes
            if (recipe.id.ToString() == cookingID)
            {
                //Found a match
                print("Found Match!");
                return recipe.finishedProductID;
                
            }
            else
            {
                //Did not find match
                print("No Match!");
                //Disgusting Soup ID
                return 41;
            }
        }
        return 0;
    }



    /// <summary>
    /// Opens the ingredient menu!
    /// </summary>
    public void OpenIngredientMenu()
    {
        //Pop up the ingredient menu
        ingredientMenu.SetActive(true);
    }


    /// <summary>
    /// Closes the ingredient menu!
    /// </summary>
    public void CloseIngredientMenu()
    {
        //Close ingredient menu
        ingredientMenu.SetActive(false);
    }

    /// <summary>
    /// Starts a zone game!
    /// </summary>
    void StartZoneGame()
    {
        //This is where the difficulty would be calculated and ingredients inputted.
        zoneGame.percentDifficulty = 20;
        zoneGame.speed = 200;

        cookingGame.SetActive(true);
    }

    /// <summary>
    /// Call to save the result of the cooking game and reset the game itself
    /// </summary>
    public void GameFinished()
    {
        theResult = zoneGame.Result();
        if(theResult < 0.6f)
        {
            //Whatever the disgusting ID is.
            product = 41;
        }
        print("The quality of food is: " + theResult);
        zoneGame.ResetGame();
        InteractionManager.IM.StopInteraction();
        YieldFood();
    }

    /// <summary>
    /// Give the player the food they cooked.
    /// </summary>
    void YieldFood()
    {
        if(Inventory.inventory.CanAddItem(product, 1))
        {
            Inventory.inventory.AddItemToInventory(product, 1);
        } else
        {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Instantiate(ItemIDManager.instance.GetItem(product), player.position, Quaternion.identity);
        }
    }

}
