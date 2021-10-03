using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];

    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if (root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);
            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length-1; i++)
            {
                GameObject go = new GameObject { name = soundNames[i] };
                _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }
            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }
    }

    public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path,type);
        Play(audioClip, type, pitch);
    }
    public void Play(AudioClip clip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
    {
        if (clip == null) return;

        if (type == Define.Sound.Bgm)
        {
            
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = clip;
            audioSource.Play();
        }
        else
        {
           
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(clip);
        }
    }
    public AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
    {
        if (path.Contains("Sounds/") == false)
        {
            path = $"Sounds/{path}";
        }

        AudioClip clip = null;

        if (type == Define.Sound.Bgm)
        {
            clip = Managers.Resource.Load<AudioClip>(path);
        }
        else
        {
            
            if (_audioClips.TryGetValue(path, out clip) == false)
            {
                clip = Managers.Resource.Load<AudioClip>(path);
                _audioClips.Add(path, clip);
            }

        }

        if (clip == null)
        {
            Debug.Log($"AudioClip missing {path}");
        }

        return clip;
    }

    public void Clear()
    {
        foreach (var source in _audioSources)
        {
            source.clip = null;
            source.Stop();
        }
        _audioClips.Clear();
    }
}
