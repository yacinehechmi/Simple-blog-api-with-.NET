namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;
/*
    this DTO is used in a restirced endpoint
    that would work only if a user is authenticated

    used for quering user credentials 

    !! this will be restricted !!

    a User model is mapped to a UserCredentialsDTO
*/

public record UserDTO(
        [Required] int Id,
        [Required] string Username,
        [Required] string Password,
        [Required] string Email,
        [Required] string Role);
