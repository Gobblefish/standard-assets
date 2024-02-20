// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Audio;

namespace Gobblefish.Audio {

    public class MusicController : MonoBehaviour {

        // The audio source that plays the music.
        [HideInInspector]
        private AudioSource m_Source;

        // Runs once before the first frame.
        public void Load() {
            if (m_Source != null) { return; }
            m_Source = GenerateSource();
            SetVolume(AudioManager.Settings.musicVolume);
        }

        // Generates the audio source to play the music from.
        private AudioSource GenerateSource() {
            AudioSource audioSource = new GameObject(gameObject.name + " AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            audioSource.transform.SetParent(transform);
            audioSource.transform.localPosition = Vector3.zero;
            audioSource.loop = true;
            audioSource.pitch = 1f;
            return audioSource;
        }

        public void Play(AudioClip audioClip) {
            m_Source.clip = audioClip;
            m_Source.Play();
        }

        public void Play() {
            m_Source.Play();
        }

        public void PlayAtSamePosition(AudioClip audioClip) {
            int delay = m_Source.timeSamples;
            m_Source.clip = audioClip;
            m_Source.Play();
            m_Source.timeSamples = delay;
        }

        public void PlayAtSameBar(AudioClip audioClip, int newBpm) {
            
            float time = m_Source.time;
            float bps = this.bpm /= 60;

            int beatsPerBar = 4;
            float barDuration = bps * beatsPerBar;

            float secondsOverPreviousBar = time % barDuration;
            float secondsUntilEndOfThisBar = barDuration - secondsOverPreviousBar; 
        
            m_Source.clip = audioClip;
            m_Source.Play((ulong)secondsUntilEndOfThisBar);

            this.bpm = newBpm;
        
        }
        private int bpm = 90;


        public void PlayWithDelay(AudioClip audioClip, float delay) {
            m_Source.Stop();
            m_Source.clip = audioClip;
            // m_Source.Play((ulong)(delay * UnityEngine.AudioSettings.outputSampleRate));
            m_Source.PlayDelayed(delay);
        }

        // public void Stop() {
        //     m_Source.Stop();
        // }

        public bool SameAudio(AudioClip audioClip) {
            return m_Source.clip == audioClip;
        }

        public void Pause() {
            m_Source.Stop();
        }

        public void Stop() {
            // m_Source.clip = null;
            m_Source.Stop();
        }

        public void SetVolume(float volume) {
            m_Source.volume = volume;
        }

    }
}