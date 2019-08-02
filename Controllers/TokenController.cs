using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ColaTerminal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static ColaTerminal.Controllers.LoginController;

public class TokenController : Controller
{
    private IConfiguration configuration;

    private static string SECRET_KEY;
    public static SymmetricSecurityKey SIGNING_KEY;

    private readonly traperto_kurtContext dbcontext;

    public TokenController(traperto_kurtContext dbcontext, IConfiguration configuration)
    {
        this.configuration = configuration;
        this.dbcontext = dbcontext;
        SECRET_KEY = configuration.GetValue<string>("JWTSecretKey");
        SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

    }

    public IConfiguration Configuration { get; set; }

    [HttpPost]
    [Route("api/Token/")]
    public IActionResult Post([FromBody] LoginInput userParam)
    {

        var user = this.dbcontext.User.FirstOrDefault(b => b.UserName == userParam.Username);

        if (user == null)
        {
            return NotFound("User could not be found for username: " + userParam.Username);
        }


        var hashedPassword = createHashedPassword(userParam.Password);

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
            ExpireDate = DateTime.Now.AddMinutes(60),
            UserId = user.Id
        });
    }

    // Generate a Token with expiration date and Claim meta-data.
    // And sign the token with the SIGNING_KEY
    private string GenerateToken(RestToken restToken)
    {
        var token = new JwtSecurityToken(
            claims: new Claim[] { 
                new Claim("userId", restToken.UserId.ToString()),
             },
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
        public uint UserId { get; set; }
    }

    public class RestToken
    {

        public uint UserId { get; set; }
        public DateTime ExpireDate { get; set; }
    }

    public static string createHashedPassword(string password)
    {
        SHA256 sha256Hash = SHA256.Create();
        byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }

        return builder.ToString();
    }
}