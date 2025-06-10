namespace interview_prep.Services.Interfaces;

public interface ICookieService
{
    void AppendCookies(HttpResponse response, string authToken);
}