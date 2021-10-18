using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static int PlayerPoints = 0;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TMP_Text _points;

    private void Start()
    {
        _healthSlider.maxValue = _playerController.Health;
        _healthSlider.value = _playerController.Health;
    }

    private void Update()
    {
        _points.SetText($"{ PlayerPoints }");
    }

    public void SetHealth(float health)
    {
        _healthSlider.value = _playerController.CurrentHealth;
    }
}
