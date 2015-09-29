using UnityEngine;
using System.Collections;

public class RobotMusic : MonoBehaviour
{
    public AudioClip _moveClip;

    public void PLayMoveClip()
    {
        GetComponent<AudioSource>().clip = _moveClip;

        GetComponent<AudioSource>().Play();
    }

    public void StopMoveClip()
    {
        GetComponent<AudioSource>().Stop();
    }

    public bool IsPlaying()
    {
        return GetComponent<AudioSource>().isPlaying;
    }
}
