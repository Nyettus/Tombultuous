using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/SoundCard")]
public class SoundCard : ScriptableObject
{
    public AudioClip[] sounds;
    [Range(0, 3)] public float minPitch = 0.8f;
    [Range(0, 3)] public float maxPitch = 1.2f;
    [Range(0, 1)] public float volume = 1f;
    public void PlayRandomOneShot(AudioSource sound)
    {
        //sound.volume = volume;
        AudioClip randClip = sounds[Random.Range(0, sounds.Length)];
        sound.pitch = Random.Range(minPitch, maxPitch);
        sound.PlayOneShot(randClip, volume);
    }

}

public class SFXSource : MonoBehaviour
{
    internal AudioSource sound;
    protected virtual void Awake()
    {
        sound = GetComponent<AudioSource>();
    }
    public virtual void PlayRandomOneShot(SoundCard card)
    {
        card.PlayRandomOneShot(sound);
    }
    //public float pitchMultiplier { get; }
    //public void Play(ISoundAble actor)
    //{
    //    actor.sound.volume = volume;
    //    actor.sound.clip = sounds[Random.Range(0, sounds.Length)];
    //    actor.sound.pitch = Random.Range(minPitch, maxPitch)/* * actor.pitchMultiplier*/;
    //    actor.sound.Play();
    //}
    //public void PlayOneShot(ISoundAble actor)
    //{
    //    actor.sound.volume = volume;
    //    actor.sound.pitch = Random.Range(minPitch, maxPitch);
    //    actor.sound.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    //}
    //public void PlayAfterFinish(ISoundAble actor)
    //{
    //    if (actor.sound.isPlaying) return;
    //    Play(actor);
    //}

    //public void SourcePlayOneShot(AudioSource source)
    //{
    //    source.volume = volume;
    //    source.clip = sounds[Random.Range(0, sounds.Length)];
    //    source.pitch = Random.Range(minPitch, maxPitch);
    //    source.PlayOneShot(source.clip);
    //}
    //public void SourcePlay(AudioSource sound)
    //{
    //    sound.volume = volume;
    //    sound.clip = sounds[Random.Range(0, sounds.Length)];
    //    sound.pitch = Random.Range(minPitch, maxPitch);
    //    sound.Play();
    //}
}