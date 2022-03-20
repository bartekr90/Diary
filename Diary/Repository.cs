using Diary.Model.Domains;
using Diary.Model.Wrappers;
using Diary.Model.Conventers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Diary.Model;

namespace Diary
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<StudentWrapper> GetStudents(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var students = context
                    .Students
                    .Include(x => x.Group)
                    .Include(x => x.Ratings)
                    .AsQueryable();

                if (groupId != 0)
                    students = students.Where(x => x.GroupId == groupId);

                return students
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

        public void DeleteStudent(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var studentToDelete = context.Students.Find(id);
                context.Students.Remove(studentToDelete);
                context.SaveChanges();
            }
        }

        public void AddStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var rating = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                var dbStudent = context.Students.Add(student);

                rating.ForEach(x =>
                {
                    x.StudentId = dbStudent.Id;
                    context.Ratings.Add(x);
                });
                context.SaveChanges();

            }
        }

        public void UpdateStudent(StudentWrapper studentWrapper)
        {
            var student = studentWrapper.ToDao();
            var rating = studentWrapper.ToRatingDao();

            using (var context = new ApplicationDbContext())
            {
                UpdateStudentProp(student, context);
                List<Rating> studentsRatings = GetStudentRatings(student, context);

                UpdateRate(student, rating, context, studentsRatings, Subject.Math);
                UpdateRate(student, rating, context, studentsRatings, Subject.ForeignLang);
                UpdateRate(student, rating, context, studentsRatings, Subject.Physics);
                UpdateRate(student, rating, context, studentsRatings, Subject.PolishLang);
                UpdateRate(student, rating, context, studentsRatings, Subject.Technology);

                context.SaveChanges();

            }
        }

        private static List<Rating> GetStudentRatings(Student student, ApplicationDbContext context)
        {
            return context
                .Ratings
                .Where(x => x.StudentId == student.Id)
                .ToList();
        }

        private static void UpdateStudentProp(Student student, ApplicationDbContext context)
        {
            var dbStudentToUpdate = context.Students.Find(student.Id);

            dbStudentToUpdate.FirstName = student.FirstName;
            dbStudentToUpdate.LastName = student.LastName;
            dbStudentToUpdate.Activities = student.Activities;
            dbStudentToUpdate.Comments = student.Comments;
            dbStudentToUpdate.GroupId = student.GroupId;
        }

        private static void UpdateRate(Student student, List<Rating> newRating, 
            ApplicationDbContext context, List<Rating> studentsRatings, Subject sub)
        {
            var subRating = studentsRatings
                .Where(x => x.SubjectId == (int)sub)
                .Select(x => x.Rate);

            var newSubRating = newRating
                .Where(x => x.SubjectId == (int)sub)
                .Select(x => x.Rate);

            var subRatingToDelete = subRating.Except(newSubRating).ToList();
            var subRatingToAdd = newSubRating.Except(subRating).ToList();

            subRatingToDelete.ForEach(x =>
            {
                var ratingToDelete = context.Ratings.First(y =>
                y.Rate == x &&
                y.StudentId == student.Id &&
                y.SubjectId == (int)sub);
                context.Ratings.Remove(ratingToDelete);
            });

            subRatingToAdd.ForEach(x =>
            {
                var rateToAdd = new Rating
                {
                    Rate = x,
                    StudentId = student.Id,
                    SubjectId = (int)sub
                };
                context.Ratings.Add(rateToAdd);
            });
        }
    }
}
