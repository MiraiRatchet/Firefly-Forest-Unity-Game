using UnityEngine;

public class EvilBugBehavior : MonoBehaviour
{

    public int lifes;

    public int speed;

    public float radiusOfExistingMin;

    public float radiusOfExistingMax;

    public float radiusOfAttack;

    private GameObject player;
    Transform playerTransform;

    private Transform target;

    private GameManager gameManager;
    private AudioManager audioManager;


    Vector3 newPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();

        newPosition = generatePosition();
    }

    void Update()
    {
        Vector3 playerPos = playerTransform.position;
        float distance = (transform.position - playerPos).magnitude;
        if (distance > radiusOfExistingMax)
        {
            float R = Random.Range(radiusOfExistingMin, radiusOfExistingMax);
            float angle = Random.Range(0, 2 * 3.14f);
            transform.position = playerPos + new Vector3(R * Mathf.Cos(angle), R * Mathf.Sin(angle), 0);
            newPosition = generatePosition();
        }
        else
        {
            if(distance < radiusOfAttack && gameManager.my_bugs.Count > 0)
            {
                target = gameManager.my_bugs[0].GetComponent<Transform>();


                //if != null
                //BadBug is moving to his victum
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                newPosition = generatePosition();
            }
            else
            {
                if (transform.position != newPosition)
                    transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
                else newPosition = generatePosition();
            }
        }      
    }

    Vector3 generatePosition()
    {
        float radius = Random.Range(0f, 2.9f);
        float angle = Random.Range(0f, 2 * Mathf.PI);
        return transform.position + new Vector3(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle), 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "GoodBug")
        {
            //Instantiate(effect, transform.position, transform.rotation);
            lifes -= 1;
            //audioManager.Play("DeathOfTheGoodBug");
            if (lifes == 0)
            {

                gameManager.spawnEvilBug();
                //������� �� ����
                gameManager.evil_bugs.Remove(this.gameObject);
                this.enabled = false;
                Destroy(this.gameObject);
            }



        }
    }

}
