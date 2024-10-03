﻿using System.ComponentModel.DataAnnotations;

namespace TheSchoolManagementSystem.Models
{
    public class Registration
    {
        public int RegistrationId { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public string? Grade { get; set; }
        [Required]
        [Range(0, 100)]
        public int? Marks { get; set; }

        public string GetLetterGrade()
        {
            if (Marks == null)
            {
                return "No Marks"; // Handle case where marks are not set
            }
            if (Marks >= 90 && Marks <= 100)
            {
                return "A";
            }
            else if (Marks >= 75 && Marks <= 89)
            {
                return "B";
            }
            else if (Marks >= 65 && Marks <= 74)
            {
                return "C";
            }
            else if (Marks >= 50 && Marks <= 64)
            {
                return "D";
            }
            else
            {
                return "F";
            }


        }
    }
}
