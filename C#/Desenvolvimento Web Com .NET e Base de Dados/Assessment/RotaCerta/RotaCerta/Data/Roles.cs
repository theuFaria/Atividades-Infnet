using Microsoft.AspNetCore.Identity;

namespace RotaCerta.Data;

public static class Roles
{
    public static async Task CriarRoles(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        string[] roles = { "Admin", "User" };

        foreach (var role in roles)
        {
            //Verifica se a role já existe, se não existir, cria ela.
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
}