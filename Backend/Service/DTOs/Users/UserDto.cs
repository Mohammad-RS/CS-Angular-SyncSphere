﻿using System.ComponentModel.DataAnnotations;

namespace Service.DTOs.Users
{
    public class UserDto
    {
        // rxclude the unncessary ones
        public int Id { get; set; }

        [Required]
        [StringLength(32, MinimumLength = 5)]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(254)]
        public required string Email { get; set; }

        [Required]
        [MaxLength(16)]
        public required byte[] Password { get; set; }

        [StringLength(100)]
        public string? FullName { get; set; } = null;

        [StringLength(256)]
        public string? Avatar { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdateAt { get; set; } = null;

        public DateTime? LastLogin { get; set; } = null;

        public bool IsActive { get; set; } = true;

        public bool IsAdmin { get; set; } = false;
    }
}
