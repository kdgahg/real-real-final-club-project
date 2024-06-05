using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    // HpBar Slider를 연동하기 위한 Slider 객체
    [SerializeField] private Slider _hpBar;

    // 플레이어의 HP
    private int _hp;

    public int Hp
    {
        get => _hp;
        // HP는 PlayerController에서만 수정 하도록 private으로 처리
        // Mathf.Clamp 함수를 사용해서 hp가 0보다 아래로 떨어지지 않게 합니다.
        private set => _hp = Mathf.Clamp(value, 0, (int)_hpBar.maxValue);
    }

    private void Awake()
    {
        _hp = 100;
        SetMaxHealth(100); // MaxValue를 세팅하는 함수입니다.
    }

    public void SetMaxHealth(int health)
    {
        _hpBar.maxValue = health;
        _hpBar.value = health;
        _hp = health; // 슬라이더 값을 설정한 후 hp 값을 동기화
    }

    // 플레이어가 대미지를 받으면 대미지 값을 전달 받아 HP에 반영합니다.
    public void GetDamage(int damage)
    {
        Hp -= damage;
        _hpBar.value = Hp;
    }

    public void GetHeal(int heal)
    {
        Hp += heal;
        _hpBar.value = Hp;
    }

    private void Update()
    {
        if (_hp != (int)_hpBar.value)
        {
            _hp = (int)_hpBar.value;
        }
    }
}