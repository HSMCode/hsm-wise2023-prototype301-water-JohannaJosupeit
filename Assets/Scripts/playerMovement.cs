using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// this script moves the orca and starts the game
public class playerMovement : MonoBehaviour
{
    private int score;
    public static float exactScore;

    [SerializeField] private float speed = 5f;
   
    private bool moveUp = true;
    private Text scoreText;
    public static bool isAlive, isStarted;
    


    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
        isAlive = true;
        score = 0;
        exactScore = 0;
        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }
   
    // Update is called once per frame
    void Update()
    {
        // round the score to a whole number
        score = Mathf.RoundToInt(exactScore);
        // and update the score text 
        scoreText.text = "Score:" + exactScore;


     // (RE)STARTING THE GAME

        // if r is pressed and the player is dead
        if (Input.GetKeyDown(KeyCode.Space) && !isAlive)
        {
            // reload to scene to restart the game
            SceneManager.LoadScene("Level_0");
        }
        // if space is pressed and the game hasnt been started yet
        else if (Input.GetButtonDown("Jump") && !isStarted)
        {
            // start the game
           isStarted = true;
        }
     // MOVEMENT
        
        // if space  is pressed and the game has been started already
        else if (Input.GetButtonDown("Jump"))
        {
            // Change the direction
            moveUp = !moveUp;
        }

        // if the game has been started and the player is alive 
        if (isStarted && isAlive)
        {
            // see line 72
            MoveObject();
        }
      
    }

    // moves the player when called
    void MoveObject()
    {
        // Switch between 2 floats depending on the bool "moveUp"
        float moveDirection = moveUp ? 1f : -1f;
        // Calculate movement in local space
        float translation = moveDirection * speed * Time.deltaTime;

        // Move the object along the y-axis
        transform.Translate(new Vector3(0f, translation, 0f));

        // Stop at -1 or 1 on the y-axis
        Vector3 clampedPosition = transform.position;
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -1.5f, 1.5f);
        transform.position = clampedPosition;
    }
}
