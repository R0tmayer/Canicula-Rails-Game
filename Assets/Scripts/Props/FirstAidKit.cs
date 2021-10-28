using UnityEngine;

public class FirstAidKit : MonoBehaviour, ICollectable
{
    [SerializeField] private int _healValue;
    
    public void Collect(APlayer player)
    {
        player.Heal(_healValue);
        gameObject.SetActive(false);
    }
}
