// System.
using System.Collections;
using System.Collections.Generic;
// Unity.
using UnityEngine;
// Gobblefish.
using Gobblefish.Audio;

namespace Gobblefish.Audio {

    [System.Serializable]
    public class MusicController {

        // The audio source that plays the music.
        private List<AudioSource> m_Sources;
        private AudioSource m_Source => m_Sources[0];

        // The name of this thing.
        private string name;

        // The transform to parent things to.
        private Transform transform;

        // The queue time.
        public double nextQueueTime = -1.0;

        // Runs once before the first frame.
        public MusicController(string name, float volume, Transform transform) {
            this.name = name;
            this.transform = transform;
            m_Sources = new List<AudioSource>();
            for (int i = 0; i < 1; i++) {
                m_Sources.Add(GenerateSource());
            }
            nextQueueTime = -1.0;
            SetVolume(volume);
        }

        // Generates the audio source to play the music from.
        private AudioSource GenerateSource() {
            AudioSource audioSource = new GameObject(name + " AudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
            audioSource.transform.SetParent(transform);
            audioSource.transform.localPosition = Vector3.zero;
            audioSource.loop = false;
            audioSource.pitch = 1f;
            return audioSource;
        }

        public void Play(AudioClip audioClip) {
            m_Source.clip = audioClip;
            m_Source.Play();
        }

        public void Queue(AudioClip audioClip) {
            if (audioClip == null) { return; }

            // int sourceIndex = 0;
            //     if (!m_Sources[i].isPlaying) {
            //         sourceIndex
            //     }
            // }
            
            if (nextQueueTime == -1.0) {
                nextQueueTime = UnityEngine.AudioSettings.dspTime;
            }

            double delay = 0.0;
            if (m_Source.clip != null && m_Source.isPlaying) {
                double clipLength = SamplesToDSPTime(m_Source.clip.samples);
                double lengthPlayed = SamplesToDSPTime(m_Source.timeSamples);
                delay = clipLength - lengthPlayed;
            }

            nextQueueTime = nextQueueTime + delay;
            // if (delay < 1.0) {
            m_Source.PlayScheduled(nextQueueTime);
            // }
            // else {
            //     LoadQueuedClip(nextQueueTime);
            // }
            // currTime + delay;
        }

        // public void LoadQueuedClip(double heldQueueTime) {
        //     m_Source.PlayScheduled(heldQueueTime);
        // }

        public void Play() {
            // for (int i = 0; i < m_Sources.Count; i++) {
            //     m_Sources[i].Stop();
            // }
            // if (m_Sources.Count > 0) {
            //     m_Sources[0].Play();
            // }
            m_Source.Play();
        }

        public double SamplesToDSPTime(int samples) {
            return (double)samples / UnityEngine.AudioSettings.outputSampleRate;
        }

        // public bool SameAudio(AudioClip audioClip) {
        //     return m_Source.clip == audioClip;
        // }

        public void Pause() {
            // for (int i = 0; i < m_Sources.Count; i++) {
            //     m_Sources[i].Stop();
            // }
            m_Source.Stop();
        }

        public void Stop() {
            // for (int i = 0; i < m_Sources.Count; i++) {
            //     m_Sources[i].Stop();
            // }
            m_Source.Stop();
        }

        public void SetVolume(float volume) {
            // for (int i = 0; i < m_Sources.Count; i++) {
            //     m_Sources[i].volume = volume;
            // }
            m_Source.volume = volume;
        }

    }
}