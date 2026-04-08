using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Task3_Lcm.Controllers
{
    [Route("api")]
    [ApiController]
    public class LcmController : ControllerBase
    {
        [Produces("text/plain")]
        [HttpGet("ebrar_guzel26_gmail_com")]
        public string Get(string? x, string? y)
        {
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
                return "NaN";

            if (!long.TryParse(x, out var a) || !long.TryParse(y, out var b))
                return "NaN";

            if (a < 0 || b < 0)
                return "NaN";

            if (a == 0 || b == 0)
                return "0";

            return (a / Gcd(a, b) * b).ToString();
        }

        private long Gcd(long a, long b)
        {
            while (b != 0)
            {
                long t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
    }
}
