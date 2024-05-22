namespace Auth.Api.Services;

public class AuthService(ApplicationDbContext dbContext,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IJwtTokenGenerator jwtTokenGenerator):IAuthService
{
    public async Task<(UserDto User,string ErrorMessage)> Register(RegistrationRequestDto registrationRequestDto)
    {
        ApplicationUser user = new()
        {
            UserName = registrationRequestDto.Email,
            Email = registrationRequestDto.Email,
            NormalizedEmail = registrationRequestDto.Email.ToUpper(),
            Name = registrationRequestDto.Name,
            PhoneNumber = registrationRequestDto.PhoneNumber
        };

        var result = await userManager.CreateAsync(user, registrationRequestDto.Password);
        if (!result.Succeeded) return (null,result.Errors.FirstOrDefault().Description);
        var userToReturn = dbContext.ApplicationsUsers.First(u => u.UserName == registrationRequestDto.Email);
        var userDto = new UserDto(userToReturn.Id, userToReturn.Email, userToReturn.Name, userToReturn.PhoneNumber);
        return (userDto,null);
    }

    public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
    {
        var user =await dbContext.ApplicationsUsers.FirstOrDefaultAsync(u =>
            string.Equals(u.UserName.ToLower(), loginRequestDto.Username.ToLower(), StringComparison.Ordinal));

        var isValid = await userManager.CheckPasswordAsync(user,loginRequestDto.Password);

        if(user==null || isValid == false)
        {
            return new LoginResponseDto(null, string.Empty);
        }
        //if user was found , Generate JWT Token
        var roles = await userManager.GetRolesAsync(user);
        var token = jwtTokenGenerator.GenerateToken(user,roles);

        var userDto = new UserDto(user.Id, user.Email, user.Name, user.PhoneNumber);
        var loginResponseDto = new LoginResponseDto(userDto,token);
        return loginResponseDto;}

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = await dbContext.ApplicationsUsers.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        if (user == null) return false;
        if (!roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
            //create role if it does not exist
            roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
        await userManager.AddToRoleAsync(user, roleName);
        return true;
    }
}

    
