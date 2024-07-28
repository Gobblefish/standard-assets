// Unity.
using UnityEngine;

namespace GobbleFish.Audio {

    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName="Sound Effect Collection", menuName="Collections/Sounds")]
    public class SoundEffectCollection : ScriptableCollection<SoundEffect> {

        public void Play(string name) {
            SoundEffect sfx = Get(name);
            if (sfx != null) {
                sfx.Play();       
            }
        }

        public void Stop(string name) {
            SoundEffect sfx = Get(name);
            if (sfx != null) {
                sfx.Stop();       
            }
        }
    
    }

}