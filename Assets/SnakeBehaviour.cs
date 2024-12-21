using UnityEngine;

public class SnakeBehaviour : MonoBehaviour
{
    public float moveSpeed = 0.1f;            // Speed of the snake's movement.
    public GameObject snakeSegmentPrefab;     // Prefab for snake body segments.
    public List<Transform> snakeBody = new List<Transform>();  // List to hold the snake's body segments.

    private Vector2 moveDirection = Vector2.right;  // Initial movement direction (right).
    private bool isGrowing = false;                 // Whether the snake is growing after eating food.
    private int foodEaten = 0;                     // Counter for the number of food pieces eaten.

    void Start()
    {
        // Initialize the snake's body with a single segment (the head).
        snakeBody.Add(this.transform);  // Add the head as the first segment.
    }

    void Update()
    {
        // Handle player input for snake direction.
        HandleInput();

        // Move the snake.
        MoveSnake();

        // Check if the snake has bitten itself or hit the fence.
        CheckCollisions();
    }

    // Handle the movement input from the player (using arrow keys or WASD).
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = Vector2.right;
        }
    }

    // Move the snake by updating the position of the head and each body segment.
    private void MoveSnake()
    {
        Vector2 newHeadPosition = (Vector2)transform.position + moveDirection * moveSpeed;
        Transform newHead = Instantiate(snakeSegmentPrefab, newHeadPosition, Quaternion.identity).transform;

        // Add the new head to the list of body segments.
        snakeBody.Insert(0, newHead);

        // If the snake isn't growing, remove the last body segment (i.e., the tail).
        if (!isGrowing)
        {
            Destroy(snakeBody[snakeBody.Count - 1].gameObject);
            snakeBody.RemoveAt(snakeBody.Count - 1);
        }
        else
        {
            isGrowing = false;  // Reset growth flag after the snake grows.
        }

        // Move the head to the correct position.
        transform.position = newHeadPosition;
    }

    // Check if the snake has bitten itself or hit the fence.
    private void CheckCollisions()
    {
        // Check if the snake's head touches any of the body segments (self-collision).
        for (int i = 1; i < snakeBody.Count; i++)
        {
            if ((Vector2)transform.position == (Vector2)snakeBody[i].position)
            {
                GameOver();
            }
        }

        // Check if the snake hits the fence (boundary check).
        if (transform.position.x < -9f || transform.position.x > 9f || transform.position.y < -5f || transform.position.y > 5f)
        {
            GameOver();
        }
    }

    // Call this method to make the snake grow.
    public void GrowSnake()
    {
        isGrowing = true;
        foodEaten++;

        if (foodEaten % 10 == 0)
        {
            SpawnBonusFood();  // Spawn bonus food after 10 regular foods.
        }
    }

    // Handle game over by stopping the game.
    private void GameOver()
    {
        Time.timeScale = 0;  // Pause the game.
        Debug.Log("Game Over!");
    }

    // Spawn the bonus food after 10 regular foods are eaten.
    private void SpawnBonusFood()
    {
        // Randomize the position of the bonus food.
        Vector2 bonusPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
        // Spawn bonus food here for 7 seconds (visible for 7 seconds).
        // You can create a separate script for bonus food or handle its behavior here.
    }
}
