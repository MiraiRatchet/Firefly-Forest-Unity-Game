using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 movementDirection;
    float movementSpeed = 5f;
    Animator animator;
    bool isMoving;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isMoving = false;
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection.x=Input.GetAxis("Horizontal");
        movementDirection.y=Input.GetAxis("Vertical");
        animator.SetFloat("PlayerSpeed", movementDirection.sqrMagnitude);
        animator.SetFloat("Horizontal", movementDirection.x);
        animator.SetFloat("Vertical", movementDirection.y);
        
        if (movementDirection.magnitude > 0)
        {
            if (isMoving == false)
            {
                isMoving = true;
                audioManager.Play("FootSteps");
            }

        }
        else
        {
            if (isMoving == true)
            {
                isMoving = false;
                audioManager.Stop("FootSteps");

            }


        }
    }

    void FixedUpdate()
    {
        transform.position += new Vector3(movementDirection.x, movementDirection.y,0).normalized*movementSpeed*Time.fixedDeltaTime;
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "FROG")
        {

            Debug.Log("FROG");
            audioManager.Play("FROG");
        }
        else
        {
            if (collider.tag == "Lusha")
            {

                Debug.Log("Lusha");
                //audioManager.Play("Lusha");
            }
            else
            {
                if(collider.tag == "Grass")
                {
                    Debug.Log("Grass");
                    //audioManager.Play("Grass");
                }
            }

        }
    }
}
