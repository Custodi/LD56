using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPawn : MonoBehaviour
{
    Pawn pawnBuff;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Pawn>(out pawnBuff))
        {
            if (pawnBuff is not AvoidPawn)
            {
                collision.rigidbody.gravityScale = 0;
                collision.rigidbody.velocity = Vector3.zero;
                collision.rigidbody.angularVelocity = 0f;
                pawnBuff.DestroyPawn();
            }
        }
    }
}
