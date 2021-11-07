using System.Collections;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    private GameDifficult _gameDifficultInstance;
    private GameSettingsSO currentDifficult;

    private float _moveSpeed;
    private float _rotateSpeed;
    private float _nextWaypointDelay;

    private Waypoint _currentWaypoint;
    private IEnumerator _moveWaypointCoroutine;
    [SerializeField] private AnimationController animationController;

    private DataSceneStorage _dataSceneStorage;

    protected void Start()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        currentDifficult = _gameDifficultInstance.CurrentDifficult;

        _dataSceneStorage = FindObjectOfType<DataSceneStorage>();
        _currentWaypoint = _dataSceneStorage.FirstWaypoint;


        _moveSpeed = currentDifficult.PlayerMoveSpeed;
        _rotateSpeed = currentDifficult.PlayerRotateSpeed;
        _nextWaypointDelay = currentDifficult.nextWaypointDelay;


        foreach (var waypoint in _dataSceneStorage.AllWaypointsOnScene)
        {
            waypoint.AllEnemiesDied += OnAllEnemiesDied;
        }

        _moveWaypointCoroutine = MoveToWaypoint();
        StartCoroutine(_moveWaypointCoroutine);
    }

    private IEnumerator MoveToWaypoint()
    {
        yield return new WaitForSeconds(_nextWaypointDelay);
        animationController.PlayWalkAnimation();

        Transform waypointTransform = _currentWaypoint.transform;
        
        Vector3 waypointPosition = waypointTransform.position;
        Quaternion waypointRotation = waypointTransform.rotation;

        while (Vector3.Distance(transform.position, waypointPosition) > 0.5f ||
               Quaternion.Angle(transform.rotation, waypointRotation) > 0.5f)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypointPosition, _moveSpeed * Time.deltaTime);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, waypointRotation,
                _rotateSpeed * Time.deltaTime);

            yield return null;
        }

        _currentWaypoint.ActivateEnemies();
        animationController.PlayIdleAnimation();
    }

    private void OnAllEnemiesDied()
    {
        _currentWaypoint.AllEnemiesDied -= OnAllEnemiesDied;
        _currentWaypoint = _dataSceneStorage.NextWaypoint;

        if (_moveWaypointCoroutine == null)
        {
            Debug.Log("MoveWaypointCoroutine is Null");
            return;
        }
        
        StopCoroutine(_moveWaypointCoroutine);
        _moveWaypointCoroutine = MoveToWaypoint();
        StartCoroutine(_moveWaypointCoroutine);
    }


}