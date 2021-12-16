using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr instance;

    public AudioClip[] effectClip; //0�ѼҸ�,1����ź
    public AudioClip[] BGClip; //0�޴�, 1��������

    public AudioSource BGSound;

    public GameObject audioListenerObj; //�޴��� ��� ���ؼ�

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

        BGSound.clip = BGClip[index]; //�޴�,��������

        BGSound.loop = true;
        BGSound.volume = 0.5f;
        BGSound.Play();
    }

}
