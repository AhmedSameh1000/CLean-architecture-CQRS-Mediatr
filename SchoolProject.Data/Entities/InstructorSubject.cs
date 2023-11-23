﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolProject.Data.Entities
{
    public class InstructorSubjects
    {
        public int InstructorId { get; set; }

        public int SubjectId { get; set; }

        [ForeignKey(nameof(InstructorId))]
        public Instructor Instructor { get; set; }

        [ForeignKey(nameof(SubjectId))]
        public Subject Subject { get; set; }
    }
}