using DG.Tweening.Core.Easing;
using System;
using UnityEngine;

public class BossItem : Coin
{
    internal void Inicialize(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            WinWindow gameManager = GameObject.FindFirstObjectByType<WinWindow>();
            if (gameManager != null)
            {
                gameManager.ShowWinPanel();
            }
        }
    }
}
