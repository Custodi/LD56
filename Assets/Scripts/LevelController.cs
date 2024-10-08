using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static LevelController _instance;

    public static LevelController Instance {  get { return _instance; } }

    [SerializeField]
    private PawnChecker _pawnChecker;

    [SerializeField]
    private AwayTargetZone _awayTargetZone;

    [SerializeField]
    private AvoidTargetZone _avoidTargetZone;

    [SerializeField]
    private MagnetTargetZone _magnetTargetZone;

    public Action OnTargetCompleted;

    public Action OnTargetUncompleted;

    private int _availableZones = 0;

    private int _completeZones;

    private void Awake()
    {
        if(Instance == null)
        {
            _instance = this;
        }
        else if(Instance != this)
        {
            var buff = Instance;
            _instance = this;
            Destroy(buff.gameObject);
           
        }
        DontDestroyOnLoad(Instance);

        _pawnChecker = GameObject.Find("Pawns")?.GetComponent<PawnChecker>();
        _awayTargetZone = GameObject.Find("Away target zone")?.GetComponent<AwayTargetZone>();
        _avoidTargetZone = GameObject.Find("Avoid target zone")?.GetComponent<AvoidTargetZone>();
        _magnetTargetZone = GameObject.Find("Magnet target zone")?.GetComponent<MagnetTargetZone>();
    }

    private void OnEnable()
    {
        if (_awayTargetZone != null)
        {
            _availableZones++;
            _awayTargetZone.OnTargetZoneCompleted += OnZoneCompleted;
            _awayTargetZone.OnTargetZoneUncompleted += OnZoneUncompleted;
        }
        if (_avoidTargetZone != null)
        {
            _availableZones++;
            _avoidTargetZone.OnTargetZoneCompleted += OnZoneCompleted;
            _avoidTargetZone.OnTargetZoneUncompleted += OnZoneUncompleted;
        }
        if (_magnetTargetZone != null)
        {
            _availableZones++;
            _magnetTargetZone.OnTargetZoneCompleted += OnZoneCompleted;
            _magnetTargetZone.OnTargetZoneUncompleted += OnZoneUncompleted;
        }
        Debug.Log($"AZ: {_availableZones}");
    }

    private void OnZoneCompleted()
    {
       
        _completeZones++;
        Debug.Log($"CZ:{_completeZones} + AZ:{_availableZones}");
        if (_completeZones == _availableZones)
        {
            Debug.Log($"Level completion");
            OnTargetCompleted?.Invoke();
            _completeZones = 0;
        }
    }

    private void OnZoneUncompleted()
    {
        _completeZones--;
        Debug.Log($"CZ:{_completeZones} + AZ:{_availableZones}");
    }


    public void ReloadlLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

}
