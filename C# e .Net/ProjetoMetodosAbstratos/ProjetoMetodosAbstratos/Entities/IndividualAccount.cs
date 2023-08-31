using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMetodosAbstratos.Entities {
    class IndividualAccount : Account {
        public double GastoSaude { get; set; }

        public IndividualAccount(string nome, double renda, double gasto) : base(nome, renda) {
            GastoSaude = gasto;
        }

        public override double CalcularImposto() {
            if (RendaAnual < 20000.00) {
                return (RendaAnual * 0.15) - (GastoSaude - 0.5);
            }else {
                return (RendaAnual * 0.25) - (GastoSaude * 0.5);
            }
        }
    }
}
