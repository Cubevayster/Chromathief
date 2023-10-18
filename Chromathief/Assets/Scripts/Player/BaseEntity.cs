using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    [SerializeField] GameObject detectedEffect;

    protected List<GameObject> detectedBy = new List<GameObject>();
    public bool ContainsDected(GameObject g) { return detectedBy.Contains(g); }
    public void AddDetected(GameObject g) { if(!ContainsDected(g)) detectedBy.Add(g); }
    public void RemoveDetected(GameObject g) { detectedBy.Remove(g); }
    public bool Detected { get { return detectedBy.Count > 0; } }
    public bool DetectedBy(GameObject g) { return detectedBy.Contains(g); }

    protected virtual void Update()
    {
        if (detectedEffect != null)
        {
            detectedEffect.SetActive(Detected);
        }
    }

    protected virtual void Start() { }
}
