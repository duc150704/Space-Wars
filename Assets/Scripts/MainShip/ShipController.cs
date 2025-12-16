using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] int _gunPower;
    [SerializeField] int _maxGunPower;

    [SerializeField] float _speed;
    [SerializeField] float _knockBackForce;
    [SerializeField] float _freezeTime;
    float _freezeTimeCounter = 0;
    bool _canMove = false;

    [SerializeField] GameObject _explEffect;
    [SerializeField] GameObject _currentProjectile;
    [SerializeField] Animator _engineAnimator;

    Rigidbody2D _rigidbody2D;

    [SerializeField] Transform _middleGun;
    [SerializeField] Transform _leftGun;
    [SerializeField] Transform _rightGun;

    private void Start()
    {
        Cursor.visible = false;
        EventManager.Subscribe(EEventType.StartPlaying, OnGameStart);
        _rigidbody2D = GetComponent<Rigidbody2D>();

        EventManager.Subscribe(EEventType.PlayerRespawn, OnGameStart);
        OnGameStart();
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
            SoundsManager.PlaySound(ESoundType.Bullet1);
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
        yield return new WaitForSeconds(1.5f);
        EventManager.Notify(EEventType.ShieldOn);
        float time = 2f;
        Vector3 desPos = new Vector3(0f, -8f, 0f);
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
        switch (_gunPower)
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
        EventManager.Notify(EEventType.PlayerDead);
        SoundsManager.PlaySound(ESoundType.ShipExpl);
        GameObject go = PoolsManager.Instance.TakeObjFromPool(_explEffect);
        go.transform.position = transform.position;
        go.transform.localScale = new Vector3(3f, 3f, 1f);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEventType.StartPlaying, OnGameStart);

        EventManager.Unsubscribe(EEventType.PlayerRespawn, OnGameStart);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destruction();
            collision.gameObject.GetComponent<Enemy>()?.GetDamage(5f);
        }
    }

    public void PowerUp()
    {
        if (_gunPower < _maxGunPower)
            _gunPower++;
    }
}
