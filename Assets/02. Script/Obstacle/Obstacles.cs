using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private float movingamount;
    [SerializeField] private float minPos;
    [SerializeField] private float maxPos;
    [SerializeField] private float movingSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float force;

    private bool isMoveDirection;
    Vector3 nowPos;

    private void Start()
    {
        nowPos = transform.position;

        minPos = nowPos.x - movingamount;
        maxPos = nowPos.x + movingamount;

        movingSpeed = Random.Range(minSpeed, maxSpeed);
    }

    private void Update()
    {
        Moving();
    }

    void Moving()
    {
        if (transform.position.x <= minPos)
        {
            isMoveDirection = false;
        }
        else if (transform.position.x >= maxPos)
        {
            isMoveDirection = true;
        }

        if (isMoveDirection)
        {
            transform.position += Vector3.left * movingSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += Vector3.right * movingSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

            Vector3 pushDirection = (collision.transform.position - transform.position).normalized;

            pushDirection.y = 0;

            rb.AddForce(pushDirection * force, ForceMode.Impulse);
        }
    }
}
