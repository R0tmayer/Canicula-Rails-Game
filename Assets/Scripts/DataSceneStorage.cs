using System;
using UnityEngine;

public class DataSceneStorage : MonoBehaviour
{
    [SerializeField] private Enemy[] _pull;
    [SerializeField] private Waypoint[] _waypointsOnScene;
    private int _index;

    public Enemy[] Pull
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
                return _waypointsOnScene[_index];
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }

    public Waypoint FirstWaypoint
    {
        get => _waypointsOnScene[0];
    }

    public Waypoint[] WaypointsOnScene
    {
        get => _waypointsOnScene;
    }
}