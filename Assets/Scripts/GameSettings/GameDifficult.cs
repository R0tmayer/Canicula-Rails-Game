using System;
using UnityEngine;

public class GameDifficult : MonoBehaviour
{
    public enum Difficult
    {
        Easy,
        Normal,
        Hard,
        Custom
    }

    [SerializeField] private Difficult _difficultType;
    
    [SerializeField] private GameSettingsSO _easyDifficult;
    [SerializeField] private GameSettingsSO _normalDifficult;
    [SerializeField] private GameSettingsSO _hardDifficult;
    [SerializeField] private GameSettingsSO _customDifficult;

    public GameSettingsSO CurrentDifficult { get; private set; }

    private void Awake()
    {
        switch (_difficultType)
        {
            case Difficult.Easy:
                CurrentDifficult = _easyDifficult;
                break;
            case Difficult.Normal:
                CurrentDifficult = _normalDifficult;
                break;
            case Difficult.Hard:
                CurrentDifficult = _hardDifficult;
                break;
            case Difficult.Custom:
                CurrentDifficult = _customDifficult;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
