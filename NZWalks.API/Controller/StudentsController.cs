using Microsoft.AspNetCore.Mvc;

//here Students is a Resource.
namespace NZWalks.API.Controller
{
    [ApiController]
    //http://localhost:portnumber/api/students
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        //using HTTP get Attribute
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] students = new string[]
            {
                "Naruto Uzumaki",
                "Sakura Haruno",
                "Sasuke Uchiha",
                "Shikamaru Nara",
                "Hinata Hyuga"
            };
            //Ok is used to send statuscode as 200
            return Ok(students);
        }
    }
}
