using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hover;
    public AudioClip click;
    public AudioClip murmur;
    public AudioClip mör;
    public AudioClip murimuri;
    int mur;

    public void HoverSound()
    {
        audioSource.PlayOneShot(hover);
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(click);
    }
    public void bearSound()
    {
        mur = Random.Range(1, 4);

        switch (mur)
        {
            case 1:
                audioSource.PlayOneShot(mör);
                break;
            case 2:
                audioSource.PlayOneShot(murmur);
                break;
            case 3:
                audioSource.PlayOneShot(murimuri);
                break;
            default:
                audioSource.PlayOneShot(murimuri);
                break;
        }
    }
}
