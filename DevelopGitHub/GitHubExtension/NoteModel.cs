using System;
using System.ComponentModel.DataAnnotations;

public class NoteModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string CollaboratorId { get; set; }
    
    [Required]
    public string Body { get; set; }
}
