using UnityEngine;
using HyperPuzzleEngine;
using System;

namespace HyperPuzzleEngine
{
    public class AudioManager : MonoBehaviour
    {

        //THE GAMES DO NOT HAVE SOUND EFFECTS RIGHT NOW, BUT IF YOU ADD YOUR OWN SOUNDS, JUST UN-COMMENT THE "RETURN" OF EACH FUNCTION AND IT WILL WORK PROPERLY
        //----------------------------------------------------------------------------------------------------------------------------------------



        #region Create Instance

        public static AudioManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        private bool canPlaySounds = true;

        public GameObject soundEffectSource;
        public AudioClip[] soundEffects;

        public bool IsPlaying(AudioClip soundClip)
        {
            foreach (AudioSource sources in soundEffectSource.GetComponents<AudioSource>())
            {
                if (sources.clip == soundClip)
                {
                    return sources.isPlaying;
                }
            }
            return false;
        }

        public void PlaySoundEffect(string nameOfSoundClip)
        {
            return;

            if (canPlaySounds)
            {
                AudioClip clipToPlay = null;

                for (int i = 0; i < soundEffects.Length; i++)
                {
                    if (soundEffects[i].name == nameOfSoundClip)
                    {
                        clipToPlay = soundEffects[i];
                        break;
                    }
                }

                if (clipToPlay != null)
                {
                    AudioSource newSource = soundEffectSource.AddComponent<AudioSource>();
                    newSource.clip = clipToPlay;
                    newSource.Play();
                }
            }
        }

        public void PlaySoundEffect(AudioClip soundClip)
        {
            return;

            if (canPlaySounds)
            {
                if (soundClip != null)
                {
                    AudioSource newSource = soundEffectSource.AddComponent<AudioSource>();
                    newSource.clip = soundClip;
                    newSource.Play();

                    Destroy(newSource, soundClip.length + 0.5f);
                }
            }
        }

        public void DisableAllSoundEffects()
        {
            return;

            canPlaySounds = false;
            foreach (AudioSource source in soundEffectSource.GetComponents<AudioSource>())
            {
                Destroy(source);
            }
        }
    }
}