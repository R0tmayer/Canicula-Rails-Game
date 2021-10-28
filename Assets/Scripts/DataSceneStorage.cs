using System;
using UnityEngine;

public class DataSceneStorage : MonoBehaviour
{
    [SerializeField] private AEnemy[] _pull;
    [SerializeField] private Waypoint[] _waypoints;
    private int _index;

    public AEnemy[] Pull
    {
        get => _pull;
    }

    public Waypoint NextWaypoint
    {
        get
        {
            _index++;

            try
            {
                return _waypoints[_index];
            }
            catch (IndexOutOfRangeException)
            {
                throw new IndexOutOfRangeException();
            }
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