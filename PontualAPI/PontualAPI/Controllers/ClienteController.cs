using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PontualAPI.Data;
using PontualAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PontualAPI.Controllers
{
    [ApiController]
    [Route("/v1/clientes")]
    [EnableCors("EnableAllCrossOriginRequests")]
    public class ClienteController : Controller
    {
        protected PontualDataContext dataContext;
        public ClienteController([FromServices] PontualDataContext pontualDataContext)
        {
            dataContext = pontualDataContext;
        }

        /// <summary>
        /// Método que retorna todos os clientes do banco, sem filtro
        /// </summary>
        /// <returns>Lista de todos os clientes no banco</returns>
        [HttpGet]
        [Route("")]
        public ActionResult<List<Cliente>> GetAll()
        {
            return dataContext.Clientes.ToList();
        }

        /// <summary>
        /// Método que retorna todos os clientes do banco, filtrando pelo Nome Fantasia ou Razao Social
        /// </summary>
        /// <param name="nome">Nome Fantasia ou Razao Social do Cliente</param>
        /// <returns>Lista de todos os clientes no banco que contenham no Nome Fantasia/Razao Social o texto informado</returns>
        [HttpGet]
        [Route("nome/{nome}")]
        public ActionResult<List<Cliente>> GetAllByName(string nome)
        {
            return dataContext
                .Clientes
                .Where(e => e.NomeFantasia.Contains(nome) || e.RazaoSocial.Contains(nome))
                .OrderByDescending(e => e.NomeFantasia)
                .ThenBy(e => e.RazaoSocial)
                .ToList();
        }

        /// <summary>
        /// Método que retorna um único cliente de acordo com seu Id
        /// </summary>
        /// <param name="id">Id do Cliente</param>
        /// <returns>Lista de todos os clientes no banco com o Nome Fantasia especificado</returns>
        [HttpGet]
        [Route("id/{id}")]
        public ActionResult<Cliente> GetById(long id)
        {
            return dataContext.Clientes.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Método para inserir os dados um cliente
        /// </summary>
        /// <param name="cliente">Informações do Cliente</param>
        /// <returns>Objeto de cliente enviado na requisição</returns>
        [HttpPost]
        [Route("cadastrar")]
        public ActionResult<Cliente> Insert([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var validacao = ValidaCampos(cliente);
                if (validacao.Any())
                    return BadRequest(validacao.Select(e => e.ErrorMessage).Aggregate((a, x) => a + "\n" + x));

                dataContext.Clientes.Add(cliente);
                dataContext.SaveChanges();
                return Ok(cliente);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Método para alterar os dados um cliente
        /// </summary>
        /// <param name="cliente">Informações do Cliente</param>
        /// <returns>Objeto de cliente enviado na requisição</returns>
        [HttpPut]
        [Route("alterar")]
        public ActionResult<Cliente> Update([FromBody] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                var clienteDb = dataContext.Clientes.Where(e => e.Id == cliente.Id)?.FirstOrDefault();
                if (clienteDb == null)
                    return BadRequest("O Id de cliente informado não existe.");
                var validacao = ValidaCampos(cliente);
                if (validacao.Any())
                    return BadRequest(validacao.Select(e => e.ErrorMessage).Aggregate((a, x) => a + "\n" + x));

                clienteDb.NomeFantasia = cliente.NomeFantasia;
                clienteDb.RazaoSocial = cliente.RazaoSocial;
                clienteDb.CNPJ = cliente.CNPJ;
                clienteDb.Email = cliente.Email;
                clienteDb.Telefone = cliente.Telefone;

                dataContext.Clientes.Update(clienteDb);
                dataContext.SaveChanges();

                return Ok(cliente);
            }
            else
            {
                return BadRequest(cliente);
            }
        }

        /// <summary>
        /// Método para excluir os dados um cliente
        /// </summary>
        /// <param name="idCliente">Id do Cliente</param>
        /// <returns>Código REST 200 ou 400</returns>
        [HttpDelete]
        [Route("excluir")]
        public ActionResult<Cliente> Remove([FromBody] Cliente cliente)
        {
            var clienteDb = dataContext.Clientes.Where(e => e.Id == cliente.Id)?.FirstOrDefault();
            if (clienteDb == null)
                return BadRequest("O Id de cliente informado não existe.");

            dataContext.Clientes.Remove(clienteDb);
            dataContext.SaveChanges();

            return Ok();
        }

        private IEnumerable<ValidationResult> ValidaCampos(Cliente cliente)
        {
            var validationResult = new List<ValidationResult>();
            if (dataContext.Clientes.Any(e => e.CNPJ == cliente.CNPJ && e.Id != cliente.Id))
                validationResult.Add(new ValidationResult("O CNPJ informado já está em uso"));
            return validationResult;
        }

    }
}
