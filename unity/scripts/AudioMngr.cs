using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMngr : MonoBehaviour
{
    private int currentBGM = -1;

    public AudioSource sourceBGM;
    public AudioSource sourceSound;

    public List<AudioClip> bgms;
    public List<AudioClip> effects;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBGM(int idx) // 배경음
    {
        if (currentBGM != idx && idx >= 0 && bgms.Count > idx)
        {
            sourceBGM.Stop();
            currentBGM = idx;
            sourceBGM.clip = bgms[idx];
            sourceBGM.Play();
        }
    }

    public void PlaySound(int idx) // 효과음 
    {
        if (idx >= 0 && effects.Count > idx)
        {
            //sourceSound.clip = effects[idx]; // 기존 clip을 변경 않음. clip 설정 없이 바로 재생.
            sourceSound.PlayOneShot(effects[idx]); // 지정한 오디오 클립을 짧게 한 번만 재생. 주로 총소리, 버튼 클릭 등의 효과음.
        }
    }

    public int NumSounds()
    {
        return effects.Count;
    }

    public int NumBGMs()
    {
        return bgms.Count;
    }

    public int GetCurrentBGM()
    {
        return currentBGM;
    }

    public void PlayNextBGM()
    {
        currentBGM++;
        if (bgms.Count <= currentBGM)
        {
            currentBGM = 0;
        }
        if (bgms.Count > currentBGM)
        {
            Debug.Log("Playing BGM #" + currentBGM);
            sourceBGM.clip = bgms[currentBGM];
            sourceBGM.loop = true;
            sourceBGM.Play();
        }

    }
}
