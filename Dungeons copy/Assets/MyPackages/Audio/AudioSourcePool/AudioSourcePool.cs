using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class AudioSourcePool : MonoBehaviour
{
    [SerializeField] GameObject _audioSourcePrefab;
    private ObjectPool<GameObject> _audioSourcePool;

    // Start is called before the first frame update
    void Awake()
    {
        _audioSourcePool = new ObjectPool<GameObject>(CrateSource, OnTakeAudioSourceFromPool, OnReturnAudioSourceToPool);
    }

    public AudioSource GetSource()
    {
        return _audioSourcePool.Get().GetComponent<AudioSource>();
    }
    GameObject CrateSource()
    {
        GameObject source = Instantiate(_audioSourcePrefab);
        return source;

    }
    public void OnTakeAudioSourceFromPool(GameObject source)
    {
        source.gameObject.SetActive(true);
    }
    public void OnReturnAudioSourceToPool(GameObject source)
    {
        source.SetActive(false);
    }
}
