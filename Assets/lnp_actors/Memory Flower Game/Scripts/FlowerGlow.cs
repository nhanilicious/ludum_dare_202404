using System.Collections;
using UnityEngine;

public class FlowerGlow : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    public Material GlowyMaterial;
    public Material NonGlowyMaterial;

    // Start is called before the first frame update
    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public IEnumerator Glow(float waitingTime)
    {
        _meshRenderer.material = GlowyMaterial;
        yield return new WaitForSeconds(waitingTime);
        _meshRenderer.material = NonGlowyMaterial;
    }
}
