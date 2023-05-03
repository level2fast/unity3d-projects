using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 _startPosition;
    [SerializeField] Vector3 _movementPosition;
    //[SerializeField][Range(0, 1)] float _movementFactor;
    float _movementFactor;
    [SerializeField] float period = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        Debug.Log(_startPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) return;
        float cycles = Time.time / period; // continually growing over time
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1
        _movementFactor = (rawSinWave + 1.0f) / 2.0f; // go from 0 t0 1 
        Vector3 offset = _movementPosition * _movementFactor;
        transform.position = _startPosition + offset;
    }
}
