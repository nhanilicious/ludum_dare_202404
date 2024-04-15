using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] public IInteractable Interactable;

    // Start is called before the first frame update
    void Start()
    {
        Interactable = GetComponent<IInteractable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable.Interact();
        }
    }
}
