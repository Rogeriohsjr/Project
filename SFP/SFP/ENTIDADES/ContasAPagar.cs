using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFP
{
    public class ContasAPagar
    {
        private DateTime? _dataPagamento;

        public Int32 IdContasAPagar { get; set; }
        public String Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        public Decimal ValorTotal { get; set; }
        public DateTime? DataPagamento
        {
            get { return _dataPagamento; }
            set {
                if(("01/01/1900 00:00:00".Equals(value.ToString())) || (String.IsNullOrWhiteSpace(value.ToString())))
                    this._dataPagamento = null;
                else

                    this._dataPagamento = value;
            }
        }
        public Decimal ValorPago { get; set; }
        public String Documento { get; set; }
        public String Historico { get; set; }
        public Categoria Categoria { get; set; }
        public Usuario Usuario { get; set; }

        public String CategoriaDescricao
        {
            get
            {
                if (Categoria == null)
                    return "";
                else
                    return Categoria.Descricao;
            }
        }
        public String DataPagamentoFormated
        {
            get
            {
                if (_dataPagamento == null)
                    return "";
                else
                    return _dataPagamento.Value.ToShortDateString();

            }
        }


    }
}