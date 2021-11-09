using System;
using UnityEngine;
using UnityEngine.UI;

public class GameDifficult : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdown;

    [SerializeField] private GameSettingsSO _easyDifficult;
    [SerializeField] private GameSettingsSO _normalDifficult;
    [SerializeField] private GameSettingsSO _hardDifficult;

    public GameSettingsSO CurrentDifficult { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        CurrentDifficult = _easyDifficult;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 100;
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(delegate { DropdownValueChanged(_dropdown); });
    }

    //Ouput the new value of the Dropdown into Text
    void DropdownValueChanged(Dropdown dropdown)
    {
        switch (dropdown.value)
        {
            case 0:
                CurrentDifficult = _easyDifficult;
                break;
            case 1:
                CurrentDifficult = _normalDifficult;
                break;
            case 2:
                CurrentDifficult = _hardDifficult;
                break;
        }
    }
}