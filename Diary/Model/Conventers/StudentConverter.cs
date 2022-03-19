using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diary.Model.Domains;
using Diary.Model.Wrappers;


namespace Diary.Model.Conventers
{
    public static class StudentConverter
    {
        public static StudentWrapper ToWrapper(this Student model)
        {
            return new StudentWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Activities = model.Activities,
                Comments = model.Comments,
                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name
                },

                Math = string.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.Math)
                .Select(y => y.Rate)),

                Physics = string.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.Physics)
                .Select(y => y.Rate)),

                PolishLang = string.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.PolishLang)
                .Select(y => y.Rate)),

                Technology = string.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.Technology)
                .Select(y => y.Rate)),

                ForeignLang = string.Join(", ", model.Ratings
                .Where(y => y.SubjectId == (int)Subject.ForeignLang)
                .Select(y => y.Rate)),

            };
        }

        public static Student ToDao(this StudentWrapper model)
        {
            return new Student
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Comments = model.Comments,
                GroupId = model.Group.Id,
                Activities = model.Activities,
                
            };
        }
        public static List<Rating> ToRatingDao(this StudentWrapper model)
        {
            var ratings = new List<Rating>();


            return ratings;
        }
    }
}
