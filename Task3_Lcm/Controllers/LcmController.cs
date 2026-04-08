using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Task3_Lcm.Controllers
{
    [Route("api")]
    [ApiController]
    public class LcmController : ControllerBase
    {
        [Produces("text/plain")]
        [HttpGet("/ebrar_guzel26_gmail_com")]
        public string Get(string? x, string? y)
        {
            if (string.IsNullOrEmpty(x) || string.IsNullOrEmpty(y))
                return "NaN";

            if (!BigInteger.TryParse(x, out var a) || !BigInteger.TryParse(y, out var b))
                return "NaN";

            if (a < 0 || b < 0)
                return "NaN";

            if (a == 0 || b == 0)
                return "0";

            var gcd = Gcd(a, b);
            var lcm = (a / gcd) * b;

            return lcm.ToString();
        }

        private BigInteger Gcd(BigInteger a, BigInteger b)
        {
            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
    }
}
