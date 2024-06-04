using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UserInterface : MonoBehaviour
{
    // HpBar Slider�� �����ϱ� ���� Slider ��ü
    [SerializeField] private Slider _hpBar;

    // �÷��̾��� HP
    public int _hp;

    public int Hp
    {
        get => _hp;
        // HP�� PlayerController������ ���� �ϵ��� private���� ó��
        // Math.Clamp �Լ��� ����ؼ� hp�� 0���� �Ʒ��� �������� �ʰ� �մϴ�.
        private set => _hp = Math.Clamp(value, 0, _hp);
    }

    private void Awake()
    {
        _hp = 100;
        // MaxValue�� �����ϴ� �Լ��Դϴ�.
        SetMaxHealth(_hp);
    }

    public void SetMaxHealth(int health)
    {
        _hpBar.maxValue = health;
        _hpBar.value = health;
    }

    // �÷��̾ ������� ������ ����� ���� ���� �޾� HP�� �ݿ��մϴ�.
    public void GetDamage(int damage)
    {
        int getDamagedHp = Hp - damage;
        Hp = getDamagedHp;
        _hpBar.value = Hp;
    }
}