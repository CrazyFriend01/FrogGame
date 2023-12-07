using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttack : MonoBehaviour
{
    public GameObject patrolA;
    public GameObject patrolB;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;
    
    public float speed;
    public float verticalSpeed;
    public GameObject upperBound;
    public GameObject lowerBound;

    private float uBSinCoeff;
    private float lBSinCoeff;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        currentPoint = patrolA.transform;
        //animator.SetBool("isRunning", true);
        uBSinCoeff = Math.Abs(upperBound.transform.position.y - patrolA.transform.position.y);
        lBSinCoeff = Math.Abs(patrolA.transform.position.y - lowerBound.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == patrolB.transform)
        {
            rb.velocity = new Vector2(speed, CalculateSin(patrolB.transform.position.x - transform.position.x));
        }
        else
        {
            rb.velocity = new Vector2(-speed, CalculateSin(transform.position.x - patrolA.transform.position.x));
        }

        ChangeDirection(patrolA.transform, patrolB.transform);
        ChangeDirection(patrolB.transform, patrolA.transform);
        
        //if ((Vector2.Distance(transform.position, currentPoint.position) < 0.5f) && currentPoint == patrolB.transform)
        //{
        //    Flip();
        //    currentPoint = patrolA.transform;
        //}
        //if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == patrolA.transform)
        //{
        //    Flip();
        //    currentPoint = patrolB.transform;
        //}
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void ChangeDirection(Transform curDestination, Transform newDestination)
    {
        if (Math.Abs(transform.position.x - currentPoint.position.x) < 0.5f && currentPoint == curDestination)
        {
            Flip();
            currentPoint = newDestination;
        }
    }

    private float CalculateSin(float x)
    {
        var sin = Mathf.Sin(x);

        return sin;// * sin < 0 ? lBSinCoeff : uBSinCoeff;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(patrolA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(patrolB.transform.position, 0.5f);
        Gizmos.DrawLine(patrolA.transform.position, patrolB.transform.position);
        Gizmos.DrawLine(
            new Vector3(
                patrolA.transform.position.x, upperBound.transform.position.y, patrolA.transform.position.z), 
            new Vector3(
                patrolB.transform.position.x, upperBound.transform.position.y, patrolB.transform.position.z));
        Gizmos.DrawLine(
            new Vector3(
                patrolA.transform.position.x, lowerBound.transform.position.y, patrolA.transform.position.z),
            new Vector3(
                patrolB.transform.position.x, lowerBound.transform.position.y, patrolB.transform.position.z));
    }

}
