using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D enemyRigidBody2D;

    void Start()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (IsFacingRight())
            enemyRigidBody2D.velocity = new Vector2(moveSpeed, 0f);
        else
            enemyRigidBody2D.velocity = new Vector2(-moveSpeed, 0f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Call when enemy go out of the box collider
        float newScaleX = -(Mathf.Sign(enemyRigidBody2D.velocity.x));
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(newScaleX, currentScale.y, currentScale.z);
    }
}
