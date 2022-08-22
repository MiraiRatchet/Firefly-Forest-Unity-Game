using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBugBehavior : MonoBehaviour
{

    float speed = 3f;

    private GameObject player;

    private Transform playerTransform;

    public float radiusOfExistingMin;
    public float radiusOfExistingMax;

    private GameManager gameManager;
    private AudioManager audioManager;
    Vector3 newPosition;


    private bool isFollowingPlayer = false;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
        //newPosition=generatePosition();
    }

    Vector3 generatePosition(){
        float radius = Random.Range(0f, 2.9f);
        float angle = Random.Range(0f, Mathf.PI);
        return playerTransform.position+new Vector3(radius*Mathf.Cos(angle),radius*Mathf.Sin(angle),0);
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = playerTransform.position;
        float distance = (transform.position - playerPos).magnitude;
        if (distance > radiusOfExistingMax && !isFollowingPlayer)
        {
            float R = Random.Range(radiusOfExistingMin, radiusOfExistingMax);
            float angle = Random.Range(0, 2 * 3.14f);
            transform.position = playerPos + new Vector3(R * Mathf.Cos(angle), R * Mathf.Sin(angle), 0);
        }
        else
        {
            if (!isFollowingPlayer && (playerTransform.position - transform.position).magnitude < 2)
            {
                isFollowingPlayer = true;
                gameManager.my_bugs.Add(this.gameObject);
                newPosition = generatePosition();
                if (gameManager.my_bugs.Count > gameManager.numberOfBugsToWin)
                {
                    Debug.Log("Win!!!");

                }
            }
            if (!isFollowingPlayer)
            {

            }
            else
            {
                if (transform.position != newPosition)
                    transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
                else newPosition = generatePosition();
            }
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "EvilBug")
        {


            gameManager.spawnGoodBug();

            gameManager.my_bugs.Remove(this.gameObject);
            this.enabled = false;
            Destroy(this.gameObject);

            

        }
    }

}
