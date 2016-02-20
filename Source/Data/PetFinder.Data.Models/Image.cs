﻿namespace PetFinder.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    
    public class Image : BaseModel<int>
    {
        public byte[] Content { get; set; }

        [Required]
        public string FileExtension { get; set; }
    }
}
