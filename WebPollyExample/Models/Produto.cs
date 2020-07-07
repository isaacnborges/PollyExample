using System;

namespace WebPollyExample.Models
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
    }
}
