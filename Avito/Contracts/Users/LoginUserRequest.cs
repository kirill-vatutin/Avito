﻿using System.ComponentModel.DataAnnotations;

namespace Avito.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);

}
