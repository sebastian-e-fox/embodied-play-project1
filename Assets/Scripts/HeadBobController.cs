using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField] private bool _enable = true;

    [SerializeField, Range(0, 0.1f)] private float _normalAmplitude = 0.0025f;
    [SerializeField, Range(0, 30)] private float _normalFrequency = 10.0f;

    [SerializeField, Range(0, 0.1f)] private float _sprintAmplitude = 0.005f;
    [SerializeField, Range(0, 30)] private float _sprintFrequency = 18.0f;

    private float _amplitude;
    private float _frequency;

    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    public bool IsSprinting { get; set; } // ← This gets set by PlayerMove

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;

        _amplitude = _normalAmplitude;
        _frequency = _normalFrequency;
    }

    private void Update()
    {
        if (!_enable) return;

        // Smooth transition between normal and sprinting values
        _amplitude = Mathf.Lerp(_amplitude, IsSprinting ? _sprintAmplitude : _normalAmplitude, 10 * Time.deltaTime);
        _frequency = Mathf.Lerp(_frequency, IsSprinting ? _sprintFrequency : _normalFrequency, 10 * Time.deltaTime);

        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _amplitude * 2;
        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }

    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
