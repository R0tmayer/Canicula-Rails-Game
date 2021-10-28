using System.Collections;
using UnityEngine;

public class PlayerMover : APlayer
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Waypoint _currentWaypoint;
    private IEnumerator _moveWaypointCoroutine;

    private DataSceneStorage _dataSceneStorage;

    private void Start()
    {
        _dataSceneStorage = FindObjectOfType<DataSceneStorage>();

        _currentWaypoint = _dataSceneStorage.FirstWaypoint;
        
        foreach (var waypoint in _dataSceneStorage.AllWaypointsOnScene)
        {
                waypoint.AllEnemiesDied += OnAllEnemiesDied;
        }

        _moveWaypointCoroutine = MoveToWaypoint();
        StartCoroutine(_moveWaypointCoroutine);
    }

    private void OnDisable()
    {
    }

    private IEnumerator MoveToWaypoint()
    {
        Vector3 waypointPosition = _currentWaypoint.transform.position;
        Quaternion waypointRotation = _currentWaypoint.transform.rotation;

        while (Vector3.Distance(transform.position, waypointPosition) > 0.5f ||
               Quaternion.Angle(transform.rotation, waypointRotation) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypointPosition, _moveSpeed * Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, waypointRotation,
                _rotateSpeed * Time.deltaTime);

            yield return null;
        }

        _currentWaypoint.ActivateEnemies();
    }

    private void OnAllEnemiesDied()
    {
        _currentWaypoint.AllEnemiesDied -= OnAllEnemiesDied;
        _currentWaypoint = _dataSceneStorage.NextWaypoint;

        if (_moveWaypointCoroutine != null)
        {
            StopCoroutine(_moveWaypointCoroutine);
            _moveWaypointCoroutine = MoveToWaypoint();
            StartCoroutine(_moveWaypointCoroutine);
        }
    }
}