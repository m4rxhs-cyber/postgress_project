using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using conexaoPostgre.Models;
 
namespace conexaoPostgre.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ColaboradorController : ControllerBase
    {
        private BDContexto contexto;
 
        public ColaboradorController(BDContexto bdContexto)
        {
            contexto = bdContexto;
        }
 
        [HttpGet]
        public List<Colaborador> Listar()
        {
            return contexto.Colaboradores.Include(c => c.Cargo).OrderBy(c => c.Id).Select
                (
                    c => new Colaborador 
                    { 
                        Id = c.Id,
                        Nome = c.Nome,
                        Salario = c.Salario,
                        IdCargo = c.IdCargo,
                        Cargo = new Cargo 
                        { 
                            Id = c.Cargo.Id, 
                            Nome = c.Cargo.Nome,
                            Tipo = c.Cargo.Tipo,
                            SalarioMinimo = c.Cargo.SalarioMinimo, 
                            SalarioMaximo = c.Cargo.SalarioMaximo 
                        } 
                    }
                ).ToList();
            }
 
        [HttpGet]
        public List<Colaborador> ListarPorFaixa(double valorInicial, double valorFinal)
        {
            return contexto.Colaboradores.Where(c => c.Salario >= valorInicial && c.Salario <= valorFinal).Select
            (
                c => new Colaborador 
                { 
                    Id = c.Id,
                    Nome = c.Nome,
                    Salario = c.Salario,
                    Cargo = new Cargo 
                    { 
                        Id = c.Cargo.Id, 
                        Nome = c.Cargo.Nome,
                        Tipo = c.Cargo.Tipo,
                        SalarioMinimo = c.Cargo.SalarioMinimo, 
                        SalarioMaximo = c.Cargo.SalarioMaximo                     
                    } 
                }).ToList();
        }
 
        [HttpGet]
        public List<Colaborador> ListarPorCargo(string cargo)
        {
            return contexto.Colaboradores.Where(c => c.Cargo.Nome == cargo).Select
            (
                c => new Colaborador 
                { 
                    Id = c.Id,
                    Nome = c.Nome,
                    Salario = c.Salario,
                    Cargo = new Cargo 
                    { 
                        Id = c.Cargo.Id, 
                        Nome = c.Cargo.Nome,
                        Tipo = c.Cargo.Tipo,
                        SalarioMinimo = c.Cargo.SalarioMinimo, 
                        SalarioMaximo = c.Cargo.SalarioMaximo                     
                    } 
                }).ToList();
        }
 
         [HttpPost]
        public string Cadastrar([FromBody]Colaborador dados)
        {
            contexto.Add(dados);
            contexto.SaveChanges();
            
            return "Colaborador cadastrado com sucesso!";
        }
 
        [HttpDelete]
        public string Excluir([FromBody]int id)
        {
            Colaborador? dados = contexto.Colaboradores.FirstOrDefault(p => p.Id == id);
 
            if (dados == null)
            {
                return "Não foi encontrado Colaborador para o ID informado!";
            }
            else
            {
                contexto.Remove(dados);
                contexto.SaveChanges();
            
                return "Colaborador excluído com sucesso!";
            }
        }
 
        [HttpGet]
        public Colaborador? Visualizar(int id)
        {
            return contexto.Colaboradores.Include(p => p.Cargo)
            .Select(c => new Colaborador 
            { 
                Id = c.Id,
                Nome = c.Nome,
                Salario = c.Salario,
                Cargo = new Cargo 
                { 
                    Id = c.Cargo.Id, 
                    Nome = c.Cargo.Nome 
                } 
            }).FirstOrDefault(p => p.Id == id);
        }
 
        [HttpPut]
        public string Alterar([FromBody]Colaborador dados)
        {
            contexto.Update(dados);
            contexto.SaveChanges();
            
            return "Colaborador alterado com sucesso!";
        }
    }
}