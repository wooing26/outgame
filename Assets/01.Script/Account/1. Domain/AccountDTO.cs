public class AccountDTO
{
    public string Email;
    public string Nickname;
    public string Password;

    // ���� ������ (�Է°��� ����)
    public AccountDTO(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }

    // ������ ��ü ��� ������
    public AccountDTO(Account account)
    {
        Email = account.Email;
        Nickname = account.Nickname;
        Password = account.Password;
    }
}
