using CadastroAluno.Data;
using CadastroAluno.Models;
using CadastroAluno.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace CadastroAluno.Controllers
{
    public class AlunosController : Controller
    {
        private readonly MvcDbContext mvcDbContext;

        public AlunosController(MvcDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AlunoViewModel addAlunoRequest)
        {
            var aluno = new Aluno()
            {
                Id = Guid.NewGuid(),
                Nome = addAlunoRequest.Nome,
                Email = addAlunoRequest.Email,
                DataNascimento = addAlunoRequest.DataNascimento
            };

            await mvcDbContext.Alunos.AddAsync(aluno);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]

        public async Task<IActionResult> Index()
        {
            var alunos = await mvcDbContext.Alunos.ToListAsync();
            return View(alunos);
        }

        [HttpGet]

        public async Task<IActionResult> Ver(Guid id)
        { 
            var aluno = await mvcDbContext.Alunos.FirstOrDefaultAsync(x => x.Id == id);

            if (aluno != null)
            {
                var verModel = new UpdateAlunoViewModel()
                {
                    Id = aluno.Id,
                    Nome = aluno.Nome,
                    Email = aluno.Email,
                    DataNascimento = aluno.DataNascimento
                };

                return await Task.Run(() => View("Ver", verModel));

            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Ver(UpdateAlunoViewModel model)
        {
            var aluno = await mvcDbContext.Alunos.FindAsync(model.Id);

            if (aluno != null)
            {
                aluno.Nome = model.Nome;
                aluno.Email = model.Email;
                aluno.DataNascimento = model.DataNascimento;

                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(UpdateAlunoViewModel model)
        {
           var aluno = await mvcDbContext.Alunos.FindAsync(model.Id);

            if (aluno != null)
            {
                mvcDbContext.Alunos.Remove(aluno);
                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
