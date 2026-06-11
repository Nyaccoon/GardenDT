using UnityEngine;
using System.Collections.Generic;

public class BuildingPreview : MonoBehaviour
{
    [SerializeField] private Material PositiveMaterial;
    [SerializeField] private Material NegativeMaterial;

    public Support.PreviewState _state { get; private set; } = Support.PreviewState.Negative;
    public BuildingData _data { get; private set; }
    public BuildingModel model { get; private set; }
    private List<Renderer> renderers = new();
    private List<Collider> colliders = new();

    public void Setup(BuildingData data)
    {
        _data = data;
        model = Instantiate(data.model,transform.position,Quaternion.identity,transform);
        renderers.AddRange(model.GetComponentsInChildren<Renderer>());
        colliders.AddRange(model.GetComponentsInChildren<Collider>());

        foreach(var col in colliders)
        {
            col.enabled= false;
        }
        SetPreviewMaterial(_state);
    }

    public void ChangeState(Support.PreviewState state)
    {
        if (state == _state) return;
        _state = state;
        SetPreviewMaterial(_state);
    }

    public void Rotate(int rotationStep)
    {
        model.Rotate(rotationStep);
    }

    private void SetPreviewMaterial(Support.PreviewState state)
    {
        Material previewMat = state == Support.PreviewState.Positive ? PositiveMaterial : NegativeMaterial;
        foreach(var rend in renderers)
        {
            Material[] mats = new Material[rend.sharedMaterials.Length];
            for(int i = 0; i < mats.Length; i++)
            {
                mats[i] = previewMat;
            }
            rend.materials = mats;
        }
    }
}