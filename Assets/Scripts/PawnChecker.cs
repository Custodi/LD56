using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PawnChecker : MonoBehaviour
{
    private int _initialAvoidPawns = 0;
    private int _initialAwayPawns = 0;
    private int _initialMagnetPawns = 0;

    private int _currentAvoidPawns = 0;
    private int _currentAwayPawns = 0;
    private int _currentMagnetPawns = 0;

    public Action OnPawnAmountUpdate;

    //[SerializeField]
    private List<Pawn> _pawns = new List<Pawn>();

    public int CurrentAvoidPawns { get => _currentAvoidPawns; set { _currentAvoidPawns = value; OnPawnAmountUpdate?.Invoke(); } }
    public int CurrentAwayPawns { get => _currentAwayPawns; set { _currentAwayPawns = value; OnPawnAmountUpdate?.Invoke(); } }
    public int CurrentMagnetPawns { get => _currentMagnetPawns; set { _currentMagnetPawns = value; OnPawnAmountUpdate?.Invoke(); } }

    private void OnEnable()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            _pawns.Add(transform.GetChild(i).GetComponentInChildren<Pawn>());
        }

        foreach(var pawn in _pawns)
        {
            if (pawn is AvoidPawn)
            {
                _initialAvoidPawns++;
            }
            else if (pawn is AwayPawn)
            {
                _initialAwayPawns++;
            }
            else if (pawn is MagnetPawn)
            {
                _initialMagnetPawns++;
            }

            pawn.OnPawnDestroyed += OnPawnDeath;
        }

        CurrentAvoidPawns = _initialAvoidPawns;
        CurrentAwayPawns = _initialAwayPawns;
        CurrentMagnetPawns = _initialMagnetPawns;
    }

    private void OnPawnDeath(Pawn pawn)
    {
        if (pawn is AvoidPawn)
        {
            CurrentAvoidPawns--;
        }
        else if (pawn is AwayPawn)
        {
            CurrentAwayPawns--;
        }
        else if(pawn is MagnetPawn)
        {
            CurrentMagnetPawns--;
        }
        else
        {
            Debug.Log($"Error - No type of pawn");
        }
    }
}
