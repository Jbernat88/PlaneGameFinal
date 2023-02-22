using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering.PostProcessing;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerModel;


    [Space]

    [Header("Parameters")]
    public float xySpeed = 18;
    public float lookSpeed = 340;
    public float forwardSpeed = 12;
    public float boostTime = 1;
    public bool canBoost;
    public bool isBoost;
    public float maxboost = 20;

    [Space]

    [Header("Public References")]
    public Transform aimTarget;
    public CinemachineDollyCart dolly;
    public Transform cameraParent;
    public CinemachineDollyCart dolly2;

    [Space]

    [Header("Particles")]
    public ParticleSystem trail;
    public ParticleSystem circle;
    public ParticleSystem barrel;
    public ParticleSystem stars;

    void Start()
    {
        //dolly.m_Speed
        playerModel = transform.GetChild(0);
        SetSpeed(forwardSpeed);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        LocalMove(h, v, xySpeed);
        RotationLook(h,v, lookSpeed);
        HorizontalLean(playerModel, h, 80, .1f);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isBoost)
            {
                canBoost = true;
                isBoost = true;

                trail.Play();
                circle.Play();

                StartCoroutine(BoostCoolDown());
            }
        }
     
        if (canBoost)
        {
            dolly2.m_Speed = maxboost;
            //dolly2.m_Speed = Mathf.Lerp(dolly.m_Speed, 20, Time.deltaTime * boostTime)      
        }
        /*
        else
        {
            dolly2.m_Speed = forwardSpeed;
           // dolly2.m_Speed = Mathf.Lerp(dolly.m_Speed, forwardSpeed, Time.deltaTime * boostTime);
        }
        */
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            int dir = Input.GetKeyDown(KeyCode.Q) ? -1 : 1;
            QuickSpin(dir);
        }
 
    }

    //Cambiamos el movimento a local para no afectar a la camara
    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }

    //Limites Camara
    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    //Mira el objeto aimPosition
    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    //Para mejorar la sensacion de movimiento nos input z.
    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    //Dibuja el objeto aimPosition 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);

    }

    public void QuickSpin(int dir)
    {
        if (!DOTween.IsTweening(playerModel))
        {
            //Hace que el player rote rapidamente.
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            barrel.Play();
        }
    }

    //Mantiene la velocidad de forward speed
    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    //Zoom de la camara cuando hacemos el boost.
    void SetCameraZoom(float zoom, float duration)
    {
        cameraParent.DOLocalMove(new Vector3(0, 0, zoom), duration);
    }

    
    void DistortionAmount(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<LensDistortion>().intensity.value = x;
    }

    void FieldOfView(float fov)
    {
        cameraParent.GetComponentInChildren<CinemachineVirtualCamera>().m_Lens.FieldOfView = fov;
    }

    void Chromatic(float x)
    {
        Camera.main.GetComponent<PostProcessVolume>().profile.GetSetting<ChromaticAberration>().intensity.value = x;
    }

    IEnumerator BoostCoolDown()
    {
        yield return new WaitForSeconds(5f); //primeros 2.5 sec te quita el boost i luego espera 5 a volver a boostear.
        canBoost = false;

        trail.Stop();
        StartCoroutine(CoolDownSpeed());


        yield return new WaitForSeconds(5);
        isBoost = false;
    }

    IEnumerator CoolDownSpeed()
    {
        float reduction = (maxboost - forwardSpeed) / 3;
        Debug.Log($"Max: {maxboost} - Forward {forwardSpeed}");

        for(int i =1; i <= 3; i++)
        {
            yield return new WaitForSeconds(1f);
            dolly2.m_Speed -= reduction;
        }

        dolly2.m_Speed = forwardSpeed;
    }


}
