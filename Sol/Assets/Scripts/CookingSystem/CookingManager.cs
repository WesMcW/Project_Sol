using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    public static CookingManager CM;
    [Header("Cooking Zone Game")]
    [SerializeField] GameObject cookingGame;
    ZoneGame zoneGame;
    float theResult;
    private void Awake()
    {
        zoneGame = cookingGame.GetComponent<ZoneGame>();
        cookingGame.SetActive(false);
        if(CM == null)
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
    public void StartCooking()
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
        print("The quality of food is: " + theResult);
        zoneGame.ResetGame();
        InteractionManager.IM.StopInteraction();
    }

}
