using System.Collections.Generic;
using System.Linq;
using Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AudioSystem.Runtime
{
    public class AudioManager : BaseMonobehaviour
    {

        #region Publics

        public static AudioManager Instance { get; private set; }

        public enum AudioChannel
        {
            Master,
            Music,
            SFX,
            Ambiance
        }

        #endregion


        #region Unity API

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            _sfxSource = gameObject.AddComponent<AudioSource>();
        }

        #endregion


        #region Main Methods

        public void PlayLevelMusic()
        {
            if (_levelMusic != null)
            {
                PlayMusic(_levelMusic, true);
            }
        }
        
        public void PlaySFX(AudioClip clip)
        {
            if(clip == null) return;
            _sfxSource.PlayOneShot(clip);
        }

        public void PlaySFXByName(string name)
        {
            AudioClip clip = _sfxLibrary.FirstOrDefault(c => c.name == name);
            if (clip != null)
            {
                PlaySFX(clip);
            }
        }
        
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if(clip == null) return;
            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.Play();
        }

        public void PlayAmbiance(AudioClip clip, bool loop = true)
        {
            if(clip == null) return;
            _ambianceSource.clip = clip;
            _ambianceSource.loop = true;
            _ambianceSource.Play();
        }

        public void SetMasterVolume(float value)
        {
            if (_audioMixer != null)
            {
                _audioMixer.SetFloat("MasterVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
            }
        }
        
        public void SetMusicVolume(float value)
        {
            if (_audioMixer != null)
            {
                _audioMixer.SetFloat("MusicVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
            }
        }
        
        public void SetSFXVolume(float value)
        {
            if (_audioMixer != null)
            {
                _audioMixer.SetFloat("SFXVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
            }
        }
        
        public void SetAmbianceVolume(float value)
        {
            if (_audioMixer != null)
            {
                _audioMixer.SetFloat("AmbianceVolume", Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1)) * 20);
            }
        }

        public void BinderVolume(Slider slider, AudioChannel channel)
        {
            if(slider == null) return;

            switch (channel)
            {
                case AudioChannel.Master:
                    slider.onValueChanged.AddListener(SetMasterVolume);
                    break;
                case AudioChannel.Music:
                    slider.onValueChanged.AddListener(SetMusicVolume);
                    break;
                case AudioChannel.SFX:
                    slider.onValueChanged.AddListener(SetSFXVolume);
                    break;
                case AudioChannel.Ambiance:
                    slider.onValueChanged.AddListener(SetAmbianceVolume);
                    break;
            }
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected
        
        [Header("Source audio du level")]
        [SerializeField] private AudioClip _levelMusic;

        [Header("Audio Sources")]
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _ambianceSource;
        [SerializeField] private List<AudioClip> _sfxLibrary;
        
        [Header("Mixer (Optional)")]
        [SerializeField] private AudioMixer _audioMixer;

        private AudioSource _sfxSource;
        
        #endregion
    }
}

