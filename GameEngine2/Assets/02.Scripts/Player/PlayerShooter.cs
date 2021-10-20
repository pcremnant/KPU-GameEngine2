using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �־��� Gun ������Ʈ�� ��ų� ������
// �˸��� �ִϸ��̼��� ����ϰ� IK�� ����� ĳ���� ����� �ѿ� ��ġ�ϵ��� ����
public class PlayerShooter : MonoBehaviour
{
    public enum AimState
    {
        Idle,
        HipFire
    }

    public AimState aimState { get; private set; }

    public Gun gun; // ����� ��
    public LayerMask excludeTarget;

    private PlayerInput playerInput;
    private Animator playerAnimator; // �ִϸ����� ������Ʈ
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
        if (playerInput.fire)
        {
            lastFireInputIime = Time.time;
            Shoot();
        }
        else if (playerInput.reload)
        {
            Reload();
        }
    }

    private void Update()
    {
        UpdateAimTarget();

        var angle = playerCamera.transform.eulerAngles.x;
        if (angle > 270f) angle -= 360f;

        angle = angle / 180f * -1f + 0.5f;

        //playerAnimator.SetFloat("Angle", angle);

        if (!playerInput.fire && Time.time >= lastFireInputIime + waitingTimeForReleasingAim)
        {
            aimState = AimState.Idle;
        }

        //UpdateUI();
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
        // ������ �Է� ������ ������
        //if (gun.Reload()) playerAnimator.SetTrigger("Reload");
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

    // ź�� UI ����
    //private void UpdateUI()
    //{
    //    if (gun == null || UIManager.Instance == null) return;

    //    // UI �Ŵ����� ź�� �ؽ�Ʈ�� źâ�� ź��� ���� ��ü ź���� ǥ��
    //    UIManager.Instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);

    //    UIManager.Instance.SetActiveCrosshair(hasEnoughDistance);
    //    UIManager.Instance.UpdateCrossHairPosition(aimPoint);
    //}

    // �ִϸ������� IK ����
    private void OnAnimatorIK(int layerIndex)
    {
        if (gun == null || gun.state == Gun.State.Reloading) return;

        // IK�� ����Ͽ� �޼��� ��ġ�� ȸ���� ���� ������ �����̿� �����
        playerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
        playerAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

        playerAnimator.SetIKPosition(AvatarIKGoal.LeftHand,
            gun.leftHandMount.position);
        playerAnimator.SetIKRotation(AvatarIKGoal.LeftHand,
            gun.leftHandMount.rotation);
    }
}