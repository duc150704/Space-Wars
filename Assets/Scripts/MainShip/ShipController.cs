using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] int _gunPower;
    public int GunPower
    {
        get => _gunPower;
        set
        {
            if(value <= 0)
            {
                _gunPower = 1;
                OnPlayerInfoChanged?.Invoke(new PlayerInforData( null, _gunPower));
                return;
            }
            _gunPower = value;
            OnPlayerInfoChanged?.Invoke(new PlayerInforData(null, _gunPower));
        }
    }
    [SerializeField] int _maxGunPower;
    [SerializeField] int _lives;
    public int Lives
    {
        get => _lives;
        set
        {
            if(value < 0)
            {
                _lives = 0;
                OnPlayerInfoChanged?.Invoke(new PlayerInforData(_lives));
                return;
            }
            _lives = value;
            OnPlayerInfoChanged?.Invoke(new PlayerInforData(_lives));
        }
    }
    [SerializeField] float _speed;
    [SerializeField] float _knockBackForce;
    [SerializeField] float _freezeTime;
    float _freezeTimeCounter = 0;
    bool _canMove = false;
    bool _hasShield = false;
    public bool HasShield
    {
        get => _hasShield;
        set => _hasShield = value;
    }

    [SerializeField] GameObject _explEffect;
    [SerializeField] GameObject _currentProjectile;
    [SerializeField] Animator _engineAnimator;

    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spriteRenderer;
    Collider2D _collider;

    [SerializeField] Transform _middleGun;
    [SerializeField] Transform _leftGun;
    [SerializeField] Transform _rightGun;

    public static event Action<PlayerInforData> OnPlayerInfoChanged;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        EventManager.Subscribe(EEventType.StartPlaying, OnGameStart);
    }
    private void Start()
    {
        OnGameStart();
        OnPlayerInfoChanged?.Invoke(new PlayerInforData(_lives, _gunPower));
    }

    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEventType.StartPlaying, OnGameStart);
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
        _canMove = false;
        yield return new WaitForSeconds(1.5f);
        EventManager.Notify(EEventType.ShieldOn);
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
        DestructionEffect();
        if( _lives > 0)
        {
            --Lives;
            GunPower -= 2;
            StartCoroutine(Respawn());
        }
        else
        {
            Destroy(gameObject);
            GameManager.Instance.ChangeState(GameManager.GameState.Lose);
            return;
        }
    }

    IEnumerator Respawn()
    {
        transform.position = GameManager.Instance.PlayerSpawnPosition;

        yield return new WaitForSeconds(1f);

        _spriteRenderer.enabled = true;
        _collider.enabled = true;
        StartCoroutine(GameStart_IE());
    }

    void DestructionEffect()
    {
        SoundsManager.PlaySound(ESoundType.ShipExpl);
        EventManager.Notify(EEventType.PlayerDead);

        GameObject go = PoolsManager.Instance.TakeObjFromPool(_explEffect);
        go.transform.position = transform.position;
        go.transform.localScale = new Vector3(3f, 3f, 1f);

        _canMove = false;
        _spriteRenderer.enabled = false;
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !_hasShield)
        {
            Destruction();
            collision.gameObject.GetComponent<Enemy>()?.GetDamage(5f);
        }

    }

    public void PowerUp()
    {
        if (_gunPower >= _maxGunPower)
            return;
        ++GunPower;
    }
}
