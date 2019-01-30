using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _clips;

    private IEnumerator _playRoutine;


    public void Play()
    {
        _audioSource.clip = _clips[Random.Range(0, _clips.Count)];
        _audioSource.Play();
    }

    public void PlayRepeating()
    {
        if(_playRoutine != null)
            StopCoroutine(_playRoutine);

        _playRoutine = Repeat();
        StartCoroutine(_playRoutine);
    }

    private IEnumerator Repeat()
    {
        while (true)
        {
            // Get clip
            _audioSource.clip = _clips[Random.Range(0, _clips.Count)];

            // Get clip time
            var time = _audioSource.clip.length;

            // Play
            _audioSource.Play();

            // Wait for sfx to finish
            yield return new WaitForSeconds(time);
        }

        yield return null;
    }

    public void StopRepeating()
    {
        if(_playRoutine != null)
            StopCoroutine(_playRoutine);
    }

}
