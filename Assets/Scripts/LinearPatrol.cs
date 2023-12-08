using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPatrol : MonoBehaviour
{
    public GameObject patrolA;
    public GameObject patrolB;
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        currentPoint = patrolB.transform;
        //animator.SetBool("isRunning", true);

        Flip();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == patrolB.transform)
        {    
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if ((Vector2.Distance(transform.position, currentPoint.position) < 0.5f) && currentPoint == patrolB.transform)
        {
            Flip();
            currentPoint = patrolA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == patrolA.transform)
        {
            Flip();
            currentPoint = patrolB.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.DrawWireSphere(patrolA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(patrolB.transform.position, 0.5f);
        Gizmos.DrawLine(patrolA.transform.position, patrolB.transform.position);
    }

}
