using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 주어진 Gun 오브젝트를 쏘거나 재장전
// 알맞은 애니메이션을 재생하고 IK를 사용해 캐릭터 양손이 총에 위치하도록 조정
public class PlayerShooter : MonoBehaviour
{
    public enum AimState
    {
        Idle,
        HipFire
    }

    public AimState aimState { get; private set; }

    public Gun gun; // 사용할 총
    public LayerMask excludeTarget;

    public GameObject p_Grenade;
    public float GrenadeThrowpower = 10;

    private bool isThrowGrenade = false;

    private PlayerInput playerInput;
    private Animator playerAnimator; // 애니메이터 컴포넌트
    private Camera playerCamera;

    private float waitingTimeForReleasingAim = 2.5f;
    private float lastFireInputIime;

    private Vector3 aimPoint;
    private bool linedUp => !(Mathf.Abs(playerCamera.transform.eulerAngles.y - transform.eulerAngles.y) > 1f);
    private bool hasEnoughDistance => !Physics.Linecast(transform.position + Vector3.up * gun.fireTransform.position.y, gun.fireTransform.position, ~excludeTarget);

    void Awake()
    {
        if (excludeTarget != (excludeTarget | (1 << gameObject.layer)))
        {
            excludeTarget |= 1 << gameObject.layer;
        }
    }

    private void Start()
    {
        playerCamera = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        playerAnimator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        aimState = AimState.Idle;
        gun.gameObject.SetActive(true);
        gun.Setup(this);
    }

    private void OnDisable()
    {
        aimState = AimState.Idle;
        gun.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        //if (playerInput.fire)
        //{
        //    lastFireInputIime = Time.time;
        //    Shoot();
        //}
        //else if (playerInput.reload)
        //{
        //    Reload();
        //}
        //else if (playerInput.throwG)
        //{
        //    ThrowGrenade();
        //}
    }

    private void Update()
    {
        UpdateAimTarget();

        var angle = playerCamera.transform.eulerAngles.x;
        if (angle > 270f) angle -= 360f;

        angle = angle / 180f * -1f + 0.5f;
        //Debug.Log(angle);
        playerAnimator.SetFloat("Angle", angle);

        if (!playerInput.fire && Time.time >= lastFireInputIime + waitingTimeForReleasingAim)
        {
            aimState = AimState.Idle;
        }

        //UpdateUI();

        if (playerInput.fire)
        {
            lastFireInputIime = Time.time;
            Shoot();
        }
        else if (playerInput.reload)
        {
            Reload();
        }
        else if (playerInput.throwG)
        {
            ThrowGrenade();
        }
    }

    public void Shoot()
    {
        if (aimState == AimState.Idle)
        {
            if (linedUp) aimState = AimState.HipFire;
        }
        else if (aimState == AimState.HipFire)
        {
            if (hasEnoughDistance)
            {
                gun.Fire(aimPoint);
                //if (gun.Fire(aimPoint)) playerAnimator.SetTrigger("Shoot");
            }
            else
            {
                aimState = AimState.Idle;
            }
        }
    }

    public void Reload()
    {
        // 재장전 입력 감지시 재장전
        if (gun.Reload()) playerAnimator.SetTrigger("Reload");
    }

    public void ThrowGrenade()
    {
        //Debug.Log("throwInput");
        if (gun.state != Gun.State.Reloading && !isThrowGrenade && PlayerInfo.Instance.grenade > 0)
        {
            PlayerInfo.Instance.grenade -= 1;
            isThrowGrenade = true;
            playerAnimator.SetTrigger("Throw");

            StartCoroutine(MakeGrenade());
        }
    }
    private IEnumerator MakeGrenade()
    {
        yield return new WaitForSeconds(2.0f);
        var pos = transform.position;
        var dir = transform.forward;
        pos += dir * 3;
        pos.y += 1;
        var grenade = Instantiate(p_Grenade, pos, transform.rotation);
        var rigidGrenade = grenade.GetComponent<Rigidbody>();

        dir.y += 0.3f;
        rigidGrenade.AddForce(dir * GrenadeThrowpower, ForceMode.Impulse);
        rigidGrenade.AddTorque(Vector3.back * 10, ForceMode.Impulse);

        yield return new WaitForSeconds(2.0f);
        isThrowGrenade = false;
    }



    private void UpdateAimTarget()
    {
        RaycastHit hit;

        var ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 1f));

        if (Physics.Raycast(ray, out hit, gun.fireDistance, ~excludeTarget))
        {
            aimPoint = hit.point;
            if (Physics.Linecast(gun.fireTransform.position, hit.point, out hit, ~excludeTarget))
            {
                aimPoint = hit.point;
            }
        }
        else
        {
            aimPoint = playerCamera.transform.position + playerCamera.transform.forward * gun.fireDistance;
        }
    }

    // 탄약 UI 갱신
    //private void UpdateUI()
    //{
    //    if (gun == null || UIManager.Instance == null) return;

    //    // UI 매니저의 탄약 텍스트에 탄창의 탄약과 남은 전체 탄약을 표시
    //    UIManager.Instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);

    //    UIManager.Instance.SetActiveCrosshair(hasEnoughDistance);
    //    UIManager.Instance.UpdateCrossHairPosition(aimPoint);
    //}

    // 애니메이터의 IK 갱신
    private void OnAnimatorIK(int layerIndex)
    {
        if (gun == null || gun.state == Gun.State.Reloading) return;

        // IK를 사용하여 왼손의 위치와 회전을 총의 오른쪽 손잡이에 맞춘다
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand,
            gun.leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand,
            gun.leftHandMount.rotation);
    }
}