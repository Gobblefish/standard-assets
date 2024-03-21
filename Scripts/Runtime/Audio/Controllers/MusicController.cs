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

        // The queue controls.
        private double nextQueueTime = -1.0;
        private int queueIndex = 0;
        private List<AudioClip> queueClips = new List<AudioClip>();
        private int sourceCurrentIndex => queueIndex % m_Sources.Count;
        private int nextSourceIndex => (queueIndex + 1) % m_Sources.Count;

        // Runs once before the first frame.
        public MusicController(string name, float volume, Transform transform) {
            this.name = name;
            this.transform = transform;
            m_Sources = new List<AudioSource>();
            for (int i = 0; i < 2; i++) {
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
            audioSource.loop = true;
            audioSource.pitch = 1f;
            return audioSource;
        }

        public void Play(AudioClip audioClip) {
            m_Source.clip = audioClip;
            m_Source.PlayScheduled(UnityEngine.AudioSettings.dspTime);
        }

        // Checks the audio queue.
        public void CheckQueue() {
            if (queueClips.Count == 0 || nextQueueTime > UnityEngine.AudioSettings.dspTime) {
                return;
            }

            if (nextQueueTime == -1.0) {
                nextQueueTime = UnityEngine.AudioSettings.dspTime;
            }

            AudioSource currentSource = m_Sources[sourceCurrentIndex];
            currentSource.loop = false;
            
            double timeLeftForEndOfClip = 0d;
            if (currentSource.clip != null && currentSource.isPlaying) {
                double clipLength = currentSource.clip.Length();
                double lengthPlayed = (double)currentSource.timeSamples / currentSource.clip.frequency;
                timeLeftForEndOfClip = clipLength - lengthPlayed;
            }

            if (timeLeftForEndOfClip < 1d) {
                PlayNextInQueue(timeLeftForEndOfClip);
            }

        }

        public void PlayNextInQueue(double delay) {
            // Get the audio source that is next to be used.
            AudioSource queueSource = m_Sources[nextSourceIndex];
            nextQueueTime = UnityEngine.AudioSettings.dspTime + delay;
            // Set the params of that clip.
            queueSource.clip = queueClips[queueIndex % queueClips.Count];
            queueSource.PlayScheduled(nextQueueTime);
            queueIndex += 1;
        }

        public void Queue(AudioClip audioClip) {
            queueClips.Add(audioClip);
        }

        public double SamplesToDSPTime(int samples) {
            return (double)samples / UnityEngine.AudioSettings.outputSampleRate;
        }

        public void Pause() {
            for (int i = 0; i < m_Sources.Count; i++) {
                m_Sources[i].Stop();
            }
            m_Source.Stop();
        }

        public void Stop() {
            for (int i = 0; i < m_Sources.Count; i++) {
                m_Sources[i].Stop();
            }
        }

        public void SetVolume(float volume) {
            for (int i = 0; i < m_Sources.Count; i++) {
                m_Sources[i].volume = volume;
            }
        }

    }
}