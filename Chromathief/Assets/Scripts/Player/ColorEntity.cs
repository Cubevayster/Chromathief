using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorEntity : BaseEntity
{
    [SerializeField]
    protected List<GameObject> colorableObjects;

    [SerializeField] protected bool red; public bool Red { get { return red; } }
    [SerializeField] protected bool blue; public bool Blue { get { return blue; } }
    [SerializeField] protected bool yellow; public bool Yellow { get { return yellow; } }
    public ColorManager.GameColor Color { get { return ColorManager.ColorOf(red, blue, yellow); } }
    public Color MatColor { get { return ColorManager.GetColor(Color); } }

    [SerializeField]
    bool update;

    protected override void Start()
    {
        base.Start();
        SetupMaterials(); UpdateColor();
    }

    private void Update()
    {
        if (update) { UpdateColor(); update = false; }
    }

    public void ToggleRed() { ChangeRed(!red); } public void ChangeRed(bool r) { red = r; UpdateColor(); HUD.Instance?.TogglePrimaryColor(ColorManager.GameColor.Red, red); }
    public void ToggleBlue() { ChangeBlue(!blue); } public void ChangeBlue(bool b) { blue = b; UpdateColor(); HUD.Instance?.TogglePrimaryColor(ColorManager.GameColor.Blue, blue); }
    public void ToggleYellow() { ChangeYellow(!yellow); } public void ChangeYellow(bool Y) { yellow = Y; UpdateColor(); HUD.Instance?.TogglePrimaryColor(ColorManager.GameColor.Yellow, yellow); }

    void UpdateColor()
    {
        foreach(GameObject g in colorableObjects)
        {
            ApplyColor(g);
        }
        HUD.Instance?.SetCurrentColor(Color);
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
    }
}
