using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Crafting;
public class EasterEgg : MonoBehaviour
{
    public Transform cam;
    public AudioClip sound;
    public void Update()
    {
        if (Input.GetKey(KeyCode.X) && Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.N))
        {
            var i = IngotGameObject.Spawn(new CraftingMaterialID(MetalMaterial.silver), cam.position + cam.forward * 2);
            i.GetComponent<Rigidbody>().AddForce(cam.forward * 25, ForceMode.Impulse);
            AudioSource.PlayClipAtPoint(sound, cam.position + cam.forward);
        }
    }
}
