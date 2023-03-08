using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLevels : MonoBehaviour
{
    public Material unlockedMat;
    private MeshRenderer _meshRenderer;
    private SpinObject spinObjectScript;
    public string key;

    private void Awake()
    {
        //Conect the scripts
        _meshRenderer = GetComponent<MeshRenderer>();
        spinObjectScript = GetComponent<SpinObject>();
    }
    // Start is called before the first frame update
    void Start()
    {       
        if (PlayerPrefs.GetInt(key) == 3)//cahnge the material and active the level
        {
            spinObjectScript.enabled = true;
            spinObjectScript.activePlanet = true;
            _meshRenderer.material = unlockedMat;
        }     
    }
}
