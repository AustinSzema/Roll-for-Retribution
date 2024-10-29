using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorMaterialAssignment : MonoBehaviour
{

    [SerializeField] private Material[] _CloakMaterials;
    [SerializeField] private Material[] _MaskMaterials;

    // Start is called before the first frame update
    void Start()
    {
        if (_CloakMaterials.Length > 0 && _MaskMaterials.Length > 0)
        {
            Material randCloakMat = _CloakMaterials[Random.Range(0, _CloakMaterials.Length)];
            Material randMaskMat = _MaskMaterials[Random.Range(0, _MaskMaterials.Length)];
            List<Material> randMats = new List<Material>();
            randMats.Add(randCloakMat);
            randMats.Add(randMaskMat);
            gameObject.GetComponent<MeshRenderer>().SetMaterials(randMats);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
