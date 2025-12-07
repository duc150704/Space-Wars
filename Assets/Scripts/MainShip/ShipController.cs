using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] int GunPower;

    [SerializeField] float _speed;
    [SerializeField] float _knockBackForce;
    [SerializeField] float _freezeTime;
    float _freezeTimeCounter = 0;
    bool _canMove = false;

    [SerializeField] GameObject _currentProjectile;
    [SerializeField] Animator _engineAnimator;

    Rigidbody2D _rigidbody2D;

    [SerializeField] Transform _middleGun;
    [SerializeField] Transform _leftGun;
    [SerializeField] Transform _rightGun;

    private void Start()
    {
        Cursor.visible = false;
        EventManager.Subscribe(EEventType.GameStart, OnGameStart);
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!_canMove)
            return;
        Move();
        _freezeTimeCounter += Time.deltaTime;
        if (InputManager.Instance.IsShootinButtonPressed() && _freezeTimeCounter >= _freezeTime)
        {
            Shoot();
            SoundsManager.Instance.PlayMainShipShootingSound();
            KnockBack();
            _freezeTimeCounter = 0;
        }
    }
    public void OnGameStart()
    {
        StartCoroutine(GameStart_IE());
    }

    IEnumerator GameStart_IE()
    {
        float time = 2f;
        Vector3 desPos = new Vector3(0f, -6f, 0f);
        while(!_canMove && Vector3.Distance(desPos, transform.position) >= 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, desPos, 5f * time * Time.deltaTime);
            yield return null;
        }
        _canMove = true;
    }

    void Move()
    {
        Vector3 mousePosition = InputManager.Instance.GetMousePositon();
        transform.position = Vector3.Lerp(transform.position, mousePosition, _speed);
        _engineAnimator.SetBool("isPowering", (Vector3.Distance(mousePosition, transform.position) > 0.1f) ? true : false);
    }

    void KnockBack()
    {
        StartCoroutine(Push());
    }

    IEnumerator Push()
    {
        _rigidbody2D.AddForce(Vector3.down * _knockBackForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        _rigidbody2D.velocity = Vector3.zero;
    }

    void Shoot()
    {
        switch (GunPower)
        {
            case 1:
                CreatePojectiles(_middleGun.position);
                break;
            case 2:
                CreatePojectiles(_leftGun.position);
                CreatePojectiles(_rightGun.position);
                break;
            case 3:
                CreatePojectiles(_middleGun.position);
                CreatePojectiles(_leftGun.position);
                CreatePojectiles(_rightGun.position);
                break;
            default:
                CreatePojectiles(_middleGun.position);
                break;  
        }
    }

    void CreatePojectiles(Vector3 position)
    {
        GameObject poj = PoolsManager.Instance.TakeObjFromPool(_currentProjectile);
        poj.transform.position = position;
    }

    public void Destruction()
    {
        Destroy(gameObject);
    }
}
