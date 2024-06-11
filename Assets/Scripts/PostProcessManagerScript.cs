using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessManagerScript : MonoBehaviour
{
    public Material Edge;
    [Range(0.0001f, 10f)]
    public float edgeThreshold = 0.5f;
    public Color edgeColor = Color.white;
    [Range(0f, 1f)]
    public float edgeOpacity = 0f;
    public bool colorEdges = false;
    public bool enableEdges = true;

    public Material Outline;
    [Range(0.01f, 10f)]
    public float outlineThreshold = 0.01f;
    public Color outlineColor = Color.black;
    [Range(0f,1f)]
    public float outlineOpacity = 0f;
    public bool enableOutlines = true;

    // Start is called before the first frame update
    void Start()
    {
        Edge.SetFloat("_Edge_Threshold", edgeThreshold);
        Edge.SetColor("_Edge_Color", edgeColor);
        Edge.SetFloat("_Edge_Opacity", edgeOpacity);

        if (colorEdges) Edge.EnableKeyword("_COLOR");
        else Edge.DisableKeyword("_COLOR");

        if (enableEdges) Edge.EnableKeyword("_ENABLE");
        else Edge.DisableKeyword("_ENABLE");

        Outline.SetFloat("_Outline_Threshold", outlineThreshold);
        Outline.SetColor("_Outline_Color", outlineColor);
        Outline.SetFloat("_Outline_Opacity", outlineOpacity);

        if (enableOutlines) Outline.EnableKeyword("_ENABLE");
        else Outline.DisableKeyword("_ENABLE");
    }

    private void OnValidate()
    {
        Edge.SetFloat("_Edge_Threshold", edgeThreshold);
        Edge.SetColor("_Edge_Color", edgeColor);
        Edge.SetFloat("_Edge_Opacity", edgeOpacity);

        if (colorEdges) Edge.EnableKeyword("_COLOR");
        else Edge.DisableKeyword("_COLOR");


        if (enableEdges) Edge.EnableKeyword("_ENABLE");
        else Edge.DisableKeyword("_ENABLE");

        Outline.SetFloat("_Outline_Threshold", outlineThreshold);
        Outline.SetColor("_Outline_Color", outlineColor);
        Outline.SetFloat("_Outline_Opacity", outlineOpacity);

        if (enableOutlines) Outline.EnableKeyword("_ENABLE");
        else Outline.DisableKeyword("_ENABLE");
    }

    private void Update()
    {
        if (enableEdges) Edge.EnableKeyword("_ENABLE");
        else Edge.DisableKeyword("_ENABLE");

        if (enableOutlines) Outline.EnableKeyword("_ENABLE");
        else Outline.DisableKeyword("_ENABLE");
    }
}
