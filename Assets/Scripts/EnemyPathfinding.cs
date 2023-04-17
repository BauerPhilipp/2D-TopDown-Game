using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{

    [SerializeField] float moveSpeed = 2f;
    Vector2 moveDir;
    Rigidbody2D rb;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        EnemyRoaming();
    }

    private void EnemyRoaming()
    {
        rb.MovePosition(rb.position + moveDir * Time.fixedDeltaTime * moveSpeed);
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;
    }


}
