using UnityEngine;

public class FirstAidKit : MonoBehaviour, ICollectable
{
    private GameDifficult _gameDifficultInstance;
    protected GameSettingsSO currentDifficult;
    
    private float _healingValue;

    private void STart()
    {
        _gameDifficultInstance = FindObjectOfType<GameDifficult>();
        currentDifficult = _gameDifficultInstance.CurrentDifficult;
        
        _healingValue = currentDifficult.FirstAidKitHeal;
    }

    public void Collect(APlayer player)
    {
        player.Heal(_healingValue);
        gameObject.SetActive(false);
    }
}
