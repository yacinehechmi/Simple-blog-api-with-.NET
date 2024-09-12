namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;

/*
    Used for inserting new Users inside a user context
    by default the Role field will be filled by "standard" value

    !! this will be restricted !!

    then CreateUserDTO will be mapped into a Comment User
*/

public record CreateUserDTO(
        [Required] string Username,
        [Required] string Password,
        string Role, // always going to be "standard", only the admin can change it
        [Required] string Email);
