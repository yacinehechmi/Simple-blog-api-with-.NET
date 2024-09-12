namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;
/*
    Used to authenticate users

    !! this will be restricted !!
*/

public record UserLoginDTO(
        [Required] string Username,
        [Required] string Password,
        [Required] DateTime LastLogin);
