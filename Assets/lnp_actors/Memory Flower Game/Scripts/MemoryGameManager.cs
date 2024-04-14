using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour
{
    public static MemoryGameManager Instance;

    // Flowers
    public FlowerGlow FlowerG3;
    public FlowerGlow FlowerC4;
    public FlowerGlow FlowerD4;
    public FlowerGlow FlowerE4;

    private List<FlowerGlow> _flowerSequence1;
    private List<FlowerGlow> _flowerSequence2;
    private List<FlowerGlow> _flowerSequence3;

    // Memory Game
    private AudioSource _audioSource;

    public AudioClip G3;
    public AudioClip C4;
    public AudioClip D4;
    public AudioClip E4;

    private List<AudioClip> _memorySequence1;
    private List<AudioClip> _memorySequence2;
    private List<AudioClip> _memorySequence3;

    public float NoteInterval = 0.5f;

    public bool IsPlayingMemorySequence = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _memorySequence1 = new List<AudioClip>();
        _memorySequence1.Add(C4);
        _memorySequence1.Add(E4);
        _memorySequence1.Add(D4);

        _flowerSequence1 = new List<FlowerGlow>();
        _flowerSequence1.Add(FlowerC4);
        _flowerSequence1.Add(FlowerE4);
        _flowerSequence1.Add(FlowerD4);

        _memorySequence2 = new List<AudioClip>(_memorySequence1);
        _memorySequence2.Add(G3);

        _flowerSequence2 = new List<FlowerGlow>(_flowerSequence1);
        _flowerSequence2.Add(FlowerG3);

        _memorySequence3 = new List<AudioClip>(_memorySequence2);
        _memorySequence3.Add(C4);

        _flowerSequence3 = new List<FlowerGlow>(_flowerSequence2);
        _flowerSequence3.Add(FlowerC4);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPlayingMemorySequence)
        {
            IsPlayingMemorySequence = false;
            StartCoroutine(PlaySequence(_memorySequence3, _flowerSequence3));
        }
    }

    private IEnumerator PlaySequence(List<AudioClip> sequence, List<FlowerGlow> flowerSequence)
    {
        for (int i = 0; i < sequence.Count; i++)
        {
            _audioSource.clip = sequence[i];
            _audioSource.Play();
            yield return flowerSequence[i].Glow(NoteInterval);
            //yield return new WaitForSeconds(NoteInterval);
        }
    }

    public IEnumerator PlaySequence(int sequence)
    {
        switch (sequence)
        {
            case 1:
                yield return PlaySequence(_memorySequence1, _flowerSequence1);
                break;
            case 2:
                yield return PlaySequence(_memorySequence2, _flowerSequence2);
                break;
            case 3:
                yield return PlaySequence(_memorySequence3, _flowerSequence3);
                break;
        }
    }
}
