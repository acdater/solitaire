using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetItem : MonoBehaviour
{
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    public void TriggerReset()
    {
        _canvasManager.ResetAsideToMain();
        _audioSource.PlayOneShot(_audioClip);
    }
}
