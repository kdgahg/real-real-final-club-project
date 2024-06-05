using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    // HpBar Slider�� �����ϱ� ���� Slider ��ü
    [SerializeField] private Slider _hpBar;

    // �÷��̾��� HP
    private int _hp;

    public int Hp
    {
        get => _hp;
        // HP�� PlayerController������ ���� �ϵ��� private���� ó��
        // Mathf.Clamp �Լ��� ����ؼ� hp�� 0���� �Ʒ��� �������� �ʰ� �մϴ�.
        private set => _hp = Mathf.Clamp(value, 0, (int)_hpBar.maxValue);
    }

    private void Awake()
    {
        _hp = 100;
        SetMaxHealth(100); // MaxValue�� �����ϴ� �Լ��Դϴ�.
    }

    public void SetMaxHealth(int health)
    {
        _hpBar.maxValue = health;
        _hpBar.value = health;
        _hp = health; // �����̴� ���� ������ �� hp ���� ����ȭ
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

    private void Update()
    {
        if (_hp != (int)_hpBar.value)
        {
            _hp = (int)_hpBar.value;
        }
    }
}