using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MemoryGameManager : MonoBehaviour, IInteractable
{
    public static MemoryGameManager Instance;

    // UI
    private Canvas _canvas;

    // Flowers
    public FlowerGlow FlowerG3;
    public FlowerGlow FlowerC4;
    public FlowerGlow FlowerD4;
    public FlowerGlow FlowerE4;

    private List<FlowerGlow> _flowerSequence1;
    private List<FlowerGlow> _flowerSequence2;
    private List<FlowerGlow> _flowerSequence3;

    public List<FlowerGlow> EnteredSequence;

    // Memory Game
    public int CurrentSequence = 1;
    private float _nextInteractTime = 0f;
    private float _interactCooldown;
    private bool _inRange = false;

    public float NoteInterval = 0.75f;

    public bool IsPlayingMemorySequence = false;
    public bool AdvanceGame = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();

        _flowerSequence1 = new List<FlowerGlow>();
        _flowerSequence1.Add(FlowerC4);
        _flowerSequence1.Add(FlowerE4);
        _flowerSequence1.Add(FlowerD4);

        _flowerSequence2 = new List<FlowerGlow>(_flowerSequence1);
        _flowerSequence2.Add(FlowerG3);

        _flowerSequence3 = new List<FlowerGlow>(_flowerSequence2);
        _flowerSequence3.Add(FlowerC4);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _nextInteractTime && _inRange)
        {
            EnableCanvas();
        }
        else
        {
            DisableCanvas();
        }

        if (Time.time > _nextInteractTime && AdvanceGame)
        {
            AdvanceGame = false;
            IsPlayingMemorySequence = true;
            StartCoroutine(PlaySequence(GetCurrentFlowerSequence(CurrentSequence)));
            IsPlayingMemorySequence = false;
        }
    }

    private IEnumerator PlaySequence(List<FlowerGlow> flowerSequence)
    {
        _interactCooldown = flowerSequence.Count * NoteInterval;
        _nextInteractTime = Time.time + _interactCooldown;

        for (int i = 0; i < flowerSequence.Count; i++)
        {
            yield return flowerSequence[i].Glow(NoteInterval);
        }
    }

    public IEnumerator PlaySequence(int sequence)
    {
        IsPlayingMemorySequence = true;
        yield return StartCoroutine(PlaySequence(GetCurrentFlowerSequence(sequence)));
        IsPlayingMemorySequence = false;
    }

    public void EnterFlower(FlowerGlow flowerGlow)
    {
        EnteredSequence.Add(flowerGlow);
        _nextInteractTime = Time.time + NoteInterval;

        int result = CheckEnteredSequence();

        switch (result)
        {
            case 0:
                if (CurrentSequence == 3)
                {
                    // TODO: Tell GameManager we spawned SirCroakaint
                }

                CurrentSequence += CurrentSequence < 3 ? 1 : 0;
                EnteredSequence = new List<FlowerGlow>();
                AdvanceGame = true;

                // TODO: play happy sound if sequence 3 is completed
                break;
            case -1:
                EnteredSequence = new List<FlowerGlow>();
                // TODO: play sad sound
                break;
            default: break;
        }
    }

    // -1 -> mistake
    // 0 -> complete
    // anything else -> incomplete
    private int CheckEnteredSequence()
    {
        List<FlowerGlow> currentSeq = GetCurrentFlowerSequence(CurrentSequence);
        int enteredSize = EnteredSequence.Count;
        int currentSeqSize = currentSeq.Count;

        for (int i = 0; i < enteredSize; i++)
        {
            if (EnteredSequence[i] != currentSeq[i])
            {
                return -1;
            }
        }

        return enteredSize == currentSeqSize ? 0 : enteredSize;
    }

    public List<FlowerGlow> GetCurrentFlowerSequence(int sequence)
    {
        switch (sequence)
        {
            case 1:
                return _flowerSequence1;
            case 2:
                return _flowerSequence2;
            case 3:
                return _flowerSequence3;
            default:
                return _flowerSequence3;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnableCanvas();
        _inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        DisableCanvas();
        _inRange = false;
    }

    private void EnableCanvas()
    {
        _canvas.enabled = true;
    }

    private void DisableCanvas()
    {
        _canvas.enabled = false;
    }

    public void Interact()
    {
        if (Time.time > _nextInteractTime && _inRange)
        {
            StartCoroutine(PlaySequence(CurrentSequence));
        }
    }
}
