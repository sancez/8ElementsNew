using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {        
  
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            using (var context = new BookStoresDBContext())
            {
                //Get All Authors -> this is the first step
                //return context.Author.Tolist();

                //get author by id -> this is the second
                //return context.Author.Where(x => x.AuthorId == 1).ToList();

                //add a author ->this is the three add Data
                //Author author = new Author(); 
                //author.FirstName = "Sance";
                //author.LastName = "Aenul Yakin";
                //context.Author.Add(author);
                //context.SaveChanges();                
                //return context.Author.Where(x => x.FirstName == "Sance").ToList();

                //this is the four -> for edit or update data using where
                //Author author = context.Author.Where(z => z.Firstname == "sance").FirstOrDefault();
                //author.Phone = "999-999-999";
                //context.SaveChanges();
                //return context.Author.Where(kl => kl.FirstName == "sance").ToList();

                //remove author
                Author author = context.Author.Where(i => i.Firstname = "sance").FirstOrDefault();
                Content.Author.Remove(author);
                context.SaveChanges();
                return Context.Author.where(u => u.FirstName =="sance").ToList();
                
            }
        }
    }
}
