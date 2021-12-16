using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr instance;

    public AudioClip[] effectClip; //0총소리,1수류탄
    public AudioClip[] BGClip; //0메뉴, 1스테이지

    public AudioSource BGSound;

    public GameObject audioListenerObj; //메뉴에 브금 위해서

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            BGSoundPlay(0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //BGSoundPlay(BGClip[0]);
    }

    void Update()
    {
        
    }

    public void SFXPlay(string sfxName, AudioClip clip)
    {
        GameObject go = new GameObject(sfxName + "Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();

        Destroy(go, clip.length);
    }

    public void BGSoundPlay(int index)
    {
        if (index == 0)
            audioListenerObj.SetActive(true);
        else
            audioListenerObj.SetActive(false);

        BGSound.clip = BGClip[index]; //메뉴,스테이지

        BGSound.loop = true;
        BGSound.volume = 0.5f;
        BGSound.Play();
    }

}
