using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    public AudioSource _audio;
    public AudioClip _missionComplete;

    public static MusicController Main { get; set; }

    public void Awake()
    {
        if (Main != null)
        {
            print("Two music controllers!");
        }
        else
        {
            Main = this;
        }
    }

    public static void PlayMissionComplete()
    {
        Main._audio.PlayOneShot(Main._missionComplete);
    }
}
