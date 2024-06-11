using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SoundType
{
    Bgm,
    Effect,
    MaxCount,  // �ƹ��͵� �ƴ�, ���� �˱� ����
}

public class SoundManager : Singleton<SoundManager>
{
    public AudioClip[] bgmClips;
    public AudioClip[] effectClips;
    public GameObject root;

    // �����, ȿ���� ��ø�� �����ϱ� ����
    private AudioSource[] _audioSources = new AudioSource[(int)SoundType.MaxCount];

    private void Start()
    {
        Init();
        if (bgmClips.Length > 0)
        {
            Play(0, 0.2f, SoundType.Bgm);
        }
    }

    public void Init()
    {
        root = GameObject.Find("Sound");
        if (root == null)
        {
            root = new GameObject { name = "Sound" };

            string[] soundNames = System.Enum.GetNames(typeof(SoundType)); // "Bgm", "Effect"
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)SoundType.Bgm].loop = true; // bgm ������ ���� �ݺ� ���
        }
    }

    public void Clear()
    {
        // ����� ���� ��� ��ž, ���� ����
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
            Destroy(root);
        }
    }

    public void Play(int index, float volume = 1.0f, SoundType type = SoundType.Effect, bool loop = false)
    {
        AudioClip[] clips = type == SoundType.Bgm ? bgmClips : effectClips;

        if (index < 0 || index >= clips.Length)
        {
            Debug.LogError($"{type} index out of range.");
            return;
        }

        AudioClip clip = clips[index];
        if (clip == null) return;

        AudioSource audioSource = _audioSources[(int)type];
        audioSource.volume = volume;
        audioSource.loop = loop;

        if (type == SoundType.Bgm)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
            if (loop)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
            else
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }

    public void Stop(SoundType type = SoundType.Effect)
    {
        AudioSource audioSource = _audioSources[(int)type];
        audioSource.loop = false;
        audioSource.Stop();
    }
}
