using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {
        // ��Ģ�� ��ü�� ĸ��ȭ�ؼ� �и��Ѵ�.
        // �׷��� �����ΰ� UI�� ��� "�� ��Ģ�� �����ϴ�?" ������ �ȴ�.
        // ĸ��ȭ�� ��Ģ : ��

        // �̸��� ����
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // �г��� ����
        var nicknameSpecification = new AccountNicknameSpecification();
        if (!nicknameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nicknameSpecification.ErrorMessage);
        }

        

        // ��й�ȣ ����
        if (string.IsNullOrEmpty(password))
        {
            throw new Exception("��й�ȣ�� ������� �� �����ϴ�.");
        }

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDTO ToDTO()
    {
        return new AccountDTO(Email, Nickname, Password);
    }
}
