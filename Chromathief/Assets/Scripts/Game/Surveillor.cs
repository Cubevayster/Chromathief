using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surveillor : MonoBehaviour
{
    [SerializeField] Vector2 angles;
    [SerializeField] float range;

    [Space(5)]

    [SerializeField] LineRenderer line;
    [SerializeField] Transform forwardRef;
    [SerializeField] LayerMask layer;

    [Space(5)]

    [SerializeField] float detectionUpdateRate;
    [SerializeField] int detectionRayCount;
    [SerializeField] float horizontalRayFrequence;

    List<BaseEntity> detectedEntities;

    private void Start()
    {
        detectedEntities = new List<BaseEntity>();
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        while(true)
        {
            ResetEntities();
            detectedEntities = DetectEntities();
            foreach (BaseEntity e in detectedEntities) { e.SetDetected(true); }
            yield return new WaitForSeconds(1f / detectionUpdateRate);

            /*for (int i = 0; i < detectionRayCount; i++)
            {
                CastRay(i / (float)detectionRayCount);
                yield return new WaitForSeconds(0.1f);
            }*/
        }
    }

    void ResetEntities()
    {
        foreach(BaseEntity e in detectedEntities) { e.SetDetected(false); }
        detectedEntities.Clear();
    }

    BaseEntity CastRay(float r)
    {
        float w = Mathf.Sin(r * Mathf.PI) * angles.x;
        float x = w * Mathf.Cos(r * 2 * Mathf.PI * horizontalRayFrequence);
        float y = Mathf.Cos(r * Mathf.PI) * angles.y;

        Vector3 dir = forwardRef.forward + (forwardRef.right * x) + (forwardRef.up * y);

        //Debug.DrawRay(forwardRef.position, dir * range, Color.red, 0.2f);
        if (Physics.Raycast(forwardRef.position, dir, out RaycastHit hit, range, layer))
        {
            if(hit.collider.TryGetComponent(out BaseEntity e))
            {
                return e;
            }
        }

        return null;
    }

    List<BaseEntity> DetectEntities()
    {
        List<BaseEntity> entities = new List<BaseEntity>();

        for(int i = 0; i < detectionRayCount; i++)
        {
            BaseEntity e = CastRay(i / (float)detectionRayCount);
            if (e != null) { entities.Add(e); }
        }

        return entities;
    }

    private void OnDrawGizmos()
    {
        Vector3 bot = forwardRef.forward - (forwardRef.up * angles.y);
        Vector3 top = forwardRef.forward + (forwardRef.up * angles.y);
        Vector3 right = forwardRef.forward + (forwardRef.right * angles.x);
        Vector3 left = forwardRef.forward - (forwardRef.right * angles.x);
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(forwardRef.position, forwardRef.position + bot * range);
        Gizmos.DrawLine(forwardRef.position, forwardRef.position + top * range);
        Gizmos.DrawLine(forwardRef.position, forwardRef.position + right * range);
        Gizmos.DrawLine(forwardRef.position, forwardRef.position + left * range);
    }
}
