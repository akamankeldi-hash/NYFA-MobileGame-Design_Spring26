using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearanceApplier : MonoBehaviour
{
    [Header("Upper body options — index matches E_UpperBody")]
    [SerializeField] private GameObject[] upperBodyOptions;

    [Header("Lower body options — index matches E_LowerBody")]
    [SerializeField] private GameObject[] lowerBodyOptions;

    [Header("Prop catalog — index matches E_PropPiece")]
    [SerializeField] private GameObject[] propCatalog;
    [Header("Face options — index matches E_Face")]
    [SerializeField] private GameObject faceGameObject;
    [SerializeField] private Material[] faceMaterials;

    public void Apply(CharacterTemplateSO template)
    {
        ApplyUpperBody(template.upperBody, template.upperBodyMaterial);
        ApplyLowerBody(template.lowerBody, template.lowerBodyMaterial);
        ApplyProps(template.activeProps, template.propMaterialOverrides);
        ApplyFace(template.face);
    }

    private void ApplyUpperBody(E_UpperBody choice, Material overrideMaterial)
    {
        for (int i = 0; i < upperBodyOptions.Length; i++)
        {
            if (upperBodyOptions[i] == null) continue;
            bool isActive = i == (int)choice;
            upperBodyOptions[i].SetActive(isActive);
        }

        if (overrideMaterial != null)
            ApplyMaterial(upperBodyOptions[(int)choice], overrideMaterial);
    }

    private void ApplyLowerBody(E_LowerBody choice, Material overrideMaterial)
    {
        for (int i = 0; i < lowerBodyOptions.Length; i++)
        {
            if (lowerBodyOptions[i] == null) continue;
            bool isActive = i == (int)choice;
            lowerBodyOptions[i].SetActive(isActive);
        }

        if (overrideMaterial != null)
            ApplyMaterial(lowerBodyOptions[(int)choice], overrideMaterial);
    }

    private void ApplyProps(List<CharacterTemplateSO.PropUnlockEntry> active, List<CharacterTemplateSO.PropMaterialOverride> overrides)
    {
        for (int i = 0; i < propCatalog.Length; i++)
        {
            if (propCatalog[i] != null)
                propCatalog[i].SetActive(false);
        }

        if (active == null) return;

        foreach (var entry in active)
        {
            int index = (int)entry.prop;
            if (index < propCatalog.Length && propCatalog[index] != null)
            {
                propCatalog[index].SetActive(true);

                var propItem = propCatalog[index].GetComponent<PropItem>();
                if (propItem != null)
                    propItem.SetUnlockTarget(entry.unlockTarget);
            }
        }

        if (overrides == null) return;

        foreach (var entry in overrides)
        {
            int index = (int)entry.prop;
            if (index < propCatalog.Length && propCatalog[index] != null && entry.material != null)
                ApplyMaterial(propCatalog[index], entry.material);
        }
    }

    private void ApplyFace(E_Face choice)
    {
        if (faceGameObject == null) return;

        for (int i = 0; i < faceMaterials.Length; i++)
        {
            if (faceMaterials[i] == null) continue;
            
            Renderer renderer = faceGameObject.GetComponent<Renderer>();
            if (renderer != null)
                renderer.sharedMaterial = faceMaterials[(int)choice];
        }
    }

    private void ApplyMaterial(GameObject obj, Material material)
    {
        if (obj == null) return;
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
            renderer.sharedMaterial = material;
    }
}