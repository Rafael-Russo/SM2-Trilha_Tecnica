namespace ProjetoMetodosAbstratos.Entities {
    abstract class Account {
        public string Nome { get; set; }
        public double RendaAnual { get; set; }

        public Account(string nome, double renda) {
            Nome = nome;
            RendaAnual = renda;
        }
        public abstract double CalcularImposto();
    }
}
