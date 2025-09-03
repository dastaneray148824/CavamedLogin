namespace CavamedLogin.Services.Security;

public interface ILoginAttemptStore
{
    int Increment(string key); // artırır ve güncel değeri döner
    void Reset(string key);
    int Get(string key);
}
