using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Slider _healthSlider;

    private void Start()
    {
        _healthSlider.maxValue = _playerController.Health;
        _healthSlider.value = _playerController.Health;
    }

    public void SetHealth(float health)
    {
        _healthSlider.value = _playerController.CurrentHealth;
    }
}
