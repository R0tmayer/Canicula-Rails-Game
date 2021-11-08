using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class StartingMessage : MonoBehaviour
{
    private CanvasGroup _startingMessageCanvasGroup;

    private void Awake()
    {
        _startingMessageCanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        StartCoroutine(FadeOutStartingMessage());
    }

    private IEnumerator FadeOutStartingMessage()
    {
        var startGameDelay = 2;
        
        yield return new WaitForSeconds(startGameDelay);

        while (_startingMessageCanvasGroup.alpha > 0)
        {
            _startingMessageCanvasGroup.alpha -= Time.deltaTime;
            yield return null;
        }
    }
}
