using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task3_Lcm.Controllers
{
    //[Route("api/[controller]")]
    [Route("ebrar_guzel26_gmail_com")]
    [ApiController]
    public class LcmController : ControllerBase
    {
        [HttpGet]
        public string Get(string x, string y)
        {
            if (!int.TryParse(x, out int a) || !int.TryParse(y, out int b) || a < 0 || b < 0)
                return "NaN";

            int Gcd(int m, int n)
            {
                while (n != 0)
                {
                    int temp = n;
                    n = m % n;
                    m = temp;
                }
                return m;
            }

            int lcm = (a == 0 || b == 0) ? 0 : (a * b) / Gcd(a, b);
            return lcm.ToString();
        }
    }
}
