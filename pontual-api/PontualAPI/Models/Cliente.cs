using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PontualAPI.Models
{
    public class Cliente
    {
        [Key]
        [Required]
        public long Id { get; set; }
        
        [Required]
        [MaxLength(250, ErrorMessage = "O campo 'Nome Fantasia' não pode conter mais que 250 caracteres")]
        public string NomeFantasia { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "O campo 'Razão Social' não pode conter mais que 250 caracteres")]
        public string RazaoSocial { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage="O campo 'CNPJ' não pode conter mais que 20 caracteres")]
        public string CNPJ { get; set; }

        [Required]
        [MaxLength(150, ErrorMessage="O campo 'Email' não pode conter mais que 150 caracteres")]
        public string Email { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage= "O campo 'Telefone' não pode conter mais que 30 caracteres")]
        public string Telefone { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
