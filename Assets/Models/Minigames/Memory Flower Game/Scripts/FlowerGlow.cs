using System.Collections;
using UnityEngine;

public class FlowerGlow : MonoBehaviour, IInteractable
{
    private MeshRenderer _meshRenderer;
    public Material GlowyMaterial;
    public Material NonGlowyMaterial;
    private AudioSource _audioSource;

    // UI
    private Canvas _canvas;

    private float _nextInteractTime = 0f;
    private float _interactCooldown = 0.75f;
    private bool _inRange = false;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Time.time > _nextInteractTime && _inRange)
        {
            EnableCanvas();
        }
        else
        {
            DisableCanvas();
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

    public IEnumerator Glow(float waitingTime)
    {
        _interactCooldown = waitingTime;
        _nextInteractTime = Time.time + waitingTime;
        _meshRenderer.material = GlowyMaterial;
        _audioSource.Play();
        yield return new WaitForSeconds(waitingTime);
        _meshRenderer.material = NonGlowyMaterial;
    }

    public void Interact()
    {
        if (Time.time > _nextInteractTime && _inRange && !MemoryGameManager.Instance.IsPlayingMemorySequence)
        {
            MemoryGameManager.Instance.EnterFlower(this);
            StartCoroutine(Glow(_interactCooldown));
        }
    }
}
