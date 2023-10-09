using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEntity : BaseEntity
{
    [SerializeField]
    protected List<GameObject> colorableObjects;
    [SerializeField]
    protected ParticleSystem mainEffect;
 
    [SerializeField] protected bool red; public bool Red { get { return red; } }
    [SerializeField] protected bool blue; public bool Blue { get { return blue; } }
    [SerializeField] protected bool yellow; public bool Yellow { get { return yellow; } }
    public ColorManager.GameColor Color { get { return ColorManager.ColorOf(red, blue, yellow); } }
    public Color MatColor { get { return ColorManager.GetColor(Color); } }

    protected override void Start()
    {
        base.Start();
        SetupMaterials(); UpdateColor();
    }

    public virtual void Clear() { red = false; blue = false; yellow = false; UpdateColor(true, false, false, false); }
    public virtual void All() { red = true; blue = true; yellow = true; UpdateColor(true, true, true, true); }
    public void ToggleRed() { ChangeRed(!red); } public virtual void ChangeRed(bool r) { red = r; UpdateColor(true, true, false, false); }// HUD.Instance?.TogglePrimaryColor(ColorManager.GameColor.Red, red); }
    public void ToggleBlue() { ChangeBlue(!blue); } public virtual void ChangeBlue(bool b) { blue = b; UpdateColor(true, false, true, false);}//HUD.Instance?.TogglePrimaryColor(ColorManager.GameColor.Blue, blue); }
    public void ToggleYellow() { ChangeYellow(!yellow); } public virtual void ChangeYellow(bool y) { yellow = y; UpdateColor(true, false, false, true); }// HUD.Instance?.TogglePrimaryColor(ColorManager.GameColor.Yellow, yellow); }

    void PlayEffect(Color colorApplied)
    {
        if(mainEffect == null) { return; }
        mainEffect.GetComponent<ParticleSystemRenderer>().sharedMaterial.color = colorApplied;
        mainEffect.Play();
    }

    protected virtual void UpdateColor(bool playEffect = false, bool _red = false, bool _blue = false, bool _yellow = false)
    {
        if (playEffect) { PlayEffect(ColorManager.GetColor(_red,_blue,_yellow)); }
        foreach (GameObject g in colorableObjects)
        {
            ApplyColor(g);
        }
    }

    void ApplyColor(GameObject g)
    {
        if(g.TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.sharedMaterial.color = MatColor;
        }

    }

    void SetupMaterials()
    {
        foreach(GameObject g in colorableObjects)
        {
            if (g.TryGetComponent(out MeshRenderer meshRenderer))
            {
                meshRenderer.sharedMaterial = new Material(meshRenderer.sharedMaterial);
            }
        }
        if (mainEffect != null)
        {
            mainEffect.GetComponent<ParticleSystemRenderer>().sharedMaterial = new Material(mainEffect.GetComponent<ParticleSystemRenderer>().sharedMaterial);
        }
    }
}
