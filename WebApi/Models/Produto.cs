using System;

namespace WebApi.Models
{
    public class Produto
    {
        public Guid Id { get; private set; }
        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }

        public Produto(Guid id, int codigo, string descricao, decimal valor)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Valor = valor;
        }

        public Produto InformarCodigo(int codigo)
        {
            Codigo = codigo;
            return this;
        }

        public Produto InformarDescricao(string descricao)
        {
            Descricao = descricao;
            return this;
        }

        public Produto InformarValor(decimal valor)
        {
            Valor = valor;
            return this;
        }
    }
}
