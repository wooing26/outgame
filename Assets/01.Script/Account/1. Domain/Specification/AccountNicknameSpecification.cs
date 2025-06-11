using System;
using System.Text.RegularExpressions;
using UnityEngine;

public class AccountNicknameSpecification : ISpecification<string>
{
    // �г���: �ѱ� �Ǵ� ����� ����, 2~7��
    private static readonly Regex NicknameRegex = new Regex(@"^[��-�Ra-zA-Z]{2,7}$", RegexOptions.Compiled);
    // ������ �г��� (��Ӿ� ��)
    private static readonly string[] ForbiddenNicknames = { "�ٺ�", "��û��", "���", "��ȫ��" };

    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            ErrorMessage = "�г����� ������� �� �����ϴ�.";
            return false;
        }

        if (!NicknameRegex.IsMatch(value))
        {
            ErrorMessage = "�г����� 2�� �̻� 7�� ������ �ѱ� �Ǵ� �����̾�� �մϴ�.";
            return false;
        }

        foreach (var forbidden in ForbiddenNicknames)
        {
            if (value.Contains(forbidden))
            {
                ErrorMessage = $"�г��ӿ� �������� �ܾ ���ԵǾ� �ֽ��ϴ�: {forbidden}";
                return false;
            }
        }

        return true;
    }

    public string ErrorMessage { get; private set; }
}
