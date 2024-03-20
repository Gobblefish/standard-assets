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
            audioSource.loop = false;
            audioSource.pitch = 1f;
            return audioSource;
        }

        public void Play(AudioClip audioClip) {
            m_Source.clip = audioClip;
            m_Source.PlayScheduled(UnityEngine.AudioSettings.dspTime);
        }

        // double nextQueueTime = 0d;
        int queueIndex = 0;
        List<AudioClip> queueClips = new List<AudioClip>();
        int sourceCurrentIndex => queueIndex % m_Sources.Count;
        int nextSourceIndex => (queueIndex + 1) % m_Sources.Count;

        public void CheckQueue() {
            if (nextQueueTime > UnityEngine.AudioSettings.dspTime) {
                return;
            }

            if (nextQueueTime == -1.0) {
                nextQueueTime = UnityEngine.AudioSettings.dspTime;
            }

            AudioSource currentSource = m_Sources[sourceCurrentIndex];
            
            double timeLeftForEndOfClip = 0d;
            if (currentSource.clip != null && currentSource.isPlaying) {
                double clipLength = currentSource.clip.Length();
                double lengthPlayed = (double)currentSource.timeSamples / currentSource.clip.frequency;
                timeLeftForEndOfClip = clipLength - lengthPlayed;
                // Debug.Log(clipLength);
                // Debug.Log(lengthPlayed);
            }

            if (timeLeftForEndOfClip < 1d) {
                PlayNextInQueue(timeLeftForEndOfClip);
            }

        }

        public void PlayNextInQueue(double delay) {
            AudioSource queueSource = m_Sources[nextSourceIndex];
            nextQueueTime = UnityEngine.AudioSettings.dspTime + delay;

            queueSource.clip = queueClips[queueIndex % queueClips.Count];
            queueSource.PlayScheduled(nextQueueTime);
            queueIndex += 1;
            // nextSourceQueued = true;
        }

        public void Queue(AudioClip audioClip) {
            queueClips.Add(audioClip);
        }

        // public void _Queue(AudioClip audioClip) {
        //     if (audioClip == null) { return; }

        //     AudioSource queueSource = GenerateSource();
        //     m_Sources.Add(queueSource);
        //     queueSource.gameObject.name = "Queue " + m_Sources.Count.ToString() + " " + queueSource.gameObject.name;
        //     queueSource.loop = false;
            
        //     // int sourceIndex = 0;
        //     //     if (!m_Sources[i].isPlaying) {
        //     //         sourceIndex
        //     //     }
        //     // }
            
        //     if (nextQueueTime == -1.0) {
        //         nextQueueTime = UnityEngine.AudioSettings.dspTime;
        //     }

        //     double delay = 0.0;
            
        //     AudioSource prevSource = m_Sources[m_Sources.Count - 2];
        //     prevSource.loop = false;
        //     if (prevSource.clip != null) {
        //         double clipLength = prevSource.clip.Length();
        //         double lengthPlayed = (double)prevSource.timeSamples / prevSource.clip.frequency;
        //         delay = clipLength - lengthPlayed;
        //         Debug.Log(clipLength);
        //         Debug.Log(lengthPlayed);
        //     }
        //     Debug.Log(delay);

        //     // assuming BPM of 140 and in 4/4.
        //     double quarterNote = 60d / 140;
        //     double fourbars = quarterNote * 4 * 4;
        //     double sixteenth = quarterNote / 4; 

        //     Debug.Log(delay);
        //     Debug.Log(fourbars);
        //     Debug.Log(delay == fourbars);

        //     nextQueueTime = nextQueueTime + delay; // fourbars; // delay;
        //     // if (delay < 1.0) {
            
        //     queueSource.volume = m_Source.volume;
        //     queueSource.clip = audioClip;
        //     queueSource.PlayScheduled(nextQueueTime);
        //     // }
        //     // else {
        //     //     LoadQueuedClip(nextQueueTime);
        //     // }
        //     // currTime + delay;
        // }

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
            for (int i = 0; i < m_Sources.Count; i++) {
                m_Sources[i].volume = volume;
            }
            // m_Source.volume = volume;
        }

    }
}