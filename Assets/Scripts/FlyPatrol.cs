using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyAttack : MonoBehaviour
{
    public GameObject PatrolA;
    public GameObject PatrolB;
    
    private Rigidbody2D rb;
    private Animator animator;
    private Transform currentPoint;

    public float Speed;
    public float VerticalSpeed;
    public GameObject VerticalBound;

    private float amplitude;
    private float distAB;

    // Start is called before the first frame update
    void Start()
    {
        distAB = Vector3.Distance(PatrolA.transform.position, PatrolB.transform.position);
        amplitude = 2 * (VerticalBound.transform.position.y - PatrolA.transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PatrolA.transform;

        transform.position = new Vector3(transform.position.x, currentPoint.position.y - amplitude / 4, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        amplitude = 2 * (VerticalBound.transform.position.y - PatrolA.transform.position.y);
        Vector2 point = currentPoint.position - transform.position;
        var flyOffset = Mathf.Sin(Time.time * VerticalSpeed)
            * (amplitude);

        if (currentPoint == PatrolB.transform)
        {
            rb.velocity = new Vector2(Speed, flyOffset);
        }
        else
        {
            rb.velocity = new Vector2(-Speed, flyOffset);
        }

        ChangeDirection(PatrolA.transform, PatrolB.transform);
        ChangeDirection(PatrolB.transform, PatrolA.transform);
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void ChangeDirection(Transform curDestination, Transform newDestination)
    {
        //var distEnemyToNew = Vector3.Distance(transform.position, newDestination.position);

        if (//distEnemyToNew - distAB > 0.5f
            //&& transform.position.y < curDestination.position.y
            Math.Abs(transform.position.x - currentPoint.position.x) < 0.5f 
            && currentPoint == curDestination)
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
        var lowerBound = PatrolA.transform.position.y - (VerticalBound.transform.position.y - PatrolA.transform.position.y);
        Gizmos.DrawWireSphere(PatrolA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PatrolB.transform.position, 0.5f);
        Gizmos.DrawLine(PatrolA.transform.position, PatrolB.transform.position);
        Gizmos.DrawLine(
            new Vector3(
                PatrolA.transform.position.x, VerticalBound.transform.position.y, PatrolA.transform.position.z), 
            new Vector3(
                PatrolB.transform.position.x, VerticalBound.transform.position.y, PatrolB.transform.position.z));
        Gizmos.DrawLine(
            new Vector3(
                PatrolA.transform.position.x, lowerBound, PatrolA.transform.position.z),
            new Vector3(
                PatrolB.transform.position.x, lowerBound, PatrolB.transform.position.z));
    }

}
