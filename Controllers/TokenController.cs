using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static ColaTerminal.Controllers.LoginController;

public class TokenController : Controller
{
    private const string SECRET_KEY = "TQvgjeABMPOwCycOqah5EQu5yyVjpmVG";
    public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

    private readonly traperto_kurtContext dbcontext;

    public TokenController(traperto_kurtContext dbcontext)
    {
        this.dbcontext = dbcontext;
    }

    [HttpPost]
    [Route("api/Token/")]
    public IActionResult Post([FromBody] LoginInput userParam)
    {
        Console.WriteLine(userParam.Password);

        var user = this.dbcontext.User.FirstOrDefault(b => b.UserName == userParam.Username);

        if (user == null)
        {
            return NotFound("User could not be found for username: " + userParam.Username);
        }

        SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(userParam.Password));

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }

        var hashedPassword = builder.ToString();


        if (hashedPassword != user.Password)
        {
            return BadRequest("Incorrect password entered!");
        }

        return Ok(new JwtToken()
        {
            Token = GenerateToken(new RestToken()
            {
                UserId = user.Id,
                ExpireDate = DateTime.Now.AddMinutes(60)
            }),
            ExpireDate = DateTime.Now.AddMinutes(60)
        });
    }

    // Generate a Token with expiration date and Claim meta-data.
    // And sign the token with the SIGNING_KEY
    private string GenerateToken(RestToken restToken)
    {
        var token = new JwtSecurityToken(
            claims: new Claim[] { new Claim(ClaimTypes.Name, restToken.UserId.ToString()) },
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
            signingCredentials: new SigningCredentials(SIGNING_KEY,
                                                SecurityAlgorithms.HmacSha256)
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public class JwtToken
    {
        public string Token { get; set; }

        public DateTime ExpireDate { get; set; }
    }

    public class RestToken
    {

        public uint UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}