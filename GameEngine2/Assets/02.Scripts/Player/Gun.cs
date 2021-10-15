using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum State
    {
        Ready,
        Empty, 
        Reloading 
    }
    public State state { get; private set; } // 현재 총의 상태
    
    private PlayerShooter gunHolder;
    private LineRenderer bulletLineRenderer; // 총알 궤적을 그리기 위한 렌더러
    
    //private AudioSource gunAudioPlayer; // 총 소리 재생기
    //public AudioClip shotClip; // 발사 소리
    //public AudioClip reloadClip; // 재장전 소리
    
    public ParticleSystem muzzleFlashEffect; // 총구 화염 효과
    public ParticleSystem shellEjectEffect; // 탄피 배출 효과
    
    public Transform fireTransform; // 총알이 발사될 위치
    public Transform leftHandMount;

    public float damage = 25; // 공격력
    public float fireDistance = 100f; // 사정거리

    public int ammoRemain = 100; // 남은 전체 탄약
    public int magAmmo; // 현재 탄창에 남아있는 탄약
    public int magCapacity = 30; // 탄창 용량

    public float timeBetFire = 0.12f; // 총알 발사 간격
    public float reloadTime = 1.8f; // 재장전 소요 시간
    
    [Range(0f, 10f)] public float maxSpread = 3f;
    [Range(1f, 10f)] public float stability = 1f;
    [Range(0.01f, 3f)] public float restoreFromRecoilSpeed = 2f;
    private float currentSpread;
    private float currentSpreadVelocity;

    private float lastFireTime; // 총을 마지막으로 발사한 시점

    private LayerMask excludeTarget;

    private void Awake()
    {
        //gunAudioPlayer = GetComponent<AudioSource>();
        bulletLineRenderer = GetComponent<LineRenderer>();

        bulletLineRenderer.positionCount = 2;
        bulletLineRenderer.enabled = false;
    }

    public void Setup(PlayerShooter gunHolder)
    {
        this.gunHolder = gunHolder;
        excludeTarget = gunHolder.excludeTarget;
    }

    private void OnEnable()
    {
        currentSpread = 0;
        magAmmo = magCapacity;
        state = State.Ready;
        lastFireTime = 0;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public bool Fire(Vector3 aimTarget)
    {
        if (state == State.Ready
            && Time.time >= lastFireTime + timeBetFire)
        {
            Debug.Log("Gun.Fire");
            //var xError = Utility.GetRandomNormalDistribution(0f, currentSpread);
            //var yError = Utility.GetRandomNormalDistribution(0f, currentSpread);


            var fireDirection = aimTarget - fireTransform.position;

            //fireDirection = Quaternion.AngleAxis(yError, Vector3.up) * fireDirection;
            //fireDirection = Quaternion.AngleAxis(xError, Vector3.right) * fireDirection;

            currentSpread += 1f / stability;

            lastFireTime = Time.time;
            Shot(fireTransform.position, fireDirection);

            return true;
        }

        return false;
    }

    // 실제 발사 처리
    private void Shot(Vector3 startPoint, Vector3 direction)
    {
        // 레이캐스트에 의한 충돌 정보를 저장하는 컨테이너
        RaycastHit hit;
        var hitPosition = Vector3.zero;

        // 레이캐스트(시작지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(startPoint, direction, out hit, fireDistance, ~excludeTarget))
        {
            //var target =
            //    hit.collider.GetComponent<IDamageable>();

            //if (target != null)
            //{
            //    DamageMessage damageMessage;

            //    damageMessage.damager = gunHolder.gameObject;
            //    damageMessage.amount = damage;
            //    damageMessage.hitPoint = hit.point;
            //    damageMessage.hitNormal = hit.normal;

            //    // 상대방의 OnDamage 함수를 실행시켜서 상대방에게 데미지 주기
            //    target.ApplyDamage(damageMessage);
            //}
            //else
            //{
            //    EffectManager.Instance.PlayHitEffect(hit.point, hit.normal, hit.transform);
            //}

            //hitPosition = hit.point;
        }
        else
        {
            hitPosition = startPoint + direction * fireDistance;
        }

        StartCoroutine(ShotEffect(hitPosition));

        //magAmmo--;
        //if (magAmmo <= 0)
        //    state = State.Empty;
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        muzzleFlashEffect.Play();
        shellEjectEffect.Play();

        //gunAudioPlayer.PlayOneShot(shotClip);

        bulletLineRenderer.SetPosition(0, fireTransform.position);
        bulletLineRenderer.SetPosition(1, hitPosition);
        bulletLineRenderer.enabled = true;

        yield return new WaitForSeconds(0.03f);

        bulletLineRenderer.enabled = false;
    }

    public bool Reload()
    {
        if (state == State.Reloading ||
            ammoRemain <= 0 || magAmmo >= magCapacity)
            // 이미 재장전 중이거나, 남은 총알이 없거나
            // 탄창에 총알이 이미 가득한 경우 재장전 할수 없다
            return false;

        StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        state = State.Reloading;
        //gunAudioPlayer.PlayOneShot(reloadClip);

        yield return new WaitForSeconds(reloadTime);

        var ammoToFill = magCapacity - magAmmo;

        if (ammoRemain < ammoToFill) ammoToFill = ammoRemain;

        magAmmo += ammoToFill;
        ammoRemain -= ammoToFill;

        state = State.Ready;
    }

    private void Update()
    {
        currentSpread = Mathf.SmoothDamp(currentSpread, 0f, ref currentSpreadVelocity, 1f / restoreFromRecoilSpeed);
        currentSpread = Mathf.Clamp(currentSpread, 0f, maxSpread);
    }
}