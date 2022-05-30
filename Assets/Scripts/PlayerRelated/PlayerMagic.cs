using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject aimArrow;

    [SerializeField] FireBall fireBall;
    PlayerInput input;

    bool aiming;

    private void Awake()
    {
        input = new PlayerInput();

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
        aimArrow.transform.rotation = Quaternion.AngleAxis(angle-90f, Vector3.forward);
        animator.SetFloat("AimingRight", Mathf.Clamp(dir.x, -1, 1));
        animator.SetFloat("AimingUp", Mathf.Clamp(dir.y, -1, 1));
    }


    void AimFireball()
    {
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
        if (aiming == false)
        {
            return;
        }
        aimArrow.SetActive(false);

        aiming = false;
        animator.SetBool("MagicAim", false);
        FireBall fireShot = Instantiate(fireBall, transform.position, Quaternion.identity);
        fireShot.gameObject.SetActive(true);
        fireShot.FollowDirection(dir);
    }
}
