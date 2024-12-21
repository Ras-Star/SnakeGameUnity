using System.Collections;
using UnityEngine;

public class FeedingMechanism : MonoBehaviour
{
    public GameObject foodPrefab;         // Prefab for regular food.
    public GameObject bonusFoodPrefab;    // Prefab for bonus food.
    public SnakeBehaviour snakeBehaviour;  // Reference to SnakeController to track food eaten.

    void Start()
    {
        SpawnFood();
    }

    // Spawn food at a random position.
    public void SpawnFood()
    {
        // If bonus food should appear, spawn bonus food.
        if (snakeBehaviour.foodEaten % 10 == 0 && snakeBehaviour.foodEaten != 0)
        {
            SpawnBonusFood();
        }
        else
        {
            // Regular food spawn.
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            Instantiate(foodPrefab, spawnPosition, Quaternion.identity);
        }
    }

    // Spawn bonus food.
    private void SpawnBonusFood()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        GameObject bonusFood = Instantiate(bonusFoodPrefab, spawnPosition, Quaternion.identity);
        Destroy(bonusFood, 7f);  // Destroy bonus food after 7 seconds.
    }
}
