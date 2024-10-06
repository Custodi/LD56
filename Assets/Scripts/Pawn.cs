using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    public Action<Pawn> OnPawnDestroyed;

    public virtual void DestroyPawn()
    {

    }
}
