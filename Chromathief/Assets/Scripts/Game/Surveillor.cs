using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surveillor : MonoBehaviour
{
    [SerializeField] Vector2 angles;
    [SerializeField] float range;

    [Space(5)]

    [SerializeField] Transform forwardRef;
    [SerializeField] LayerMask layer;

    [Space(5)]

    [SerializeField] float detectionUpdateRate;
    [SerializeField] int detectionRayCount;
    [SerializeField] float detectionRayFrequence;

    [Space(5)]
    [SerializeField] LineRenderer[] lines; //List<Ray> lineRays;
    [SerializeField] float laserRotationSpeed;

    [SerializeField] LineRenderer focusLine;

    /*
    [SerializeField] int lineRayCount;
    [SerializeField] float lineRayUpdateRate;
    [SerializeField] float lineRayFrequence;
    [SerializeField] float lineRaySpeed;
    [SerializeField] float lineRayMaxDistance;
    */

    List<PlayingEntity> detectedEntities;
    ColorEntity detectedWall;
    float laserCurrentAngle;

    private void Start()
    {
        detectedEntities = new List<PlayingEntity>();
        //lineRays = new List<Ray>();
        StartCoroutine(DetectionCoroutine());
        //StartCoroutine(DrawLines());
    }

    private void Update()
    {
        DrawZone();
        laserCurrentAngle += Time.deltaTime * laserRotationSpeed;

        if(detectedEntities.Count > 0) 
        {
            focusLine.gameObject.SetActive(true);
            foreach (PlayingEntity pe in detectedEntities)
            {
                if (pe.DetectedBy(gameObject))
                {
                    DrawFocusLine(pe.transform); break;
                }
            }
        }
        else { focusLine.gameObject.SetActive(false); }
    }

    #region Detection
    IEnumerator DetectionCoroutine()
    {
        while(true)
        {
            ResetEntities();
            detectedEntities = DetectEntities();
            ProcessDetectedEntities();
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
        foreach(BaseEntity e in detectedEntities) { e.RemoveDetected(gameObject); }
        detectedEntities.Clear();
    }

    Ray BuildRay(float r,float freq)
    {
        float w = Mathf.Sin(r * Mathf.PI) * angles.x;
        float x = w * Mathf.Cos(r * 2 * Mathf.PI * freq);
        float y = Mathf.Cos(r * Mathf.PI) * angles.y;

        Vector3 dir = forwardRef.forward + (forwardRef.right * x) + (forwardRef.up * y);

        return new Ray(forwardRef.position, dir);
    }

    ColorEntity CastRay(float r)
    {
        Ray ray = BuildRay(r, detectionRayFrequence);

        //Debug.DrawRay(forwardRef.position, dir * range, Color.red, 0.2f);
        if (Physics.Raycast(ray, out RaycastHit hit, range, layer))
        {
            if(hit.collider.TryGetComponent(out ColorEntity e))
            {
                return e;
            }
        }

        return null;
    }

    List<PlayingEntity> DetectEntities()
    {
        List<PlayingEntity> entities = new List<PlayingEntity>();

        for(int i = 0; i < detectionRayCount; i++)
        {
            ColorEntity e = CastRay(i / (float)detectionRayCount);
            if (e != null)
            {
                if (e.TryGetComponent(out PlayingEntity pe))
                {
                    entities.Add(pe);
                }
                else if (e.tag == "Wall")
                {
                    detectedWall = e;
                }
            }
        }

        return entities;
    }

    void ProcessDetectedEntities()
    {
        foreach(PlayingEntity pe in detectedEntities)
        {
            if(detectedWall == null || pe.Color != detectedWall.Color)
            {
                pe.AddDetected(gameObject);
            }
            else 
            { 
                pe.RemoveDetected(gameObject); 
            }
        }
    }

    #endregion

    #region Display
    /*
    void AppendLine(Ray ray)
    {
        lineRays.Add(ray);
        if(lineRays.Count > lines.Length) { lineRays.RemoveAt(0); }
    }

    void UpdateLines()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            if (i >= lineRays.Count) { break; }

            Vector3[] arr = new Vector3[2] { lineRays[i].origin, lineRays[i].origin + (lineRays[i].direction * range) };

            for (int l = 0; l < 2; l++)
            {
                if ((arr[l] - lines[i].GetPosition(l)).magnitude > lineRayMaxDistance)
                {
                    lines[i].SetPosition(l, arr[l]);
                }
                else
                {
                    Vector3 p = Vector3.MoveTowards(lines[i].GetPosition(l), arr[l], Time.deltaTime * lineRaySpeed);
                    lines[i].SetPosition(l, p);
                }
            }

        }
    }

    IEnumerator DrawLines()
    {
        while(true)
        {
            for(int i = 0; i < lineRayCount; i++)
            {
                Ray ray = BuildRay(i / (float)lineRayCount,lineRayFrequence);
                AppendLine(ray);
                UpdateLines();
                yield return new WaitForSeconds(1f / lineRayUpdateRate);
            }
            
        }
    }*/

    Vector3 GetRayVect(float offset)
    {
        return new Vector3(
            Mathf.Cos(laserCurrentAngle + offset) * range * angles.x, 
            Mathf.Sin(laserCurrentAngle + offset) * range * angles.y, 
            range);
    }

    void DrawZone()
    {
        Vector3 bot = GetRayVect(0);
        Vector3 right = GetRayVect(Mathf.PI * .5f);
        Vector3 top = GetRayVect(Mathf.PI);
        Vector3 left = GetRayVect(Mathf.PI * 1.5f);

        lines[0].SetPositions(new Vector3[2] { Vector3.zero, Vector3.zero + bot});
        lines[1].SetPositions(new Vector3[2] { Vector3.zero, Vector3.zero + top });
        lines[2].SetPositions(new Vector3[2] { Vector3.zero, Vector3.zero + right });
        lines[3].SetPositions(new Vector3[2] { Vector3.zero, Vector3.zero + left });

        
        
    }

    void DrawFocusLine(Transform target)
    {
        Vector3 dir = (target.position - forwardRef.position);
        focusLine.SetPositions(new Vector3[2] { forwardRef.position, forwardRef.position + dir });
    }

    #endregion

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
