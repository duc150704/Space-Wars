using System.Collections;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _knockBackForce;
    [SerializeField] float _knockBackTime;

    WaitForSeconds _time;
    Rigidbody2D _rigidbody2D;
    Transform _transform;
    Vector2 _mousePosition;

    bool _canMove = false;
    public bool CanMove => _canMove;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = this.transform;
    }

    private void OnEnable()
    {
        _time = new WaitForSeconds(_knockBackTime);
        EventManager.Subscribe(EEvent.GameStart, OnAppear);
        EventManager.Subscribe(EEvent.OnPlayerRespawn, OnRespawn);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe(EEvent.GameStart, OnAppear);
        EventManager.Unsubscribe(EEvent.OnPlayerRespawn, OnRespawn);
    }

    private void Update()
    {
        if (!_canMove)
            return;
        Move();
    }

    private void Start()
    {
        OnAppear();
    }

    public void OnAppear()
    {
        StartCoroutine(Appear_IE());
    }

    public void OnRespawn()
    {
        OnAppear();
    }

    void Move()
    {
        _mousePosition = InputManager.Instance.MousePositon();
        _transform.position = Vector3.Lerp(transform.position, _mousePosition, _speed);
    }

    public void KnockBack()
    {
        StartCoroutine(Push());
    }

    IEnumerator Push()
    {
        _rigidbody2D.AddForce(Vector2.down * _knockBackForce, ForceMode2D.Impulse);
        yield return _time;
        _rigidbody2D.velocity = Vector2.zero;
    }

    IEnumerator Appear_IE()
    {
        _transform.position = new Vector3(0f, -12f, 0f);
        _canMove = false;
        yield return new WaitForSeconds(1.5f);
        float time = 2f;
        Vector3 desPos = new Vector3(0f, -5.5f, 0f);
        while (!_canMove && Vector3.Distance(desPos, _transform.position) >= 0.1f)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, desPos, 7f * time * Time.deltaTime);
            yield return null;
        }
        _canMove = true;
    }
}
