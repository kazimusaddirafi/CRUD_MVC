using CRUD_MVC.Models;
using CRUD_MVC.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD_MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext context;

        public StudentController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new StudentModel{
                Name=viewModel.Name,
                Email=viewModel.Email,
                Phone=viewModel.Phone,
                IsActive=viewModel.IsActive,
            };
            await context.Students.AddAsync(student);
            await context.SaveChangesAsync();   
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var studentList = await context.Students.ToListAsync();
            return View(studentList);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var student=await context.Students.FindAsync(id);
            return View(student);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(StudentModel viewModel)
        {
            var studentInfo = await context.Students.FindAsync(viewModel.Id);

            if(studentInfo != null)
            {
                studentInfo.Name = viewModel.Name;
                studentInfo.Email = viewModel.Email;
                studentInfo.Phone = viewModel.Phone;
                studentInfo.IsActive = viewModel.IsActive;
                await context.SaveChangesAsync();
                return RedirectToAction("List", "Student");

            }
            return View();
           
        }

        [HttpPost]
        public async Task<IActionResult>Delete(StudentModel viewModel)
        {
            var student = await context.Students.FindAsync(viewModel.Id);
            if(student != null) { 
                context.Students.Remove(student);
                await context.SaveChangesAsync();
                return RedirectToAction("List", "Student");
            }
            return RedirectToAction("Edit", "Student");
           
        }

    }
}
