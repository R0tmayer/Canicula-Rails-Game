using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(AudioSource))]
public class HurtDecalDisplayer : MonoBehaviour
{
    [SerializeField] private Image _hurtDecal;
    [SerializeField] private AudioSource _hurtSoundSource;
    [SerializeField] private float _imageLifeTime;
    [SerializeField] private float _timeToNextImage;

    private PlayerLife _player;
    private float _timer;

    private void Awake()
    {
        _player = FindObjectOfType<PlayerLife>();
    }

    private void OnEnable()
    {
        _player.Hitted += OnPlayerHitted;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
    }

    private void OnDisable()
    {
        _player.Hitted -= OnPlayerHitted;
    }

    private void OnPlayerHitted()
    {
        if (_timer >= _timeToNextImage)
        {
            StartCoroutine(DisplayDecal());
            _timer = 0;
        }
        
        PlayHurtSound();
    }

    private IEnumerator DisplayDecal()
    {
        Image image = Instantiate(_hurtDecal, transform);
        image.gameObject.SetActive(true);
        var randomPosition = new Vector2(Random.Range(-300, 300), Random.Range(-200, 200));
        image.GetComponent<RectTransform>().anchoredPosition = randomPosition;
        image.GetComponent<RectTransform>().rotation = Quaternion.Euler(Vector3.forward * Random.Range(0, 360));
        yield return new WaitForSeconds(_imageLifeTime);
        image.gameObject.SetActive(false);

    }

    private void PlayHurtSound()
    {
        _hurtSoundSource.pitch = Random.Range(0.7f, 1);
        _hurtSoundSource.Play();
    }
}
