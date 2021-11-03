using UnityEngine;

[CreateAssetMenu]
public class GameSettingsSO : ScriptableObject
{
    [Header("Player")]
    [SerializeField] private float _playerMaxHealth;
    [SerializeField] private float _playerDamage;
    [SerializeField] private float _playerMoveSpeed;
    [SerializeField] private float _playerRotateSpeed;
    [SerializeField] private float _nextWaypointDelay;

    [Header("Bots")]
    [SerializeField] private float _botsStartDelay;
    [SerializeField] private float _botsMaxHealth;
    [SerializeField] private float _botsDamage;
    [SerializeField] private float _botsTimeDeactivate;
    
    [Header("Range")]
    [SerializeField] private float _rangeMoveSpeed;
    [SerializeField] private float _rangeMovePeriod;
    [SerializeField] private float _rangeShotPeriod;
    
    [Header("Melee")]
    [SerializeField] private float _meleeMoveSpeed;
    [SerializeField] private float _meleeStopDistance;

    [Header("Props")]
    [SerializeField] private float _barrelMaxHealth;
    [SerializeField] private float _barrelDamage;
    [SerializeField] private float _barrelRadius;
    
    [Space(5)]
    [SerializeField] private float _firstAidKitHeal;
    

    public float PlayerMaxHealth => _playerMaxHealth;
    public float PlayerDamage => _playerDamage;
    public float PlayerMoveSpeed => _playerMoveSpeed;
    public float PlayerRotateSpeed => _playerRotateSpeed;
    public float nextWaypointDelay => _nextWaypointDelay;

    public float BotsStartDelay => _botsStartDelay;
    public float BotsMaxHealth => _botsMaxHealth;
    public float BotsDamage => _botsDamage;
    public float BotsTimeDeactivate => _botsTimeDeactivate;
    
    public float RangeMoveSpeed => _rangeMoveSpeed;
    public float RangeMovePeriod => _rangeMovePeriod;
    public float RangeShotPeriod => _rangeShotPeriod;
    
    public float MeleeMoveSpeed => _meleeMoveSpeed;
    public float MeleeStopDistance => _meleeStopDistance;

    public float BarrelMaxHealth => _barrelMaxHealth;
    public float BarrelDamage => _barrelDamage;
    public float BarrelRadius => _barrelRadius;

    public float FirstAidKitHeal => _firstAidKitHeal;

}
