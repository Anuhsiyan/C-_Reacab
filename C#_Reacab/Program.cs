using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Contexts;
using System.Collections;

namespace C__Reacab
{
    internal class Program
    {
        static string ConnectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = c#_Reacab_Database; Integrated Security = True;";
        //static void Main(string[] args)
        //{
        //    bool showMenu = true; // control the display of the menu
        //    while (showMenu)
        //    {
        //        Console.WriteLine("\n Menu");
        //        Console.WriteLine(" 1. Add Course");
        //        Console.WriteLine(" 2. View Courses");
        //        Console.WriteLine(" 3. Update Course");
        //        Console.WriteLine(" 4. Delete Course");
        //        Console.WriteLine(" 5. Exit");
        //        Console.Write("Select an option: ");
        //        int choice = Convert.ToInt32(Console.ReadLine());
        //        Console.WriteLine();

        //        switch (choice)
        //        {
        //            case 1:
        //                AddCourse();
        //                break;
        //            case 2:
        //                ViewCourse();
        //                break;
        //            case 3:
        //                UpdateCourse();
        //                break;
        //            case 4:
        //                Deletecourse();
        //                break;
        //            case 5:

        //                Console.WriteLine("Thank you for your visiting.");
        //                showMenu = false; // Hide the menu when showMenu is false
        //                break;
        //            default:
        //                Console.WriteLine("Invalid choice. Please Try Again.");
        //                break;
        //        }
        //    }

        //  Console.ReadLine();
        //}


       

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("---**Main Menu**---");
                Console.WriteLine();
                Console.WriteLine("1. Course Menu");
                Console.WriteLine("2. Student Menu");
                Console.WriteLine("3. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CourseMenu();
                        break;
                    case "2":
                        StudentMenu();
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.WriteLine("Thank You for Your Visiting.");
                        running = false;
                    
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
            Console.ReadKey();
        }

        static void CourseMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("--**Course Menu**--");
                Console.WriteLine();    
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. View Courses");
                Console.WriteLine("3. Update Course");
                Console.WriteLine("4. Delete Course");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddCourse();
                        break;
                    case "2":
                        ViewCourse();
                        break;
                    case "3":
                        UpdateCourse();
                        break;
                    case "4":
                        Deletecourse();
                        break;
                    case "5":
                        
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
          
        }

        static void StudentMenu()
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("--**Student Menu**--");
                Console.WriteLine();
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. View Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Back to Main Menu");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        AddStudent();
                        break;
                    case "2":
                        ViewStudents();
                        break;
                    case "3":
                        UpdateStudent();
                        break;
                    case "4":
                        DeleteStudent();
                        break;
                    case "5":
                       
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
         
        }
        static void AddCourse()
        {

            Console.Write("Enter Course ID: ");
            int courseId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Course Name: ");
            string courseName = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {

                conn.Open();
                string query = "INSERT INTO Courses (CourseID, CourseName) VALUES (@CourseID, @CourseName)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.Parameters.AddWithValue("@CourseName", courseName);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Course added successfully.");
                        Console.Write("Press Enter to Continue..........");
                        Console.ReadKey();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627) // Unique constraint error
                        {
                            Console.WriteLine("A course with this name already exists.");
                        }
                        else
                        {
                            // Handle other SQL errors
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }
                    }


                }
            }
        }

        static void ViewCourse()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                
               string query = "SELECT * FROM Courses";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Course ID: {reader["CourseID"]},\n Course Name: {reader["CourseName"]}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    Console.Write("Press Enter to Continue..........");
                    Console.ReadKey();
                }
            }
        }

        static void UpdateCourse()
        {
            Console.Write("Enter Course ID to update: ");
            int courseId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter New Course Name: ");
            string NewcourseName = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Courses SET CourseName = @CourseName WHERE CourseID = @CourseID";
               
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID", courseId);
                    cmd.Parameters.AddWithValue("@CourseName", NewcourseName);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if(rowsaffected > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Course updated successfully.");
                        Console.Write("Press Enter to Continue..........");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Course Not Found.");
                    }
                }
            }
        }

        static void Deletecourse()
        {
            Console.Write("Enter Your ID to delete: "); 
            int courseid = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Courses WHERE CourseID = @CourseID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CourseID",courseid);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if(rowsaffected > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Course Record Deleted successfully.");
                        Console.Write("Press Enter to Continue..........");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Course not found.");
                    }
                }                                                
            }
        }

        static void AddStudent()
        {
            
            Console.Write("Enter Student ID: ");
            int studentId = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Your FirstName: ");
            string FirstName = Console.ReadLine();
            Console.Write("Enter Your LastName: ");
            string LastName = Console.ReadLine();
            Console.Write("Enter the city: ");
            string City = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Students (StudentID, FirstName,LastName,City) VALUES (@StudentID, @FirstName,@LastName,@City)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@StudentID", studentId);
                    cmd.Parameters.AddWithValue("@FirstName", FirstName);
                    cmd.Parameters.AddWithValue("@LastName", LastName);
                    cmd.Parameters.AddWithValue("@City", City);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Student added successfully.");         
                        Console.Write("Press Enter to Continue..........");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("No rows affected. The student might not have been added.");
                    }
                  
                   







                }
            }
        }
        static void ViewStudents()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Students";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"StudentID: {reader["StudentID"]}.\n FirstName: {reader["FirstName"]}.\n LastName: {reader["LastName"]}.\n City: {reader["City"]}");
                        Console.WriteLine();
                    }
                    Console.WriteLine();                   
                    Console.Write("Press Enter to Continue..........");
                    Console.ReadKey();
                }
            }
        }

        static void UpdateStudent()
        {
            Console.Write("Enter Student ID to update: ");
            int stduentid = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter Your New City: ");
            string city = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "UPDATE Students SET City = @City WHERE StudentID = @StudentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", stduentid);
                    cmd.Parameters.AddWithValue("@City", city);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Student updated successfully.");
                        Console.Write("Press Enter to Continue..........");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Student Not Found.");
                    }
                }
            }
        }

        static void DeleteStudent()
        {
            Console.Write("Enter Your StudentID to delete: ");
            int StudentID = Convert.ToInt32(Console.ReadLine());
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "DELETE FROM Students WHERE StudentID = @StudentID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentID", StudentID);
                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Student Record Deleted successfully.");
                        Console.Write("Press Enter to Continue..........");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Student not found.");
                    }
                }
            }
        }


    }
    
}






