using System.Collections;
using UnityEngine;

public class SlowMotion : MonoBehaviour
{
    [SerializeField] private float _targetSpeed;
    [SerializeField] private float _targetSpeedChange;
    [SerializeField] private float _endTime;

    public void StartSlowMotion()
    {
        StartCoroutine(SlowCoroutine());
    }

    private IEnumerator SlowCoroutine()
    {
        // Slow down the game
        while (Mathf.Abs(Time.timeScale - _targetSpeed) > 0.05f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, _targetSpeed, Time.unscaledDeltaTime * _targetSpeedChange);

            // Wait a little so the effect is animated
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // Set the final slowdown speed
        Time.timeScale = _targetSpeed;

        // Wait until the effect needs to be reverted
        yield return new WaitForSecondsRealtime(_endTime);

        // Speed the game back up to normal
        while (Mathf.Abs(Time.timeScale) < 9.95f)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1, Time.unscaledDeltaTime * _targetSpeedChange);

            // Wait a little so the effect is animated
            yield return new WaitForSeconds(Time.deltaTime);
        }

        // Set the final normal speed
        Time.timeScale = 1;
    }

    // If the object is destroyed before the effect is over, return to normal speed
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
