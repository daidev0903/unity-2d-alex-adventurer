using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorgothBehaviourScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D enemyRigidBody2D;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider[] colliders;

    void Start()
    {
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponentsInChildren<CapsuleCollider>();
    }

    void Update()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = (i == spriteRenderer.sprite.textureRect.x);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Call when enemy go out of the box collider
        float newScaleX = -(Mathf.Sign(enemyRigidBody2D.velocity.x));
        Vector3 currentScale = transform.localScale;
        transform.localScale = new Vector3(newScaleX, currentScale.y, currentScale.z);
    }
}
