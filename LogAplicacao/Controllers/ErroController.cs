using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LogAplicacao.Controllers
{
    [ApiController]
    public class ErroController : ControllerBase
    {
        private readonly Conexoes.SqlServer _sql;

        public ErroController()
        {
            _sql = new Conexoes.SqlServer();
        }
        [HttpPost("v1/CatalogarErro")]
        public IActionResult CatalogarErro(Entidades.Erro erro)
        {
            if (erro.MensagemErro.Length <= 0 || erro.MensagemErro.Length >1000)
            {
                return StatusCode(400, "Tamanho de mensagem de erro inválida!");
            }
            if (erro.RastreioErro.Length <= 0 || erro.RastreioErro.Length > 3000)
            {
                return StatusCode(400, "Tamanho de rastreio de erro inválido!");
            }
            if (erro.NomeMaquina.Length <= 0 || erro.NomeMaquina.Length > 80)
            {
                return StatusCode(400, "Tamanho de nome da máquina inválido!");
            }
            if (erro.NomeAplicacao.Length <= 0 || erro.NomeMaquina.Length > 80)
            {
                return StatusCode(400, "Tamanho de nome de aplicação inválido!");
            }
            if (erro.Usuario.Length <= 0 || erro.Usuario.Length > 80)
            {
                return StatusCode(400, "Tamanho de nome de Usuário inválido!");
            }
            try
            {
                _sql.CatalogarErro(erro);
            }
            catch (InvalidOperationException)
            {
                return StatusCode(400, "Dados incorretos!");
            }
            catch (Exception)
            {
                return StatusCode(500, "Algo deu errado!");
            }
            return StatusCode(200);
        }
    }
}
