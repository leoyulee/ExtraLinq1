using System;
using System.Linq;
using System.Collections.Generic;

namespace ExtraLinq1
{
    class Program
    {
        // Create a data source by using a collection initializer.
        static List<Student> students = new List<Student>
    {
        new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
        new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
        new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
        new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
        new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
        new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}},
        new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}},
        new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}},
        new Student {First="Lance", Last="Tucker", ID=119, Scores= new List<int> {68, 79, 88, 92}},
        new Student {First="Terry", Last="Adams", ID=120, Scores= new List<int> {99, 82, 81, 79}},
        new Student {First="Eugene", Last="Zabokritski", ID=121, Scores= new List<int> {96, 85, 91, 60}},
        new Student {First="Michael", Last="Tucker", ID=122, Scores= new List<int> {94, 92, 91, 91}}
    };
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var studentQuery =
                from student in students
                where student.Scores[0] > 90
                select student;
            PrintQuery("For this first query, just grab all of the students with the score that is higher than 90 in their first test.", studentQuery);
            var alphebeticalStudentQuery =
                from student in studentQuery
                orderby student.Last ascending
                select student;
            PrintQuery("For this second query, grab the previous query and order it by last name", alphebeticalStudentQuery);
            var scoreStudentQuery =
                from student in alphebeticalStudentQuery
                orderby student.Scores[0] descending
                select student;
            PrintQuery("For this third query, grab the previous query and order it by score", scoreStudentQuery);
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            var groupedStudentQuery =
                from student in students
                group student by student.Last[0];
            PrintQuery("For this query, grab all students and group them by their first letter in their last name.", groupedStudentQuery);
            var alphebeticalGroupedStudentQuery =
                from studentGroup in groupedStudentQuery
                orderby studentGroup.Key
                select studentGroup;
            PrintQuery("For this next query, grab the previous query and order it alphebetically by the key.", alphebeticalGroupedStudentQuery);
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            var studentQuery2 =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] +
                    student.Scores[2] + student.Scores[3]
                where totalScore / 4 < student.Scores[0]
                select student.Last + " " + student.First;
            Console.WriteLine("For this query, select the full names of the student where their first score was higher than their average score.");
            Console.WriteLine("First Last");
            foreach (string name in studentQuery2)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            var totalScoreQuery =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] +
                    student.Scores[2] + student.Scores[3]
                select totalScore;
            double averageScore = totalScoreQuery.Average();
            Console.WriteLine("For this query, get the total scores of each student and then return the average of them.");
            Console.WriteLine("Class average score = {0}", averageScore);
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            var GarciaQuery =
                from student in students
                where student.Last == "Garcia"
                select student.First;
            Console.WriteLine("The Garcias in the class are:");
            foreach (string s in GarciaQuery)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("------------------------------------------------------------------------------------------------");
            var privateScoreQuery =
                from student in students
                let x = student.Scores[0] + student.Scores[1] +
                    student.Scores[2] + student.Scores[3]
                where x > averageScore
                select new { id = student.ID, score = x };
            foreach (var thing in privateScoreQuery)
            {
                Console.WriteLine("Student ID: {0}, Score: {1}", thing.id, thing.score);
            }
        }
        static void PrintQuery(string message, IEnumerable<Student> query)
        {
            Console.WriteLine("\n" + message);
            Console.WriteLine("ID) Last, First: Score");
            foreach (Student student in query)
            {
                Console.WriteLine("{0}) {1}, {2}: {3}", student.ID, student.Last, student.First, student.Scores[0]);
            }
        }
        static void PrintQuery(string message, IEnumerable<IGrouping<char, Student>> groupQuery)
        {
            Console.WriteLine("\n" + message);
            Console.WriteLine("Group");
            Console.WriteLine("\tID) Last, First: Score");
            foreach(var studentGroup in groupQuery)
            {
                Console.WriteLine(studentGroup.Key);
                foreach(var student in studentGroup)
                {
                    Console.WriteLine("\t{0}) {1}, {2}: {3}", student.ID, student.Last, student.First, student.Scores[0]);
                }
            }
        }
    }
}
