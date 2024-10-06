using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    Pawn pawn;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Pawn>(out pawn))
        {
            pawn.DestroyPawn();
        }
    }
}
