namespace ProjetoMetodosAbstratos.Entities {
    class CompanyAccount : Account {
        public int NumeroFuncionarios { get; set; }

        public CompanyAccount(string nome, double gastos, int funcionarios) : base(nome, gastos) {
            NumeroFuncionarios = funcionarios;
        }

        public override double CalcularImposto() {
            if (NumeroFuncionarios <= 10) {
                return RendaAnual * 0.16;
            } else {
                return RendaAnual * 0.14;
            }
        }
    }
}
