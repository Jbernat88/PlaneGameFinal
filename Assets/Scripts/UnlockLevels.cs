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
        _meshRenderer = GetComponent<MeshRenderer>();
        spinObjectScript = GetComponent<SpinObject>();
    }
    // Start is called before the first frame update
    void Start()
    {       
        if (PlayerPrefs.GetInt(key) == 3)//canvia es material i activa es nivell
        {
            spinObjectScript.enabled = true;
            spinObjectScript.activePlanet = true;
            _meshRenderer.material = unlockedMat;
        }
       
    }
}
