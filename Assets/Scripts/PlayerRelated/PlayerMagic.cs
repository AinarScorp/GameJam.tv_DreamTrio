using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class PlayerMagic : MonoBehaviour
{
    [SerializeField] int startingFireballs = 1;
    [Header("Better not touch")]

    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] Animator animator;
    [SerializeField] GameObject aimArrow;

    [SerializeField] List<CanvasGroup> fireBallImages = new List<CanvasGroup>();
    [SerializeField] FireBall fireBall;
    [SerializeField] Transform parentForFireballs;


    int maxShots = 3;
    int availableFireballs;
    PlayerInput input;
    PlayerManager playerManager;
    bool aiming;



    private void Awake()
    {
        input = new PlayerInput();
        playerManager = FindObjectOfType<PlayerManager>();

    }
    private void OnEnable() => input.Enable();

    private void OnDisable()
    {
        PlayerBasicAttack.Instance.FinishAttack();
        input.Disable();
    }
    private void Start()
    {
        input.PlayerBasic.MagicAttack.performed += _ => AimFireball();
        input.PlayerBasic.MagicAttack.canceled += _ => ShootFireball();
        aimArrow.SetActive(false);
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        playerManager.SubscribeToImmidiateActions(() => this.enabled = false, true);
        playerManager.SubscribeToActivateControls( Revive, false);
        availableFireballs = startingFireballs;
        HighlightAlpha(availableFireballs);
    }
    Vector3 dir;

    private void Update()
    {
        if (!aiming)
            return;
        Vector3 mousePos = input.PlayerBasic.MousePos.ReadValue<Vector2>();
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //mousePos -= transform.position;
        //var angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //aimArrow.transform.rotation.eulerAngles.z = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //aimArrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
        dir = (mousePos - pos).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        aimArrow.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        animator.SetFloat("AimingRight", Mathf.Clamp(dir.x, -1, 1));
        animator.SetFloat("AimingUp", Mathf.Clamp(dir.y, -1, 1));

    }


    void AimFireball()
    {
        if (availableFireballs<= 0)
        {
            return;
        }
        animator.SetBool("CanMove", false);
        if (!animator.GetBool("MagicAim"))
        {
            aimArrow.SetActive(true);

            animator.SetBool("MagicAim", true);
            aiming = true;
        }
    }
    void ShootFireball()
    {
        if (aiming == false || !playerHealth.IsAlive)
        {
            return;
        }
        aimArrow.SetActive(false);
        availableFireballs--;
        aiming = false;
        animator.SetBool("MagicAim", false);
        FireBall fireShot = Instantiate(fireBall, transform.position, Quaternion.identity);
        fireShot.gameObject.SetActive(true);
        fireShot.FollowDirection(dir);
        DimOneFireBallImages();
    }

    void Revive()
    {
        this.enabled = true;
        availableFireballs += playerManager.GetCollectedFireBalls();
        if (availableFireballs > maxShots)
        {
            availableFireballs = maxShots;
        }
        DimFireballImages();
        HighlightAlpha(availableFireballs);
    }
    private void HighlightAlpha(int fireballsToHighlight)
    {
        for (int i = 0; i < fireballsToHighlight; i++)
        {
            fireBallImages[i].alpha = 1f;
        }
    }
    void DimOneFireBallImages()
    {
        fireBallImages.ToList().FirstOrDefault().alpha = 0.3f;
    }
    private void DimFireballImages()
    {

        fireBallImages.ToList().ForEach(image => image.alpha = 0.3f);
    }
}
