using GestionCentreDeFormation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GestionCentreDeFormation.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            // 1. Seed Roles (Always check this, even if DB is seeded)
            string[] roleNames = { "Admin", "Student", "Instructor" };
            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Look for any courses.
            if (context.Courses.Any())
            {
                return;   // DB has been seeded
            }

            // 2. Seed Instructors
            var instructors = new Instructor[]
            {
                new Instructor { Name = "Dr. Sarah Connor", Title = "Senior AI Researcher", Bio = "Expert in Artificial Intelligence and Neural Networks with 15 years of experience.", ImageUrl = "https://images.unsplash.com/photo-1573496359142-b8d87734a5a2?w=800&q=80" },
                new Instructor { Name = "Prof. Alan Turing", Title = "Computer Scientist", Bio = "Pioneer of theoretical computer science and artificial intelligence.", ImageUrl = "https://images.unsplash.com/photo-1560250097-0b93528c311a?w=800&q=80" },
                new Instructor { Name = "Emily Chen", Title = "Cloud Architect", Bio = "Certified AWS and Azure Solutions Architect specializing in scalable infrastructure.", ImageUrl = "https://images.unsplash.com/photo-1580489944761-15a19d654956?w=800&q=80" },
                new Instructor { Name = "Marcus Rodriguez", Title = "Cybersecurity Expert", Bio = "Former ethical hacker now teaching network security and threat analysis.", ImageUrl = "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?w=800&q=80" },
                new Instructor { Name = "Lisa Wong", Title = "Full Stack Developer", Bio = "Specializes in MERN stack and modern web technologies.", ImageUrl = "https://images.unsplash.com/photo-1594744803329-e58b31de8bf5?w=800&q=80" }
            };

            foreach (var i in instructors)
            {
                context.Instructors.Add(i);
            }
            await context.SaveChangesAsync();

            // 3. Seed Courses
            var courses = new Course[]
            {
                // Development
                new Course { 
                    Title = "Full Stack Web Development", 
                    Description = "Master the MERN stack (MongoDB, Express, React, Node.js) and build modern web applications.", 
                    Price = 1200, 
                    Capacity = 25, 
                    StartDate = DateTime.Now.AddDays(14), 
                    EndDate = DateTime.Now.AddDays(104), 
                    ImageUrl = "https://images.unsplash.com/photo-1633356122544-f134324a6cee?w=800&q=80",
                    WhatYoullLearn = "React Hooks, Redux, Node.js API, MongoDB Aggregation",
                    InstructorId = instructors[4].Id 
                },
                new Course { 
                    Title = "Advanced C# & .NET Core", 
                    Description = "Deep dive into C# 12, .NET 8, Entity Framework Core, and Microservices architecture.", 
                    Price = 1500, 
                    Capacity = 20, 
                    StartDate = DateTime.Now.AddDays(7), 
                    EndDate = DateTime.Now.AddDays(67), 
                    ImageUrl = "https://images.unsplash.com/photo-1517694712202-14dd9538aa97?w=800&q=80",
                    WhatYoullLearn = "LINQ, Async/Await, Dependency Injection, Clean Architecture",
                    InstructorId = instructors[1].Id 
                },
                // Data Science
                new Course { 
                    Title = "Data Science with Python", 
                    Description = "Learn data analysis, visualization, and machine learning using Pandas, NumPy, and Scikit-learn.", 
                    Price = 1800, 
                    Capacity = 30, 
                    StartDate = DateTime.Now.AddDays(21), 
                    EndDate = DateTime.Now.AddDays(111), 
                    ImageUrl = "https://images.unsplash.com/photo-1551288049-bebda4e38f71?w=800&q=80",
                    WhatYoullLearn = "Data Cleaning, Matplotlib, Regression, Classification",
                    InstructorId = instructors[0].Id 
                },
                // Cybersecurity
                new Course { 
                    Title = "Ethical Hacking & Security", 
                    Description = "Understand network security, penetration testing, and how to defend against cyber attacks.", 
                    Price = 2000, 
                    Capacity = 15, 
                    StartDate = DateTime.Now.AddDays(10), 
                    EndDate = DateTime.Now.AddDays(70), 
                    ImageUrl = "https://images.unsplash.com/photo-1550751827-4bd374c3f58b?w=800&q=80",
                    WhatYoullLearn = "Kali Linux, Metasploit, Wireshark, Network Scanning",
                    InstructorId = instructors[3].Id 
                },
                // Cloud
                new Course { 
                    Title = "AWS Certified Solutions Architect", 
                    Description = "Prepare for the AWS certification with hands-on labs and real-world scenarios.", 
                    Price = 2200, 
                    Capacity = 20, 
                    StartDate = DateTime.Now.AddDays(30), 
                    EndDate = DateTime.Now.AddDays(120), 
                    ImageUrl = "https://www.simplilearn.com/ice9/free_resources_article_thumb/cloudtools_b.jpg",
                    WhatYoullLearn = "EC2, S3, VPC, Lambda, IAM",
                    InstructorId = instructors[2].Id 
                }
            };

            foreach (var c in courses)
            {
                context.Courses.Add(c);
            }
            await context.SaveChangesAsync();

            // 4. Seed Students
            var students = new[]
            {
                new { First = "Alice", Last = "Smith", Email = "alice@example.com" },
                new { First = "Bob", Last = "Johnson", Email = "bob@example.com" },
                new { First = "Charlie", Last = "Williams", Email = "charlie@example.com" },
                new { First = "Diana", Last = "Brown", Email = "diana@example.com" },
                new { First = "Evan", Last = "Jones", Email = "evan@example.com" },
                new { First = "Fiona", Last = "Miller", Email = "fiona@example.com" },
                new { First = "George", Last = "Davis", Email = "george@example.com" },
                new { First = "Hannah", Last = "Garcia", Email = "hannah@example.com" },
                new { First = "Ian", Last = "Rodriguez", Email = "ian@example.com" },
                new { First = "Julia", Last = "Wilson", Email = "julia@example.com" }
            };

            foreach (var s in students)
            {
                if (await userManager.FindByEmailAsync(s.Email) == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = s.Email,
                        Email = s.Email,
                        FirstName = s.First,
                        LastName = s.Last,
                        EmailConfirmed = true,
                        Balance = 2000 // Initial Balance
                    };
                    await userManager.CreateAsync(user, "Password@123");
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }

            // 5. Seed Enrollments
            var allStudents = await userManager.GetUsersInRoleAsync("Student");
            var allCourses = await context.Courses.ToListAsync();
            var random = new Random();

            foreach (var student in allStudents)
            {
                // Enroll each student in 1-2 random courses
                int enrollmentsCount = random.Next(1, 3);
                for (int i = 0; i < enrollmentsCount; i++)
                {
                    var course = allCourses[random.Next(allCourses.Count)];
                    
                    // Check if already enrolled
                    if (!context.Enrollments.Any(e => e.StudentId == student.Id && e.CourseId == course.Id))
                    {
                        // Deduct balance if enough funds (simulating the logic)
                        if (student.Balance >= course.Price)
                        {
                            student.Balance -= course.Price;
                            
                            var enrollment = new Enrollment
                            {
                                StudentId = student.Id,
                                CourseId = course.Id,
                                EnrollmentDate = DateTime.UtcNow.AddDays(-random.Next(1, 30)), // Enrolled in the last 30 days
                                Status = EnrollmentStatus.Active
                            };
                            context.Enrollments.Add(enrollment);
                        }
                    }
                }
            }
            await context.SaveChangesAsync();
        }
    }
}
