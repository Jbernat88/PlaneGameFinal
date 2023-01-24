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

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Takeoff()
    {
        yield return new WaitForSeconds(10f);
        playerAnimator.SetBool("Despegue", true);
    }

    public IEnumerator Return()
    {
        yield return new WaitForSeconds(10f);
        playerAnimator.SetBool("Despegue", true);
    }
}
