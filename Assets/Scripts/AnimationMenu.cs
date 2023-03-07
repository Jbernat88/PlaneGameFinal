using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMenu : MonoBehaviour
{
    private Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        StartCoroutine(Takeoff());
    }

    //Espera 5 seg a instanciarse la animacion del player
    public IEnumerator Takeoff()
    {
        yield return new WaitForSeconds(5f);
        playerAnimator.SetBool("Despegue", true);
    }
}
