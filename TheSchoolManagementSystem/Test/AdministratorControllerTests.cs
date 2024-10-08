using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using TheSchoolManagementSystem.Controllers;
using TheSchoolManagementSystem.Data;
using TheSchoolManagementSystem.Models;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace TheSchoolManagementSystem.Tests
{
    public class AdministratorControllerTests
    {
        private readonly ApplicationDbContext _context;
        private readonly AdministratorController _controller;

        public AdministratorControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase_" + Guid.NewGuid()) // Unique name for each test run
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new AdministratorController(_context);
        }

        [Fact]
        public async Task CreateStudent_ValidModel_RedirectsToStudents()
        {
            // Arrange
            var student = new Student { StudentId = 1, FirstName = "John", LastName = "Doe" };

            // Act
            var result = await _controller.CreateStudent(student) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Students", result.ActionName);
            Assert.Single(await _context.Students.ToListAsync()); // Check if the student was added
        }

        [Fact]
        public async Task EditStudent_ValidModel_RedirectsToStudents()
        {
            // Arrange
            var student = new Student { StudentId = 1, FirstName = "John", LastName = "Doe" };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            var updatedStudent = new Student { StudentId = 1, FirstName = "John", LastName = "Smith" };

            // Act
            var result = await _controller.EditStudent(1, updatedStudent) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Students", result.ActionName);
            var studentInDb = await _context.Students.FindAsync(1);
            Assert.Equal("Smith", studentInDb.LastName); // Check if the last name was updated
        }

        [Fact]
        public async Task DeleteStudentConfirmed_ExistingStudent_RedirectsToStudents()
        {
            // Arrange
            var student = new Student { StudentId = 1, FirstName = "John", LastName = "Doe" };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            // Act
            var result = await _controller.DeleteStudentConfirmed(student) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Students", result.ActionName);
            Assert.Empty(await _context.Students.ToListAsync()); // Check if the student was deleted
        }

        // Additional test methods can go here...
    }
}
