using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVisuals : MonoBehaviour
{
    [Header("Heads")]
    [SerializeField] private List<Mesh> hairStyles = new List<Mesh>();
    [SerializeField] private List<Material> hairColors = new List<Material>();

    [Header("Bodys")]
    [SerializeField] private List<Mesh> bodyStyles = new List<Mesh>();
    [SerializeField] private List<Material> bodyColors = new List<Material>();

    [Header("Legs")]
    [SerializeField] private List<Mesh> legsStyles = new List<Mesh>();
    [SerializeField] private List<Material> legsColors = new List<Material>();

    [Space(20)]
    [SerializeField] private MeshFilter hairMesh;
    [SerializeField] private MeshRenderer hairRenderer;

    [Space(20)]
    [SerializeField] private MeshFilter bodyMesh;
    [SerializeField] private MeshRenderer bodyRenderer;

    [Space(20)]
    [SerializeField] private MeshFilter legsMesh;
    [SerializeField] private MeshRenderer legsRenderer;

    private CharacterVisuals GetVisuals()
    {
        CharacterVisuals v = new CharacterVisuals();

        v.hairStyle = hairStyles[Random.Range(0, hairStyles.Count)];
        v.hairColor = hairColors[Random.Range(0, hairColors.Count)];

        v.bodyStyle = bodyStyles[Random.Range(0, bodyStyles.Count)];
        v.bodyColor = bodyColors[Random.Range(0, bodyColors.Count)];

        v.legsStyle = legsStyles[Random.Range(0, legsStyles.Count)];
        v.legsColor = legsColors[Random.Range(0, legsColors.Count)];

        return v;
    }

    public void RandomizeLook()
    {
        return;
        CharacterVisuals v = GetVisuals();

        hairMesh.mesh = v.hairStyle;
        hairRenderer.material = v.hairColor;

        bodyMesh.mesh = v.bodyStyle;
        bodyRenderer.material = v.bodyColor;

        legsMesh.mesh = v.legsStyle;
        legsRenderer.material = v.legsColor;
    }
}

public struct CharacterVisuals
{
    public Mesh hairStyle;
    public Material hairColor;

    public Mesh bodyStyle;
    public Material bodyColor;

    public Mesh legsStyle;
    public Material legsColor;
}
