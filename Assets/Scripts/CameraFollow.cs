using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    public float smoothSpeed = 0.5f;
    Vector3 offset=new Vector3(0,0,-10);
    Vector3 desiredPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.position = player.position + offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player.gameObject.activeSelf)
        {
            desiredPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        }
    }
}
