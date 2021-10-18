using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager I;
    public static int PlayerPoints = 0;

    [SerializeField] private PlayerController _playerController;
    [SerializeField] private TMP_Text _points;

    [SerializeField] private Sprite _brokeHeart;
    [SerializeField] private Image[] hearts;

    private Sprite _defaultSprite;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        _defaultSprite = hearts[0].sprite;
    }

    private void Update()
    {
        _points.SetText($"{ PlayerPoints }");

        if (_playerController.CurrentHealth <= 300) {
            hearts[0].sprite = _brokeHeart;
        }
        if (_playerController.CurrentHealth <= 200) {
            hearts[1].sprite = _brokeHeart;
        }
        if (_playerController.CurrentHealth <= 100) {
            hearts[2].sprite = _brokeHeart;
        }
        if (_playerController.CurrentHealth <= 0) {
            hearts[3].sprite = _brokeHeart; ;
        }
    }

    public void ResetHearts()
    {
        for (int i = 0; i < hearts.Length; i++) {
            hearts[i].sprite = _defaultSprite;
        }
    }
}
