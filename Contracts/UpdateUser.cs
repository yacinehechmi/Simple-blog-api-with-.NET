namespace Tidjma.Contracts;
using System.ComponentModel.DataAnnotations;
/*
    this DTO is used in a restirced endpoint
    that would work only if a user is authenticated

    Used for updating new Users inside a user context

    !! this will be restricted !!

    then UpdateUserDTO will be mapped into a Comment User
*/

public record class UpdateUserDTO(
        [Required] int Id,
        [Required] string Username,
        [Required] string Password,
        [Required] string Role, // this can only be changed by the admin
        [Required] string Email);

