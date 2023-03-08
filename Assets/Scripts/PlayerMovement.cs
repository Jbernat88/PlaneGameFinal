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

    //Audio
    private AudioSource playerMovAudioSource;
    public AudioClip[] mov;

    void Start()
    {
        playerMovAudioSource = GetComponent<AudioSource>();

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
                playerMovAudioSource.PlayOneShot(mov[0]);
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
        }
        
        //Quick spin of the player
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E))
        {
            playerMovAudioSource.PlayOneShot(mov[1]);
            int dir = Input.GetKeyDown(KeyCode.Q) ? -1 : 1;
            QuickSpin(dir);
        }
    }

    //We change the movement to local so as not to affect the camera
    void LocalMove(float x, float y, float speed)
    {
        transform.localPosition += new Vector3(x, y, 0) * speed * Time.deltaTime;
        ClampPosition();
    }

    //Camera Limits
    void ClampPosition()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    //LOook the object (aimPosition)
    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed * Time.deltaTime);
    }

    //To improve the sensation of movement input z.
    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }

    //Draw the lines of aimPosition
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
            //Makes the player rotate quickly
            playerModel.DOLocalRotate(new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            barrel.Play();
        }
    }

    //Mantain the forward speed
    void SetSpeed(float x)
    {
        dolly.m_Speed = x;
    }

    //Zoom of the camera when the boost is active.
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

    //CoolDown of Boost
    IEnumerator BoostCoolDown()
    {
        yield return new WaitForSeconds(5f); //first 2.5 sec it removes your boost and then wait 5 sec to boost again.
        canBoost = false;

        trail.Stop();
        StartCoroutine(CoolDownSpeed());


        yield return new WaitForSeconds(5);
        isBoost = false;
    }

    //CoolDown of Speed
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
