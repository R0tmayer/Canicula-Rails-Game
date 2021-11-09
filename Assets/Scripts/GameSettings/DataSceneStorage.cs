using System;
using UnityEngine;
using UnityEngine.UI;

public class DataSceneStorage : MonoBehaviour
{
    [SerializeField] private AEnemy[] _pull;
    [SerializeField] private Waypoint[] _waypoints;
    
    [Header("Medals")]
    [SerializeField] private Sprite _noMedal;
    [SerializeField] private float _noGoal;
    [Space(5)]
    [SerializeField] private Sprite _bronzeMedal;
    [SerializeField] private float _bronzeGoal;
    [Space(5)]
    [SerializeField] private Sprite _silverMedal;
    [SerializeField] private float _silverGoal;    
    [Space(5)]
    [SerializeField] private Sprite _goldMedal;
    [SerializeField] private float _goldGoal;
    
    [Header("Bonuses")]
    [SerializeField] private float _healthBonus;
    [SerializeField] private float _accuracyBonus;
    [SerializeField] private float _completionBonus;

    private float _index;

    public event Action LastWaypointReached;

    public AEnemy[] Pull
    {
        get => _pull;
    }

    public Waypoint NextWaypoint
    {
        get
        {
            _index++;

            if (_index == _waypoints.Length)
            {
                LastWaypointReached?.Invoke();
                return null;
            }

            return _waypoints[(int)_index];
        }
    }

    public float CurrentWaypointIndex => _index;

    public Waypoint FirstWaypoint
    {
        get => _waypoints[0];
    }

    public float WaypointsCount
    {
        get => _waypoints.Length;
    }

    public Waypoint[] AllWaypointsOnScene
    {
        get => _waypoints;
    }

    //Bonuses properties
    public float HealthBonus => _healthBonus;
    public float AccuracyBonus => _accuracyBonus;
    public float CompletionBonus => _completionBonus;
    
    // Goals properties
    public float NoGoal => _noGoal;
    public float BronzeGoal => _bronzeGoal;
    public float SilverGoal => _silverGoal;
    public float GoldGoal => _goldGoal;
    
    //Medals properties
    
    public Sprite NoMedal => _noMedal;
    public Sprite BronzeMedal => _bronzeMedal;
    public Sprite SilverMedal => _silverMedal;
    public Sprite GoldMedal => _goldMedal;
}