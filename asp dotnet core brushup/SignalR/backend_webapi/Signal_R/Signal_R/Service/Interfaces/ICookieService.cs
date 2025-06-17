namespace Signal_R.Service.Interfaces
{
    public interface ICookieService
    {
        void AppendCookies(CookieOptions options, HttpResponse response, string token);
        CookieOptions GetCookieOptions();
    }
}
