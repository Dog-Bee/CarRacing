using UnityEngine;

public class BoostService
{
    private float _boostMaxCount;
    private float _boostUsage;
    private float _boostCount;
    private float _boostDelay;

    private bool _isBoosting;
    
    private float _boostTimer;
    
    public bool IsBoosting => _isBoosting;
    public float BoostMaxCount => _boostMaxCount;
    public float BoostCount => _boostCount;
    

    public BoostService(float boostMaxCount, float boostUsage, float boostDelay)
    {
        _boostMaxCount = boostMaxCount;
        _boostUsage = boostUsage;
        _boostDelay = boostDelay;
        _boostTimer = boostDelay;
        _boostCount = 0;
    }
    

    public void Update()
    {
        if (!_isBoosting)
        {
            _boostCount = Mathf.Clamp(_boostCount + Time.deltaTime, 0, _boostMaxCount);
            _boostTimer = Mathf.Clamp(_boostTimer-Time.deltaTime, 0, _boostDelay);
        }

        if (_isBoosting)
        {
            _boostCount = Mathf.Clamp(_boostCount-Time.deltaTime*_boostUsage, 0, _boostMaxCount);
            
            if (_boostCount == 0)
                _boostTimer = _boostDelay;
        }
        
    }

    public bool BoostAvailability()
    {
        return (_boostCount>0 && _boostTimer <= 0.1f) || _isBoosting;
    }

    public bool TryBoost(bool isBoost)
    {
        return _isBoosting = isBoost && _boostCount>0 && _boostTimer == 0;
    }
    
    
}
