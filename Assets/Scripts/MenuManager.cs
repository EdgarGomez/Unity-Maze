using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TMP_Dropdown qualityDropdown;
    [SerializeField] private AudioSource audioSource;


    private void Start()
    {
        optionsPanel.SetActive(false);
        InitializeUI();
    }

    private void InitializeUI()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        qualityDropdown.onValueChanged.AddListener(SetQuality);
    }

    public void PlayGame()
    {
        audioSource.Play();
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        audioSource.Play();
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        audioSource.Play();
        optionsPanel.SetActive(false);
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        audioSource.Play();
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
