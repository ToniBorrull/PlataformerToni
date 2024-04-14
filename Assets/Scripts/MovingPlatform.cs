using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform A;
    public Transform B;
    public float speed = 2f;

    private Vector3 nextPos;

    private void Start()
    {
        nextPos = B.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
        if(transform.position == nextPos)
        {
            nextPos = (nextPos == A.position) ? B.position : A.position;
        }
    }
}
