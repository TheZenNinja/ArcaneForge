using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Crafting;

public class IngotDispenser : MonoBehaviour
{
    public TextMeshProUGUI text;
    public MetalMaterial material;
    public Vector3 offset;
    AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void SpawnIngot()
    {
        var i = IngotGameObject.Spawn(new CraftingMaterialID(material), transform.TransformPoint(offset));
        audio.Play();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.TransformPoint(offset), .1f);
    }
    private void OnValidate()
    {
        if (text)
            text.text = material.ToString().Capitalize() + " Ingot";
    }
}
