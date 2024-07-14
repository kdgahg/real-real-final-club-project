using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    // HpBar Slider�� �����ϱ� ���� Slider ��ü
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _StBar;
    // �÷��̾��� HP
    public int _hp;
    public float _st;
    public float RegenS;


    public int Hp
    {
        get => _hp;
        private set => _hp = Mathf.Clamp(value, 0, (int)_hpBar.maxValue);
    }

    private void Awake()
    {
        _hp = 100;
        SetMaxHealth(100); // MaxValue�� �����ϴ� �Լ��Դϴ�.
        SetMaxStamina(100);
    }

    public void SetMaxHealth(int health)
    {
        _hpBar.maxValue = health;
        _hpBar.value = health;
        _hp = health; // �����̴� ���� ������ �� hp ���� ����ȭ
    }
    public void SetMaxStamina(int Stamina)
    {
        _StBar.maxValue = Stamina;
        _StBar.value = Stamina;
        _st = Stamina; // �����̴� ���� ������ �� hp ���� ����ȭ
    }
    public void UseStamina(int stamina)
    {
        _StBar.value -= stamina;
        _st -= stamina;
    }
    public void UseHp(int p_Hp)
    {
        _hpBar.value -= p_Hp;
        _hp -= p_Hp;
    }

    // �÷��̾ ������� ������ ����� ���� ���� �޾� HP�� �ݿ��մϴ�.
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
    public void RegenStamina(float Regen)
    {
        _StBar.value += Regen * 1 / 10;
        _st += Regen * 1 / 10;
    }

    private void Update()
    {
        if (_hp != (int)_hpBar.value)
        {
            _hp = (int)_hpBar.value;
        }
    }
    private void FixedUpdate()
    {
        RegenStamina(RegenS);
    }
}