using System;
using UnityEngine;

public class DataSceneStorage : MonoBehaviour
{
    [SerializeField] private AEnemy[] _pull;
    [SerializeField] private Waypoint[] _waypoints;
    private int _index;

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

            if(_index == _waypoints.Length)
            {
                LastWaypointReached?.Invoke();
                return null;
            }

            return _waypoints[_index];

        }
    }

    public Waypoint FirstWaypoint
    {
        get => _waypoints[0];
    }

    public Waypoint[] AllWaypointsOnScene
    {
        get => _waypoints;
    }
}