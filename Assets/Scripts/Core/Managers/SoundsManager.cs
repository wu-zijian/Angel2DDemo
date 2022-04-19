using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

///<summary>
///音乐音效管理
///</summary>
public class SoundsManager : Singleton<SoundsManager>
{
    public AudioSource audioSource;
    public AudioMixer mainAudioMixer;
    public AudioMixer audioMixer;
    [SerializeField]
    public AudioClip jumpAudio, hurtAudio, eatAudio, deathAudio, gameOverAudio, vectoryAudio, runAudio, shootAudio, fightAudio;
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }

    public void HurtAudio()
    {
        audioSource.clip = hurtAudio;
        audioSource.Play();
    }

    public void EatAudio()
    {
        audioSource.clip = eatAudio;
        audioSource.Play();
    }
    public void DeathAudio()
    {
        audioSource.clip = deathAudio;
        audioSource.Play();
    }
    public void GameOverAudio()
    {
        audioSource.clip = gameOverAudio;
        audioSource.Play();
    }
    public void VectoryAudio()
    {
        audioSource.clip = vectoryAudio;
        audioSource.Play();
    }
    public void RunAudio()
    {
        audioSource.clip = runAudio;
        audioSource.Play();
    }
    public void ShootAudio()
    {
        audioSource.clip = shootAudio;
        audioSource.Play();
    }
    public void FightAudio()
    {
        audioSource.clip = fightAudio;
        audioSource.Play();
    }
}
