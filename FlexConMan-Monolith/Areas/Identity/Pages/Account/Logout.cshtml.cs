// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using FlexConMan_Monolith.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FlexConMan_Monolith.Areas.Identity.Pages.Account;

public class LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger)
    : PageModel
{
    public async Task<IActionResult> OnPost(string returnUrl = null)
    {
        await signInManager.SignOutAsync();
        logger.LogInformation("User logged out.");
        return RedirectToPage("/Account/Login");
    }
}